/**
 * Created by sabal.prasain on 06/08/2015.
 */


(function () {

    angular.module('app').factory('stateService', [function () {

        var factory = {};

        // set the page state
        factory.setPageState = function (pageName, state) {
            state = JSON.stringify(state);
            localStorage.setItem("page." + pageName, state);
        };

        // return the page state
        factory.getPageState = function (pageName, noDelete) {

            if (localStorage.getItem("page." + pageName) === null) return null;

            var pageState = JSON.parse(localStorage.getItem("page." + pageName));
            if (noDelete !== true) {
                factory.clearPageState(pageName);
            }

            return pageState;

        };

        factory.clearPageState = function (pageName) {

            if (localStorage.getItem("page." + pageName) === null) return;
            localStorage.removeItem("page." + pageName);
        }

        // return the state
        return factory;

    }]);

}());


