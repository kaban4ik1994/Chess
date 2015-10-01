
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
        '$scope', '$interval', '$routeParams', 'gameApi', 'authService', 'blockUI', function ($scope, $interval, $routeParams, gameApi, authService, blockUI) {
        	$scope.prompt = '';

        	$scope.selectedItem = function (pickResult) {
        		var clickingChessPosition = customScene.getChessPositionByPosition(pickResult.pickedPoint);
        		var activeFigureId = customScene.getActiveFigureKey();
        		var activateFigureChessPosition = {};
        		var currentMeshId = -1;
        		if (activeFigureId != -1) {
        			activateFigureChessPosition = customScene.getChessPositionByPosition(scene.meshes[activeFigureId].position);
        		}
        		if (pickResult.hit) {
        			currentMeshId = customScene.getMeshIdByPosition(customScene.roundPoint(pickResult.pickedPoint));

        		}

        		$scope.prompt = undefined;
        		if (($scope.chessBoard.LogIndex % 2 != 0
                    && authService.authentication.UserId == $scope.chessBoard.FirstPlayerId
                    && (currentMeshId == -1 || scene.meshes[currentMeshId].figureColor == 2
                    || (currentMeshId != -1 && activeFigureId != -1 && scene.meshes[activeFigureId].figureColor == 2 && scene.meshes[currentMeshId].figureColor == 1)
                    || (currentMeshId != -1 && activeFigureId != -1 && scene.meshes[activeFigureId].figureColor == 2 && scene.meshes[currentMeshId].figureColor == 2 && scene.meshes[activeFigureId].figureType == 1 && scene.meshes[currentMeshId].figureType == 4)))
                    || ($scope.chessBoard.LogIndex % 2 == 0
                    && authService.authentication.UserId == $scope.chessBoard.SecondPlayerId
                    && (currentMeshId == -1 || scene.meshes[currentMeshId].figureColor == 1
                    || (currentMeshId != -1 && activeFigureId != -1 && scene.meshes[activeFigureId].figureColor == 1 && scene.meshes[currentMeshId].figureColor == 2)
                    || (currentMeshId != -1 && activeFigureId != -1 && scene.meshes[activeFigureId].figureColor == 1 && scene.meshes[currentMeshId].figureColor == 1 && scene.meshes[activeFigureId].figureType == 1 && scene.meshes[currentMeshId].figureType == 4)))) {

        			var isMove = false;
        			if (activeFigureId != null) {
        				customScene.refreshSelectingMesh();
        				isMove = true;

        			}
        			if (isMove && activeFigureId != -1
                        && (currentMeshId == -1 || scene.meshes[currentMeshId].figureColor != scene.meshes[activeFigureId].figureColor || (scene.meshes[currentMeshId].figureColor == scene.meshes[activeFigureId].figureColor && scene.meshes[activeFigureId].figureType == 1 && scene.meshes[currentMeshId].figureType == 4))) {
        				gameApi.save({ GameId: $scope.chessBoard.GameId, FromX: activateFigureChessPosition.X, FromY: activateFigureChessPosition.Y, ToX: clickingChessPosition.X, ToY: clickingChessPosition.Y, }, function (data) {
					        refreshBoard();
        					$scope.chessBoard.GameLog = angular.fromJson(data.GameData.GameLog);
        					$scope.chessBoard.LogIndex = data.GameData.LogIndex;

        				}, function (error) {
        					$scope.prompt = error.data.Message;
        				});
        			} else {
        				if (currentMeshId != -1) {
        					customScene.selectMeshById(currentMeshId);
        				}
        			}
        		};
        	}

        	//scene
        	blockUI.start();

        	function refreshBoard() {
        		var selectedFigureKey = customScene.getActiveFigureKey();
        		gameApi.get({ invitationId: $routeParams.invitationId, logId: $scope.logIndex }, function (data) {

        			customScene.refreshTempProperties();
        			$scope.moveCount = data.Count;
        			$scope.chessBoard = data.GameData;
        			$scope.chessBoard.GameLog = angular.fromJson(data.GameData.GameLog);
        			var gameLog = angular.fromJson(data.GameData.GameLog);
        			angular.forEach(gameLog, function (row) {
        				angular.forEach(row, function (cell) {
        					if (cell.Figure) {
        						if (cell.Figure.Color == 1) {
        							if (cell.Figure.Type == 1) {
        								scene.meshes[customScene.getMeshIdByName('black_king')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('black_king')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('black_king')].position.y = 2.5;
        								scene.meshes[customScene.getMeshIdByName('black_king')].figureColor = 1;
        								scene.meshes[customScene.getMeshIdByName('black_king')].figureType = 1;

        							} else if (cell.Figure.Type == 2) {
        								scene.meshes[customScene.getMeshIdByName('black_queen')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('black_queen')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('black_queen')].position.y = 2.5;
        								scene.meshes[customScene.getMeshIdByName('black_queen')].figureColor = 1;
        								scene.meshes[customScene.getMeshIdByName('black_king')].figureType = 2;

        							} else if (cell.Figure.Type == 3) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('black_bishop1')].isUse ? scene.meshes[customScene.getMeshIdByName('black_bishop1')] : scene.meshes[customScene.getMeshIdByName('black_bishop2')];
        								mesh.figureType = 3;
        								mesh.figureColor = 1;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 4) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('black_rook1')].isUse ? scene.meshes[customScene.getMeshIdByName('black_rook1')] : scene.meshes[customScene.getMeshIdByName('black_rook2')];
        								mesh.figureType = 4;
        								mesh.figureColor = 1;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 5) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('black_knight1')].isUse ? scene.meshes[customScene.getMeshIdByName('black_knight1')] : scene.meshes[customScene.getMeshIdByName('black_knight2')];
        								mesh.figureType = 5;
        								mesh.figureColor = 1;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 6) {
        								var mesh;
        								if (!scene.meshes[customScene.getMeshIdByName('black_pawn1')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn1')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn2')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn2')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn3')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn3')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn4')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn4')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn5')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn5')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn6')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn6')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn7')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn7')];
        								} else {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn8')];
        								}
        								mesh.figureType = 6;
        								mesh.figureColor = 1;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1;
        							}
        						} else if (cell.Figure.Color == 2) {
        							if (cell.Figure.Type == 1) {
        								scene.meshes[customScene.getMeshIdByName('white_king')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('white_king')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('white_king')].position.y = 2.5;
        								scene.meshes[customScene.getMeshIdByName('white_king')].figureColor = 2;
        								scene.meshes[customScene.getMeshIdByName('white_king')].figureType = 1;
        							} else if (cell.Figure.Type == 2) {
        								scene.meshes[customScene.getMeshIdByName('white_queen')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('white_queen')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('white_queen')].position.y = 2.5;
        								scene.meshes[customScene.getMeshIdByName('white_queen')].figureColor = 2;
        								scene.meshes[customScene.getMeshIdByName('white_queen')].figureType = 2;
        							} else if (cell.Figure.Type == 3) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('white_bishop1')].isUse ? scene.meshes[customScene.getMeshIdByName('white_bishop1')] : scene.meshes[customScene.getMeshIdByName('white_bishop2')];
        								mesh.figureType = 3;
        								mesh.figureColor = 2;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 4) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('white_rook1')].isUse ? scene.meshes[customScene.getMeshIdByName('white_rook1')] : scene.meshes[customScene.getMeshIdByName('white_rook2')];
        								mesh.figureType = 4;
        								mesh.figureColor = 2;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 5) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('white_knight1')].isUse ? scene.meshes[customScene.getMeshIdByName('white_knight1')] : scene.meshes[customScene.getMeshIdByName('white_knight2')];
        								mesh.figureType = 5;
        								mesh.figureColor = 2;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 6) {
        								var mesh;
        								if (!scene.meshes[customScene.getMeshIdByName('white_pawn1')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn1')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn2')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn2')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn3')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn3')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn4')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn4')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn5')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn5')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn6')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn6')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn7')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn7')];
        								} else {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn8')];
        								}
        								mesh.figureType = 6;
        								mesh.figureColor = 2;
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1;
        							}
        						}
        					}
        				});
        			});

        			customScene.refreshSelectingMesh();
        			if (selectedFigureKey != -1) {
        				customScene.selectMeshById(selectedFigureKey);
        			}
        		}, function (error) {
        			$interval.cancel(refreshBoardInterval);
        			$scope.prompt = "Game over!";
        		});
        	}

        	var functionApplyAfterLoadMesh = function () {
        		blockUI.stop();
        		refreshBoard();
        	}
        	//

        	var canvas = document.getElementById('scene');
        	// init Babylon engine
        	var engine = new BABYLON.Engine(canvas, true);
        	var customScene = new CustomScene(canvas, engine, functionApplyAfterLoadMesh);
        	var scene = customScene.getScene();
        	engine.runRenderLoop(function () {
        		scene.render();
        	});

        	var refreshBoardInterval = $interval(function () {
		        console.log(1);
        		refreshBoard();
        	}, 5000);

        	window.addEventListener("click", function () {
        		// We try to pick an object
        		var pickResult = scene.pick(scene.pointerX, scene.pointerY);
        		$scope.selectedItem(pickResult);
        	});

        	$scope.$on('$destroy', function () {
        		$interval.cancel(refreshBoardInterval);
        	});

        }
    ])

     .controller('GameViewController', [
        '$scope', '$interval', '$routeParams', 'gameLogApi', 'authService', 'blockUI', function ($scope, $interval, $routeParams, gameLogApi, authService, blockUI) {
        	//scene
        	blockUI.start();

        	var refreshBoard = function () {
        		gameLogApi.get({ invitationId: $routeParams.invitationId, logId: $scope.logIndex }, function (data) {
        			customScene.refreshTempProperties();
        			$scope.moveCount = data.Count;
        			var gameLog = angular.fromJson(data.GameData.Log);
        			angular.forEach(gameLog, function (row) {
        				angular.forEach(row, function (cell) {
        					if (cell.Figure) {
        						if (cell.Figure.Color == 1) {
        							if (cell.Figure.Type == 1) {
        								scene.meshes[customScene.getMeshIdByName('black_king')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('black_king')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('black_king')].position.y = 2.5;
        							} else if (cell.Figure.Type == 2) {
        								scene.meshes[customScene.getMeshIdByName('black_queen')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('black_queen')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('black_queen')].position.y = 2.5;
        							} else if (cell.Figure.Type == 3) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('black_bishop1')].isUse ? scene.meshes[customScene.getMeshIdByName('black_bishop1')] : scene.meshes[customScene.getMeshIdByName('black_bishop2')];
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 4) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('black_rook1')].isUse ? scene.meshes[customScene.getMeshIdByName('black_rook1')] : scene.meshes[customScene.getMeshIdByName('black_rook2')];
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 5) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('black_knight1')].isUse ? scene.meshes[customScene.getMeshIdByName('black_knight1')] : scene.meshes[customScene.getMeshIdByName('black_knight2')];
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 6) {
        								var mesh;
        								if (!scene.meshes[customScene.getMeshIdByName('black_pawn1')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn1')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn2')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn2')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn3')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn3')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn4')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn4')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn5')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn5')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn6')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn6')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('black_pawn7')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn7')];
        								} else {
        									mesh = scene.meshes[customScene.getMeshIdByName('black_pawn8')];
        								}

        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1;
        							}
        						} else if (cell.Figure.Color == 2) {
        							if (cell.Figure.Type == 1) {
        								scene.meshes[customScene.getMeshIdByName('white_king')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('white_king')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('white_king')].position.y = 2.5;
        							} else if (cell.Figure.Type == 2) {
        								scene.meshes[customScene.getMeshIdByName('white_queen')].position = customScene.getCenterPointsByPosition(cell.Position);
        								scene.meshes[customScene.getMeshIdByName('white_queen')].isVisible = true;
        								scene.meshes[customScene.getMeshIdByName('white_queen')].position.y = 2.5;
        							} else if (cell.Figure.Type == 3) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('white_bishop1')].isUse ? scene.meshes[customScene.getMeshIdByName('white_bishop1')] : scene.meshes[customScene.getMeshIdByName('white_bishop2')];
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 4) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('white_rook1')].isUse ? scene.meshes[customScene.getMeshIdByName('white_rook1')] : scene.meshes[customScene.getMeshIdByName('white_rook2')];
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 5) {
        								var mesh = !scene.meshes[customScene.getMeshIdByName('white_knight1')].isUse ? scene.meshes[customScene.getMeshIdByName('white_knight1')] : scene.meshes[customScene.getMeshIdByName('white_knight2')];
        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
        								mesh.position.y = 1.5;
        							} else if (cell.Figure.Type == 6) {
        								var mesh;
        								if (!scene.meshes[customScene.getMeshIdByName('white_pawn1')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn1')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn2')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn2')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn3')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn3')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn4')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn4')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn5')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn5')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn6')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn6')];
        								} else if (!scene.meshes[customScene.getMeshIdByName('white_pawn7')].isUse) {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn7')];
        								} else {
        									mesh = scene.meshes[customScene.getMeshIdByName('white_pawn8')];
        								}

        								mesh.isUse = true;
        								mesh.isVisible = true;
        								mesh.position = customScene.getCenterPointsByPosition(cell.Position);
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

        	var functionApplyAfterLoadMesh = function () {
        		blockUI.stop();
        		refreshBoard();
        	}

        	var canvas = document.getElementById('scene');
        	// init Babylon engine
        	var engine = new BABYLON.Engine(canvas, true);
        	var customScene = new CustomScene(canvas, engine, functionApplyAfterLoadMesh);
        	var scene = customScene.getScene();
        	engine.runRenderLoop(function () {
        		scene.render();
        	});
        	// customScene.renderScene();

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



