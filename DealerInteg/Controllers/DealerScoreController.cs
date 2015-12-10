using System;
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
    public class DealerScoreController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public DealerScoreController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /DealerScore/
        public async Task<ActionResult> Index()
        {
            //return View(await db.DealerScore.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllDealerScoreViewModelAsync());
        }

        // GET: /DealerScore/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DealerScore dealerscore = await db.DealerScore.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DealerScoreViewModel dealerscoreViewModel = await scoreManager.FindDealerScoreViewModelAsync(id);

            if (dealerscoreViewModel == null)
            {
                return HttpNotFound();
            }
            return View(dealerscoreViewModel);
        }

        // GET: /DealerScore/Create
        public ActionResult Create()
        {
            DealerScoreViewModel DealerScoreViewModel = new DealerScoreViewModel();
            return View(DealerScoreViewModel);
        }

        // POST: /DealerScore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DealerScoreScore,DealerScoreDisAjd,DealerScoreLTVCap,DealerScoreMinDisc,DealerScoreCreatedByID,DealerScoreModifiedByID,DealerScoreCreatedDate,DealerScoreModifiedDate")] DealerScoreViewModel DealerScoreViewModel)
        {
            IScore scoreManager = new ScoreManager(db);

            if (ModelState.IsValid)
            {
                //db.DealerScore.Add(dealerscore);
                //await db.SaveChangesAsync();

                DateTime currentTime = DateTime.Now;
                DealerScore DealerScore = new DealerScore();
                DealerScore.CreatedDate = currentTime;
                DealerScore.ModifiedDate = currentTime;
                DealerScore.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                DealerScore.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);

                DealerScore.Score = DealerScoreViewModel.DealerScoreScore;
                DealerScore.DisAjd = DealerScoreViewModel.DealerScoreDisAjd;
                DealerScore.LTVCap = DealerScoreViewModel.DealerScoreLTVCap;
                DealerScore.MinDisc = DealerScoreViewModel.DealerScoreMinDisc;

                await scoreManager.AddDealerScoreAsync(DealerScore);

                return RedirectToAction("Index");
            }

            return View(DealerScoreViewModel);
        }

        // GET: /DealerScore/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DealerScore dealerscore = await db.DealerScore.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            DealerScoreViewModel DealerScoreViewModel = await scoreManager.FindDealerScoreViewModelAsync(id);

            if (DealerScoreViewModel == null)
            {
                return HttpNotFound();
            }
            return View(DealerScoreViewModel);
        }

        // POST: /DealerScore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DealerScoreID,DealerScoreScore,DealerScoreDisAjd,DealerScoreLTVCap,DealerScoreMinDisc,DealerScoreCreatedByID,DealerScoreModifiedByID,DealerScoreCreatedDate,DealerScoreModifiedDate")] DealerScoreViewModel DealerScoreViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                //db.Entry(dealerscore).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                DealerScore DealerScore = new DealerScore();
                DateTime currentTime = DateTime.Now;

                DealerScore.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                DealerScore.ModifiedDate = currentTime;
                DealerScore.CreatedByID = DealerScoreViewModel.DealerScoreCreatedByID;
                DealerScore.CreatedDate = DealerScoreViewModel.DealerScoreCreatedDate;

                DealerScore.ID = DealerScoreViewModel.DealerScoreID;
                DealerScore.Score = DealerScoreViewModel.DealerScoreScore;
                DealerScore.DisAjd = DealerScoreViewModel.DealerScoreDisAjd;
                DealerScore.LTVCap = DealerScoreViewModel.DealerScoreLTVCap;
                DealerScore.MinDisc = DealerScoreViewModel.DealerScoreMinDisc;

                await scoreManager.ChangeDealerScoreAsync(DealerScore);

                return RedirectToAction("Index");
            }
            return View(DealerScoreViewModel);
        }

        // GET: /DealerScore/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DealerScore dealerscore = await db.DealerScore.FindAsync(id);

            IScore scoreManager = new ScoreManager(db);
            DealerScore dealerscore = await scoreManager.FindDealerScoreAsync(id);

            if (dealerscore == null)
            {
                return HttpNotFound();
            }
            return View(dealerscore);
        }

        // POST: /DealerScore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //DealerScore dealerscore = await db.DealerScore.FindAsync(id);
            //db.DealerScore.Remove(dealerscore);
            //await db.SaveChangesAsync();

            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveDealerScoreAsync(id);

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
