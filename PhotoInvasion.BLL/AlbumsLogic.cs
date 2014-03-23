using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoInvasion.DAL;

namespace PhotoInvasion.BLL
{
    public class AlbumsLogic
    {
        AlbumsAccess _access = new AlbumsAccess();

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
