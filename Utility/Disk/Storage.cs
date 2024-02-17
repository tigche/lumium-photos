using Lumium.Photos.Models.Application.Models;
using Lumium.Photos.Models.Db.Context;
using Lumium.Photos.Models.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumium.Photos.Utility.Disk
{
    public class Storage : IDisposable
    {
        private ApplicationContext db;
        public Storage(IDbContextFactory<ApplicationContext> contextFactory)
        {
            db = contextFactory.CreateDbContext();
        }

        public IEnumerable<Photo> GetPhotos(string userId)
        {
            return db.Photos.Where(photo => photo.User.Id == userId);
        }
        public IEnumerable<FileStream> GetPhotoFileStreams()
            => GetPhotoFileStreams(db.Photos);
        public IEnumerable<FileStream> GetPhotoFileStreams(string userId)
        {
            IEnumerable<Photo> photos = GetPhotos(userId);
            return GetPhotoFileStreams(photos);
        }
        public IEnumerable<FileStream> GetPhotoFileStreams(IEnumerable<Photo> photos)
        {
            return photos.Select(photo =>
            {
                string path = photo.GetFullPath(db.Settings.RepositoryLocation);
                return new FileStream(path, FileMode.Open, FileAccess.Read);
            });
        }

        public IEnumerable<MemoryStream> GetPhotoMemoryStreams()
            => GetPhotoMemoryStreams(db.Photos);
        public IEnumerable<MemoryStream> GetPhotoMemoryStreams(string userId)
        {
            IEnumerable<Photo> photos = GetPhotos(userId);
            return GetPhotoMemoryStreams(photos);
        }
        public IEnumerable<MemoryStream> GetPhotoMemoryStreams(IEnumerable<Photo> photos)
        {
            return photos.Select(photo =>
            {
                string path = photo.GetFullPath(db.Settings.RepositoryLocation);
                using FileStream file = new(path, FileMode.Open, FileAccess.Read);

                MemoryStream memoryStream = new();
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            });
        }

        public PhotoCollection GetPhotoCollection()
            => new(db.Photos, db.Settings.RepositoryLocation);
        public PhotoCollection GetPhotoCollection(string userId)
        {
            IEnumerable<Photo> photos = GetPhotos(userId);
            return new(photos, db.Settings.RepositoryLocation);
        }

        public void Dispose() => db.Dispose();
    }
}
