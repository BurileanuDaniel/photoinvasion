using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.DAL
{
    public class RatingsDAL
    {
        DatabaseEntities _entities = new DatabaseEntities();

        public void addRating(Rating rating)
        {
            _entities.Rating.Add(rating);
            _entities.SaveChanges();
        }

        public int getRating(int photoId, int userId)
        {
            Rating rating = _entities.Rating
                        .Where(p => p.PhotoId == photoId && p.UserId == userId)
                        .SingleOrDefault();

            return rating == null ? -1 : rating.Rating1;
        }

        public void deleteRating(int photoId, int userId)
        {
            Rating rating = _entities.Rating
                                .Where(p => p.PhotoId == photoId && p.UserId == userId)
                                .SingleOrDefault();
            _entities.Rating
                    .Remove(rating);
            _entities.SaveChanges();
        }
    }
}
