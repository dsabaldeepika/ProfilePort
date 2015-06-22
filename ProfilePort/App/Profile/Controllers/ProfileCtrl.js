(function () {
    angular.module('app').controller('ProfileCtrl', ['$scope', '$routeParams', '$modal', 'profileService', 'menuService', '$location', 'loanService', 'toastr', '$log',
    function ($scope, $routeParams, $modal, profileService, menuService, $location, loanService, toastr, $log) {

        $scope.form = {};
    
        $scope.datePickers = {};

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
            showWeeks: false
        };

        // open date picker
        $scope.openDatePicker = function ($event, pickerName) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datePickers[pickerName] = true;
        };

        if ($routeParams.id !== "new") {
            $log.debug($routeParams.id);
            $log.debug($scope.profileId);
        }

        profileService.getprofileApplication($scope.profileId).then(function (result) {

            $scope.model = result.data;
        
        });

        $scope.displayModal = function (messages) {

            var modal = $modal.open({
                templateUrl: 'app/common/partials/errorModal.html',
                controller: 'errorModalCtrl',
                size: 'md',
                resolve: {
                    config: function () {
                        return {
                            title: 'Save profile Error',
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

        $scope.saveprofile = function (andClose) {

            // validate the basic required fields for the Save profile.
           
                profileService.saveprofile($scope.model).then(function (response) {

                    if (response.data.resultCode !== 0) {

                        var modal = $modal.open({
                            templateUrl: 'App/Common/Partials/errorModal.html',
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
                        $location.path('/dashboard');
                    }
                    else if ($scope.profileId == "") {
                        $location.path('profile/');
                    }

                }, function (result) {

                    var modal = $modal.open({
                        templateUrl: 'App/Common/Partials/errorModal.html',
                        controller: 'errorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save profile Error',
                                    errors: _.map(result.data.responseStatus.error, 'message'),
                                    messages: result.data.responseStatus.message ? result.data.responseStatus.message : ""
                                };
                            },
                            $modalInstance: function () {
                                return function () {
                                    return modal;
                                }
                            }
                        }
                    });
                });
            
        };

    }]);
}());