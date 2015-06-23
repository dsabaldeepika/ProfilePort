using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Contact :IContact
       
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      
        DataModel.ContactInfo IContact.GetContactInfo(int id)
        {
            return db.ContactInfos.Where(m => m.Id == id).FirstOrDefault();
        }

        DataModel.ContactInfo IContact.PostNewContactInfo(string UserId, DataModel.ContactInfo newContactInfo)
        {
            ContactInfo ContactInfo = new ContactInfo();
            ContactInfo.EmailAddress = newContactInfo.EmailAddress;
            ContactInfo.EmailAddress2 = newContactInfo.EmailAddress2;
            ContactInfo.HomeCity = newContactInfo.HomeCity;
            ContactInfo.HomeState = newContactInfo.HomeState;
            ContactInfo.HomeStreetAddress = newContactInfo.HomeStreetAddress;
            ContactInfo.HomeStreetAddress2 = newContactInfo.HomeStreetAddress2;
            ContactInfo.FacebookUrl = newContactInfo.FacebookUrl;
            ContactInfo.GoogleplusUrl = newContactInfo.GoogleplusUrl;
            ContactInfo.WorkCity = newContactInfo.WorkCity;
            ContactInfo.WorkState = newContactInfo.WorkState;
            ContactInfo.WorkStreetAddress = newContactInfo.WorkStreetAddress;
            ContactInfo.WorkStreetAddress2 = newContactInfo.WorkStreetAddress2;
            ContactInfo.LinkedinUrl = newContactInfo.LinkedinUrl;
            ContactInfo.PhoneNumber2 = newContactInfo.PhoneNumber2;
            ContactInfo.PhoneNumber = newContactInfo.PhoneNumber;
            ContactInfo.MainUrl = newContactInfo.MainUrl;
            db.ContactInfos.Add(ContactInfo);
            db.SaveChanges();
            return newContactInfo;
        }

        DataModel.ContactInfo IContact.PutContactInfo(int id, DataModel.ContactInfo newContactInfo)
        {
            ContactInfo ContactInfo = new ContactInfo();

            ContactInfo = db.ContactInfos.Where(p => p.Id == id)
            .FirstOrDefault();
            ContactInfo.EmailAddress = newContactInfo.EmailAddress;
            ContactInfo.EmailAddress2 = newContactInfo.EmailAddress2;
            ContactInfo.HomeCity = newContactInfo.HomeCity;
            ContactInfo.HomeState = newContactInfo.HomeState;
            ContactInfo.HomeStreetAddress = newContactInfo.HomeStreetAddress;
            ContactInfo.HomeStreetAddress2 = newContactInfo.HomeStreetAddress2;
            ContactInfo.FacebookUrl = newContactInfo.FacebookUrl;
            ContactInfo.GoogleplusUrl = newContactInfo.GoogleplusUrl;
            ContactInfo.WorkCity = newContactInfo.WorkCity;
            ContactInfo.WorkState = newContactInfo.WorkState;
            ContactInfo.WorkStreetAddress = newContactInfo.WorkStreetAddress;
            ContactInfo.WorkStreetAddress2 = newContactInfo.WorkStreetAddress2;
            ContactInfo.LinkedinUrl = newContactInfo.LinkedinUrl;
            ContactInfo.PhoneNumber2 = newContactInfo.PhoneNumber2;
            ContactInfo.PhoneNumber = newContactInfo.PhoneNumber;
            ContactInfo.MainUrl = newContactInfo.MainUrl;

            db.SaveChanges();
            return newContactInfo;
        }

        DataModel.ContactInfo IContact.DeleteContactInfo(int id)
        { ContactInfo ContactInfos = new ContactInfo();

            ContactInfos = db.ContactInfos.Where(p => p.Id == id)
          .FirstOrDefault();
            db.ContactInfos.Remove(ContactInfos);
            db.SaveChanges();
            return ContactInfos;
        }
    }
}