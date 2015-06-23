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
    public class ContactInfosController : ApiController
    {
        private  ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ContactInfos
        public IQueryable<ContactInfo> GetContactInfos()
        {
            return db.ContactInfos;
        }

        // GET: api/ContactInfos/5
        [ResponseType(typeof(ContactInfo))]
        public IHttpActionResult GetContactInfo(int id)
        {
            ContactInfo contactInfo = db.ContactInfos.Find(id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return Ok(contactInfo);
        }

        // PUT: api/ContactInfos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactInfo(int id, ContactInfo contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactInfo.Id)
            {
                return BadRequest();
            }

            db.Entry(contactInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactInfoExists(id))
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

        // POST: api/ContactInfos
        [ResponseType(typeof(ContactInfo))]
        public IHttpActionResult PostContactInfo(ContactInfo contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactInfos.Add(contactInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactInfo.Id }, contactInfo);
        }

        // DELETE: api/ContactInfos/5
        [ResponseType(typeof(ContactInfo))]
        public IHttpActionResult DeleteContactInfo(int id)
        {
            ContactInfo contactInfo = db.ContactInfos.Find(id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            db.ContactInfos.Remove(contactInfo);
            db.SaveChanges();

            return Ok(contactInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactInfoExists(int id)
        {
            return db.ContactInfos.Count(e => e.Id == id) > 0;
        }
    }
}