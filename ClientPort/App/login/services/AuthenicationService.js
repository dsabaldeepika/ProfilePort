

(function () {

    angular.module('app').factory('messageService', ['$http', 'toastr',
        function ($http, toastr) {

            var factory = {};
            factory.Login = Login;
            factory.SetCredentials = SetCredentials;
            factory.ClearCredentials = ClearCredentials;

           factory.Login= function(username, password, callback) {

                /* Dummy authentication for testing, uses $timeout to simulate api call
                 ----------------------------------------------*/
                $timeout(function () {
                    var response;
                    UserService.GetByUsername(username)
                        .then(function (user) {
                            if (user !== null && user.password === password) {
                                response = { success: true };
                            } else {
                                response = { success: false, message: 'Username or password is incorrect' };
                            }
                            callback(response);
                        });
                }, 1000);

                /* Use this for real authentication
                 ----------------------------------------------*/
                //$http.post('/api/authenticate', { username: username, password: password })
                //    .success(function (response) {
                //        callback(response);
                //    });

            }

           factory.SetCredentials= function(username, password) {
                var authdata = Base64.encode(username + ':' + password);

                $rootScope.globals = {
                    currentUser: {
                        username: username,
                        authdata: authdata
                    }
                };

                $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata; // jshint ignore:line
                $cookieStore.put('globals', $rootScope.globals);
            }

           factory.ClearCredentials= function() {
                $rootScope.globals = {};
                $cookieStore.remove('globals');
                $http.defaults.headers.common.Authorization = 'Basic ';
            }


            // return the factory.
            return factory;
        }]);


}());























