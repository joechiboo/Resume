using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resume.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">GuestId</param>
        /// <returns></returns>
        /// /Guest/Seat/VckvXlAvsB
        public ActionResult Seat(String Id) {
            ViewBag.Id = Id;

            return View();
        }

        public ActionResult Chat() {
            return View();
        }

    }
}