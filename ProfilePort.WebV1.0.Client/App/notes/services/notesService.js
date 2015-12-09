

(function () {

    angular.module('app').factory('notesService', ['$http','$location', 'toastr',
        function ($http,$location, toastr) {
            var url = 'https://profileport.firebaseio.com';
            //  var url = constantsService.serverPath,
                factory = {};

            //Save notes for a user
            factory.saveNotes = function (note) {

                notes = { title: "title", noteContent: note, dashboardId: "2" };
                return $http.post(url+'/notes'+'/.json', notes)
                    .success(function (data, status, headers, config) {
                        $location.path("/note");

                        return true;
                    })
                    .error(function (data, status, headers, config) {

                        return false;
                    });
            }

            factory.getNotes = function (id) {

                // return $http.get(url + '/notes' +id+ '/json')
                return $http.get(url + '/notes/.json' )
                    .success(function (data, status, headers, config) {
                       // console.log(JSON.stringify(data))
                        return data;

                    })
                    .error(function (data, status, headers, config) {
                       
                        return null;
                    });
            };

            factory.deleteNote = function (key) {
          
                return $http.delete(url + '/notes/' + key+ '.json')
                
                    .success(function (data, status, headers, config) {
                        console.log(headers);
                        return true;

                    })
                    .error(function (data, status, headers, config) {

                        return null;
                    });
            };

            // return the factory.
            return factory;
        }]);

}());












































