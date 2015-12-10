using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;


using DealerPortalCRM.DAService;
using DealerPortalCRM.DAServiceImpl;
using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
using Com.CoreLane.Common.Models;

namespace DealerPortalCRM
{
    public class AdjustmentTypeController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public AdjustmentTypeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }       

        // GET: /AdjustmentType/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllAdjustmentTypeAsync());
        }

        // GET: /AdjustmentType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            AdjustmentType adjustmenttype = await scoreManager.FindAdjustmentTypeAsync(id);
            
            if (adjustmenttype == null)
            {
                return HttpNotFound();
            }
            return View(adjustmenttype);
        }

        // GET: /AdjustmentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AdjustmentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,Type,DisplayName,IsActive")] AdjustmentType adjustmenttype)
        {
            if (ModelState.IsValid)
            {
                IScore scoreManager = new ScoreManager(db);
                DateTime currentTime = DateTime.Now;
                adjustmenttype.CreatedDate = currentTime;
                adjustmenttype.ModifiedDate = currentTime;
                adjustmenttype.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name); 
                adjustmenttype.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                
                await scoreManager.AddAdjustmentTypeAsync(adjustmenttype);

                return RedirectToAction("Index");
            }

            return View(adjustmenttype);
        }

        // GET: /AdjustmentType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            IScore scoreManager = new ScoreManager(db);
            AdjustmentType adjustmenttype = await scoreManager.FindAdjustmentTypeAsync(id);
            
            if (adjustmenttype == null)
            {
                return HttpNotFound();
            }
            return View(adjustmenttype);
        }

        // POST: /AdjustmentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Type,DisplayName,CreatedDate,CreatedByID,IsActive")] AdjustmentType adjustmenttype)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(adjustmenttype).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);
                DateTime currentTime = DateTime.Now;
                adjustmenttype.ModifiedDate = currentTime;
                adjustmenttype.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                adjustmenttype.CreatedByID = adjustmenttype.CreatedByID;
                adjustmenttype.CreatedDate = adjustmenttype.CreatedDate;
                
                await scoreManager.ChangeAdjustmentTypeAsync(adjustmenttype);
                return RedirectToAction("Index");
            }
            return View(adjustmenttype);
        }

        // GET: /AdjustmentType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            AdjustmentType adjustmenttype = await scoreManager.FindAdjustmentTypeAsync(id);
            
            if (adjustmenttype == null)
            {
                return HttpNotFound();
            }
            return View(adjustmenttype);
        }

        // POST: /AdjustmentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveAdjustmentTypeAsync(id);
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
