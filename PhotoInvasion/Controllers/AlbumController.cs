using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoInvasion.BLL;
using PhotoInvasion.Models;
using WebMatrix.WebData;
using PhotoInvasion.Filters;

namespace PhotoInvasion.Controllers
{
    [InitializeSimpleMembership]
    public class AlbumController : Controller
    {
        AlbumsBLL _albumLogic = new AlbumsBLL();
        CategoriesBLL _categoriesLogic = new CategoriesBLL();
        PhotosBLL _photosLogic = new PhotosBLL();
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
        [HttpGet]
        public ActionResult AddPhoto(int? id)
        {
            if (id == null)
            {
                return Content("No album selected!");
            }

            var model = new AddPhotoModel
            {
                CategoryOptions = _categoriesLogic.getCategories()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddPhoto(AddPhotoModel model, int? id)
        {
            if (id == null)
            {
                return Content("No album selected!");
            }

            if (ModelState.IsValid)
            {
                _photosLogic.addPhoto(
                    new PhotoInvasion.DAL.Photo
                    {
                        AlbumId = id.Value,
                        UserId = WebSecurity.CurrentUserId,
                        Source = model.Source,
                        Description = model.Description,
                        CategoryId = model.CategoryId
                    });
            }

            return RedirectToAction("ViewAlbum", "Album", new { id = id.Value });
        }

        /*
         *  @param: id - Id of the deleted photo. 
         * 
         * */
        public ActionResult DeletePhoto(int? id, int? AlbumId)
        {
            if (id == null)
            {
                return Content("No photo selected");
            }

            _photosLogic.deletePhoto(id.Value);
            return RedirectToAction("ViewAlbum", "Album", new { id = AlbumId.Value});
        }
    }
}
