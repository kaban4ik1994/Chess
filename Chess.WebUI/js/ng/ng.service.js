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

    .factory('availableInvitationApi', [
        '$resource', function ($resource) {
            return $resource(urlApiAvailableInvitation, {}, {

            });
        }
    ])

    .factory('acceptInvitation', [
        '$resource', function ($resource) {
            return $resource(urlApiAcceptInvitation, {}, {

            });
        }])

    .factory('invitationApi', [
        '$resource', function ($resource) {
            return $resource(urlApiInvitation, {}, {
                add: {
                    method: 'PUT'
                }
            });
        }
    ])
    .factory('gameApi', [
        '$resource', function ($resource) {
            return $resource(urlApiGame, {}, {
            });
        }
    ])

     .factory('TestApi', [
        '$resource', function ($resource) {
            return $resource(urlApiTestChess, {}, {
            });
        }
     ])
     .factory('badRequestInterceptorService', [
        '$q', function ($q) {
            var badRequestInterceptorService = {};

            var _responseError = function (rejection) {
                if (rejection.status === 400) {
                    console.log(rejection)
                    var data;
                    for (var key in rejection.data.ModelState) {
                        data = rejection.data.ModelState[key];
                        break;
                    }

                    var message = data
                        ? data[0]
                        : "Не удалось выполнить запрос";
                    $.smallBox({
                        title: message,
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#FF0000",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                }
                return $q.reject(rejection);
            };

            badRequestInterceptorService.responseError = _responseError;

            return badRequestInterceptorService;
        }
     ])

    .factory('successInterceptorService', [
        '$q', '$injector', '$location', 'localStorageService',
        function ($q, $injector, $location, localStorageService) {
            var successInterceptorService = {};

            function showSmallBox(message) {
                $.smallBox({
                    title: message,
                    content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                    color: "#296191",
                    iconSmall: "fa fa-check bounce animated",
                    timeout: 3000
                });
            }

            var request = function (config) {
                return config;
            };

            var response = function (res) {
                if (res.status === 200 && res.config.method === "POST") {
                    showSmallBox("Сохранение данных выполнено успешно.");
                } else if (res.status === 201 && res.config.method === "PUT") {
                    showSmallBox("Добавление данных выполнено успешно.");
                } else if (res.status === 200 && res.config.method === "DELETE") {
                    showSmallBox("Данные успешно удалены.");
                }
                return res;
            };

            var responseError = function (rejection) {
                return $q.reject(rejection);
            };

            successInterceptorService.response = response;
            successInterceptorService.request = request;
            successInterceptorService.responseError = responseError;

            return successInterceptorService;
        }
    ])

    .factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
        var authServiceFactory = {};
        var _authentication = {
            UserId: 0,
            Email: "",
            Token: "",
            IsAdmin: "",
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

                var isAdmin = false;
                response.Roles.forEach(function (element) {
                    if (element === 'Admin') {
                        isAdmin = true;
                    }
                });

                localStorageService.set('authorizationData',
                {
                    UserId: response.UserId,
                    Email: response.Email,
                    Token: response.Token,
                    IsAdmin: isAdmin,
                    UserName: response.UserName,
                    FirstName: response.FirstName,
                    SecondName: response.SecondName,
                });
                _authentication.isAuth = true;
                _authentication.UserId = response.UserId;
                _authentication.Email = response.Email;
                _authentication.Token = response.Token;
                _authentication.IsAdmin = isAdmin;
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
            _authentication.IsAdmin = "";
            _authentication.UserName = "";
            _authentication.FirstName = "";
            _authentication.SecondName = "";

        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.UserId = authData.UserId;
                _authentication.Email = authData.Email;
                _authentication.Token = authData.Token;
                _authentication.IsAdmin = authData.IsAdmin;
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
