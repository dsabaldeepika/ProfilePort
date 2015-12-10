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
    public class StateCodeController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;
        private ScoringEngineEntities db;

        public StateCodeController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }
        //  private ScoringEngineEntities db = new ScoringEngineEntities();

        // GET: /StateCode/
        public async Task<ActionResult> Index()
        {
            //return View(await db.StateCode.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllStateCodeAsync());
        }

        // GET: /StateCode/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateCode statecode = await db.StateCode.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            StateCode statecode = await scoreManager.FindStateCodeAsync(id);

            if (statecode == null)
            {
                return HttpNotFound();
            }
            return View(statecode);
        }

        // GET: /StateCode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StateCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Abbreviation,SortOrder,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] StateCode statecode)
        {
            if (ModelState.IsValid)
            {
                //db.StateCode.Add(statecode);
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);
                DateTime currentTime = DateTime.Now;

                statecode.CreatedDate = currentTime;
                statecode.ModifiedDate = currentTime;
                statecode.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                statecode.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);


                await scoreManager.AddStateCodeAsync(statecode);

                return RedirectToAction("Index");
            }

            return View(statecode);
        }

        // GET: /StateCode/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateCode statecode = await db.StateCode.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            StateCode statecode = await scoreManager.FindStateCodeAsync(id);

            if (statecode == null)
            {
                return HttpNotFound();
            }
            return View(statecode);
        }

        // POST: /StateCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Abbreviation,SortOrder,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] StateCode statecode)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(statecode).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                IScore scoreManager = new ScoreManager(db);

                DateTime currentTime = DateTime.Now;
                statecode.ModifiedDate = currentTime;
                statecode.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                statecode.CreatedDate = statecode.CreatedDate;
                statecode.CreatedByID = statecode.CreatedByID;

                await scoreManager.ChangeStateCodeAsync(statecode);


                return RedirectToAction("Index");
            }
            return View(statecode);
        }

        // GET: /StateCode/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //StateCode statecode = await db.StateCode.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            StateCode statecode = await scoreManager.FindStateCodeAsync(id);


            if (statecode == null)
            {
                return HttpNotFound();
            }
            return View(statecode);
        }

        // POST: /StateCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //StateCode statecode = await db.StateCode.FindAsync(id);
            //db.StateCode.Remove(statecode);
            //await db.SaveChangesAsync();
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveStateCodeAsync(id);

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
