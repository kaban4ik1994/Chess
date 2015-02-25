
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
        '$scope', '$rootScope', '$location', 'authService', 'ngAuthSettings', 'ngProgress',
        function ($scope, $rootScope, $location, authService, ngAuthSettings, ngProgress) {

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

                $scope.deleteMessage = function () {
                    $scope.message = null;
                    $scope.showErrorMessages = true
                }


                $scope.login = function () {
                    ngProgress.start();
                    $scope.showErrorMessages = true;
                    authService.login($scope.loginData).then(function (response) {
                        ngProgress.complete();
                        $location.path('/Home');
                        $rootScope.isHideHeader = false;
                        $rootScope.isHideLeftPanel = false;
                        $rootScope.isHideFooter = false;
                        $rootScope.isHideMainContent = false;
                    },
                        function (err) {
                            ngProgress.complete();
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

    // Path: /Home
    .controller('HomeController', [
        '$scope', '$rootScope', 'authService', '$location', function ($scope, $rootScope, authService, $location) {
            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {

            }
        }
    ])

    // Path: /Orders
    .controller('OrdersController', [
        '$scope', '$rootScope', 'orderApi', 'orderTableApi', 'ngTableParams', 'ngProgress', 'authService', '$location',
        function ($scope, $rootScope, orderApi, orderTableApi, ngTableParams, ngProgress, authService, $location) {

            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {

                $scope.isLoading = true;

                //init ngTable
                $scope.tableParams = new ngTableParams({
                    page: 1, // show first page
                    count: 10, // count per page
                    sorting: {},
                }, {
                    total: 0, // length of data
                    counts: false,
                    getData: function ($defer, params) {
                        // ajax request to api
                        ngProgress.start();
                        orderTableApi.getItems(params.url(), function (data) {
                            // update table params
                            params.total(data.Count);
                            // set new data
                            $defer.resolve(data.Items);
                            $scope.isLoading = false;
                            $scope.opened = false;
                            ngProgress.complete();
                        }, function (error) {
                            $scope.isLoading = false;
                            ngProgress.complete();
                        });
                    }
                });

                //delete order
                $scope.deleteOrder = function (orderId) {
                    $.SmartMessageBox({
                        title: "Удалить заказ?",
                        content: "Вы действительно хотите удалить заказ?",
                        buttons: '[Нет][Да]'
                    }, function (ButtonPressed) {
                        if (ButtonPressed === "Да") {
                            ngProgress.start();
                            orderApi.deleteItem({ Id: orderId }, function (data) {
                                ngProgress.complete();
                                $.smallBox({
                                    title: 'Заказ успешно удален.',
                                    content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                                    color: "#296191",
                                    iconSmall: "fa fa-check bounce animated",
                                    timeout: 3000
                                });
                                $scope.tableParams.reload();
                            }, function (error) {
                                ngProgress.complete();
                                $.smallBox({
                                    title: 'Не удалось удалить заказ.',
                                    content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                                    color: "#FF0000",
                                    iconSmall: "fa fa-check bounce animated",
                                    timeout: 3000
                                });
                            });
                        }
                    });
                }
            }
        }
    ])

    // Path: /Order
    .controller('OrderController', [
        '$scope', '$rootScope', '$location', 'clientApi', '$http', '$routeParams', 'authService',
        'orderApi', 'companyApi', 'airlineApi', 'airportApi', 'hotelApi', 'managerApi', 'countryApi', 'regionApi', 'ngProgress',
        function ($scope, $rootScope, $location, clientApi, $http, $routeParams, authService,
            orderApi, companyApi, airlineApi, airportApi, hotelApi, managerApi, countryApi, regionApi, ngProgress) {

            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {

                function convertOrderModelToOrderViewModel(item) {
                    var result = {
                        number: item.Number,
                        orderId: item.OrderId,
                        date: item.Date,
                        manager: item.Manager,
                        client: item.Client,
                        company: item.Company,
                        travelers: []
                    };
                    return result;
                }

                $scope.clientApi = clientApi;
                $scope.companyApi = companyApi;
                $scope.airlineApi = airlineApi;
                $scope.airportApi = airportApi;
                $scope.hotelApi = hotelApi;
                $scope.managerApi = managerApi;
                $scope.countryApi = countryApi;
                $scope.regionApi = regionApi;

                $scope.order = { orderId: 0, client: {}, manager: {}, company: {}, travelers: [], airticket: {} };


                $scope.filter = {};
                $scope.typePerson = 'PhysicalPerson';

                if ($routeParams.orderId && $routeParams.orderId != 0) {
                    ngProgress.start();
                    $rootScope.isLoading = true;
                    orderApi.getItems({ "filter[OrderId]": $routeParams.orderId, count: 1 }, function (data) {

                        if (!data.Items[0]) $location.path('/Orders');

                        else {
                            if (data.Items[0].Company) {
                                $scope.typePerson = 'LegalPerson';
                            }
                            $scope.order = convertOrderModelToOrderViewModel(data.Items[0]);

                            data.Items[0].Travellers.forEach(function (element) {
                                $scope.order.travelers.push({ id: element.PersonId, label: element.Name + ' (' + element.Email + ')' });
                            });
                        }
                        $rootScope.isLoading = false;
                        ngProgress.complete();
                    });
                }

                $scope.saveOrder = function () {
                    ngProgress.start();
                    var travellerIds = [];
                    $scope.order.travelers.forEach(function (element) {
                        travellerIds.push(element.id);
                    });

                    var orderToSave = {
                        OrderId: $scope.order.orderId,
                        Number: $scope.order.number,
                        Date: $scope.order.date,
                        ManagerId: $scope.order.manager.ManagerId,
                        ClientId: $scope.order.client.PersonId,
                        CompanyId: $scope.order.company ? $scope.order.company.CompanyId : null,
                        TravellerIds: travellerIds
                    };

                    var saveOrderFucntion = $scope.order.orderId == 0 ? orderApi.addItem : orderApi.saveItem;

                    saveOrderFucntion(orderToSave, function (result) {
                        ngProgress.complete();
                        $scope.order.orderId = result.Id;
                        $.smallBox({
                            title: 'Заказ успешно сохранен.',
                            content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                            color: "#296191",
                            iconSmall: "fa fa-check bounce animated",
                            timeout: 3000
                        });

                    }, function (error) {
                        ngProgress.complete();
                        $.smallBox({
                            title: 'Не удалось сохранить заказ.',
                            content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                            color: "#FF0000",
                            iconSmall: "fa fa-check bounce animated",
                            timeout: 3000
                        });
                    });
                };
            }
        }
    ])

    //Path: /Order/:orderId/Services
    .controller('ServicesController', [
        '$scope', 'ngTableParams', 'ngProgress', 'authService', 'serviceApi', 'currencyApi', '$routeParams', '$location',
        'orderApi', 'companyApi', 'airlineApi', 'airportApi', 'hotelApi', 'managerApi', 'countryApi', 'regionApi', 'clientApi',
        'ticketServiceApi', 'transferServiceApi', 'hotelServiceApi', 'tourPackageServiceApi',
        function ($scope, ngTableParams, ngProgress, authService, serviceApi, currencyApi, $routeParams, $location,
            orderApi, companyApi, airlineApi, airportApi, hotelApi, managerApi, countryApi, regionApi, clientApi,
            ticketServiceApi, transferServiceApi, hotelServiceApi, tourPackageServiceApi) {

            //alert("Start services controller");

            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {

                $scope.clientApi = clientApi;
                $scope.companyApi = companyApi;
                $scope.airlineApi = airlineApi;
                $scope.airportApi = airportApi;
                $scope.hotelApi = hotelApi;
                $scope.managerApi = managerApi;
                $scope.countryApi = countryApi;
                $scope.regionApi = regionApi;

                $scope.showOptions = {
                    isShowServicesGrid: true,
                    isShowSelectionMenu: true,
                    ShowFormType: ''
                };

                $scope.orderId = $routeParams.orderId;

                $scope.currencyApi = currencyApi;

                $scope.isLoading = true;

                //alert("Before set function to get data from API");

                //init ngTable
                $scope.tableParamsServices = new ngTableParams({
                    page: 1, // show first page
                    count: 10, // count per page
                    'filter[OrderId]': '',
                    sorting: {},
                }, {
                    total: 0, // length of data
                    counts: false,
                    getData: function ($defer, params) {
                        params.filter()['OrderId'] = $routeParams.orderId;
                        // ajax request to api
                        ngProgress.start();
                        serviceApi.getItems(params.url(), function (data) {
                            // update table params
                            params.total(data.Count);
                            // set new data
                            //alert("Get data from API");

                            $defer.resolve(data.Items);
                            $scope.isLoading = false;
                            $scope.opened = false;
                            ngProgress.complete();
                        }, function (error) {
                            $scope.isLoading = false;
                            ngProgress.complete();
                        });
                    }
                });

                //alert("After set function to get data from API")

                //delete service
                $scope.deleteService = function (serviceId, serviceTypeId) {
                    var api;
                    if (serviceTypeId == 1) api = ticketServiceApi;
                    else if (serviceTypeId == 2) api = hotelServiceApi;
                    else if (serviceTypeId == 3) api = tourPackageServiceApi;
                    else if (serviceTypeId == 4) api = transferServiceApi;

                    if (api == null) return;

                    ngProgress.start();
                    api.deleteItem({ Id: serviceId }, function (data) {
                        ngProgress.complete();
                        $.smallBox({
                            title: 'Услуга успешно удалена.',
                            content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                            color: "#296191",
                            iconSmall: "fa fa-check bounce animated",
                            timeout: 3000
                        });
                        $scope.tableParamsServices.reload();
                    }, function (error) {
                        ngProgress.complete();
                        $.smallBox({
                            title: 'Не удалось удалить услугу.',
                            content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                            color: "#FF0000",
                            iconSmall: "fa fa-check bounce animated",
                            timeout: 3000
                        });
                    });
                }
            }
        }])

    //Path: /Order/:orderId/Services/Service/ and /Order/:orderId/Services/:serviceType/:serviceId
    .controller('ServiceController', [
        '$scope', '$routeParams', 'authService', 'ticketServiceApi', 'transferServiceApi', 'hotelServiceApi', 'tourPackageServiceApi', 'roomTypeApi', 'feedTypeApi',
         'orderApi', 'companyApi', 'airlineApi', 'airportApi', 'hotelApi', 'managerApi', 'countryApi', 'regionApi', 'clientApi', 'currencyApi',
        function ($scope, $routeParams, authService, ticketServiceApi, transferServiceApi, hotelServiceApi, tourPackageServiceApi, roomTypeApi, feedTypeApi,
             orderApi, companyApi, airlineApi, airportApi, hotelApi, managerApi, countryApi, regionApi, clientApi, currencyApi) {

            if (authService.authentication.isAuth == false) {
                $location.path('/Login');
            } else {

                $scope.showOptions = {
                    isShowServicesGrid: true,
                    isShowSelectionMenu: true,
                    ShowFormType: '',
                    isTransferFormDisabled: false,
                    isHotelFormDisabled: false,
                    isTicketFormDisabled: false,
                    isPackageFormDisabled: false
                };

                $scope.clientApi = clientApi;
                $scope.companyApi = companyApi;
                $scope.airlineApi = airlineApi;
                $scope.airportApi = airportApi;
                $scope.hotelApi = hotelApi;
                $scope.managerApi = managerApi;
                $scope.countryApi = countryApi;
                $scope.regionApi = regionApi;
                $scope.currencyApi = currencyApi;

                if ($routeParams.serviceType == 'Hotel') {
                    $scope.showOptions.ShowFormType = 'Hotel';
                    $scope.showOptions.isTransferFormDisabled = true;
                    $scope.showOptions.isHotelFormDisabled = false;
                    $scope.showOptions.isTicketFormDisabled = true;
                    $scope.showOptions.isPackageFormDisabled = true;
                } else if ($routeParams.serviceType == 'Ticket') {
                    $scope.showOptions.ShowFormType = 'Ticket';
                    $scope.showOptions.isTransferFormDisabled = true;
                    $scope.showOptions.isHotelFormDisabled = true;
                    $scope.showOptions.isTicketFormDisabled = false;
                    $scope.showOptions.isPackageFormDisabled = true;
                } else if ($routeParams.serviceType == 'Package') {
                    $scope.showOptions.ShowFormType = 'TourPackage';
                    $scope.showOptions.isTransferFormDisabled = true;
                    $scope.showOptions.isHotelFormDisabled = true;
                    $scope.showOptions.isTicketFormDisabled = true;
                    $scope.showOptions.isPackageFormDisabled = false;
                } else if ($routeParams.serviceType == 'Transfer') {
                    $scope.showOptions.ShowFormType = 'Transfer';
                    $scope.showOptions.isTransferFormDisabled = false;
                    $scope.showOptions.isHotelFormDisabled = true;
                    $scope.showOptions.isTicketFormDisabled = true;
                    $scope.showOptions.isPackageFormDisabled = true;
                }

                $scope.selector = function (model) {
                    return {
                        CountryId: model.Country.CountryId,
                        Name: model.Name,
                        RegionId: model.Region.RegionId,
                        Stars: model.Stars
                    };
                };

                $scope.roomTypes = roomTypeApi.getItems();
                $scope.feedTypes = feedTypeApi.getItems();

                $scope.serviceType = $routeParams.serviceType;
                $scope.serviceId = $routeParams.serviceId;
                $scope.orderId = $routeParams.orderId;
            }
        }])

    //Autocomplete Dropdown list travelers controller
    .controller('DropdownlistTravelersController', [
       '$scope', 'travelerApi', function ($scope, travelerApi) {

           $scope.getTravelers = function (term, result) {
               var travelerIds = [];
               console.log($scope.order.travelers);
               $scope.order.travelers.forEach(function (value) {
                   travelerIds.push(value.id);
               });

               var api = travelerApi.getTravelers({ "filter[FirstName,SecondName,Email]": term, count: NumberOfTravelersForAutoCompleteDropDown, "excludingFilter[PersonId]": travelerIds });

               api.$promise.then(function (data) {
                   var travelers = [];
                   angular.forEach(data.Items, function (item) {
                       travelers.push(
                       { id: item.PersonId, label: item.Name + ' (' + item.Email + ')' });
                   });
                   result(travelers);
               });
           };
       }
    ])

    //Air ticket service controller
    .controller('AirTicketServiceController', [
        '$scope', '$rootScope', 'ticketServiceApi', 'ngProgress', '$location', function ($scope, $rootScope, ticketServiceApi, ngProgress, $location) {

            function convertAirticketViewModelToairticketmodel(airticket) {
                var result = {
                    IsRoundTrip: airticket.IsRoundTrip,
                    DepartureDate: airticket.DepartureDate,
                    ReturnDate: airticket.ReturnDate ? airticket.ReturnDate : null,
                    AirlineId: airticket.airline.AirlineId,
                    DepartureAirportId: airticket.departureAirport.AirportId,
                    ReturnAirportId: airticket.returnAirport ? airticket.returnAirport.AirportId : null,
                    Price: airticket.price,
                    Comission: airticket.commission,
                    Number: airticket.number,
                    OrderId: angular.copy($scope.orderId),
                    CurrencyId: airticket.currency.CurrencyId,
                    AirTicketId: airticket.AirTicketId
                };
                return result;
            }

            function convertAirticketModelToAirticketViewModel(airticket) {
                var result = {
                    IsRoundTrip: airticket.IsRoundTrip,
                    DepartureDate: airticket.DepartureDate,
                    ReturnDate: airticket.ReturnDate,
                    airline: airticket.Airline,
                    departureAirport: airticket.DepartureAirport,
                    returnAirport: airticket.ReturnAirport,
                    price: airticket.ServiceData.Price,
                    commission: airticket.ServiceData.Comission,
                    number: airticket.ServiceData.Number,
                    OrderId: angular.copy($scope.orderId),
                    currency: airticket.ServiceData.Currency,
                    AirTicketId: airticket.AirTicketId
                }
                return result;
            }

            $scope.resetReturnAirportAndReturnDate = function () {
                $scope.airticket.IsRoundTrip = !$scope.airticket.IsRoundTrip;
                if (!$scope.airticket.IsRoundTrip) {
                    $scope.airticket.ReturnDate = '';
                    $scope.airticket.returnAirport = {};
                }
            }

            $scope.airticket = { AirTicketId: 0 };

            if ($scope.serviceType == 'Ticket') {
                ngProgress.start();
                $rootScope.isLoading = true;
                ticketServiceApi.getItems({ Id: $scope.serviceId }, function (data) {
                    ngProgress.complete();
                    $rootScope.isLoading = false;
                    console.log(data);
                    if (data.Items.length == 0 || !data.Items) {
                        $location.path('/Order/' + $scope.orderId + '/Services/');
                    } else {
                        $scope.airticket = convertAirticketModelToAirticketViewModel(data.Items[0]);
                    }
                }, function () {
                    ngProgress.complete();
                    $rootScope.isLoading = false;
                    $location.path('/Order/' + $scope.orderId + '/Services/');
                });
            }

            $scope.save = function (item) {

                ngProgress.start();
                var requestItem = convertAirticketViewModelToairticketmodel(item);
                var apiFunction = requestItem.AirTicketId == 0 ? ticketServiceApi.addItem : ticketServiceApi.saveItem;
                console.log(requestItem);
                apiFunction(requestItem, function (data) {
                    ngProgress.complete();
                    $.smallBox({
                        title: 'Билет успешно сохранен.',
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#296191",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                    $location.path('/Order/' + $scope.orderId + '/Services');

                }, function (error) {
                    ngProgress.complete();
                    $.smallBox({
                        title: 'Не удалось сохранить билет.',
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#FF0000",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                });
            }
        }
    ])

    //transfer service controller
    .controller('TransferServiceController', [
    '$scope', '$rootScope', 'transferServiceApi', 'ngProgress', '$location', function ($scope, $rootScope, transferServiceApi, ngProgress, $location) {
        $scope.transfer = { isRoundTransfer: false };

        function convertTransferViewModelToTransfermodel(transfer) {
            var result = {
                TransferId: transfer.transferId ? transfer.transferId : 0,
                IsRoundTransfer: transfer.isRoundTransfer,
                DepartureDate: transfer.departureDate,
                ReturnDate: transfer.returnDate ? transfer.returnDate : null,
                StartLocation: transfer.startLocation,
                EndLocation: transfer.endLocation,
                Number: transfer.number,
                Price: transfer.price,
                Comission: transfer.commission,
                OrderId: angular.copy($scope.orderId),
                CurrencyId: transfer.currency.CurrencyId
            };
            return result;
        }

        function convertTransferModelToTransferViewModel(transfer) {
            var result = {
                transferId: transfer.TransferId,
                isRoundTransfer: transfer.IsRoundTransfer,
                departureDate: transfer.DepartureDate,
                returnDate: transfer.ReturnDate,
                startLocation: transfer.StartLocation,
                endLocation: transfer.EndLocation,
                number: transfer.ServiceData.Number,
                price: transfer.ServiceData.Price,
                commission: transfer.ServiceData.Comission,
                OrderId: angular.copy($scope.orderId),
                currency: transfer.ServiceData.Currency,
            };
            return result;
        }

        if ($scope.serviceType == 'Transfer') {
            ngProgress.start();
            $rootScope.isLoading = true;
            transferServiceApi.getItems({ Id: $scope.serviceId }, function (data) {
                ngProgress.complete();
                $rootScope.isLoading = false;
                console.log(data);
                if (data.Items.length == 0 || !data.Items) {
                    $location.path('/Order/' + $scope.orderId + '/Services/');
                } else {
                    $scope.transfer = convertTransferModelToTransferViewModel(data.Items[0]);
                }
            }, function () {
                ngProgress.complete();
                $rootScope.isLoading = false;
                $location.path('/Order/' + $scope.orderId + '/Services/');
            });
        }

        $scope.save = function (item) {
            ngProgress.start();
            var requestItem = convertTransferViewModelToTransfermodel(item);
            var apiFunction = requestItem.TransferId == 0 ? transferServiceApi.addItem : transferServiceApi.saveItem;

            apiFunction(requestItem, function (data) {
                ngProgress.complete();
                $.smallBox({
                    title: 'Сервис "Перевозка" успешно сохранен.',
                    content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                    color: "#296191",
                    iconSmall: "fa fa-check bounce animated",
                    timeout: 3000
                });
                $location.path('/Order/' + $scope.orderId + '/Services');

            }, function (error) {
                ngProgress.complete();
                $.smallBox({
                    title: 'Не удалось сохранить сервис "Перевозка".',
                    content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                    color: "#FF0000",
                    iconSmall: "fa fa-check bounce animated",
                    timeout: 3000
                });
            });
        }
    }])

    //hotel service controller
    .controller('HotelServiceController', [
        '$scope', '$rootScope', 'hotelServiceApi', 'ngProgress', '$location',
        function ($scope, $rootScope, hotelServiceApi, ngProgress, $location) {
            $scope.hotel = {};

            $scope.GetExtendedFiters = function () {
                var extendedFilters = {};
                extendedFilters['filter[CountryId]'] = $scope.hotel.Country ? $scope.hotel.Country.CountryId : undefined;
                extendedFilters['filter[RegionId]'] = $scope.hotel.Region ? $scope.hotel.Region.RegionId : undefined;
                return extendedFilters;
            };

            function convertHotelServiceViewModelToHotelServicemodel(hotelService) {
                var result = {
                    hotelServiceId: hotelService.hotelServiceId ? hotelService.hotelServiceId : 0,
                    HotelId: hotelService.Hotel.HotelId,
                    Number: hotelService.number,
                    Price: hotelService.price,
                    Comission: hotelService.commission,
                    OrderId: angular.copy($scope.orderId),
                    CurrencyId: hotelService.currency.CurrencyId,
                    FeedType: hotelService.FeedTypeId,
                    RoomType: hotelService.RoomTypeId,
                    AdultCount: hotelService.adultCount,
                    ChildrenCount: hotelService.childrenCount
                };
                return result;
            }

            function convertHotelServiceModelToHotelServiceViewModel(hotelService) {
                var result = {
                    hotelServiceId: hotelService.HotelServiceId,
                    Hotel: hotelService.Hotel,
                    number: hotelService.ServiceData.Number,
                    price: hotelService.ServiceData.Price,
                    commission: hotelService.ServiceData.Comission,
                    OrderId: angular.copy($scope.orderId),
                    currency: hotelService.ServiceData.Currency,
                    FeedTypeId: hotelService.FeedType,
                    RoomTypeId: hotelService.RoomType,
                    adultCount: hotelService.AdultCount,
                    childrenCount: hotelService.ChildrenCount,
                    Region: { Name: hotelService.Hotel.RegionName, RegionId: hotelService.Hotel.RegionId },
                    Country: { Name: hotelService.Hotel.CountryName, CountryId: hotelService.Hotel.CountryId },
                };
                return result;
            }

            if ($scope.serviceType == 'Hotel') {
                ngProgress.start();
                $rootScope.isLoading = true;
                hotelServiceApi.getItems({ Id: $scope.serviceId }, function (data) {
                    ngProgress.complete();
                    $rootScope.isLoading = false;
                    if (data.Items.length == 0 || !data.Items) {
                        $location.path('/Order/' + $scope.orderId + '/Services/');
                    } else {
                        $scope.hotel = convertHotelServiceModelToHotelServiceViewModel(data.Items[0]);
                        console.log($scope.hotel);
                    }
                }, function () {
                    ngProgress.complete();
                    $rootScope.isLoading = false;
                    $location.path('/Order/' + $scope.orderId + '/Services/');
                });
            }

            $scope.save = function (item) {
                ngProgress.start();

                var requestItem = convertHotelServiceViewModelToHotelServicemodel(item);
                var apiFunction = requestItem.hotelServiceId == 0 ? hotelServiceApi.addItem : hotelServiceApi.saveItem;

                apiFunction(requestItem, function (data) {
                    ngProgress.complete();
                    $.smallBox({
                        title: 'Сервис "Отель" успешно сохранен.',
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#296191",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                    $location.path('/Order/' + $scope.orderId + '/Services');

                }, function (error) {
                    ngProgress.complete();
                    $.smallBox({
                        title: 'Не удалось сохранить сервис "Отель".',
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#296191",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                });
            }
        }
    ])

    //tour package service controller
    .controller('TourPackageServiceController', [
        '$scope', '$rootScope', 'ngProgress', 'tourPackageServiceApi', '$location',
        function ($scope, $rootScope, ngProgress, tourPackageServiceApi, $location) {
            $scope.tourPackage = {};

            $scope.GetExtendedFiters = function () {
                var extendedFilters = {};
                extendedFilters['filter[CountryId]'] = $scope.tourPackage.Country ? $scope.tourPackage.Country.CountryId : undefined;
                extendedFilters['filter[RegionId]'] = $scope.tourPackage.Region ? $scope.tourPackage.Region.RegionId : undefined;
                return extendedFilters;
            };

            function convertTourPackageViewModelToTourPackagemodel(tourPackage) {
                var result = {
                    TourPackageId: tourPackage.TourPackageId ? tourPackage.TourPackageId : 0,
                    ArrivalDate: tourPackage.arrivalDate,
                    DepartureDate: tourPackage.departureDate,
                    ReturnDate: tourPackage.returnDate,
                    HotelId: tourPackage.Hotel.HotelId,
                    DepartureAirportId: tourPackage.fromAirport.AirportId,
                    ReturnAirportId: tourPackage.toAirport.AirportId,
                    Price: tourPackage.price,
                    Comission: tourPackage.commission,
                    Number: tourPackage.number,
                    OrderId: angular.copy($scope.orderId),
                    CurrencyId: tourPackage.currency.CurrencyId,
                    FeedType: tourPackage.FeedTypeId,
                    RoomType: tourPackage.RoomTypeId,
                    AdultCount: tourPackage.adultCount,
                    ChildrenCount: tourPackage.childrenCount
                };
                return result;
            }

            function convertTourPackageModelToTourPackageViewModel(tourPackage) {
                var result = {
                    TourPackageId: tourPackage.TourPackageId,
                    arrivalDate: tourPackage.ArrivalDate,
                    departureDate: tourPackage.DepartureDate,
                    returnDate: tourPackage.ReturnDate,
                    Hotel: tourPackage.Hotel,
                    Region: { Name: tourPackage.Hotel.RegionName, RegionId: tourPackage.Hotel.RegionId },
                    Country: { Name: tourPackage.Hotel.CountryName, CountryId: tourPackage.Hotel.CountryId },
                    fromAirport: tourPackage.DepartureAirport,
                    toAirport: tourPackage.ReturnAirport,
                    price: tourPackage.ServiceData.Price,
                    commission: tourPackage.ServiceData.Comission,
                    number: tourPackage.ServiceData.Number,
                    OrderId: angular.copy($scope.orderId),
                    currency: tourPackage.ServiceData.Currency,
                    FeedTypeId: tourPackage.FeedType,
                    RoomTypeId: tourPackage.RoomType,
                    adultCount: tourPackage.AdultCount,
                    childrenCount: tourPackage.ChildrenCount
                }
                return result;
            }

            if ($scope.serviceType == 'Package') {
                ngProgress.start();
                $rootScope.isLoading = true;
                tourPackageServiceApi.getItems({ Id: $scope.serviceId }, function (data) {
                    ngProgress.complete();
                    $rootScope.isLoading = false;
                    if (data.Items.length == 0 || !data.Items) {
                        $location.path('/Order/' + $scope.orderId + '/Services/');
                    } else {
                        $scope.tourPackage = convertTourPackageModelToTourPackageViewModel(data.Items[0]);
                    }
                }, function () {
                    ngProgress.complete();
                    $rootScope.isLoading = false;
                    $location.path('/Order/' + $scope.orderId + '/Services/');
                });
            }

            $scope.save = function (item) {
                ngProgress.start();
                var requestItem = convertTourPackageViewModelToTourPackagemodel(item);
                var apiFunction = requestItem.TourPackageId == 0 ? tourPackageServiceApi.addItem : tourPackageServiceApi.saveItem;

                apiFunction(requestItem, function (data) {
                    ngProgress.complete();
                    $.smallBox({
                        title: 'Тур-пакет успешно сохранен.',
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#296191",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                    $location.path('/Order/' + $scope.orderId + '/Services');

                }, function (error) {
                    ngProgress.complete();
                    $.smallBox({
                        title: 'Не удалось сохранить тур-пакет.',
                        content: "<i class='fa fa-clock-o'></i> <i>" + 'Это окно автоматически закроется через 3 секунды.' + "</i>",
                        color: "#FF0000",
                        iconSmall: "fa fa-check bounce animated",
                        timeout: 3000
                    });
                });
            }
        }])

    //hotel modal dialog controller
    .controller('HotelModalDialogController', [
    '$scope', function ($scope) {

        $scope.GetExtendedFiters = function () {
            var extendedFilters = {};
            extendedFilters['filter[CountryId]'] = $scope.item.Country ? $scope.item.Country.CountryId : undefined;
            return extendedFilters;
        };

    }])


    // Path: /error/404
    .controller('Error404Controller', ['$scope', '$rootScope', '$location', '$window', 'authService', function ($scope, $rootScope, $location, $window, authService) {

        if (authService.authentication.isAuth == false) {
            $location.path('/Login');
        } else {
            $rootScope.isLoading = false;
            $scope.$root.title = 'Error 404: Page Not Found';
        }
    }]);


angular.module('app.demoControllers', [])
	.controller('ActivityDemoCtrl', ['$scope', function ($scope) {
	    var ctrl = this;
	    ctrl.getDate = function () {
	        return new Date().toUTCString();
	    };

	    $scope.refreshCallback = function (contentScope, done) {

	        // use contentScope to get access with activityContent directive's Control Scope
	        console.log(contentScope);

	        // for example getting your very long data ...........
	        setTimeout(function () {
	            done();
	        }, 3000);

	        $scope.footerContent = ctrl.getDate();
	    };

	    $scope.items = [
			{
			    title: 'Msgs',
			    count: 14,
			    src: 'ajax/notify/mail.html',
			    onload: function (item) {
			        console.log(item);
			        alert('[Callback] Loading Messages ...');
			    }
			},
			{
			    title: 'Notify',
			    count: 3,
			    src: 'ajax/notify/notifications.html'
			},
			{
			    title: 'Tasks',
			    count: 4,
			    src: 'ajax/notify/tasks.html',
			    //active: true
			}
	    ];

	    $scope.total = 0;
	    angular.forEach($scope.items, function (item) {
	        $scope.total += item.count;
	    })

	    $scope.footerContent = ctrl.getDate();

	}]);

