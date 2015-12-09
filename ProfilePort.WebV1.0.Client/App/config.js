
(function () {

    // configure the routing for the site.
    angular.module('app').config(['$routeProvider', '$locationProvider', '$httpProvider', '$logProvider',
        function ($routeProvider, $locationProvider, $httpProvider, $logProvider) {

            $logProvider.debugEnabled(true);

            $locationProvider.html5Mode(false);

            // Routing structure.
            $routeProvider
                .when('/home/:id?', { templateUrl: 'app/home/partials/home.html', controller: 'home' })
                .when('/profile/:id?', { templateUrl: 'app/profile/partials/profile.html', controller: 'profileCtrl' })
                .when('/404', { templateUrl: 'app/common/partials/404.html', controller: '404Ctrl' })
                .when('/note/:id?', { templateUrl: 'app/notes/partials/notes.html', controller: 'notesCtrl' })
                .when('/message/:id?', { templateUrl: 'app/message/partials/message.html', controller: 'messageCtrl' })
                .when('/contact', { templateUrl: 'app/contact/partials/contact.html', controller: 'contactCtrl' })
                 .when('/test', { templateUrl: 'app/common/partials/test.html', controller: 'coTestCtrl' })
                .when('/education/:id?', { templateUrl: 'app/education/partials/education.html', controller: 'educationCtrl' })
                .when('/', { redirectTo: 'home' })

                // otherwise redirect to the 404 error page
                .otherwise({ redirectTo: 'profile' });

            // Http Settings
          //  $httpProvider.defaults.withCredentials = true;

            // alternatively, register the interceptor via an anonymous factoryjust for future use
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