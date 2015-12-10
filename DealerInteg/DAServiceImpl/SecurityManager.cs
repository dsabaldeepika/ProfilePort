using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


using DealerPortalCRM.DAService;
using DealerPortalCRM.ViewModels;
using DealerPortalCRM.Constants;



using DealerPortalCRM.DAService;
using DealerPortalCRM.ViewModels;
using Com.CoreLane.ScoringEngine.Models;

namespace DealerPortalCRM.DAServiceImpl
{
    public class SecurityManager : ISecurity
    {
        private readonly ScoringEngineEntities _dbContext;

        public SecurityManager(ScoringEngineEntities db)
        {
            this._dbContext = db;
        }

        public int getUserID(string UserName)
        {
            object[] parameters = { UserName };

            String userQuery = "SELECT ID from Users " +
            "WHERE Users.ADUser_Name = '" + UserName + "'";         

            int UserID = _dbContext.Database.SqlQuery<int>(userQuery, parameters).FirstOrDefault<int>();
            return UserID;

        }
    }
}

