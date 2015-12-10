using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;


using DealerPortalCRM.DAService;
using DealerPortalCRM.DAServiceImpl;
using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
using Com.CoreLane.Common.Models;

namespace DealerPortalCRM.Controllers
{
    public class VehicleClassTypeController : Controller
    {
       // private ScoringEngineEntities db = new ScoringEngineEntities();
        string connectionString;
        ConnectionStringProperty connectionStringProperty;
        private ScoringEngineEntities db;

        public VehicleClassTypeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /VehicleClassType/
        public async Task<ActionResult> Index()
        {
            //return View(await db.VehicleClassType.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllVehicleClassTypeAsync());
        }

        // GET: /VehicleClassType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleClassType vehicleclasstype = await db.VehicleClassType.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            VehicleClassType VehicleClassType = await scoreManager.FindVehicleClassTypeAsync(id);
            if (VehicleClassType == null)
            {
                return HttpNotFound();
            }
            return View(VehicleClassType);
        }

        // GET: /VehicleClassType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VehicleClassType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,Name,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] VehicleClassType vehicleclasstype)
        {
            if (ModelState.IsValid)
            {
                //db.VehicleClassType.Add(vehicleclasstype);
                //await db.SaveChangesAsync();
                DateTime currentTime = DateTime.Now;
                IScore scoreManager = new ScoreManager(db);

                vehicleclasstype.CreatedDate = currentTime;
                vehicleclasstype.ModifiedDate = currentTime;
                vehicleclasstype.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                vehicleclasstype.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);

                
                await scoreManager.AddVehicleClassTypeAsync(vehicleclasstype);

                return RedirectToAction("Index");
            }

            return View(vehicleclasstype);
        }

        // GET: /VehicleClassType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleClassType vehicleclasstype = await db.VehicleClassType.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            VehicleClassType vehicleclasstype = await scoreManager.FindVehicleClassTypeAsync(id);

            if (vehicleclasstype == null)
            {
                return HttpNotFound();
            }
            return View(vehicleclasstype);
        }

        // POST: /VehicleClassType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] VehicleClassType vehicleclasstype)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vehicleclasstype).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);
                DateTime currentTime = DateTime.Now;
                vehicleclasstype.ModifiedDate = currentTime;
                vehicleclasstype.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                vehicleclasstype.CreatedDate = vehicleclasstype.CreatedDate;
                vehicleclasstype.CreatedByID = vehicleclasstype.CreatedByID;
                
                await scoreManager.ChangeVehicleClassTypeAsync(vehicleclasstype);

                return RedirectToAction("Index");
            }
            return View(vehicleclasstype);
        }

        // GET: /VehicleClassType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //VehicleClassType vehicleclasstype = await db.VehicleClassType.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            VehicleClassType vehicleclasstype = await scoreManager.FindVehicleClassTypeAsync(id);

            if (vehicleclasstype == null)
            {
                return HttpNotFound();
            }
            return View(vehicleclasstype);
        }

        // POST: /VehicleClassType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //VehicleClassType vehicleclasstype = await db.VehicleClassType.FindAsync(id);
            //db.VehicleClassType.Remove(vehicleclasstype);
            //await db.SaveChangesAsync();
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveVehicleClassTypeAsync(id);
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
