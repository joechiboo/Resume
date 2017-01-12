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
            if (_isExist(information.hash))
            {
                var info = db.Infomations.Where(p => p.hash == information.hash).FirstOrDefault();
                
                    info.accept = information.accept;
                    info.side = information.side;
                    info.number = information.number;
                    info.children = information.children;
                    info.vegetable = information.vegetable;
                    info.address = information.address;
                    info.bus = information.bus;
                    info.HSR = information.HSR;
                    info.write = information.write;
            }
            else {
                db.Infomations.Add(information);
            }
            db.SaveChanges();

            return Ok();
        }

        private bool _isExist(Guid memberhash)
        {
            Information information = db.Infomations.Where(p => p.hash == memberhash).FirstOrDefault();

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