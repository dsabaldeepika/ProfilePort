﻿

(function () {

    angular.module('app').factory('homeService', ['$http', 'constantsService', 'toastr',
        function ($http,constantsService, toastr) {
           
                factory = {};
            var url= constantsService.serverPath
       
            factory.getUser = function (id) {

                return $http.get(url+'/home/'+ id)
                    .success(function (data, status, headers, config) {

                        return data.User;

                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        return null;
                    });
            };

            return factory;
        }]);


}());























