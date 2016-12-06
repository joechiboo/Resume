using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Resume.DAL;
using Resume.Models;
using System.Linq;
using System;

namespace Resume.Controllers.api
{
    public class InformationController : ApiController
    {
        private MarriedContext db = new MarriedContext();

        // GET: api/Information/5
        [ResponseType(typeof(Information))]
        public IHttpActionResult GetInformation(Guid id)
        {
            Information information = db.Infomations.Find(id);
            if (information == null)
            {
                return Ok("");
            }

            return Ok(information);
        }

        // POST: api/Information
        [ResponseType(typeof(Information))]
        public IHttpActionResult PostInformation(Information information)
        {
            if (_isExist(information.memberid))
            {
                // 有問題
                db.Entry(information).State = EntityState.Modified;
            }
            else {
                db.Infomations.Add(information);
            }
            db.SaveChanges();

            return Ok();
        }

        private bool _isExist(int memberid)
        {
            Information information = db.Infomations.Where(p => p.memberid == memberid).FirstOrDefault();

            return information != null;
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