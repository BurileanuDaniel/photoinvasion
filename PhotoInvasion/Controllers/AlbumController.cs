using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoInvasion.BLL;


namespace PhotoInvasion.Controllers
{
    public class AlbumController : Controller
    {
        AlbumsLogic _albumLogic = new AlbumsLogic();

        //
        // GET: /Album/

        public ActionResult Index()
        {
            return View();
        }

        /*
         * @param: id - Id of the selected album.
         * 
         * */
        public ActionResult ViewAlbum(int? id)
        {
            if (id == null)
            {
                return Content("No album to display.");
            }

            var model = _albumLogic.getAlbumDetails(id.Value);

            return View(model);
        }

        /*
         * @param: id - Id of the album that will contain the added photo.
         * 
         * */
        public ActionResult AddPhoto(int? id)
        {
            return View();
        }

        /*
         *  @param: id - Id of the deleted photo. 
         * 
         * */
        public ActionResult DeletePhoto(int? id)
        {
            return View();
        }
    }
}
