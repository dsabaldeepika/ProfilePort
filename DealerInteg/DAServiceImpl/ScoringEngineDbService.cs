//-----------------------------------------------------------------------
// <copyright  company="Versos">
//     Copyright (c) Versos  All rights reserved.
// </copyright>
//-------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Com.CoreLane.ScoringEngine.Models;
using ScoringEngine.Exceptions;
using ScoringEngine.InterfaceModels;
using ScoringEngine.Services;

namespace DealerPortalCRM.DAServiceImpl
{
    /// <summary>
    ///     Database Services for the Scoring Engine
    /// </summary>
    public class ScoringEngineDbService : IScoringEngineDbService
    {
        private readonly ScoringEngineEntities _dbContext;

        public ScoringEngineDbService(ScoringEngineEntities db)
        {
            _dbContext = db;
        }

        // For Mock tests Only
        public ScoringEngineDbService()
        {
        }

        public double GetTermLimit(int amountFinanced)
        {
            return 100;
        }


        /// <summary>
        ///     Returns a list  of Expressions for a given Module
        /// </summary>
        /// <param name="moduleId">
        ///     The module ID of the module or screen
        /// </param>
        /// <returns />
        /// - void
        /// <exception />
        /// -  none -
        public List<ExpressionCatalogModel> GetAllExpressionsForModule(int moduleId)
        {
            return (from e in _dbContext.ExpressionCatalog
                join usageDetail in _dbContext.ExpressionUsageDetail on e.ID equals usageDetail.ExpressionCatalog.ID
                join usage in _dbContext.ExpressionUsage on usageDetail.ExpressionUsage.ID equals usage.ID
                orderby usageDetail.ExecutionSequence
                where usage.ID == moduleId
                select new ExpressionCatalogModel
                {
                    ID = e.ID,
                    Expression = e.Expression,
                    Name = e.Name,
                    IsActive = e.IsActive,
                    IsComposite = e.IsComposite
                }).ToList();
        }


        /// <summary>
        ///     Returns application data mocked using table ScoringEngine.TempApplicationModel
        /// </summary>
        /// <returns />
        /// - List of TempAppInterfaceModel from table ScoringEngine.TempApplicationModel
        /// <exception />
        /// -  none -
        public List<TempAppInterfaceModel> GetAllApplicationData()
        {
            return (from e in _dbContext.TempApplicationModel
                select new TempAppInterfaceModel
                {
                    ID = e.ID,
                    Name = e.Name,
                    Value = e.Value
                }).ToList();
        }


        public int UpdateScoringEngineResults(List<TempResultsModel> resultsModels)
        {
            foreach (var tempResultsModel in resultsModels)
            {
                var tempScoreEngineResults = new TempScoreEngineResults();
                tempScoreEngineResults.Expression = tempResultsModel.Expression;
                tempScoreEngineResults.Name = tempResultsModel.Name;
                tempScoreEngineResults.Result = tempResultsModel.Result;
                _dbContext.TempScoreEngineResults.Add(tempScoreEngineResults);
            }

            return _dbContext.SaveChanges();
        }

        public long CreateScoringEngineRun(string referenceValue)
        {
            var scoringEngineRun = new ScoringEngineRun
            {
                RefValue = referenceValue,
                CreatedByID = 1,
                ModifiedByID = 1
            };
            _dbContext.ScoringEngineRun.Add(scoringEngineRun);
            _dbContext.SaveChanges();
            return scoringEngineRun.ID;
        }

        public int UpdateScoringEngineResults(TempResultsModel resultsModel, List<ParamLogModel> paramModels,
            long scoringEngineRunID)
        {
            var scoringEngineRun = (from r in _dbContext.ScoringEngineRun
                where r.ID == scoringEngineRunID
                select r).Single();

            var tempScoreEngineResults = new TempScoreEngineResults();
            tempScoreEngineResults.ScoringEngineRun = scoringEngineRun;
            tempScoreEngineResults.Expression = resultsModel.Expression;
            tempScoreEngineResults.Name = resultsModel.Name;
            tempScoreEngineResults.Result = resultsModel.Result;
            tempScoreEngineResults.CreatedByID = 1;
            tempScoreEngineResults.ModifiedByID = 1;
            _dbContext.TempScoreEngineResults.Add(tempScoreEngineResults);

            foreach (var paramModel in paramModels)
            {
                var tempScoringEngineParams = new TempScoringEngineParamLog();
                tempScoringEngineParams.TempScoreEngineResults = tempScoreEngineResults;

                tempScoringEngineParams.ParamName = paramModel.ParamName;
                tempScoringEngineParams.ParamValue = paramModel.ParamValue;
                tempScoringEngineParams.CreatedByID = 1;
                tempScoringEngineParams.ModifiedByID = 1;


                _dbContext.TempScoringEngineParamLog.Add(tempScoringEngineParams);
            }

            return _dbContext.SaveChanges();
        }


        public double GetDealerDiscountForState(string state, int dealerType)
        {
            var retrun = (from d in _dbContext.DealerDiscount
                join st in _dbContext.StateCode on d.StateCode equals st
                where st.Abbreviation == state && d.DealerType.ID == dealerType
                select d.Discount).Single();


            return Convert.ToDouble(retrun);
        }


        public double GetTermCapAdjByAdjType(int adjType, double amountFinanced)
        {
            return Convert.ToDouble((from termCap in _dbContext.TermCap
                where
                    termCap.MinFinanceAmt <= amountFinanced && termCap.MaxFinanceAmt > amountFinanced &&
                    termCap.AdjustmentType.ID == adjType
                select termCap.AdjustmentCap).Single());
        }


        public double GetExcessMileageLTVAdj(double vehicleMileage)
        {
            return Convert.ToDouble((from e in _dbContext.ExcessMileage
                where vehicleMileage >= e.MinMileage && vehicleMileage < e.MaxMileage
                select e.LTVAdj).Single());
        }


        public double GetExcessMileageDiscountAdj(double vehicleMileage)
        {
            return Convert.ToDouble((from e in _dbContext.ExcessMileage
                where vehicleMileage >= e.MinMileage && vehicleMileage < e.MaxMileage
                select e.DiscAdj).Single());
        }


        public double GetExcessMileageTermCap(double vehicleMileage)
        {
            return Convert.ToDouble((from e in _dbContext.ExcessMileage
                where vehicleMileage >= e.MinMileage && vehicleMileage < e.MaxMileage
                select e.TermCap).Single());
        }


        public double GetAdjustmentRangeDiscount(int adjustmentType, double rangeValue)
        {
            var decRDecimal = (decimal) rangeValue;
            return Convert.ToDouble((from adjRange in _dbContext.AdjustmentRange
                where
                    adjRange.AdjustmentType.ID == adjustmentType && decRDecimal >= adjRange.MinTerm &&
                    decRDecimal < adjRange.MaxTerm
                select adjRange.AdjustmentDiscount).Single());
        }


        public DocFeeViewModel GetDocFeeByTermAndVehicleAge(int vehicleAge, int term)
        {
            var docFeeViewModel = (from docFee in _dbContext.DocFee
                where docFee.MinTerm <= term && docFee.MaxTerm >= term && docFee.VehicleAge == vehicleAge
                select new DocFeeViewModel
                {
                    ID = docFee.ID,
                    MaxMiles = docFee.MaxMiles,
                    MaxTerm = docFee.MaxTerm,
                    MinTerm = docFee.MinTerm,
                    VehicleAge = docFee.VehicleAge,
                    VehicleClass = docFee.VehicleClass
                }).FirstOrDefault();


            if (docFeeViewModel == null)
                return new DocFeeViewModel
                {
                    ID = 0,
                    MaxMiles = 0,
                    MaxTerm = 0,
                    MinTerm = 0,
                    VehicleAge = 0,
                    VehicleClass = 0
                };

            return docFeeViewModel;
        }


        public double GetDealerStateAPR(int minOrMax, string stateAbbr)
        {
            //1 = Min
            if (minOrMax == 1)
            {
                return Convert.ToDouble((from stateAdjustment in _dbContext.StateAdjustment
                    join state in _dbContext.StateCode on stateAdjustment.StateCode.ID equals state.ID
                    where state.Abbreviation.Trim() == stateAbbr
                    select stateAdjustment.MaxStateAPR).Single());
            }
            return Convert.ToDouble((from state in _dbContext.StateCode
                join stateAdjustment in _dbContext.StateAdjustment on state equals stateAdjustment.StateCode
                where state.Abbreviation.Trim() == stateAbbr
                select stateAdjustment.MaxStateAPR).Single());
        }


        public double GetDealerGapCap(string stateCode)
        {
            //    return System.Convert.ToDouble((from state in _dbContext.StateCode
            //                                    join gap in _dbContext.GapCap on state equals gap.StateCode
            //                                    where state.Abbreviation == stateCode
            //                                    select gap.Cap).Single());

            return 1.0;
        }


        public double GetStateMaxAPR(int adjType, string stateCode, int ageOfVehicle, int vehicleYear, int term,
            double amountFinanced)
        {
            double returnValue = 0;

            var decAmountFinanced = (decimal) amountFinanced;


            var stateCodeParameter = new SqlParameter("@StateCode", SqlDbType.VarChar)
            {
                Value = stateCode
            };


            var ageOfVehicleParameter = new SqlParameter("@VehicleAge", SqlDbType.Int)
            {
                Value = ageOfVehicle
            };

            var vehicleYearParameter = new SqlParameter("@VehicleYear", SqlDbType.Int)
            {
                Value = vehicleYear
            };

            var termParameter = new SqlParameter("@LoanTerm", SqlDbType.Int)
            {
                Value = term
            };

            var amountFinancedParameter = new SqlParameter("@LoanAmount", SqlDbType.Decimal)
            {
                Value = decAmountFinanced
            };


            var results =
                _dbContext.Database.SqlQuery<decimal>(
                    "EXEC ScoringEngine.GetStateMaxAPR   @StateCode , @VehicleAge   ,@VehicleYear , @LoanTerm , @LoanAmount ",
                    stateCodeParameter, ageOfVehicleParameter, vehicleYearParameter, termParameter,
                    amountFinancedParameter).Single();

            if (results != null)
            {
                returnValue = (double) results;
                return returnValue;
            }
            throw new RecordNotFoundException("MaxAPR not found for State : " + stateCode);
        }


        public double GetSateAdjustmentDiscount(int adjType, string stateCode, int ageOfVehicle, int vehicleYear,
            int term, double amountFinanced)
        {
            switch (adjType)
            {
                case 1:
                    return Convert.ToDouble((from state in _dbContext.StateCode
                        join stateAdj in _dbContext.StateAdjustment on state equals stateAdj.StateCode
                        where state.Abbreviation == stateCode
                        select stateAdj.DiscountAdj).Single());
                    break;

                case 2:

                    return Convert.ToDouble((from state in _dbContext.StateCode
                        join stateAdj in _dbContext.StateAdjustment on state equals stateAdj.StateCode
                        where state.Abbreviation == stateCode
                        select stateAdj.LTVAdj).Single());
                    break;

                case 3:


                    return GetStateMaxAPR(adjType, stateCode, ageOfVehicle, vehicleYear, term, amountFinanced);

                    break;

                case 4:

                    return Convert.ToDouble((from state in _dbContext.StateCode
                        join stateAdj in _dbContext.StateAdjustment on state equals stateAdj.StateCode
                        where state.Abbreviation == stateCode
                        select stateAdj.GapCap).Single());
                    break;

                default:
                    throw new RecordNotFoundException("Invalid adjType passed to GetSateAdjustmentDiscount");
            }
        }


        public double GetAdjustmentDiscountByType(int adjType, string adjNumber)
        {
            return Convert.ToDouble((from adj in _dbContext.Adjustment
                join adjustmentType in _dbContext.AdjustmentType on adj.AdjustmentType equals adjustmentType
                where adj.AdjustmentNumber == adjNumber && adjustmentType.ID == adjType
                select adj.AdjustmentDiscount).Single());
        }


        public double GetPricingBaseByBookValueAndLTV(double bookValue, double ltv)
        {
            // decimal decBookValue = 5000;
            //  decimal decLtv = 82 ; 
            var decBookValue = (decimal) bookValue;
            var decLtv = (decimal) ltv;

            return Convert.ToDouble((from pricingBase in _dbContext.PricingBase
                where decBookValue >= pricingBase.MinBV && decBookValue < pricingBase.MaxBV &&
                      decLtv >= pricingBase.MinLTV && decLtv < pricingBase.MaxLTV
                select pricingBase.PricingRateDiscount).Single());
        }


        public double GetBuyRate(double bookValue, double ficoScore)
        {
            var decBookValue = (decimal) bookValue;
            var decFicoScore = (decimal) ficoScore;

            return Convert.ToDouble((from buyRate in _dbContext.BuyRate
                where decBookValue >= buyRate.MinBV && decBookValue < buyRate.MaxBV &&
                      decFicoScore >= buyRate.MinFico && decFicoScore < buyRate.MaxFico
                select buyRate.BuyRateValue).Single());
        }


        public double GetClassCodeAdjustment(int adjType, string vehicleClassName)
        {
            // 1 LTV Cap
            // 2  LTVAdj
            //3 DiscountAdj 
            // 4 FinancedCap

            switch (adjType)
            {
                case 1:
                    return Convert.ToDouble((from classCode in _dbContext.ClassCodeAdj
                        join vehicleClassType in _dbContext.VehicleClassType on classCode.VehicleClassType equals
                            vehicleClassType
                        where vehicleClassType.Name.Trim() == vehicleClassName
                        select classCode.LTVCap).Single());
                    break;


                case 2:
                    return Convert.ToDouble((from classCode in _dbContext.ClassCodeAdj
                        join vehicleClassType in _dbContext.VehicleClassType on classCode.VehicleClassType.ID equals
                            vehicleClassType.ID
                        where vehicleClassType.Name.Trim() == vehicleClassName
                        select classCode.LTVAdj).Single());
                    break;

                case 3:
                    return Convert.ToDouble((from classCode in _dbContext.ClassCodeAdj
                        join vehicleClassType in _dbContext.VehicleClassType on classCode.VehicleClassType equals
                            vehicleClassType
                        where vehicleClassType.Name.Trim() == vehicleClassName
                        select classCode.DiscountAdj).Single());
                    break;

                case 4:
                    return Convert.ToDouble((from classCode in _dbContext.ClassCodeAdj
                        join vehicleClassType in _dbContext.VehicleClassType on classCode.VehicleClassType equals
                            vehicleClassType
                        where vehicleClassType.Name.Trim() == vehicleClassName
                        select classCode.FinancedCap).Single());

                default:
                    return 0;
            }
        }


        public string GetVehicleClassForMakeAndModel(string make, string model)
        {
            return (from vehicleClassType in _dbContext.VehicleClassType
                join makeModelClass in _dbContext.VehicleMakeModelClass on vehicleClassType equals
                    makeModelClass.VehicleClassType
                join makeType in _dbContext.VehicleMakeType on makeModelClass.VehicleMakeType equals makeType
                join modelType in _dbContext.VehicleModelType on makeModelClass.VehicleModelType equals modelType
                where makeType.Name.Trim() == make && modelType.Name.Trim() == model
                select vehicleClassType.Name).Single();
        }


        public double GetDealerDiscountByScore(int discountType, string dealerScoreArg)
        {
            switch (discountType)
            {
                case 1:


                    return Convert.ToDouble((from dealerScore in _dbContext.DealerScore
                        where dealerScore.Score.Trim() == dealerScoreArg
                        select dealerScore.DisAjd).Single());


                    break;

                case 2:


                    return Convert.ToDouble((from dealerScore in _dbContext.DealerScore
                        where dealerScore.Score.Trim() == dealerScoreArg
                        select dealerScore.LTVCap).Single());


                    break;

                case 3:


                    return Convert.ToDouble((from dealerScore in _dbContext.DealerScore
                        where dealerScore.Score.Trim() == dealerScoreArg
                        select dealerScore.MinDisc).Single());


                default:
                    return 0;
            }
        }


        public double GetMinAprFromStateFicoRange(string stateCodeParam, int ficoScore)
        {
            return Convert.ToDouble((from stateFicoRange in _dbContext.StateFicoRange
                join state in _dbContext.StateCode on stateFicoRange.StateCode.ID equals state.ID
                where state.Abbreviation == stateCodeParam
                      && ficoScore >= stateFicoRange.FicoScoreMin && ficoScore < stateFicoRange.FicoScoreMax
                select stateFicoRange.MaxAPRDiscountAdj).Single());
        }


        public double GetDocFeesMileLimitByVehicleAge(double ageOfVehicle)
        {
            return 0;
        }

        public double GetDealerMinimumDiscount(string dealerRank, string dealerType)
        {
            return 0;
        }
    }
}