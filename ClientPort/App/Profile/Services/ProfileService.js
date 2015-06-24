(function () {

    angular.module('app').factory('profileService', ['$http', 'toastr', '$log',
        function ($http, toastr, $log) {
            factory = {};

            var urlBase = '/api/profile';
            //var urlBase= 'https://cashcall.firebaseio.com';
            // This pulls in the content for the loan queue.    
            factory.getprofile = function (userId) {

                return $http.get(urlBase +'/'+ userId )
                    .success(function (data, status, headers, config) {

                        return data;

                    })
                    .error(function (data, status, headers, config) {

                        return [];


                    });
            };

            // save the profile record
factory.postprofile = function (Userid, profile) {
    var data ={ Userid:Userid,profile:profile};

    return $http.post(urlBase, data)

                    .success(function (data, status, headers, config) {
                       
                        return data;
                    })
                    .error(function (data, status, headers, config) {

                        return data;
                    })
            }

    // update the profile record
factory.putprofile = function (userId, Profile) {

        return $http.put(urlBase + '/PostProfile/' + userId, Profile)

            .success(function (data, status, headers, config) {

                return data;
            })
            .error(function (data, status, headers, config) {

                return data;
            });
    }

            // update the profile record
    factory.deleteprofile = function (Id) {

        return $http.post(urlBase +'/DeleteProfile/'+Id)

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