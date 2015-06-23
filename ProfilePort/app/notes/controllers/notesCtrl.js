

(function () {

    angular.module('app').controller('notesCtrl', ['$scope', '$routeParams','$controller', 'notesService', 'toastr', , '$modal', '$location', function ($scope, $routeParams, $controller, notesService, toastr, $modal, $location) {
        $scope.model = {};

        $scope.model.getNotes = function () {

            notesService.getNotes($scope.model.leadId).then(function (result) {
                $scope.notes = result.data.notes;
                $scope.originalModel = _.cloneDeep($scope.model);
            })
        };

        $scope.model.saveNotes = function () {

            notesService.saveNotes($scope.model).then(function (response) {

                if (response.data.resultCode === 0 && response.statusText === "OK") {
                    toastr.success("The Note has been saved.");
                    $scope.model.getNotes();
                    $scope.model.note = "";
                    $scope.originalModel = _.cloneDeep($scope.model);
                }

                else {
                    var modal = $modal.open({
                        templateUrl: '/app/common/partials/messageModal.html',
                        controller: 'messageModalCtrl',
                        backdrop: 'static',
                        size: 'sm',
                        resolve: {
                            $modalInstance: function () { return function () { return modal; } },
                            config: function () {
                                return {

                                    title: "Lead Notes Save Error",
                                    message: response.data.description,
                                    buttons: [
               { text: "Ok", style: "btn-primary", result: true }

                                    ]
                                }
                            }
                        }
                    });

                }
            });

        };
        $scope.originalModel = _.cloneDeep($scope.model);

        $scope.model.confirm = function () {

            if (!_.isEqual($scope.model, $scope.originalModel)) {

                var modal = $modal.open({
                    templateUrl: '/app/common/partials/messageModal.html',
                    controller: 'messageModalCtrl',
                    backdrop: 'static',
                    size: 'sm',
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        config: function () {
                            return {

                                title: "Save Changes",
                                message: "Do you want to save your changes before you leave?",
                                buttons: [
                                    { text: "Yes", style: "btn-primary", result: true },
                                    { text: "No", style: "btn-danger", result: false },
                                    { text: "Cancel", style: "btn-warning", result: "" }
                                ]
                            }
                        }
                    }
                });

                modal.result.then(function (result) {
                    // if they want to save before leaving...
                    if (result === true) {
                        // save the changes and close form.
                        $scope.model.saveNotes();
                        $location.path($scope.redirectPath);
                    }
                    else if (result === false) {
                        // close form by directing away away.
                        $location.path($scope.redirectPath);
                    }
                });
            }
            else {
                // close the form by directing away.
                $location.path($scope.redirectPath);
            }
        };


    }]);
}());
