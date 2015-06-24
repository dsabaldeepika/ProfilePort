
(function () {

    angular.module('app').factory('notesService', ['$http',  'toastr',
        function ($http,  toastr) {
          
                factory = {};

            //Save notes for a notes 
            factory.getNotes = function (model) {

                return $http.get('/notes', model)
                    .success(function (data, status, headers, config) {

                   
                        return true;
                    })
                    .error(function (data, status, headers, config) {
                       

                        return false;
                    });
            }


           
            return factory;
        }]);


}());




















