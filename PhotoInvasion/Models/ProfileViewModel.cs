using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoInvasion.Models
{
    public class ProfileViewModel
    {
        public PhotoInvasion.DAL.UserProfile userProfile { get; set; }
        public List<PhotoInvasion.DAL.Photo> recentActivity { get; set; }
    }
}