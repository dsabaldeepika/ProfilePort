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
    public class AdjustmentRangeController : Controller
    {
        private readonly string connectionString;
        private readonly ConnectionStringProperty connectionStringProperty;
        private readonly ScoringEngineEntities db;

        public AdjustmentRangeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /AdjustmentRange/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllAdjustmentRangeViewModelAsync());
        }

        //
        // GET: /AdjustmentRange/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AdjustmentRange/Create
        public ActionResult Create()
        {
            var adjustmentRangeViewModel = new AdjustmentRangeViewModel();
            IScore scoreManager = new ScoreManager(db);
            adjustmentRangeViewModel.liAdjustmentType = new List<AdjustmentType>();
            adjustmentRangeViewModel.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            return View(adjustmentRangeViewModel);
        }

        //
        // POST: /AdjustmentRange/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "MinTerm,MaxTerm,AdjustmentDiscount,AdjustmentTypeID")] AdjustmentRangeViewModel
                adjustmentRangeViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                var adjustmentRange = new AdjustmentRange();
                adjustmentRange.MinTerm = adjustmentRangeViewModel.MinTerm;
                adjustmentRange.MaxTerm = adjustmentRangeViewModel.MaxTerm;
                adjustmentRange.AdjustmentType = new AdjustmentType();
                adjustmentRange.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustmentRange.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustmentRange.ModifiedDate = DateTime.Now;
                adjustmentRange.CreatedDate = DateTime.Now;
                adjustmentRange.AdjustmentDiscount = adjustmentRangeViewModel.AdjustmentDiscount;
                adjustmentRange.AdjustmentType_ID = adjustmentRangeViewModel.AdjustmentTypeID;

                await scoreManager.AddAdjustmentRangeAsync(adjustmentRange);
                return RedirectToAction("Index");
            }
            var adjRangeVM = new AdjustmentRangeViewModel();
            adjRangeVM.liAdjustmentType = new List<AdjustmentType>();
            adjRangeVM.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();
            return View(adjRangeVM);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            var adjustmentRangeViewModel = await scoreManager.FindAdjustmentRangeViewModelAsync(id);
            adjustmentRangeViewModel.liAdjustmentType = new List<AdjustmentType>();
            adjustmentRangeViewModel.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();

            if (adjustmentRangeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(adjustmentRangeViewModel);
        }

        [HttpGet]
        public JsonResult GetAllActiveAdjustmentType()
        {
            IScore score = new ScoreManager(db);
            return Json(new SelectList(score.GetAllActiveAdjustmentType().ToArray(), "ID", "DisplayName"),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EditAdjustmentRange(
            [Bind(Include = "AdjustmentRangeID,MinTerm,MaxTerm,AdjustmentDiscount,AdjustmentTypeID")] AdjustmentRangeViewModel adjustmentRangeViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            var adjustmentRange = new AdjustmentRange();
            adjustmentRange.ID = adjustmentRangeViewModel.AdjustmentRangeID;
            adjustmentRange.AdjustmentDiscount = adjustmentRangeViewModel.AdjustmentDiscount;
            adjustmentRange.MinTerm = adjustmentRangeViewModel.MinTerm;
            adjustmentRange.MaxTerm = adjustmentRangeViewModel.MaxTerm;
            adjustmentRange.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
            adjustmentRange.ModifiedDate = DateTime.Now;
            adjustmentRange.AdjustmentType_ID = adjustmentRangeViewModel.AdjustmentTypeID;
            //adjustmentRange.AdjustmentType =
            //await scoreManager.FindAdjustmentTypeAsync(adjustmentRangeViewModel.AdjustmentTypeID);
            
            await scoreManager.AdjustmentRangeInLineEditAsync(adjustmentRange);
            var savedAdjustmentRange =
                await scoreManager.FindAdjustmentRangeInLineEditingViewModelAsync(adjustmentRange.ID);
            return Json(savedAdjustmentRange, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "AdjustmentRangeID,MinTerm,MaxTerm,AdjustmentDiscount,AdjustmentTypeID,AdjustmentRangeCreatedDate,AdjustmentRangeCreatedByID"
                )] AdjustmentRangeViewModel adjustmentRangeViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                var adjustmentRange = new AdjustmentRange();
                adjustmentRange.ID = adjustmentRangeViewModel.AdjustmentRangeID;
                adjustmentRange.AdjustmentDiscount = adjustmentRangeViewModel.AdjustmentDiscount;
                adjustmentRange.MinTerm = adjustmentRangeViewModel.MinTerm;
                adjustmentRange.MaxTerm = adjustmentRangeViewModel.MaxTerm;
                adjustmentRange.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustmentRange.ModifiedDate = DateTime.Now;
                adjustmentRange.CreatedDate = adjustmentRangeViewModel.AdjustmentRangeCreatedDate;
                adjustmentRange.CreatedByID = adjustmentRangeViewModel.AdjustmentRangeCreatedByID;
                adjustmentRange.AdjustmentType_ID = adjustmentRangeViewModel.AdjustmentTypeID;
                //adjustmentRange.AdjustmentType = await scoreManager.FindAdjustmentTypeAsync(adjustmentRangeViewModel.AdjustmentTypeID);

                await scoreManager.ChangeAdjustmentRangeAsync(adjustmentRange);
                return RedirectToAction("Index");
            }

            var adjRangeVM =
                await scoreManager.FindAdjustmentRangeViewModelAsync(adjustmentRangeViewModel.AdjustmentRangeID);
            adjRangeVM.liAdjustmentType = new List<AdjustmentType>();
            adjRangeVM.liAdjustmentType = scoreManager.GetAllActiveAdjustmentType();

            return View(adjRangeVM);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}