using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.DAL
{
    public class UsersAccess
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
            return _entities.UserProfile.ToList();
        }
    }
}
