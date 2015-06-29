

(function () {

    angular.module('app').factory('notesService', ['$http', 'toastr',
        function ($http, toastr) {

            var serviceBase = constantsService.serverPath,
                factory = {};

            //Save notes for a user
            factory.saveNotes = function (note) {

                notes = { title: "title", noteContent: note, dashboardId: "2" };
                return $http.post(url+'/notes', notes)
                    .success(function (data, status, headers, config) {


                        return true;
                    })
                    .error(function (data, status, headers, config) {


                        return false;
                    });
            }


            factory.getNotes = function (leadId) {

                return $http.get('/lead/agentnote/' + leadId)
                    .success(function (data, status, headers, config) {

                        return data.notes;

                    })
                    .error(function (data, status, headers, config) {
                       
                        return null;
                    });
            };

            // return the factory.
            return factory;
        }]);

}());












































