using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardPort.Adapters.Interfaces
{
   public interface IDashboard
    {
        User GetDashboard(string id);
        User AddDashboard(string DashboardId, User dashboard);
        string UpdateDashboard(string DashboardId, User dashboard);
        User DeleteDashboard(string DashboardId);
    }
}
