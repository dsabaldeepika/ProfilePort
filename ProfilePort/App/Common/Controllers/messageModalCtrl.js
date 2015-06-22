/**
 * Created by devin.delapp on 1/8/2015.
 */

(function () {
    angular.module('app').controller('messageModalCtrl', ['$scope', '$modalInstance', 'config',
        function ($scope, $modalInstance, config) {

            $scope.model = {
                title: config.title,
                message: config.message,
                buttons: config.buttons
            };

            $scope.buttonClick = function (btn) {
                $modalInstance().close(btn.result);
            };
        }]
    );
}());