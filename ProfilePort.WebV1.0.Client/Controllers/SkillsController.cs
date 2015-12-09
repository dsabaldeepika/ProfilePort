﻿//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using ProfilePort.Data;
//using ProfilePort.DataModel;

//namespace ProfilePort.Controllers
//{
//    public class SkillsController : ApiController
//    {
//        private  ApplicationDbContext db = new ApplicationDbContext();

//        // GET: api/Skills
//        public IQueryable<Skill> GetSkills()
//        {
//            return db.Skills;
//        }

//        // GET: api/Skills/5
//        [ResponseType(typeof(Skill))]
//        public IHttpActionResult GetSkill(int id)
//        {
//            Skill skill = db.Skills.Find(id);
//            if (skill == null)
//            {
//                return NotFound();
//            }

//            return Ok(skill);
//        }

//        // PUT: api/Skills/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutSkill(int id, Skill skill)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != skill.SkillId)
//            {
//                return BadRequest();
//            }

//            db.Entry(skill).State = EntityState.Modified;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!SkillExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Skills
//        [ResponseType(typeof(Skill))]
//        public IHttpActionResult PostSkill(Skill skill)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.Skills.Add(skill);
//            db.SaveChanges();

//            return CreatedAtRoute("DefaultApi", new { id = skill.SkillId }, skill);
//        }

//        // DELETE: api/Skills/5
//        [ResponseType(typeof(Skill))]
//        public IHttpActionResult DeleteSkill(int id)
//        {
//            Skill skill = db.Skills.Find(id);
//            if (skill == null)
//            {
//                return NotFound();
//            }

//            db.Skills.Remove(skill);
//            db.SaveChanges();

//            return Ok(skill);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool SkillExists(int id)
//        {
//            return db.Skills.Count(e => e.SkillId == id) > 0;
//        }
//    }
//}