using ProfilePort.DataModel;
using ProfilePort.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardPort.Adapters.Interfaces
{
   public interface IDashboard
    {
       DashboardVM GetDashboard(string UserId);
        void DeleteDashboard(string DashboardId);
    }
}

