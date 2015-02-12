
var contr = angular.module('app.controllers', [])
    .factory('settings', [
        '$rootScope', function($rootScope) {
            // supported languages

            var settings = {
                languages: [
                    {
                        language: 'English',
                        translation: 'English',
                        langCode: 'en',
                        flagCode: 'us'
                    },
                    //{
                    //    language: 'Espanish',
                    //    translation: 'Espanish',
                    //    langCode: 'es',
                    //    flagCode: 'es'
                    //},
                    //{
                    //    language: 'German',
                    //    translation: 'Deutsch',
                    //    langCode: 'de',
                    //    flagCode: 'de'
                    //},
                    //{
                    //    language: 'Korean',
                    //    translation: '한국의',
                    //    langCode: 'ko',
                    //    flagCode: 'kr'
                    //},
                    //{
                    //    language: 'French',
                    //    translation: 'français',
                    //    langCode: 'fr',
                    //    flagCode: 'fr'
                    //},
                    //{
                    //    language: 'Portuguese',
                    //    translation: 'português',
                    //    langCode: 'pt',
                    //    flagCode: 'br'
                    //},
                    {
                        language: 'Russian',
                        translation: 'русский',
                        langCode: 'ru',
                        flagCode: 'ru'
                    },
                    //{
                    //    language: 'Chinese',
                    //    translation: '中國的',
                    //    langCode: 'zh',
                    //    flagCode: 'cn'
                    //}
                ],

            };

            return settings;

        }
    ])
    .controller('PageViewController', [
        '$scope', '$route', '$animate', function($scope, $route, $animate) {
            // controler of the dynamically loaded views, for DEMO purposes only.
            /*$scope.$on('$viewContentLoaded', function() {
			
		});*/
        }
    ])
    .controller('SmartAppController', [
        '$scope', '$rootScope', '$location', 'authService', function($scope, $rootScope, $location, authService) {
            // your main controller

            $scope.logOut = function() {
                authService.logOut();
                $location.path('/Login');
            };

            $scope.authentication = authService.authentication;
        }
    ])
    .controller('LangController', [
        '$scope', 'settings', 'localize', function($scope, settings, localize) {
            $scope.languages = settings.languages;
            $scope.currentLang = settings.currentLang;
            $scope.setLang = function(lang) {
                settings.currentLang = lang;
                $scope.currentLang = lang;
                localize.setLang(lang);
            };

            // set the default language
            $scope.setLang($scope.currentLang);

        }
    ])

    // Path: /Login
    .controller('LoginController', [
        '$scope', '$rootScope', '$location', 'authService', 'ngAuthSettings', 'ngProgress',
        function($scope, $rootScope, $location, authService, ngAuthSettings, ngProgress) {

            if (authService.authentication.isAuth == true) {
                $location.path('/Home');
            } else {
                $rootScope.isLoading = false;
                $rootScope.isHideHeader = true;
                $rootScope.isHideLeftPanel = true;
                $rootScope.isHideFooter = true;
                $rootScope.isHideMainContent = true;

                $scope.loginData = {
                    userName: "",
                    password: "",
                    useRefreshTokens: false
                };

                $scope.message = null;

                $scope.deleteMessage = function() {
                    $scope.message = null;
                    $scope.showErrorMessages = true;
                };

                $scope.login = function() {
                    ngProgress.start();
                    $scope.showErrorMessages = true;
                    authService.login($scope.loginData).then(function(response) {
                            ngProgress.complete();
                            $location.path('/Home');
                            $rootScope.isHideHeader = false;
                            $rootScope.isHideLeftPanel = false;
                            $rootScope.isHideFooter = false;
                            $rootScope.isHideMainContent = false;
                        },
                        function(err) {
                            ngProgress.complete();
                            var errorMessage = "Login or password are not correct";
                            $scope.message = errorMessage; //err.error_description;
                        });
                };

                $scope.authExternalProvider = function(provider) {

                    var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

                    var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                        + "&response_type=token&client_id=" + ngAuthSettings.clientId
                        + "&redirect_uri=" + redirectUri;
                    window.$windowScope = $scope;

                    var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
                };

                $scope.authCompletedCB = function(fragment) {

                    $scope.$apply(function() {

                        if (fragment.haslocalaccount == 'False') {

                            authService.logOut();

                            authService.externalAuthData = {
                                provider: fragment.provider,
                                userName: fragment.external_user_name,
                                externalAccessToken: fragment.external_access_token
                            };

                            $location.path('/associate');

                        } else {
                            //Obtain access token and redirect to orders
                            var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                            authService.obtainAccessToken(externalData).then(function(response) {

                                    $location.path('/orders');

                                },
                                function(err) {
                                    $scope.message = err.error_description;
                                });
                        }

                    });
                }
            }
        }
    ])

    // Path: /Register
    .controller('RegisterController', [
        '$scope', '$rootScope', '$location', 'authService', 'ngAuthSettings', 'ngProgress', 'accountApi',
        function ($scope, $rootScope, $location, authService, ngAuthSettings, ngProgress, accountApi) {
            if (authService.authentication.isAuth == true) {
                $location.path('/Home');
            } else {
                $rootScope.isLoading = false;
                $rootScope.isHideHeader = true;
                $rootScope.isHideLeftPanel = true;
                $rootScope.isHideFooter = true;
                $rootScope.isHideMainContent = true;
            }
        }])

// Path: /Home
.controller('HomeController', [
    '$scope', '$rootScope', 'authService', '$location', function ($scope, $rootScope, authService, $location) {
        if (authService.authentication.isAuth == false) {
            $location.path('/Login');
        } else {

        }
    }
])



 // Path: /error/404
.controller('Error404Controller', ['$scope', '$rootScope', '$location', '$window', 'authService', function ($scope, $rootScope, $location, $window, authService) {

    if (authService.authentication.isAuth == false) {
        $location.path('/Login');
    } else {
        $rootScope.isLoading = false;
        $scope.$root.title = 'Error 404: Page Not Found';
    }
}]);



