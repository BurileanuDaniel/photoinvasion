using PhotoInvasion.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.BLL
{
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

        public List<PhotoInvasion.DAL.Photo> getNewsfeed()
        {
            return _access.getNewsfeed();
        }
    }
}
