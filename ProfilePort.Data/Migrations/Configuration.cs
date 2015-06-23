
using ProfilePort.DataModel;
using System;
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

        context.Profiles.AddOrUpdate(
          p => p.ProfileId,
          new Profile { IsLookingForJob = true, Sex = "Male", PicFile = "Pic1", DateOfBirth = new DateTime(1985, 7, 5), UserId = "Rango", },
           new Profile { IsLookingForJob = false, Sex = "Male", PicFile = "Pic2", DateOfBirth = new DateTime(1988, 4, 1), UserId = "Hambo", },
           new Profile { IsLookingForJob = false, Sex = "Male", PicFile = "Pic3", DateOfBirth = new DateTime(1990, 7, 4), UserId = "Rango", });

        context.SaveChanges();
        context.Notes.AddOrUpdate(
        p => p.Id,
        new Note { NoteContent = " THis is a Real Note", Title = "My note", UserId = "Rango", },
         new Note { NoteContent = " THis is a your Note", Title = "Your note", UserId = "Hambo", },
          new Note
          {
              NoteContent = " THis is a Sample Note",
              Title = "Sample note",
              UserId = "Rango",
          });
        context.SaveChanges();

        context.Messages.AddOrUpdate(
    p => p.MessageId,

    new Message { UserId = "Rango", From = "Sabal", To = "Prbal", ToId = "Hambo", FromId = "Rango", Subject = "Message", Content = "Hello Hello", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DateRead = DateTime.Now, });
        context.SaveChanges();

        context.Messages.AddOrUpdate(
    p => p.MessageId,

    new Message { UserId = "Hambo", Subject = "Message", Content = "Hello Hello", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DateRead = DateTime.Now, });
        context.SaveChanges();

        context.Jobs.AddOrUpdate(
        p => p.JobId,
        new Job { JobTitle = "Hardware Mechanic", StartDate = new DateTime(1989, 2, 2), Description = "Good job", UserId = "Hambo", YearsExperience = 5, },
        new Job { JobTitle = "Software Engineering", StartDate = new DateTime(2014, 2, 2), Description = "Good job", UserId = "Hambo", YearsExperience = 5, },
        new Job { JobTitle = "Project Manager", StartDate = new DateTime(2010, 2, 6), Description = "Manage Project", UserId = "Rango", YearsExperience = 5, });
        context.SaveChanges();

        context.Skills.AddOrUpdate(
        p => p.SkillId,
        new Skill { Name = "Javascript", Description = "Real Awesome SKill these days", UserId = "Rango", });
        context.SaveChanges();

        context.Educations.AddOrUpdate(p => p.EducationId,
            new Education { FieldofStudy = "Science & Tech", Grade = 'A', School = "University Of Houston", UserId = "Rango", Activities = "Nothing", },
        new Education { FieldofStudy = "Business", Grade = 'A', School = "University Of Houston", UserId = "Rango", Activities = "Nothing", },
        new Education { FieldofStudy = "Technology", Grade = 'A', School = "University Of Houston", UserId = "Rango", Activities = "Nothing", });
        context.SaveChanges();

        context.Favorites.AddOrUpdate(p => p.FavoriteId,
            new Favorite { Name = "Ipad", UserId = "Rango", Description = "Ipad is my fav", },
            new Favorite { Name = "Iphone", UserId = "Rango", Description = "Ipad is my fav", },
            new Favorite { Name = "Ipad", UserId = "Rango", Description = "Ipad is my fav", });
        context.SaveChanges();


        context.ContactInfos.AddOrUpdate(p => p.Id,
           new ContactInfo { LinkedinUrl = "www.linkedin.conm", PhoneNumber = "453245", PhoneNumber2 = "2452345", MainUrl = "www.werewr.com", WorkCity = "Anaheim", WorkZipCode = "23454", WorkState = "Texas", HomeCity = "sadf", HomeState = "asdf", HomeZipCode = "435", TwitterUrl = "www.sdafasd.com", FacebookUrl = "asdfasd", GoogleplusUrl = "sadfsadf", HomeStreetAddress = "sadfsad", HomeStreetAddress2 = "asdfasdf", EmailAddress = "asdfasdf", EmailAddress2 = "fasdfasdf", WorkStreetAddress = "asdfasdf", WorkStreetAddress2 = "asdfasd", UserId = "Rango", }
            );
        context.SaveChanges();

        context.Interests.AddOrUpdate(p => p.InterestId,
            new Interest { Description = "my interest", Name = "Book", UserId = "Rango", }
            );
        context.SaveChanges();


    }
}


