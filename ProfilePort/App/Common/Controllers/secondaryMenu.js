
(function () {

    angular.module('app').controller('coSecondaryMenuCtrl', ['$scope', 'menuService',
        function ($scope, menuService) {
            $scope.secondaryMenu = {
                menu: []
            };
            // send the secondary scope to the menu service.
            menuService.secondaryMenuScope = $scope;
        }
    ]);

}());