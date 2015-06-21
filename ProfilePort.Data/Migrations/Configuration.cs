
using ProfilePort.DataModel;
using System;
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
        context.Profiles.AddOrUpdate(
          p => p.ProfileId,
          new Profile { IsLookingForJob = true, Sex = "Male", PicFile = "Pic1", DateOfBirth = new DateTime(1985, 7, 5), UserId = "Sambo", },
           new Profile { IsLookingForJob = false, Sex = "Male", PicFile = "Pic2", DateOfBirth = new DateTime(1988, 4, 1), UserId = "Prambo", },
           new Profile { IsLookingForJob = false, Sex = "Male", PicFile = "Pic3", DateOfBirth = new DateTime(1990, 7, 4), UserId = "Zango", });

        context.SaveChanges();
        context.Notes.AddOrUpdate(
        p => p.Id,
        new Note { NoteContent = " THis is a Real Note", Title = "My Notesss", UserId = "Zango", },
         new Note { NoteContent = " THis is a your Note", Title = "Your note", UserId = "Prambo", },
          new Note
          {
              NoteContent = " THis is a Sample Note",
              Title = "Sample note",
              UserId = "Zango",
          });
        context.SaveChanges();

        context.Messages.AddOrUpdate(
    p => p.MessageId,

    new Message { UserId = "Prambo", Subject = "Message", Content = "Hello Hello", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DateRead = DateTime.Now, });
        context.SaveChanges();

        context.Messages.AddOrUpdate(
    p => p.MessageId,

    new Message { UserId = "Prambo", Subject = "Message", Content = "Hello Hello", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DateRead = DateTime.Now, });
       
        context.SaveChanges();
       
        context.Jobs.AddOrUpdate(
        p => p.JobId,
        new Job { JobTitle = "Hardware Mechanic", StartDate = new DateTime(1989, 2, 2), Description = "Good job", UserId = "Prambo", YearsExperience = 5, },
        new Job { JobTitle = "Software Engineering", StartDate = new DateTime(2014, 2, 2), Description = "Good job", UserId = "Prambo", YearsExperience = 5, },
        new Job { JobTitle = "Project Manager", StartDate = new DateTime(2010, 2, 6), Description = "Manage Project", UserId = "Zango", YearsExperience = 5, });
        context.SaveChanges();

        context.Skills.AddOrUpdate(
        p => p.SkillId,
        new Skill { Name = "Javascript", Description = "REal Awesome SKill these days", UserId = "Zango", });
        context.SaveChanges();

        context.Educations.AddOrUpdate(p => p.EducationId,
            new Education { FieldofStudy = "Science & Tech", Grade = 'A', School = "University Of Houston", UserId = "Zango", Activities = "Nothing", },
        new Education { FieldofStudy = "Business", Grade = 'A', School = "University Of Houston", UserId = "Zango", Activities = "Nothing", },
        new Education { FieldofStudy = "Technology", Grade = 'A', School = "University Of Houston", UserId = "Zango", Activities = "Nothing", });
        context.SaveChanges();

        context.Favorites.AddOrUpdate(p => p.FavoriteId,
            new Favorite { Name = "Ipad", UserId = "Zango", Description = "Ipad is my fav", },
            new Favorite { Name = "Iphone", UserId = "Zango", Description = "Ipad is my fav", },
            new Favorite { Name = "Ipad", UserId = "Zango", Description = "Ipad is my fav", });
        context.SaveChanges();


        context.ContactInfos.AddOrUpdate(p => p.Id,
           new ContactInfo { LinkedinUrl = "www.linkedin.conm", PhoneNumber = "453245", PhoneNumber2 = "2452345", MainUrl = "www.werewr.com", WorkCity = "Anaheim", WorkZipCode = "23454", WorkState = "Texas", HomeCity = "sadf", HomeState = "asdf", HomeZipCode = "435", TwitterUrl = "www.sdafasd.com", FacebookUrl = "asdfasd", GoogleplusUrl = "sadfsadf", HomeStreetAddress = "sadfsad", HomeStreetAddress2 = "asdfasdf", EmailAddress = "asdfasdf", EmailAddress2 = "fasdfasdf", WorkStreetAddress = "asdfasdf", WorkStreetAddress2 = "asdfasd", UserId = "Zango", }
            );
        context.SaveChanges();

        context.Interests.AddOrUpdate(p => p.InterestId,
            new Interest { Description = "my interest", Name = "Book", UserId = "Zango", }
            );
        context.SaveChanges();


    }
}

