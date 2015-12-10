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
    public class DocFeeController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public DocFeeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /DocFee/
        public async Task<ActionResult> Index()
        {
            //return View(await db.DocFee.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllDocFeeAsync());
        }

        // GET: /DocFee/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DocFee docfee = await db.DocFee.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            DocFee DocFee = await scoreManager.FindDocFeeAsync(id);
            if (DocFee == null)
            {
                return HttpNotFound();
            }
            return View(DocFee);
        }

        // GET: /DocFee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DocFee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MinTerm,MaxTerm,MaxMiles,VehicleAge,VehicleClass,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] DocFee DocFee)
        {
            if (ModelState.IsValid)
            {
                //db.DocFee.Add(docfee);
                //await db.SaveChangesAsync();
                DateTime currentTime = DateTime.Now;

                IScore scoreManager = new ScoreManager(db);

                DocFee.CreatedDate = currentTime;
                DocFee.ModifiedDate = currentTime;
                DocFee.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name); 
                DocFee.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);

                await scoreManager.AddDocFeeAsync(DocFee);

                return RedirectToAction("Index");
            }

            return View(DocFee);
        }

        // GET: /DocFee/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DocFee docfee = await db.DocFee.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DocFee DocFee = await scoreManager.FindDocFeeAsync(id);
            if (DocFee == null)
            {
                return HttpNotFound();
            }
            return View(DocFee);
        }

        // POST: /DocFee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MinTerm,MaxTerm,MaxMiles,VehicleAge,VehicleClass,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] DocFee DocFee)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(docfee).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);
                DateTime currentTime = DateTime.Now;
                DocFee.ModifiedDate = currentTime;
                DocFee.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                DocFee.CreatedDate = DocFee.CreatedDate;
                DocFee.CreatedByID = DocFee.CreatedByID;
                
                await scoreManager.ChangeDocFeeAsync(DocFee);

                return RedirectToAction("Index");
            }
            return View(DocFee);
        }

        // GET: /DocFee/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DocFee docfee = await db.DocFee.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DocFee DocFee = await scoreManager.FindDocFeeAsync(id);
            if (DocFee == null)
            {
                return HttpNotFound();
            }
            return View(DocFee);
        }

        // POST: /DocFee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //DocFee docfee = await db.DocFee.FindAsync(id);
            //db.DocFee.Remove(docfee);
            //await db.SaveChangesAsync();
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveDocFeeAsync(id);

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
