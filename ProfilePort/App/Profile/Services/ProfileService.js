(function () {

    angular.module('app').factory('profileService', ['$http', 'toastr', '$log',
        function ($http, toastr, $log) {
            factory = {};

            // This pulls in the content for the loan queue.    
            factory.getprofile = function (userId) {

                return $http.get('/profile/' + userId)
                    .success(function (data, status, headers, config) {

                        return data;


                    })
                    .error(function (data, status, headers, config) {

                        return [];


                    });
            };

            // save the profile record
            factory.saveprofile = function (userId, Profile) {
                
                return $http.post('/profile/upsert', request)

                    .success(function (data, status, headers, config) {
                   
                        return data;
                    })
                    .error(function (data, status, headers, config) {

                        return data;
                    });
            }

            // return the factory.
            return factory;
        }]);


}());