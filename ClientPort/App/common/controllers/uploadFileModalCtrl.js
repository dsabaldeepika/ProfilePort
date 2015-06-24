
(function () {
    angular.module('app').controller('uploadFileModalCtrl', ['$scope', '$modalInstance', 'config',
        function ($scope, $modalInstance, config) {

            $scope.title = config.title || "Upload File";
            $scope.message = config.message;
            $scope.closeText = config.closeText || "Close";

            $scope.fileConfig = {

                saveUrl: config.saveUrl,
                removeUrl: config.removeUrl,
                autoUpload: config.autoUpload || true,
                select: config.onSelect || function (e) { },
                complete: config.onComplete || function (e) { },
                error: config.onError || function (e) { return e; },
                success: config.onSuccess || function (e) { return e; }
            };

            $scope.cancel = function () {
                $modalInstance().dismiss('cancel');
            };
        }]
    );
}());