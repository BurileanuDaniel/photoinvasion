using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoInvasion.DAL;

namespace PhotoInvasion.BLL
{
    public class UsersBLL
    {
        PhotoInvasion.DAL.UsersDAL _access = new PhotoInvasion.DAL.UsersDAL();

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
