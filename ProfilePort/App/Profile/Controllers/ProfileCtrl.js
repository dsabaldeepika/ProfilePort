(function () {
    angular.module('app').controller('leApplicationCtrl', ['$scope', '$routeParams', '$modal', 'leadService', 'menuService', '$location', 'loanService', 'toastr', 'addressService', '$log',
    function ($scope, $routeParams, $modal, leadService, menuService, $location, loanService, toastr, addressService, $log) {

        $scope.form = {};
        $scope.form.username = {};
        $scope.page.title = 'Loan Application';
        $scope.datePickers = {};
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1,
            showWeeks: false
        };

        $scope.leadId = "";
        $scope.maskSsn = false;
        $scope.activeAddressTab = {
            home: true,
            property: false,
            prior: false
        };

        // open date picker
        $scope.openDatePicker = function ($event, pickerName) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datePickers[pickerName] = true;
        };

        if ($routeParams.id !== "new") {
            $log.debug($routeParams.id);
            $scope.leadId = decodeURIComponent($routeParams.id);
            $log.debug($scope.leadId);
        }

        // Add the notes link to the menu when a lead Id exists.
        if ($scope.leadId !== "") {

            // render the secondary menu
            menuService.setSecondaryMenu([
                { title: 'View Notes', icon: 'fa-file-text-o', url: '#/lead/notes/' + $scope.leadId },
                { title: 'Broker documents', icon: 'fa fa-newspaper-o', url: '#/lead/broker/' + $scope.leadId }
            ]);
        }


        //$scope.$watch('model.borrower.username', function (inputValue) {

        //    if (inputValue == undefined) return ''

        //    var transformedInput = inputValue.replace(/[^0-9a-zA-Z., _ \n$]/g, '');

        //    if (transformedInput != inputValue) {
        //        toastr.info(inputValue)

        //        $scope.form.username.$setViewValue(transformedInput);
        //        $scope.form.username.$setViewValue(transformedInput)


        //    }
        //},true)

        leadService.getLeadApplication($scope.leadId).then(function (result) {

            $scope.model = result.data;
            $scope.invalidBusinessCategory = false;

            if ($scope.model.loanTypeId) $scope.category = _.cloneDeep($scope.model.business.category);

            //$scope.form.socialSecurityNumber.$setPristine();

            $scope.maskSsn = typeof ($scope.model.borrower.socialSecurityNumber) != "undefined" &&
                                $scope.model.borrower.socialSecurityNumber != null &&
                                $scope.model.borrower.socialSecurityNumber != "";

            if ($scope.model.agentName == "") $scope.model.agentName = "Unassigned";

            if ($scope.model.business && $scope.model.business.loanPurpose != null) {
                $scope.model.business.loanPurpose = $scope.model.business.loanPurpose.toString();
            }
            if ($scope.model.business && $scope.model.business.entityType) {
                $scope.model.business.entityType = $scope.model.business.entityType.toString();
            }
            if ($scope.model.employer && $scope.model.employer.employmentType) {
                $scope.model.employer.employmentType = $scope.model.employer.employmentType.toString();
            }

            // check to see if the loan was submitted
            if (result.data.publicLoanId != null) {
                $scope.submitApplication(true);
                return;
            }

            // check for manual lead creation
            if ($scope.leadId == "" && $scope.model.agentLimits !== null) {
                $scope.manualLeadModal();
            }

            // prefill the password if needed
            $scope.generatePassword();

            // check to see if the state is fundable
            $scope.isStateFundable(true);

            // perform retread check
            $scope.performRetreadCheck(true);

        }, function (result) {

            // report on application load errors
            var message = "Could not load the application.";
            if (result.data.responseStatus != undefined) {
                message = "Could not load application be cause of the error '" +
                    result.data.responseStatus.errorCode + ": " + result.data.responseStatus.message + "'";
            }

            toastr.error(message, "Application Load Error");
            $location.path('/lead/pipeline');
        });


        $scope.$watch('[model.borrower.firstName,model.borrower.lastName,model.borrower.dateOfBirth]', function () {
            $scope.generatePassword();
        }, true);


        $scope.manualLeadModal = function () {

            if ($scope.model.agentLimits.maxManualLeads - $scope.model.agentLimits.todayManualLeads <= 0) {

                modal = $modal.open({
                    templateUrl: '/app/common/partials/messageModal.html',
                    controller: 'messageModalCtrl',
                    backdrop: 'static',
                    size: 'md',
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        config: function () {
                            return {
                                title: "Cannot Create Manual Leads",
                                message: "You have reached your maximum manual lead limit of " + $scope.model.agentLimits.maxManualLeads + " for today.",
                                buttons: [
                                    { text: "OK", style: "btn-primary", result: "OK" }
                                ]
                            }
                        }
                    }
                });

                modal.result.then(function () {
                    // redirect back.
                    $location.path('/lead/pipeline');

                });

                return;
            }

            var remainingLeads = $scope.model.agentLimits.maxManualLeads - $scope.model.agentLimits.todayManualLeads - 1;
            var remainingMessage = remainingLeads == 1 ? remainingLeads + " more lead" : remainingLeads + " more leads";
            var msg = "Are you sure you want to add a new lead? After this you can add " + remainingMessage +
                ". You are limited to " + $scope.model.agentLimits.maxManualLeads + " per day.";

            var modal = $modal.open({
                templateUrl: '/app/common/partials/messageModal.html',
                controller: 'messageModalCtrl',
                backdrop: 'static',
                size: 'md',
                resolve: {
                    $modalInstance: function () { return function () { return modal; } },
                    config: function () {
                        return {
                            title: "Create a Manual Lead?",
                            message: msg,
                            buttons: [
                                { text: "Yes", style: "btn-primary", result: "Yes" },
                                { text: "No", style: "btn-danger", result: "No" }
                            ]
                        }
                    }
                }
            });

            modal.result.then(function (action) {

                // keep going
                if (action === 'Yes') return;



                // redirect back.
                $location.path('/lead/pipeline');

            });

        };

        // generates a password for the loan.
        $scope.generatePassword = function () {

            // exit when there is no model set.
            if (typeof ($scope.model) == "undefined") return;

            // convert fields to a workable section
            var firstName = $scope.model.borrower.firstName;
            var lastName = $scope.model.borrower.lastName;
            var dateOfBirth = moment($scope.model.borrower.dateOfBirth).year();

            if (firstName == null || lastName == null || firstName.length === 0 || lastName.length === 0 || isNaN(dateOfBirth)) return;

            // format the password
            $scope.model.borrower.password = firstName[0].toLowerCase() + lastName[0].toLowerCase() + dateOfBirth;
        }

        // validate the birthday and check it for the minimum age.
        $scope.validateDateOfBirth = function () {
            var dateOfBirth = $scope.model.borrower.dateOfBirth;
            if (!appHelpers.common.isNotNull(dateOfBirth)) return;
            if (!$scope.model.business) return;
            var years = moment().diff(dateOfBirth, 'years');
            $scope.model.borrower.age = years;

            leadService.getLoanMinimumAge($scope.model.loanTypeId).then(function (result) {

                $scope.model.loanMinimumAge = result.data;

                if ($scope.model.borrower.age >= $scope.model.loanMinimumAge) {
                    $scope.performDuplicateCheck();
                    return;
                }

                var message = "This person must be at least " + $scope.model.loanMinimumAge +
                    ' to apply for this type of loan.';

                var modal = $modal.open({
                    templateUrl: '/app/common/partials/messageModal.html',
                    controller: 'messageModalCtrl',
                    backdrop: 'static',
                    size: 'sm',
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        config: function () {
                            return {
                                title: "Minimum Age Requirements",
                                message: message,
                                buttons: [{ text: "OK", style: "btn-danger", result: true }]
                            }
                        }
                    }
                });
            });
        }

        $scope.reApproveDisclosureConsumerLoan = function (obj) {

            // exit if the incoming changed value is null
            if (!appHelpers.common.isNotNull(obj)) return;

            if ($scope.model.loanTypeId == 0) {
                if ($scope.model.borrower.agreedToDisclosures == true) {
                    $scope.model.borrower.agreedToDisclosures = false;
                }
            }
        }

        $scope.reapproveDisclosureBusinessLoan = function (obj) {
            // exit if the incoming changed value is null
            if (!appHelpers.common.isNotNull(obj)) return;

            if ($scope.model.loanTypeId == 3) {
                if ($scope.model.borrower.agreedToDisclosures == true) {
                    $scope.model.borrower.agreedToDisclosures = false;
                }
            }
        }

        // this will determine if the state is fundable based on the loan and entity if a business loan.
        $scope.isStateFundable = function (obj) {

            // exit if the incoming changed value is null
            if (!appHelpers.common.isNotNull(obj)) return;

            var homeState = $scope.model.addresses.home.state;
            var loanTypeId = $scope.model.loanTypeId;

            var businessState = $scope.model.business != null ? $scope.model.business.state : null;
            var entityTypeId = $scope.model.business != null ? $scope.model.business.entityType : null;

            // a home state must ALWAYS be provided.
            if (!appHelpers.common.isNotNull(homeState) || homeState == "") {
                $scope.aFundableState = false;
                return;
            }

            // get the requirements for the business
            if (loanTypeId == 0) {

                // default the unused values
                businessState = "";
                entityTypeId = 0;
            }
            else if (loanTypeId == 3) {

                // The entity type and business state must be provided
                if (!appHelpers.common.isNotNull(entityTypeId) || entityTypeId == "" ||
                    !appHelpers.common.isNotNull(businessState) || businessState == "") {

                    $scope.aFundableState = false;
                    return;
                }
            }
            else {

                // ensure the loan type id is consumer or business
                $scope.aFundableState = false;
                return;
            }

            leadService.isStateFundable(homeState, loanTypeId, entityTypeId, businessState).then(function (result) {
                var msg;
                // set the a fundable state.
                $scope.aFundableState = result.data === "Yes" ? true : null;
                if (result.data === "Yes") return;

                // build the error modal and messages
                var businessFullForm = $scope.model.lists.states[businessState];
                var homeFullForm = $scope.model.lists.states[homeState];

                var buttons = [{ text: "OK", style: "btn-danger", result: true }];
                var title = "Cannot Fund Consumer Loan";

                if (loanTypeId == 3 && result.data === "HomeNotValid") {

                    msg = "A business loan cannot be funded in  " + businessFullForm + " because the home state of "
                        + homeFullForm + "  does not allow it.";
                    title = "Cannot Fund Business Loan";
                }
                else if (loanTypeId == 3 && result.data === "No") {
                    msg = "A business loan cannot be funded in the state of  " + businessFullForm;
                    title = "Cannot Fund Business Loan";
                }
                else if (loanTypeId == 0 && result.data === "No") {
                    msg = "A consumer loan cannot be funded in the state of  " + homeFullForm;
                    title = "Cannot Fund Consumer Loan";
                }

                if (result.data === "BusinessOnly") {
                    // business only.
                    msg = "A consumer loan is not available in the state of " + homeFullForm +
                        ". However, a business loan may be available. Would you like to apply for a business loan instead?";
                    buttons = [
                        { text: "Yes", style: "btn-primary", result: "Yes" },
                        { text: "No", style: "btn-danger", result: "No" }
                    ];
                }

                else if (loanTypeId == 3 && result.data === "EntityNotValid") {
                    var entityTypeName = $scope.model.lists.businessEntities[entityTypeId];
                    msg = "A business loan cannot be funded for a " + entityTypeName + " business entity in the state of " + businessFullForm + ".";
                    title = "Cannot Fund Business Loan";
                }

                var modal = $modal.open({
                    templateUrl: '/app/common/partials/messageModal.html',
                    controller: 'messageModalCtrl',
                    backdrop: 'static',
                    size: 'md',
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        config: function () {
                            return {
                                title: title,
                                message: msg,
                                buttons: buttons
                            }
                        }
                    }
                });

                modal.result.then(function (action) {
                    if (action !== 'Yes') return;
                    $scope.model.loanTypeId = 3;
                });
            });
        }

        // use the home address for an additional type
        $scope.useHomeAddress = function (addressType) {
            if (addressType === "property") {
                $scope.model.addresses.property.address1 = $scope.model.addresses.home.address1;
                $scope.model.addresses.property.address2 = $scope.model.addresses.home.address2;
                $scope.model.addresses.property.state = $scope.model.addresses.home.state;
                $scope.model.addresses.property.zipCode = $scope.model.addresses.home.zipCode;
                $scope.model.addresses.property.city = $scope.model.addresses.home.city;
            }
            else if (addressType === "business") {
                $scope.model.business.address1 = $scope.model.addresses.home.address1;
                $scope.model.business.address2 = $scope.model.addresses.home.address2;
                $scope.model.business.state = $scope.model.addresses.home.state;
                $scope.model.business.zipCode = $scope.model.addresses.home.zipCode;
                $scope.model.business.city = $scope.model.addresses.home.city;
            }
        };

        // auto change the property and prior when selecting them.
        $scope.changeAddressTab = function (tabName) {
            if (!$scope.model.addresses.isHomeOwner && tabName == "property") return;
            if (!$scope.model.addresses.hasPriorAddress && tabName == "prior") return;

            if ($scope.model.addresses.isHomeOwner && tabName == "property" &&
                $scope.model.addresses.property.address1 == null) {
                $scope.useHomeAddress("property");
            }

            $scope.activeAddressTab.home = false;
            $scope.activeAddressTab.property = false;
            $scope.activeAddressTab.prior = false;
            $scope.activeAddressTab[tabName] = true;
        }

        // check zip-code
        $scope.checkZipCode = function (group, zipCode) {
            // validate the zipcode.
            addressService.getZipCode(zipCode).then(function (result) {

                if (result.data === "") return;
                var city = result.data.city;
                var state = result.data.stateCode;

                if (group === 'business') {

                    $scope.model.business.city = city;
                    $scope.model.business.state = state;

                } else if (group === 'home') {
                    $scope.model.addresses[group].city = city;
                    $scope.model.addresses[group].state = state;

                } else if (group === 'property') {
                    $scope.isStateFundable(state);
                    $scope.model.addresses[group].city = city;
                    $scope.model.addresses[group].state = state;

                } else if (group === 'prior') {
                    $scope.isStateFundable(state);
                    $scope.model.addresses[group].city = city;
                    $scope.model.addresses[group].state = state;

                }
                $scope.isStateFundable(state);
            });
        };

        // require the  user to provide the SSN again to validate.
        $scope.validateSsn = function () {

            // Since validate ssn runs before the xt-validate directive, it doesn't remove the 
            // messages in $error, and the system thinks there is an error, which is a false 
            // positive.  To correct this, delete the error if it exists and revalidate.
            if ($scope.model.borrower.socialSecurityNumber == null ||
                $scope.model.borrower.socialSecurityNumber == "") return;

            if ($scope.form.socialSecurityNumber.$invalid &&
                typeof ($scope.form.socialSecurityNumber.$error.messages) == "undefined") {
                delete $scope.form.socialSecurityNumber.$error.messages;
            }
            // exit if the social is invalid
            if (typeof ($scope.model.borrower.socialSecurityNumber) == "undefined" ||
                $scope.model.borrower.socialSecurityNumber == "" ||
                !$scope.form.socialSecurityNumber.$dirty) return;

            $scope.maskSsn = true;
            $scope.validatingSsn = true;

            var modalInstance = $modal.open({
                templateUrl: 'app/lead/partials/verifySsnModal.html',
                controller: 'leVerifySsnModalCtrl',
                size: 'sm',
                scope: $scope,
                resolve: {
                    parentSocialSecurityNumber: function () {
                        return $scope.model.borrower.socialSecurityNumber;
                    },
                    $modalInstance: function () { return function () { return modalInstance; } }
                }
            });
            modalInstance.result.then(function () { }, function (data) {

                $scope.validatingSsn = false;

                if (data !== "valid") {
                    $scope.model.borrower.socialSecurityNumber = "";
                    $scope.maskSsn = false;
                }

                // check for duplicates.
                $scope.performDuplicateCheck(true);
            });
        }

        // perform duplicate check on the last name, social and dob.
        $scope.performDuplicateCheck = function (performRetreadCheckAfter) {

            var lastName = $scope.model.borrower.lastName;
            var socialSecurityNumber = $scope.model.borrower.socialSecurityNumber;
            var dateOfBirth = moment($scope.model.borrower.dateOfBirth).format("YYYY-MM-DD");
            if (!appHelpers.common.isNotNull(socialSecurityNumber) || socialSecurityNumber == "") return;

            if (lastName == null || dateOfBirth == null || dateOfBirth == "Invalid date") {
                // check to see if a retread chould be performed
                if (performRetreadCheckAfter == true) $scope.performRetreadCheck(false);
                return;
            }

            leadService.getDuplicateLeads($scope.leadId, lastName, socialSecurityNumber, dateOfBirth).then(function (result) {

                // If no data is present, no duplicates found.
                if (result.data.length == 0) {

                    // check to see if a retread chould be performed
                    if (performRetreadCheckAfter == true) $scope.performRetreadCheck(false);
                    return;
                }

                var modal = $modal.open({
                    templateUrl: '/app/lead/partials/duplicateCheckModal.html',
                    controller: 'leDuplicateCheckModalCtrl',
                    size: 'lg',
                    keyboard: false,
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        leads: function () { return result.data; },
                        leadId: function () { return $scope.leadId; }
                    }
                });

                modal.result.then(function () { }, function () {
                    // check to see if a retread chould be performed
                    if (performRetreadCheckAfter == true) $scope.performRetreadCheck(false);
                });
            });
        }


        $scope.setStatus = function (model) {
            if (model == "Not Interested") {

                var modal = $modal.open({
                    templateUrl: '/app/common/partials/messageModal.html',
                    controller: 'messageModalCtrl',
                    backdrop: 'static',
                    size: 'md',
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        config: function () {
                            return {
                                title: "Confirm Not Interested",
                                message: "Are you sure you want to set this lead to Not Interested?  This lead will be closed and removed from your pipeline immediately.",
                                buttons: [
                                    { text: "Yes", style: "btn-primary", result: true },
                                    { text: "No", style: "btn-danger", result: false }
                                ]
                            }
                        }
                    }
                });

                modal.result.then(function (result) {
                    if (result === false)
                        $modalInstance().dismiss('cancel');

                    else
                        leadService.setNotInterested($scope.leadId).then(function () {

                            // redirect after the lead has been saved.
                            $location.path('/lead/pipeline');
                        });

                });


            }
        }

        // perform a retread check on the ssn
        $scope.performRetreadCheck = function (silent) {

            if ($scope.model.borrower.socialSecurityNumber === null ||
                $scope.model.borrower.socialSecurityNumber == "") {

                $scope.model.retreadLoan = false;
                return;
            }

            leadService.getRetreadLeads($scope.leadId, $scope.model.borrower.socialSecurityNumber).then(function (result) {

                // If no data is present, no retreads found.
                $scope.retreads = result.data.retreads;
                $log.debug($scope.retreads);

                // no existing loans found, continue
                if ($scope.retreads.length == 0) {
                    // only set the retread loan flag to true, meaning it cannot be false once it's become true
                    if ($scope.model.retreadLoan != true) $scope.model.retreadLoan = false;
                    $scope.model.canRetread = false;
                    return;
                }

                // sum the code, anything higher than 0 is a cannot retread.
                $scope.model.canRetread = $scope.retreads.reduce(function (sum, obj) { return sum + obj.status; }, 0) == 0;
                $scope.model.retreadLoan = true;

                var bday = result.data.dateOfBirth != null ? result.data.dateOfBirth : '';

                // do not display if silent
                if (silent == true) return;

                var modal = $modal.open({
                    templateUrl: 'app/lead/partials/retreadWarningModal.html',
                    controller: 'leRetreadWarningModalCtrl',
                    backdrop: 'static',
                    size: 'md',
                    scope: $scope,
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } }
                    }
                });
                modal.result.then(function () { }, function (action) {

                    if (action == 'cancel') {
                        $scope.maskSsn = false;
                        $scope.model.canRetread = null;
                        $scope.model.retreadLoan = null;
                        $scope.model.borrower.socialSecurityNumber = "";
                    }
                    else if (action == "not-qualified") {

                        var status = "Cannot Retread";
                        // mark the lead as Cannot retread (per cory)


                        $scope.model.leadStatus = status;
                        $scope.model.retreadLoan = true;
                        $scope.saveLead();
                        if (!_.contains($scope.model.lists.leadStatuses, status)) {
                            $scope.model.lists.leadStatuses.push(status);
                        }

                    }
                    else if (action == "ok") {
                        if ($scope.model.canRetread) {
                            $scope.model.borrower.dateOfBirth = bday;
                        }

                        $scope.saveLead();
                    }
                });

            });
        };

        // display a modal with the list of loans for this social.
        $scope.viewRetreads = function () {

            if ($scope.retreads == undefined || $scope.retreads.length == 0) {
                return;
            }

            var modal = $modal.open({
                templateUrl: '/app/lead/partials/retreadCheckModal.html',
                controller: 'leRetreadCheckModalCtrl',
                backdrop: 'static',
                size: 'lg',
                keyboard: false,
                resolve: {
                    $modalInstance: function () { return function () { return modal; } },
                    retreads: function () { return $scope.retreads; }
                }
            });
        }

        // open the submit application window.
        $scope.submitApplication = function (byPassValidation) {

            //check if the business category is valid and not pristine

            if ($scope.model.business.category != null) {
                $scope.checkCategory($scope.model.business.category);

            }
            if ($scope.model.loanTypeId == 3 && $scope.model.borrower.acceptsAutoPay != true) {
                $scope.form.autoPay.$setValidity("required", false);
            }

            if ($scope.invalidBusinessCategory == false) {

                if (byPassValidation !== true) {

                    $scope.form.validate();
                    if ($scope.form.$invalid) {

                        var modal = $modal.open({
                            templateUrl: 'app/common/partials/errorModal.html',
                            controller: 'coErrorModalCtrl',
                            size: 'md',
                            resolve: {
                                config: function () {
                                    return {
                                        title: 'Application Error',
                                        description: 'Please correct the following issues before submitting the application:',
                                        form: $scope.form
                                        //errors: _.map(messages, 'message'),
                                        // messages:messages ?messages:""
                                    };
                                },
                                $modalInstance: function () {
                                    return function () {
                                        return modal;
                                    }
                                }
                            }
                        });

                        return;
                    }
                }
                var modalInstance = $modal.open({
                    templateUrl: 'app/lead/partials/submitApplication.html',
                    controller: 'leSubmitApplicationModalCtrl',
                    size: 'md',
                    scope: $scope,
                    keyboard: false,
                    backdrop: 'static',
                    resolve: {
                        parentScope: function () {
                            return $scope;
                        },
                        '$modalInstance': function () { return function () { return modalInstance; } }
                    }
                });

                modalInstance.result.then(function () {
                }, function (data) {

                    // if the action was cancelled, it will not return a productId.
                    if (typeof (data.productId) === "undefined") {
                        return;
                    }

                    // set the model to submitted to prevent resubmissions.
                    $scope.model.submitted = true;

                });

            }
        }

        // open the lead history table.
        $scope.openLeadHistory = function () {

            // get the lead history from the web service.
            leadService.getLeadHistory($scope.leadId).then(function (result) {

                $log.debug(result);

                // open the modal and supply the modal with the lead history table.
                var modal = $modal.open({
                    templateUrl: 'app/lead/partials/historyModal.html',
                    controller: 'leHistoryModalCtrl',
                    size: 'md',
                    resolve: {
                        $modalInstance: function () { return function () { return modal; } },
                        history: function () { return result.data.history; }
                    }
                });
            });
        };

        $scope.displayModal = function (messages) {

            var modal = $modal.open({
                templateUrl: 'app/common/partials/errorModal.html',
                controller: 'coErrorModalCtrl',
                size: 'md',
                resolve: {
                    config: function () {
                        return {
                            title: 'Save Lead Error',
                            description: 'Please correct the following issues before saving the application:',
                            messages: messages,


                        };
                    },
                    $modalInstance: function () {
                        return function () {
                            return modal;

                        }
                    }
                }
            });

            return;
        }

        $scope.checkCategory = function (model) {

            if (!_.isEqual(model, $scope.category)) {

                if (!_.contains($scope.businessCategoriesAutoComplete.dataSource._data, model) && $scope.model.loanTypeId === 3) {
                    var message = "Please choose a business category from the dropdown.";
                    $scope.invalidBusinessCategory = true;
                    $scope.displayModal(message);

                }
                else {
                    $scope.invalidBusinessCategory = false;
                }
            }
        }

        $scope.saveLead = function (andClose) {

            $scope.model.borrower.username = $scope.model.borrower.username ? $scope.model.borrower.username.toLowerCase() : "";

            //if ($scope.form.username.$dirty && $scope.form.username.$error.pattern) $scope.displayModal("Username must contain at least 6 characters. Do not include special characters (e.g.: &, *, @, #, -)"); return;

            //defaulting category to valid before saving
            if ($scope.model.business.category == null) $scope.invalidBusinessCategory = false;

            //check if the business category is valid and not pristine
            if ($scope.model.loanTypeId == 3 && $scope.model.business.category != null) {
                $scope.checkCategory($scope.model.business.category);
            }

            // validate the basic required fields for the Save Lead.
            if (!$scope.invalidBusinessCategory) {
                leadService.saveLead($scope.model).then(function (response) {

                    if (response.data.resultCode !== 0) {

                        var modal = $modal.open({
                            templateUrl: 'app/common/partials/errorModal.html',
                            controller: 'coErrorModalCtrl',
                            size: 'md',
                            resolve: {
                                config: function () {
                                    return {
                                        title: 'Save Lead Error',
                                        errors: response.data.description ? response.data.description : "",
                                        messages: response.data.message ? response.data.message : ""
                                    };
                                },
                                $modalInstance: function () {
                                    return function () {
                                        return modal;
                                    }
                                }
                            }
                        });

                        return;
                    }

                    toastr.success("The lead has been saved.", "Lead Saved");



                    if (andClose) {
                        $location.path('/lead/pipeline');
                    }
                    else if ($scope.leadId == "") {
                        $location.path('lead/application/' + response.data.leadId);
                    }

                }, function (result) {

                    var modal = $modal.open({
                        templateUrl: 'app/common/partials/errorModal.html',
                        controller: 'coErrorModalCtrl',
                        size: 'md',
                        resolve: {
                            config: function () {
                                return {
                                    title: 'Save Lead Error',
                                    errors: _.map(result.data.responseStatus.error, 'message'),
                                    messages: result.data.responseStatus.message ? result.data.responseStatus.message : ""
                                };
                            },
                            $modalInstance: function () {
                                return function () {
                                    return modal;
                                }
                            }
                        }
                    });
                });
            }
        };

        // perform the auto complete searching.
        $scope.businessCategoriesAutoComplete = {
            dataSource: new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (options) {
                        // get the lead pipeline data
                        leadService.getBusinessCategories($scope.model.business.category).then(function (d) {
                            options.success(d.data);
                        });
                    }
                }
            })
        };

    }]);
}());