(function () {
    angular.module('app').controller('home', ['$scope', '$routeParams', '$modal', 'homeService', '$location', 'toastr',
    function ($scope, $routeParams, $modal, homeService, $location, toastr) {

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

        //homeService.getUser(2).then(function (result) {
        //    $scope.model.user = result.data;
        //});

        $scope.displayModal = function (messages) {

            var modal = $modal.open({
                templateUrl: 'app/common/partials/errorModal.html',
                controller: 'coErrorModalCtrl',
                size: 'md',
                resolve: {
                    config: function () {
                        return {
                            title: 'Save User Error',
                            description: 'Please correct the following issues before saving:',
                            messages: messages,


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

        $scope.saveUser = function (andClose) {

            homeService.postUser($scope.UserId, $scope.model.user).then(function (response) {

                if (response.data.resultCode !== 0) {
                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save User Error',
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
                toastr.success("The User has been saved.", "User Saved");

                if (andClose) {
                    $location.path('/User/');
                }
                else if ($scope.UserId == "") {
                    $location.path('/User/');
                }

            });

        };

        $scope.putUser = function (andClose) {

            homeService.putUser($scope.UserId, $scope.model.user).then(function (response) {

                if (response.data.resultCode !== 0) {

                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save User Error',
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
                toastr.success("The User has been updated.", "User Updated");

                if (andClose) {
                    $location.path('/User/');
                }
                else if ($scope.UserId == "") {
                    $location.path('/User/');
                }

            });

        };

        $scope.deleteUser = function (andClose) {

            homeService.deleteUser(2).then(function (response) {

                if (response.data.resultCode !== 0) {

                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save User Error',
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

                toastr.success("The User has been deleted.", "User deleted");

                if (andClose) {
                    $location.path('/User/');
                }
                else if ($scope.UserId == "") {
                    $location.path('/User/');
                }

            });

        };
    }]);
}());