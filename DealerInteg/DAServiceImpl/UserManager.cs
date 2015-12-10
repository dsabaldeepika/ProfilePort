using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


using DealerPortalCRM.DAService;
using DealerPortalCRM.ViewModels;
using DealerPortalCRM.Constants;
using Com.CoreLane.ScoringEngine.Models;


namespace DealerPortalCRM.DAServiceImpl
{
    public class UserManager: IUser
    {
        private readonly ScoringEngineEntities _dbContext;

        public UserManager(ScoringEngineEntities db)
        {
            this._dbContext = db;
        }

        public int getSalesRepID(string SalesRepName)
        {
            object[] parameters = { SalesRepName };

            String salesRepQuery = "SELECT Top 1 SalesReps.ID from Users " +
            "INNER JOIN Contacts ON  Users.Contact_ID = Contacts.ID " +
            "INNER JOIN SalesReps ON SalesReps.Contact_ID = Contacts.ID " +
            "WHERE Users.ADUser_Name = {0} ";

            int salesRepID = _dbContext.Database.SqlQuery<int>(salesRepQuery, parameters).First<int>();
            return salesRepID;

        }

        public int getDealerID(string DealerName)
        {
            // future Implementation ... 
            return 0;        
        }


        public void switchToExecutive(string userName)
        {
            _dbContext.Database.ExecuteSqlCommand("Delete from aspnet_UsersInRoles Where UserID = (SELECT UserID from aspnet_Users where UserName = '" + userName + "')");
            _dbContext.Database.ExecuteSqlCommand("INSERT INTO aspnet_UsersInRoles(UserID, RoleID) SELECT UserID = (SELECT UserID from aspnet_users WHERE UserID = (SELECT UserID from aspnet_Users where UserName = '" + userName + "')), RoleID = (SELECT RoleID from aspnet_Roles WHERE LoweredRoleName = 'executive')");
        }

        public void switchToSalesRep(string userName)
        {
            _dbContext.Database.ExecuteSqlCommand("Delete from aspnet_UsersInRoles Where UserID = (SELECT UserID from aspnet_Users where UserName = '" + userName + "')");
            _dbContext.Database.ExecuteSqlCommand("INSERT INTO aspnet_UsersInRoles(UserID, RoleID) SELECT UserID = (SELECT UserID from aspnet_users WHERE UserID = (SELECT UserID from aspnet_Users where UserName = '" + userName + "')), RoleID = (SELECT RoleID from aspnet_Roles WHERE LoweredRoleName = 'salesrep')");
        }

    }
}