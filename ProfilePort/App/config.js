
(function () {
    // configure the routing for the site.
    angular.module('app').config(['$routeProvider', '$locationProvider', '$httpProvider', '$logProvider',
        function ($routeProvider, $locationProvider, $httpProvider, $logProvider) {

            $logProvider.debugEnabled(true);
            $locationProvider.html5Mode(false);

            // Routing structure
            $routeProvider
                .when('/404', { templateUrl: 'App/Common/Partials/404.html', controller: '404Ctrl' })
                 .when('/app/', { templateUrl: 'index', controller: 'app' })
                .when('/dashboard', { templateUrl: 'App/Dashboard/Partials/dashboard.html', controller: 'DashboardCtrl' })
                .when('/profile/:id?', { templateUrl: 'App/Profile/Partials/Profile.html', controller: 'ProfileCtrl'})
                 .when('/message/:id?', { templateUrl: 'App/Message/Partials/Message.html', controller: 'ProfileCtrl' })


                // reports
                .when('/', { redirectTo: 'app' })
                .otherwise({ redirectTo: '404' });

            // Http Settings
            $httpProvider.defaults.withCredentials = true;

            // alternatively, register the interceptor via an anonymous factory
            $httpProvider.interceptors.push(['$location', '$q', '$rootScope', function ($location, $q, $rootScope) {
                return {
                    'request': function (config) {

                        return config;
                    },

                    'response': function (response) {
                        return response;
                    },
                    'responseError': function (response) {
                        // request was unauthenticated, redirect away.
                        if (response.status === 401) {

                            if ($location.$$path.indexOf("/login") === 0) {
                                // do nothing when you're on the login page, without this line the site will lockup
                            }
                            else {
                                var expectedUrl = encodeURIComponent(window.location.hash.replace("#", ""));
                                $location.path('/login/' + expectedUrl);
                            }
                            return $q.reject(response);
                        }
                        $rootScope.$broadcast('httpResponseError', { response: response });

                        return $q.reject(response);
                    }
                };
            }]);


        }]);
}());