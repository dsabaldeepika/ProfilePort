
(function () {

    angular.module('app').factory('dealerportalService', ['$http', '$location', 
        function ($http, $location) {
          
            factory = {};

            factory.deleteItem = function () {
                var id = 2;
                return $http.get('api/VehicleMakeModel/FindVehicleMakeModelClassViewModelAsync' + id)
             
                    .success(function (data, status, headers, config) {
                        console.log(JSON.stringify(data))
                        console.log(status)

                        return data;

                    })
                    .error(function (data, status, headers, config) {
                        console.log(status)
                        return null;
                    });
            };

          

            // return the factory.
            return factory;
        }]);

}());