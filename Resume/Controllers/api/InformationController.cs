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
using Resume.DAL;
using Resume.Models;

namespace Resume.Controllers.api
{
    public class InformationController : ApiController
    {
        private MarriedContext db = new MarriedContext();
        
        // GET: api/Information/5
        [ResponseType(typeof(Information))]
        public IHttpActionResult GetInformation(int id)
        {
            Information information = db.Infomations.Find(id);
            if (information == null)
            {
                return NotFound();
            }

            return Ok(information);
        }

        // PUT: api/Information/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInformation(int id, Information information)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Entry(information).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Information
        [ResponseType(typeof(Information))]
        public IHttpActionResult PostInformation(Information information)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Infomations.Add(information);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = information.hash }, information);
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