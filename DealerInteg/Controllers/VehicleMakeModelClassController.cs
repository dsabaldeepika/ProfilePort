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
    public class VehicleMakeModelClassController : Controller
    {
        // private ScoringEngineEntities db = new ScoringEngineEntities();
        private readonly string connectionString;
        private readonly ConnectionStringProperty connectionStringProperty;
        private readonly ScoringEngineEntities db;

        public VehicleMakeModelClassController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /VehicleMakeModelClass/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllVehicleMakeModelClassViewModelAsync());
            //return View(await db.VehicleMakeModelClass.ToListAsync());
        }

        // GET: /VehicleMakeModelClass/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleMakeModelClass vehiclemakemodelclass = await db.VehicleMakeModelClass.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            var vehiclemakemodelclassviewmodel = await scoreManager.FindVehicleMakeModelClassViewModelAsync(id);

            return View(vehiclemakemodelclassviewmodel);
        }

        // GET: /VehicleMakeModelClass/Create
        public ActionResult Create()
        {
            //return View();
            var vehiclemakemodelclassviewmodel = new VehicleMakeModelClassViewModel();
            IScore scoreManager = new ScoreManager(db);
            vehiclemakemodelclassviewmodel.liVehicleMakeType = new List<VehicleMakeType>();
            vehiclemakemodelclassviewmodel.liVehicleMakeType = scoreManager.GetAllActiveVehicleMakeType();

            vehiclemakemodelclassviewmodel.liVehicleModelType = new List<VehicleModelType>();
            //vehiclemakemodelclassviewmodel.liVehicleModelType = scoreManager.GetVehicleModelTypeByVehicleMakeTypeID(vehiclemakemodelclassviewmodel.liVehicleMakeType[0].ID);

            vehiclemakemodelclassviewmodel.liVehicleClassType = new List<VehicleClassType>();
            vehiclemakemodelclassviewmodel.liVehicleClassType = scoreManager.GetAllActiveVehicleClassType();

            return View(vehiclemakemodelclassviewmodel);
        }

        //[HttpGet]
        //public JsonResult VehicleModelTypeListByVehicleMakeTypeID(int id)
        //{
        //    IScore score = new ScoreManager(db);
        //    return Json(new SelectList((score.GetVehicleModelTypeByVehicleMakeTypeID(id)).ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
        //}

        // POST: /VehicleMakeModelClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "VehicleMakeTypeID,VehicleMakeTypeID, VehicleModelTypeID, VehicleClassTypeID")] VehicleMakeModelClassViewModel vehiclemakemodelclassViewModel)
        {
            var bSuccess = false;
            if (ModelState.IsValid)
            {
                IScore scoreManager = new ScoreManager(db);

                //checking to see if record already exists

                bSuccess = scoreManager.FindVehicleMakeModelAsync(vehiclemakemodelclassViewModel.VehicleMakeTypeID,
                    vehiclemakemodelclassViewModel.VehicleModelTypeID, vehiclemakemodelclassViewModel.VehicleClassTypeID);

                if (bSuccess == false)
                {
                    var vehicleMakeModelClass = new VehicleMakeModelClass();

                    vehicleMakeModelClass.VehicleMakeType_ID = vehiclemakemodelclassViewModel.VehicleMakeTypeID;
                    vehicleMakeModelClass.VehicleModelType_ID = vehiclemakemodelclassViewModel.VehicleMakeTypeID;
                    vehicleMakeModelClass.VehicleClassType_ID = vehiclemakemodelclassViewModel.VehicleClassTypeID;

                    vehicleMakeModelClass.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                    vehicleMakeModelClass.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                    vehicleMakeModelClass.CreatedDate = DateTime.Now;
                    vehicleMakeModelClass.ModifiedDate = DateTime.Now;

                    await scoreManager.AddVehicleMakeModelClassAsync(vehicleMakeModelClass);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: /VehicleMakeModelClass/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            //VehicleMakeModelClass vehiclemakemodelclass = await db.VehicleMakeModelClass.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            var vehicleMakeModelClassViewModel = await scoreManager.FindVehicleMakeModelClassViewModelAsync(id);

            vehicleMakeModelClassViewModel.liVehicleMakeType = scoreManager.GetAllActiveVehicleMakeType();

            vehicleMakeModelClassViewModel.liVehicleModelType = new List<VehicleModelType>();
            vehicleMakeModelClassViewModel.liVehicleModelType =
                scoreManager.GetVehicleModelTypeByVehicleMakeTypeID(vehicleMakeModelClassViewModel.VehicleMakeTypeID);

            vehicleMakeModelClassViewModel.liVehicleClassType = new List<VehicleClassType>();
            vehicleMakeModelClassViewModel.liVehicleClassType = scoreManager.GetAllActiveVehicleClassType();

            return View(vehicleMakeModelClassViewModel);
        }

        // POST: /VehicleMakeModelClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "VehicleMakeModelClassID,VehicleMakeTypeID, VehicleModelTypeID, VehicleClassTypeID, VehicleMakeModelClassCreatedDate,VehicleMakeModelClassCreatedByID"
                )] VehicleMakeModelClassViewModel vehiclemakemodelclassViewModel)
        {
            var bSuccess = false;
            if (ModelState.IsValid)
            {
                IScore scoreManager = new ScoreManager(db);

                //checking to see if record already exists
                bSuccess = scoreManager.FindVehicleMakeModelAsync(vehiclemakemodelclassViewModel.VehicleMakeTypeID,
                    vehiclemakemodelclassViewModel.VehicleModelTypeID, vehiclemakemodelclassViewModel.VehicleClassTypeID);

                if (!bSuccess)
                {
                    var vehicleMakeModelClass = new VehicleMakeModelClass();
                    vehicleMakeModelClass.ID = vehiclemakemodelclassViewModel.VehicleMakeModelClassID;
                    vehicleMakeModelClass.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                    vehicleMakeModelClass.ModifiedDate = DateTime.Now;
                    vehicleMakeModelClass.CreatedDate = vehiclemakemodelclassViewModel.VehicleMakeModelClassCreatedDate;
                    vehicleMakeModelClass.CreatedByID = vehiclemakemodelclassViewModel.VehicleMakeModelClassCreatedByID;
                    vehicleMakeModelClass.VehicleMakeType_ID = vehiclemakemodelclassViewModel.VehicleMakeTypeID;
                    vehicleMakeModelClass.VehicleModelType_ID = vehiclemakemodelclassViewModel.VehicleMakeTypeID;
                    vehicleMakeModelClass.VehicleClassType_ID = vehiclemakemodelclassViewModel.VehicleClassTypeID;

                    await scoreManager.ChangeVehicleMakeModelClassAsync(vehicleMakeModelClass);
                }
            }

            return RedirectToAction("Index");
        }

        // GET: /VehicleMakeModelClass/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleMakeModelClass vehiclemakemodelclass = await db.VehicleMakeModelClass.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            var vehicleMakeModelClass = await scoreManager.FindVehicleMakeModelClassAsync(id);


            if (vehicleMakeModelClass == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMakeModelClass);
        }

        // POST: /VehicleMakeModelClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveVehicleMakeModelClassAsync(id);
            return RedirectToAction("Index");

            //VehicleMakeModelClass vehiclemakemodelclass = await db.VehicleMakeModelClass.FindAsync(id);
            //db.VehicleMakeModelClass.Remove(vehiclemakemodelclass);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");
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