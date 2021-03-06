﻿using Microsoft.WindowsAzure;
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

        public void viewPhoto(int id)
        {
            Photo photo = _entities.Photo.Find(id);
                            //.Select(p => p.Id == id)
                            //.First();
                            //.SingleOrDefault();
            photo.Views += 1;
            _entities.SaveChanges();
                
        }

        public PhotoInvasion.DAL.Photo getPhoto(int id)
        {
            return _entities.Photo
                        .Where(p => p.Id == id)
                        .SingleOrDefault();
        }

        public void deletePhoto(int id)
        {
            var photo = _entities.Photo
                            .Where(p => p.Id == id)
                            .SingleOrDefault();
            _entities.Photo.Remove(photo);
            _entities.SaveChanges();
        }

        public List<PhotoInvasion.DAL.Photo> getNewsfeed()
        {
            return _entities.Photo
                            .OrderByDescending(p => p.Id)
                            .Take(28)
                            .ToList();
        }

        public List<PhotoInvasion.DAL.Photo> getMostViewed()
        {
            return _entities.Photo
                        .OrderByDescending(p => p.Views)
                        .Take(28)
                        .ToList();
        }

        public List<PhotoInvasion.DAL.Photo> getBestRated()
        {
            return _entities.Photo
                        .OrderByDescending(p => p.Rating.Average(r => r.Rating1))
                        .Take(28)
                        .ToList();
        }

        public List<PhotoInvasion.DAL.Photo> getRecentActivity(int userId)
        {
            return _entities.Photo
                        .OrderByDescending(p => p.Id)
                        .Where(p => p.UserId == userId)
                        .Take(5)
                        .ToList();
        }
    }
}
