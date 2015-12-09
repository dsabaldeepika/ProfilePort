
(function () {
    angular.module('app').directive('restrictCharacters', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {

                element.bind("blur", function (e) {
                    if (ngModel.$viewValue === "") {
                        ngModel.$setPristine();
                        ngModel.$validate();
                    }
                });
                element.bind("keydown", function (e) {
                    // reset the form
                    if (e.keyCode === 27) {
                        ngModel.$setViewValue("");
                        ngModel.$setPristine();
                        ngModel.$render();
                        element[0].blur();
                    }
                });
                element.bind("keypress", function (e) {
                    var regex = new RegExp(attrs.ngPattern);
                    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                    if (!regex.test(key)) {
                        event.preventDefault();
                        return false;
                    }
                });
            }
        }
    });
}());