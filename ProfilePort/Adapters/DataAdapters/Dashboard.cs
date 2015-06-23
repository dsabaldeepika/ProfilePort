using DashboardPort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Dashboard :IDashboard

    {
        private ApplicationDbContext db = new ApplicationDbContext();
      
        DataModel.User IDashboard.GetDashboard(string id)
        {
            User newUser = new User();
            newUser = db.Users.Find(id);
            return newUser;
        }

        DataModel.User IDashboard.AddDashboard(string userID, DataModel.User dashboard)
        {
            db.Users.Add(dashboard);
            db.SaveChanges();

            return dashboard;
        }

        string IDashboard.UpdateDashboard(string userId, DataModel.User dashboard)
        {
            User newUser = new User();
            newUser = db.Users.Find(userId);
            if (newUser !=null)
            {
                db.Entry(dashboard).State = EntityState.Modified;

            }
            return userId;

        }

        DataModel.User IDashboard.DeleteDashboard(string userId)
        {
            User newUser = new User();
            newUser = db.Users.Find(userId);

            db.Users.Remove(newUser);
            db.SaveChanges();

            return newUser;
        }
    }
}