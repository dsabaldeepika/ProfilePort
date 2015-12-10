(function () {
    'use strict';
    var app = angular.module('eliteApp', [
    //'ngRoute',
        //'ngSanitize',

        // 3rd Party Modules
        'ui.bootstrap',
        'ui.router',
        //'ui.calendar',
        //'uiGmapgoogle-maps',
        //'ui.ace',
        //'ui.grid'
        //'ui.grid.importer',
        //'ui.grid.edit',
        //'ui.grid.resizeColumns',
        //'ui.select',
        //'ui.alias'
    ]);


    app.config(['$stateProvider', '$urlRouterProvider', configRoutes]);

    function configRoutes($stateProvider, $urlRouterProvider) {

        

        $stateProvider
            .state('app', {
                url: '/league/:leagueId',
                templateUrl: 'app-user/layout/shell.html',
                controller: 'ShellCtrl',
                controllerAs: 'vm'
            })
            .state('app.home', {
                url: '/',
                views: {
                    'appContent': {
                        templateUrl: 'app-user/home/home.html',
                        controller: 'HomeCtrl',
                        controllerAs: 'vm',
                        resolve: {
                            initialData: ['$stateParams', 'eliteApi', function ($stateParams, eliteApi) {
                                return eliteApi.getAllLeagueData($stateParams.leagueId);
                            }]
                        }
                    }
                }
            })
            .state('app.teams', {
                url: '/teams',
                views: {
                    'appContent': {
                        templateUrl: 'app-user/teams/teams.html',
                        controller: 'TeamsCtrl',
                        controllerAs: 'vm',
                        resolve: {
                            initialData: ['$stateParams', 'eliteApi', function ($stateParams, eliteApi) {
                                return eliteApi.getAllLeagueData($stateParams.leagueId);
                            }]
                        }
                    }
                }
            })
            .state('app.team-schedule', {
                url: '/team-schedule/:id',
                views: {
                    'appContent': {
                        templateUrl: 'app-user/teams/team-schedule.html',
                        controller: 'TeamScheduleCtrl',
                        controllerAs: 'vm',
                        resolve: {
                            initialData: ['$stateParams', 'eliteApi', function ($stateParams, eliteApi) {
                                return eliteApi.getAllLeagueData($stateParams.leagueId);
                            }]
                        }
                    }
                }
            })
            .state('app.locations', {
                url: '/locations',
                views: {
                    'appContent': {
                        templateUrl: 'app-user/locations/locations.html',
                        controller: 'LocationsCtrl',
                        controllerAs: 'vm',
                        resolve: {
                            initialData: ['$stateParams', 'eliteApi', function ($stateParams, eliteApi) {
                                return eliteApi.getAllLeagueData($stateParams.leagueId);
                            }]
                        }
                    }
                }
            })
            .state('app.location-map', {
                url: '/location-map/:id',
                views: {
                    'appContent': {
                        templateUrl: 'app-user/locations/location-map.html',
                        controller: 'LocationMapCtrl',
                        controllerAs: 'vm',
                        resolve: {
                            initialData: ['$stateParams', 'eliteApi', function ($stateParams, eliteApi) {
                                return eliteApi.getAllLeagueData($stateParams.leagueId);
                            }],
                            maps: ['uiGmapGoogleMapApi', function(uiGmapGoogleMapApi){
                                return uiGmapGoogleMapApi;
                            }]
                        }
                    }
                }
            });


        $urlRouterProvider.otherwise('/');
    }

    app.run(['$state', function ($state) {
        // Include $route to kick start the router.
    }]);
})();
