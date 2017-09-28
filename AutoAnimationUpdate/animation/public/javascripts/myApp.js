var app = angular.module('myApp', ['ngRoute', 'ngResource', 'ui.bootstrap', 'ngFileUpload']);

app.config(function($routeProvider, $locationProvider){
    $routeProvider
        .when('/', {
            templateUrl: 'animations.html',
            controller: 'MainCtrl'
        })
        .when('/update', {
            templateUrl: 'update.html',
            controller: 'MainCtrl'
        })
        .when('/subtitle', {
            templateUrl: 'subtitle.html',
            controller: 'SubtitleCtrl'
        })
        .when('/register', {
            templateUrl: 'register.html',
            controller: 'RegisterCtrl'
        })
        .when('/days/:day', {
            templateUrl: 'day.html',
            controller: 'DayCtrl'
        })
        .otherwise({
            redirectTo: '/'
        });
});

app.factory('getService', function($resource){
    return $resource('/animation/:title/:episode');
});
app.factory('updateService', function($resource){
    return $resource('/animation/update');
});
app.factory('registerService', function($resource){
    return $resource('/animation/register');
});
app.factory('dayService', function($resource){
    return $resource('/animation/days');
});

app.run(function($rootScope, $window, $uibModal, $log, registerService, dayService, updateService){
    $rootScope.isNavCollapsed = true;
    $rootScope.days = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun', 'Fin'];

    $rootScope.getDayAni = function(day){
        var animations = dayService.query( {day: day} );
        return animations;
    };

    $rootScope.delete = function(team, title){
        registerService.delete({team: team, title: title}).$promise.then(function(){
            $window.location.reload();
        });
    };

    $rootScope.update = function(team, title){
        updateService.save({team: team, title: title}).$promise.then(function(){
            $window.location.reload();
        });
    };

    $rootScope.day = function(team, title, day){
        dayService.save({team: team, title: title, day: day}).$promise.then(function(){
            $window.location.reload();
        });
    };
    //Episode Modal
    $rootScope.openEpisodeModal = function (episode){
        var modalInstance = $uibModal.open({
            animation: true,
            size: 'lg',
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'episode.html',
            controller: 'EpisodeCtrl',
            scope: $rootScope,
            resolve:{
                episode: function(){
                    return episode;
                }
            }
        });

        modalInstance.result.then(function(res){
            //WebTorrent 기술과 접목할 부분!
            $log.info(res);
        });
    };
    //Google Login
    $rootScope.profile = null;
    window.onSignIn = function onSignIn(googleUser) {
        $rootScope.profile = googleUser.getBasicProfile();
        //console.log('ID: ' + $rootScope.profile.getId());
        //console.log('Name: ' + $rootScope.profile.getName());
        //console.log('Image URL: ' + $rootScope.profile.getImageUrl());
        console.log('Email: ' + $rootScope.profile.getEmail());
        $rootScope.$digest();
    };
});

app.controller('MainCtrl', function($rootScope, $scope, getService, $log){
    $scope.animations = getService.query();
});

app.controller('DayCtrl', function($rootScope, $scope, $routeParams, dayService, $log){
    $scope.day = $routeParams.day;
    $scope.animations = dayService.query({ day: $routeParams.day });
});

app.controller('RegisterCtrl', function($rootScope, $scope, $window, registerService, updateService){
    $scope.team = "Ohys-Raws";
    $scope.title = "";
    $scope.day = "Fin";
    $scope.episode = 0;

    $scope.register = function(team, title, day, episode){
        registerService.save({team: team, title: title, day: day, episode: episode}).$promise.then(function(){
            if(day != "Fin"){
                updateService.save({team: team, title: title}).$promise.then(function(){
                    $window.location.reload();
                });
            }
            else{
                $window.location.reload();
            }
        });
    };
});

app.controller('SubtitleCtrl', function($rootScope, $scope, dayService, Upload, $log){

    $scope.upload = function (title, episode, file) {
        Upload.upload({
            url: '/animation/subtitle',
            data: {
                'title': title,
                'episode': episode,
                'file': file
            }
        }).then(function (resp) {
            $log.info('Success ' + resp.config.data.file.name + ' uploaded. Response: ' + resp.data);
        }, function (resp) {
            $log.info('Error status: ' + resp.status);
        }, function (evt) {
            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            $log.info('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
        });
    };
});

app.controller('EpisodeCtrl', function($scope, $uibModalInstance, episode, $log){
    //WebTorrent 기술과 접목할 부분!
    $scope.episode = episode;
    $scope.close = function () {
        $uibModalInstance.close(episode);
    };

    //custom subtitle upload
    $scope.uploadcustomsub = function(element){
        var subtitle = element.files[0];
        var subURL = URL.createObjectURL(subtitle);
        document.querySelector("#subtitle").src = subURL;
    };
});
