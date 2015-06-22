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

namespace ProfilePort.Controllers
{
    public class EducationsController : ApiController
    {
        private  ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Educations
        public IQueryable<Education> GetEducations()
        {
            return db.Educations;
        }

        // GET: api/Educations/5
        [ResponseType(typeof(Education))]
        public IHttpActionResult GetEducation(int id)
        {
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return NotFound();
            }

            return Ok(education);
        }

        // PUT: api/Educations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEducation(int id, Education education)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != education.EducationId)
            {
                return BadRequest();
            }

            db.Entry(education).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(id))
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

        // POST: api/Educations
        [ResponseType(typeof(Education))]
        public IHttpActionResult PostEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Educations.Add(education);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = education.EducationId }, education);
        }

        // DELETE: api/Educations/5
        [ResponseType(typeof(Education))]
        public IHttpActionResult DeleteEducation(int id)
        {
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return NotFound();
            }

            db.Educations.Remove(education);
            db.SaveChanges();

            return Ok(education);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EducationExists(int id)
        {
            return db.Educations.Count(e => e.EducationId == id) > 0;
        }
    }
}