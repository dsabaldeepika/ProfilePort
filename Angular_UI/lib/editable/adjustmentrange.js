var TableEditable = function () {
    var isSaveComplete = [];
    var adjustmentRangeId = null;
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

            //initialise adjustmentRangeId
            adjustmentRangeId = $(jqTds[6]).children("span").text();
            jqTds[0].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[0].replace(/,/g, "") + '">';
            jqTds[1].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[1].replace(/,/g, "") + '">';
            jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + ((aData[2].replace(/,/g, "").substring(0, aData[2].replace(/,/g, "").length - 1)) / 100).toFixed(4) + '">';
            jqTds[4].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[4] + '" disabled>';
            jqTds[5].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[5] + '" disabled>';
            jqTds[6].innerHTML = '<a class="edit" href="">Save</a>';
            jqTds[7].innerHTML = '<a class="cancel" href="">Cancel</a>';

            //get dropdown data from server
            dataService.getAllActiveAdjustmentType().done(function (data) {
                var items = '<select id="ddlEditAdjDisc" class="form-control input-small">';
                jQuery.each(data, function (i, modelType) {
                    items += "<option value='" + modelType.Value + "'>" + modelType.Text + "</option>";
                });
                items += '</select>';

                // Fill VehicleModelTypeID Dropdown list
                jqTds[3].innerHTML = items;
                $("#ddlEditAdjDisc option").filter(function () {
                    return $(this).text() === aData[3];
                }).prop('selected', true);
            });
        }

        function saveRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            var jqSelects = $('select', nRow);

            var regexDecimalFourPrecision = /^(\d+)(\.\d{1,4})?$/gm;
            var jqInputs = $('input', nRow);

            var elem = $(jqInputs[0]).closest('td');
            if (jqInputs[0].value === '') {
                if (elem.find('.custDecimalValError').length) {
                    elem.find('span.custDecimalValError').remove();
                }
                if (!elem.find('.custValError').length) {
                    elem.append('<span class="custValError error-class">Min term is required</span>');
                }
                isSaveComplete[0] = false;
            } else if (!jqInputs[0].value.match(regexDecimalFourPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Min term must be decimal number.</span>');
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
                    elem.append('<span class="custValError error-class">Max term is required</span>');
                }
                isSaveComplete[1] = false;
            } else if (!jqInputs[1].value.match(regexDecimalFourPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Max term must be decimal number.</span>');
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
                    elem.append('<span class="custValError error-class">Adjustment discount is required</span>');
                }
                isSaveComplete[2] = false;
            } else if (!jqInputs[2].value.match(regexDecimalFourPrecision)) {
                if (elem.find('.custValError').length) {
                    elem.find('span.custValError').remove();
                }
                if (!elem.find('.custDecimalValError').length) {
                    elem.append('<span class="custDecimalValError  error-class">Adjustment discount must be decimal number.</span>');
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

            if (jQuery.inArray(false, isSaveComplete) === -1) {
                //Create AdjustmentRange model to save data using ajax

                var adjustmentRangeModel = {
                    "AdjustmentRangeID": adjustmentRangeId,
                    "MinTerm": jqInputs[0].value,
                    "MaxTerm": jqInputs[1].value,
                    "AdjustmentDiscount": jqInputs[2].value,
                    "AdjustmentTypeID": jqSelects[0].value
                };

                dataService.editAdjustmentRange(adjustmentRangeModel).done(function (data) {
                    oTable.fnUpdate(data.MinTerm, nRow, 0, false).toFixed(6);;
                    oTable.fnUpdate(data.MaxTerm, nRow, 1, false);
                    oTable.fnUpdate(data.AdjustmentDiscount + '%', nRow, 2, false);
                    oTable.fnUpdate(data.AdjustmentTypeDisplayName, nRow, 3, false);
                    oTable.fnUpdate(data.AdjustmentRangeModifiedByID, nRow, 4, false);
                    oTable.fnUpdate(data.AdjustmentRangeModifiedDate, nRow, 5, false);
                    oTable.fnUpdate('<a class="edit" href="">Edit</a><span style="display:none">' + adjustmentRangeId + '</span>', nRow, 6, false);
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

        var table = $('#tblAdjRange');

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