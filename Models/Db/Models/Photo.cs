
using Microsoft.EntityFrameworkCore;

namespace Lumium.Photos.Models.Db.Models
{
    [Index(nameof(FileName), IsUnique = true)]
    public class Photo
    {
        public int Id { get; set; } = -1;
        public ApplicationUser User { get; set; } = new();
        public string FileName { get; set; } = Guid.NewGuid().ToString();

        public string CreateName(string name = "")
        {
            FileName = $"{Guid.NewGuid()}-{name}";
            return FileName;
        }
        public string GetFullPath(string repositoryLocation)
            => Path.Combine(repositoryLocation, FileName);
    }
}
