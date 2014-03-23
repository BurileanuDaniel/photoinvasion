using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoInvasion.DAL;

namespace PhotoInvasion.BLL
{
    public class UsersLogic
    {
        PhotoInvasion.DAL.UsersAccess _access = new PhotoInvasion.DAL.UsersAccess();

        public UserProfile getUserProfile(int userId)
        {
            return _access.getUserProfile(userId);
        }

        public List<UserProfile> getUserProfiles()
        {
            return _access.getUserProfiles();
        }

    }
}
