using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
   public interface IProfiles
    {
        ProfileVM GetProfile(int id);
        Profile AddProfile(string userID, ProfileVM newTalent);
        Profile UpdateProfile(int ProfileID, ProfileVM newTalentVM);
        Profile DeleteProfile(int ProfileID);
    }
}
