var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var aniSchema = new mongoose.Schema({
    title: {type: String, required: true},
    team: {type: String, required: true},
    day: String,
    episodes: [{
        number: { type: Number, required: true },
        date: Date,
        video: String,
        tumbnail: String,
        subtitle: String
    }]
});

mongoose.model('Animation', aniSchema);
