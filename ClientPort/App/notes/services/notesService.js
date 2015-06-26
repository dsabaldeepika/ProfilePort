
(function () {

    angular.module('app').factory('notesService', ['$http',  'toastr',
        function ($http,  toastr) {
          
                factory = {};

                var URL = "https://moviez.firebaseio.com/";
            //Save notes for a notes 
            factory.getNotes = function () {

                return $http.get(URL)
                    .success(function (data, status, headers, config) {

                   
                        return true;
                    })
                    .error(function (data, status, headers, config) {
                       

                        return false;
                    });
            }
        

            //Save notes for a notes 
            factory.postNotes = function (note) {

                return $http.post(URL+note+'.json')
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




















