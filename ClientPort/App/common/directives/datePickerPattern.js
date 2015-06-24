
(function () {

    angular.module('app').directive('datepickerPattern', ['toastr', function (toastr) {

        var REQUIRED_PATTERNS = [
         /\d+/,
         "\^(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])\/(199\d)|([2-9]\d{3})$\/"
        ];

        return {
            restrict: 'AEC',
            require: 'ngModel',

            link: function (scope, elem, attrs, modelCtrl) {

                var date, day, month, newYear, value, year;

                // if (attrs.myPattern == "setDashes") {

                //scope.$watch(attrs.ngModel, function (dirtyModel) {
                //    var alphaPattern = new RegExp(EQUIRED_PATTERNS)

                //    var isAlha = alphaPattern.test(dirtyModel);
                //    var arr = String(dirtyModel).split("");

                //    if (dirtyModel == undefined || isAlha) return;
                //    if (!isAlha && arr.length == 10) {

                //        cleanModel = dirtyModel.replace(/\D[^]/g, "");
                //        dirtyModel = cleanModel.slice(0, 3) + "-" + cleanModel.slice(3, 6) + "-" + cleanModel.slice(6);

                //        modelCtrl.$setViewValue(dirtyModel)
                //        modelCtrl.$render();

                //    }

                //    return attrs.ngModel;

                //}, true);

                modelCtrl.$parsers.unshift(function (inputValue) {
                    if (typeof inputValue === "object") {
                        return;
                        modelCtrl.$setValidity('date', true);
                    }
                    if (_.isString(inputValue) && inputValue.length >= 6) {
                        toastr.info(typeof inputValue);
                        var expression = new RegExp(attrs.datepickerPattern);
                        var isValid = expression.test(inputValue);
                        modelCtrl.$setValidity('date', isValid);

                        if (isValid) {
                            var date = inputValue.split('/');
                            month = date[0];
                            day = date[1];
                            year = date[2];
                            if (year.length < 1 && year.length < 4) {
                                year = "20" + year;
                                // year = 1900 + parseInt(year);
                                modelCtrl.$setViewValue(month + '/' + day + '/' + year)
                                modelCtrl.$render();

                            }
                        }
                    }

                    return inputValue;
                });


                //scope.$watch('model.filters.startDate', function (newValue, oldValue) {


                //    var arr = String(newValue).split("");
                //    //if (arr.length === 0) return;
                //    if (arr.length === 1 && (arr[0] == '/' || arr[0] === '-')) return;
                //    //if (arr.length === 2 && newValue === '-.') return;
                //    if (typeof newValue==='undefined') {


                //        scope.model.filters.startDate = oldValue;

                //    }
                //});

                //scope.$watch('model.filters.endDate', function (newValue, oldValue) {


                //    var arr = String(newValue).split("");
                //    //if (arr.length === 0) return;
                //    if (arr.length === 1 && (arr[0] == '/' || arr[0] === '-')) return;
                //    //if (arr.length === 2 && newValue === '-.') return;
                //    if (typeof newValue === 'undefined') {

                //        scope.model.filters.endDate = oldValue;
                //    }
                //});

            }
        };
    }]);
})();