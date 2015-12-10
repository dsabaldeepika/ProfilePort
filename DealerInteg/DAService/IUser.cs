
using DealerPortalCRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerPortalCRM.Constants;

namespace DealerPortalCRM.DAService
{
    public interface IUser
    {
        int getSalesRepID(string SalesRepName);

        int getDealerID(string DealerName);

        void switchToExecutive(string userName);

        void switchToSalesRep(string userName);

    }

}
