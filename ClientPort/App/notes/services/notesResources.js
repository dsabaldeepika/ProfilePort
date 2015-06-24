
(function () {

    angular.module('app').factory('notesResources', ['$resource', 'constantsService', 'toastr',
        function ($resource, constantsService, toastr) {


            var serviceBase = constantsService.serverPath;
            return $resource(serviceBase + "api/Notes/:id",

                {
                    id:'@id'

                },
                {
                    dashboardId: '@dashboardId'
                },


                {
                    update:
                        {

                            method: 'PUT'

                        }
                });

        }]);


}());



















