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
//using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web.Helpers;

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

        public ActionResult ViewAlbum(int? id, int? a)
        {
            if (a == null)
            {
                return Content("No album to display.");
            }

            ViewBag.viewerId = (null != id ) ? id.Value : WebSecurity.CurrentUserId;
            ViewBag.albumId = a.Value;
            var model = _albumLogic.getAlbumDetails(a.Value);

            return View(model);
        }

        [HttpGet]
        public ActionResult AddPhoto(int? id, int? a)
        {
            if (a == null)
            {
                return Content("No album selected!");
            }

            var model = new AddPhotoModel
            {
                AlbumId = a.Value,
                CategoryOptions = _categoriesLogic.getCategories()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddPhoto(AddPhotoModel model, int? id, int? a)  //HttpPostedFileBase file
        {

            if (a == null)
            {
                return Content("No album selected!");
            }

            string status = "Upload cu success";
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            container.CreateIfNotExists();
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });


            var file = Request.Files["myfile"];
            string url = "";
            if (file != null)
            {
                Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);

                try
                {
                    Stream stream = new MemoryStream();
                    stream = VaryQualityLevel(file);
                    stream.Seek(0, SeekOrigin.Begin);
                    if (model.WM.Equals("Watermark"))
                    {
                        byte[] buffer = new byte[1048576 * 2];
                        buffer = WaterMark(stream);
                        stream = new MemoryStream(buffer);
                    }
                    blockBlob.UploadFromStream(stream);
                    url = "http://storagetest.blob.core.windows.net" + blockBlob.Uri.AbsolutePath;
                }
                catch (Exception e)
                {                  
                    return Content("Message: " + e.Message + " Trace: " + e.StackTrace.ToString() + " Data: " + e.Data.ToString());
                }
            }
            
            if (ModelState.IsValid)
            {
                _photosLogic.addPhoto(
                    new PhotoInvasion.DAL.Photo
                    {
                        AlbumId = a.Value,
                        UserId = WebSecurity.CurrentUserId,
                        //Source = model.Source,
                        Source = url,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        Views = 0,
                        Date = DateTime.Now
                    });
            }

            return RedirectToAction("ViewAlbum", "Album", new {id = WebSecurity.CurrentUserId, a = a.Value });
        }


        public ActionResult ViewPhoto(int? id, int? p, string returnUrl)
        {
            if (null == id)
            {
                ViewBag.viewer = WebSecurity.CurrentUserId;
            }
            else
            {
                ViewBag.viewer = id.Value;
            }

            if (p == null)
            {
                return Content("No photo selected!");
            }
            else
            {
                int rating = _ratingsLogic.getRating(p.Value, WebSecurity.CurrentUserId);
                _photosLogic.viewPhoto(p.Value);
                var photo = _photosLogic.getPhoto(p.Value);
                var model = new PhotoViewModel
                {
                    photo = photo,
                    returnUrl = returnUrl,
                    rating = rating
                };


                return View(model);
            }
        }

        public ActionResult DeletePhoto(int? id, int? p, int? AlbumId)
        {
            if (p == null)
            {
                return Content("No photo selected");
            }

            _photosLogic.deletePhoto(p.Value);
            return RedirectToAction("ViewAlbum", "Album", new { id = WebSecurity.CurrentUserId, a = AlbumId.Value });
        }

        public ActionResult RatePhoto(int id, int p, int rating, string returnUrl)
        {
            _ratingsLogic.addRating(new PhotoInvasion.DAL.Rating
            {
                PhotoId = p,
                Rating1 = rating,
                Seen = 0,
                UserId = WebSecurity.CurrentUserId
            });

            return RedirectToAction("ViewPhoto", "Album", new { id = id, p = p, returnUrl = returnUrl });
        }

        public ActionResult DeleteRating(int id, string returnUrl)
        {
            _ratingsLogic.deleteRating(id, WebSecurity.CurrentUserId);

            return RedirectToAction("ViewPhoto", "Album", new { id = id, returnUrl = returnUrl });
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private MemoryStream VaryQualityLevel(HttpPostedFileBase file)
        {
            // Get a bitmap.
            file = Request.Files["myfile"];
            Stream s = Request.Files["myfile"].InputStream;
            System.Drawing.Image img = System.Drawing.Image.FromStream(s);
            Bitmap bmp1 = (Bitmap)img;
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID 
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            // An EncoderParameters object has an array of EncoderParameter 
            // objects. In this case, there is only one 
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            MemoryStream ms = new MemoryStream();
            bmp1.Save(ms, jgpEncoder, myEncoderParameters);

            return ms;

        }
        private Byte[] WaterMark(Stream s)
        {
            MemoryStream str = new MemoryStream();
            System.Web.Helpers.WebImage webimg = new System.Web.Helpers.WebImage(s);
            String wm = WebSecurity.CurrentUserName;
            webimg.AddTextWatermark(wm, "White", 16, "Regular", "Microsoft Sans Serif", "Right", "Bottom", 50, 10);

            return webimg.GetBytes();
        }
    }
}
