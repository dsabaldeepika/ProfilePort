using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
    public interface IContact
    {
        ContactInfo GetContactInfo(int id);
        ContactInfo PostNewContactInfo(string UserId, ContactInfo newContactInfo);
        ContactInfo PutContactInfo(int id, ContactInfo newContactInfo);
        ContactInfo DeleteContactInfo(int id);

    }
}
