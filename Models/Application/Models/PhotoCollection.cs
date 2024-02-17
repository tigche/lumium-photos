using Lumium.Photos.Models.Db.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumium.Photos.Models.Application.Models
{
    public class PhotoCollection : IEnumerable<FileStream>, IDisposable
    {
        private IEnumerable<Photo> photos;
        private Dictionary<string, FileStream> openStreams = new();
        private string repositoryLocation;
        public PhotoCollection(IEnumerable<Photo> photos, string repositoryLocation)
        {
            this.photos = photos;
            this.repositoryLocation = repositoryLocation;
        }

        public IEnumerable<LazyLoadPhoto> LazyLoad()
        {
            return photos
                .Select(photo =>
                {
                    return LazyLoadPhoto.FromPhoto(photo, ph => GetStream(ph));
                });
        }

        public FileStream this[int index]
        {
            get
            {
                if (index < 0 || index >= photos.Count())
                    throw new IndexOutOfRangeException("Index out of range.");

                return GetStream(photos.ElementAt(index));
            }
        }

        public IEnumerator<FileStream> GetEnumerator()
        {
            foreach (Photo photo in photos)
                yield return GetStream(photo);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public FileStream GetStream(Photo photo)
        {
            FileStream stream;
            if (!openStreams.TryGetValue(photo.FileName, out stream!))
            {
                string path = photo.GetFullPath(repositoryLocation);
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                openStreams.Add(photo.FileName, stream);
            }

            return stream;
        }

        public void Dispose()
        {
            openStreams
                .Select(kvp => kvp.Value)
                .ToList()
                .ForEach(stream =>
                {
                    stream.Close();
                    stream.Dispose();
                });
            openStreams.Clear();
        }
    }
}
