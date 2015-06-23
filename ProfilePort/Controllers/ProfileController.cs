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

namespace ProfilePort.Controllers
{

    public class ProfileController : ApiController
    {IProfiles profileAdapter;
        ApplicationDbContext db = new ApplicationDbContext();

        public ProfileController()
        {
            profileAdapter = new Profiles();

        }
     
        // GET: api/Profile/5
        [ResponseType(typeof(Profile))]
        public IHttpActionResult GetProfile(int id)
        {
            ProfileVM myProfile = new ProfileVM();
            myProfile = profileAdapter.GetProfile(id);
            if (myProfile == null)
            {
                return NotFound();
            }

            return Ok(myProfile);
        }

      
        public IHttpActionResult PutProfile(int id, ProfileVM newTalent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Profile myProfile = new Profile();
                myProfile = profileAdapter.UpdateProfile(id, newTalent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Profile
        [ResponseType(typeof(Profile))]
        public IHttpActionResult PostProfile(ProfileVM profile)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            Profile myProfile = new Profile();
            myProfile = profileAdapter.AddProfile("Zango",profile);

            return CreatedAtRoute("DefaultApi", new { id = profile.ProfileId }, profile);
        }

        // DELETE: api/Profile/5
        [ResponseType(typeof(Profile))]
        public IHttpActionResult DeleteProfile(int id)
        {

            try
            {
                profileAdapter.DeleteProfile(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        private bool ProfileExists(int id)
        {
            return db.Profiles.Count(e => e.ProfileId == id) > 0;
        }
    }
}