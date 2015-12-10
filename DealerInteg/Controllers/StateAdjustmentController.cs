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
    public class StateAdjustmentController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public StateAdjustmentController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /StateAdjustment/
        public async Task<ActionResult> Index()
        {
            //return View(await db.StateAdj.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllStateAdjustmentViewModelAsync());
        }

        // GET: /StateAdjustment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateAdjustment stateadjustment = await db.StateAdj.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            StateAdjustmentViewModel StateAdjustmentViewModel = await scoreManager.FindStateAdjustmentViewModelAsync(id);

            if (StateAdjustmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(StateAdjustmentViewModel);
        }

        // GET: /StateAdjustment/Create
        public ActionResult Create()
        {
            //return View();
            StateAdjustmentViewModel StateAdjustmentViewModel = new StateAdjustmentViewModel();
            IScore scoreManager = new ScoreManager(db);
            StateAdjustmentViewModel.liStateCode = new List<StateCode>();
            StateAdjustmentViewModel.liStateCode = scoreManager.GetAllStateCode();
            return View(StateAdjustmentViewModel);
        }

        // POST: /StateAdjustment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,StateAdjustmentDiscountAdj,StateAdjustmentLTVAdj,StateAdjustmentMaxStateAPR,StateAdjustmentGapCap,StateAdjustmentCreatedByID,StateAdjustmentModifiedByID,StateAdjustmentCreatedDate,StateAdjustmentModifiedDate,StateCodeID")] StateAdjustmentViewModel StateAdjustmentViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                //db.StateAdjustment.Add(stateadjustment);
                //await db.SaveChangesAsync();

                StateAdjustment StateAdjustment = new StateAdjustment();
                StateAdjustment.DiscountAdj = StateAdjustmentViewModel.StateAdjustmentDiscountAdj;
                StateAdjustment.LTVAdj = StateAdjustmentViewModel.StateAdjustmentLTVAdj;
                StateAdjustment.MaxStateAPR = StateAdjustmentViewModel.StateAdjustmentMaxStateAPR;
                StateAdjustment.GapCap = StateAdjustmentViewModel.StateAdjustmentGapCap;
                StateAdjustment.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                StateAdjustment.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                StateAdjustment.ModifiedDate = DateTime.Now;
                StateAdjustment.CreatedDate = DateTime.Now;
                StateAdjustment.StateCode = await scoreManager.FindStateCodeAsync(StateAdjustmentViewModel.StateCodeID);

                await scoreManager.AddStateAdjustmentAsync(StateAdjustment);
                return RedirectToAction("Index");
            }

            StateAdjustmentViewModel adjVM = new StateAdjustmentViewModel();
            adjVM.liStateCode = new List<StateCode>();
            adjVM.liStateCode = scoreManager.GetAllStateCode();
            return View(adjVM);
        }

        // GET: /StateAdjustment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateAdjustment stateadjustment = await db.StateAdjustment.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            StateAdjustmentViewModel StateAdjustmentViewModel = await scoreManager.FindStateAdjustmentViewModelAsync(id);
            StateAdjustmentViewModel.liStateCode = new List<StateCode>();
            StateAdjustmentViewModel.liStateCode = scoreManager.GetAllStateCode();



            if (StateAdjustmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(StateAdjustmentViewModel);
        }

        // POST: /StateAdjustment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StateAdjustmentID,StateAdjustmentDiscountAdj,StateAdjustmentLTVAdj,StateAdjustmentMaxStateAPR,StateAdjustmentGapCap,StateAdjustmentCreatedByID,StateAdjustmentModifiedByID,StateAdjustmentCreatedDate,StateAdjustmentModifiedDate,StateCodeID")] StateAdjustmentViewModel StateAdjustmentViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                //db.Entry(StateAdjustmentViewModel).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                StateAdjustment StateAdjustment = new StateAdjustment();
                StateAdjustment.ID = StateAdjustmentViewModel.StateAdjustmentID;
                StateAdjustment.DiscountAdj = StateAdjustmentViewModel.StateAdjustmentDiscountAdj;
                StateAdjustment.LTVAdj = StateAdjustmentViewModel.StateAdjustmentLTVAdj;
                StateAdjustment.MaxStateAPR = StateAdjustmentViewModel.StateAdjustmentMaxStateAPR;
                StateAdjustment.GapCap = StateAdjustmentViewModel.StateAdjustmentGapCap;
                StateAdjustment.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                StateAdjustment.ModifiedDate = DateTime.Now;
                StateAdjustment.CreatedDate = StateAdjustmentViewModel.StateAdjustmentCreatedDate;
                StateAdjustment.CreatedByID = StateAdjustmentViewModel.StateAdjustmentCreatedByID;
                StateAdjustment.StateCode = await  scoreManager.FindStateCodeAsync(StateAdjustmentViewModel.StateCodeID);

              await  scoreManager.ChangeStateAdjustmentAsync(StateAdjustment);

                return RedirectToAction("Index");
            }
            StateAdjustmentViewModel adjVM = new StateAdjustmentViewModel();
            adjVM.liStateCode = new List<StateCode>();
            adjVM.liStateCode = scoreManager.GetAllStateCode();
            return View(adjVM);
        }

        // GET: /StateAdjustment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateAdjustment stateadjustment = await db.StateAdjustment.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            StateAdjustment StateAdjustment = await scoreManager.FindStateAdjustmentAsync(id);

            if (StateAdjustment == null)
            {
                return HttpNotFound();
            }
            return View(StateAdjustment);
        }

        // POST: /StateAdjustment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StateAdjustment stateadjustment = await db.StateAdjustment.FindAsync(id);
            //db.StateAdjustment.Remove(stateadjustment);
            //await db.SaveChangesAsync();
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveStateAdjustmentAsync(id);
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
