using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoInvasion.Models
{
    public class PhotoViewModel
    {
        public string returnUrl { get; set; }
        public PhotoInvasion.DAL.Photo photo { get; set; }
        public int rating { get; set; }

        public IEnumerable<PhotoInvasion.DAL.Category> CategoryOptions;
    }
}