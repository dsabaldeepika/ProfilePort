using System;
using System.Collections.Generic;
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
    public class TermCapController : Controller
    {
      //  private ScoringEngineEntities db = new ScoringEngineEntities();
        string connectionString;
        ConnectionStringProperty connectionStringProperty;
        private ScoringEngineEntities db;

        public TermCapController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }
        // GET: /TermCap/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllTermCapViewModelAsync());
        }

        // GET: /TermCap/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TermCap termcap = await db.TermCap.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            TermCapViewModel TermCapViewModel = await scoreManager.FindTermCapViewModelAsync(id);
            if (TermCapViewModel == null)
            {
                return HttpNotFound();
            }
            return View(TermCapViewModel);
        }

        // GET: /TermCap/Create
        public ActionResult Create()
        {
            TermCapViewModel TermCapViewModel = new TermCapViewModel();
            IScore scoreManager = new ScoreManager(db);
            TermCapViewModel.liAdjustmentType = new List<AdjustmentType>();
            TermCapViewModel.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            return View(TermCapViewModel);
        }

        // POST: /TermCap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TermCapMinFinanceAmt,TermCapMaxFinanceAmt,TermCapAdjustmentCap,TermCapCreatedByID,TermCapModifiedByID,TermCapCreatedDate,TermCapModifiedDate,AdjustmentTypeID")] TermCapViewModel TermCapViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                TermCap TermCap = new TermCap();
                TermCap.MinFinanceAmt = TermCapViewModel.TermCapMinFinanceAmt;
                TermCap.MaxFinanceAmt = TermCapViewModel.TermCapMaxFinanceAmt;
                TermCap.AdjustmentCap = TermCapViewModel.TermCapAdjustmentCap;
                TermCap.AdjustmentType = new AdjustmentType();
                TermCap.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                TermCap.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                TermCap.ModifiedDate = DateTime.Now;
                TermCap.CreatedDate = DateTime.Now;
                TermCap.AdjustmentType = await scoreManager.FindAdjustmentTypeAsync(TermCapViewModel.AdjustmentTypeID);

                await scoreManager.AddTermCapAsync(TermCap);
                return RedirectToAction("Index");
            }

            TermCapViewModel adjVM = new TermCapViewModel();
            adjVM.liAdjustmentType = new List<AdjustmentType>();
            adjVM.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            return View(adjVM);
        }

        // GET: /TermCap/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TermCap termcap = await db.TermCap.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            TermCapViewModel TermCapViewModel = await scoreManager.FindTermCapViewModelAsync(id);
            TermCapViewModel.liAdjustmentType = new List<AdjustmentType>();
            TermCapViewModel.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            if (TermCapViewModel == null)
            {
                return HttpNotFound();
            }
            return View(TermCapViewModel);
        }

        // POST: /TermCap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TermCapID,TermCapMinFinanceAmt,TermCapMaxFinanceAmt,TermCapAdjustmentCap,TermCapCreatedByID,TermCapModifiedByID,TermCapCreatedDate,TermCapModifiedDate,AdjustmentTypeID")] TermCapViewModel TermCapViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                TermCap TermCap = new TermCap();
                TermCap.ID = TermCapViewModel.TermCapID;
                TermCap.MinFinanceAmt = TermCapViewModel.TermCapMinFinanceAmt;
                TermCap.MaxFinanceAmt = TermCapViewModel.TermCapMaxFinanceAmt;
                TermCap.AdjustmentCap = TermCapViewModel.TermCapAdjustmentCap;
                TermCap.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                TermCap.ModifiedDate = DateTime.Now;
                TermCap.CreatedDate = TermCapViewModel.TermCapCreatedDate;
                TermCap.CreatedByID = TermCapViewModel.TermCapCreatedByID;
                TermCap.AdjustmentType = await scoreManager.FindAdjustmentTypeAsync(TermCapViewModel.AdjustmentTypeID);

                await scoreManager.ChangeTermCapAsync(TermCap);
                return RedirectToAction("Index");
            }

            TermCapViewModel adjVM = await scoreManager.FindTermCapViewModelAsync(TermCapViewModel.TermCapID);
            adjVM.liAdjustmentType = new List<AdjustmentType>();
            adjVM.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();

            return View(adjVM);
        }

        // GET: /TermCap/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TermCap termcap = await db.TermCap.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            TermCap TermCap = await scoreManager.FindTermCapAsync(id);
            if (TermCap == null)
            {
                return HttpNotFound();
            }
            return View(TermCap);
        }

        // POST: /TermCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //TermCap termcap = await db.TermCap.FindAsync(id);
            //db.TermCap.Remove(termcap);
            //await db.SaveChangesAsync();
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveTermCapAsync(id);
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
