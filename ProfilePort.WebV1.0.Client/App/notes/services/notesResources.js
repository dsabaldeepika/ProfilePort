
(function () {

    angular.module('app').factory('notesResources', ['$resource', 'constantsService', 'toastr',
        function ($resource, constantsService, toastr) {


            var serviceBase = constantsService.serverPath;
            return $resource(serviceBase + "api/Notes/:id /:dashboardId",
                {
                    id:'@id'

                },
                {
                    dashboardId: '@dashboardId'
                });

        }]);


}());



















