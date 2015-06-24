
(function () {

    angular.module('app').controller('messageCtrl', ['$scope','constantsService', '$routeParams', '$controller', 'messageService', 'toastr',  '$modal', '$location', function ($scope,constantsService, $routeParams, $controller, messagesService, toastr, $modal, $location) {

       
        $scope.model = {};

        $scope.dashboardId = "2";
        $scope.getMessage = function () {

            messagesService.getMessage().then(function (result) {
                $scope.model.messages = result.data;
                $scope.originalModel = _.cloneDeep($scope.model);
            })
        };

        $scope.getMessage();

        $scope.postMessage = function () {

            messagesService.postMessages($scope.model).then(function (response) {

                $scope.model.messages = "";
                $scope.originalModel = _.cloneDeep($scope.model);
          
            });

        };
        $scope.originalModel = _.cloneDeep($scope.model);


    }]);
}());
