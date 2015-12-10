using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DealerPortalCRM.ViewModels;
using System.Text;
using System.Threading.Tasks;
using DealerPortalCRM.Constants;

namespace DealerPortalCRM.DAService
{
    public interface ISecurity
    {
        int getUserID(string UserName);

    }
}
