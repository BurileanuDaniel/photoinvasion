using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.DAL
{
    public class UsersDAL
    {
        DatabaseEntities _entities = new DatabaseEntities();

        /* Get the profile of the user given as a parameter. */
        public UserProfile getUserProfile(int userId)
        {
            return _entities.UserProfile
                        .Where(u => u.UserId == userId)
                        .SingleOrDefault();
        }

        public List<UserProfile> getUserProfiles()
        {
            var result = from user in _entities.UserProfile 
                         join roles in _entities.webpages_UsersInRoles
                            on user.UserId equals roles.UserId
                         where roles.RoleId == 1
                         select user;

            return result.ToList();
            //return _entities.UserProfile.ToList();
        }
    }
}
