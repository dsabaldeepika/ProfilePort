using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;


using DealerPortalCRM.DAService;
using DealerPortalCRM.DAServiceImpl;
using DealerPortalCRM.ViewModels;
using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
using Com.CoreLane.Common.Models;

namespace DealerPortalCRM.Controllers
{
    public class ExcessMileageController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public ExcessMileageController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /ExcessMileage/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllExcessMileageViewModelAsync());

        }

        // GET: /ExcessMileage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            ExcessMileageViewModel excessmileageViewModel = await scoreManager.FindExcessMileageViewModelAsync(id);

            if (excessmileageViewModel == null)
            {
                return HttpNotFound();
            }
            return View(excessmileageViewModel);
        }

        // GET: /ExcessMileage/Create
        public ActionResult Create()
        {
            ExcessMileageViewModel ExcessMileageViewModel = new ExcessMileageViewModel();
            return View(ExcessMileageViewModel);
        }

        // POST: /ExcessMileage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ExcessMileageMinMileage,ExcessMileageMaxMileage,ExcessMileageLTVAdj,ExcessMileageDiscAdj,ExcessMileageTermCap,ExcessMileageIsActive,ExcessMileageCreatedByID,ExcessMileageCreatedDate,ExcessMileageModifiedByID,ExcessMileageModifiedDate")] ExcessMileageViewModel ExcessMileageViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {

                ExcessMileage ExcessMileage = new ExcessMileage();
                DateTime currentTime = DateTime.Now;
                ExcessMileage.CreatedDate = currentTime;
                ExcessMileage.ModifiedDate = currentTime;
                ExcessMileage.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                ExcessMileage.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);

                ExcessMileage.LTVAdj = ExcessMileageViewModel.ExcessMileageLTVAdj;
                ExcessMileage.DiscAdj = ExcessMileageViewModel.ExcessMileageDiscAdj;
                ExcessMileage.TermCap = ExcessMileageViewModel.ExcessMileageTermCap;
                ExcessMileage.IsActive = ExcessMileageViewModel.ExcessMileageIsActive;
                ExcessMileage.MinMileage = ExcessMileageViewModel.ExcessMileageMinMileage;
                ExcessMileage.MaxMileage = ExcessMileageViewModel.ExcessMileageMaxMileage;

                await scoreManager.AddExcessMileageAsync(ExcessMileage);

                return RedirectToAction("Index");
            }

            return View(ExcessMileageViewModel);
        }

        // GET: /ExcessMileage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            ExcessMileageViewModel ExcessMileageViewModel = await scoreManager.FindExcessMileageViewModelAsync(id);
            if (ExcessMileageViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ExcessMileageViewModel);
        }

        // POST: /ExcessMileage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExcessMileageID,ExcessMileageMinMileage,ExcessMileageMaxMileage,ExcessMileageLTVAdj,ExcessMileageDiscAdj,ExcessMileageTermCap,ExcessMileageCreatedDate,CExcessMileagereatedByID,ExcessMileageIsActive")] ExcessMileageViewModel ExcessMileageViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                DateTime currentTime = DateTime.Now;
                ExcessMileage ExcessMileage = new ExcessMileage();

                ExcessMileage.ModifiedDate = currentTime;
                ExcessMileage.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                ExcessMileage.ID = ExcessMileageViewModel.ExcessMileageID;
                ExcessMileage.LTVAdj = ExcessMileageViewModel.ExcessMileageLTVAdj;
                ExcessMileage.DiscAdj = ExcessMileageViewModel.ExcessMileageDiscAdj;
                ExcessMileage.TermCap = ExcessMileageViewModel.ExcessMileageTermCap;
                ExcessMileage.IsActive = ExcessMileageViewModel.ExcessMileageIsActive;
                ExcessMileage.MinMileage = ExcessMileageViewModel.ExcessMileageMinMileage;
                ExcessMileage.MaxMileage = ExcessMileageViewModel.ExcessMileageMaxMileage;

                await scoreManager.ChangeExcessMileageAsync(ExcessMileage);
                return RedirectToAction("Index");
            }
            return View(ExcessMileageViewModel);
        }

        // GET: /ExcessMileage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            ExcessMileageViewModel ExcessMileageViewModel = await scoreManager.FindExcessMileageViewModelAsync(id);

            if (ExcessMileageViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ExcessMileageViewModel);
        }

        // POST: /ExcessMileage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveExcessMileageAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
