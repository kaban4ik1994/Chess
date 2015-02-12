'use strict';
// Demonstrate how to register services
// In this case it is a simple value service.
angular.module('app.services', ['ngResource'])

     .factory('accountApi', [
        '$resource', function ($resource) {
            return $resource(urlApiAccountChess, {}, {
                add: {
                    method: 'PUT'
                }
            });
        }
     ])

    .factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
        var authServiceFactory = {};
        var _authentication = {
            UserId: 0,
            Email: "",
            Token: "",
            Roles: "",
            UserName: "",
            FirstName: "",
            SecondName: "",
            isAuth: false,
        };

        var _login = function (loginData) {
            console.log(loginData);
            var data = "email=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.get(urlApiAccountChess + '?' + data.toString(), { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, data: data }).success(function (response) {
                localStorageService.set('authorizationData',
                {
                    UserId: response.UserId,
                    Email: response.Email,
                    Token: response.Token,
                    Roles: response.Roles,
                    UserName: response.UserName,
                    FirstName: response.FirstName,
                    SecondName: response.SecondName,
                });
                _authentication.isAuth = true;
                _authentication.UserId = response.UserId;
                _authentication.Email = response.Email;
                _authentication.Token = response.Token;
                _authentication.Roles = response.Roles;
                _authentication.UserName = response.UserName;
                _authentication.FirstName = response.FirstName;
                _authentication.SecondName = response.SecondName;
                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });
            return deferred.promise;
        };

        var _logOut = function () {
            localStorageService.remove('authorizationData');
            _authentication.isAuth = false;
            _authentication.UserId = 0;
            _authentication.Email = "";
            _authentication.Token = "";
            _authentication.Roles = "";
            _authentication.UserName = "";
            _authentication.FirstName = "";
            _authentication.SecondName = "";

        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = false;
                _authentication.UserId = authData.UserId;
                _authentication.Email = authData.Email;
                _authentication.Token = authData.Token;
                _authentication.Roles = authData.Roles;
                _authentication.UserName = authData.UserName;
                _authentication.FirstName = authData.FirstName;
                _authentication.SecondName = authData.SecondName;
            }
        };

        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        return authServiceFactory;
    }])
    .factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', function ($q, $injector, $location, localStorageService) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = authData.Token;
            }
            return config;
        };

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                var authService = $injector.get('authService');
                var authData = localStorageService.get('authorizationData');
                authService.logOut();
                $location.path('/Login');
            }
            return $q.reject(rejection);
        };
        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;
        return authInterceptorServiceFactory;
    }]);;
