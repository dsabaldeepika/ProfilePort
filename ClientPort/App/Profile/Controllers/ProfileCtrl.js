(function () {
    angular.module('app').controller('profileCtrl', ['$scope', '$routeParams', '$modal', 'profileService', '$location', 'toastr',
    function ($scope, $routeParams, $modal, profileService,  $location,  toastr ) {
        $scope.model = {};
        $scope.datePickers = {};
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
            showWeeks: false
        };

        $scope.openDatePicker = function ($event, pickerName) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datePickers[pickerName] = true;
        };

        $scope.UserId = "Zango;"

        //profileService.getprofile(2).then(function (result) {
        //    $scope.model.user = result.data;
        //});

        $scope.showSpinner = false;
        $scope.spinnerMessage = 'Retrieving data...';

        $scope.spinnerOptions = {
            radius: 40,
            lines: 8,
            length: 0,
            width: 30,
            speed: 1.7,
            corners: 1.0,
            trail: 100,
            color: '#428bca'
        };

        $scope.saveprofile = function (andClose) {

            profileService.postprofile($scope.UserId, $scope.model.user).then(function (response) {

                if (response.data.resultCode !== 0) {
                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save profile Error',
                                    errors: response.data.description ? response.data.description : "",
                                    messages: response.data.message ? response.data.message : ""
                                };
                            },
                            $modalInstance: function () {
                                return function () {
                                    return modal;
                                }
                            }
                        }
                    });

                    return;
                }
                toastr.success("The profile has been saved.", "profile Saved");

                if (andClose) {
                    $location.path('/profile/');
                }
                else if ($scope.profileId == "") {
                    $location.path('/profile/');
                }

            });

        };

        $scope.putprofile = function (andClose) {

            profileService.putprofile($scope.UserId, $scope.model.user).then(function (response) {

                if (response.data.resultCode !== 0) {

                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save profile Error',
                                    errors: response.data.description ? response.data.description : "",
                                    messages: response.data.message ? response.data.message : ""
                                };
                            },
                            $modalInstance: function () {
                                return function () {
                                    return modal;
                                }
                            }
                        }
                    });

                    return;
                }
                toastr.success("The profile has been updated.", "profile Updated");

                if (andClose) {
                    $location.path('/profile/');
                }
                else if ($scope.profileId == "") {
                    $location.path('/profile/');
                }

            });

        };

        $scope.deleteprofile = function (andClose) {

            profileService.deleteprofile(2).then(function (response) {

                if (response.data.resultCode !== 0) {

                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save profile Error',
                                    errors: response.data.description ? response.data.description : "",
                                    messages: response.data.message ? response.data.message : ""
                                };
                            },
                            $modalInstance: function () {
                                return function () {
                                    return modal;
                                }
                            }
                        }
                    });

                    return;
                }

                toastr.success("The profile has been deleted.", "profile deleted");

                if (andClose) {
                    $location.path('/profile/');
                }
                else if ($scope.profileId == "") {
                    $location.path('/profile/');
                }

            });

        };
    }]);
}());