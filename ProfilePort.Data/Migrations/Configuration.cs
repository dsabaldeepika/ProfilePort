namespace ProfilePort.Data.Migrations
{
    using ProfilePort.DataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProfilePort.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProfilePort.Data.ApplicationDbContext context)
        {

            //context.Dashboards.AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);
            
            //var NoteList = new List<Note>() {
            // new Note  { NoteContent = " THis is a Real Note", Title = "My note", DashboardId = "1", },
            // new Note { NoteContent = " THis is a your Note", Title = "Your note", DashboardId = "2", },
            // new Note { NoteContent = " This is a Sample Note", Title = "Sample note", DashboardId = "1"}
            //}
        
        }
    }
}
