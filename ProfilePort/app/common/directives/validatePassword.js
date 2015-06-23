''/**
 * Created by devin.delapp on 12/10/2014.
 */
(function () {
    // this directive is much easier to use than a traditional pattern because you can specify each pattern piece.
    angular.module('app').directive('validatePassword', function () {

        var REQUIRED_PATTERNS = [
          /\d+/,    //numeric values
          /[a-z]+/, //lowercase values
          /[A-Z]+/, //uppercase values
         // /\W+/,    //special characters
         // /^\S+$/   //no whitespace allowed
        ];

        return {
            require: 'ngModel',
            link: function ($scope, element, attrs, ngModel) {
                ngModel.$validators.passwordCharacters = function (value) {
                    var status = true;
                    angular.forEach(REQUIRED_PATTERNS, function (pattern) {
                        status = status && pattern.test(value);
                    });
                    return status;
                };
            }
        }
    });
}());