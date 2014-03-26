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
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        UsersBLL _usersLogic = new UsersBLL();
        AlbumsBLL _albumsLogic = new AlbumsBLL();
        PhotosBLL _photosLogic = new PhotosBLL();

        public ActionResult Index()
        {
            HomePagePhotos result = _photosLogic.getHomePhotos();

            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult ViewProfile(int? id)
        {
            var userId = (id == null) ? (WebSecurity.CurrentUserId) : (id.Value);

            var model = new ProfileViewModel
            {
                userProfile = _usersLogic.getUserProfile(userId),
                recentActivity = _photosLogic.getRecentActivity(userId)
            };

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
