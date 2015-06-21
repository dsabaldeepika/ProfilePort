using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
   public interface IInterests
    {
        List<Interest> GetInterest(String UserId);
        Interest GetInterest(int id);
        Interest PostNewInterest(string UserID, Interest newInterest);
        Interest PutInterest(int id, Interest newInterest);
        Interest DeleteInterest(int id);
    }
}
