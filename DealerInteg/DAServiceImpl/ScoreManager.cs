using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Com.CoreLane.ScoringEngine.Models;
using DealerPortalCRM.DAService;
using DealerPortalCRM.ViewModels;

namespace DealerPortalCRM.DAServiceImpl
{
    public class ScoreManager : IScore
    {
        private readonly ScoringEngineEntities _dbContext;

        public ScoreManager(ScoringEngineEntities db)
        {
            _dbContext = db;
        }

        #region Methods for User validation

        public int GetUserID(string username)
        {
            ISecurity security = new SecurityManager(_dbContext);
            int userID;

            try
            {
                userID = security.getUserID(username);

                if (userID == 0)
                    if (userID == 0)
                        userID = 12;

                //throw new Exception("No user found!");
            }
            catch (Exception ex)
            {
                throw;
            }
            return userID;
        }

        #endregion

        #region Methods for AdjustmentType

        public async Task<List<AdjustmentType>> GetAllAdjustmentTypeAsync()
        {
            return await _dbContext.AdjustmentType.ToListAsync();
        }

        public async Task<AdjustmentType> FindAdjustmentTypeAsync(int? id)
        {
            var adjustment = await _dbContext.AdjustmentType.FindAsync(id);
            return adjustment;
        }

        public async Task<bool> RemoveAdjustmentTypeAsync(int? id)
        {
            var adjustmenttype = await FindAdjustmentTypeAsync(id);
            _dbContext.AdjustmentType.Remove(adjustmenttype);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddAdjustmentTypeAsync(AdjustmentType adjustmenttype)
        {
            _dbContext.AdjustmentType.Add(adjustmenttype);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeAdjustmentTypeAsync(AdjustmentType adjustmenttype)
        {
            _dbContext.Entry(adjustmenttype).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<AdjustmentType> GetAllActiveAdjustmentType()
        {
            var adjustmentTypeQuery = from at in _dbContext.AdjustmentType
                where at.IsActive
                select at;

            var resultList = new List<AdjustmentType>();
            resultList = adjustmentTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for Adjustment

        public async Task<List<Adjustment>> GetAllAdjustmentAsync()
        {
            return await _dbContext.Adjustment.ToListAsync();
        }

        public async Task<List<AdjustmentViewModel>> GetAllAdjustmentViewModelAsync()
        {
            var adjustmentQuery = from a in _dbContext.Adjustment
                join at in _dbContext.AdjustmentType on a.AdjustmentType.ID equals at.ID
                select new AdjustmentViewModel
                {
                    AdjustmentTypeID = at.ID,
                    AdjustmentTypeType = at.Type,
                    AdjustmentTypeDisplayName = at.DisplayName,
                    AdjustmentID = a.ID,
                    AdjustmentNumber = a.AdjustmentNumber,
                    AdjustmentDiscount = a.AdjustmentDiscount*100,
                    AdjustmentCreatedByID = a.CreatedByID,
                    AdjustmentModifiedByID = a.ModifiedByID,
                    AdjustmentCreatedDate = a.CreatedDate,
                    AdjustmentModifiedDate = a.ModifiedDate,
                    AdjustmentIsActive = a.IsActive
                };
            return await adjustmentQuery.ToListAsync();
        }

        public async Task<Adjustment> FindAdjustmentAsync(int? id)
        {
            return await _dbContext.Adjustment.FindAsync(id);
        }

        public async Task<AdjustmentViewModel> FindAdjustmentViewModelAsync(int? id)
        {
            var adjustmentQuery = from a in _dbContext.Adjustment
                join at in _dbContext.AdjustmentType on a.AdjustmentType.ID equals at.ID
                where a.ID == id
                select new AdjustmentViewModel
                {
                    AdjustmentTypeID = at.ID,
                    AdjustmentTypeType = at.Type,
                    AdjustmentTypeDisplayName = at.DisplayName,
                    AdjustmentID = a.ID,
                    AdjustmentNumber = a.AdjustmentNumber,
                    AdjustmentDiscount = a.AdjustmentDiscount,
                    AdjustmentCreatedByID = a.CreatedByID,
                    AdjustmentModifiedByID = a.ModifiedByID,
                    AdjustmentCreatedDate = a.CreatedDate,
                    AdjustmentModifiedDate = a.ModifiedDate,
                    AdjustmentIsActive = a.IsActive
                };

            return await adjustmentQuery.FirstAsync();
        }

        public async Task<bool> RemoveAdjustmentAsync(int? id)
        {
            var adjustment = await FindAdjustmentAsync(id);
            _dbContext.Adjustment.Remove(adjustment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddAdjustmentAsync(Adjustment adjustment)
        {
            _dbContext.Adjustment.Add(adjustment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeAdjustmentAsync(Adjustment adjustment)
        {
            _dbContext.Entry(adjustment).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.Adjustment SET AdjustmentType_ID = " + adjustment.AdjustmentType_ID + " WHERE ID = " + adjustment.ID );
            return true;
        }

        #endregion

        #region Method For AdjustmentRange

        public async Task<List<AdjustmentRange>> GetAllAdjustmentRangeAsync()
        {
            return await _dbContext.AdjustmentRange.ToListAsync();
        }

        public async Task<List<AdjustmentRangeViewModel>> GetAllAdjustmentRangeViewModelAsync()
        {
            var adjustmentRangeQuery = from ar in _dbContext.AdjustmentRange
                join at in _dbContext.AdjustmentType on ar.AdjustmentType.ID equals at.ID
                select new AdjustmentRangeViewModel
                {
                    AdjustmentTypeID = at.ID,
                    AdjustmentTypeType = at.Type,
                    AdjustmentTypeDisplayName = at.DisplayName,
                    AdjustmentRangeID = ar.ID,
                    MinTerm = ar.MinTerm,
                    MaxTerm = ar.MaxTerm,
                    AdjustmentDiscount = ar.AdjustmentDiscount*100,
                    AdjustmentRangeCreatedByID = ar.CreatedByID,
                    AdjustmentRangeModifiedByID = ar.ModifiedByID,
                    AdjustmentRangeCreatedDate = ar.CreatedDate,
                    AdjustmentRangeModifiedDate = ar.ModifiedDate
                };
            return await adjustmentRangeQuery.ToListAsync();
        }

        public async Task<AdjustmentRange> FindAdjustmentRangeAsync(int? id)
        {
            return await _dbContext.AdjustmentRange.FindAsync(id);
        }

        public async Task<AdjustmentRangeViewModel> FindAdjustmentRangeViewModelAsync(int? id)
        {
            var adjustmentRangeQuery = from ar in _dbContext.AdjustmentRange
                join at in _dbContext.AdjustmentType on ar.AdjustmentType.ID equals at.ID
                where ar.ID == id
                select new AdjustmentRangeViewModel
                {
                    AdjustmentTypeID = at.ID,
                    AdjustmentTypeType = at.Type,
                    AdjustmentTypeDisplayName = at.DisplayName,
                    AdjustmentRangeID = ar.ID,
                    MinTerm = ar.MinTerm,
                    MaxTerm = ar.MaxTerm,
                    AdjustmentDiscount = ar.AdjustmentDiscount,
                    AdjustmentRangeCreatedByID = ar.CreatedByID,
                    AdjustmentRangeModifiedByID = ar.ModifiedByID,
                    AdjustmentRangeCreatedDate = ar.CreatedDate,
                    AdjustmentRangeModifiedDate = ar.ModifiedDate
                };

            return await adjustmentRangeQuery.FirstAsync();
        }

        public async Task<AdjustmentRangeInLineEditingViewModel> FindAdjustmentRangeInLineEditingViewModelAsync(int? id)
        {
            var query = await (from ar in _dbContext.AdjustmentRange
                join at in _dbContext.AdjustmentType on ar.AdjustmentType.ID equals at.ID
                where ar.ID == id
                select new
                {
                    AdjustmentTypeDisplayName = at.DisplayName,
                    AdjustmentRangeID = ar.ID,
                    ar.MinTerm,
                    ar.MaxTerm,
                    ar.AdjustmentDiscount,
                    AdjustmentRangeModifiedByID = ar.ModifiedByID,
                    AdjustmentRangeModifiedDate = ar.ModifiedDate
                }).FirstAsync();


            return new AdjustmentRangeInLineEditingViewModel
            {
                AdjustmentTypeDisplayName = query.AdjustmentTypeDisplayName,
                AdjustmentRangeID = query.AdjustmentRangeID,
                MinTerm = string.Format("{0:N4}", query.MinTerm),
                MaxTerm = string.Format("{0:N4}", query.MaxTerm),
                AdjustmentDiscount = string.Format("{0:N4}", query.AdjustmentDiscount*100),
                AdjustmentRangeModifiedByID = query.AdjustmentRangeModifiedByID,
                AdjustmentRangeModifiedDate = query.AdjustmentRangeModifiedDate.ToString()
            };
        }


        public Task<bool> RemoveAdjustmentRangeAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAdjustmentRangeAsync(AdjustmentRange adjustmentRange)
        {
            _dbContext.AdjustmentRange.Add(adjustmentRange);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeAdjustmentRangeAsync(AdjustmentRange adjustmentRange)
        {
            _dbContext.Entry(adjustmentRange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.AdjustmentRange SET AdjustmentType_ID = " +
            //                                      adjustmentRange.AdjustmentType.ID + " WHERE ID = " +
            //                                      adjustmentRange.ID);
            return true;
        }

        public async Task<bool> AdjustmentRangeInLineEditAsync(AdjustmentRange adjustmentRange)
        {
            var adjRange = _dbContext.AdjustmentRange.First(ar => ar.ID == adjustmentRange.ID);
            adjRange.MinTerm = adjustmentRange.MinTerm;
            adjRange.MaxTerm = adjustmentRange.MaxTerm;
            adjRange.AdjustmentDiscount = adjustmentRange.AdjustmentDiscount;
            adjRange.AdjustmentType = adjustmentRange.AdjustmentType;
            adjRange.ModifiedByID = adjustmentRange.ModifiedByID;
            adjRange.ModifiedDate = adjustmentRange.ModifiedDate;
            await _dbContext.SaveChangesAsync();

            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.AdjustmentRange SET AdjustmentType_ID = " +
            //                                      adjustmentRange.AdjustmentType.ID + " WHERE ID = " +
            //                                      adjustmentRange.ID);
            return true;
        }

        #endregion

        #region Methods for VehicleMakeType Methods

        public async Task<List<VehicleMakeType>> GetAllVehicleMakeTypeAsync()
        {
            return await _dbContext.VehicleMakeType.ToListAsync();
        }

        public async Task<VehicleMakeType> FindVehicleMakeTypeAsync(int? id)
        {
            return await _dbContext.VehicleMakeType.FindAsync(id);
        }

        public async Task<bool> RemoveVehicleMakeTypeAsync(int? id)
        {
            var vehiclemaketype = await FindVehicleMakeTypeAsync(id);
            _dbContext.VehicleMakeType.Remove(vehiclemaketype);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddVehicleMakeTypeAsync(VehicleMakeType vehiclemaketype)
        {
            _dbContext.VehicleMakeType.Add(vehiclemaketype);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeVehicleMakeTypeAsync(VehicleMakeType vehiclemaketype)
        {
            _dbContext.Entry(vehiclemaketype).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<VehicleMakeType> GetAllActiveVehicleMakeType()
        {
            var vehicleMakeTypeQuery = from at in _dbContext.VehicleMakeType
                select at;

            var resultList = new List<VehicleMakeType>();
            resultList = vehicleMakeTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for VehicleModelType Methods

        public async Task<List<VehicleModelType>> GetAllVehicleModelTypeAsync()
        {
            return await _dbContext.VehicleModelType.ToListAsync();
        }

        public async Task<VehicleModelType> FindVehicleModelTypeAsync(int? id)
        {
            return await _dbContext.VehicleModelType.FindAsync(id);
        }

        public async Task<List<VehicleModelTypeViewModel>> GetAllVehicleModelTypeViewModelAsync()
        {
            var VehicleModelTypeQueryAll = from a in _dbContext.VehicleModelType
                join at in _dbContext.VehicleMakeType on a.VehicleMakeType.ID equals at.ID
                select new VehicleModelTypeViewModel
                {
                    VehiclModelTypeID = a.ID,
                    VehicleModelTypeName = a.Name,
                    VehicleMakeTypeDisplayName = at.Name,
                    VehicleModelTypeCreatedByID = a.CreatedByID,
                    VehicleModelTypeModifiedByID = a.ModifiedByID,
                    VehicleModelTypeCreatedDate = a.CreatedDate,
                    VehicleModelTypeModifiedDate = a.ModifiedDate
                };

            return await VehicleModelTypeQueryAll.ToListAsync();
        }

        //-------------------

        public async Task<VehicleModelTypeViewModel> FindVehicleModelTypeViewModelAsync(int? id)
        {
            var VehicleModelTypeQuery = from a in _dbContext.VehicleModelType
                join at in _dbContext.VehicleMakeType on a.VehicleMakeType.ID equals at.ID
                where a.ID == id
                select new VehicleModelTypeViewModel
                {
                    VehiclModelTypeID = a.ID,
                    VehicleModelTypeName = a.Name,
                    VehicleMakeTypeID = a.VehicleMakeType.ID,
                    VehicleMakeTypeDisplayName = at.Name,
                    VehicleModelTypeCreatedByID = a.CreatedByID,
                    VehicleModelTypeModifiedByID = a.ModifiedByID,
                    VehicleModelTypeCreatedDate = a.CreatedDate,
                    VehicleModelTypeModifiedDate = a.ModifiedDate
                };

            return await VehicleModelTypeQuery.FirstAsync();
        }

        //--------------------
        public async Task<bool> RemoveVehicleModelTypeAsync(int? id)
        {
            var vehiclemodeltype = await FindVehicleModelTypeAsync(id);

            _dbContext.VehicleModelType.Remove(vehiclemodeltype);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AddVehicleModelTypeAsync(VehicleModelType vehiclemodeltype)
        {
            _dbContext.VehicleModelType.Add(vehiclemodeltype);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeVehicleModelTypeAsync(VehicleModelType vehiclemodeltype)
        {
            _dbContext.Entry(vehiclemodeltype).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            //var sqlstmt = "Update ScoringEngine.VehicleModelType SET VehicleMakeType_ID = " +
            //              vehiclemodeltype.VehicleMakeType.ID + " WHERE ID = " + vehiclemodeltype.ID;
            //_dbContext.Database.ExecuteSqlCommand(sqlstmt);
            return true;
        }

        public List<VehicleModelType> GetAllActiveVehicleModelType()
        {
            var vehicleModelTypeQuery = from at in _dbContext.VehicleModelType
                select at;

            var resultList = new List<VehicleModelType>();
            resultList = vehicleModelTypeQuery.ToList();
            return resultList;
        }

        public List<VehicleMakeType> GetAllActiveVehicleModelMakeType()
        {
            var vehicleMakeTypeQuery = from at in _dbContext.VehicleMakeType
                select at;

            var resultList = new List<VehicleMakeType>();
            resultList = vehicleMakeTypeQuery.ToList();
            return resultList;
        }

        public List<VehicleModelType> GetVehicleModelTypeForMake(int makeid)
        {
            var vehicleModelTypeQuery = from at in _dbContext.VehicleModelType
                where at.VehicleMakeType.ID == makeid
                select at;

            var resultList = new List<VehicleModelType>();
            resultList = vehicleModelTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for VehicleClassType Methods

        public async Task<List<VehicleClassType>> GetAllVehicleClassTypeAsync()
        {
            return await _dbContext.VehicleClassType.ToListAsync();
        }

        public async Task<VehicleClassType> FindVehicleClassTypeAsync(int? id)
        {
            return await _dbContext.VehicleClassType.FindAsync(id);
        }

        public async Task<bool> RemoveVehicleClassTypeAsync(int? id)
        {
            var VehicleClassType = await FindVehicleClassTypeAsync(id);
            _dbContext.VehicleClassType.Remove(VehicleClassType);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddVehicleClassTypeAsync(VehicleClassType VehicleClassType)
        {
            _dbContext.VehicleClassType.Add(VehicleClassType);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeVehicleClassTypeAsync(VehicleClassType VehicleClassType)
        {
            _dbContext.Entry(VehicleClassType).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<VehicleClassType> GetAllActiveVehicleClassType()
        {
            var vehicleClassTypeQuery = from at in _dbContext.VehicleClassType
                select at;

            var resultList = new List<VehicleClassType>();
            resultList = vehicleClassTypeQuery.ToList();
            return resultList;
        }

        public List<VehicleClassType> GetAllVehicleClassType()
        {
            var VehicleClassTypeQuery = from at in _dbContext.VehicleClassType
                select at;

            var resultList = new List<VehicleClassType>();
            resultList = VehicleClassTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for VehicleMakeModelClass

        public async Task<List<VehicleMakeModelClass>> GetAllVehicleMakeModelClassAsync()
        {
            return await _dbContext.VehicleMakeModelClass.ToListAsync();
        }

        public async Task<List<VehicleMakeModelClassViewModel>> GetAllVehicleMakeModelClassViewModelAsync()
        {
            var VehicleMakeModelClassQuery = from a in _dbContext.VehicleMakeModelClass
                join at in _dbContext.VehicleMakeType on a.VehicleMakeType.ID equals at.ID
                join bt in _dbContext.VehicleModelType on a.VehicleModelType.ID equals bt.ID
                join ct in _dbContext.VehicleClassType on a.VehicleClassType.ID equals ct.ID
                select new VehicleMakeModelClassViewModel
                {
                    VehicleMakeModelClassID = a.ID,
                    VehicleMakeTypeDisplayName = at.Name,
                    VehicleModelTypeDisplayName = bt.Name,
                    VehicleClassTypeDisplayName = ct.Name,
                    VehicleMakeModelClassCreatedByID = a.CreatedByID,
                    VehicleMakeModelClassModifiedByID = a.ModifiedByID,
                    VehicleMakeModelClassCreatedDate = a.CreatedDate,
                    VehicleMakeModelClassModifiedDate = a.ModifiedDate
                };

            return await VehicleMakeModelClassQuery.ToListAsync();
        }

        public List<VehicleModelType> GetVehicleModelTypeByVehicleMakeTypeID(int id)
        {
            var vehicleModelTypeQuery = from at in _dbContext.VehicleModelType
                where at.VehicleMakeType.ID == id
                select at;
            return vehicleModelTypeQuery.ToList();
        }

        public async Task<VehicleMakeModelClass> FindVehicleMakeModelClassAsync(int? id)
        {
            return await _dbContext.VehicleMakeModelClass.FindAsync(id);
        }

        public async Task<VehicleMakeModelClassViewModel> FindVehicleMakeModelClassViewModelAsync(int? id)
        {
            var VehicleMakeModelClassQuery = from a in _dbContext.VehicleMakeModelClass
                join at in _dbContext.VehicleMakeType on a.VehicleMakeType.ID equals at.ID
                join bt in _dbContext.VehicleModelType on a.VehicleModelType.ID equals bt.ID
                join ct in _dbContext.VehicleClassType on a.VehicleClassType.ID equals ct.ID
                where a.ID == id
                select new VehicleMakeModelClassViewModel
                {
                    VehicleMakeModelClassID = a.ID,
                    VehicleMakeTypeID = at.ID,
                    VehicleModelTypeID = bt.ID,
                    VehicleClassTypeID = ct.ID,
                    VehicleMakeTypeDisplayName = at.Name,
                    VehicleModelTypeDisplayName = bt.Name,
                    VehicleClassTypeDisplayName = ct.Name,
                    VehicleMakeModelClassCreatedByID = a.CreatedByID,
                    VehicleMakeModelClassModifiedByID = a.ModifiedByID,
                    VehicleMakeModelClassCreatedDate = a.CreatedDate,
                    VehicleMakeModelClassModifiedDate = a.ModifiedDate
                };

            return await VehicleMakeModelClassQuery.FirstAsync();
        }

        public async Task<bool> RemoveVehicleMakeModelClassAsync(int? id)
        {
            var vehicleMakeModelClass = await FindVehicleMakeModelClassAsync(id);
            _dbContext.VehicleMakeModelClass.Remove(vehicleMakeModelClass);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddVehicleMakeModelClassAsync(VehicleMakeModelClass vehicleMakeModelClass)
        {
            _dbContext.VehicleMakeModelClass.Add(vehicleMakeModelClass);
            await _dbContext.SaveChangesAsync();

            //var sqlstmt =
            //    "Insert into ScoringEngine.Vehicle (Name, CreatedByID, CreatedDate, ModifiedByID, ModifiedDate, Organization_ID, VehicleMakeModelClass_ID) values ('" +
            //    "Vehicle Info" + "', 1,'" + DateTime.Now.ToShortDateString() + "',1,'" +
            //    DateTime.Now.ToShortDateString() + "',1, " + vehicleMakeModelClass.ID + ")";
            //_dbContext.Database.ExecuteSqlCommand(sqlstmt);

            return true;
        }

        public async Task<bool> ChangeVehicleMakeModelClassAsync(VehicleMakeModelClass vehicleMakeModelClass)
        {
            _dbContext.Entry(vehicleMakeModelClass).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();


            return true;
        }

        public bool FindVehicleMakeModelAsync(int? MakeID, int? ModelID, int? ClassID)
        {
            var VehicleMakeModelQuery = from a in _dbContext.VehicleMakeModelClass
                where a.VehicleMakeType.ID == MakeID
                      && a.VehicleModelType.ID == ModelID
                      && a.VehicleClassType.ID == ClassID
                select a;

            if (VehicleMakeModelQuery.ToList().Count > 0)
                return true;
            return false;
        }

        #endregion

        #region Methods for BuyRate

        public async Task<List<BuyRate>> GetAllBuyRateAsync()
        {
            return await _dbContext.BuyRate.ToListAsync();
        }

        public async Task<BuyRate> FindBuyRateAsync(int? id)
        {
            return await _dbContext.BuyRate.FindAsync(id);
        }

        public async Task<bool> RemoveBuyRateAsync(int? id)
        {
            var buyRate = await FindBuyRateAsync(id);
            _dbContext.BuyRate.Remove(buyRate);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddBuyRateAsync(BuyRate buyrate)
        {
            _dbContext.BuyRate.Add(buyrate);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeBuyRateAsync(BuyRate buyrate)
        {
            _dbContext.Entry(buyrate).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<BuyRate> GetAllActiveBuyRate()
        {
            var adjustmentTypeQuery = from at in _dbContext.BuyRate
                where at.IsActive == true
                select at;

            var resultList = new List<BuyRate>();
            resultList = adjustmentTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for ExcessMileage

        public async Task<List<ExcessMileage>> GetAllExcessMileageAsync()
        {
            return await _dbContext.ExcessMileage.ToListAsync();
        }

        public async Task<List<ExcessMileageViewModel>> GetAllExcessMileageViewModelAsync()
        {
            var ExcessMileageQuery = from a in _dbContext.ExcessMileage
                select new ExcessMileageViewModel
                {
                    ExcessMileageID = a.ID,
                    ExcessMileageMinMileage = a.MinMileage,
                    ExcessMileageMaxMileage = a.MaxMileage,
                    ExcessMileageLTVAdj = a.LTVAdj*100,
                    ExcessMileageDiscAdj = a.DiscAdj*100,
                    ExcessMileageIsActive = a.IsActive,
                    ExcessMileageTermCap = a.TermCap,
                    ExcessMileageCreatedByID = a.CreatedByID,
                    ExcessMileageCreatedDate = a.CreatedDate,
                    ExcessMileageModifiedByID = a.ModifiedByID,
                    ExcessMileageModifiedDate = a.ModifiedDate
                };
            return await ExcessMileageQuery.ToListAsync();
        }


        public async Task<ExcessMileage> FindExcessMileageAsync(int? id)
        {
            return await _dbContext.ExcessMileage.FindAsync(id);
        }


        public async Task<ExcessMileageViewModel> FindExcessMileageViewModelAsync(int? id)
        {
            var ExcessMileageQuery = from a in _dbContext.ExcessMileage
                where a.ID == id
                select new ExcessMileageViewModel
                {
                    ExcessMileageID = a.ID,
                    ExcessMileageMinMileage = a.MinMileage,
                    ExcessMileageMaxMileage = a.MaxMileage,
                    ExcessMileageLTVAdj = a.LTVAdj,
                    ExcessMileageDiscAdj = a.DiscAdj,
                    ExcessMileageIsActive = a.IsActive,
                    ExcessMileageTermCap = a.TermCap,
                    ExcessMileageCreatedByID = a.CreatedByID,
                    ExcessMileageCreatedDate = a.CreatedDate,
                    ExcessMileageModifiedByID = a.ModifiedByID,
                    ExcessMileageModifiedDate = a.ModifiedDate
                };

            return await ExcessMileageQuery.FirstAsync();
        }

        public async Task<bool> RemoveExcessMileageAsync(int? id)
        {
            var buyRate = await FindExcessMileageAsync(id);
            _dbContext.ExcessMileage.Remove(buyRate);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddExcessMileageAsync(ExcessMileage excessmileage)
        {
            _dbContext.ExcessMileage.Add(excessmileage);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeExcessMileageAsync(ExcessMileage excessmileage)
        {
            _dbContext.Entry(excessmileage).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<ExcessMileage> GetAllActiveExcessMileage()
        {
            var adjustmentTypeQuery = from at in _dbContext.ExcessMileage
                where at.IsActive
                select at;

            var resultList = new List<ExcessMileage>();
            resultList = adjustmentTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for PricingBase

        public async Task<List<PricingBase>> GetAllPricingBaseAsync()
        {
            return await _dbContext.PricingBase.ToListAsync();
        }

        public async Task<List<PricingBaseViewModel>> GetAllPricingBaseViewModelAsync()
        {
            var PricingBaseQuery = from a in _dbContext.PricingBase
                select new PricingBaseViewModel
                {
                    PricingBaseID = a.ID,
                    PricingBaseMinBV = (decimal) a.MinBV,
                    PricingBaseMaxBV = (decimal) a.MaxBV,
                    PricingBaseMinLTV = (decimal) a.MinLTV,
                    PricingBaseMaxLTV = (decimal) a.MaxLTV,
                    PricingBasePricingRateDiscount = a.PricingRateDiscount, //initially it was PricingRateDiscount*100
                    PricingBaseIsActive = a.IsActive,
                    PricingBaseCreatedByID = a.CreatedByID,
                    PricingBaseModifiedByID = a.ModifiedByID,
                    PricingBaseCreatedDate = a.CreatedDate,
                    PricingBaseModifiedDate = a.ModifiedDate
                };
            return await PricingBaseQuery.ToListAsync();
        }

        public async Task<PricingBase> FindPricingBaseAsync(int? id)
        {
            return await _dbContext.PricingBase.FindAsync(id);
        }

        public async Task<PricingBaseViewModel> FindPricingBaseViewModelAsync(int? id)
        {
            var PricingBaseQuery = from a in _dbContext.PricingBase
                where a.ID == id
                select new PricingBaseViewModel
                {
                    PricingBaseID = a.ID,
                    PricingBaseMinBV = (decimal) a.MinBV,
                    PricingBaseMaxBV = (decimal) a.MaxBV,
                    PricingBaseMinLTV = (decimal) a.MinLTV,
                    PricingBaseMaxLTV = (decimal) a.MaxLTV,
                    PricingBasePricingRateDiscount = a.PricingRateDiscount,
                    PricingBaseIsActive = a.IsActive,
                    PricingBaseCreatedByID = a.CreatedByID,
                    PricingBaseModifiedByID = a.ModifiedByID,
                    PricingBaseCreatedDate = a.CreatedDate,
                    PricingBaseModifiedDate = a.ModifiedDate
                };

            return await PricingBaseQuery.FirstAsync();
        }


        public async Task<PricingBaseInLineEditingViewModel> FindPricingBaseInLineEditingViewModelAsync(int? id)
        {
            var query = await (from p in _dbContext.PricingBase
                where p.ID == id
                select new
                {
                    minBV = p.MinBV,
                    maxBV = p.MaxBV,
                    minLTV = p.MinLTV,
                    maxLTV = p.MaxLTV,
                    PriceingDiscount = p.PricingRateDiscount,
                    p.IsActive
                }).FirstAsync();


            return new PricingBaseInLineEditingViewModel
            {
                PricingBaseMinBV = string.Format("{0:C0}", query.minBV),
                PricingBaseMaxBV = string.Format("{0:C0}", query.maxBV),
                PricingBaseMinLTV = string.Format("{0:n2}", query.minLTV),
                PricingBaseMaxLTV = string.Format("{0:n2}", query.maxLTV),
                PricingBasePricingRateDiscount = string.Format("{0:n6}", query.PriceingDiscount),
                IsActive = query.IsActive
            };
        }

        public async Task<bool> RemovePricingBaseAsync(int? id)
        {
            var buyRate = await FindPricingBaseAsync(id);
            _dbContext.PricingBase.Remove(buyRate);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPricingBaseAsync(PricingBase pricingbase)
        {
            _dbContext.PricingBase.Add(pricingbase);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePricingBaseAsync(PricingBase pricingbase)
        {
            _dbContext.Entry(pricingbase).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PricingBaseInLineEditAsync(PricingBase pricingBase)
        {
            var prBase = _dbContext.PricingBase.First(p => p.ID == pricingBase.ID);
            prBase.MinBV = pricingBase.MinBV;
            prBase.MaxBV = pricingBase.MaxBV;
            prBase.MinLTV = pricingBase.MinLTV;
            prBase.MaxLTV = pricingBase.MaxLTV;
            prBase.PricingRateDiscount = pricingBase.PricingRateDiscount;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public List<PricingBase> GetAllActivePricingBase()
        {
            var adjustmentTypeQuery = from at in _dbContext.PricingBase
                where at.IsActive
                select at;

            var resultList = new List<PricingBase>();
            resultList = adjustmentTypeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for StateCode Methods

        public async Task<List<StateCode>> GetAllStateCodeAsync()
        {
            return await _dbContext.StateCode.ToListAsync();
        }

        public async Task<StateCode> FindStateCodeAsync(int? id)
        {
            return await _dbContext.StateCode.FindAsync(id);
        }

        public async Task<bool> RemoveStateCodeAsync(int? id)
        {
            var StateCode = await FindStateCodeAsync(id);
            _dbContext.StateCode.Remove(StateCode);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStateCodeAsync(StateCode StateCode)
        {
            _dbContext.StateCode.Add(StateCode);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStateCodeAsync(StateCode StateCode)
        {
            _dbContext.Entry(StateCode).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<StateCode> GetAllStateCode()
        {
            var StateCodeQuery = from at in _dbContext.StateCode
                select at;

            var resultList = new List<StateCode>();
            resultList = StateCodeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for DealerScore Methods

        public async Task<List<DealerScore>> GetAllDealerScoreAsync()
        {
            return await _dbContext.DealerScore.ToListAsync();
        }

        public async Task<List<DealerScoreViewModel>> GetAllDealerScoreViewModelAsync()
        {
            var DealerScoreQuery = from a in _dbContext.DealerScore
                select new DealerScoreViewModel
                {
                    DealerScoreID = a.ID,
                    DealerScoreScore = a.Score,
                    DealerScoreDisAjd = a.DisAjd*100,
                    DealerScoreLTVCap = a.LTVCap*100,
                    DealerScoreMinDisc = a.MinDisc*100,
                    DealerScoreCreatedByID = a.CreatedByID,
                    DealerScoreModifiedByID = a.ModifiedByID,
                    DealerScoreCreatedDate = a.CreatedDate,
                    DealerScoreModifiedDate = a.ModifiedDate
                };
            return await DealerScoreQuery.ToListAsync();
        }

        public async Task<DealerScore> FindDealerScoreAsync(int? id)
        {
            return await _dbContext.DealerScore.FindAsync(id);
        }

        public async Task<DealerScoreViewModel> FindDealerScoreViewModelAsync(int? id)
        {
            var DealerScoreQuery = from a in _dbContext.DealerScore
                where a.ID == id
                select new DealerScoreViewModel
                {
                    DealerScoreID = a.ID,
                    DealerScoreScore = a.Score,
                    DealerScoreDisAjd = a.DisAjd,
                    DealerScoreLTVCap = a.LTVCap,
                    DealerScoreMinDisc = a.MinDisc,
                    DealerScoreCreatedByID = a.CreatedByID,
                    DealerScoreModifiedByID = a.ModifiedByID,
                    DealerScoreCreatedDate = a.CreatedDate,
                    DealerScoreModifiedDate = a.ModifiedDate
                };

            return await DealerScoreQuery.FirstAsync();
        }

        public async Task<bool> RemoveDealerScoreAsync(int? id)
        {
            var DealerScore = await FindDealerScoreAsync(id);
            _dbContext.DealerScore.Remove(DealerScore);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddDealerScoreAsync(DealerScore DealerScore)
        {
            _dbContext.DealerScore.Add(DealerScore);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeDealerScoreAsync(DealerScore DealerScore)
        {
            _dbContext.Entry(DealerScore).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Methods for DocFee

        public async Task<List<DocFee>> GetAllDocFeeAsync()
        {
            return await _dbContext.DocFee.ToListAsync();
        }

        public async Task<DocFee> FindDocFeeAsync(int? id)
        {
            return await _dbContext.DocFee.FindAsync(id);
        }

        public async Task<bool> RemoveDocFeeAsync(int? id)
        {
            var DocFee = await FindDocFeeAsync(id);
            _dbContext.DocFee.Remove(DocFee);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddDocFeeAsync(DocFee DocFee)
        {
            _dbContext.DocFee.Add(DocFee);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeDocFeeAsync(DocFee DocFee)
        {
            _dbContext.Entry(DocFee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<DocFee> GetAllDocFee()
        {
            var DocFeeQuery = from at in _dbContext.DocFee
                select at;

            var resultList = new List<DocFee>();
            resultList = DocFeeQuery.ToList();
            return resultList;
        }

        #endregion

        #region Methods for StateAdjustment

        public async Task<List<StateAdjustment>> GetAllStateAdjustmentAsync()
        {
            return await _dbContext.StateAdjustment.ToListAsync();
        }

        public async Task<List<StateAdjustmentViewModel>> GetAllStateAdjustmentViewModelAsync()
        {
            var StateAdjustmentQuery = from a in _dbContext.StateAdjustment
                join at in _dbContext.StateCode on a.StateCode.ID equals at.ID
                select new StateAdjustmentViewModel
                {
                    StateCodeID = at.ID,
                    StateCodeName = at.Name,
                    StateCodeAbbreviation = at.Abbreviation,
                    StateAdjustmentID = a.ID,
                    StateAdjustmentDiscountAdj = a.DiscountAdj*100,
                    StateAdjustmentLTVAdj = a.LTVAdj*100,
                    StateAdjustmentMaxStateAPR = a.MaxStateAPR*100,
                    StateAdjustmentGapCap = a.GapCap,
                    StateAdjustmentCreatedByID = a.CreatedByID,
                    StateAdjustmentModifiedByID = a.ModifiedByID,
                    StateAdjustmentCreatedDate = a.CreatedDate,
                    StateAdjustmentModifiedDate = a.ModifiedDate
                };
            return await StateAdjustmentQuery.ToListAsync();
        }

        public async Task<StateAdjustment> FindStateAdjustmentAsync(int? id)
        {
            return await _dbContext.StateAdjustment.FindAsync(id);
        }

        public async Task<StateAdjustmentViewModel> FindStateAdjustmentViewModelAsync(int? id)
        {
            var StateAdjustmentQuery = from a in _dbContext.StateAdjustment
                join at in _dbContext.StateCode on a.StateCode.ID equals at.ID
                where a.ID == id
                select new StateAdjustmentViewModel
                {
                    StateCodeID = at.ID,
                    StateCodeName = at.Name,
                    StateCodeAbbreviation = at.Abbreviation,
                    StateAdjustmentID = a.ID,
                    StateAdjustmentDiscountAdj = a.DiscountAdj,
                    StateAdjustmentLTVAdj = a.LTVAdj,
                    StateAdjustmentMaxStateAPR = a.MaxStateAPR,
                    StateAdjustmentGapCap = a.GapCap,
                    StateAdjustmentCreatedByID = a.CreatedByID,
                    StateAdjustmentModifiedByID = a.ModifiedByID,
                    StateAdjustmentCreatedDate = a.CreatedDate,
                    StateAdjustmentModifiedDate = a.ModifiedDate
                };

            return await StateAdjustmentQuery.FirstAsync();
        }

        public async Task<bool> RemoveStateAdjustmentAsync(int? id)
        {
            var StateAdjustment = await FindStateAdjustmentAsync(id);
            _dbContext.StateAdjustment.Remove(StateAdjustment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStateAdjustmentAsync(StateAdjustment StateAdjustment)
        {
            _dbContext.StateAdjustment.Add(StateAdjustment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStateAdjustmentAsync(StateAdjustment StateAdjustment)
        {
            _dbContext.Entry(StateAdjustment).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();


            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.StateAdjustment SET StateCode_ID = " +
            //                                      StateAdjustment.StateCode.ID + " WHERE ID = " + StateAdjustment.ID);
            return true;
        }

        #endregion

        #region Methods for StateFICORange

        public async Task<List<StateFicoRange>> GetAllStateFicoRangeAsync()
        {
            return await _dbContext.StateFicoRange.ToListAsync();
        }

        public async Task<List<StateFICORangeViewModel>> GetAllStateFicoRangeViewModelAsync()
        {
            var StateFicoRangeQuery = from a in _dbContext.StateFicoRange
                join at in _dbContext.StateCode on a.StateCode.ID equals at.ID
                select new StateFICORangeViewModel
                {
                    StateCodeID = at.ID,
                    StateCodeName = at.Name,
                    StateCodeAbbreviation = at.Abbreviation,
                    StateFICORangeID = a.ID,
                    StateFICORangeFicoScoreMin = a.FicoScoreMin,
                    StateFICORangeFicoScoreMax = a.FicoScoreMax,
                    StateFICORangeMinAPR = a.MinAPR*100,
                    StateFICORangeCreatedByID = a.CreatedByID,
                    StateFICORangeModifiedByID = a.ModifiedByID,
                    StateFICORangeCreatedDate = a.CreatedDate,
                    StateFICORangeModifiedDate = a.ModifiedDate
                };
            return await StateFicoRangeQuery.ToListAsync();
        }

        public async Task<StateFicoRange> FindStateFicoRangeAsync(int? id)
        {
            return await _dbContext.StateFicoRange.FindAsync(id);
        }

        public async Task<StateFICORangeViewModel> FindStateFicoRangeViewModelAsync(int? id)
        {
            var StateFicoRangeQuery = from a in _dbContext.StateFicoRange
                join at in _dbContext.StateCode on a.StateCode.ID equals at.ID
                where a.ID == id
                select new StateFICORangeViewModel
                {
                    StateCodeID = at.ID,
                    StateCodeName = at.Name,
                    StateCodeAbbreviation = at.Abbreviation,
                    StateFICORangeID = a.ID,
                    StateFICORangeFicoScoreMin = a.FicoScoreMin,
                    StateFICORangeFicoScoreMax = a.FicoScoreMax,
                    StateFICORangeMinAPR = a.MinAPR,
                    StateFICORangeCreatedByID = a.CreatedByID,
                    StateFICORangeModifiedByID = a.ModifiedByID,
                    StateFICORangeCreatedDate = a.CreatedDate,
                    StateFICORangeModifiedDate = a.ModifiedDate
                };

            return await StateFicoRangeQuery.FirstAsync();
        }

        public async Task<bool> RemoveStateFicoRangeAsync(int? id)
        {
            var StateFicoRange = await FindStateFicoRangeAsync(id);
            _dbContext.StateFicoRange.Remove(StateFicoRange);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStateFicoRangeAsync(StateFicoRange StateFicoRange)
        {
            _dbContext.StateFicoRange.Add(StateFicoRange);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStateFicoRangeAsync(StateFicoRange StateFicoRange)
        {
            _dbContext.Entry(StateFicoRange).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.StateFicoRange SET StateCode_ID = " +
            //                                      StateFicoRange.StateCode.ID + " WHERE ID = " + StateFicoRange.ID);
            return true;
        }

        #endregion

        #region Methods for DealerDiscount

        public async Task<List<DealerDiscount>> GetAllDealerDiscountAsync()
        {
            return await _dbContext.DealerDiscount.ToListAsync();
        }

        public async Task<List<DealerDiscountViewModel>> GetAllDealerDiscountViewModelAsync()
        {
            var DealerDiscountQuery = from a in _dbContext.DealerDiscount
                join at in _dbContext.StateCode on a.StateCode.ID equals at.ID
                join bt in _dbContext.DealerType on a.DealerType.ID equals bt.ID
                select new DealerDiscountViewModel
                {
                    StateCodeID = at.ID,
                    StateCodeName = at.Name,
                    DealerTypeID = bt.ID,
                    DealerTypeType = bt.Type,
                    DealerDiscountID = a.ID,
                    DealerDiscountDiscount = a.Discount*100,
                    DealerDiscountCreatedByID = a.CreatedByID,
                    DealerDiscountModifiedByID = a.ModifiedByID,
                    DealerDiscountCreatedDate = a.CreatedDate,
                    DealerDiscountModifiedDate = a.ModifiedDate
                };

            return await DealerDiscountQuery.ToListAsync();
        }

        public async Task<DealerDiscount> FindDealerDiscountAsync(int? id)
        {
            return await _dbContext.DealerDiscount.FindAsync(id);
        }

        public async Task<DealerDiscountViewModel> FindDealerDiscountViewModelAsync(int? id)
        {
            var DealerDiscountQuery = from a in _dbContext.DealerDiscount
                join at in _dbContext.StateCode on a.StateCode.ID equals at.ID
                join bt in _dbContext.DealerType on a.DealerType.ID equals bt.ID
                where a.ID == id
                select new DealerDiscountViewModel
                {
                    StateCodeID = at.ID,
                    StateCodeName = at.Name,
                    DealerTypeID = bt.ID,
                    DealerTypeType = bt.Type,
                    DealerDiscountID = a.ID,
                    DealerDiscountDiscount = a.Discount,
                    DealerDiscountCreatedByID = a.CreatedByID,
                    DealerDiscountModifiedByID = a.ModifiedByID,
                    DealerDiscountCreatedDate = a.CreatedDate,
                    DealerDiscountModifiedDate = a.ModifiedDate
                };

            return await DealerDiscountQuery.FirstAsync();
        }

        public async Task<bool> RemoveDealerDiscountAsync(int? id)
        {
            var DealerDiscount = await FindDealerDiscountAsync(id);
            _dbContext.DealerDiscount.Remove(DealerDiscount);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddDealerDiscountAsync(DealerDiscount DealerDiscount)
        {
            _dbContext.DealerDiscount.Add(DealerDiscount);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeDealerDiscountAsync(DealerDiscount DealerDiscount)
        {
            _dbContext.Entry(DealerDiscount).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();


            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.DealerDiscount SET StateCode_ID = " +
            //                                      DealerDiscount.StateCode.ID + " WHERE ID = " + DealerDiscount.ID);
            return true;
        }

        #endregion methods for DealerDiscount

        #region Methods for DealerType Methods

        public async Task<List<DealerType>> GetAllDealerTypeAsync()
        {
            return await _dbContext.DealerType.ToListAsync();
        }

        public async Task<DealerType> FindDealerTypeAsync(int? id)
        {
            return await _dbContext.DealerType.FindAsync(id);
        }

        public List<DealerType> GetAllDealerType()
        {
            var DealerTypeQuery = from at in _dbContext.DealerType
                select at;

            var resultList = new List<DealerType>();
            resultList = DealerTypeQuery.ToList();
            return resultList;
        }

        #endregion DealerType

        #region Methods for ClassCodeAdj

        public async Task<List<ClassCodeAdj>> GetAllClassCodeAdjAsync()
        {
            return await _dbContext.ClassCodeAdj.ToListAsync();
        }

        public async Task<List<ClassCodeAdjViewModel>> GetAllClassCodeAdjViewModelAsync()
        {
            var ClassCodeAdjQuery = from a in _dbContext.ClassCodeAdj
                join at in _dbContext.VehicleClassType on a.VehicleClassType.ID equals at.ID
                select new ClassCodeAdjViewModel
                {
                    VehicleClassTypeID = at.ID,
                    VehicleClassTypeName = at.Name,
                    ClassCodeAdjID = a.ID,
                    ClassCodeAdjLTVAdj = a.LTVAdj*100,
                    ClassCodeAdjLTVCap = a.LTVCap,
                    ClassCodeAdjDiscountAdj = a.DiscountAdj*100,
                    ClassCodeAdjFinancedCap = a.FinancedCap,
                    ClassCodeAdjCreatedByID = a.CreatedByID,
                    ClassCodeAdjModifiedByID = a.ModifiedByID,
                    ClassCodeAdjCreatedDate = a.CreatedDate,
                    ClassCodeAdjModifiedDate = a.ModifiedDate
                };
            return await ClassCodeAdjQuery.ToListAsync();
        }

        public async Task<ClassCodeAdj> FindClassCodeAdjAsync(int? id)
        {
            return await _dbContext.ClassCodeAdj.FindAsync(id);
        }

        public async Task<ClassCodeAdjViewModel> FindClassCodeAdjViewModelAsync(int? id)
        {
            var ClassCodeAdjtQuery = from a in _dbContext.ClassCodeAdj
                join at in _dbContext.VehicleClassType on a.VehicleClassType.ID equals at.ID
                where a.ID == id
                select new ClassCodeAdjViewModel
                {
                    VehicleClassTypeID = at.ID,
                    VehicleClassTypeName = at.Name,
                    ClassCodeAdjID = a.ID,
                    ClassCodeAdjLTVAdj = a.LTVAdj,
                    ClassCodeAdjLTVCap = a.LTVCap,
                    ClassCodeAdjDiscountAdj = a.DiscountAdj,
                    ClassCodeAdjFinancedCap = a.FinancedCap,
                    ClassCodeAdjCreatedByID = a.CreatedByID,
                    ClassCodeAdjModifiedByID = a.ModifiedByID,
                    ClassCodeAdjCreatedDate = a.CreatedDate,
                    ClassCodeAdjModifiedDate = a.ModifiedDate
                };

            return await ClassCodeAdjtQuery.FirstAsync();
        }

        public async Task<bool> RemoveClassCodeAdjAsync(int? id)
        {
            var ClassCodeAdj = await FindClassCodeAdjAsync(id);
            _dbContext.ClassCodeAdj.Remove(ClassCodeAdj);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddClassCodeAdjAsync(ClassCodeAdj ClassCodeAdj)
        {
            _dbContext.ClassCodeAdj.Add(ClassCodeAdj);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeClassCodeAdjAsync(ClassCodeAdj ClassCodeAdj)
        {
            _dbContext.Entry(ClassCodeAdj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.ClassCodeAdj SET VehicleClassType_ID = " +
            //                                      ClassCodeAdj.VehicleClassType.ID + " WHERE ID = " + ClassCodeAdj.ID);
            return true;
        }

        #endregion

        #region Methods for TermCap

        public async Task<List<TermCap>> GetAllTermCapAsync()
        {
            return await _dbContext.TermCap.ToListAsync();
        }

        public async Task<List<TermCapViewModel>> GetAllTermCapViewModelAsync()
        {
            var TermCapQuery = from a in _dbContext.TermCap
                join at in _dbContext.AdjustmentType on a.AdjustmentType.ID equals at.ID
                select new TermCapViewModel
                {
                    AdjustmentTypeID = at.ID,
                    AdjustmentTypeType = at.Type,
                    AdjustmentTypeDisplayName = at.DisplayName,
                    TermCapID = a.ID,
                    TermCapMinFinanceAmt = a.MinFinanceAmt,
                    TermCapMaxFinanceAmt = a.MaxFinanceAmt,
                    TermCapAdjustmentCap = a.AdjustmentCap,
                    TermCapCreatedByID = a.CreatedByID,
                    TermCapModifiedByID = a.ModifiedByID,
                    TermCapCreatedDate = a.CreatedDate,
                    TermCapModifiedDate = a.ModifiedDate
                };
            return await TermCapQuery.ToListAsync();
        }

        public async Task<TermCap> FindTermCapAsync(int? id)
        {
            return await _dbContext.TermCap.FindAsync(id);
        }

        public async Task<TermCapViewModel> FindTermCapViewModelAsync(int? id)
        {
            var TermCapQuery = from a in _dbContext.TermCap
                join at in _dbContext.AdjustmentType on a.AdjustmentType.ID equals at.ID
                where a.ID == id
                select new TermCapViewModel
                {
                    AdjustmentTypeID = at.ID,
                    AdjustmentTypeType = at.Type,
                    AdjustmentTypeDisplayName = at.DisplayName,
                    TermCapID = a.ID,
                    TermCapMinFinanceAmt = a.MinFinanceAmt,
                    TermCapMaxFinanceAmt = a.MaxFinanceAmt,
                    TermCapAdjustmentCap = a.AdjustmentCap,
                    TermCapCreatedByID = a.CreatedByID,
                    TermCapModifiedByID = a.ModifiedByID,
                    TermCapCreatedDate = a.CreatedDate,
                    TermCapModifiedDate = a.ModifiedDate
                };

            return await TermCapQuery.FirstAsync();
        }

        public async Task<bool> RemoveTermCapAsync(int? id)
        {
            var TermCap = await FindTermCapAsync(id);
            _dbContext.TermCap.Remove(TermCap);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTermCapAsync(TermCap TermCap)
        {
            _dbContext.TermCap.Add(TermCap);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeTermCapAsync(TermCap TermCap)
        {
            _dbContext.Entry(TermCap).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            //_dbContext.Database.ExecuteSqlCommand("Update ScoringEngine.TermCap SET AdjustmentType_ID = " +
            //                                      TermCap.AdjustmentType.ID + " WHERE ID = " + TermCap.ID);
            return true;
        }

        #endregion
    }
}