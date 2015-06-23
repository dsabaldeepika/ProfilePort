using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
    public interface IEducations
    {
        List<Education> GetEducation(string DashboardId);
        Education GetEducation(int id);
        Education PostNewEducation(Education newEducation);
        Education PutEducation(int id, Education newEducation);
        Education DeleteEducation(int id);
    }
}