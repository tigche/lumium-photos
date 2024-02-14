using Lumium.Photos.Models.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumium.Photos.Models.Db.Context
{
    public class DataContext : DbContext
    {
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

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
