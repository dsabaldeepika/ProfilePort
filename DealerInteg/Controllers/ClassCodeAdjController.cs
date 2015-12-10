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
    public class ClassCodeAdjController : Controller
    {
        string connectionString;
        ConnectionStringProperty connectionStringProperty;        
        private ScoringEngineEntities db;

        public ClassCodeAdjController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
        }

        // GET: /ClassCodeAdj/
        public async Task<ActionResult> Index()
        {
            //return View(await db.ClassCodeAdj.ToListAsync());
            IScore scoreManager = new ScoreManager(db);
            return View(await scoreManager.GetAllClassCodeAdjViewModelAsync());
        }

        // GET: /ClassCodeAdj/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ClassCodeAdj classcodeadj = await db.ClassCodeAdj.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            ClassCodeAdjViewModel ClassCodeAdjViewModel = await scoreManager.FindClassCodeAdjViewModelAsync(id);
            if (ClassCodeAdjViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ClassCodeAdjViewModel);
        }

        // GET: /ClassCodeAdj/Create
        public ActionResult Create()
        {
            //return View();
            ClassCodeAdjViewModel ClassCodeAdjViewModel = new ClassCodeAdjViewModel();
            IScore scoreManager = new ScoreManager(db);
            ClassCodeAdjViewModel.liVehicleClassType = new List<VehicleClassType>();
            ClassCodeAdjViewModel.liVehicleClassType = scoreManager.GetAllVehicleClassType();
            return View(ClassCodeAdjViewModel);
        }

        // POST: /ClassCodeAdj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClassCodeAdjLTVCap,ClassCodeAdjLTVAdj,ClassCodeAdjDiscountAdj,ClassCodeAdjFinancedCap,ClassCodeAdjCreatedByID,ClassCodeAdjModifiedByID,ClassCodeAdjCreatedDate,ClassCodeAdjModifiedDate,VehicleClassTypeID")] ClassCodeAdjViewModel ClassCodeAdjViewModel)
        {
            IScore scoreManager = new ScoreManager(db);
            if (ModelState.IsValid)
            {
                //db.ClassCodeAdj.Add(classcodeadj);
                //await db.SaveChangesAsync();
                ClassCodeAdj ClassCodeAdj = new ClassCodeAdj();
                ClassCodeAdj.LTVAdj = ClassCodeAdjViewModel.ClassCodeAdjLTVAdj;
                ClassCodeAdj.LTVCap = ClassCodeAdjViewModel.ClassCodeAdjLTVCap;
                ClassCodeAdj.DiscountAdj = ClassCodeAdjViewModel.ClassCodeAdjDiscountAdj;
                ClassCodeAdj.FinancedCap = ClassCodeAdjViewModel.ClassCodeAdjFinancedCap;
                ClassCodeAdj.CreatedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                ClassCodeAdj.CreatedDate = DateTime.Now;
                ClassCodeAdj.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                ClassCodeAdj.ModifiedDate = DateTime.Now;

                ClassCodeAdj.VehicleClassType = await scoreManager.FindVehicleClassTypeAsync(ClassCodeAdjViewModel.VehicleClassTypeID);

                await scoreManager.AddClassCodeAdjAsync(ClassCodeAdj);
                return RedirectToAction("Index");
            }

            ClassCodeAdjViewModel adjVM = new ClassCodeAdjViewModel();
            adjVM.liVehicleClassType = new List<VehicleClassType>();
            adjVM.liVehicleClassType = scoreManager.GetAllVehicleClassType();
            return View(adjVM);

            //return View(ClassCodeAdj);
        }

        // GET: /ClassCodeAdj/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ClassCodeAdj classcodeadj = await db.ClassCodeAdj.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            ClassCodeAdjViewModel ClassCodeAdjViewModel = await scoreManager.FindClassCodeAdjViewModelAsync(id);
            ClassCodeAdjViewModel.liVehicleClassType = new List<VehicleClassType>();
            ClassCodeAdjViewModel.liVehicleClassType = scoreManager.GetAllVehicleClassType();
            if (ClassCodeAdjViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ClassCodeAdjViewModel);
        }

        // POST: /ClassCodeAdj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClassCodeAdjID,ClassCodeAdjLTVCap,ClassCodeAdjLTVAdj,ClassCodeAdjDiscountAdj,ClassCodeAdjFinancedCap,ClassCodeAdjCreatedByID,ClassCodeAdjModifiedByID,ClassCodeAdjCreatedDate,ClassCodeAdjModifiedDate,VehicleClassTypeID")] ClassCodeAdjViewModel ClassCodeAdjViewModel)
        {
            IScore scoreManager = new ScoreManager(db);

            if (ModelState.IsValid)
            {
                //db.Entry(classcodeadj).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                ClassCodeAdj ClassCodeAdj = new ClassCodeAdj();
                ClassCodeAdj.ID = ClassCodeAdjViewModel.ClassCodeAdjID;
                ClassCodeAdj.LTVAdj = ClassCodeAdjViewModel.ClassCodeAdjLTVAdj;
                ClassCodeAdj.LTVCap = ClassCodeAdjViewModel.ClassCodeAdjLTVCap;
                ClassCodeAdj.DiscountAdj = ClassCodeAdjViewModel.ClassCodeAdjDiscountAdj;
                ClassCodeAdj.FinancedCap = ClassCodeAdjViewModel.ClassCodeAdjFinancedCap;
                ClassCodeAdj.ModifiedByID = scoreManager.GetUserID(HttpContext.User.Identity.Name);
                ClassCodeAdj.ModifiedDate = DateTime.Now;
                ClassCodeAdj.CreatedDate = ClassCodeAdjViewModel.ClassCodeAdjCreatedDate;
                ClassCodeAdj.CreatedByID = ClassCodeAdjViewModel.ClassCodeAdjCreatedByID;

                ClassCodeAdj.VehicleClassType = await scoreManager.FindVehicleClassTypeAsync(ClassCodeAdjViewModel.VehicleClassTypeID);

                await scoreManager.ChangeClassCodeAdjAsync(ClassCodeAdj);


                return RedirectToAction("Index");
            }
            ClassCodeAdjViewModel adjVM = new ClassCodeAdjViewModel();
            adjVM.liVehicleClassType = new List<VehicleClassType>();
            adjVM.liVehicleClassType = scoreManager.GetAllVehicleClassType();
            return View(adjVM);
           // return View(classcodeadj);
        }

        // GET: /ClassCodeAdj/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ClassCodeAdj classcodeadj = await db.ClassCodeAdj.FindAsync(id);
            IScore scoreManager = new ScoreManager(db);
            ClassCodeAdj ClassCodeAdj = await scoreManager.FindClassCodeAdjAsync(id);

            if (ClassCodeAdj == null)
            {
                return HttpNotFound();
            }
            return View(ClassCodeAdj);
        }

        // POST: /ClassCodeAdj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //ClassCodeAdj classcodeadj = await db.ClassCodeAdj.FindAsync(id);
            //db.ClassCodeAdj.Remove(classcodeadj);
            //await db.SaveChangesAsync();

            IScore scoreManager = new ScoreManager(db);
            await scoreManager.RemoveClassCodeAdjAsync(id);

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
