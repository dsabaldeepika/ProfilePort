(function () {
    angular.module('app').controller('home', ['$scope', '$routeParams', '$modal', 'homeService', '$location', 'toastr',
    function ($scope, $routeParams, $modal, homeService, $location, toastr) {

        //hardcoded for now- Todo: to be changed 
        
        $scope.userId = 'sdf';

        $scope.status =
            {
                isdeleted: true
            };

       //var userId= $scope.userId
       // homeResources.query({ userId: userId }, function (response) {
       ////homeResources.query(function (response) {
       //    $scope.dashboard = JSON.parse(response);
       //    console.log($scope.dashboard);
       //});

       // //ngresource is better for restful api, built on top of $http
       //$scope.deactivateDashboard = function () {
       //    homeResources.dashboard.$delete(function () {
       //        //this is needed to update , but since one dashboard corresponds to one user there is no need to update
       //        //this will deactivate the profile for a certain time but will not delete
       //        //$scope.dashboard = homeResources.query({ userId: userId });
       //    });
       //};

        $scope.getuser = function () {

            homeService.getUser($scope.userId).then(function (result) {
                $scope.dashboard = result;
                console.log($scope.dashboard);
                console.log(result);
                $scope.originalModel = _.cloneDeep($scope.model);
            })
        };

        $scope.getuser();









    
    }]);
}());