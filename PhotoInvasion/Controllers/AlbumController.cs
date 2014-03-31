using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoInvasion.Models;
using WebMatrix.WebData;
using PhotoInvasion.Filters;
using PhotoInvasion.BLL;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PhotoInvasion.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AlbumController : Controller
    {
        AlbumsBLL _albumLogic = new AlbumsBLL();
        CategoriesBLL _categoriesLogic = new CategoriesBLL();
        PhotosBLL _photosLogic = new PhotosBLL();
        RatingsBLL _ratingsLogic = new RatingsBLL();

        private CloudStorageAccount storageAccount =
           CloudStorageAccount.Parse(
               "DefaultEndpointsProtocol=http;AccountName=storagetest;AccountKey=wLExFPdTfbZ4KTqB890l+gzIKNMTww6PeKpXhJ8LPVR7pwdgjft0Z1KaO5wjdqmtzS5R7Nrs4G1sxWqgkPGcFQ==;");


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
            //if (User.Identity.IsAuthenticated == false)
            //{
            //    return Content("User must be authenitcated");
            //}

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

            string status = "Upload cu success";
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            container.CreateIfNotExists();

            var file = Request.Files["myfile"];
            string url = "";
            if (file != null)
            {
                Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);

                try
                {
                    blockBlob.UploadFromStream(file.InputStream);
                    url = "http://storagetest.blob.core.windows.net" + blockBlob.Uri.AbsolutePath;
                }
                catch (Exception)
                {
                    status = "Upload nereusit";
                }
            }



            if (ModelState.IsValid)
            {
                _photosLogic.addPhoto(
                    new PhotoInvasion.DAL.Photo
                    {
                        AlbumId = id.Value,
                        UserId = WebSecurity.CurrentUserId,
                        //Source = model.Source,
                        Source = url,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        Views = 0,
                        Date = DateTime.Now
                    });
            }

            return RedirectToAction("ViewAlbum", "Album", new { id = id.Value });
        }

        public ActionResult ViewPhoto(int? id, string returnUrl)
        {
            if (id == null)
            {
                return Content("No photo selected!");
            }
            else
            {
                int rating = _ratingsLogic.getRating(id.Value, WebSecurity.CurrentUserId);
                _photosLogic.viewPhoto(id.Value);
                var photo = _photosLogic.getPhoto(id.Value);
                var model = new PhotoViewModel
                {
                    photo = photo,
                    returnUrl = returnUrl,
                    rating = rating
                };


                return View(model);
            }
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

        public ActionResult RatePhoto(int id, int rating, string returnUrl)
        {
            _ratingsLogic.addRating(new PhotoInvasion.DAL.Rating
            {
                PhotoId = id,
                Rating1 = rating,
                Seen = 0,
                UserId = WebSecurity.CurrentUserId
            });

            return RedirectToAction("ViewPhoto", "Album", new {id = id, returnUrl = returnUrl});
        }

        public ActionResult DeleteRating(int id, string returnUrl)
        {
            _ratingsLogic.deleteRating(id, WebSecurity.CurrentUserId);

            return RedirectToAction("ViewPhoto", "Album", new { id = id, returnUrl = returnUrl });
        }
    }
}
