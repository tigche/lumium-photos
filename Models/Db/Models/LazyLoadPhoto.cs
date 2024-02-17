using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumium.Photos.Models.Db.Models
{
    public class LazyLoadPhoto : Photo
    {
        public delegate FileStream OpenPhoto(Photo photo);
        private OpenPhoto OpenWithPhoto { get; init; }
        public FileStream Open() => OpenWithPhoto(this);
        internal LazyLoadPhoto(OpenPhoto open)
        {
            OpenWithPhoto = open;
        }

        internal static LazyLoadPhoto FromPhoto(Photo photo, OpenPhoto open)
        {
            return new LazyLoadPhoto(open)
            {
                Id = photo.Id,
                User = photo.User,
                FileName = photo.FileName
            };
        }
    }
}
