

(function () {

    angular.module('app').factory('homeService', ['$http', 'constantsService', 'toastr',
        function ($http,constantsService, toastr) {
           
                factory = {};
            var url= constantsService.serverPath
       
            factory.getUser = function (userId) {

                return $http.get(url+'/home/'+ userId)
                    .success(function (data, status, headers, config) {

                        return data;

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























