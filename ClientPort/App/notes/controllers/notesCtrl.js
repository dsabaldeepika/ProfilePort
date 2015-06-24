





(function () {

    angular.module('app').controller('notesCtrl', ['$scope', 'constantsService', '$routeParams', '$controller', 'notesService', 'toastr', '$modal', '$location', 'notesResources', function ($scope, constantsService, $routeParams, $controller, notessService, toastr, $modal, $location, notesResources) {


        $scope.model = {};

        $scope.dashboardId = "2";
        //$scope.getnotes = function () {

        //    notessService.getNotes().then(function (result) {
        //        $scope.model.notess = result.data;
        //        $scope.originalModel = _.cloneDeep($scope.model);
        //    })
        //};
        
            var dashboardId = 2;    

            notesResources.query({ dashboardId: '@dashboardId' }, function (response) {

                $scope.notes = response;
            })

                $scope.originalModel = _.cloneDeep($scope.model);

                $scope.saveNotes = function () {

                    notesResources.save(function (data) {
                        $scope.notes = data;
                    })

                    $scope.originalModel = _.cloneDeep($scope.model);

                }



      

        $scope.postNotes = function () {

            notessService.postnotess($scope.model).then(function (response) {

                $scope.model.notess = "";
                $scope.originalModel = _.cloneDeep($scope.model);

            });

        };
        $scope.originalModel = _.cloneDeep($scope.model);


    }]);
}());


