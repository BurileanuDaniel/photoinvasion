using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoInvasion.Models
{
    public class AddPhotoModel
    {
        [Required]
        [Display(Name = "Category")]
        public int  CategoryId { get; set; }

        //[Required]
        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Watemark")]
        public string WM { get; set; }

        public IEnumerable<PhotoInvasion.DAL.Category> CategoryOptions;
    }

    public class AddAlbumModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

}