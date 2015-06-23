/**
 * Created by sabal.prasain on 12/20/2014
 */

(function () {

    angular.module('app').factory('notesService', ['$http', 'siteConfig', 'toastr',
        function ($http, siteConfig, toastr) {
            var serviceBase = siteConfig.apiHosts.lead,
                factory = {};

            //Save notes for a lead 
            factory.saveNotes = function (model) {

                return $http.post('/lead/agentnote', model)
                    .success(function (data, status, headers, config) {

                   
                        return true;
                    })
                    .error(function (data, status, headers, config) {
                       

                        return false;
                    });
            }


            // nnew service has to be implemented later and new endpoints has to be created 

            factory.getLeadApplication = function (leadId) {

                return $http.get('/lead/details/' + leadId)
                    .success(function (data, status, headers, config) {
                        // this callback will be called asynchronously
                        // when the response is available

                        //factory.firstName = data.borrower.firstName;
                        //factory.lastName = data.borrower.lastName;

                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        return null;
                    });
            };

            factory.getNotes = function (leadId) {

                return $http.get('/lead/agentnote/' + leadId)
                    .success(function (data, status, headers, config) {
                       
                        return data.notes;
                       
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























