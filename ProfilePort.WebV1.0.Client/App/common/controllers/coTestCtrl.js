
(function () {


    angular.module('app').controller('coTestCtrl', ['$scope', '$modalInstance', 'config',
        function ($scope, $modalInstance, config) {

            $scope.readableString = function (str) {

                var returnString = str[0].toUpperCase();

                for (var i = 1; i < str.length; i++) {

                    if (str[i] >= 'A' && str[i] <= 'Z') {
                        returnString += ' ' + str[i];
                    } else if (str[i] == '-' || str[i] == '_') {
                        returnString += ' ';
                    } else {
                        returnString += str[i];
                    }
                }

                return returnString;
            }



            if (config.form != undefined) {

                // Create a list of generic messages.
                var errorMessages = {
                    required: " is required.",
                    minlength: " does not meet the minimum length.",
                    maxlength: " exceeds the maximum length.",
                    pattern: " has an invalid pattern.",
                    email: " is not a valid e-mail.",
                    mask: " does not match the expected format.",
                    number: " is not a number.",
                    url: " is not a valid url.",
                    minvalue: " is too small.",
                    maxvalue: " is too large.",
                    min: " does not meet the minimum requirement.",
                    max: " does not meet the maximum requirement.",
                    // invalidCategory: "  is invalid. Please Select a Business Category from the DropDown"

                };

                // compile a list of all errors associated to the parent form and display in an array.
                var errors = [];
                angular.forEach(config.form.$error, function (value, key) {
                    angular.forEach(value, function (field, k) {


                        // get the readable name
                        var name = $scope.readableString(field.$name);
                        var message = name + " has a " + key + " error.";
                        if (errorMessages[key] != undefined) {
                            message = name + errorMessages[key];
                        }

                        this.push(message);

                    }, this);
                }, errors);

                $scope.errors = errors;
            }
                // assume an errors list was passed.
            else {

                $scope.errors = config.errors;
            }

            $scope.errorModal = {
                title: config.title || "Validation Error",
                description: config.description || "Please review the following errors:",

                closeButton: config.closeButton || "Close"
            };
            $scope.messages = config.messages;

            $scope.close = function () {
                $modalInstance().dismiss('close');
            };

        }]);

}());