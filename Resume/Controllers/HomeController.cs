//using Resume.Untity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resume.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String name, String email, String phone, String message)
        {
            var Body =
                "姓　　名： " + name + "<br/>" +
                "電子郵件： " + email + "<br/>" +
                "連絡電話： " + phone + "<br/>" +                
                
                "您的意見： " + message + "<br/>";
            //GMailer.Send(email, Body);

            return RedirectToAction("Index");
        }

        public FileResult Download()
        {
            //return File("~\\file\\android-debug.apk", "application / vnd.android.package - archive", "resume.apk");
            return File("~\\file\\android.apk", "application / vnd.android.package - archive", "resume.apk");
        }
    }
}