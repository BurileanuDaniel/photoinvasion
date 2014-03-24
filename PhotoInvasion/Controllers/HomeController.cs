using PhotoInvasion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoInvasion.BLL;
using WebMatrix.WebData;
using PhotoInvasion.Filters;


namespace PhotoInvasion.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        UsersBLL _usersLogic = new UsersBLL();
        AlbumsBLL _albumsLogic = new AlbumsBLL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult ViewProfile(int? id)
        {
            var userId = (id == null) ? (WebSecurity.CurrentUserId) : (id.Value);

            var model = _usersLogic.getUserProfile(userId);

            return View(model);
        }

        public ActionResult Albums(int? id)
        {
            var userId = (id == null) ? (WebSecurity.CurrentUserId) : (id.Value);

            var model = _albumsLogic.getAlbums(userId);

            return View(model);
        }

        public ActionResult Users()
        {
            var model = _usersLogic.getUserProfiles();

            return View(model);
        }
    }
}
