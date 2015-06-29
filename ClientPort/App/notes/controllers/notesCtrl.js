
(function () {

    angular.module('app').controller('notesCtrl', ['$scope', '$routeParams',  '$controller', 'notesService', 'toastr', '$modal', '$location', function ($scope, $routeParams, $controller, notesService, toastr, $modal, $location) {
      
        $scope.model = {};
        $scope.redirectPath = 'profile/'
        //$scope.model.UserId = $routeParams.id;

        $scope.model.getNotes = function () {

            notesService.getNotes().then(function (result) {
                $scope.notes = result.data;
                $scope.originalModel = _.cloneDeep($scope.model);
            })
        };

        $scope.model.getNotes();

        $scope.model.saveNotes = function () {

            if ($scope.model.note) {

                notesService.saveNotes($scope.model).then(function (response) {

                    if (response.status === 200 && response.statusText === "OK") {
                        $scope.model.getNotes();
                        toastr.success("The Note has been saved.");

                        $scope.model.note = "";
                        $scope.originalModel = _.cloneDeep($scope.model);
                    }

                    else {
                        $scope.messagemodal = 'Your note could not be saved';
                        $('#myModal').modal('show');
                    };

                });
            }
            else {
                $scope.messagemodal = 'Please enter the note and hit save';
                $('#myModal').modal('show');
                return;
            }
           
        };

        $scope.model.deleteNote= function (key) {

            notesService.deleteNote(key).then(function (response) {

                if (response.status === 200 && response.statusText === "OK") {
                    toastr.success("The Note has been deleted.");
                    $scope.model.getNotes();
                   
                }

                else {
                    $('#myModal').modal('show');
                };

            }
            );

        };


     

    }]);
}());





// latest angular seems to have an issue
//$scope.model.confirm = function () {

//    if (!_.isEqual($scope.model, $scope.originalModel)) {



//        var modal = $modal.open({
//            templateUrl: '/app/common/partials/messageModal.html',
//            controller: 'messageModalCtrl',
//            backdrop: 'static',
//            size: 'sm',
//            resolve: {
//                $modalInstance: function () { return function () { return modal; } },
//                config: function () {
//                    return {

//                        title: "Save Changes",
//                        message: "Do you want to save your changes before you leave?",
//                        buttons: [
//                            { text: "Yes", style: "btn-primary", result: true },
//                            { text: "No", style: "btn-danger", result: false },
//                            { text: "Cancel", style: "btn-warning", result: "" }
//                        ]
//                    }
//                }
//            }
//        });

//        modal.result.then(function (result) {
//            // if they want to save before leaving...
//            if (result === true) {
//                // save the changes and close form.
//                $scope.model.saveNotes();
//                $location.path($scope.redirectPath);
//            }
//            else if (result === false) {
//                // close form by directing away away.
//                $location.path($scope.redirectPath);
//            }
//        });
//    }
//    else {
//        // close the form by directing away.
//        $location.path($scope.redirectPath);
//    }
//};
