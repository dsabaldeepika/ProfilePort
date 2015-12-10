
var appendeAddress = "/Portal/"
var dataService = new function () {
    getVehicleModelTypeByVehicleMakeTypeID = function (vehicleMakeTypeId) {
        var serviceUrl = appendeAddress + "VehicleMakeModelClass/VehicleModelTypeListByVehicleMakeTypeID";
        return $.getJSON(serviceUrl, { id: vehicleMakeTypeId });
    };

    getAllActiveAdjustmentType = function () {
        var serviceUrl = appendeAddress + "AdjustmentRange/GetAllActiveAdjustmentType";
        return $.getJSON(serviceUrl);
    };

    editAdjustmentRange = function (model) {
        var serviceUrl = appendeAddress + "AdjustmentRange/EditAdjustmentRange";
        return $.post(serviceUrl,
                          {
                              AdjustmentRangeID: model.AdjustmentRangeID,
                              MinTerm: model.MinTerm,
                              MaxTerm: model.MaxTerm,
                              AdjustmentDiscount: model.AdjustmentDiscount,
                              AdjustmentTypeID: model.AdjustmentTypeID
                          });
    };

    editPricingBase = function (model) {
        var serviceUrl = appendeAddress + "PricingBase/EditPricingBase";
        return $.post(serviceUrl,
                          {
                              PricingBaseID: model.PricingBaseID,
                              PricingBaseMinBV: model.PricingBaseMinBV,
                              PricingBaseMaxBV: model.PricingBaseMaxBV,
                              PricingBaseMinLTV: model.PricingBaseMinLTV,
                              PricingBaseMaxLTV: model.PricingBaseMaxLTV,
                              PricingBasePricingRateDiscount: model.PricingBasePricingRateDiscount
                          });
    };

    return {
        getVehicleModelTypeByVehicleMakeTypeID: getVehicleModelTypeByVehicleMakeTypeID,
        getAllActiveAdjustmentType: getAllActiveAdjustmentType,
        editAdjustmentRange: editAdjustmentRange,
        editPricingBase: editPricingBase
    };

}();