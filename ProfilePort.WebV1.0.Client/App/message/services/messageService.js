

(function () {

    angular.module('app').factory('messageService', ['$http',  'toastr',
        function ($http, toastr) {
           
              var  factory = {};
            var URL = 'https://cashcall.firebaseio.com/';
            //var URL = 'api/message/';

            //Save message for a lead 
            factory.postMessage = function (model) {

                return $http.post(URL + ".json", model)
                    .success(function (data, status, headers, config) {
                        return true;
                    })
                    .error(function (data, status, headers, config) {
                        return false;
                    });
            }

            factory.getMessage = function (model) {

                return $http.get(URL + ".json", model)
                    .success(function (data, status, headers, config) {


                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        return null;
                    });
            };

            factory.putMessage = function (leadId) {

                return $http.put(URL + ".json", model)
                    .success(function (data, status, headers, config) {


                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        return null;
                    });
            };

            factory.deleteMessage = function (messageId) {

                return $http.post(URL + ".json", messageId)
                    .success(function (data, status, headers, config) {


                        return data;
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























