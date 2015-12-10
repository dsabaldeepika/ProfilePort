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
    public class DealerDiscountController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public DealerDiscountController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /DealerDiscount/
        public async Task<ActionResult> Index()
        {
            //return View(await db.DealerDiscount.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllDealerDiscountViewModelAsync());
        }

        // GET: /DealerDiscount/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DealerDiscount dealerdiscount = await db.DealerDiscount.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DealerDiscountViewModel DealerDiscountViewModel = await scoreManager.FindDealerDiscountViewModelAsync(id);
            if (DealerDiscountViewModel == null)
            {
                return HttpNotFound();
            }
            return View(DealerDiscountViewModel);
        }

        // GET: /DealerDiscount/Create
        public ActionResult Create()
        {
            //return View();
            DealerDiscountViewModel DealerDiscountViewModel = new DealerDiscountViewModel();
            IScore scoreManager = new ScoreManager(db);
            DealerDiscountViewModel.liStateCode = new List<StateCode>();
            DealerDiscountViewModel.liStateCode = scoreManager.GetAllStateCode();
            DealerDiscountViewModel.liDealerType = new List<DealerType>();
            DealerDiscountViewModel.liDealerType = scoreManager.GetAllDealerType();
            return View(DealerDiscountViewModel);
        }

        // POST: /DealerDiscount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DealerDiscountDiscount,DealerDiscountCreatedByID,DealerDiscountModifiedByID,DealerDiscountCreatedDate,DealerDiscountModifiedDate,DealerTypeID,StateCodeID")] DealerDiscountViewModel DealerDiscountViewModel)
        {
            IScore scoreManager = new ScoreManager(db);

            if (ModelState.IsValid)
            {
               // db.DealerDiscount.Add(dealerdiscount);
               //await db.SaveChangesAsync();
                DealerDiscount DealerDiscount = new DealerDiscount();
                DealerDiscount.Discount = DealerDiscountViewModel.DealerDiscountDiscount;
                DealerDiscount.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                DealerDiscount.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                DealerDiscount.ModifiedDate = DateTime.Now;
                DealerDiscount.CreatedDate = DateTime.Now;
                DealerDiscount.StateCode = new StateCode();
                DealerDiscount.StateCode = await scoreManager.FindStateCodeAsync(DealerDiscountViewModel.StateCodeID);
                DealerDiscount.DealerType = new DealerType();
                DealerDiscount.DealerType = await scoreManager.FindDealerTypeAsync(DealerDiscountViewModel.DealerTypeID);

                await scoreManager.AddDealerDiscountAsync(DealerDiscount);

                return RedirectToAction("Index");
            }

            //return View(dealerdiscount);
            DealerDiscountViewModel adjVM = new DealerDiscountViewModel();
            adjVM.liStateCode = new List<StateCode>();
            adjVM.liStateCode = scoreManager.GetAllStateCode();
            adjVM.liDealerType = scoreManager.GetAllDealerType();
            return View(adjVM);

        }

        // GET: /DealerDiscount/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DealerDiscount dealerdiscount = await db.DealerDiscount.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DealerDiscountViewModel DealerDiscountViewModel = await scoreManager.FindDealerDiscountViewModelAsync(id);
            DealerDiscountViewModel.liStateCode = new List<StateCode>();
            DealerDiscountViewModel.liStateCode = scoreManager.GetAllStateCode();
            DealerDiscountViewModel.liDealerType = new List<DealerType>();
            DealerDiscountViewModel.liDealerType = scoreManager.GetAllDealerType();

            if (DealerDiscountViewModel == null)
            {
                return HttpNotFound();
            }
            return View(DealerDiscountViewModel);
        }

        // POST: /DealerDiscount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DealerDiscountID,DealerDiscountDiscount,DealerDiscountCreatedByID,DealerDiscountModifiedByID,DealerDiscountCreatedDate,DealerDiscountModifiedDate,StateCodeID,DealerTypeID")] DealerDiscountViewModel DealerDiscountViewModel)
        {
            IScore scoreManager = new ScoreManager(db);

            if (ModelState.IsValid)
            {
                //db.Entry(dealerdiscount).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                DealerDiscount DealerDiscount = new DealerDiscount();
                DealerDiscount.ID = DealerDiscountViewModel.DealerDiscountID;
                DealerDiscount.Discount = DealerDiscountViewModel.DealerDiscountDiscount;
                DealerDiscount.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                DealerDiscount.ModifiedDate = DateTime.Now;
                DealerDiscount.CreatedByID = DealerDiscountViewModel.DealerDiscountCreatedByID;
                DealerDiscount.CreatedDate = DealerDiscountViewModel.DealerDiscountCreatedDate;
                DealerDiscount.StateCode = await scoreManager.FindStateCodeAsync(DealerDiscountViewModel.StateCodeID);
                DealerDiscount.DealerType = await scoreManager.FindDealerTypeAsync(DealerDiscountViewModel.DealerTypeID);

                await scoreManager.ChangeDealerDiscountAsync(DealerDiscount);

                return RedirectToAction("Index");
            }
            DealerDiscountViewModel adjVM = new DealerDiscountViewModel();
            adjVM.liStateCode = new List<StateCode>();
            adjVM.liStateCode = scoreManager.GetAllStateCode();
            adjVM.liDealerType = scoreManager.GetAllDealerType();
            return View(adjVM);
        }

        // GET: /DealerDiscount/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DealerDiscount dealerdiscount = await db.DealerDiscount.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DealerDiscount DealerDiscount = await scoreManager.FindDealerDiscountAsync(id);
            if (DealerDiscount == null)
            {
                return HttpNotFound();
            }
            return View(DealerDiscount);
        }

        // POST: /DealerDiscount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //DealerDiscount dealerdiscount = await db.DealerDiscount.FindAsync(id);
            //db.DealerDiscount.Remove(dealerdiscount);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");

            DealerDiscount DealerDiscount  = await db.DealerDiscount.FindAsync(id);
            //db.StateAdjustment.Remove(stateadjustment);
            //await db.SaveChangesAsync();
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveDealerDiscountAsync(id);
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
