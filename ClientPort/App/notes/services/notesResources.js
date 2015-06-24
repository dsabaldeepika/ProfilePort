﻿
(function () {

    angular.module('app').factory('notesResources', ['$resource', 'constantsService', 'toastr',
        function ($resource, constantsService, toastr) {


            var serviceBase = constantsService.serverPath;
            return $resource(serviceBase + "api/Notes/:id" ,
                {
                    dashboardId: '@_dashboardId'
                },


                {
                    update:
                        {

                            method: 'PUT'

                        }
                });

        }]);


}());



















