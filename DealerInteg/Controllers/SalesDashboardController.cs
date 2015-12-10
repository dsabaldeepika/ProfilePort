//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Xml.Linq;
//using System.Configuration;

//using DealerPortalCRM.ViewModels;
//using DealerPortalCRM.DAServiceImpl;
//using DealerPortalCRM.DAService;

//using DealerPortalCRM.Constants;

//using System.Data; 
//using System.Data.SqlClient;
//using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
//using Com.CoreLane.Common.Models;

//namespace DealerPortalCRM.Controllers
//{

//    public class SalesDashboardController : Controller
//    {
//        string connectionString;
//        ConnectionStringProperty connectionStringProperty;        
//        private ScoringEngineEntities db;

//        public SalesDashboardController()
//        {
//            connectionStringProperty = new ConnectionStringProperty();
//            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
//            db = new ScoringEngineEntities(connectionString);
//        }


//        [Authorize(Roles = "Administrator, ITUser, Executive, Manager, SalesRep")]
//        public ActionResult ByDealers(int? SalesRepID, int? DealerID)
//        {
//            IApplication app = new ApplicationManager(db);
//            IUser user = new UserManager(db);
//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();

//            // get salesRep ID when user is a SalesRep
//            if (User.IsInRole("SalesRep") && SalesRepID == null)
//            {
//                SalesRepID = user.GetSalesRepId(User.Identity.Name);
//                return RedirectToAction("ByDealers", new { SalesRepID = SalesRepID, DealerID = DealerID });
//            }
            
//            displayViewModel.liSalesDashboardViewModel = app.getSalesDashBoard_Test(SalesDashboardGroupByEnum.GroupByDealers, SalesRepID, DealerID);
//            return View(displayViewModel);
//        }


//        [Authorize(Roles = "Administrator, ITUser, Executive,  Manager")]
//        public ActionResult BySalesReps()
//        {
//            IApplication app = new ApplicationManager(db);
//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();
            
//            displayViewModel.liSalesDashboardViewModel = app.getSalesDashBoard_Test(SalesDashboardGroupByEnum.GroupBySalesReps, null, null);
//            return View(displayViewModel);
//        }


//        [Authorize(Roles = "Administrator, ITUser, Executive, Manager, SalesRep")]
//        public ActionResult Main(int? SalesRepID, int? DealerID)
//        {
//            IApplication app = new ApplicationManager(db);
//            IUser user = new UserManager(db);
//            SalesDashboardDisplayViewModel displayViewModel = new SalesDashboardDisplayViewModel();

//            // get salesRep ID when user is a SalesRep
//            if (User.IsInRole("SalesRep") && SalesRepID == null)
//            {
//                SalesRepID = user.GetSalesRepId(User.Identity.Name);
//                return RedirectToAction("Main", new { SalesRepID = SalesRepID, DealerID = DealerID });
//            }

//            if (DealerID != null)
//                displayViewModel.liSalesDashboardViewModel = app.getSalesDashBoard_Test(SalesDashboardGroupByEnum.GroupByDealers, SalesRepID, DealerID);
//            else if (SalesRepID != null)
//                displayViewModel.liSalesDashboardViewModel = app.getSalesDashBoard_Test(SalesDashboardGroupByEnum.GroupBySalesReps, SalesRepID, DealerID);
//            else if (User.IsInRole("Administrator") || User.IsInRole("ITUser") || User.IsInRole("Executive") || User.IsInRole("Manager"))
//                displayViewModel.liSalesDashboardViewModel = app.getSalesDashBoard_Test(SalesDashboardGroupByEnum.All, null, null);

//            return View(displayViewModel);
//        }


//        [Authorize(Roles = "Administrator, ITUser, Executive, Manager, SalesRep")]
//        public ActionResult Detail(SalesDashboardStatusEnum SalesDashboardStatus, int? SalesRepID, int? DealerID)
//        {
//            IApplication app = new ApplicationManager(db);
//            SalesDashboardDetailDisplayViewModel displayViewModel = new SalesDashboardDetailDisplayViewModel();

//            if (DealerID != null)
//                displayViewModel.liSalesDashboardDetailViewModel = app.getSalesDashBoardDetail_Test(SalesDashboardGroupByEnum.GroupByDealers, SalesDashboardStatus, SalesRepID, DealerID);
//            else if (SalesRepID != null)
//                displayViewModel.liSalesDashboardDetailViewModel = app.getSalesDashBoardDetail_Test(SalesDashboardGroupByEnum.GroupBySalesReps, SalesDashboardStatus, SalesRepID, DealerID);
//            else if (User.IsInRole("Administrator") || User.IsInRole("ITUser") || User.IsInRole("Executive") || User.IsInRole("Manager"))
//                displayViewModel.liSalesDashboardDetailViewModel = app.getSalesDashBoardDetail_Test(SalesDashboardGroupByEnum.All, SalesDashboardStatus, null, null);
            
//            return View(displayViewModel);
//        }


//        public ActionResult Refresh(int ApplicationID, SalesDashboardStatusEnum SalesDashboardStatus, int? SalesRepID, int? DealerID)
//        {
//            // call the web service to get XML
//            string dataDumpXML = ExtractTCIXML(ApplicationID, Convert.ToString(ConfigurationManager.AppSettings["TCIWSDataDumpCommand"]));

//            // insert XML and extract to 
//            // ?? should use entity
//            if (!string.IsNullOrEmpty(dataDumpXML))
//            {
//                int rowsAffected = 0;
//                try
//                {
//                    using (SqlConnection conn = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["ScoringEngineEntities"])))
//                    {
//                        using (SqlCommand cmd = new SqlCommand("usp_DP_InsertXMLImport", conn))
//                        {
//                            cmd.CommandType = CommandType.StoredProcedure;
//                            cmd.Parameters.Add(new SqlParameter("@ApplicationID", ApplicationID));
//                            cmd.Parameters.Add(new SqlParameter("@XMLData", dataDumpXML));
//                            conn.Open();
//                            rowsAffected = cmd.ExecuteNonQuery();
//                        }
//                    }
//                }
//                catch (Exception ex) { throw ex; }
//            }

//            return RedirectToAction("Detail", new { SalesDashboardStatus, SalesRepID = SalesRepID, DealerID = DealerID });
//        }


//        private string ExtractTCIXML(int ApplicationID, string Command)
//        {
//            string TCIWholeXML = string.Empty;
//            string TCIrawResponseXML = string.Empty;
//            string TCIrawResponseXMLheader = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>";
//            string payloadXML = string.Empty;

//            try
//            {
//                string requestData = "<?xml version='1.0' encoding='UTF-8' standalone='yes'?><RequestData>" +
//                    "<clientCode>" + Convert.ToString(ConfigurationManager.AppSettings["TCIWSDataDumpClientCode"]) + "</clientCode>" +
//                    "<companyId>" + Convert.ToString(ConfigurationManager.AppSettings["TCIWSDataDumpCompanyID"]) + "</companyId>" +
//                    "<userId>" + Convert.ToString(ConfigurationManager.AppSettings["TCIWSDataDumpUserID"]) + "</userId>" +
//                    "<password>" + Convert.ToString(ConfigurationManager.AppSettings["TCIWSDataDumpUserPassword"]) + "</password>" +
//                    "<rawRequest>" + Convert.ToString(ApplicationID) + "</rawRequest></RequestData>";

//                // calling TCI web service
//                TCIWS.DLService tciwx = new TCIWS.DLService();
//                TCIWholeXML = tciwx.handleRequest(Command, requestData);
//                XDocument XMLDoc = XDocument.Parse(TCIWholeXML);

//                // if it has error code then display error, else get the rawResponse payload
//                if (XMLDoc.Root.Element("errCode") != null)
//                    throw new Exception("Error:" + TCIWholeXML);
//                else
//                {
//                    TCIrawResponseXML = XMLDoc.Descendants("rawResponse").Single().Value;
//                    payloadXML = TCIrawResponseXML.Replace(TCIrawResponseXMLheader, "");
//                }
//            }
//            catch (Exception ex)
//            {
//                //Log(ApplicationID, "Error", "Error in Extracting Data from TCI - " + Command, ex.Message, "VCWCFWebService", "ExtractTCIXML-" + new System.Diagnostics.StackFrame(1).GetMethod().Name);
//                //payloadXML = string.Empty;

//                throw new Exception(ex.Message);
//            }

//            return payloadXML;
//        }


//    }
//}
