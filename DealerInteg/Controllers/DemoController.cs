//using System.Web.Mvc;
//using DealerPortalCRM.DAServiceImpl;
//using DealerPortalCRM.DAService;
//using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
//using Com.CoreLane.Common.Models;

//namespace DealerPortalCRM.Controllers
//{
//    public class DemoController : Controller
//    {
//        string connectionString;
//        ConnectionStringProperty connectionStringProperty;        
//        private ScoringEngineEntities db;

//        public DemoController()
//        {
//            connectionStringProperty = new ConnectionStringProperty();
//            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
//            db = new ScoringEngineEntities(connectionString);
//        }
//        public ActionResult Users()
//        {
//            return View();
//        }

//        public ActionResult SwitchToExecutive()
//        {
//            ScoringEngineEntities db = new ScoringEngineEntities();
//            IUser user = new UserManager(db);
//            user.SwitchToExecutive(User.Identity.Name);

//            return RedirectToAction("Index", "Home");
//        }

//        public ActionResult SwitchToSalesRep()
//        {
//            //ScoringEngineEntities db = new ScoringEngineEntities();
//            IUser user = new UserManager(db);
//            user.switchToSalesRep(User.Identity.Name);

//            return RedirectToAction("Index", "Home");
//        }
//    }
//}
