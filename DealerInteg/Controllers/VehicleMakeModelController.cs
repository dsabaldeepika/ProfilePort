using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Com.CoreLane.Common.Models;
using Com.CoreLane.ConfigDB.Models.ConnectionTools;
using Com.CoreLane.ScoringEngine.Models;
using DealerPortalCRM.DAService;
using DealerPortalCRM.DAServiceImpl;
using DealerPortalCRM.ViewModels;

namespace DealerPortalCRM.Controllers
{
    [SuppressMessage("ReSharper", "RedundantCatchClause")]
    public class VehicleMakeModelController : ApiController
    {

           // private ScoringEngineEntities db = new ScoringEngineEntities();
        private readonly string connectionString;
        private readonly ConnectionStringProperty connectionStringProperty;
        private readonly ScoringEngineEntities db;
        private readonly ScoreManager scoreManager;
        public VehicleMakeModelController()
        {
            connectionStringProperty = new ConnectionStringProperty();
            connectionString = connectionStringProperty.GetConnection(ConnectionStringTypeEnum.ScoringEngine);
            db = new ScoringEngineEntities(connectionString);
            scoreManager = new ScoreManager(db);
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> GetAllVehicleMakeModelClassViewModelAsync()
        {
            var vehiclemakemodel = await scoreManager.GetAllVehicleMakeModelClassViewModelAsync();

            if (vehiclemakemodel== null)
            {
                return this.NotFound();
            }

            return this.Ok(vehiclemakemodel);
            
           
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> FindVehicleMakeModelClassViewModelAsync(int id)
        {
            var vehiclemakemodel = await scoreManager.FindVehicleMakeModelClassViewModelAsync(id);
            

            if (vehiclemakemodel == null)
            {
                return this.NotFound();
            }

            return this.Ok(vehiclemakemodel);
           
        }


        //// GET api/<controller>/5
        public List<VehicleMakeType> GetAllActiveVehicleMakeType(int id)
        {
             return scoreManager.GetAllActiveVehicleMakeType();
        }


        public List<VehicleModelType> GetVehicleModelTypeByVehicleMakeTypeID(int id)
        {
            return scoreManager.GetVehicleModelTypeByVehicleMakeTypeID(id);
        }


        public List<VehicleClassType> GetAllActiveVehicleClassType()
        {
            return scoreManager.GetAllActiveVehicleClassType();
        }




        [ResponseType(typeof(VehicleMakeModelClassViewModel))]
        public async Task<IHttpActionResult> Post([FromBody]VehicleMakeModelClassViewModel vehiclemakemodelclassviewmodel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }


            var foundExisting = scoreManager.FindVehicleMakeModelAsync(vehiclemakemodelclassviewmodel.VehicleMakeTypeID,
                vehiclemakemodelclassviewmodel.VehicleModelTypeID, vehiclemakemodelclassviewmodel.VehicleClassTypeID);

            if (!foundExisting)
            {
                var vehicleMakeModelClass = new VehicleMakeModelClass();

                vehicleMakeModelClass.VehicleMakeType_ID = vehiclemakemodelclassviewmodel.VehicleMakeTypeID;
                vehicleMakeModelClass.VehicleModelType_ID = vehiclemakemodelclassviewmodel.VehicleMakeTypeID;
                vehicleMakeModelClass.VehicleClassType_ID = vehiclemakemodelclassviewmodel.VehicleClassTypeID;

                vehicleMakeModelClass.CreatedByID =  scoreManager.GetUserID(HttpContext.Current.User.Identity.Name);
                vehicleMakeModelClass.ModifiedByID = scoreManager.GetUserID(HttpContext.Current.User.Identity.Name);
                vehicleMakeModelClass.CreatedDate = DateTime.Now;
                vehicleMakeModelClass.ModifiedDate = DateTime.Now;
                await scoreManager.AddVehicleMakeModelClassAsync(vehicleMakeModelClass);
            }

            return this.Ok(vehiclemakemodelclassviewmodel);
        }

        [ResponseType(typeof(VehicleMakeModelClassViewModel))]
        public async Task<IHttpActionResult> Put([FromBody]VehicleMakeModelClassViewModel vehiclemakemodelclassviewmodel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var foundExisting = scoreManager.FindVehicleMakeModelAsync(vehiclemakemodelclassviewmodel.VehicleMakeTypeID,
                vehiclemakemodelclassviewmodel.VehicleModelTypeID, vehiclemakemodelclassviewmodel.VehicleClassTypeID);

            if (!foundExisting)
            {
                var vehicleMakeModelClass = new VehicleMakeModelClass();
                vehicleMakeModelClass.ID = vehiclemakemodelclassviewmodel.VehicleMakeModelClassID;
                vehicleMakeModelClass.ModifiedByID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                vehicleMakeModelClass.ModifiedDate = DateTime.Now;
                vehicleMakeModelClass.CreatedDate = vehiclemakemodelclassviewmodel.VehicleMakeModelClassCreatedDate;
                vehicleMakeModelClass.CreatedByID = vehiclemakemodelclassviewmodel.VehicleMakeModelClassCreatedByID;
                vehicleMakeModelClass.VehicleMakeType_ID = vehiclemakemodelclassviewmodel.VehicleMakeTypeID;
                vehicleMakeModelClass.VehicleModelType_ID = vehiclemakemodelclassviewmodel.VehicleMakeTypeID;
                vehicleMakeModelClass.VehicleClassType_ID = vehiclemakemodelclassviewmodel.VehicleClassTypeID;

                await scoreManager.ChangeVehicleMakeModelClassAsync(vehicleMakeModelClass);
            }

            return this.Ok();
        }


        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.BadRequest(this.ModelState);
               
            }
            var vehicleMakeModelClass = await scoreManager.FindVehicleMakeModelClassAsync(id);
            if (vehicleMakeModelClass == null)
            {
                return this.NotFound();
                
            }
            await scoreManager.RemoveVehicleMakeModelClassAsync(id);
          
            return this.Ok();   
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}