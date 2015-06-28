using DashboardPort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using ProfilePort.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Dashboards : IDashboard
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        getDashboardVM IDashboard.GetDashboard(string UserId)
        {
            Dashboard _dashmodel = new Dashboard();
            _dashmodel = db.Dashboards.Find(UserId);

            getDashboardVM _dashboardvm = new getDashboardVM
            {
                Educations = _dashmodel.Educations,
                Favorites = _dashmodel.Favorites,
                Interests = _dashmodel.Interests,
                Jobs = _dashmodel.Jobs,
                Messages = _dashmodel.Messages,
                Notes = _dashmodel.Notes,
                Skills = _dashmodel.Skills,
                Profile = _dashmodel.Profile

            };

            return _dashboardvm;
        }

        void IDashboard.DeleteDashboard(string DashboardId)
        {
            Dashboard _dashmodel = new Dashboard();
            _dashmodel = db.Dashboards.Find(DashboardId);

        }
    }
}