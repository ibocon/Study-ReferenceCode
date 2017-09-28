var path = require('path');
var fs = require('fs');
var mongoose = require('mongoose');
var request = require('request');
var FeedParser = require('feedparser');
var async = require('async');
var https = require('https');
var webTorrent = require('webtorrent');

//set up
var Animation = mongoose.model('Animation');

//Global variables
var DOWNLOAD_DIR = path.join(__dirname,'..','/public/videos/');

//Helper function
String.prototype.replaceAll = function(search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

function downloadTorrent(downloadLink, TeamName, AnimationTitle, AnimationEpisode, finish){

    var filePath = 'videos/' + AnimationTitle.replaceAll(' ', '')+"/" + AnimationTitle.replaceAll(' ', '') + "-" + AnimationEpisode + ".mp4";

    async.series({
        directory: function(callback){
            var directoryPath = DOWNLOAD_DIR+AnimationTitle.replaceAll(' ', '');
            if (!fs.existsSync(directoryPath)){
			    fs.mkdirSync(directoryPath);
			}
            callback(null, directoryPath);
        },
        media: function(callback){
            var mediaPath = DOWNLOAD_DIR+AnimationTitle.replaceAll(' ', '')+"/"+AnimationTitle.replaceAll(' ', '') + "-" + AnimationEpisode + ".mp4";
            fs.access(mediaPath, fs.constants.F_OK, function(err){
				if(err){
					if(err.code === "ENOENT"){
						callback(null, false);
					}
					else{
						throw err;
					}
				}
				else{
					callback(null, true);
				}
			});
        },
        torrent: function(callback){
            var torrentPath = DOWNLOAD_DIR+AnimationTitle.replaceAll(' ', '')+"/"+AnimationTitle.replaceAll(' ', '') + "-" + AnimationEpisode + ".torrent";
			fs.access(torrentPath, fs.constants.F_OK, function(err){
				if(err){
					if(err.code === "ENOENT"){
						var writer = fs.createWriteStream(torrentPath);
						https.get(downloadLink, function(res) {
							res.on('data', function(data) {
								writer.write(data);
							});
							res.on('end', function(){
								callback(null, torrentPath);
							});
						});
					}
					else{
						throw err;
					}
				}
				else{
					callback(null, torrentPath);
				}
			});
        }
    },
    function(err, results){
        if(err) {throw err;}
        console.log('Download '+AnimationTitle + " - " + AnimationEpisode + '\tmedia = ' + results.media );
        var client = new webTorrent();
        if(results.media === false)
		{
			async.retry(
				{interval:5000},
				function download(cb, res){
					client.add(results.torrent, {path: results.directory}, function ontorrent(torrent){

                        var interval = setInterval(function () {
                            console.log('Progress of '+ AnimationTitle + ' - ' + AnimationEpisode +' : ' + (torrent.progress * 100).toFixed(1) + '%');
                        }, 30000);

						torrent.on('done', function(){
							var oldPath = DOWNLOAD_DIR + AnimationTitle.replaceAll(' ', '')+"/" + torrent.files[0].name;
                            var newPath = DOWNLOAD_DIR + AnimationTitle.replaceAll(' ', '')+"/" + AnimationTitle.replaceAll(' ', '') + "-" + AnimationEpisode + ".mp4";
                            fs.rename(oldPath, newPath);
                            clearInterval(interval);
                            console.log('\nFinished to download ' + AnimationTitle + ' - ' + AnimationEpisode +'\n' );
							cb(null, filePath);
						});

						torrent.on('error', function(err){
							if(err) {cb(null, false);}
						});
					});

					client.on('error', function(err){
                        if(err) cb(err, false);
					});
				},
				function(err, results){
					if(err) {throw err;}
                    return finish(null, results);
				}
			);
		}
        else{
            finish(null, filePath);
        }
    });
}

function getEpiNum(feedTitle, TeamName, AnimationTitle){
    var titleStart = feedTitle.indexOf("]") + 1;
	var titleEnd; //it is decided depending on RSS feed title format.
	if(feedTitle.search("RAW") !== -1)
	{
		titleEnd = feedTitle.search("RAW");
	}
	else if(feedTitle.search("END") !== -1)
	{
		titleEnd = feedTitle.search("END");
	}
	else
	{
		titleEnd = feedTitle.indexOf("(");
	}

	//parsedTitle have information about Episode or OP, ED.

	var parsedTitle = feedTitle.slice(titleStart, titleEnd);
	parsedTitle = parsedTitle.replace(new RegExp(AnimationTitle, "ig"), "").trim();
	parsedTitle = parsedTitle.split(" ");
    //console.log('getEpiNum - parsedTitle = ' + parsedTitle);

	if(parsedTitle[0] === '-')
	{
		return parseFloat(parsedTitle[1]);
	}
	else
	{
		return false;
	}
}

function recordAni(feed, TeamName, AnimationTitle){

    var AnimationEpisode = getEpiNum(feed.title, TeamName, AnimationTitle);
    //console.log('AnimationEpisode = ' + feed.title + '\t' + AnimationEpisode);
    if(AnimationEpisode === false) {return;}
    var date = new Date(feed.date);
    downloadTorrent(feed.link, TeamName, AnimationTitle, AnimationEpisode, function(err, path){
        if(err) throw err;
        if(path === false) throw new Error("NO File Path");

        var episode = {
            number: AnimationEpisode,
            date: date,
            video: path,
            tumbnail: null,
            subtitle: null
        };

        console.log("Recording ["+TeamName+"]"+AnimationTitle+" - "+AnimationEpisode);

        Animation.findOneAndUpdate({ title: AnimationTitle, team: TeamName, 'episodes.number': {$ne: episode.number} }, { $addToSet : {'episodes' : episode}}, {new: true}, function(err, result){
            console.log('Update Animation [' +TeamName+'] ' + AnimationTitle + ' - ' + AnimationEpisode);
            if(err) { throw err; }
        });
    });
}

function getRSSFeed(searchLink, TeamName, AnimationTitle, final){
    var header = {
			url:searchLink,
			headers:{
				'User-Agent':'Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0.517.44 Safari/534.7'
			}
	};

    var req = request(header);
    var feedparser = new FeedParser();

    req.on('error', function (err) {
        if(err){ throw err; }
    });
    req.on('response', function (res) {
        var stream = this;
        if (res.statusCode != 200) return this.emit('error', new Error('Bad status code'));
        //console.log('Parse Feed');
        Animation.findOne({ title: AnimationTitle, team: TeamName }, function(err, result){
            if(err) { throw err; }
            if(result === null){
                var animation = new Animation();
                animation.title = AnimationTitle;
                animation.team = TeamName;
                animation.day = "";
                animation.episodes = [];
                animation.save();
            }
        });

        stream.pipe(feedparser);
    });

    feedparser.on('error', function(err){
		if(err) { throw err; }
	});

	feedparser.on('readable', function() {
		recordAni(this.read(), TeamName, AnimationTitle);
	});

    feedparser.on('end', function(){
        final(null, TeamName, AnimationTitle);
    });
}

module.exports.updateAnimation = function(TeamName, AnimationTitle, final){
    if(TeamName === undefined || TeamName === null || AnimationTitle === undefined || AnimationTitle === null){
        return; //Animation Title is not exist
    }
    var url = "https://www.nyaa.se/?page=rss&term=%5B"+ TeamName +"%5D+"+ AnimationTitle +"+mp4";
    //console.log("start URL = " + url);
    getRSSFeed(url, TeamName, AnimationTitle, final);
};
