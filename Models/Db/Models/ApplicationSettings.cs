using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumium.Photos.Models.Db.Models
{
    public class ApplicationSettings
    {
        public int Id { get; set; } = -1;
        public string RepositoryLocation { get; set; } = String.Empty;

        public void Replace(ApplicationSettings settings)
        {
            RepositoryLocation = settings.RepositoryLocation;
        }
    }
}
