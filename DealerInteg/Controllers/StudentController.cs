//using System;
//using System.Collections.Generic;
//using System.Data; 
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;


//using DealerPortalCRM.DAService;
//using DealerPortalCRM.Repository;
//using DealerPortalCRM.DAServiceImpl;
//using DealerPortalCRM.ViewModels;
// using ScoringEngine.Services ;
//using ScoringEngine.ServicesImpl;
//using IScoringEngineDbService = ScoringEngine.Services.IScoringEngineDbService;
//using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
//using Com.CoreLane.Common.Models;

//namespace DealerPortalCRM.Controllers
//{
//    public class StudentController : Controller
//    {
//       // private ScoringEngineEntities db = new ScoringEngineEntities();
//        string connectionString;
//        ConnectionStringProperty connectionStringProperty;
//        private ScoringEngineEntities db;

//        public StudentController()
//        {
//            connectionStringProperty = new ConnectionStringProperty();
//            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
//            db = new ScoringEngineEntities(connectionString);
//        }

//        // GET: /Student/
//        public async Task<ActionResult> Index()
//        {
//            // return View(await db.Students.ToListAsync());

//            IStudent studentRepo = new StudentRepo(db);
//            return View(await studentRepo.GetAllStudentsAsync());
//        }

//        // GET: /Student/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Student student = await db.Students.FindAsync(id);
//            if (student == null)
//            {
//                return HttpNotFound();
//            }
//            return View(student);
//        }

//        // GET: /Student/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: /Student/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Students.Add(student);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(student);
//        }

//        // GET: /Student/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Student student = await db.Students.FindAsync(id);
//            if (student == null)
//            {
//                return HttpNotFound();
//            }
//            return View(student);
//        }

//        // POST: /Student/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(student).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(student);
//        }

//        // GET: /Student/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Student student = await db.Students.FindAsync(id);
//            if (student == null)
//            {
//                return HttpNotFound();
//            }
//            return View(student);
//        }

//        // POST: /Student/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Student student = await db.Students.FindAsync(id);
//            db.Students.Remove(student);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }



//        public ActionResult Test()
//        {

//            IApplication app = new ApplicationManager(db);

//            SalesDashboardViewModel salesDashBoardTest = app.getSalesDashBoard();

//            return View(salesDashBoardTest);

//        }



       



//           public ActionResult SalesRepDetail()
//        {

//            //int ApplicationStatus = 1 ;
//            //ApplicationManager app = new ApplicationManager(db);

//            //List<SalesDashboardDetailViewModel> salesDashBoardList  = app.getApplicationsBySalesRep2( ApplicationStatus);

//            //return View(salesDashBoardList);

//            return View();

//        }
        
//         public ActionResult SalesRep()
//        {

//             int SalesRepID = 0 ;
//            IApplication app = new ApplicationManager(db);

//            List<SalesDashboardViewModel> salesDashBoardTest = app.getSalesDashBoardBySalesRep( SalesRepID);

//            return View(salesDashBoardTest);

//        }

    

//        public ActionResult NativeQuery()
//        {

//            SalesDashBoardTest salesDashBoardTest = new SalesDashBoardTest();
//            int maxCount = db.Database.SqlQuery<int>("Select count(*) from dbo.Applications").FirstOrDefault<int>();

//            salesDashBoardTest.TotalCount = maxCount;
//            salesDashBoardTest.Description = "Test";

//            return View(salesDashBoardTest);

//        }



//        public ActionResult TestScoreEngine()
//        {
         
//            return View();
//        }


//        public ActionResult RunScoreEngine()
//        {

//            IScoringEngineDbService scoringEngineDbService = new ScoringEngineDbService(db);
//            IApplicationScore applicationScore = new ApplicationScore();
//            applicationScore.GetContextFromDb(scoringEngineDbService);
//            applicationScore.ScoreApplication(scoringEngineDbService, "0");

//            return RedirectToAction("Index", "ScoringEngineResults");
//        }



//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }




//    }
//}
