
(function () {

    angular.module('app').factory('menuService', ['$http',
        function ($http) {

            var factory = {
                secondaryMenuScope: null,
                primaryMenu: null
            };

            factory.getPrimaryMenu = function () {

                return $http.get('/App/Resources/menu/'+ administrator + '.json')
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

