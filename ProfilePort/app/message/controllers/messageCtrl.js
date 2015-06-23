
(function () {

    angular.module('app').controller('messageCtrl', ['$scope', '$routeParams', '$controller', 'messageService', 'toastr', 'leadService', '$modal', '$location', function ($scope, $routeParams, menuService, $controller, messagesService, toastr, leadService, $modal, $location) {
        $scope.model = {};

       
        $scope.dashboardId = "2";
        $scope.model.getmessages = function () {

            messagesService.getmessages().then(function (result) {
                $scope.model.messages = result.data.messages;
                $scope.originalModel = _.cloneDeep($scope.model);
            })
        };

        $scope.model.getmessages();

        $scope.model.postMessages = function () {

            messagesService.essages($scope.model).then(function (response) {

                $scope.model.messages = "";
                $scope.originalModel = _.cloneDeep($scope.model);
          
            });

        };
        $scope.originalModel = _.cloneDeep($scope.model);


    }]);
}());
