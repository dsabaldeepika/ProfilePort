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
    public class StateFicoRangeController : Controller
    {
       // private ScoringEngineEntities db = new ScoringEngineEntities();

        string connectionString;
        ConnectionStringProperty connectionStringProperty;
        private ScoringEngineEntities db;

        public StateFicoRangeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /StateFicoRange/
        public async Task<ActionResult> Index()
        {
            //return View(await db.StateFicoRange.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllStateFicoRangeViewModelAsync());
        }

        // GET: /StateFicoRange/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateFicoRange stateficorange = await db.StateFicoRange.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            StateFICORangeViewModel StateFICORangeViewModel = await scoreManager.FindStateFicoRangeViewModelAsync(id);
            if (StateFICORangeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(StateFICORangeViewModel);
        }

        // GET: /StateFicoRange/Create
        public ActionResult Create()
        {
            //return View();
            StateFICORangeViewModel StateFICORangeViewModel = new StateFICORangeViewModel();
            IScore scoreManager = new ScoreManager(db);
            StateFICORangeViewModel.liStateCode = new List<StateCode>();
            StateFICORangeViewModel.liStateCode = scoreManager.GetAllStateCode();
            return View(StateFICORangeViewModel);
        }

        // POST: /StateFicoRange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,StateFicoRangeFicoScoreMin,StateFicoRangeFicoScoreMax,StateFicoRangeMinAPR,StateFicoRangeCreatedByID,StateFicoRangeModifiedByID,StateFicoRangeCreatedDate,StateFicoRangeModifiedDate,StateCodeID")] StateFICORangeViewModel StateFICORangeViewModel)
        {
              IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                //db.StateFicoRange.Add(stateficorange);
                //await db.SaveChangesAsync();
                StateFicoRange StateFicoRange = new StateFicoRange();

                StateFicoRange.FicoScoreMin = StateFICORangeViewModel.StateFICORangeFicoScoreMin;
                StateFicoRange.FicoScoreMax = StateFICORangeViewModel.StateFICORangeFicoScoreMax;
                StateFicoRange.MinAPR = StateFICORangeViewModel.StateFICORangeMinAPR;
                StateFicoRange.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                StateFicoRange.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                StateFicoRange.CreatedDate = DateTime.Now;
                StateFicoRange.ModifiedDate = DateTime.Now;
                StateFicoRange.StateCode = await scoreManager.FindStateCodeAsync(StateFICORangeViewModel.StateCodeID);


                await scoreManager.AddStateFicoRangeAsync(StateFicoRange);

                return RedirectToAction("Index");
            }

            StateFICORangeViewModel adjVM = new StateFICORangeViewModel();
            adjVM.liStateCode = new List<StateCode>();
            adjVM.liStateCode = scoreManager.GetAllStateCode();
            return View(adjVM);
        }

        // GET: /StateFicoRange/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateFicoRange stateficorange = await db.StateFicoRange.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            StateFICORangeViewModel StateFICORangeViewModel = await scoreManager.FindStateFicoRangeViewModelAsync(id);
            StateFICORangeViewModel.liStateCode = new List<StateCode>();
            StateFICORangeViewModel.liStateCode = scoreManager.GetAllStateCode();

            if (StateFICORangeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(StateFICORangeViewModel);
        }

        // POST: /StateFicoRange/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StateFicoRangeID,StateFicoRangeFicoScoreMin,StateFicoRangeFicoScoreMax,StateFicoRangeMinAPR,StateFicoRangeCreatedByID,StateFicoRangeModifiedByID,StateFicoRangeCreatedDate,StateFicoRangeModifiedDate,StateCodeID")] StateFICORangeViewModel StateFICORangeViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                //db.Entry(stateficorange).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                StateFicoRange StateFicoRange = new StateFicoRange();

                StateFicoRange.ID = StateFICORangeViewModel.StateFICORangeID;
                StateFicoRange.FicoScoreMin = StateFICORangeViewModel.StateFICORangeFicoScoreMin;
                StateFicoRange.FicoScoreMax = StateFICORangeViewModel.StateFICORangeFicoScoreMax;
                StateFicoRange.MinAPR = StateFICORangeViewModel.StateFICORangeMinAPR;
                StateFicoRange.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                StateFicoRange.ModifiedDate = DateTime.Now;
                StateFicoRange.CreatedByID = StateFICORangeViewModel.StateFICORangeCreatedByID;
                StateFicoRange.CreatedDate = StateFICORangeViewModel.StateFICORangeCreatedDate;
                StateFicoRange.StateCode = await scoreManager.FindStateCodeAsync(StateFICORangeViewModel.StateCodeID);

                await scoreManager.ChangeStateFicoRangeAsync(StateFicoRange);

                return RedirectToAction("Index");
            }

            StateFICORangeViewModel adjVM = new StateFICORangeViewModel();
            adjVM.liStateCode = new List<StateCode>();
            adjVM.liStateCode = scoreManager.GetAllStateCode();
            return View(adjVM);
        }

        // GET: /StateFicoRange/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateFicoRange stateficorange = await db.StateFicoRange.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            StateFicoRange StateFicoRange = await scoreManager.FindStateFicoRangeAsync(id);
            if (StateFicoRange == null)
            {
                return HttpNotFound();
            }
            return View(StateFicoRange);
        }

        // POST: /StateFicoRange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StateFicoRange stateficorange = await db.StateFicoRange.FindAsync(id);
            //db.StateFicoRange.Remove(stateficorange);
            //await db.SaveChangesAsync();

            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveStateFicoRangeAsync(id);

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
