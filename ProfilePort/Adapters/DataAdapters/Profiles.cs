using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Profiles:IProfiles
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProfileVM GetProfile(int id)
        {
            Profile MyProfile = new Profile();
            MyProfile = db.Profiles.Where(p => p.ProfileId == id).FirstOrDefault();

            ProfileVM VM = new ProfileVM();

            VM.ProfileId = MyProfile.ProfileId;
            VM.Sex = MyProfile.Sex;
            VM.PicFile = MyProfile.PicFile; //Added by George W. 5/6/14
            VM.DateOfBirth = MyProfile.DateOfBirth;
            VM.IsLookingForJob = MyProfile.IsLookingForJob;
            return VM;
        }

        public Profile AddProfile(string userID, ProfileVM newProfile)
        {
            Profile MyProfile = new Profile();
            MyProfile.UserId = userID;
            MyProfile.ProfileId = newProfile.ProfileId;
            MyProfile.Sex = newProfile.Sex;
            MyProfile.PicFile = newProfile.PicFile; 
            MyProfile.DateOfBirth = newProfile.DateOfBirth;
            MyProfile.IsLookingForJob = newProfile.IsLookingForJob;
            db.Profiles.Add(MyProfile);
            db.SaveChanges();

            return MyProfile; 
        }

        public Profile UpdateProfile(int ProfileID, ProfileVM newProfile)
        {
            Profile MyProfile = new Profile();

            MyProfile = db.Profiles.Where(p => p.ProfileId == ProfileID).FirstOrDefault();
            MyProfile.ProfileId = ProfileID;
            MyProfile.ProfileId = newProfile.ProfileId;
            MyProfile.Sex = newProfile.Sex;
            MyProfile.PicFile = newProfile.PicFile; 
            MyProfile.DateOfBirth = newProfile.DateOfBirth;
            MyProfile.IsLookingForJob = newProfile.IsLookingForJob;
            db.Profiles.Add(MyProfile);
            db.SaveChanges();

            return MyProfile; 
        }

        public Profile DeleteProfile(int ProfileID)
        {
            Profile MyProfile = new Profile();
            MyProfile = db.Profiles.Where(p => p.ProfileId == ProfileID).FirstOrDefault();
            db.Profiles.Remove(MyProfile);
            db.SaveChanges();

            return db.Profiles.FirstOrDefault();
           
        }
    }
}