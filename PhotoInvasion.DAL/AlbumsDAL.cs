using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.DAL
{
    public class AlbumsDAL
    {
        DatabaseEntities _entities = new DatabaseEntities();

        public void addAlbum(Album album)
        {
            _entities.Album.Add(album);
            _entities.SaveChanges();
        }

        public List<Album> getAlbums(int userId)
        {
            return _entities.Album
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        public Album getAlbumDetails(int albumId)
        {
            return _entities.Album
                .Where(a => a.Id == albumId)
                .SingleOrDefault();
        }
    }
}
