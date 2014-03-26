using PhotoInvasion.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.BLL
{
    public class HomePagePhotos
    {
        public List<PhotoInvasion.DAL.Photo> newsFeedPhotos { get; set; }
        public List<PhotoInvasion.DAL.Photo> mostViewedPhotos { get; set; }
        public List<PhotoInvasion.DAL.Photo> bestRatedPhotos { get; set; }
    }

    public class PhotosBLL
    {
        PhotosDAL _access = new PhotosDAL();

        public void addPhoto(PhotoInvasion.DAL.Photo photo)
        {
            _access.addPhoto(photo);
        }

        public void deletePhoto(int id)
        {
            _access.deletePhoto(id);
        }

        public HomePagePhotos getHomePhotos()
        {
            var newsFeed = _access.getNewsfeed();
            var mostViewed = _access.getMostViewed();
            var bestRated = _access.getBestRated();

            return new HomePagePhotos {
                newsFeedPhotos = newsFeed,
                mostViewedPhotos = mostViewed,
                bestRatedPhotos = bestRated,
            };
        }

        public List<PhotoInvasion.DAL.Photo> getRecentActivity(int userId)
        {
            return _access.getRecentActivity(userId);
        }
       
    }
}
