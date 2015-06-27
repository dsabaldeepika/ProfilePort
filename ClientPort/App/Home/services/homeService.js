

(function () {

    angular.module('app').factory('homeService', ['$http',  'toastr',
        function ($http, toastr) {
           
                factory = {};

            //Save User for a lead 
            factory.saveUser = function (model) {

                return $http.post('/home', model)
                    .success(function (data, status, headers, config) {

                        return true;
                    })
                    .error(function (data, status, headers, config) {


                        return false;
                    });
            }

            factory.getUser = function () {

                return $http.get("http://localhost:53977/api/home/"+'8')
                    .success(function (data, status, headers, config) {

                        return data.User;

                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        return null;
                    });
            };


            // return the factory.
            return factory;
        }]);


}());























