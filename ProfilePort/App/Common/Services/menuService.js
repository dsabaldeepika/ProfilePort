
(function () {

    angular.module('app').factory('menuService', ['$http', 'sessionService',
        function ($http, sessionService) {

            var factory = {
                secondaryMenuScope: null,
                primaryMenu: null
            };

            factory.getPrimaryMenu = function () {

                var role = sessionService.getPrimaryRole();
                if (role == "" || role == null) return null;

                return $http.get('/app/resources/menu/' + role + '.json')
                    .success(function (result) {
                        return result.data;
                    })
                    .error(function () {
                        return null;
                    }
                );
            }

            factory.setSecondaryMenu = function (menu) {
                if (factory.secondaryMenuScope == null ||
                    typeof factory.secondaryMenuScope == 'undefined') return;
                factory.secondaryMenuScope.secondaryMenu.menu = menu;
            };

            factory.clearSecondarMenu = function () {
                // if the menu isn't set yet, don't do anything.
                if (factory.secondaryMenuScope == null) return;

                factory.secondaryMenuScope.secondaryMenu.menu = [];
            };


            return factory;
        }]);

}());

