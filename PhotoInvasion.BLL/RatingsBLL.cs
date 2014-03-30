using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoInvasion.DAL;

namespace PhotoInvasion.BLL
{
    public class RatingsBLL
    {
        RatingsDAL _access = new RatingsDAL();

        public void addRating(PhotoInvasion.DAL.Rating rating)
        {
            _access.addRating(rating);
        }

        public int getRating(int photoId, int userId)
        {
            return _access.getRating(photoId, userId);
        }

        public void deleteRating(int photoId, int userId)
        {
            _access.deleteRating(photoId, userId);
        }
    }
}
