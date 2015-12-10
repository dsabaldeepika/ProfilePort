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
    public class BuyRateController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public BuyRateController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        } 

        // GET: /BuyRate/
        public async Task<ActionResult> Index()
        {
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllBuyRateAsync());
            }

        // GET: /BuyRate/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            BuyRate buyrate = await scoreManager.FindBuyRateAsync(id);


            if (buyrate == null)
            {
                return HttpNotFound();
            }
            return View(buyrate);
        }

        // GET: /BuyRate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BuyRate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,MinBV,MaxBV,MinFico,MaxFico,BuyRateValue,IsActive")] BuyRate buyrate)
        {
            if (ModelState.IsValid)
            {
                IScore scoreManager = new ScoreManager(db);
                DateTime currentTime = DateTime.Now;
                buyrate.CreatedDate = currentTime;
                buyrate.ModifiedDate = currentTime;
                buyrate.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);     // set to 1 for now until db is ready
                buyrate.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);   // set to 1 for now until db is ready
                
                await scoreManager.AddBuyRateAsync(buyrate);

                return RedirectToAction("Index");
            }

            return View(buyrate);
        }

        // GET: /BuyRate/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            BuyRate buyrate = await scoreManager.FindBuyRateAsync(id);

            if (buyrate == null)
            {
                return HttpNotFound();
            }
            return View(buyrate);
        }

        // POST: /BuyRate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,MinBV,MaxBV,MinFico,MaxFico,BuyRateValue,CreatedDate,CreatedByID,IsActive")] BuyRate buyrate)
        {

            if (ModelState.IsValid)
            {
                DateTime currentTime = DateTime.Now;
                buyrate.ModifiedDate = currentTime;
                IScore scoreManager = new ScoreManager(db);
                buyrate.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);            // set to 1 for now until db is ready
                await scoreManager.ChangeBuyRateAsync(buyrate);
                return RedirectToAction("Index");
            }
            return View(buyrate);
        }

        // GET: /BuyRate/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IScore scoreManager = new ScoreManager(db);
            BuyRate buyrate = await scoreManager.FindBuyRateAsync(id);

            if (buyrate == null)
            {
                return HttpNotFound();
            }
            return View(buyrate);
        }

        // POST: /BuyRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveBuyRateAsync(id);
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
