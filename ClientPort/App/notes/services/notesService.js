
(function () {

    angular.module('app').factory('notesService', ['$http', 'constantsService', 'toastr',
        function ($http,constantsService,  toastr) {
            var url = constantsService.serverPath
                factory = {};

            //Save notes for a notes 
                factory.getNotes = function (id) {

                    return $http.get(url+'/notes')
                    .success(function (data, status, headers, config) {

                   
                        return true;
                    })
                    .error(function (data, status, headers, config) {
                       

                        return false;
                    });
            }
        

            //Save notes for a notes 
            factory.postNotes = function (note) {
                notes = { title: "title", noteContent: note,dashboardId :"2"};
                return $http.post(url+'/notes', notes)
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




















