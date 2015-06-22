﻿using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{

    public class Interests : IInterests
    {

        ApplicationDbContext db = new ApplicationDbContext();

        List<DataModel.Interest> IInterests.GetInterest(string UserId)
        {
            return db.Interests.Where(m => m.UserId == UserId).ToList();
        }

        DataModel.Interest IInterests.GetInterest(int id)
        {
            return db.Interests.Where(m => m.InterestId == id).FirstOrDefault();
        }

        DataModel.Interest IInterests.PostNewInterest(string UserID, DataModel.Interest newInterest)
        {
            Interest Interest = new Interest();
            Interest.Name = newInterest.Name;
            Interest.Description = newInterest.Description;
            Interest.UserId = UserID;
            db.Interests.Add(Interest);
            db.SaveChanges();
            return newInterest;
        }

        DataModel.Interest IInterests.PutInterest(int id, DataModel.Interest newInterest)
        {
            Interest Interest = new Interest();
            Interest = db.Interests.Where(p => p.InterestId == id).FirstOrDefault();
            Interest.Name = newInterest.Name;
            Interest.Description = newInterest.Description;

            db.SaveChanges();
            return newInterest;
        }

        DataModel.Interest IInterests.DeleteInterest(int id)
        {
            Interest Interest = new Interest();
            Interest = db.Interests.Where(p => p.InterestId == id).FirstOrDefault();
            db.Interests.Remove(Interest);
            db.SaveChanges();
            return Interest;
        }
    }
}