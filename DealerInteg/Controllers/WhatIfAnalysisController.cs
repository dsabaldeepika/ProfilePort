using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Com.CoreLane.Common.Models;
using Com.CoreLane.ConfigDB.Models.ConnectionTools;
using Com.CoreLane.ScoringEngine.Models;
using DealerPortalCRM.DAServiceImpl;
using ScoringEngine.Services;
using ScoringEngine.ServicesImpl;

namespace DealerPortalCRM.Controllers
{
    public class WhatIfAnalysisController : Controller
    {
        // private ScoringEngineEntities db = new ScoringEngineEntities();
        private readonly string connectionString;
        private readonly ConnectionStringProperty connectionStringProperty;
        private readonly ScoringEngineEntities db;

        public WhatIfAnalysisController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /WhatIfAnalysis/
        public async Task<ActionResult> Index()
        {
            return View(await db.TempApplicationModel.ToListAsync());
        }

        // GET: /WhatIfAnalysis/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempApplicationModel tempapplicationmodel = await db.TempApplicationModel.FindAsync(id);
            if (tempapplicationmodel == null)
            {
                return HttpNotFound();
            }
            return View(tempapplicationmodel);
        }

        // GET: /WhatIfAnalysis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /WhatIfAnalysis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ID,Name,Value,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] TempApplicationModel
                tempapplicationmodel)
        {
            if (ModelState.IsValid)
            {
                db.TempApplicationModel.Add(tempapplicationmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tempapplicationmodel);
        }

        // GET: /WhatIfAnalysis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempApplicationModel tempapplicationmodel = await db.TempApplicationModel.FindAsync(id);
            if (tempapplicationmodel == null)
            {
                return HttpNotFound();
            }
            return View(tempapplicationmodel);
        }

        // POST: /WhatIfAnalysis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "ID,Name,Value,CreatedByID,ModifiedByID,CreatedDate,ModifiedDate")] TempApplicationModel
                tempapplicationmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tempapplicationmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tempapplicationmodel);
        }

        // GET: /WhatIfAnalysis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempApplicationModel tempapplicationmodel = await db.TempApplicationModel.FindAsync(id);
            if (tempapplicationmodel == null)
            {
                return HttpNotFound();
            }
            return View(tempapplicationmodel);
        }

        // POST: /WhatIfAnalysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TempApplicationModel tempapplicationmodel = await db.TempApplicationModel.FindAsync(id);
            db.TempApplicationModel.Remove(tempapplicationmodel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult RunScoreEngine()
        {
            IScoringEngineDbService scoringEngineDbService = new ScoringEngineDbService(db);
            IApplicationScore applicationScore = new ApplicationScore();
            applicationScore.GetContextFromDb(scoringEngineDbService);
            applicationScore.ScoreApplication(scoringEngineDbService, "0");

            return RedirectToAction("ScoringEngineResults");
        }


        // GET: /ScoringEngineResults/
        public async Task<ActionResult> ScoringEngineResults()
        {
            return View(await db.TempScoreEngineResults.ToListAsync());
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