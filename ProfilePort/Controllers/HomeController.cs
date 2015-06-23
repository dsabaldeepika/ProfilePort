using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProfilePort.Data;
using ProfilePort.DataModel;
using ProfilePort.Adapters.Interfaces;
using ProfilePort.Adapters.DataAdapters;
using DashboardPort.Adapters.Interfaces;

namespace DashboardPort.Controllers
{
   
    public class HomeController : ApiController
    {
        IDashboard newDashboard;
        ApplicationDbContext db = new ApplicationDbContext();

        public HomeController()
        {
            newDashboard = new ProfilePort.Adapters.DataAdapters.Dashboard();

        }

        // GET: api/Dashboard/5
        [ResponseType(typeof(ProfilePort.DataModel.Dashboard))]
        public IHttpActionResult GetDashboard(string id)
        {
            User myDashboard = new User();
            myDashboard = newDashboard.GetDashboard(id);
         
            if (myDashboard == null)
            {
                return NotFound();
            }

            return Ok(myDashboard);
        }


        public IHttpActionResult PutDashboard(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                var myDashboard = newDashboard.UpdateDashboard(id, user);
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Dashboard
        [ResponseType(typeof(ProfilePort.DataModel.Dashboard))]
        public IHttpActionResult PostDashboard(string UserId, User Dashboard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           User myDashboard = new User();
           myDashboard = newDashboard.AddDashboard(UserId, Dashboard);

           return Ok(Dashboard);
        }

        // DELETE: api/Dashboard/5
        [ResponseType(typeof(ProfilePort.DataModel.Dashboard))]
        public IHttpActionResult DeleteDashboard(string id)
        {

            try
            {
                newDashboard.DeleteDashboard(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DashboardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DashboardExists(string id)
        {
            return db.Dashboards.Count(e => e.Id == id) > 0;
        }
    }
}