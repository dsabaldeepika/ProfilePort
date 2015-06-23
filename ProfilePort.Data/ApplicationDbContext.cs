using Microsoft.AspNet.Identity.EntityFramework;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Data
{

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
           this.Configuration.LazyLoadingEnabled = false;
           this.Configuration.ProxyCreationEnabled = false;
        }
     
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
    
        
    }
}
        
   