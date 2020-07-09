using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DUT4UStudentApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "More about the app.";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Group 03 : DUT4U";

            return View();
        }
    }
}