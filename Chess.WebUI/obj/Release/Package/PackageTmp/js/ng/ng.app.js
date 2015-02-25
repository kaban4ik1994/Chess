var smartApp = angular.module('smartApp', [
  	'ngRoute',
    'ngTable',
    'ui.select2.sortable',
    'ui.select2',
    'ngProgress',
    'ngAnimate', // this is buggy, jarviswidget will not work with ngAnimate :(
  	'ui.bootstrap',
    'plunker',
    'app.services',
  	'app.controllers',
  	'app.demoControllers',
  	'app.main',
  	'app.navigation',
  	'app.localize',
  	'app.activity',
  	'app.smartui',
    'LocalStorageModule'
]);

smartApp.config(['$routeProvider', '$provide', function ($routeProvider, $provide) {
    $routeProvider
		.when('/', {
		    redirectTo: '/Home'
		})

		/* We are loading our views dynamically by passing arguments to the location url */

		// A bug in smartwidget with angular (routes not reloading). 
		// We need to reload these pages everytime so widget would work
		// The trick is to add "/" at the end of the view.
		// http://stackoverflow.com/a/17588833
        .when('/Home', {
            controller: 'HomeController',
            templateUrl: 'views/home.html'
        })
        .when('/Orders', {
            controller: 'OrdersController',
            templateUrl: 'views/orders.html'
        })
        .when('/Order', {
            controller: 'OrderController',
            templateUrl: 'views/order.html'
        })
        .when('/Order/:orderId', {
            controller: 'OrderController',
            templateUrl: 'views/order.html'
        })
        .when('/Order/:orderId/Services', {
            controller: 'ServicesController',
            templateUrl: 'views/services.html'
        })

        .when('/Order/:orderId/Services/Service', {
            controller: 'ServiceController',
            templateUrl: 'views/service.html'
        })
         .when('/Order/:orderId/Services/:serviceType/:serviceId', {
             controller: 'ServiceController',
             templateUrl: 'views/service.html'
         })
        .when('/Login', {
            controller: 'LoginController',
            templateUrl: 'views/login.html'
        })
		.otherwise({
		    controller: 'Error404Controller',
		    templateUrl: 'views/404.html'
		});

    //// with this, you can use $log('Message') same as $log.info('Message');
    //$provide.decorator('$log', ['$delegate',
    //function($delegate) {
    //	// create a new function to be returned below as the $log service (instead of the $delegate)
    //	function logger() {
    //		// if $log fn is called directly, default to "info" message
    //		logger.info.apply(logger, arguments);
    //	}

    //	// add all the $log props into our new logger fn
    //	angular.extend(logger, $delegate);
    //	return logger;
    //}]); 

}]);

var serviceBase = 'http://localhost:30578/api/';
smartApp.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

smartApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

smartApp.run(['$rootScope', 'settings', 'authService', function ($rootScope, settings, authService) {
    settings.currentLang = settings.languages[1]; // ru
    $rootScope.isLoading = false;
    $rootScope.isShowHeader = true;
    $rootScope.isShowLeftPanel = true;
    $rootScope.isShowFooter = true;
    $rootScope.isShowMainContent = true;
    $rootScope.NumberOfClientsForAutoCompleteDropDown = NumberOfClientsForAutoCompleteDropDown;
    $rootScope.NumberOfCompaniesForAutoCompleteDropDown = NumberOfCompaniesForAutoCompleteDropDown;
    $rootScope.NumberOfManagersForAutoCompleteDropDown = NumberOfManagersForAutoCompleteDropDown;
    $rootScope.NumberOfAirlinesForAutoCompleteDropDown = NumberOfAirlinesForAutoCompleteDropDown;
    $rootScope.NumberOfAirportsForAutoCompleteDropDown = NumberOfAirportsForAutoCompleteDropDown;
    $rootScope.NumberOfCountriesForAutoCompleteDropDown = NumberOfCountriesForAutoCompleteDropDown;
    $rootScope.NumberOfRegionsForAutoCompleteDropDown = NumberOfRegionsForAutoCompleteDropDown;
    $rootScope.NumberOfHotelsForAutoCompleteDropDown = NumberOfHotelsForAutoCompleteDropDown;
    authService.fillAuthData();
}])