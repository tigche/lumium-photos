using Lumium.Photos.Models.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lumium.Photos.Models.Db.Context;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationSettings> ApplicationSettings { get; set; }
    public ApplicationSettings Settings
    {
        get
        {
            VerifySettingsCreated();
            return ApplicationSettings.First();
        }
        set
        {
            VerifySettingsCreated();
            ApplicationSettings.First().Replace(value);
            SaveChanges();
        }
    }

    protected void VerifySettingsCreated()
    {
        if (ApplicationSettings.Count() < 1)
        {
            ApplicationSettings.Add(new());
            SaveChanges();
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
