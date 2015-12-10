
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerPortalCRM.ViewModels;
using Com.CoreLane.ScoringEngine.Models;

namespace DealerPortalCRM.DAService
{
    public interface IScore
    {

        #region Methods for AdjustmentType
        Task<List<AdjustmentType>> GetAllAdjustmentTypeAsync();

        Task<AdjustmentType> FindAdjustmentTypeAsync(int? id);

        Task<bool> RemoveAdjustmentTypeAsync(int? id);

        Task<bool> AddAdjustmentTypeAsync(AdjustmentType adjustmenttype);

        Task<bool> ChangeAdjustmentTypeAsync(AdjustmentType adjustmenttype);

        List<AdjustmentType> GetAllActiveAdjustmentType();
        #endregion

        #region Methods for Adjustment

        Task<List<Adjustment>> GetAllAdjustmentAsync();
        Task<List<AdjustmentViewModel>> GetAllAdjustmentViewModelAsync();

        Task<Adjustment> FindAdjustmentAsync(int? id);
        Task<AdjustmentViewModel> FindAdjustmentViewModelAsync(int? id);

        Task<bool> RemoveAdjustmentAsync(int? id);

        Task<bool> AddAdjustmentAsync(Adjustment Adjustment);

        Task<bool> ChangeAdjustmentAsync(Adjustment Adjustment);

        #endregion Methods for Adjustment

        #region Methods for AdjustmentRange

        Task<List<AdjustmentRange>> GetAllAdjustmentRangeAsync();
        Task<List<AdjustmentRangeViewModel>> GetAllAdjustmentRangeViewModelAsync();

        Task<AdjustmentRange> FindAdjustmentRangeAsync(int? id);
        Task<AdjustmentRangeViewModel> FindAdjustmentRangeViewModelAsync(int? id);

        Task<bool> RemoveAdjustmentRangeAsync(int? id);

        Task<bool> AddAdjustmentRangeAsync(AdjustmentRange AdjustmentRange);

        Task<bool> ChangeAdjustmentRangeAsync(AdjustmentRange AdjustmentRange);

        #endregion Methods for Adjustment

        #region Methods for VehicleMakeType

        Task<List<VehicleMakeType>> GetAllVehicleMakeTypeAsync();

        Task<VehicleMakeType> FindVehicleMakeTypeAsync(int? id);

        Task<bool> RemoveVehicleMakeTypeAsync(int? id);

        Task<bool> AddVehicleMakeTypeAsync(VehicleMakeType VehicleMakeType);

        Task<bool> ChangeVehicleMakeTypeAsync(VehicleMakeType VehicleMakeType);

        List<VehicleMakeType> GetAllActiveVehicleMakeType();
        List<VehicleModelType> GetVehicleModelTypeByVehicleMakeTypeID(int id);

        #endregion

        #region Methods for VehicleModelType

        Task<List<VehicleModelType>> GetAllVehicleModelTypeAsync();
        Task<VehicleModelType> FindVehicleModelTypeAsync(int? id);

        Task<List<VehicleModelTypeViewModel>> GetAllVehicleModelTypeViewModelAsync();
        Task<VehicleModelTypeViewModel> FindVehicleModelTypeViewModelAsync(int? id);

        Task<bool> RemoveVehicleModelTypeAsync(int? id);

        Task<bool> AddVehicleModelTypeAsync(VehicleModelType vehiclemodeltype);

        Task<bool> ChangeVehicleModelTypeAsync(VehicleModelType VehicleModelType);

        List<VehicleModelType> GetAllActiveVehicleModelType();
        List<VehicleMakeType> GetAllActiveVehicleModelMakeType();
        List<VehicleModelType> GetVehicleModelTypeForMake(int makeid);
        #endregion

        #region Methods for VehicleClassType

        Task<List<VehicleClassType>> GetAllVehicleClassTypeAsync();

        Task<VehicleClassType> FindVehicleClassTypeAsync(int? id);

        Task<bool> RemoveVehicleClassTypeAsync(int? id);

        Task<bool> AddVehicleClassTypeAsync(VehicleClassType VehicleClassType);

        Task<bool> ChangeVehicleClassTypeAsync(VehicleClassType VehicleClassType);

        List<VehicleClassType> GetAllActiveVehicleClassType();

        List<VehicleClassType> GetAllVehicleClassType();

        #endregion

        #region Methods for VehicleMakeModelClass
        Task<List<VehicleMakeModelClass>> GetAllVehicleMakeModelClassAsync();
        Task<List<VehicleMakeModelClassViewModel>> GetAllVehicleMakeModelClassViewModelAsync();

        Task<VehicleMakeModelClass> FindVehicleMakeModelClassAsync(int? id);
        Task<VehicleMakeModelClassViewModel> FindVehicleMakeModelClassViewModelAsync(int? id);

        Task<bool> RemoveVehicleMakeModelClassAsync(int? id);

        Task<bool> AddVehicleMakeModelClassAsync(VehicleMakeModelClass VehicleMakeModelClass);

        Task<bool> ChangeVehicleMakeModelClassAsync(VehicleMakeModelClass VehicleMakeModelClass);

        //Task<bool> FindVehicleMakeModelClassAsync(int? MakeID, int? ModelID);

        bool FindVehicleMakeModelAsync(int? MakeID, int? ModelID, int? ClassID);

        #endregion Methods for VehicleMakeModelClass

        #region Methods for BuyRate
        Task<List<BuyRate>> GetAllBuyRateAsync();

        Task<BuyRate> FindBuyRateAsync(int? id);

        Task<bool> RemoveBuyRateAsync(int? id);

        Task<bool> AddBuyRateAsync(BuyRate buyrate);

        Task<bool> ChangeBuyRateAsync(BuyRate buyrate);

        #endregion

        #region Methods for ExcessMileage
        Task<List<ExcessMileage>> GetAllExcessMileageAsync();

        Task<List<ExcessMileageViewModel>> GetAllExcessMileageViewModelAsync();

        Task<ExcessMileage> FindExcessMileageAsync(int? id);

        Task<ExcessMileageViewModel> FindExcessMileageViewModelAsync(int? id);

        Task<bool> RemoveExcessMileageAsync(int? id);

        Task<bool> AddExcessMileageAsync(ExcessMileage buyrate);

        Task<bool> ChangeExcessMileageAsync(ExcessMileage buyrate);

        #endregion

        #region Methods for PricingBase
        Task<List<PricingBase>> GetAllPricingBaseAsync();

        Task<List<PricingBaseViewModel>> GetAllPricingBaseViewModelAsync();

        Task<PricingBase> FindPricingBaseAsync(int? id);

        Task<PricingBaseViewModel> FindPricingBaseViewModelAsync(int? id);

        Task<bool> RemovePricingBaseAsync(int? id);

        Task<bool> AddPricingBaseAsync(PricingBase pricingbase);

        Task<bool> ChangePricingBaseAsync(PricingBase pricingbase);

        Task<bool> PricingBaseInLineEditAsync(PricingBase pricebase);

        Task<PricingBaseInLineEditingViewModel> FindPricingBaseInLineEditingViewModelAsync(int? id);
        #endregion

        #region Methods for State Code

        Task<List<StateCode>> GetAllStateCodeAsync();

        Task<StateCode> FindStateCodeAsync(int? id);

        Task<bool> RemoveStateCodeAsync(int? id);

        Task<bool> AddStateCodeAsync(StateCode StateCode);

        Task<bool> ChangeStateCodeAsync(StateCode StateCode);

        List<StateCode> GetAllStateCode();

        #endregion

        #region Methods for Dealer Score

        Task<List<DealerScore>> GetAllDealerScoreAsync();

        Task<List<DealerScoreViewModel>> GetAllDealerScoreViewModelAsync();

        Task<DealerScore> FindDealerScoreAsync(int? id);

        Task<DealerScoreViewModel> FindDealerScoreViewModelAsync(int? id);

        Task<bool> RemoveDealerScoreAsync(int? id);

        Task<bool> AddDealerScoreAsync(DealerScore DealerScore);

        Task<bool> ChangeDealerScoreAsync(DealerScore DealerScore);

        #endregion

        #region Methods for DocFee

        Task<List<DocFee>> GetAllDocFeeAsync();

        Task<DocFee> FindDocFeeAsync(int? id);

        Task<bool> RemoveDocFeeAsync(int? id);

        Task<bool> AddDocFeeAsync(DocFee DocFee);

        Task<bool> ChangeDocFeeAsync(DocFee DocFee);

        List<DocFee> GetAllDocFee();


        #endregion Methods for DocFee

        #region Methods for StateAdjustment

        Task<List<StateAdjustment>> GetAllStateAdjustmentAsync();
        Task<List<StateAdjustmentViewModel>> GetAllStateAdjustmentViewModelAsync();

        Task<StateAdjustment> FindStateAdjustmentAsync(int? id);

        Task<StateAdjustmentViewModel> FindStateAdjustmentViewModelAsync(int? id);

        Task<bool> RemoveStateAdjustmentAsync(int? id);

        Task<bool> AddStateAdjustmentAsync(StateAdjustment StateAdjustment);

        Task<bool> ChangeStateAdjustmentAsync(StateAdjustment StateAdjustment);

        Task<bool> AdjustmentRangeInLineEditAsync(AdjustmentRange AdjustmentRange);

        Task<AdjustmentRangeInLineEditingViewModel> FindAdjustmentRangeInLineEditingViewModelAsync(int? id);

        #endregion Methods for StateAdjustment

        #region Methods for StateFicoRange

        Task<List<StateFicoRange>> GetAllStateFicoRangeAsync();

        Task<List<StateFICORangeViewModel>> GetAllStateFicoRangeViewModelAsync();

        Task<StateFicoRange> FindStateFicoRangeAsync(int? id);

        Task<StateFICORangeViewModel> FindStateFicoRangeViewModelAsync(int? id);

        Task<bool> RemoveStateFicoRangeAsync(int? id);

        Task<bool> AddStateFicoRangeAsync(StateFicoRange StateFicoRange);

        Task<bool> ChangeStateFicoRangeAsync(StateFicoRange StateFicoRange);

        #endregion Methods for StateFicoRange

        #region Methods for DealerDiscount

        Task<List<DealerDiscount>> GetAllDealerDiscountAsync();
        Task<List<DealerDiscountViewModel>> GetAllDealerDiscountViewModelAsync();

        Task<DealerDiscount> FindDealerDiscountAsync(int? id);

        Task<DealerDiscountViewModel> FindDealerDiscountViewModelAsync(int? id);

        Task<bool> RemoveDealerDiscountAsync(int? id);

        Task<bool> AddDealerDiscountAsync(DealerDiscount DealerDiscount);

        Task<bool> ChangeDealerDiscountAsync(DealerDiscount DealerDiscount);

        #endregion Methods for DealerDiscount

        #region Methods for Dealer Type

        Task<List<DealerType>> GetAllDealerTypeAsync();

        Task<DealerType> FindDealerTypeAsync(int? id);

        List<DealerType> GetAllDealerType();

        #endregion

        #region Methods for ClassCodeAdj

        Task<List<ClassCodeAdj>> GetAllClassCodeAdjAsync();
        Task<List<ClassCodeAdjViewModel>> GetAllClassCodeAdjViewModelAsync();

        Task<ClassCodeAdj> FindClassCodeAdjAsync(int? id);
        Task<ClassCodeAdjViewModel> FindClassCodeAdjViewModelAsync(int? id);

        Task<bool> RemoveClassCodeAdjAsync(int? id);

        Task<bool> AddClassCodeAdjAsync(ClassCodeAdj ClassCodeAdj);

        Task<bool> ChangeClassCodeAdjAsync(ClassCodeAdj ClassCodeAdj);

        #endregion Methods for ClassCodeAdj

        #region Methods for TermCap

        Task<List<TermCap>> GetAllTermCapAsync();

        Task<List<TermCapViewModel>> GetAllTermCapViewModelAsync();

        Task<TermCap> FindTermCapAsync(int? id);

        Task<TermCapViewModel> FindTermCapViewModelAsync(int? id);

        Task<bool> RemoveTermCapAsync(int? id);

        Task<bool> AddTermCapAsync(TermCap TermCap);

        Task<bool> ChangeTermCapAsync(TermCap TermCap);

        #endregion Methods for TermCap

        #region Methods user validation

          int GetUserID(string username);
         
        #endregion
    }
}
