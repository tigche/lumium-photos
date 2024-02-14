using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Lumium.Photos.Models.Db.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public ICollection<Photo> Photos { get; set; } = new Collection<Photo>();
}

