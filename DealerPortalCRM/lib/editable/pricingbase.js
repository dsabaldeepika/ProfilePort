var TableEditable = function () {
    var isSaveComplete = [];
    var pricingBaseId = null;
    var handleTable = function () {

        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTable.fnUpdate(aData[i], nRow, i, false);
            }

            oTable.fnDraw();
        }

        function editRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            //initialise pricingBaseId
            pricingBaseId = $(jqTds[6]).children("span").text().trim();

            jqTds[0].innerHTML = '<input type="text" class="form-control input-small" value="' +parseFloat(aData[0].replace(/\$|,/g, '')) + '">';
            jqTds[1].innerHTML = '<input type="text" class="form-control input-small" value="' + parseFloat(aData[1].replace(/\$|,/g, '')) + '">';
            jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[2].replace(/,/g, "") + '">';
            jqTds[3].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[3].replace(/,/g, "") + '">';
            jqTds[4].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[4].substring(0, aData[4].length - 1).trim() + '">';
            //jqTds[5].innerHTML = '<span class="glyphicon glyphicon-ok" aria-hidden="'+aData[5]+'"></span>';
            jqTds[6].innerHTML = '<a class="edit" href="">Save</a>';
            jqTds[7].innerHTML = '<a class="cancel" href="">Cancel</a>';

        }

        function saveRow(oTable, nRow) {
           
            var regexDecimalTwoPrecision = /^(\d+)(\.\d{1,2})?$/gm;
            var regexDecimalSixPrecision = /^(\d+)(\.\d{1,6})?$/gm;
            var jqInputs = $('input', nRow);

            var elem = $(jqInputs[0]).closest('td');
            if (jqInputs[0].value === '') {
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                if (!elem.find('.custValError').length) {
                    elem.append('<span class="custValError error-class">Min BV is required</span>');
                }
                isSaveComplete[0] = false;
            } else if (!jqInputs[0].value.match(regexDecimalTwoPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Min BV must be decimal number.</span>');
                }
                isSaveComplete[0] = false;
            } else {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                isSaveComplete[0] = true;
            }


            elem = $(jqInputs[1]).closest('td');
            if (jqInputs[1].value === '') {
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                if (!elem.find('.custValError').length) {
                    elem.append('<span class="custValError error-class">Max BV is required</span>');
                }
                isSaveComplete[1] = false;
            } else if (!jqInputs[1].value.match(regexDecimalTwoPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Max BV must be decimal number.</span>');
                }
                isSaveComplete[1] = false;
            } else {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                isSaveComplete[1] = true;
            }


            elem = $(jqInputs[2]).closest('td');
            if (jqInputs[2].value === '') {
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                if (!elem.find('.custValError').length) {
                    elem.append('<span class="custValError error-class">Min LTV is required</span>');
                }
                isSaveComplete[2] = false;
            } else if (!jqInputs[2].value.match(regexDecimalTwoPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Min LTV must be decimal number.</span>');
                }
                isSaveComplete[2] = false;
            } else {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                isSaveComplete[2] = true;
            }

            elem = $(jqInputs[3]).closest('td');
            if (jqInputs[3].value === '') {
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                if (!elem.find('.custValError').length) {
                    elem.append('<span class="custValError error-class">Max LTV is required</span>');
                }
                isSaveComplete[3] = false;
            } else if (!jqInputs[3].value.match(regexDecimalTwoPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Max LTV must be decimal number.</span>');
                }
                isSaveComplete[3] = false;
            } else {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                isSaveComplete[3] = true;
            }

            elem = $(jqInputs[4]).closest('td');
            if (jqInputs[4].value === '') {
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                if (!elem.find('.custValError').length) {
                    elem.append('<span class="custValError error-class">Rate discount is required</span>');
                }
                isSaveComplete[4] = false;
            } else if (!jqInputs[4].value.match(regexDecimalSixPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Rate discount must be decimal number.</span>');
                }
                isSaveComplete[4] = false;
            } else {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                isSaveComplete[4] = true;
            }

            if (jQuery.inArray(false, isSaveComplete) === -1) {
                //Create AdjustmentRange model to save data using ajax

                var pricingBaseModelModel = {
                    "PricingBaseID": pricingBaseId,
                    "PricingBaseMinBV": jqInputs[0].value.trim(),
                    "PricingBaseMaxBV": jqInputs[1].value.trim(),
                    "PricingBaseMinLTV": jqInputs[2].value.trim(),
                    "PricingBaseMaxLTV": jqInputs[3].value.trim(),
                    "PricingBasePricingRateDiscount": jqInputs[4].value.trim()

                };

                dataService.editPricingBase(pricingBaseModelModel).done(function (data) {
                    oTable.fnUpdate(data.PricingBaseMinBV, nRow, 0, false);
                    oTable.fnUpdate(data.PricingBaseMaxBV, nRow, 1, false);
                    oTable.fnUpdate(data.PricingBaseMinLTV, nRow, 2, false);
                    oTable.fnUpdate(data.PricingBaseMaxLTV, nRow, 3, false);
                    oTable.fnUpdate(data.PricingBasePricingRateDiscount+'%', nRow, 4, false);
                    if (data.IsActive == true) {
                        oTable.fnUpdate('<span class="glyphicon glyphicon-ok" aria-hidden="true"></span>', nRow, 5, false);
                    }
                    oTable.fnUpdate('<a class="edit" href="">Edit</a><span style="display:none">' + pricingBaseId + '</span>', nRow, 6, false);
                    oTable.fnUpdate('', nRow, 7, false);
                    oTable.fnDraw();
                });

            }
        }

        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 6, false);
            oTable.fnDraw();
        }

        var table = $('#tblPricingBase');

        var oTable = table.dataTable({
            fixedHeader: {
                header: true,
                headerOffset: $('.navbar').height()
            },

            "lengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"] // change per page values here
            ],



            // set the initial value
            "pageLength": 10,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": false,
                "targets": [0]
            }],
            "order": [
                    [0, "asc"]
            ] // set first column as a default sort by asc
        });


        var nEditing = null;
        var nNew = false;

        table.on('click', '.delete', function (e) {
            e.preventDefault();

            if (confirm("Are you sure to delete this row ?") == false) {
                return;
            }

            var nRow = $(this).parents('tr')[0];
            oTable.fnDeleteRow(nRow);
            alert("Deleted! Do not forget to do some ajax to sync with backend :)");
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nEditing = null;
                nNew = false;
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {

            e.preventDefault();

            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveRow(oTable, nEditing);
                if (isSaveComplete == true) {
                    nEditing = null;
                }

            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });
    }

    return {

        //main function to initiate the module
        init: function () {
            handleTable();
        }

    };

}();