





(function () {

    angular.module('app').controller('notesCtrl', ['$scope', 'constantsService', '$routeParams', '$controller', 'notesService', 'toastr', '$modal', '$location', 'notesResources', function ($scope, constantsService, $routeParams, $controller, notessService, toastr, $modal, $location, notesResources) {


        $scope.model = {};
        $scope.originalModel = _.cloneDeep($scope.model);
        $scope.dashboardId = "2";
        //$scope.getnotes = function () {

        //    notessService.getNotes().then(function (result) {
        //        $scope.model.notess = result.data;
        //        $scope.originalModel = _.cloneDeep($scope.model);
        //    })
        //};

        var dashboardId = 2;

        //notesResources.query(function (response) {

        //    $scope.notes = response;
        //})


        notessService.getNotes(dashboardId).then(function (response) {

            $scope.model.notes = "";
            $scope.originalModel = _.cloneDeep($scope.model);

        });

        $scope.postNotes = function () {

            notessService.postNotes($scope.note).then(function (response) {

                $scope.notes = "";
                $scope.originalModel = _.cloneDeep($scope.model);

            });

        };
        $scope.originalModel = _.cloneDeep($scope.model);


    }]);
}());


