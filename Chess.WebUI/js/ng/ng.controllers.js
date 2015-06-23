
var contr = angular.module('app.controllers', [])
    .factory('settings', [
        '$rootScope', function ($rootScope) {
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
        '$scope', '$route', '$animate', function ($scope, $route, $animate) {
            // controler of the dynamically loaded views, for DEMO purposes only.
            /*$scope.$on('$viewContentLoaded', function() {
			
		});*/
        }
    ])
    .controller('SmartAppController', [
        '$scope', '$rootScope', '$location', 'authService', function ($scope, $rootScope, $location, authService) {
            // your main controller

            $scope.logOut = function () {
                authService.logOut();
                $location.path('/Login');
            };

            $scope.authentication = authService.authentication;
        }
    ])
    .controller('LangController', [
        '$scope', 'settings', 'localize', function ($scope, settings, localize) {
            $scope.languages = settings.languages;
            $scope.currentLang = settings.currentLang;
            $scope.setLang = function (lang) {
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
        '$scope', '$rootScope', '$location', 'authService', 'ngAuthSettings',
        function ($scope, $rootScope, $location, authService, ngAuthSettings) {

            if (authService.authentication.isAuth == true) {
                $location.path('/Home');
            } else {
                $rootScope.isLoading = false;
                $rootScope.isHideHeader = true;
                $rootScope.isHideLeftPanel = true;
                $rootScope.isHideFooter = true;
                $rootScope.isHideMainContent = true;

                $scope.loginData = {
                    userName: "Admin@gmail.com",
                    password: "Admin",
                    useRefreshTokens: false
                };

                $scope.message = null;

                $scope.deleteMessage = function () {
                    $scope.message = null;
                    $scope.showErrorMessages = true;
                };

                $scope.login = function () {
                    $scope.showErrorMessages = true;
                    authService.login($scope.loginData).then(function (response) {
                        $location.path('/Home');
                        $rootScope.isHideHeader = false;
                        $rootScope.isHideLeftPanel = false;
                        $rootScope.isHideFooter = false;
                        $rootScope.isHideMainContent = false;
                    },
                        function (err) {
                            var errorMessage = "Login or password are not correct";
                            $scope.message = errorMessage; //err.error_description;
                        });
                };

                $scope.authExternalProvider = function (provider) {

                    var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

                    var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                        + "&response_type=token&client_id=" + ngAuthSettings.clientId
                        + "&redirect_uri=" + redirectUri;
                    window.$windowScope = $scope;

                    var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
                };

                $scope.authCompletedCB = function (fragment) {

                    $scope.$apply(function () {

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
                            authService.obtainAccessToken(externalData).then(function (response) {

                                $location.path('/orders');

                            },
                                function (err) {
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
        '$scope', '$rootScope', '$location', 'authService', 'ngAuthSettings', 'accountApi',
        function ($scope, $rootScope, $location, authService, ngAuthSettings, accountApi) {
            if (authService.authentication.isAuth == true) {
                $location.path('/Home');
            } else {
                $rootScope.isLoading = false;
                $rootScope.isHideHeader = true;
                $rootScope.isHideLeftPanel = true;
                $rootScope.isHideFooter = true;
                $rootScope.isHideMainContent = true;

                $scope.register = function (item) {
                    accountApi.add(item, function () {
                        $location.path('/Login');
                    });
                };
            }
        }
    ])

// Path: /Home
    .controller('HomeController', [
        '$scope', '$rootScope', 'authService', '$location', 'TestApi',
        function ($scope, $rootScope, authService, $location, TestApi) {
            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {
                TestApi.get();
            }
        }
    ])

    // Path : /Invitation
    .controller('InvitationController', [
        '$scope', '$rootScope', 'authService', '$location', '$interval', 'availableInvitationApi', 'invitationApi', 'acceptInvitationApi', 'closedInvitationApi', 'blockUI', 'botInvitationApi',
        function ($scope, $rootScope, authService, $location, $interval, availableInvitationApi, invitationApi, acceptInvitationApi, closedInvitationApi, blockUI, botInvitationApi) {

            function removeInvitationFromListById(list, id) {
                angular.forEach(list, function (value, index) {
                    if (value.Id == id) {
                        list.splice(index, 1);
                        list.Count--;
                    }
                });
            }

            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {

                $scope.isAdmin = authService.authentication.IsAdmin;
                $scope.currentUserId = authService.authentication.UserId;

                $scope.newInvitationWithBot = function (botType) {
                    botInvitationApi.add({ InvitatorId: authService.authentication.UserId, BotType: botType }, function (item) {
                        $scope.acceptInvitations.Items.push(item);
                        $scope.acceptInvitations.Count++;
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                };

                var acceptInvitationsInterval = $interval(function () {
                    if ($scope.refreshAcceptInvitationsTime == 0) {
                        $scope.refreshAcceptInvitations();
                    } else {
                        $scope.refreshAcceptInvitationsTime--;
                    }
                }, 1000);

                var availableInvitationsInterval = $interval(function () {
                    if ($scope.refreshAvailableInvitationsTime == 0) {
                        $scope.refreshAvailableInvitations();
                    } else {
                        $scope.refreshAvailableInvitationsTime--;
                    }
                }, 1000);

                var closedInvitationsInterval = $interval(function () {
                    if ($scope.refreshClosedInvitationsTime == 0) {
                        $scope.refreshClosedInvitations();
                    } else {
                        $scope.refreshClosedInvitationsTime--;
                    }
                }, 1000);

                $scope.newInvitation = function () {
                    blockUI.start();
                    invitationApi.add({ InvitatorId: authService.authentication.UserId }, function (item) {
                        $scope.availableInvitations.Items.push(item);
                        $scope.availableInvitations.Count++;
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                };

                $scope.deleteInvitation = function (id) {
                    blockUI.start();
                    invitationApi.delete({ invitationId: id }, function () {
                        removeInvitationFromListById($scope.availableInvitations.Items, id);
                        $scope.availableInvitations.Count--;
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                };

                $scope.refreshAvailableInvitations = function () {
                    blockUI.start();
                    $scope.refreshAvailableInvitationsTime = 60;
                    $scope.availableInvitations = availableInvitationApi.get({}, function () {
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                };

                $scope.refreshAcceptInvitations = function () {
                    blockUI.start();
                    $scope.refreshAcceptInvitationsTime = 30;
                    $scope.acceptInvitations = acceptInvitationApi.get({}, function () {
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                }

                $scope.refreshClosedInvitations = function () {
                    blockUI.start();
                    $scope.refreshClosedInvitationsTime = 100;
                    $scope.closedInvitations = closedInvitationApi.get({}, function () {
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                }

                $scope.takeInvitation = function (invitation) {
                    blockUI.start();
                    availableInvitationApi.save({ InvitationId: invitation.Id }, function () {
                        removeInvitationFromListById($scope.availableInvitations.Items, invitation.Id);
                        $scope.availableInvitations.Count--;
                        $scope.acceptInvitations.Items.push(invitation);
                        $scope.acceptInvitations.Count++;
                        blockUI.stop();
                    }, function () {
                        blockUI.stop();
                    });
                }

                $scope.refreshAvailableInvitations();
                $scope.refreshAcceptInvitations();
                $scope.refreshClosedInvitations();

                $scope.$on('$destroy', function () {
                    $interval.cancel(acceptInvitationsInterval);
                    $interval.cancel(availableInvitationsInterval);
                    $interval.cancel(closedInvitationsInterval);
                });
            }
        }
    ])

    .controller('ChatController', [
        '$scope', '$interval', '$routeParams', 'gameApi', 'authService', function ($scope, $interval, $routeParams, gameApi, authService) {
            $scope.userName = authService.authentication.UserName;
            var uri = urlApiChat + '?userName=' + $scope.userName;
            var websocket = new WebSocket(uri);
            websocket.onopen = function () {
                $scope.sendMessage = function () {
                    websocket.send($scope.message);
                    $scope.message = "";
                }
            }

            $scope.messages = [];

            websocket.onmessage = function (event) {
                $scope.messages.push(angular.fromJson(event.data));
            }
        }])


    //Path /game/Id
    .controller('GameController', [
        '$scope', '$interval', '$routeParams', 'gameApi', 'authService', function ($scope, $interval, $routeParams, gameApi, authService) {
            $scope.prompt = '';
            //////////////////////
            var centerPoints = {
                //a
                a1: new BABYLON.Vector3(8.8, 2.5, -8.75),
                a2: new BABYLON.Vector3(6.3, 2.5, -8.75),
                a3: new BABYLON.Vector3(3.8, 2.5, -8.75),
                a4: new BABYLON.Vector3(1.3, 2.5, -8.75),
                a5: new BABYLON.Vector3(-1.2, 2.5, -8.75),
                a6: new BABYLON.Vector3(-3.7, 2.5, -8.75),
                a7: new BABYLON.Vector3(-6.2, 2.5, -8.75),
                a8: new BABYLON.Vector3(-8.7, 2.5, -8.75),
                //b
                b1: new BABYLON.Vector3(8.8, 2.5, -6.25),
                b2: new BABYLON.Vector3(6.3, 2.5, -6.25),
                b3: new BABYLON.Vector3(3.8, 2.5, -6.25),
                b4: new BABYLON.Vector3(1.3, 2.5, -6.25),
                b5: new BABYLON.Vector3(-1.2, 2.5, -6.25),
                b6: new BABYLON.Vector3(-3.7, 2.5, -6.25),
                b7: new BABYLON.Vector3(-6.2, 2.5, -6.25),
                b8: new BABYLON.Vector3(-8.7, 2.5, -6.25),
                //c
                c1: new BABYLON.Vector3(8.8, 2.5, -3.75),
                c2: new BABYLON.Vector3(6.3, 2.5, -3.75),
                c3: new BABYLON.Vector3(3.8, 2.5, -3.75),
                c4: new BABYLON.Vector3(1.3, 2.5, -3.75),
                c5: new BABYLON.Vector3(-1.2, 2.5, -3.75),
                c6: new BABYLON.Vector3(-3.7, 2.5, -3.75),
                c7: new BABYLON.Vector3(-6.2, 2.5, -3.75),
                c8: new BABYLON.Vector3(-8.7, 2.5, -3.75),
                //d
                d1: new BABYLON.Vector3(8.8, 2.5, -1.25),
                d2: new BABYLON.Vector3(6.3, 2.5, -1.25),
                d3: new BABYLON.Vector3(3.8, 2.5, -1.25),
                d4: new BABYLON.Vector3(1.3, 2.5, -1.25),
                d5: new BABYLON.Vector3(-1.2, 2.5, -1.25),
                d6: new BABYLON.Vector3(-3.7, 2.5, -1.25),
                d7: new BABYLON.Vector3(-6.2, 2.5, -1.25),
                d8: new BABYLON.Vector3(-8.7, 2.5, -1.25),
                //e
                e1: new BABYLON.Vector3(8.8, 2.5, 1.25),
                e2: new BABYLON.Vector3(6.3, 2.5, 1.25),
                e3: new BABYLON.Vector3(3.8, 2.5, 1.25),
                e4: new BABYLON.Vector3(1.3, 2.5, 1.25),
                e5: new BABYLON.Vector3(-1.2, 2.5, 1.25),
                e6: new BABYLON.Vector3(-3.7, 2.5, 1.25),
                e7: new BABYLON.Vector3(-6.2, 2.5, 1.25),
                e8: new BABYLON.Vector3(-8.7, 2.5, 1.25),
                //f
                f1: new BABYLON.Vector3(8.8, 2.5, 3.75),
                f2: new BABYLON.Vector3(6.3, 2.5, 3.75),
                f3: new BABYLON.Vector3(3.8, 2.5, 3.75),
                f4: new BABYLON.Vector3(1.3, 2.5, 3.75),
                f5: new BABYLON.Vector3(-1.2, 2.5, 3.75),
                f6: new BABYLON.Vector3(-3.7, 2.5, 3.75),
                f7: new BABYLON.Vector3(-6.2, 2.5, 3.75),
                f8: new BABYLON.Vector3(-8.7, 2.5, 3.75),
                //g
                g1: new BABYLON.Vector3(8.8, 2.5, 6.25),
                g2: new BABYLON.Vector3(6.3, 2.5, 6.25),
                g3: new BABYLON.Vector3(3.8, 2.5, 6.25),
                g4: new BABYLON.Vector3(1.3, 2.5, 6.25),
                g5: new BABYLON.Vector3(-1.2, 2.5, 6.25),
                g6: new BABYLON.Vector3(-3.7, 2.5, 6.25),
                g7: new BABYLON.Vector3(-6.2, 2.5, 6.25),
                g8: new BABYLON.Vector3(-8.7, 2.5, 6.25),
                //h
                h1: new BABYLON.Vector3(8.8, 2.5, 8.75),
                h2: new BABYLON.Vector3(6.3, 2.5, 8.75),
                h3: new BABYLON.Vector3(3.8, 2.5, 8.75),
                h4: new BABYLON.Vector3(1.3, 2.5, 8.75),
                h5: new BABYLON.Vector3(-1.2, 2.5, 8.75),
                h6: new BABYLON.Vector3(-3.7, 2.5, 8.75),
                h7: new BABYLON.Vector3(-6.2, 2.5, 8.75),
                h8: new BABYLON.Vector3(-8.7, 2.5, 8.75),
            };

            var figures = [];

            var canvas = document.getElementById('scene');
            // init Babylon engine
            var engine = new BABYLON.Engine(canvas, true);

            var createScene = function () {

                // This creates a basic Babylon Scene object (non-mesh)
                var scene = new BABYLON.Scene(engine);

                scene.clearColor = new BABYLON.Color3(0, 0, 0);
                scene.shadowsEnabled = true;

                // This creates and positions a free camera (non-mesh)
                var camera = new BABYLON.ArcRotateCamera("Camera", 0, 0.8, 30, BABYLON.Vector3.Zero(), scene);
                camera.lowerBetaLimit = 0.1;
                camera.upperBetaLimit = (Math.PI / 2) * 0.9;
                camera.lowerRadiusLimit = 30;
                camera.upperRadiusLimit = 30;
                camera.attachControl(canvas, true);

                var light0 = new BABYLON.PointLight("Omni0", new BABYLON.Vector3(10, 5, -7), scene);
                var light1 = new BABYLON.PointLight("Omni1", new BABYLON.Vector3(10, 5, 7), scene);
                var light2 = new BABYLON.PointLight("Omni2", new BABYLON.Vector3(-10, 5, -7), scene);
                var light3 = new BABYLON.PointLight("Omni3", new BABYLON.Vector3(-10, 5, 7), scene);

                var lightSphere0 = BABYLON.Mesh.CreateSphere("Sphere0", 16, 0.5, scene);
                var lightSphere1 = BABYLON.Mesh.CreateSphere("Sphere1", 16, 0.5, scene);
                var lightSphere2 = BABYLON.Mesh.CreateSphere("Sphere2", 16, 0.5, scene);
                var lightSphere3 = BABYLON.Mesh.CreateSphere("Sphere3", 16, 0.5, scene);

                lightSphere0.material = new BABYLON.StandardMaterial("white", scene);
                lightSphere0.position = light0.position;
                lightSphere0.material.diffuseColor = new BABYLON.Color3(0, 0, 0);
                lightSphere0.material.specularColor = new BABYLON.Color3(0, 0, 0);
                lightSphere0.material.emissiveColor = new BABYLON.Color3(1, 1, 0);

                lightSphere1.material = new BABYLON.StandardMaterial("white", scene);
                lightSphere1.position = light1.position;
                lightSphere1.material.diffuseColor = new BABYLON.Color3(0, 0, 0);
                lightSphere1.material.specularColor = new BABYLON.Color3(0, 0, 0);
                lightSphere1.material.emissiveColor = new BABYLON.Color3(1, 0, 0);

                lightSphere2.material = new BABYLON.StandardMaterial("white", scene);
                lightSphere2.position = light2.position;
                lightSphere2.material.diffuseColor = new BABYLON.Color3(0, 0, 0);
                lightSphere2.material.specularColor = new BABYLON.Color3(0, 0, 0);
                lightSphere2.material.emissiveColor = new BABYLON.Color3(0, 1, 0);

                lightSphere3.material = new BABYLON.StandardMaterial("white", scene);
                lightSphere3.position = light3.position;
                lightSphere3.material.diffuseColor = new BABYLON.Color3(0, 0, 0);
                lightSphere3.material.specularColor = new BABYLON.Color3(0, 0, 0);
                lightSphere3.material.emissiveColor = new BABYLON.Color3(0, 0, 1);


                light0.diffuse = lightSphere0.material.emissiveColor;
                light0.specular = lightSphere0.material.emissiveColor;
                light0.intensity = 1;

                light1.diffuse = lightSphere1.material.emissiveColor;
                light1.specular = lightSphere1.material.emissiveColor;
                light1.intensity = 1;

                light2.diffuse = lightSphere2.material.emissiveColor;
                light2.specular = lightSphere2.material.emissiveColor;
                light2.intensity = 1;

                light3.diffuse = lightSphere3.material.emissiveColor;
                light3.specular = lightSphere3.material.emissiveColor;
                light3.intensity = 1;

                // Our built-in 'ground' shape. Params: name, width, depth, subdivs, scene
                var plane = BABYLON.Mesh.CreatePlane("plane", 20, scene);
                plane.position.y = 0;
                plane.rotation.x = Math.PI / 2;

                var materialPlane = new BABYLON.StandardMaterial("texturePlane", scene);
                materialPlane.diffuseTexture = new BABYLON.Texture("textures/chessboard_texture_by_sveinjo.png", scene);
                materialPlane.diffuseTexture.uScale = 2.0;//Repeat 5 times on the Vertical Axes
                materialPlane.diffuseTexture.vScale = 2.0;//Repeat 5 times on the Horizontal Axes
                materialPlane.backFaceCulling = false;//Allways show the front and the back of an element
                plane.material = materialPlane;

                BABYLON.SceneLoader.ImportMesh("", "textures/figures/", "figures.babylon.json", scene, function () {
                    for (var i = 5; i < scene.meshes.length; i++) {
                        scene.meshes[i].isVisible = true;
                    }

                    refreshBoard2();
                });


                return scene;

            };

            var scene = createScene();



            window.addEventListener("click", function (evt) {
                var pickResult = scene.pick(evt.clientX, evt.clientY);
                console.log(pickResult);
            });

            function getMeshIdByName(name) {
                console.log(this.scene);
                var result = -1;
                angular.forEach(scene.meshes, function (value, key) {
                    if (value.name == name) result = key;
                });
                return result;
            };


            function refreshBoard2() {
                var activeColumn = null;
                if ($scope.chessBoard != undefined) {
                    activeColumn = getActiveFigure();
                }
                gameApi.get({ invitationId: $routeParams.invitationId }, function (data) {
                    $scope.chessBoard = data.GameData;
                    var gameLog = angular.fromJson(data.GameData.GameLog);
                    angular.forEach(gameLog, function (row) {
                        angular.forEach(row, function (cell) {
                            if (cell.Figure) {
                                if (cell.Figure.Color == 1 && cell.Figure.Type == 1) {
                                    scene.meshes[getMeshIdByName('black_pawn1')].position = centerPoints.h1;
                                    scene.meshes[getMeshIdByName('black_pawn2')].position = centerPoints.h2;
                                    scene.meshes[getMeshIdByName('black_pawn3')].position = centerPoints.h3;
                                    scene.meshes[getMeshIdByName('black_pawn4')].position = centerPoints.h4;
                                    scene.meshes[getMeshIdByName('black_pawn5')].position = centerPoints.h5;
                                    scene.meshes[getMeshIdByName('black_pawn6')].position = centerPoints.h6;
                                    scene.meshes[getMeshIdByName('black_pawn7')].position = centerPoints.h7;
                                    scene.meshes[getMeshIdByName('black_pawn8')].position = centerPoints.h8;
                                }
                            }
                        });
                    });
                    $scope.chessBoard.GameLog = angular.fromJson(data.GameData.GameLog);
                    $scope.isCurrentUserHasBlackColor = authService.authentication.UserId === data.GameData.SecondPlayerId;
                    if (activeColumn != null)
                        angular.forEach($scope.chessBoard.GameLog, function (value) {
                            angular.forEach(value, function (item) {
                                if (activeColumn.Position.X == item.Position.X && activeColumn.Position.Y == item.Position.Y) {
                                    item.active = true;
                                }
                            });
                        });
                }, function (error) {
                    $interval.cancel(refreshBoardInterval);
                    $scope.prompt = "Game over!";
                });
            }

            engine.runRenderLoop(function () {
                scene.render();
            });


            /////////////////////////////
            function getActiveFigure() {
                var result;
                angular.forEach($scope.chessBoard.GameLog, function (row) {
                    angular.forEach(row, function (column) {
                        if (column.active == true) {
                            result = column;
                            $scope.activeColumn = column;
                        }
                    });
                });
                return result;
            }

            $scope.selectedItem = function (item) {
                var activeFigure = getActiveFigure();
                $scope.prompt = undefined;
                if (($scope.chessBoard.LogIndex % 2 != 0
                    && authService.authentication.UserId == $scope.chessBoard.FirstPlayerId
                    && (item.Figure == null || item.Figure.Color == 2
                    || (item.Figure != null && activeFigure != null && activeFigure.Figure != null && activeFigure.Figure.Color == 2 && item.Figure.Color == 1)
                    || (item.Figure != null && activeFigure != null && activeFigure.Figure != null && activeFigure.Figure.Color == 2 && item.Figure.Color == 2 && activeFigure.Figure.Type == 1 && item.Figure.Type == 4)))
                    || ($scope.chessBoard.LogIndex % 2 == 0
                    && authService.authentication.UserId == $scope.chessBoard.SecondPlayerId
                    && (item.Figure == null || item.Figure.Color == 1
                    || (item.Figure != null && activeFigure != null && activeFigure.Figure != null && activeFigure.Figure.Color == 1 && item.Figure.Color == 2)
                    || (item.Figure != null && activeFigure != null && activeFigure.Figure != null && activeFigure.Figure.Color == 1 && item.Figure.Color == 1 && activeFigure.Figure.Type == 1 && item.Figure.Type == 4)))) {

                    var isMove = false;
                    if (activeFigure != null) {
                        activeFigure.active = false;
                        isMove = true;

                    }
                    if (isMove && activeFigure.Figure != null
                        && (item.Figure == null || item.Figure.Color != activeFigure.Figure.Color || (item.Figure.Color == activeFigure.Figure.Color && activeFigure.Figure.Type == 1 && item.Figure.Type == 4))) {
                        gameApi.save({ GameId: $scope.chessBoard.GameId, FromX: activeFigure.Position.X, FromY: activeFigure.Position.Y, ToX: item.Position.X, ToY: item.Position.Y, }, function (data) {
                            $scope.chessBoard.GameLog = angular.fromJson(data.GameData.GameLog);
                            $scope.chessBoard.LogIndex = data.GameData.LogIndex;

                        }, function (error) {
                            $scope.prompt = error.data.Message;
                        });
                    } else {
                        item.active = true;
                    }
                };
            }
            var refreshBoardInterval = $interval(function () {
                refreshBoard();
            }, 5000);

            function refreshBoard() {
                var activeColumn = null;
                if ($scope.chessBoard != undefined) {
                    activeColumn = getActiveFigure();
                }
                gameApi.get({ invitationId: $routeParams.invitationId }, function (data) {
                    $scope.chessBoard = data.GameData;
                    $scope.chessBoard.GameLog = angular.fromJson(data.GameData.GameLog);
                    $scope.isCurrentUserHasBlackColor = authService.authentication.UserId === data.GameData.SecondPlayerId;
                    if (activeColumn != null)
                        angular.forEach($scope.chessBoard.GameLog, function (value) {
                            angular.forEach(value, function (item) {
                                if (activeColumn.Position.X == item.Position.X && activeColumn.Position.Y == item.Position.Y) {
                                    item.active = true;
                                }
                            });
                        });
                }, function (error) {
                    $interval.cancel(refreshBoardInterval);
                    $scope.prompt = "Game over!";
                });
            }

            refreshBoard();

            $scope.$on('$destroy', function () {
                $interval.cancel(refreshBoardInterval);
            });

        }
    ])

     .controller('GameViewController', [
        '$scope', '$interval', '$routeParams', 'gameLogApi', 'authService', function ($scope, $interval, $routeParams, gameLogApi, authService) {
            //scene
            //////////////////////
            var centerPoints = {
                //a
                A1: new BABYLON.Vector3(8.8, 2.5, -8.75),
                A2: new BABYLON.Vector3(6.3, 2.5, -8.75),
                A3: new BABYLON.Vector3(3.8, 2.5, -8.75),
                A4: new BABYLON.Vector3(1.3, 2.5, -8.75),
                A5: new BABYLON.Vector3(-1.2, 2.5, -8.75),
                A6: new BABYLON.Vector3(-3.7, 2.5, -8.75),
                A7: new BABYLON.Vector3(-6.2, 2.5, -8.75),
                A8: new BABYLON.Vector3(-8.7, 2.5, -8.75),
                //b
                B1: new BABYLON.Vector3(8.8, 2.5, -6.25),
                B2: new BABYLON.Vector3(6.3, 2.5, -6.25),
                B3: new BABYLON.Vector3(3.8, 2.5, -6.25),
                B4: new BABYLON.Vector3(1.3, 2.5, -6.25),
                B5: new BABYLON.Vector3(-1.2, 2.5, -6.25),
                B6: new BABYLON.Vector3(-3.7, 2.5, -6.25),
                B7: new BABYLON.Vector3(-6.2, 2.5, -6.25),
                B8: new BABYLON.Vector3(-8.7, 2.5, -6.25),
                //c
                C1: new BABYLON.Vector3(8.8, 2.5, -3.75),
                C2: new BABYLON.Vector3(6.3, 2.5, -3.75),
                C3: new BABYLON.Vector3(3.8, 2.5, -3.75),
                C4: new BABYLON.Vector3(1.3, 2.5, -3.75),
                C5: new BABYLON.Vector3(-1.2, 2.5, -3.75),
                C6: new BABYLON.Vector3(-3.7, 2.5, -3.75),
                C7: new BABYLON.Vector3(-6.2, 2.5, -3.75),
                C8: new BABYLON.Vector3(-8.7, 2.5, -3.75),
                //d
                D1: new BABYLON.Vector3(8.8, 2.5, -1.25),
                D2: new BABYLON.Vector3(6.3, 2.5, -1.25),
                D3: new BABYLON.Vector3(3.8, 2.5, -1.25),
                D4: new BABYLON.Vector3(1.3, 2.5, -1.25),
                D5: new BABYLON.Vector3(-1.2, 2.5, -1.25),
                D6: new BABYLON.Vector3(-3.7, 2.5, -1.25),
                D7: new BABYLON.Vector3(-6.2, 2.5, -1.25),
                D8: new BABYLON.Vector3(-8.7, 2.5, -1.25),
                //e
                E1: new BABYLON.Vector3(8.8, 2.5, 1.25),
                E2: new BABYLON.Vector3(6.3, 2.5, 1.25),
                E3: new BABYLON.Vector3(3.8, 2.5, 1.25),
                E4: new BABYLON.Vector3(1.3, 2.5, 1.25),
                E5: new BABYLON.Vector3(-1.2, 2.5, 1.25),
                E6: new BABYLON.Vector3(-3.7, 2.5, 1.25),
                E7: new BABYLON.Vector3(-6.2, 2.5, 1.25),
                E8: new BABYLON.Vector3(-8.7, 2.5, 1.25),
                //f
                F1: new BABYLON.Vector3(8.8, 2.5, 3.75),
                F2: new BABYLON.Vector3(6.3, 2.5, 3.75),
                F3: new BABYLON.Vector3(3.8, 2.5, 3.75),
                F4: new BABYLON.Vector3(1.3, 2.5, 3.75),
                F5: new BABYLON.Vector3(-1.2, 2.5, 3.75),
                F6: new BABYLON.Vector3(-3.7, 2.5, 3.75),
                F7: new BABYLON.Vector3(-6.2, 2.5, 3.75),
                F8: new BABYLON.Vector3(-8.7, 2.5, 3.75),
                //g
                G1: new BABYLON.Vector3(8.8, 2.5, 6.25),
                G2: new BABYLON.Vector3(6.3, 2.5, 6.25),
                G3: new BABYLON.Vector3(3.8, 2.5, 6.25),
                G4: new BABYLON.Vector3(1.3, 2.5, 6.25),
                G5: new BABYLON.Vector3(-1.2, 2.5, 6.25),
                G6: new BABYLON.Vector3(-3.7, 2.5, 6.25),
                G7: new BABYLON.Vector3(-6.2, 2.5, 6.25),
                G8: new BABYLON.Vector3(-8.7, 2.5, 6.25),
                //h
                H1: new BABYLON.Vector3(8.8, 2.5, 8.75),
                H2: new BABYLON.Vector3(6.3, 2.5, 8.75),
                H3: new BABYLON.Vector3(3.8, 2.5, 8.75),
                H4: new BABYLON.Vector3(1.3, 2.5, 8.75),
                H5: new BABYLON.Vector3(-1.2, 2.5, 8.75),
                H6: new BABYLON.Vector3(-3.7, 2.5, 8.75),
                H7: new BABYLON.Vector3(-6.2, 2.5, 8.75),
                H8: new BABYLON.Vector3(-8.7, 2.5, 8.75),
            };

            var canvas = document.getElementById('scene');
            // init Babylon engine
            var engine = new BABYLON.Engine(canvas, true);

            var createScene = function () {

                // This creates a basic Babylon Scene object (non-mesh)
                var scene = new BABYLON.Scene(engine);

                scene.clearColor = new BABYLON.Color3(0, 0, 0);
                scene.shadowsEnabled = true;

                // This creates and positions a free camera (non-mesh)
                var camera = new BABYLON.ArcRotateCamera("Camera", 0, 0.8, 30, BABYLON.Vector3.Zero(), scene);
                camera.lowerBetaLimit = 0.1;
                camera.upperBetaLimit = (Math.PI / 2) * 0.9;
                camera.lowerRadiusLimit = 30;
                camera.upperRadiusLimit = 30;
                camera.attachControl(canvas, true);

                var light = new BABYLON.DirectionalLight("dir01", new BABYLON.Vector3(-1, -2, -1), scene);
                light.position = new BABYLON.Vector3(6, 12, 6);
                light.intensity = 2;

                var lightSphere = BABYLON.Mesh.CreateSphere("sphere", 10, 2, scene);
                lightSphere.position = light.position;
                lightSphere.material = new BABYLON.StandardMaterial("light", scene);
                lightSphere.material.emissiveColor = new BABYLON.Color3(1, 1, 0);

                // Our built-in 'ground' shape. Params: name, width, depth, subdivs, scene
                var plane = BABYLON.Mesh.CreatePlane("plane", 20, scene);
                plane.position.y = 0;
                plane.rotation.x = Math.PI / 2;

                var materialPlane = new BABYLON.StandardMaterial("texturePlane", scene);
                materialPlane.diffuseTexture = new BABYLON.Texture("textures/chessboard_texture_by_sveinjo.png", scene);
                materialPlane.diffuseTexture.uScale = 2.0;//Repeat 5 times on the Vertical Axes
                materialPlane.diffuseTexture.vScale = 2.0;//Repeat 5 times on the Horizontal Axes
                materialPlane.backFaceCulling = false;//Allways show the front and the back of an element
                plane.material = materialPlane;

                BABYLON.SceneLoader.ImportMesh("", "textures/figures/", "figures.babylon.json", scene, function () {
                    var shadowGenerator = new BABYLON.ShadowGenerator(1024, light);
                    for (var i = 5; i < scene.meshes.length; i++) {
                        scene.meshes[i].isVisible = false;
                        scene.meshes[i].isUse = false;
                        if (scene.meshes[i].name.indexOf("black") > -1) {
                            scene.meshes[i].rotation.y = -Math.PI / 2;
                        }
                        if (scene.meshes[i].name.indexOf("white") > -1) {
                            scene.meshes[i].rotation.y = Math.PI / 2;
                        }
                        shadowGenerator.getShadowMap().renderList.push(scene.meshes[i]);
                    }
                    //// Shadows
                    shadowGenerator.useVarianceShadowMap = true;
                    plane.receiveShadows = true;

                    refreshBoard();
                });
                return scene;

            };
            var scene = createScene();

            window.addEventListener("click", function (evt) {
                var pickResult = scene.pick(evt.clientX, evt.clientY);
                console.log(pickResult);
            });

            function getMeshIdByName(name) {
                var result = -1;
                angular.forEach(scene.meshes, function (value, key) {
                    if (value.name == name) result = key;
                });
                return result;
            };

            engine.runRenderLoop(function () {
                scene.render();
            });
            //

            var refreshBoardInterval;
            $scope.logIndex = 1;
            $scope.isStop = true;
            $scope.refreshTime = 100;

            $scope.start = function () {
                refreshBoardInterval = $interval(function () {
                    refreshBoard();
                }, $scope.refreshTime);
                $scope.isStop = false;
            }

            $scope.stop = function () {
                $scope.isStop = true;
                $interval.cancel(refreshBoardInterval);
            }

            function getCenterPointsByPosition(position) {
                var key = position.X + position.Y;
                return centerPoints[key];
            }

            function refreshTempProperties() {
                for (var i = 5; i < scene.meshes.length; i++) {
                    scene.meshes[i].isUse = false;
                    scene.meshes[i].isVisible = false;
                };
            }

            function refreshBoard() {
                gameLogApi.get({ invitationId: $routeParams.invitationId, logId: $scope.logIndex }, function (data) {
                    refreshTempProperties();
                    $scope.moveCount = data.Count;
                    var gameLog = angular.fromJson(data.GameData.Log);
                    angular.forEach(gameLog, function (row) {
                        angular.forEach(row, function (cell) {
                            if (cell.Figure) {
                                if (cell.Figure.Color == 1) {
                                    if (cell.Figure.Type == 1) {
                                        scene.meshes[getMeshIdByName('black_king')].position = getCenterPointsByPosition(cell.Position);
                                        scene.meshes[getMeshIdByName('black_king')].isVisible = true;
                                        scene.meshes[getMeshIdByName('black_king')].position.y = 2.5;
                                    } else if (cell.Figure.Type == 2) {
                                        scene.meshes[getMeshIdByName('black_queen')].position = getCenterPointsByPosition(cell.Position);
                                        scene.meshes[getMeshIdByName('black_queen')].isVisible = true;
                                        scene.meshes[getMeshIdByName('black_queen')].position.y = 2.5;
                                    } else if (cell.Figure.Type == 3) {
                                        var mesh = !scene.meshes[getMeshIdByName('black_bishop1')].isUse ? scene.meshes[getMeshIdByName('black_bishop1')] : scene.meshes[getMeshIdByName('black_bishop2')];
                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1.5;
                                    } else if (cell.Figure.Type == 4) {
                                        var mesh = !scene.meshes[getMeshIdByName('black_rook1')].isUse ? scene.meshes[getMeshIdByName('black_rook1')] : scene.meshes[getMeshIdByName('black_rook2')];
                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1.5;
                                    } else if (cell.Figure.Type == 5) { 
                                        var mesh = !scene.meshes[getMeshIdByName('black_knight1')].isUse ? scene.meshes[getMeshIdByName('black_knight1')] : scene.meshes[getMeshIdByName('black_knight2')];
                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1.5;
                                    }
                                    else if (cell.Figure.Type == 6) {
                                        var mesh;
                                        if (!scene.meshes[getMeshIdByName('black_pawn1')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn1')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('black_pawn2')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn2')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('black_pawn3')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn3')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('black_pawn4')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn4')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('black_pawn5')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn5')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('black_pawn6')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn6')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('black_pawn7')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn7')];
                                        }
                                        else {
                                            mesh = scene.meshes[getMeshIdByName('black_pawn8')];
                                        }

                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1;
                                    }
                                }
                                else if (cell.Figure.Color == 2) {
                                    if (cell.Figure.Type == 1) {
                                        scene.meshes[getMeshIdByName('white_king')].position = getCenterPointsByPosition(cell.Position);
                                        scene.meshes[getMeshIdByName('white_king')].isVisible = true;
                                        scene.meshes[getMeshIdByName('white_king')].position.y = 2.5;
                                    } else if (cell.Figure.Type == 2) {
                                        scene.meshes[getMeshIdByName('white_queen')].position = getCenterPointsByPosition(cell.Position);
                                        scene.meshes[getMeshIdByName('white_queen')].isVisible = true;
                                        scene.meshes[getMeshIdByName('white_queen')].position.y = 2.5;
                                    } else if (cell.Figure.Type == 3) {
                                        var mesh = !scene.meshes[getMeshIdByName('white_bishop1')].isUse ? scene.meshes[getMeshIdByName('white_bishop1')] : scene.meshes[getMeshIdByName('white_bishop2')];
                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1.5;
                                    } else if (cell.Figure.Type == 4) {
                                        var mesh = !scene.meshes[getMeshIdByName('white_rook1')].isUse ? scene.meshes[getMeshIdByName('white_rook1')] : scene.meshes[getMeshIdByName('white_rook2')];
                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1.5;
                                    } else if (cell.Figure.Type == 5) {
                                        var mesh = !scene.meshes[getMeshIdByName('white_knight1')].isUse ? scene.meshes[getMeshIdByName('white_knight1')] : scene.meshes[getMeshIdByName('white_knight2')];
                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1.5;
                                    }
                                    else if (cell.Figure.Type == 6) {
                                        var mesh;
                                        if (!scene.meshes[getMeshIdByName('white_pawn1')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn1')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('white_pawn2')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn2')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('white_pawn3')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn3')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('white_pawn4')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn4')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('white_pawn5')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn5')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('white_pawn6')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn6')];
                                        }
                                        else if (!scene.meshes[getMeshIdByName('white_pawn7')].isUse) {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn7')];
                                        }
                                        else {
                                            mesh = scene.meshes[getMeshIdByName('white_pawn8')];
                                        }

                                        mesh.isUse = true;
                                        mesh.isVisible = true;
                                        mesh.position = getCenterPointsByPosition(cell.Position);
                                        mesh.position.y = 1;
                                    }
                                }
                            }
                        });
                    });

                    if (data.Count > $scope.logIndex) {
                        $scope.logIndex += 1;
                        $scope.chessBoard = data.GameData;
                        $scope.chessBoard.GameLog = angular.fromJson(data.GameData.Log);
                    } else {
                        $scope.chessBoard = data.GameData;
                        $scope.chessBoard.GameLog = angular.fromJson(data.GameData.Log);
                        $interval.cancel(refreshBoardInterval);
                    }
                });
            }

            $scope.$on('$destroy', function () {
                $interval.cancel(refreshBoardInterval);
            });
        }])

    //Error 404
    .controller('Error404Controller', ['$scope', '$rootScope', '$location', '$window', 'authService', function ($scope, $rootScope, $location, $window, authService) {

        if (authService.authentication.isAuth == false) {
            $location.path('/Login');
        } else {
            $rootScope.isLoading = false;
            $scope.$root.title = 'Error 404: Page Not Found';
        }
    }]);



