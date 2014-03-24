using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoInvasion.DAL;

namespace PhotoInvasion.BLL
{
    public class AlbumsBLL
    {
        AlbumsDAL _access = new AlbumsDAL();

        public List<Album> getAlbums(int userId)
        {
            return _access.getAlbums(userId);
        }

        public Album getAlbumDetails(int albumId)
        {
            return _access.getAlbumDetails(albumId);
        }
    }
}
