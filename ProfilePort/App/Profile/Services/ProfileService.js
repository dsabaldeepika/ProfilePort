(function () {

    angular.module('app').factory('leadService', ['$http', 'siteConfig', 'toastr', '$log',
        function ($http, siteConfig, toastr, $log) {
            var serviceBase = siteConfig.apiHosts.lead,
                factory = {};


            // This pulls in the content for the loan queue.
            factory.getLeadPipeline = function (filters, sort) {


                //// modify the sorting if applicable.

                if (typeof (sort) !== "undefined" && sort.length >= 1) {

                    filters.orderBy = sort[0].field;
                    filters.orderByDesc = sort[0].dir;

                }

                return $http.get('/lead/pipeline', { params: filters })
                    .success(function (data, status, headers, config) {

                        return data;


                    })
                    .error(function (data, status, headers, config) {

                        return [];


                    });
            };

            // This pulls in the content for the loan queue.
            factory.getLeadDocuments = function (filters, sort) {

                // modify the sorting if applicable.
                if (typeof (sort) !== "undefined" && sort.length >= 1) {
                    if (sort[0].dir === "asc") {
                        filters.orderBy = sort[0].field;
                    }
                    else {
                        filters.orderByDesc = sort[0].field;
                    }
                }


                return $http.get('/data/lead/documents-grid.json')
                    .success(function (data, status, headers, config) {
                        // this callback will be called asynchronously
                        // when the response is available


                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        return [];
                    });
            };

            // Query a specific lead detail record.
            factory.getLeadApplication = function (leadId) {

                return $http.get('/lead/details/' + leadId)
                    .success(function (data, status, headers, config) {
                        // this callback will be called asynchronously
                        // when the response is available
                        $log.debug(data)
                        factory.firstName = data.borrower.firstName;
                        factory.lastName = data.borrower.lastName;

                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        return null;
                    });
            };

            // save the leads assigned to the agents
            factory.AssignLeadToAgent = function (request) {

                return $http.post('/lead/multipleclaim/', request)
                    .success(function (data, status, headers, config) {

                        return true;
                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        //toastr.error("An error occurred while Assigning the leads to the agent",
                        //"Lead Assignment Error");

                        return false;
                    });
            }


            // save the lead record
            factory.saveLead = function (request) {


                return $http.post('/lead/upsert', request)

                    .success(function (data, status, headers, config) {
                        // this callback will be called asynchronously
                        // when the response is available
                        //alert(typeof config.data.)
                        return data;
                    })
                    .error(function (data, status, headers, config) {

                        return data;
                    });
            }


            // Get a list of lead Criterias for an application like Status, Agent...
            factory.getLeadPipelineViewModel = function () {

                return $http.get('/lead/pipeline/viewmodel')
                    .success(function (data, status, headers, config) {
                        factory.assignAgents = data.assignAgents
                        //console.log(data.assignAgents);
                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        return null;
                    });
            }


            // Get a list of business categories
            factory.getBusinessCategories = function (searchText) {

                return $http.get('/lead/business/category/' + searchText)
                    .success(function (data, status, headers, config) {
                        return data;
                    })
                    .error(function (data, status, headers, config) {
                        return null;
                    });
            }

            // get a list of possible duplicate leads that use fuzzy logic.
            factory.getDuplicateLeads = function (poid, lastName, socialSecurityNumber, dateOfBirth) {

                if (poid == "" || poid == null) poid = 0;
                return $http.get('/lead/duplicates/' + poid + '/' + lastName + '/' + socialSecurityNumber + '/' + dateOfBirth)
                    .success(function (result) {
                        $log.debug(result.data)
                        return result.data;
                    })
                    .error(function (result) {
                        return result.data;
                    });
            };

            // claim a lead for an agent done during a duplicate check.
            factory.claimDuplicateLead = function (duplicateLeadId, claimLeadId) {

                return $http.post('/lead/duplicate/claim', { duplicateLeadId: duplicateLeadId, claimLeadId: claimLeadId })
                    .success(function (result) {
                        return result.data;
                    })
                    .error(function (result) {
                        return result.data;
                    });
            };

            // get a list of possible retread leads.
            factory.getRetreadLeads = function (poid, socialSecurityNumber) {
                if (poid == "" || poid == null) poid = 0;
                return $http.get('/lead/retread/' + poid + '/' + socialSecurityNumber)
                    .success(function (result) {
                        return result.data;
                    })
                    .error(function (result) {
                        return result.data;
                    });
            };

            factory.isStateFundable = function (homeState, loanTypeId, entityTypeId, businessStateCode) {

                if (businessStateCode == null) {
                    businessStateCode = "";
                }

                if (homeState == null) {
                    homeState = "";
                }
                return $http.get('/loan/application/statefundable/' + homeState + '/' +
                    loanTypeId + '/' + entityTypeId + '/' + businessStateCode)
                    .success(function (result) {
                        return result.data;

                    })
                    .error(function (result) {
                        return false;

                    });
            };

            factory.getLoanMinimumAge = function (loanTypeId) {
                return $http.get('/loan/application/minimumage/' + loanTypeId)
                    .success(function (result) {
                        return result.data;
                    })
                    .error(function (result) {
                        return false;
                    });
            };

            // unassign the lead and set the status to not interested
            factory.setNotInterested = function (leadId) {

                return $http.post('/loan/application/notinterested/' + leadId)
                    .success(function (result) {
                        toastr.success("The lead have been set to Not Interested and unassigned.");
                        return result.data;
                    })
                    .error(function (result) {
                        return result.data;
                    });
            };

            factory.getLeadHistory = function (leadId) {
                return $http.get('/lead/history/' + leadId);
            };

            // return the factory.
            return factory;
        }]);


}());