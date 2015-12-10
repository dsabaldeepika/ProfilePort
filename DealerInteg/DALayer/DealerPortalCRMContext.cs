
//using System.Data.Entity;
//using Com.CoreLane.ScoringEngine.Models;



//namespace DealerPortalCRM.DALayer
//{
//    public class Dealer1PortalCRMContext : DbContext
//    {
//        public Dealer1PortalCRMContext()
//            : base("ScoringEngineEntities")
//        {

//        }
//        public Dealer1PortalCRMContext(string connectionString)
//            : base(connectionString)
//        {
//        }














//        // Scoring Engine Tables

//        public DbSet<Adjustment> Adjustment { get; set; }
//        public DbSet<AdjustmentType> AdjustmentType { get; set; }

//        public DbSet<AdjustmentRange> AdjustmentRange { get; set; }

//        public DbSet<BuyRate> BuyRate { get; set; }
//        public DbSet<ClassCodeAdj> ClassCodeAdj { get; set; }
//        public DbSet<DealerDiscount> DealerDiscount { get; set; }
//        public DbSet<DealerScore> DealerScore { get; set; }
//        public DbSet<DealerType> DealerType { get; set; }
//        public DbSet<ExcessMileage> ExcessMileage { get; set; }
//        public DbSet<PricingBase> PricingBase { get; set; }
//        public DbSet<StateAdjustment> StateAdjustment { get; set; }

//        public DbSet<StateCode> StateCode { get; set; }
//        public DbSet<VehicleMakeType> VehicleMakeType { get; set; }
//        public DbSet<VehicleClassType> VehicleClassType { get; set; }
//        public DbSet<VehicleModelType> VehicleModelType { get; set; }
//        public DbSet<VehicleMakeModelClass> VehicleMakeModelClass { get; set; }
//        public DbSet<Vehicle> Vehicle { get; set; }

//        public DbSet<DocFee> DocFee { get; set; }

//        public DbSet<TermCap> TermCap { get; set; }



//        // Expression 

//        public DbSet<ExpressionCatalog> ExpressionCatalog { get; set; }

       

//        public DbSet<ExpressionUsage> ExpressionUsage { get; set; }

//        public DbSet<ExpressionUsageDetail> ExpressionUsageDetail { get; set; }


//        public DbSet<TempApplicationModel> TempApplicationModel { get; set; }

//        public DbSet<TempScoreEngineResults> TempScoreEngineResults { get; set; }


//        public DbSet<ScoringEngineRun> ScoringEngineRun { get; set; }

//        public System.Data.Entity.DbSet<StateFicoRange> StateFicoRange { get; set; }

//        // Staging Tables


//        public DbSet<TempScoringEngineParamLog> TempScoringEngineParamLog { get; set; }

//        public DbSet<StateMaxAPR> StateMaxAPR { get; set; }



//    }
//}

