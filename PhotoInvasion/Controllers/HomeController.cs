using PhotoInvasion.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoInvasion.Controllers
{
    public class HomeController : Controller
    {
        UserProfileDbContext  _db = new UserProfileDbContext(); 

        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            var model = _db.UserProfiles.Single( u => u.UserName == User.Identity.Name);

            return View(model);
        }

        public ActionResult Users()
        {
            var model = _db.UserProfiles.ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if(_db != null)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }    
    }
}
