//using Com.CoreLane.ScoringEngine.Models;
//
//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace DealerPortalCRM.DALayer
//{

//    //DropCreateDatabaseIfModelChanges
//    //DropCreateDatabaseAlways
//    //CreateDatabaseIfNotExists
//    public class DealerPortalCRMInitializer : System.Data.Entity.CreateDatabaseIfNotExists<ScoringEngineEntities>
//    {
//        protected override void Seed(ScoringEngineEntities context)
//        {
//            var students = new List<Student>
//            {
//                new Student
//                {
//                    FirstMidName = "Carson",
//                    LastName = "Alexander",
//                    EnrollmentDate = DateTime.Parse("2005-09-01")
//                },
//                new Student
//                {
//                    FirstMidName = "Meredith",
//                    LastName = "Alonso",
//                    EnrollmentDate = DateTime.Parse("2002-09-01")
//                },
//                new Student {FirstMidName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01")},
//                new Student
//                {
//                    FirstMidName = "Gytis",
//                    LastName = "Barzdukas",
//                    EnrollmentDate = DateTime.Parse("2002-09-01")
//                },
//                new Student {FirstMidName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01")},
//                new Student
//                {
//                    FirstMidName = "Peggy",
//                    LastName = "Justice",
//                    EnrollmentDate = DateTime.Parse("2001-09-01")
//                },
//                new Student {FirstMidName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01")},
//                new Student
//                {
//                    FirstMidName = "Nino",
//                    LastName = "Olivetto",
//                    EnrollmentDate = DateTime.Parse("2005-09-01")
//                }
//            };

//            students.ForEach(s => context.Students.Add(s));
//            context.SaveChanges();


//            //// Organization 

//            //var orgList = new List<Organization>
//            //{
//            //  contact1 , contact2
//            //};
//            //contactList.ForEach(s => context.Contact.Add(s));
//            //context.SaveChanges();

//            //Stage Tables Application

//            Application application1 = new Application { ID = 1, appDate = DateTime.Parse("2015-07-28"), appTime = DateTime.Parse("2015-07-28"),  ApplicationEnteredDate = DateTime.Parse("2015-07-28"), ContractDate = DateTime.Parse("2015-07-28"), createdByID = 1, modifiedByID = 1, createdDate = DateTime.Parse("2015-07-28"), modifiedDate = DateTime.Parse("2015-07-28") };

//            var applList = new List<Application>
//            {
//                application1
//            };
//            applList.ForEach(s => context.Application.Add(s));
//            context.SaveChanges();

//            ApplicationTransaction applicationTransaction = new ApplicationTransaction { Application = application1, CreatedByID = 1, ModifiedByID = 1, CreatedDate =   DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var applTrnList = new List<ApplicationTransaction>
//            {
//                applicationTransaction
//            };
//            applTrnList.ForEach(s => context.ApplicationTransaction.Add(s));
//            context.SaveChanges();


//            ApplicationStage applicationStage = new ApplicationStage { ApplicationTransaction = applicationTransaction, CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var applStageList = new List<ApplicationStage>
//            {
//                applicationStage
//            };
//            applStageList.ForEach(s => context.ApplicationStage.Add(s));
//            context.SaveChanges();


//            LoanStructure loanStructure = new LoanStructure { totalCashPrice = 34550, downPayment = 3200, CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var applLoanStructureList = new List<LoanStructure>
//            {
//                loanStructure
//            };
//            applLoanStructureList.ForEach(s => context.LoanStructure.Add(s));
//            context.SaveChanges();


//            ApplicationDealerStage applicationDealerStage = new ApplicationDealerStage { ApplicationTransaction = applicationTransaction, branch = "70-1", CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var appldealerStageList = new List<ApplicationDealerStage>
//            {
//                applicationDealerStage
//            };
//            appldealerStageList.ForEach(s => context.ApplicationDealerStage.Add(s));
//            context.SaveChanges();



//            ApplicationContactStage applicationContactStage = new ApplicationContactStage { ApplicationTransaction = applicationTransaction, primaryFirstName = "Elton", primaryLastName = "John", CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var applcontactStageList = new List<ApplicationContactStage>
//            {
//                applicationContactStage
//            };
//            applcontactStageList.ForEach(s => context.ApplicationContactStage.Add(s));
//            context.SaveChanges();


//            ApplicationVehicleStage applicationVehicleStage = new ApplicationVehicleStage { ApplicationTransaction = applicationTransaction, make = "HONDA", model = "CIVIC", mileage = "75000", CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var applvehicleStageList = new List<ApplicationVehicleStage>
//            {
//                applicationVehicleStage
//            };
//            applvehicleStageList.ForEach(s => context.ApplicationVehicleStage.Add(s));
//            context.SaveChanges();


//            ApplicantCreditStage aplicantCreditStage = new ApplicantCreditStage { ApplicationTransaction = applicationTransaction, isHomeOwner = "Y", CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var applcreditStageList = new List<ApplicantCreditStage>
//            {
//                aplicantCreditStage
//            };
//            applcreditStageList.ForEach(s => context.ApplicantCreditStage.Add(s));
//            context.SaveChanges();


//            DecisionResults decisionResults = new DecisionResults { ApplicationTransaction = applicationTransaction, score = 76, recourse = 1, CreatedByID = 1, ModifiedByID = 1, CreatedDate = DateTime.Parse("2015-07-28"), ModifiedDate = DateTime.Parse("2015-07-28") };

//            var appldecisionresultList = new List<DecisionResults>
//            {
//                decisionResults
//            };
//            appldecisionresultList.ForEach(s => context.DecisionResults.Add(s));
//            context.SaveChanges();


//            // Contact

//            Contact contact1 = new Contact {ID = 1, FirstName = "John ", LastName = "Dalton"};
//            Contact contact2 = new Contact {ID = 2, FirstName = "Carson ", LastName = "Hayward"};



//            var contactList = new List<Contact>
//            {
//                contact1,
//                contact2
//            };
//            contactList.ForEach(s => context.Contact.Add(s));
//            context.SaveChanges();



//            SalesRep salesRep1 = new SalesRep
//            {
//                ID = 1,
//                Active = 1,
//                contact = contact1,
//                TermDate = DateTime.Parse("2005-09-01")
//            };
//            SalesRep salesRep2 = new SalesRep
//            {
//                ID = 2,
//                Active = 1,
//                contact = contact2,
//                TermDate = DateTime.Parse("2005-09-01")
//            };

//            var salesRepList = new List<SalesRep>
//            {
//                salesRep1,
//                salesRep2

//            };

//            salesRepList.ForEach(s => context.SalesRep.Add(s));
//            context.SaveChanges();



//            Dealer dealer1 = new Dealer {ID = 1, SalesRep = salesRep1, contact = contact2};

//            var dealerList = new List<Dealer>
//            {
//                dealer1

//            };

//            dealerList.ForEach(s => context.Dealer.Add(s));
//            context.SaveChanges();


//            // ApplicantType
//            var applicantTypeList = new List<ApplicantType>
//            {
//                new ApplicantType {Type = "Individual"},
//                new ApplicantType {Type = "Joint"}
//            };
//            applicantTypeList.ForEach(s => context.ApplicantType.Add(s));
//            context.SaveChanges();



//            ApplicationType applicationType1 = new ApplicationType {ID = 1, Type = "1"};
//            ApplicationType applicationType2 = new ApplicationType {ID = 2, Type = "2"};


//            // ApplicationType
//            var applicationTypeList = new List<ApplicationType>
//            {
//                applicationType1,
//                applicationType2
//            };
//            applicationTypeList.ForEach(s => context.ApplicationType.Add(s));
//            context.SaveChanges();




//            // ContractType
//            ContractType contractType1 = new ContractType {ID = 1, Type = "1"};
//            ContractType contractType2 = new ContractType {ID = 2, Type = "2"};

//            var contractTypeList = new List<ContractType>
//            {
//                contractType1,
//                contractType2

//            };
//            contractTypeList.ForEach(s => context.ContractType.Add(s));
//            context.SaveChanges();


//            // DecisionStatus
//            DecisionStatus ds1 = new DecisionStatus {ID = 1, Type = "Decision"};
//            DecisionStatus ds2 = new DecisionStatus {ID = 2, Type = "Approved"};
//            DecisionStatus ds3 = new DecisionStatus {ID = 2, Type = "Cancel Duplicate"};
//            DecisionStatus ds4 = new DecisionStatus {ID = 2, Type = "Counter Offer"};
//            DecisionStatus ds5 = new DecisionStatus {ID = 2, Type = "Declined"};
//            DecisionStatus ds6 = new DecisionStatus {ID = 2, Type = "Payment Call"};
//            DecisionStatus ds7 = new DecisionStatus {ID = 2, Type = "Pending"};





//            var decisionStatusList = new List<DecisionStatus>
//            {
//                ds1,
//                ds2,
//                ds3,
//                ds4,
//                ds5,
//                ds6,
//                ds7
//            };
//            decisionStatusList.ForEach(s => context.DecisionStatus.Add(s));
//            context.SaveChanges();



//            FundingStatus fundingstatus1 = new FundingStatus {ID = 1, Type = "Docs Received"};
//            FundingStatus fundingstatus2 = new FundingStatus {ID = 2, Type = "Funded"};
//            FundingStatus fundingstatus3 = new FundingStatus {ID = 3, Type = "Returned To Dealer"};
//            FundingStatus fundingstatus4 = new FundingStatus {ID = 3, Type = "Unable To Fund"};

//            // FundingStatus
//            var fundingStatusList = new List<FundingStatus>
//            {
//                fundingstatus1,
//                fundingstatus2,
//                fundingstatus3,
//                fundingstatus4
//            };
//            fundingStatusList.ForEach(s => context.FundingStatus.Add(s));
//            context.SaveChanges();


//            // SourceSystem

//            SourceSystem sourceSystem1 = new SourceSystem {ID = 1, Name = "1"};
//            SourceSystem sourceSystem2 = new SourceSystem {ID = 2, Name = "2"};

//            var sourceSystemList = new List<SourceSystem>
//            {

//                sourceSystem1,
//                sourceSystem2

//            };
//            sourceSystemList.ForEach(s => context.SourceSystem.Add(s));
//            context.SaveChanges();




//            Application a1 = new Application
//            {
//                ID = 1,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID=1,
//                modifiedByID=1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds1,
//                FundingStatus = fundingstatus1,
//                ApplicationType = applicationType1,
//                ContractType = contractType1,
//                SourceSystem = sourceSystem1,
//                Dealer = dealer1,
//                SalesRep = salesRep1
//            };


//            Application a2 = new Application
//            {
//                ID = 2,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID = 1,
//                modifiedByID = 1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds2,
//                FundingStatus = fundingstatus2,
//                ApplicationType = applicationType2,
//                ContractType = contractType2,
//                SourceSystem = sourceSystem2,
//                Dealer = dealer1,
//                SalesRep = salesRep2
//            };

//            Application a3 = new Application
//            {
//                ID = 3,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID = 1,
//                modifiedByID = 1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds2,
//                FundingStatus = fundingstatus2,
//                ApplicationType = applicationType2,
//                ContractType = contractType2,
//                SourceSystem = sourceSystem2,
//                Dealer = dealer1,
//                SalesRep = salesRep1
//            };


//            Application a4 = new Application
//            {
//                ID = 4,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID = 1,
//                modifiedByID = 1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds2,
//                FundingStatus = fundingstatus3,
//                ApplicationType = applicationType2,
//                ContractType = contractType2,
//                SourceSystem = sourceSystem2,
//                Dealer = dealer1,
//                SalesRep = salesRep2
//            };



//            Application a5 = new Application
//            {
//                ID = 5,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID = 1,
//                modifiedByID = 1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds2,
//                FundingStatus = fundingstatus3,
//                ApplicationType = applicationType2,
//                ContractType = contractType2,
//                SourceSystem = sourceSystem2,
//                Dealer = dealer1,
//                SalesRep = salesRep1
//            };

//            Application a6 = new Application
//            {
//                ID = 6,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID = 1,
//                modifiedByID = 1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds2,
//                FundingStatus = fundingstatus3,
//                ApplicationType = applicationType2,
//                ContractType = contractType2,
//                SourceSystem = sourceSystem2,
//                Dealer = dealer1,
//                SalesRep = salesRep1
//            };

//            Application a7 = new Application
//            {
//                ID = 7,
//                ApplicationEnteredDate = DateTime.Parse("2015-03-01"),
//                ContractDate = DateTime.Parse("2015-03-01"),
//                appDate = DateTime.Parse("2015-03-01"),
//                appTime = DateTime.Parse("2015-03-01"),
//                createdByID = 1,
//                modifiedByID = 1,
//                createdDate = DateTime.Parse("2015-03-01"),
//                modifiedDate = DateTime.Parse("2015-03-01"),
//                DecisionStatus = ds2,
//                FundingStatus = fundingstatus3,
//                ApplicationType = applicationType2,
//                ContractType = contractType2,
//                SourceSystem = sourceSystem2,
//                Dealer = dealer1,
//                SalesRep = salesRep1
//            };



//            // Application
//            var applicationList = new List<Application>
//            {
//                a1,
//                a2,
//                a3,
//                a4,
//                a5,
//                a6,
//                a7,
//            };
//            applicationList.ForEach(s => context.Application.Add(s));
//            context.SaveChanges();


//            // Application Status


//            // SourceSystem

//            ApplicationStatus appStatus1 = new ApplicationStatus {Application = a1, DPCStatusID = 1};

//            ApplicationStatus appStatus2 = new ApplicationStatus {Application = a2, DPCStatusID = 2};

//            var appStatusList = new List<ApplicationStatus>
//            {

//                appStatus1,
//                appStatus2

//            };
//            appStatusList.ForEach(s => context.ApplicationStatus.Add(s));
//            context.SaveChanges();

//            // Expression Catalog

           


            
//        }


//    }
//}