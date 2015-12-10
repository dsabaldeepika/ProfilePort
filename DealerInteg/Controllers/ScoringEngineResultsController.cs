using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;


using Com.CoreLane.ConfigDB.Models.ConnectionTools; using Com.CoreLane.ScoringEngine.Models;
using Com.CoreLane.Common.Models;

namespace DealerPortalCRM.Controllers
{
    public class ScoringEngineResultsController : Controller
    {
       string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public ScoringEngineResultsController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /ScoringEngineResults/
        public async Task<ActionResult> Index()
        {
            return View(await db.TempScoreEngineResults.ToListAsync());
        }

        // GET: /ScoringEngineResults/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempScoreEngineResults tempscoreengineresults = await db.TempScoreEngineResults.FindAsync(id);
            if (tempscoreengineresults == null)
            {
                return HttpNotFound();
            }
            return View(tempscoreengineresults);
        }

        // GET: /ScoringEngineResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ScoringEngineResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,Name,Expression,Result,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] TempScoreEngineResults tempscoreengineresults)
        {
            if (ModelState.IsValid)
            {
                db.TempScoreEngineResults.Add(tempscoreengineresults);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tempscoreengineresults);
        }

        // GET: /ScoringEngineResults/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempScoreEngineResults tempscoreengineresults = await db.TempScoreEngineResults.FindAsync(id);
            if (tempscoreengineresults == null)
            {
                return HttpNotFound();
            }
            return View(tempscoreengineresults);
        }

        // POST: /ScoringEngineResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,Name,Expression,Result,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] TempScoreEngineResults tempscoreengineresults)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tempscoreengineresults).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tempscoreengineresults);
        }

        // GET: /ScoringEngineResults/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempScoreEngineResults tempscoreengineresults = await db.TempScoreEngineResults.FindAsync(id);
            if (tempscoreengineresults == null)
            {
                return HttpNotFound();
            }
            return View(tempscoreengineresults);
        }

        // POST: /ScoringEngineResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TempScoreEngineResults tempscoreengineresults = await db.TempScoreEngineResults.FindAsync(id);
            db.TempScoreEngineResults.Remove(tempscoreengineresults);
            await db.SaveChangesAsync();
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
