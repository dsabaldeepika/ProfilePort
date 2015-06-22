
(function () {

    angular.module('app').controller('primaryMenuCtrl', ['$scope', '$location', '$rootScope', 'menuService', '$log',
        function ($scope, $location, $rootScope, menuService, $log) {

            $rootScope.$on("$routeChangeStart", function (event, next, current) {
                $log.debug('$routeChangeStart');
            });

            $rootScope.$on("loginStatusChanged", function (event, isLoggedIn) {
                $log.debug('loginStatusChanged');

                if (isLoggedIn) {
                    // render the primary menu by getting the menu in a promise
                    var menuPromise = menuService.getPrimaryMenu();
                    if (menuPromise == null) return;
                    menuPromise.then(function (result) {
                        $scope.menu = result.data.menu;
                    }, function () {
                        $scope.menu = [];
                    });
                } else {
                    $scope.menu = [];
                }
            });

            // run on load
            $scope.hideMenu = ($location.path() === "/login");

        }]);
}());