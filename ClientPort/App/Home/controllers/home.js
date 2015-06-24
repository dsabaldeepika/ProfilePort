(function () {
    angular.module('app').controller('home', ['$scope', '$routeParams', '$modal', 'homeService', '$location', 'toastr',
    function ($scope, $routeParams, $modal, homeService, $location, toastr) {

        //hardcoded for now- Todo: to be changed 
        $scope.dashboardId = "2";
    
    }]);
}());