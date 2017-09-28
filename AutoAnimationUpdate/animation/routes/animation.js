var mongoose = require('mongoose');
var path = require('path');
var fs = require('fs');
var express = require('express');
var router = express.Router();
var Animation = mongoose.model('Animation');

var update = require('../src/update');

var multer = require('multer');
var storage = multer.diskStorage({ //multers disk storage settings
    destination: function (req, file, cb) {
        cb(null, path.join(__dirname,'..','/public/videos/', req.body.title.replace(new RegExp(' ', 'g'), '')));
    },
    filename: function (req, file, cb) {
        cb(null, req.body.title.replace(new RegExp(' ', 'g'), '') + '-' + req.body.episode + '.'+ file.originalname.split('.')[file.originalname.split('.').length -1]);
    }
});
var upload = multer({storage: storage}).single('file');

//regularly updating func
setInterval(function(){
    console.log("START regular update!");
    Animation.find({ },function(err, animations){
        //callback func
        var cb = function(err, team, title){
            if(err){
                console.log("Timely Update failed to update ["+team+"]" + title);
            }else{
                console.log("Timely Update success to update ["+team+"]" + title);
            }
        };
        //update!
        animations.forEach(function(animation){
            if(animation.day != "Fin"){
                update.updateAnimation(animation.team, animation.title, cb);
            }
        });
  });
}, 24 * 60 * 60 * 1000);

router.route('/')
    .get(function(req, res, next){
        Animation.find(function(err, animations){
            if(err) { return res.status(500).send(err); }
            res.status(200).send(animations);
    });
});

router.route('/:title/:episode')
    .get(function(req, res, next){
        Animation.findOne({title:req.params.title}, function(err, animation){
            if(err) {return res.status(500).send(err); }
            res.status(200).send(animation);
    });
});

router.route('/subtitle')
    .post(function(req, res, next){
        upload(req, res, function(err){
            if(err){
                res.status(500).send(err);
                throw err;
            }
            if( typeof(req.file) === "undefined" || typeof(req.file.filename) === "undefined" || req.file === null || req.file.filename === null){
                res.status(500).send();
                return;
            }

            var subpath = '/videos/' + req.body.title.replace(new RegExp(' ', 'g'), '') + '/' + req.file.filename;

            Animation.findOneAndUpdate({ title: req.body.title, 'episodes.number': req.body.episode }, { $set: { 'episodes.$.subtitle': subpath }}, function(err, animation){
                if(err) {return res.status(500).send(err); }
                res.status(200).send(animation);
            });
        });
    }
);

router.route('/update')
    .post(function(req, res, next){
        if(req.body.team && req.body.title){
            update.updateAnimation(req.body.team, req.body.title, function(err){
                if(err){
                    res.status(500).send("Failed to update ["+req.body.team+"]" + req.body.title);
                }else{
                    res.status(200).send("Success to update ["+req.body.team+"]" + req.body.title);
                }
            });

        }
    }
);

router.route('/days')
    .get(function(req, res, next){
        Animation.find( {day: req.query.day}, function(err, animations){
            if(err) { return res.status(500).send(err); }
            res.status(200).send(animations);
        });
    })
    .post(function(req, res, next){
        Animation.findOneAndUpdate({ team: req.body.team, title: req.body.title }, { $set: { day: req.body.day }}, function(err, animation){
            if(err) {return res.status(500).send(err); }
            res.status(200).send(animation);
        });
    }
);

router.route('/register')
    .post(function(req, res, next){
        var newAni = new Animation();
        newAni.team = req.body.team;
        newAni.title = req.body.title;
        newAni.day = req.body.day;
        newAni.episodes = [];
        for(var i = 1; i <= req.body.episode; i++){
            var path = 'videos/' + req.body.title.replace(new RegExp(' ', 'g'), '') +'/'+ req.body.title.replace(new RegExp(' ', 'g'), '') + "-" + i + ".mp4";
            var episode = {
                number: i,
                date: null,
                video: path,
                tumbnail: null,
                subtitle: null
            };
            newAni.episodes.push(episode);
        }
        newAni.save(function(err){
            if(err){
                console.log(err);
                return res.status(500).send(err);
            }
            res.status(200).send(true);
        });
    })
    .delete(function(req, res, next){
        console.log(req.query);
        Animation.remove({team: req.query.team, title: req.query.title}, function(err){
            if(err){
                return res.status(500).send(err);
            }
            res.status(200).send("Successfully deleted." + "[" + req.query.team + "]" + req.query.title);
        });
    }
);

module.exports = router;
