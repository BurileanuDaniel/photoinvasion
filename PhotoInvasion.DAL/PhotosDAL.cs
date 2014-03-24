using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.DAL
{
    public class PhotosDAL
    {
        DatabaseEntities _entities = new DatabaseEntities();

        public void addPhoto(Photo photo)
        {

            try
            {
                _entities.Photo.Add(photo);
                _entities.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

        }

        public void deletePhoto(int id)
        {
            var photo = _entities.Photo
                            .Where(p => p.Id == id)
                            .SingleOrDefault();
            _entities.Photo.Remove(photo);
            _entities.SaveChanges();
        }

    }
}
