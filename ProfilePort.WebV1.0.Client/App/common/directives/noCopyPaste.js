(function () {
    angular.module('app').directive('noCopyAndPaste', function () {
        return {
            scope: {},
            link: function (scope, element) {
                element.on('cut copy paste', function (event) {
                    event.preventDefault();
                });
            }
        };
    });
}());