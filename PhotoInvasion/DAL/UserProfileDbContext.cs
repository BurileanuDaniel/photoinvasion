using PhotoInvasion.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace PhotoInvasion.DAL
{
    public class UserProfileDbContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public UserProfileDbContext() : base("name=DefaultConnection")
        {

        }

    }
}