//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Threading.Tasks;


//using DealerPortalCRM.DAService;
//using DealerPortalCRM.ViewModels;
//using DealerPortalCRM.Constants;
//using Com.CoreLane.ScoringEngine.Models;


//namespace DealerPortalCRM.DAServiceImpl
//{


    


//    public class ApplicationManager: IApplication
//    {

//        const int NEW_APPLICATION_STATUS = 1;
//        const int PENDING_STATUS = 2;
//        const int FUNDED_APPLICATION_STATUS = 3;
//        const int DECLINE_APPLICATION_STATUS = 4;
//        const int COUNTER_OFFER_APPLICATION_STATUS = 5;
//        const int IN_PROGRESS_STATUS = 6;


     
//        private readonly ScoringEngineEntities _dbContext;

//        public ApplicationManager(ScoringEngineEntities db)
//        {
//            this._dbContext = db;
//        }

//        public async Task<List<Application>> GetAllApplicationsAsync()
//        {

//            return  await _dbContext.Application.ToListAsync();

//        }

//        public List<Application> GetAllApplications()
//        {

//            return  _dbContext.Application.ToList();

//        }


//        public List<SalesDashboardViewModel> getSalesDashDealerGroups()
//        {

//            List<SalesDashboardViewModel> dealerDashBoardList = new List<SalesDashboardViewModel>();

//               var query = _dbContext.Application.GroupBy(a => new { a.Dealer })

//                                      .Select(g => new ViewModels.SalesDashBoardTest
//                                      {

//                                          DealerID = g.Key.Dealer.ID,
//                                                     DealerName = g.Key.Dealer.contact.FirstName + " " + g.Key.Dealer.contact.LastName ,
//                                          TotalCount = g.Sum(e => e.FundingStatus.ID)
//                                      })
//                                      .OrderBy(c => c.DealerID ) ;
                                      
//            var results = query.ToList();

//            List<FundingStatus> fundingStatusList = _dbContext.FundingStatus.ToList();
//             foreach (FundingStatus fundingStatus in fundingStatusList)
//             {

//                 var groupByFundingQuery = _dbContext.Application.GroupBy(a => new { a.Dealer, a.FundingStatus })

//                                           .Select(g => new ViewModels.SalesDashBoardTest
//                                           {

//                                               FundingStatusID = g.Key.FundingStatus.ID,
//                                               DealerID = g.Key.Dealer.ID,
//                                               DealerName = g.Key.Dealer.contact.FirstName + " " + g.Key.Dealer.contact.LastName,
//                                               TotalCount = g.Sum(e => e.FundingStatus.ID)
//                                           })
//                                           .OrderBy(c => c.DealerID).ThenBy(c => c.FundingStatusID)
//                                           .Where(w => w.FundingStatusID == fundingStatus.ID);



//                 var groupByFundingResults = query.ToList();
//                 foreach (SalesDashBoardTest mainSalesDashBoardTest in results)
//                 {
//                     SalesDashboardViewModel salesDashboardViewModel = new SalesDashboardViewModel();
//                     int index = results.IndexOf(mainSalesDashBoardTest);
//                     SalesDashBoardTest temp = groupByFundingResults[index];
//                     if (mainSalesDashBoardTest.DealerID == temp.DealerID)
//                     {

//                         mainSalesDashBoardTest.FundingStatusUnabletoFundTotal += temp.FundingStatusUnabletoFundTotal;
//                         mainSalesDashBoardTest.FundingStatusPendingTotal += temp.FundingStatusPendingTotal;
//                         mainSalesDashBoardTest.FundingStatusDocsReceivedTotal += temp.FundingStatusDocsReceivedTotal;

//                     }

//                     salesDashboardViewModel.DealerName = mainSalesDashBoardTest.DealerName;
//                     salesDashboardViewModel.TotalPending = mainSalesDashBoardTest.FundingStatusPendingTotal;
//                     salesDashboardViewModel.TotalNew = mainSalesDashBoardTest.FundingStatusUnabletoFundTotal;
//                     salesDashboardViewModel.TotalNew = mainSalesDashBoardTest.FundingStatusUnabletoFundTotal;
//                     dealerDashBoardList.Add(salesDashboardViewModel);
//                 }

                
//             }

//             return dealerDashBoardList;
//        }


//        public List<SalesDashboardDetailViewModel> getSalesDashBoardDetail()
//        {


//            var query = _dbContext.Application

//                                       .Select(g => new ViewModels.SalesDashboardDetailViewModel
//                                       {
//                                          ApplicationID = g.ID ,
//                                          DateOpened = g.ApplicationEnteredDate,
//                                          SalesRepID = g.SalesRep.ID,
//                                          SalesRepName = g.SalesRep.contact.FirstName + " " + g.SalesRep.contact.LastName ,
//                                          DealerID  = g.Dealer.ID,
//                                          DealerName = g.Dealer.contact.FirstName + " " + g.Dealer.contact.LastName,
//                                          Status = g.DecisionStatus.Type,
                                          

//                                       })
//                                       .OrderBy(c => c.SalesRepID)
                                      
//                                       ;

//            var results = query.ToList();



//            List<SalesDashboardDetailViewModel> deatailList = new List<SalesDashboardDetailViewModel>();
//            return deatailList;
//        }
        



//        public SalesDashboardViewModel getSalesDashBoard(){


//            SalesDashboardViewModel salesDashboardViewModel = new SalesDashboardViewModel();


//            // Q1
//            int newApplicationsCount = _dbContext.Database.SqlQuery<int>("SELECT  COUNT(Applications.ID) FROM  Applications INNER JOIN ApplicationStatus " +
//                                 " ON Applications.ID = ApplicationStatus.Application_id  WHERE ApplicationStatus.DPCStatusID = 1").FirstOrDefault<int>();

//            // Q2
//            int pendingApplicationsCount = _dbContext.Database.SqlQuery<int>("SELECT  COUNT(Applications.ID) FROM  Applications INNER JOIN ApplicationStatus " +
//                                 " ON Applications.ID = ApplicationStatus.Application_id  WHERE ApplicationStatus.DPCStatusID = 2").FirstOrDefault<int>();

//            // Q3
//            int fundedApplicationsCount = _dbContext.Database.SqlQuery<int>("SELECT  COUNT(Applications.ID) FROM  Applications INNER JOIN ApplicationStatus " +
//                               " ON Applications.ID = ApplicationStatus.Application_id  WHERE ApplicationStatus.DPCStatusID = 3").FirstOrDefault<int>();

//            // Q4
//            int declineOfferApplicationsCount = _dbContext.Database.SqlQuery<int>("SELECT  COUNT(Applications.ID) FROM  Applications INNER JOIN ApplicationStatus " +
//                            " ON Applications.ID = ApplicationStatus.Application_id  WHERE ApplicationStatus.DPCStatusID = 4").FirstOrDefault<int>();

//            // Q5
//            int counterOfferApplicationsCount = _dbContext.Database.SqlQuery<int>("SELECT  COUNT(Applications.ID) FROM  Applications INNER JOIN ApplicationStatus " +
//                             " ON Applications.ID = ApplicationStatus.Application_id  WHERE ApplicationStatus.DPCStatusID = 5").FirstOrDefault<int>();
//            // Q6
//            int inProgressApplicationsCount = _dbContext.Database.SqlQuery<int>("SELECT  COUNT(Applications.ID) FROM  Applications INNER JOIN ApplicationStatus " +
//                          " ON Applications.ID = ApplicationStatus.Application_id  WHERE ApplicationStatus.DPCStatusID = 6").FirstOrDefault<int>();

            
//            salesDashboardViewModel.TotalNew = newApplicationsCount;
//            salesDashboardViewModel.TotalFunded = fundedApplicationsCount;
//            salesDashboardViewModel.TotalCounterOffers = counterOfferApplicationsCount;
//            salesDashboardViewModel.TotalPending = pendingApplicationsCount;
//            salesDashboardViewModel.TotalInProgress = inProgressApplicationsCount;

//            return salesDashboardViewModel;
//        }


      


//       // Refactor - Condense the loops into generic functions
//        Dictionary<int, SalesDashboardViewModel> getSalesDashboardViewModels(String query, int[] applicationStatusArray)
//        {
//            List<SalesRepQueryModel> queryModelList  = null;  // Initialize query model list
//            object[] parameters = { 0 };  // Initialize the parameters array that will be passed to the query
//            Dictionary<int, SalesDashboardViewModel> salesDashboardViewModelDictionary = new Dictionary<int, SalesDashboardViewModel>();

//            /*  For each application Status passed in , run the query*/
//            foreach (int applicationStatus in applicationStatusArray)
//            {

//                parameters[0] = applicationStatus;
//                queryModelList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(query, parameters).ToList<SalesRepQueryModel>();




//            }
//            return salesDashboardViewModelDictionary;
//        }


//        public List<SalesDashboardViewModel> getSalesDashBoardBySalesRep()
//        {
//            // Initialize variables 
//            List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//            List<SalesRepQueryModel> salesRepList = null;
//            SalesDashboardViewModel appStatus = null;

//            String salesRepQuery = "Select  A.ApplicationStatus as ApplicationStatus, A.ID as SalesRepID ,  A.appCount as ApplicationCount , Contacts.FullName  as FullName  " +
//                " FROM " +
//              " (SELECT  COUNT(Applications.ID) as appCount, ApplicationStatus.DPCStatusID as ApplicationStatus, SalesReps.ID as ID  " +
//              " FROM  dbo.[Applications] INNER JOIN dbo.[ApplicationStatus]  " +
//             " ON   Applications.ID = ApplicationStatus.Application_id   " +
//             " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID   " +
//             " INNER JOIN dbo.Contacts ON Contacts.ID = SalesReps.contact_ID  " +
//              " WHERE  ApplicationStatus.DPCStatusID <> 4 " +
//             " GROUP BY ApplicationStatus.DPCStatusID, SalesReps.ID ) A INNER JOIN dbo.SalesReps on SalesReps.ID = A.ID " +
//             " INNER JOIN dbo.Contacts ON SalesReps.contact_ID = Contacts.ID ";

//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery).ToList<SalesRepQueryModel>();

//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {

//                    appStatus = new SalesDashboardViewModel();
//                    populateModel(ref appStatus, salesRepQueryModel);
//                    resultList.Add(appStatus);
                
//            }

//            return resultList;
//        }


//        public List<SalesDashboardViewModel> getSalesDashBoardBySalesRep(int SalesRepID)
//        {

//            // Initialize variables 
//            List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//            List<SalesRepQueryModel> salesRepList = null;
//            SalesDashboardViewModel appStatus = null;
//            object[] parameters = { NEW_APPLICATION_STATUS, SalesRepID };

//            Dictionary<int, SalesDashboardViewModel> dealerDictionary = new Dictionary<int, SalesDashboardViewModel>();


//            String salesRepQuery = "  Select A.ID as SalesRepID ,  A.appCount as ApplicationCount , Contacts.FullName  as FullName " +
//              " FROM " +
//             " (SELECT  COUNT(Applications.ID) as appCount, SalesReps.Contact_ID as ID " +
//             " FROM  dbo.[Applications] INNER JOIN dbo.[ApplicationStatus] " +
//             " ON   Applications.ID = ApplicationStatus.Application_id  " +
//             " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//             " INNER JOIN dbo.Contacts ON Contacts.ID = SalesReps.contact_ID " +
//             " WHERE ApplicationStatus.DPCStatusID = {0}  " +
//             " AND SalesReps.ID = {1} " +
//             " GROUP BY SalesReps.Contact_ID) A INNER JOIN dbo.Contacts ON Contacts.ID = A.ID ";


//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//            displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();



//            // New Status
//            parameters[0] = NEW_APPLICATION_STATUS;
//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();


//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {
//                appStatus = new SalesDashboardViewModel();
//                appStatus.SalesRepID = salesRepQueryModel.SalesRepID;
//                appStatus.SalesRepName = salesRepQueryModel.FullName;
//                appStatus.TotalNew = salesRepQueryModel.ApplicationCount;
//                dealerDictionary.Add(salesRepQueryModel.SalesRepID, appStatus);
//            }



//            // Pending Status
//            parameters[0] = PENDING_STATUS;
//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();
//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {

//                if (dealerDictionary.ContainsKey(salesRepQueryModel.SalesRepID))
//                {
//                    appStatus = dealerDictionary[salesRepQueryModel.SalesRepID];
//                    appStatus.TotalPending = salesRepQueryModel.ApplicationCount;
//                }
//                else
//                {

//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.SalesRepID = salesRepQueryModel.SalesRepID;
//                    appStatus.SalesRepName = salesRepQueryModel.FirstName + " " + salesRepQueryModel.LastName;
//                    appStatus.TotalPending = salesRepQueryModel.ApplicationCount;
//                    dealerDictionary.Add(salesRepQueryModel.SalesRepID, appStatus);

//                }




//            }

//            // Funded Application Status
//            parameters[0] = FUNDED_APPLICATION_STATUS;
//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();
//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {

//                if (dealerDictionary.ContainsKey(salesRepQueryModel.SalesRepID))
//                {
//                    appStatus = dealerDictionary[salesRepQueryModel.SalesRepID];
//                    appStatus.TotalFunded = salesRepQueryModel.ApplicationCount;
//                }

//            }

//            // Decline  Application Status
//            parameters[0] = DECLINE_APPLICATION_STATUS;
//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();
//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {

//                if (dealerDictionary.ContainsKey(salesRepQueryModel.SalesRepID))
//                {
//                    appStatus = dealerDictionary[salesRepQueryModel.SalesRepID];
//                  //  appStatus.TotalNew = salesRepQueryModel.ApplicationCount;
//                }

//            }



//            // COUNTER_OFFER_APPLICATION_STATUS
//            parameters[0] = COUNTER_OFFER_APPLICATION_STATUS;
//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();
//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {

//                if (dealerDictionary.ContainsKey(salesRepQueryModel.SalesRepID))
//                {
//                    appStatus = dealerDictionary[salesRepQueryModel.SalesRepID];
//                    appStatus.TotalCounterOffers = salesRepQueryModel.ApplicationCount;
//                }

//            }


//            // COUNTER_OFFER_APPLICATION_STATUS
//            parameters[0] = IN_PROGRESS_STATUS;
//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();
//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {

//                if (dealerDictionary.ContainsKey(salesRepQueryModel.SalesRepID))
//                {
//                    appStatus = dealerDictionary[salesRepQueryModel.SalesRepID];
//                    appStatus.TotalInProgress = salesRepQueryModel.ApplicationCount;
//                }

//            }



//            foreach (var item in dealerDictionary)
//            {
//                resultList.Add(item.Value);
//            }


//            return resultList;
//        }

//        public List<SalesDashboardViewModel> getSalesDashBoardBySalesRep(int SalesRepID, int ApplicationStatusID)
//        {

//            // Initialize variables 
//            List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//            List<SalesRepQueryModel> salesRepList = null;
//            SalesDashboardViewModel appStatus = null;
//            object[] parameters = { ApplicationStatusID, SalesRepID };

//            Dictionary<int, SalesDashboardViewModel> dealerDictionary = new Dictionary<int, SalesDashboardViewModel>();


//            String salesRepQuery = "  Select A.ID as SalesRepID ,  A.appCount as ApplicationCount , Contacts.FirstName  as FirstName, Contacts.LastName as LastName " +
//              " FROM " +
//             " (SELECT  COUNT(Applications.ID) as appCount, SalesReps.Contact_ID as ID " +
//             " FROM  dbo.[Applications] INNER JOIN dbo.[ApplicationStatus] " +
//             " ON   Applications.ID = ApplicationStatus.Application_id  " +
//             " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//             " INNER JOIN dbo.Contacts ON Contacts.ID = SalesReps.contact_ID " +
//             " WHERE ApplicationStatus.DPCStatusID = {0}  " +
//             " AND SalesReps.ID = {1} " +
//             " GROUP BY SalesReps.Contact_ID) A INNER JOIN dbo.Contacts ON Contacts.ID = A.ID ";


//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//            displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();

//            salesRepList = _dbContext.Database.SqlQuery<SalesRepQueryModel>(salesRepQuery, parameters).ToList<SalesRepQueryModel>();

//            foreach (SalesRepQueryModel salesRepQueryModel in salesRepList)
//            {
//                appStatus = new SalesDashboardViewModel();
//                appStatus.SalesRepID = salesRepQueryModel.SalesRepID;
//                appStatus.SalesRepName = salesRepQueryModel.FirstName + " " + salesRepQueryModel.LastName;
//                populateModel(ref appStatus, ApplicationStatusID, salesRepQueryModel.ApplicationCount);

//                appStatus.TotalNew = salesRepQueryModel.ApplicationCount;
//                dealerDictionary.Add(salesRepQueryModel.SalesRepID, appStatus);
//            }




//            foreach (var item in dealerDictionary)
//            {
//                resultList.Add(item.Value);
//            }


//            return resultList;
//        }





//        private void populateModel(ref SalesDashboardViewModel viewModel, DealerQueryModel dealerQueryModel)
//        {

//            viewModel.DealerName = dealerQueryModel.DealerFullName;
//            viewModel.DealerID = dealerQueryModel.DealerID;
//            viewModel.DealerAccountNumber = dealerQueryModel.DealerNumber;

//            switch (dealerQueryModel.ApplicationStatus)
//            {
//                case NEW_APPLICATION_STATUS:
//                    viewModel.TotalNew = dealerQueryModel.ApplicationCount;
       
//                    break;
//                case PENDING_STATUS:
//                    viewModel.TotalPending = dealerQueryModel.ApplicationCount;
//                    break;
//                case FUNDED_APPLICATION_STATUS:
//                    viewModel.TotalFunded = dealerQueryModel.ApplicationCount;
//                    break;
//                case DECLINE_APPLICATION_STATUS:

//                    break;
//                case COUNTER_OFFER_APPLICATION_STATUS:
//                    viewModel.TotalCounterOffers = dealerQueryModel.ApplicationCount;
//                    break;
//                case IN_PROGRESS_STATUS:
//                    viewModel.TotalInProgress = dealerQueryModel.ApplicationCount;
//                    break;
//                default:

//                    break;
//            }

         


//        }
//        private void populateModel(ref SalesDashboardViewModel viewModel, SalesRepQueryModel salesRepQueryModel)
//        {


//            viewModel.SalesRepName = salesRepQueryModel.FullName;
//            viewModel.SalesRepID = salesRepQueryModel.SalesRepID;

//            switch (salesRepQueryModel.ApplicationStatus)
//            {
//                case NEW_APPLICATION_STATUS:
//                    viewModel.TotalNew = salesRepQueryModel.ApplicationCount;
       
//                    break;
//                case PENDING_STATUS:
//                    viewModel.TotalPending = salesRepQueryModel.ApplicationCount;
//                    break;
//                case FUNDED_APPLICATION_STATUS:
//                    viewModel.TotalFunded = salesRepQueryModel.ApplicationCount;
//                    break;
//                case DECLINE_APPLICATION_STATUS:

//                    break;
//                case COUNTER_OFFER_APPLICATION_STATUS:
//                    viewModel.TotalCounterOffers = salesRepQueryModel.ApplicationCount;
//                    break;
//                case IN_PROGRESS_STATUS:
//                    viewModel.TotalInProgress = salesRepQueryModel.ApplicationCount;
//                    break;
//                default:

//                    break;
//            }

//            /*
//                        const int NEW_APPLICATION_STATUS = 1;
//                        const int PENDING_STATUS = 2;
//                        const int FUNDED_APPLICATION_STATUS = 3;
//                        const int DECLINE_APPLICATION_STATUS = 4;
//                        const int COUNTER_OFFER_APPLICATION_STATUS = 5;
//                        const int IN_PROGRESS_STATUS = 6;
//             * */

//        }

//        private void  populateModel( ref SalesDashboardViewModel viewModel, int ApplicationStatusID, int appCount){

//            switch (ApplicationStatusID)
//            {
//                case NEW_APPLICATION_STATUS:
//                    viewModel.TotalNew = appCount;
//                    break;
//                case PENDING_STATUS:
//                    viewModel.TotalPending = appCount;
//                    break;
//                case FUNDED_APPLICATION_STATUS:
//                    viewModel.TotalFunded = appCount;
//                    break;
//                case DECLINE_APPLICATION_STATUS:

//                    break;
//                case COUNTER_OFFER_APPLICATION_STATUS:
//                    viewModel.TotalCounterOffers = appCount;
//                    break;
//                case IN_PROGRESS_STATUS :
//                    viewModel.TotalInProgress = appCount;
//                    break;
//                default:
                   
//                    break;
//            }

///*
//            const int NEW_APPLICATION_STATUS = 1;
//            const int PENDING_STATUS = 2;
//            const int FUNDED_APPLICATION_STATUS = 3;
//            const int DECLINE_APPLICATION_STATUS = 4;
//            const int COUNTER_OFFER_APPLICATION_STATUS = 5;
//            const int IN_PROGRESS_STATUS = 6;
// * */

//        }





//        public List<SalesDashboardViewModel> getSalesDashBoardByDealer()
//        {
//            // querry 102
//            // Initialize variables 
//            List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//            List<DealerQueryModel> dealerList = null;
//            SalesDashboardViewModel appStatus = null;


//            String salesRepQuery = " Select A.ID as DealerID ,  A.appCount as ApplicationCount , A.ApplicationStatus as ApplicationStatus, " +
//                    " Contacts.FullName as DealerFullName , Contacts.ContactIdentifier as DealerNumber " +
//                    " FROM " +
//                    " (SELECT  COUNT(Applications.ID) as appCount ,ApplicationStatus.DPCStatusID as ApplicationStatus , Dealers.ID as ID   " +
//                    " FROM  dbo.[Applications] INNER JOIN dbo.[ApplicationStatus]  " +
//                    " ON   Applications.ID = ApplicationStatus.Application_id   " +
//                    " INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID   " +
//                    " GROUP BY ApplicationStatus.DPCStatusID , Dealers.ID)  A INNER JOIN dbo.Dealers ON Dealers.ID = A.ID " +
//                    " INNER JOIN  dbo.Contacts ON Contacts.ID = Dealers.Contact_ID ";



//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(salesRepQuery).ToList<DealerQueryModel>();

//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerQueryModel.ApplicationStatus != 4)  // Skip Cancelled 
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    populateModel(ref appStatus, dealerQueryModel);
//                    resultList.Add(appStatus);
//                }
//            }

//            return resultList;

//        }



//       public  List<SalesDashboardViewModel> getSalesDashBoardByDealer(int DealerID)
//       {
//           // query 1007 
//           // Initialize variables 
//           List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//           List<DealerQueryModel> dealerList = null;
//           SalesDashboardViewModel appStatus = null;
//           object[] parameters = { NEW_APPLICATION_STATUS, DealerID };

//           Dictionary<int, SalesDashboardViewModel> dealerDictionary = new Dictionary<int, SalesDashboardViewModel>();

//           String dealerQuery = " Select A.ID as DealerID ,  A.appCount as ApplicationCount , Contacts.FullName as DealerFullName " +
//            " FROM " +
//            " (SELECT  COUNT(Applications.ID) as appCount, Dealers.ID as ID  " +
//            " FROM  dbo.[Applications] INNER JOIN dbo.[ApplicationStatus]  " +
//            " ON   Applications.ID = ApplicationStatus.Application_id   " +
//            " INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID   " +
//            " INNER JOIN dbo.Contacts ON Contacts.ID = Dealers.contact_ID  " +
//            " WHERE ApplicationStatus.DPCStatusID = {0}   " +
//            " AND  Dealers.ID = {1}" +
//            " GROUP BY Dealers.ID) A INNER JOIN dbo.Dealers ON Dealers.ID = A.ID " +
//            " INNER JOIN  dbo.Contacts ON Contacts.ID = Dealers.Contact_ID";

//           SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//           displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();


//           // New Status
//           parameters[0] = NEW_APPLICATION_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();


//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//               {
//                   appStatus = dealerDictionary[dealerQueryModel.DealerID];
//               }
//               else
//               {
//                   appStatus = new SalesDashboardViewModel();
//                   appStatus.DealerID = dealerQueryModel.DealerID;
//                   dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//               }
//               appStatus.DealerName = dealerQueryModel.DealerFullName;
//               appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//           }



//           // Pending Status
//           parameters[0] = PENDING_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//               {
//                   appStatus = dealerDictionary[dealerQueryModel.DealerID];
//               }
//               else
//               {
//                   appStatus = new SalesDashboardViewModel();
//                   appStatus.DealerID = dealerQueryModel.DealerID;
//                   dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//               }
//               appStatus.DealerName = dealerQueryModel.DealerFullName;
//               appStatus.TotalPending = dealerQueryModel.ApplicationCount;

//           }

//           // Funded Application Status
//           parameters[0] = FUNDED_APPLICATION_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//               {
//                   appStatus = dealerDictionary[dealerQueryModel.DealerID];
//               }
//               else
//               {
//                   appStatus = new SalesDashboardViewModel();
//                   appStatus.DealerID = dealerQueryModel.DealerID;
//                   dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//               }
//               appStatus.DealerName = dealerQueryModel.DealerFullName;
//               appStatus.TotalFunded = dealerQueryModel.ApplicationCount;

//           }

//           // Decline  Application Status
//  /*
//           parameters[0] = DECLINE_APPLICATION_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//               {
//                   appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                 //  appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//               }

//           }

//*/

//           // COUNTER_OFFER_APPLICATION_STATUS
//           parameters[0] = COUNTER_OFFER_APPLICATION_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//               {
//                   appStatus = dealerDictionary[dealerQueryModel.DealerID];
//               }
//               else
//               {
//                   appStatus = new SalesDashboardViewModel();
//                   appStatus.DealerID = dealerQueryModel.DealerID;
//                   dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//               }
//               appStatus.DealerName = dealerQueryModel.DealerFullName;
//               appStatus.TotalCounterOffers = dealerQueryModel.ApplicationCount;

//           }


//           // IN PROGRESS
//           parameters[0] = IN_PROGRESS_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//               {
//                   appStatus = dealerDictionary[dealerQueryModel.DealerID];
//               }
//               else
//               {
//                   appStatus = new SalesDashboardViewModel();
//                   appStatus.DealerID = dealerQueryModel.DealerID;
//                   dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//               }
//               appStatus.DealerName = dealerQueryModel.DealerFullName;
//               appStatus.TotalInProgress = dealerQueryModel.ApplicationCount;

//           }


//           foreach (var item in dealerDictionary)
//           {
//               resultList.Add(item.Value);
//           }


//           return resultList;
//       }


//       public  List<SalesDashboardViewModel> getSalesDashBoardByDealer(int DealerID, int ApplicationStatusID)
//       {

//           // Initialize variables 
//           List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//           List<DealerQueryModel> dealerList = null;
//           SalesDashboardViewModel appStatus = null;
//           object[] parameters = { ApplicationStatusID, DealerID };

//           Dictionary<int, SalesDashboardViewModel> dealerDictionary = new Dictionary<int, SalesDashboardViewModel>();

//           String dealerQuery = " Select A.ID as DealerID ,  A.appCount as appCount , Contacts.FirstName  as FirstName, Contacts.LastName as LastName " +
//            " FROM " +
//            " (SELECT  COUNT(Applications.ID) as appCount, Dealers.Contact_ID as ID  " +
//            " FROM  dbo.[Applications] INNER JOIN dbo.[ApplicationStatus]  " +
//            " ON   Applications.ID = ApplicationStatus.Application_id   " +
//            " INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID   " +
//            " INNER JOIN dbo.Contacts ON Contacts.ID = Dealers.contact_ID  " +
//            " WHERE ApplicationStatus.DPCStatusID = {0}   " +
//            " AND  Dealers.ID = {1}" +
//            " GROUP BY Dealers.Contact_ID) A INNER JOIN dbo.Contacts ON Contacts.ID = A.ID ";

//           SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//           displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();


//           // New Status
//           parameters[0] = NEW_APPLICATION_STATUS;
//           dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();


//           foreach (DealerQueryModel dealerQueryModel in dealerList)
//           {

//               appStatus = new SalesDashboardViewModel();
//               appStatus.DealerID = dealerQueryModel.DealerID;
//               appStatus.DealerName = dealerQueryModel.FirstName + " " + dealerQueryModel.FirstName;
//               appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//               dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//           }



//           foreach (var item in dealerDictionary)
//           {
//               resultList.Add(item.Value);
//           }


//           return resultList;
//       }

//       public List<SalesDashboardViewModel> getSalesDashBoardByAll()
//       {
//           List<SalesDashboardViewModel> dealerList = getSalesDashBoardByDealer();

//           List<SalesDashboardViewModel> salesRepList = getSalesDashBoardBySalesRep();

//           List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//           resultList.AddRange(dealerList);
//           resultList.AddRange(salesRepList);

//           return resultList;
//       }

                      
                                  


//       public List<SalesDashboardDetailViewModel> getApplicationsBySalesRep(int ApplicationStatusID)
//       {

//           object[] parameters = { ApplicationStatusID };
//           List<SalesDashboardDetailViewModel> resultList = new List<SalesDashboardDetailViewModel>();
//           SalesDashboardDetailViewModel salesDashboardDetailViewModel = null;

//           String salesRepQuery = " SELECT   Applications.ID , Applications.ApplicationNumber," +
//                " Applications.ApplicationEnteredDate, '' as DecisionStatus, '' as FundingStatus " + 
//              // " DecisionStatus.[Type]," +
//              // " FundingStatus.[Type]," +
//               " S.FullName  as SalesRepName, " +
//               " D.FullName as  DealerName " +
//               " FROM  Applications INNER JOIN ApplicationStatus  " +
//               " ON   Applications.ID = ApplicationStatus.Application_id  " +
//               " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//                "   INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID  " +
//               " INNER JOIN dbo.Contacts S ON S.ID = SalesReps.contact_ID " + 
//             " INNER JOIN dbo.Contacts D ON D.ID = Dealers.contact_ID  " +
//            //   " INNER JOIN dbo.DecisionStatus ON DecisionStatus.ID = Applications.DecisionStatus_ID " +
//             //  " INNER JOIN dbo.FundingStatus ON FundingStatus.ID = Applications.FundingStatus_ID " +
//               " WHERE ApplicationStatus.DPCStatusID = {0}  ";
//           List<ApplicationQueryModel> applicationList = _dbContext.Database.SqlQuery<ApplicationQueryModel>(salesRepQuery, parameters).ToList<ApplicationQueryModel>();


//           foreach (ApplicationQueryModel appQueryModel in applicationList)
//           {
//               salesDashboardDetailViewModel = new SalesDashboardDetailViewModel();


//               salesDashboardDetailViewModel.ApplicationID = appQueryModel.ApplicationNumber;
//               salesDashboardDetailViewModel.DateOpened = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.StatusDate = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.SalesRepName = appQueryModel.SalesRepName;
//               salesDashboardDetailViewModel.DealerName = appQueryModel.DealerName;
            


//               resultList.Add(salesDashboardDetailViewModel);
//           }


//           return resultList;
//       }
      
//        public List<SalesDashboardDetailViewModel> getApplicationsBySalesRep(int SalesRepID, int ApplicationStatusID)
//       {

//            //905

//           object[] parameters = {SalesRepID,  ApplicationStatusID  };
//           List<SalesDashboardDetailViewModel> resultList = new List<SalesDashboardDetailViewModel>();
//           SalesDashboardDetailViewModel salesDashboardDetailViewModel = null;

//           String salesRepQuery = " SELECT   Applications.ID , Applications.ApplicationNumber," +
//                " Applications.ApplicationEnteredDate,   '' as DecisionStatus , '' as  FundingStatus ," +
//           //    " DecisionStatus.[Type]," +
//            //   " FundingStatus.[Type]," +
//               " S.FullName  as SalesRepName, " +
//               " D.FullName as DealerName " +
//               " FROM  Applications INNER JOIN ApplicationStatus  " +
//               " ON   Applications.ID = ApplicationStatus.Application_id  " +
//                " INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID "+
//               " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//                " INNER JOIN dbo.Contacts S ON S.ID = SalesReps.contact_ID " +
//                " INNER JOIN dbo.Contacts D ON D.ID = Dealers.contact_ID " +


//             //  " INNER JOIN dbo.DecisionStatus ON DecisionStatus.ID = Applications.DecisionStatus_ID " +
//             //  " INNER JOIN dbo.FundingStatus ON FundingStatus.ID = Applications.FundingStatus_ID " +
//               " WHERE  SalesReps.ID = {0} AND " + 
//                " ApplicationStatus.DPCStatusID = {1}  ";
//           List<ApplicationQueryModel> applicationList = _dbContext.Database.SqlQuery<ApplicationQueryModel>(salesRepQuery, parameters).ToList<ApplicationQueryModel>();


//           foreach (ApplicationQueryModel appQueryModel in applicationList)
//           {
//               salesDashboardDetailViewModel = new SalesDashboardDetailViewModel();


//               salesDashboardDetailViewModel.ApplicationID = appQueryModel.ApplicationNumber;
//               salesDashboardDetailViewModel.DateOpened = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.StatusDate = appQueryModel.ApplicationEnteredDate; 
//               salesDashboardDetailViewModel.SalesRepName = appQueryModel.SalesRepName;
//               salesDashboardDetailViewModel.DealerName = appQueryModel.DealerName;
//               resultList.Add(salesDashboardDetailViewModel);
//           }


//           return resultList;


//       }


//       public List<SalesDashboardDetailViewModel> getApplicationsByDealer(int DealerID)
//       {
//           object[] parameters = { DealerID };
//           List<SalesDashboardDetailViewModel> resultList = new List<SalesDashboardDetailViewModel>();
//           SalesDashboardDetailViewModel salesDashboardDetailViewModel = null;

//           String salesRepQuery = " SELECT   Applications.ID , Applications.ApplicationNumber," +
//                " Applications.ApplicationEnteredDate, " +
//               " DecisionStatus.[Type]," +
//               " FundingStatus.[Type]," +
//               " S.FirstName  + S.LastName as SalesRepName, " +
//               " D.FirstName + D.LastName as DealerName " +
//               " FROM  Applications INNER JOIN ApplicationStatus  " +
//               " ON   Applications.ID = ApplicationStatus.Application_id  " +
//               " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//               " INNER JOIN dbo.Contacts S ON S.ID = SalesReps.contact_ID " +
//               " INNER JOIN dbo.Contacts D ON D.ID = Applications.Dealer_ID " +
//               " INNER JOIN dbo.DecisionStatus ON DecisionStatus.ID = Applications.DecisionStatus_ID " +
//               " INNER JOIN dbo.FundingStatus ON FundingStatus.ID = Applications.FundingStatus_ID " +
//               " WHERE Applications.Dealer_ID = {0} ";
//           List<ApplicationQueryModel> applicationList = _dbContext.Database.SqlQuery<ApplicationQueryModel>(salesRepQuery, parameters).ToList<ApplicationQueryModel>();


//           foreach (ApplicationQueryModel appQueryModel in applicationList)
//           {
//               salesDashboardDetailViewModel = new SalesDashboardDetailViewModel();
//               salesDashboardDetailViewModel.ApplicationID = appQueryModel.ApplicationNumber;
//               salesDashboardDetailViewModel.DateOpened = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.StatusDate = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.SalesRepName = appQueryModel.SalesRepName;
//               salesDashboardDetailViewModel.DealerName = appQueryModel.DealerName;
           
//               resultList.Add(salesDashboardDetailViewModel);
//           }


//           return resultList;

//       }
//       public List<SalesDashboardDetailViewModel> getApplicationsByDealer(int DealerID, int ApplicationStatusID)
//       {

//           object[] parameters = { DealerID, ApplicationStatusID };
//           List<SalesDashboardDetailViewModel> resultList = new List<SalesDashboardDetailViewModel>();
//           SalesDashboardDetailViewModel salesDashboardDetailViewModel = null;

//           String salesRepQuery = " SELECT   Applications.ID , Applications.ApplicationNumber," +
//                " Applications.ApplicationEnteredDate, '' as DecisionStatus , '' as FundingStatus, " +
//             //  " DecisionStatus.[Type]," +
//             //  " FundingStatus.[Type]," +
//               " S.FullName as SalesRepName, " +
//               " D.FullName  as DealerName " +
//               " FROM  Applications INNER JOIN ApplicationStatus  " +
//               " ON   Applications.ID = ApplicationStatus.Application_id  " +
//               " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//              "  INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID " +
//               " INNER JOIN dbo.Contacts S ON S.ID = SalesReps.contact_ID " + 
//             "  INNER JOIN dbo.Contacts D ON D.ID = Dealers.contact_ID " +
//          //     " INNER JOIN dbo.DecisionStatus ON DecisionStatus.ID = Applications.DecisionStatus_ID " +
//          //     " INNER JOIN dbo.FundingStatus ON FundingStatus.ID = Applications.FundingStatus_ID " +
//               " WHERE Applications.Dealer_ID = {0}  AND ApplicationStatus.DPCStatusID = {1}";
//           List<ApplicationQueryModel> applicationList = _dbContext.Database.SqlQuery<ApplicationQueryModel>(salesRepQuery, parameters).ToList<ApplicationQueryModel>();


//           foreach (ApplicationQueryModel appQueryModel in applicationList)
//           {
//               salesDashboardDetailViewModel = new SalesDashboardDetailViewModel();


//               salesDashboardDetailViewModel.ApplicationID = appQueryModel.ApplicationNumber;
//               salesDashboardDetailViewModel.DateOpened = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.StatusDate = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.SalesRepName = appQueryModel.SalesRepName;
//               salesDashboardDetailViewModel.DealerName = appQueryModel.DealerName;
//                 resultList.Add(salesDashboardDetailViewModel);
//           }


//           return resultList;

//       }
//       public List<SalesDashboardDetailViewModel> getAllApplicationsByStatus(int ApplicationStatusID)
//       {

//           object[] parameters = { ApplicationStatusID };
//           List<SalesDashboardDetailViewModel> resultList = new List<SalesDashboardDetailViewModel>();
//           SalesDashboardDetailViewModel salesDashboardDetailViewModel = null;

//           String salesRepQuery = " SELECT  Applications.ID , Applications.ApplicationNumber," +
//                " Applications.ApplicationEnteredDate, " +  "'' as FundingStatus , ' ' as DecisionStatus , " +
//              // " DecisionStatus.[Type]," +
//              // " FundingStatus.[Type]," +
//               " S.FullName   as SalesRepName, " +
//               " D.FullName  as DealerName " +
//               " FROM  Applications INNER JOIN ApplicationStatus  " +
//               " ON   Applications.ID = ApplicationStatus.Application_id  " +
//              " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//             " INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID  " +
//             " INNER JOIN dbo.Contacts S ON S.ID = SalesReps.contact_ID  " +
//             " INNER JOIN dbo.Contacts D ON D.ID = Dealers.contact_ID  " +


//              // " INNER JOIN dbo.DecisionStatus ON DecisionStatus.ID = Applications.DecisionStatus_ID " +
//              // " INNER JOIN dbo.FundingStatus ON FundingStatus.ID = Applications.FundingStatus_ID " +
//               " WHERE ApplicationStatus.DPCStatusID = {0}  ";
//           List<ApplicationQueryModel> applicationList = _dbContext.Database.SqlQuery<ApplicationQueryModel>(salesRepQuery, parameters).ToList<ApplicationQueryModel>();


//           foreach (ApplicationQueryModel appQueryModel in applicationList)
//           {
//               salesDashboardDetailViewModel = new SalesDashboardDetailViewModel();


//               salesDashboardDetailViewModel.ApplicationID = appQueryModel.ApplicationNumber;
//               salesDashboardDetailViewModel.DateOpened = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.StatusDate = appQueryModel.ApplicationEnteredDate;
//               salesDashboardDetailViewModel.SalesRepName = appQueryModel.SalesRepName;
//               salesDashboardDetailViewModel.DealerName = appQueryModel.DealerName;
           
//               resultList.Add(salesDashboardDetailViewModel);
//           }


//           return resultList;

//       }





//        public List<SalesDashboardViewModel> getSalesDashBoard_Test(SalesDashboardGroupByEnum SalesDashBoardGroupBy, int? SalesRepID, int? DealerID)
//        {
//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//            displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();


//            // dealers
//            if (SalesDashBoardGroupBy == SalesDashboardGroupByEnum.GroupByDealers)
//            {
//                if (DealerID != null & SalesRepID != null)
//                    return getSalesDashboardByDealerFilterBySalesRep(Convert.ToInt32(SalesRepID), Convert.ToInt32(DealerID));
//                else if (DealerID == null & SalesRepID != null)
//                    return getSalesDashboardByDealerFilterBySalesRep(Convert.ToInt32(SalesRepID));
//                else if (DealerID != null & SalesRepID == null)
//                    return getSalesDashBoardByDealer(Convert.ToInt32(DealerID));
//                else
//                    return getSalesDashBoardByDealer();
//            }
//            else if (SalesDashBoardGroupBy == SalesDashboardGroupByEnum.GroupBySalesReps)
//            {
//                if (SalesRepID == null)
//                    return getSalesDashBoardBySalesRep();
//                else
//                    return getSalesDashBoardBySalesRep(Convert.ToInt32(SalesRepID));
//            }
//            else if (SalesDashBoardGroupBy == SalesDashboardGroupByEnum.All)
//            {
//                // get all
//                displayViewModel.liSalesDashboardViewModel.Add(getSalesDashBoard());
//                return displayViewModel.liSalesDashboardViewModel;
//            }

//            return displayViewModel.liSalesDashboardViewModel;


//        }

//        public List<SalesDashboardDetailViewModel> getSalesDashBoardDetail_Test(SalesDashboardGroupByEnum SalesDashBoardGroupBy, SalesDashboardStatusEnum SalesDashBoardStatus, int? SalesRepID, int? DealerID)
//        {
//            SalesDashboardDetailDisplayViewModel displayViewModel = new SalesDashboardDetailDisplayViewModel();
//            displayViewModel.liSalesDashboardDetailViewModel = new List<SalesDashboardDetailViewModel>();

//            // dealers
//            if (SalesDashBoardGroupBy == SalesDashboardGroupByEnum.GroupByDealers)
//            {
//                // get all applications belongs to Dealer, group by Status
//                if (DealerID != null & SalesRepID != null)
//                    return getApplicationsByDealerFilterBySalesRep(Convert.ToInt32(SalesRepID), Convert.ToInt32(DealerID), Convert.ToInt32(SalesDashBoardStatus));
//                else
//                    return getApplicationsByDealer(Convert.ToInt32(DealerID), Convert.ToInt32(SalesDashBoardStatus));
//            }
//            else if (SalesDashBoardGroupBy == SalesDashboardGroupByEnum.GroupBySalesReps)
//            {
//                // get all applications belongs to SalesRepID, group by Status
//                return getApplicationsBySalesRep(Convert.ToInt32(SalesRepID), Convert.ToInt32(SalesDashBoardStatus));
//            }
//            else if (SalesDashBoardGroupBy == SalesDashboardGroupByEnum.All)
//            {
//                // get all applications, group by Status
//                return getAllApplicationsByStatus(Convert.ToInt32(SalesDashBoardStatus));
//            }


//            return displayViewModel.liSalesDashboardDetailViewModel;


//        }


//        // Adding Functions requested by Tommy


//        public List<SalesDashboardViewModel> getSalesDashboardByDealerFilterBySalesRep(int SalesRepID)
//        {
//            // Initialize variables 
//            List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//            List<DealerQueryModel> dealerList = null;
//            SalesDashboardViewModel appStatus = null;
//            object[] parameters = { NEW_APPLICATION_STATUS, SalesRepID };

//            Dictionary<int, SalesDashboardViewModel> dealerDictionary = new Dictionary<int, SalesDashboardViewModel>();

//            /* 901 */

//            String dealerQuery = " Select A.Dealer_ID as DealerID, A.SalesRep_ID as SalesRepID , A.appCount as ApplicationCount ,  DealerContact.FullName as DealerName , " +
//                " SalesRepContact.FullName as SalesRepName , DealerContact.ContactIdentifier as DealerNumber" +
//                " FROM  " +
//                " (SELECT  Count(Applications.ID) as appCount, Applications.Dealer_ID as Dealer_ID , Applications.SalesRep_ID as SalesRep_ID " +
//                " FROM  dbo.[Applications]  " +
//                " INNER JOIN dbo.[ApplicationStatus]  ON   Applications.ID = ApplicationStatus.Application_id  " +
//                " INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID  " +
//                " INNER JOIN dbo.SalesReps ON SalesReps.ID = Applications.SalesRep_ID " +
//                " WHERE ApplicationStatus.DPCStatusID = {0}  " +
//                " AND Applications.SalesRep_ID = {1} " +
//                " GROUP BY Applications.Dealer_ID , Applications.SalesRep_ID ) A  " +
//                " INNER JOIN dbo.SalesReps ON SalesReps.ID = A.SalesRep_ID " +
//                " INNER JOIN dbo.Contacts AS SalesRepContact ON SalesRepContact.ID = SalesReps.contact_ID  " +
//                " INNER JOIN dbo.Dealers ON Dealers.ID = A.Dealer_ID " +
//                " INNER JOIN dbo.Contacts AS DealerContact ON DealerContact.ID = Dealers.contact_ID  ";
                
                


//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//            displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();



//            // New Status
//            parameters[0] = NEW_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();


//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName ;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;
//            }



//            // Pending Status
//            parameters[0] = PENDING_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalPending = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;

//            }

//            // Funded Application Status
//            parameters[0] = FUNDED_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalFunded = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;
//            }

//            // Decline  Application Status
///*
//            parameters[0] = DECLINE_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                    // appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//                }

//            }

//*/

//            // COUNTER_OFFER_APPLICATION_STATUS
//            parameters[0] = COUNTER_OFFER_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalCounterOffers = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;


//            }


//            // IN PROGRESS
//            parameters[0] = IN_PROGRESS_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalInProgress = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;


//            }

//            foreach (var item in dealerDictionary)
//            {
//                resultList.Add(item.Value);
//            }


//            return resultList;

//        }

//        // 902

      
//        public List<SalesDashboardViewModel> getSalesDashboardByDealerFilterBySalesRep(int SalesRepID , int DealerID)
//        {
//            // Initialize variables 
//            List<SalesDashboardViewModel> resultList = new List<SalesDashboardViewModel>();
//            List<DealerQueryModel> dealerList = null;
//            SalesDashboardViewModel appStatus = null;
//            object[] parameters = { NEW_APPLICATION_STATUS, SalesRepID, DealerID };

//            Dictionary<int, SalesDashboardViewModel> dealerDictionary = new Dictionary<int, SalesDashboardViewModel>();

//            /* 902 */



//            String dealerQuery = "Select A.Dealer_ID , A.SalesRep_ID , A.appCount as ApplicationCount ,   DealerContact.FullName as DealerName , " +
//                     " SalesRepContact.FullName as SalesRepName, DealerContact.ContactIdentifier as DealerNumber" +
//                      "  FROM  " +
//                        " (SELECT  Count(Applications.ID) as appCount, Applications.Dealer_ID as Dealer_ID , Applications.SalesRep_ID as SalesRep_ID " +
//                        " FROM  dbo.[Applications] " +
//                       "  INNER JOIN dbo.[ApplicationStatus]  ON   Applications.ID = ApplicationStatus.Application_id  " +
//                       "  INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID  " +
//                       "  INNER JOIN dbo.SalesReps ON SalesReps.ID = Applications.SalesRep_ID " +
//                       "  WHERE ApplicationStatus.DPCStatusID = {0} " +
//                       "  AND Applications.SalesRep_ID = {1} " +
//                       "  AND Applications.Dealer_ID = {2} " +
//                       "  GROUP BY Applications.Dealer_ID , Applications.SalesRep_ID ) A  " +
//                       "  INNER JOIN dbo.SalesReps ON SalesReps.ID = A.SalesRep_ID " +
//                       "  INNER JOIN dbo.Contacts AS SalesRepContact ON SalesRepContact.ID = SalesReps.contact_ID  " +
//                       "  INNER JOIN dbo.Dealers ON Dealers.ID = A.Dealer_ID " +
//                       "  INNER JOIN dbo.Contacts AS DealerContact ON DealerContact.ID = Dealers.contact_ID ";
                        

        


//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
//            displayViewModel.liSalesDashboardViewModel = new List<SalesDashboardViewModel>();



//            // New Status
//            parameters[0] = NEW_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();


//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;
//            }



//            // Pending Status
//            parameters[0] = PENDING_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalPending = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;
//            }

//            // Funded Application Status
//            parameters[0] = FUNDED_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalFunded = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;

//            }

//            // Decline  Application Status
//            /*
//                        parameters[0] = DECLINE_APPLICATION_STATUS;
//                        dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//                        foreach (DealerQueryModel dealerQueryModel in dealerList)
//                        {

//                            if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                            {
//                                appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                                // appStatus.TotalNew = dealerQueryModel.ApplicationCount;
//                            }

//                        }

//            */

//            // COUNTER_OFFER_APPLICATION_STATUS
//            parameters[0] = COUNTER_OFFER_APPLICATION_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalCounterOffers = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;


//            }


//            // IN PROGRESS
//            parameters[0] = IN_PROGRESS_STATUS;
//            dealerList = _dbContext.Database.SqlQuery<DealerQueryModel>(dealerQuery, parameters).ToList<DealerQueryModel>();
//            foreach (DealerQueryModel dealerQueryModel in dealerList)
//            {

//                if (dealerDictionary.ContainsKey(dealerQueryModel.DealerID))
//                {
//                    appStatus = dealerDictionary[dealerQueryModel.DealerID];
//                }

//                else
//                {
//                    appStatus = new SalesDashboardViewModel();
//                    appStatus.DealerID = dealerQueryModel.DealerID;
//                    dealerDictionary.Add(dealerQueryModel.DealerID, appStatus);
//                }

//                appStatus.TotalInProgress = dealerQueryModel.ApplicationCount;
//                appStatus.DealerName = dealerQueryModel.DealerFullName;
//                appStatus.SalesRepName = dealerQueryModel.SalesRepFullName;
//                appStatus.DealerAccountNumber = dealerQueryModel.DealerNumber;

//            }

//            foreach (var item in dealerDictionary)
//            {
//                resultList.Add(item.Value);
//            }


//            return resultList;

//        }



//        public List<SalesDashboardDetailViewModel> getApplicationsByDealerFilterBySalesRep(int SalesRepID, int DealerID, int ApplicationStatus)
//        {

//            object[] parameters = { DealerID, ApplicationStatus, SalesRepID };
//            List<SalesDashboardDetailViewModel> resultList = new List<SalesDashboardDetailViewModel>();
//            SalesDashboardDetailViewModel salesDashboardDetailViewModel = null;





//            String salesRepQuery = " SELECT   Applications.ID , Applications.ApplicationNumber," +
//              " Applications.ApplicationEnteredDate, '' as DecisionStatus, '' as FundingStatus ," +
//                // " DecisionStatus.[Type]," +
//                // " FundingStatus.[Type]," +
//             " S.FullName  as SalesRepName, " +
//             " D.FullName as  DealerName " +
//             " FROM  Applications INNER JOIN ApplicationStatus  " +
//             " ON   Applications.ID = ApplicationStatus.Application_id  " +
//             " INNER JOIN dbo.SalesReps  ON SalesReps.ID = Applications.SalesRep_ID  " +
//              "   INNER JOIN dbo.Dealers  ON Dealers.ID = Applications.Dealer_ID  " +
//             " INNER JOIN dbo.Contacts S ON S.ID = SalesReps.contact_ID " +
//           " INNER JOIN dbo.Contacts D ON D.ID = Dealers.contact_ID  " +
//             " WHERE Applications.Dealer_ID = {0}  AND ApplicationStatus.DPCStatusID = {1} AND Applications.SalesRep_ID = {2}";




//            List<ApplicationQueryModel> applicationList = _dbContext.Database.SqlQuery<ApplicationQueryModel>(salesRepQuery, parameters).ToList<ApplicationQueryModel>();


//            foreach (ApplicationQueryModel appQueryModel in applicationList)
//            {
//                salesDashboardDetailViewModel = new SalesDashboardDetailViewModel();


//                salesDashboardDetailViewModel.ApplicationID = appQueryModel.ApplicationNumber;
//                salesDashboardDetailViewModel.StatusDate = appQueryModel.ApplicationEnteredDate;
//                salesDashboardDetailViewModel.SalesRepName = appQueryModel.SalesRepName;
//                salesDashboardDetailViewModel.DealerName = appQueryModel.DealerName;
//                salesDashboardDetailViewModel.DateOpened = appQueryModel.ApplicationEnteredDate;
//                resultList.Add(salesDashboardDetailViewModel);
//            }


//            return resultList;

//        }


//    }
//}