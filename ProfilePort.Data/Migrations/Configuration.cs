namespace ProfilePort.Data.Migrations
{
    using ProfilePort.DataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProfilePort.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProfilePort.Data.ApplicationDbContext context)
        {
            var Notes = new List<Note>
            {
             new Note ()  { NoteContent = " THis is a Real Note", Title = "My note", DashboardId = "1", },
             new Note () { NoteContent = " THis is a your Note", Title = "Your note", DashboardId = "1", },
             new Note() { NoteContent = " This is a Sample Note", Title = "Sample note", DashboardId = "1"},
             new Note() { NoteContent = " This is a Sample Note", Title = "Sample note", DashboardId = "1"}

            };

            var Comments = new List<Comment>
            {
             new Comment ()  { Content="sdfsdf", DateCreated= new DateTime(2014,12,2), DateUpdated= new DateTime(2012,2,2), From= "Sabal", FromId=1,ToId=2, To="Prabal", DashboardId="1"},
             new Comment ()  { Content="sdfsdf", DateCreated= new DateTime(2014,12,2), DateUpdated= new DateTime(2012,2,2), From= "Sabal", FromId=1,ToId=2, To="Prabal" , DashboardId="1"},
 new Comment ()  { Content="sdfsdf", DateCreated= new DateTime(2014,12,2), DateUpdated= new DateTime(2012,2,2), From= "Sabal", FromId=1,ToId=2, To="Prabal" , DashboardId="1"},       
             new Comment ()  { Content="sdfsdf", DateCreated= new DateTime(2014,12,2), DateUpdated= new DateTime(2012,2,2), From= "Sabal", FromId=1,ToId=2, To="Prabal" , DashboardId="1"}
             
            };
            var Messages = new List<Message>
            {
            new Message () { DashboardId = "1", Subject = "Message", Content = "Hello Hello", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DateRead = DateTime.Now, },
             new Message() { DashboardId = "1", Subject = "Message", Content = "Hello Hello", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DateRead = DateTime.Now }
             
            };

            var Jobs = new List<Job>
            {
               new Job() { JobTitle = "Hardware Mechanic", StartDate = new DateTime(1989, 2, 2), Description = "Good job", DashboardId = "1", YearsExperience = 5, },
            new Job() { JobTitle = "Software Engineering", StartDate = new DateTime(2014, 2, 2), Description = "Good job", DashboardId = "1", YearsExperience = 5, },
            new Job() { JobTitle = "Project Manager", StartDate = new DateTime(2010, 2, 6), Description = "Manage Project", DashboardId = "1", YearsExperience = 5}
             
            };

            var Skills = new List<Skill>{ 
               new Skill { Name = "Javascript", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "Main", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "Java", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "c++", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "c#", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "Plumbing", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "Consntruction", Description = "Real Awesome SKill these days", DashboardId = "1"},
               new Skill { Name = "Destruction", Description = "Real aweful SKill ", DashboardId = "1"},
         
           };

            var Educations = new List<Education>
           { 
              new Education() { FieldofStudy = "Science & Tech", Grade = 'A', School = "University Of Houston", DashboardId = "1", Activities = "Nothing", },
            new Education() { FieldofStudy = "Business", Grade = 'A', School = "University Of Houston", DashboardId = "1", Activities = "Nothing", },
            new Education() { FieldofStudy = "Technology", Grade = 'A', School = "University Of Houston", DashboardId = "1", Activities = "Nothing", },
            new Education() { FieldofStudy = "Science & Tech", Grade = 'A', School = "University Of Houston", DashboardId = "1", Activities = "Nothing", },
            new Education() { FieldofStudy = "Business", Grade = 'A', School = "University Of Houston", DashboardId = "1", Activities = "Nothing", },
            new Education() { FieldofStudy = "Technology", Grade = 'A', School = "University Of Houston", DashboardId = "1", Activities = "Nothing", }
         
           };
            var Favorites = new List<Favorite>{
              new Favorite() { Name = "Ipad", DashboardId = "1", Description = "Ipad is my fav", },
              new Favorite() { Name = "Iphone", DashboardId = "1", Description = "Ipad is my fav", },
              new Favorite() { Name = "Ipad", DashboardId = "1", Description = "Ipad is my fav", }

        };

            var ContactInfoes = new List<ContactInfo>{
            new ContactInfo () { LinkedinUrl = "www.facebook.conm", PhoneNumber = "453245", PhoneNumber2 = "577777", MainUrl = "www.sdfg.com", WorkCity = "Anaheim", WorkZipCode = "23454", WorkState = "Texas", HomeCity = "sadf", HomeState = "asdf", HomeZipCode = "435", TwitterUrl = "www.sdafasd.com", FacebookUrl = "asdfasd", GoogleplusUrl = "sadfsadf", HomeStreetAddress = "sadfsad", HomeStreetAddress2 = "kkkkk", EmailAddress = "asdfasdf", EmailAddress2 = "fasdfasdf", WorkStreetAddress = "asdfasdf", WorkStreetAddress2 = "asdfasd", DashboardId = "1", },
            new ContactInfo () { LinkedinUrl = "www.yahoo.conm", PhoneNumber = "96789", PhoneNumber2 = "345634", MainUrl = "www.sfdg.com", WorkCity = "Houston", WorkZipCode = "23454", WorkState = "Texas", HomeCity = "sadf", HomeState = "asdf", HomeZipCode = "435", TwitterUrl = "www.sdafasd.com", FacebookUrl = "asdfasd", GoogleplusUrl = "sadfsadf", HomeStreetAddress = "sadfsad", HomeStreetAddress2 = "jjjjj", EmailAddress = "asdfasdf", EmailAddress2 = "fasdfasdf", WorkStreetAddress = "asdfasdf", WorkStreetAddress2 = "asdfasd", DashboardId = "1", },
            new ContactInfo () { LinkedinUrl = "www.google.conm", PhoneNumber = "345234", PhoneNumber2 = "6579879000", MainUrl = "www.fgdsdfg.com", WorkCity = "Dallas", WorkZipCode = "23454", WorkState = "Texas", HomeCity = "sadf", HomeState = "asdf", HomeZipCode = "435", TwitterUrl = "www.hgdfgh.com", FacebookUrl = "hgjkhgj", GoogleplusUrl = "dsfbv ", HomeStreetAddress = "dfddfd", HomeStreetAddress2 = "gghhhhh", EmailAddress = "asdfasdf", EmailAddress2 = "fasdfasdf", WorkStreetAddress = "asdfasdf", WorkStreetAddress2 = "asdfasd", DashboardId = "1", },
            new ContactInfo () { LinkedinUrl = "www.linkedin.conm", PhoneNumber = "65467456", PhoneNumber2 = "7777777", MainUrl = "www.werewr.com", WorkCity = "LA", WorkZipCode = "23454", WorkState = "Texas", HomeCity = "sadf", HomeState = "asdf", HomeZipCode = "435", TwitterUrl = "www.asdfasd.com", FacebookUrl = "asdfasd", GoogleplusUrl = "sadfsadf", HomeStreetAddress = "ffff", HomeStreetAddress2 = "gggg", EmailAddress = "asdfasdf", EmailAddress2 = "fasdfasdf", WorkStreetAddress = "asdfasdf", WorkStreetAddress2 = "asdfasd", DashboardId = "1", },
            };

            var Interests = new List<Interest>{
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", },
                new Interest() { Description = "my interest", Name = "Book", DashboardId = "1", }

            };
            
            var Profiles = new List<Profile>{
               new Profile { IsLookingForJob = true, Sex = "Male", PicFile = "Pic1", DateOfBirth = new DateTime(1985, 7, 5), DashboardId = "1", }
            };

            var Layouts = new List<Layout>{
                new Layout(){ HeaderColor="Nlack", BackgroundColor="White", FooterColor="Red", LayoutName="Real Layout", DateCreated= new DateTime(2012,4,4), DateUpdated= new DateTime(2012,4,4), NavigationBarColor="Back", BodyColor= "Red", DashboardId="1"},
                
                new Layout(){ HeaderColor="Blue", BackgroundColor="White", FooterColor="Black", LayoutName="Main Layout", DateCreated= new DateTime(2012,4,4), DateUpdated= new DateTime(2012,4,4), NavigationBarColor="Back", BodyColor= "Red", DashboardId="1"},
                
                new Layout(){ HeaderColor="White", BackgroundColor="White", FooterColor="Red", LayoutName="Modified Layout", DateCreated= new DateTime(2012,4,4), DateUpdated= new DateTime(2012,4,4), NavigationBarColor="Back", BodyColor= "Red", DashboardId="1"}
            };

            try
            {
               
            context.Dashboards.AddOrUpdate(
              p => p.Id,
              new Dashboard { DashboardName = "Sabal Prasain", Educations = Educations, Favorites = Favorites, Interests = Interests, Messages = Messages, Notes = Notes, Layout = Layouts, Profile = Profiles, Jobs = Jobs, Skills = Skills, UserId = "1", Comments=Comments, Id="1" }

            );
            context.SaveChanges();

        }
                 catch (DbEntityValidationException ex)
            {
                string errorList = string.Empty;
                ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors).ToList().ForEach(e => errorList += e.PropertyName + ": " + e.ErrorMessage + " | ");

                throw new Exception(errorList);
            }
           

        }

    }
}
