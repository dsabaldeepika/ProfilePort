using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Com.CoreLane.Common.Models;
using Com.CoreLane.ConfigDB.Models.ConnectionTools;
using Com.CoreLane.ScoringEngine.Models;
using DealerPortalCRM.DAService;
using DealerPortalCRM.DAServiceImpl;
using DealerPortalCRM.ViewModels;

namespace DealerPortalCRM.Controllers
{
    public class AdjustmentController : Controller
    {
        private readonly string connectionString;
        private readonly ConnectionStringProperty connectionStringProperty;
        private readonly ScoringEngineEntities db;

        public AdjustmentController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllAdjustmentViewModelAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            var adjustmentViewModel = await scoreManager.FindAdjustmentViewModelAsync(id);

            if (adjustmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(adjustmentViewModel);
        }

        public ActionResult Create()
        {
            var adjustmentViewModel = new AdjustmentViewModel();
            IScore scoreManager = new ScoreManager(db);
            adjustmentViewModel.liAdjustmentType = new List<AdjustmentType>();
            adjustmentViewModel.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            return View(adjustmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ID,AdjustmentNumber,AdjustmentDiscount,AdjustmentTypeID,AdjustmentIsActive")] AdjustmentViewModel adjustmentViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                var adjustment = new Adjustment();
                adjustment.AdjustmentDiscount = adjustmentViewModel.AdjustmentDiscount;
                adjustment.AdjustmentNumber = adjustmentViewModel.AdjustmentNumber;
                adjustment.AdjustmentType = new AdjustmentType();
                adjustment.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustment.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustment.ModifiedDate = DateTime.Now;
                adjustment.CreatedDate = DateTime.Now;
                adjustment.IsActive = adjustmentViewModel.AdjustmentIsActive;
                adjustment.AdjustmentType_ID = adjustmentViewModel.AdjustmentTypeID;
                
                //adjustment.AdjustmentType =
                //    await scoreManager.FindAdjustmentTypeAsync(adjustmentViewModel.AdjustmentTypeID);

                await scoreManager.AddAdjustmentAsync(adjustment);
                return RedirectToAction("Index");
            }
            var adjVM = new AdjustmentViewModel();
            adjVM.liAdjustmentType = new List<AdjustmentType>();
            adjVM.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            return View(adjVM);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            var adjustmentViewModel = await scoreManager.FindAdjustmentViewModelAsync(id);
            adjustmentViewModel.liAdjustmentType = new List<AdjustmentType>();
            adjustmentViewModel.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();

            if (adjustmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(adjustmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "AdjustmentID,AdjustmentNumber,AdjustmentDiscount,AdjustmentTypeID,AdjustmentCreatedDate,AdjustmentCreatedByID,AdjustmentIsActive"
                )] AdjustmentViewModel adjustmentViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                var adjustment = new Adjustment();
                adjustment.ID = adjustmentViewModel.AdjustmentID;
                adjustment.AdjustmentDiscount = adjustmentViewModel.AdjustmentDiscount;
                adjustment.AdjustmentNumber = adjustmentViewModel.AdjustmentNumber;
                adjustment.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustment.ModifiedDate = DateTime.Now;
                adjustment.CreatedDate = adjustmentViewModel.AdjustmentCreatedDate;
                adjustment.CreatedByID = adjustmentViewModel.AdjustmentCreatedByID;
                adjustment.IsActive = adjustmentViewModel.AdjustmentIsActive;
                adjustment.AdjustmentType_ID = adjustmentViewModel.AdjustmentTypeID;
                    //await scoreManager.FindAdjustmentTypeAsync(adjustmentViewModel.AdjustmentTypeID);

                await scoreManager.ChangeAdjustmentAsync(adjustment);
                return RedirectToAction("Index");
            }

            var adjVM = await scoreManager.FindAdjustmentViewModelAsync(adjustmentViewModel.AdjustmentID);
            adjVM.liAdjustmentType = new List<AdjustmentType>();
            adjVM.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();

            return View(adjVM);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            var adjustment = await scoreManager.FindAdjustmentAsync(id);

            if (adjustment == null)
            {
                return HttpNotFound();
            }
            return View(adjustment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveAdjustmentAsync(id);
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