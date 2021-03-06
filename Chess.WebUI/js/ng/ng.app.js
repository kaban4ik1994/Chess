var smartApp = angular.module('smartApp', [
  	'ngRoute',
    'ngTable',
    'angular-loading-bar',
    'ngDraggable',
    'ngAnimate', // this is buggy, jarviswidget will not work with ngAnimate :(
  	'ui.bootstrap',
    'plunker',
    'app.services',
  	'app.controllers',
  	'app.main',
    'app.animations',
  	'app.navigation',
  	'app.localize',
  	'app.activity',
  	'app.smartui',
    'LocalStorageModule',
    'blockUI',
]);

smartApp.config([
    'cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
    }
]);

smartApp.config(function(blockUIConfig) {

    // Change the default overlay message
    blockUIConfig.message = 'Loading...';

    blockUIConfig.autoBlock = false;

    // Change the default delay to 100ms before the blocking is visible
    blockUIConfig.delay = 20;

});

smartApp.config(['$routeProvider', '$provide', function ($routeProvider, $provide) {
    $routeProvider
		.when('/', {
			redirectTo: '/Invitation'
		})

		/* We are loading our views dynamically by passing arguments to the location url */

		// A bug in smartwidget with angular (routes not reloading). 
		// We need to reload these pages everytime so widget would work
		// The trick is to add "/" at the end of the view.
		// http://stackoverflow.com/a/17588833
        .when('/Login', {
            controller: 'LoginController',
            templateUrl: 'views/login.html'
        })
        .when('/Register', {
            controller: 'RegisterController',
            templateUrl: 'views/register.html'
        })
        .when('/Game/:invitationId', {
            controller: 'GameController',
            templateUrl: 'views/game.html'
        })
        .when('/Invitation', {
            controller: 'InvitationController',
            templateUrl: 'views/invitation.html'
        })
        .when('/GameView/:invitationId', {
            controller: 'GameViewController',
            templateUrl: 'views/gameView.html'
        })
		.otherwise({
		    controller: 'Error404Controller',
		    templateUrl: 'views/404.html'
		});
}]);

smartApp.constant('ngAuthSettings', {
    apiServiceBaseUri: baseUrlApiChess,
    clientId: 'ngAuthApp'
});

smartApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('badRequestInterceptorService');
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.interceptors.push('successInterceptorService');
});

smartApp.run(['$rootScope', 'settings', 'authService', function ($rootScope, settings, authService) {
	settings.currentLang = settings.languages[0]; // en
    $rootScope.isLoading = false;
    $rootScope.isShowHeader = true;
    $rootScope.isShowLeftPanel = true;
    $rootScope.isShowFooter = true;
    $rootScope.isShowMainContent = true;
    authService.fillAuthData();
}])