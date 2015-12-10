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
    public class PricingBaseController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public PricingBaseController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /PricingBase/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllPricingBaseViewModelAsync());
        }

        [HttpPost]
        public ActionResult PricingBaseSave(List<PricingBaseViewModel> pricingBaseViewModel)
        {
          
            //CompanyEntities DbCompany = new CompanyEntities();
            IScore scoreManager = new ScoreManager(db);

            foreach (PricingBaseViewModel pb in pricingBaseViewModel)
            {
                PricingBase Existed_pb = db.PricingBase.Find(pb.PricingBaseID);
                Existed_pb.MinBV = pb.PricingBaseMinBV;
                Existed_pb.MaxBV = pb.PricingBaseMaxBV;
                Existed_pb.MinLTV = pb.PricingBaseMinLTV;
                Existed_pb.MaxLTV = pb.PricingBaseMaxLTV;
                Existed_pb.PricingRateDiscount = pb.PricingBasePricingRateDiscount;
                Existed_pb.IsActive = pb.PricingBaseIsActive;
                Existed_pb.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                Existed_pb.ModifiedDate = DateTime.Now;
            }

            db.SaveChanges();
            //return View(scoreManager.GetAllPricingBaseViewModelAsync());
            return RedirectToAction("Index");
        }

        // GET: /PricingBase/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            
            PricingBaseViewModel pricingbaseViewModel = await scoreManager.FindPricingBaseViewModelAsync(id);

            if (pricingbaseViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pricingbaseViewModel);
        }

        // GET: /PricingBase/Create
        public ActionResult Create()
        {
            PricingBaseViewModel PricingBaseViewModel = new PricingBaseViewModel();
            return View(PricingBaseViewModel);
        }

        // POST: /PricingBase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PricingBaseMinBV,PricingBaseMaxBV,PricingBaseMinLTV,PricingBaseMaxLTV,PricingBasePricingRateDiscount,PricingBaseIsActive")] PricingBaseViewModel PricingBaseViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                PricingBase PricingBase = new PricingBase();
                DateTime currentTime = DateTime.Now;
                PricingBase.CreatedDate = currentTime;
                PricingBase.ModifiedDate = currentTime;
                PricingBase.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                PricingBase.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);

                PricingBase.MinBV = PricingBaseViewModel.PricingBaseMinBV;
                PricingBase.MaxBV = PricingBaseViewModel.PricingBaseMaxBV;
                PricingBase.MinLTV = PricingBaseViewModel.PricingBaseMinLTV;
                PricingBase.MaxLTV = PricingBaseViewModel.PricingBaseMaxLTV;
                PricingBase.PricingRateDiscount = PricingBaseViewModel.PricingBasePricingRateDiscount;
                PricingBase.IsActive = PricingBaseViewModel.PricingBaseIsActive;

                await scoreManager.AddPricingBaseAsync(PricingBase);

                return RedirectToAction("Index");

            }

            return View(PricingBaseViewModel);
        }

        // GET: /PricingBase/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            PricingBaseViewModel PricingBaseViewModel = await scoreManager.FindPricingBaseViewModelAsync(id);

            if (PricingBaseViewModel == null)
            {
                return HttpNotFound();
            }
            return View(PricingBaseViewModel);
        }

        // POST: /PricingBase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PricingBaseID,PricingBaseMinBV,PricingBaseMaxBV,PricingBaseMinLTV,PricingBaseMaxLTV,PricingBasePricingRateDiscount,PricingBaseCreatedDate,PricingBaseCreatedByID,PricingBaseIsActive")] PricingBaseViewModel PricingBaseViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                PricingBase PricingBase = new PricingBase();
                DateTime currentTime = DateTime.Now;

                PricingBase.CreatedDate = PricingBaseViewModel.PricingBaseCreatedDate;
                PricingBase.CreatedByID = PricingBaseViewModel.PricingBaseCreatedByID;

                PricingBase.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                PricingBase.ModifiedDate = currentTime;
                PricingBase.ID = PricingBaseViewModel.PricingBaseID;
                PricingBase.MinBV = PricingBaseViewModel.PricingBaseMinBV;
                PricingBase.MaxBV = PricingBaseViewModel.PricingBaseMaxBV;
                PricingBase.MinLTV = PricingBaseViewModel.PricingBaseMinLTV;
                PricingBase.MaxLTV = PricingBaseViewModel.PricingBaseMaxLTV;
                PricingBase.PricingRateDiscount = PricingBaseViewModel.PricingBasePricingRateDiscount;
                PricingBase.IsActive = PricingBaseViewModel.PricingBaseIsActive;

                await scoreManager.ChangePricingBaseAsync(PricingBase);
                return RedirectToAction("Index");
            }
            return View(PricingBaseViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditPricingBase([Bind(Include = "PricingBaseID,PricingBaseMinBV,PricingBaseMaxBV,PricingBaseMinLTV,PricingBaseMaxLTV,PricingBasePricingRateDiscount")] PricingBaseViewModel PricingBaseViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            PricingBase PricingBase = new PricingBase();
            DateTime currentTime = DateTime.Now;
            PricingBase.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
            PricingBase.ModifiedDate = currentTime;
            PricingBase.ID = PricingBaseViewModel.PricingBaseID;
            PricingBase.MinBV = PricingBaseViewModel.PricingBaseMinBV;
            PricingBase.MaxBV = PricingBaseViewModel.PricingBaseMaxBV;
            PricingBase.MinLTV = PricingBaseViewModel.PricingBaseMinLTV;
            PricingBase.MaxLTV = PricingBaseViewModel.PricingBaseMaxLTV;
            PricingBase.PricingRateDiscount = PricingBaseViewModel.PricingBasePricingRateDiscount;
            await scoreManager.PricingBaseInLineEditAsync(PricingBase);
            PricingBaseInLineEditingViewModel savedPricingBase = await scoreManager.FindPricingBaseInLineEditingViewModelAsync(PricingBase.ID);
            return Json(savedPricingBase, JsonRequestBehavior.AllowGet);
        }


        // GET: /PricingBase/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            PricingBase pricingbase = await scoreManager.FindPricingBaseAsync(id);

            if (pricingbase == null)
            {
                return HttpNotFound();
            }
            return View(pricingbase);
        }

        // POST: /PricingBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemovePricingBaseAsync(id);
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
