using Resume.DAL;
using Resume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Service
{
    public class DeviceService
    {
        private MarriedContext db = new MarriedContext();

        public void AddDevice(Device entity) {
            var model = db.Devices.Where(p => p.sessionid == entity.sessionid).FirstOrDefault();

            if (model != null)
            {

            }
            else {
                db.Devices.Add(entity);
            }

            db.SaveChanges();
        }
    }
}