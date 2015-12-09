
(function () {

    angular.module('app').directive('discardChangesConfirmation', ['$modal', '$location', function ($modal, $location) {

        return {
            restrict: 'A',
            // require: 'ngChange',
            link: function (scope, element, attrs) {
                element.bind('focus', function (e) {

                    var modal = $modal.open({
                        templateUrl: '/app/common/partials/messageModal.html',
                        controller: 'messageModalCtrl',
                        backdrop: 'static',
                        size: 'sm',
                        resolve: {
                            $modalInstance: function () { return function () { return modal; } },
                            config: function () {
                                return {
                                    title: "Discard changes ",
                                    message: "All changes in progress will be discarded, continue?",
                                    buttons: [
                                        { text: "Yes", style: "btn-danger", result: true },
                                        { text: "No", style: "btn-primary", result: false },
                                        { text: "Cancel", style: "btn-warning", result: "" }
                                    ]
                                }
                            }
                        }
                    });

                    modal.result.then(function (result) {

                        if (result) {
                            $location.path(attrs.action);
                        }

                    }, function () { });

                });
            }
        };
    }]);
}());