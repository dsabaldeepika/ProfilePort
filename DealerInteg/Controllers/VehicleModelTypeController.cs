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
    public class VehicleModelTypeController : Controller
    {
        // private ScoringEngineEntities db = new ScoringEngineEntities();
        string connectionString;
        ConnectionStringProperty connectionStringProperty;
        private ScoringEngineEntities db;

        public VehicleModelTypeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /VehicleModelType/
        public async Task<ActionResult> Index()
        {
            //return View(await db.VehicleModelType.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllVehicleModelTypeViewModelAsync());
        }

        // GET: /VehicleModelType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleModelType vehiclemodeltype = await db.VehicleModelType.FindAsync(id);
            //IScore scoreManager = new ScoreManager(db);
            //VehicleModelType VehicleModelType = await scoreManager.FindVehicleModelTypeAsync(id);

            VehicleModelTypeViewModel vehicleModelTypeViewModel = new VehicleModelTypeViewModel();
            IScore scoreManager = new ScoreManager(db);
            vehicleModelTypeViewModel = await scoreManager.FindVehicleModelTypeViewModelAsync(id);
            vehicleModelTypeViewModel.liVehicleMakeType = new List<VehicleMakeType>();
            vehicleModelTypeViewModel.liVehicleMakeType = scoreManager.GetAllActiveVehicleMakeType();

            if (vehicleModelTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModelTypeViewModel);
        }

        // GET: /VehicleModelType/Create
        public ActionResult Create()
        {
            //return View();
            VehicleModelTypeViewModel vehicleModelTypeViewModel = new VehicleModelTypeViewModel();
            IScore scoreManager = new ScoreManager(db);
            vehicleModelTypeViewModel.liVehicleMakeType = new List<VehicleMakeType>();
            vehicleModelTypeViewModel.liVehicleMakeType = scoreManager.GetAllActiveVehicleModelMakeType();

            return View(vehicleModelTypeViewModel);
        }

        // POST: /VehicleModelType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VehiclModelTypeID,VehicleModelTypeName,VehicleMakeTypeID")] VehicleModelTypeViewModel vehicleModelTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.VehicleModelType.Add(vehiclemodeltype);
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);
                VehicleModelType vehicleModelType = new VehicleModelType();

                DateTime currentTime = DateTime.Now;

                vehicleModelType.CreatedDate = currentTime;
                vehicleModelType.ModifiedDate = currentTime;
                vehicleModelType.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                vehicleModelType.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                vehicleModelType.Name = vehicleModelTypeViewModel.VehicleModelTypeName;
                vehicleModelType.VehicleMakeType = await scoreManager.FindVehicleMakeTypeAsync(vehicleModelTypeViewModel.VehicleMakeTypeID);


                await scoreManager.AddVehicleModelTypeAsync(vehicleModelType);
                return RedirectToAction("Index");
            }

            return View(vehicleModelTypeViewModel);
        }

        //Action result for ajax call
        [HttpPost]
        public ActionResult GetModelByMakeId(int makeid)
        {
            try
            {
                IScore scoreManager = new ScoreManager(db);
                List<VehicleModelType> objmodel = new List<VehicleModelType>();
                objmodel = scoreManager.GetVehicleModelTypeForMake(makeid);  // GetAllActiveVehicleModelType();//.Where(m => m.VehicleMakeType.ID == makeid).ToList();
                SelectList obgmodel = new SelectList(objmodel, "Id", "Name", 0);
                return Json(obgmodel);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // GET: /VehicleModelType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleModelType vehiclemodeltype = await db.VehicleModelType.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            VehicleModelTypeViewModel vehicleModelTypeViewModel = await scoreManager.FindVehicleModelTypeViewModelAsync(id);

            vehicleModelTypeViewModel.liVehicleMakeType = new List<VehicleMakeType>();
            vehicleModelTypeViewModel.liVehicleMakeType = scoreManager.GetAllActiveVehicleModelMakeType();


            if (vehicleModelTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModelTypeViewModel);
        }

        // POST: /VehicleModelType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VehiclModelTypeID,VehicleModelTypeName,VehicleMakeTypeID,VehicleModelTypeCreatedDate,VehicleModelTypeCreatedByID")] VehicleModelTypeViewModel vehiclemodeltypeViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vehiclemodeltype).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);
                VehicleModelType vehicleModelType = new VehicleModelType();

                vehicleModelType.Name = vehiclemodeltypeViewModel.VehicleModelTypeName;
                vehicleModelType.ID = vehiclemodeltypeViewModel.VehiclModelTypeID;
                vehicleModelType.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                vehicleModelType.ModifiedDate = DateTime.Now;
                vehicleModelType.CreatedDate = vehiclemodeltypeViewModel.VehicleModelTypeCreatedDate;
                vehicleModelType.CreatedByID = vehiclemodeltypeViewModel.VehicleModelTypeCreatedByID;
                vehicleModelType.VehicleMakeType = await scoreManager.FindVehicleMakeTypeAsync(vehiclemodeltypeViewModel.VehicleMakeTypeID);

                await scoreManager.ChangeVehicleModelTypeAsync(vehicleModelType);


                return RedirectToAction("Index");
            }
            return View(vehiclemodeltypeViewModel);
        }

        // GET: /VehicleModelType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleModelType vehiclemodeltype = await db.VehicleModelType.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            VehicleModelType vehicleModelType = await scoreManager.FindVehicleModelTypeAsync(id);
            if (vehicleModelType == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModelType);
        }

        // POST: /VehicleModelType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveVehicleModelTypeAsync(id);
            return RedirectToAction("Index");
            //VehicleModelType vehiclemodeltype = await db.VehicleModelType.FindAsync(id);
            //db.VehicleModelType.Remove(vehiclemodeltype);
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
