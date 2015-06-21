﻿using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
  public  interface IJobs
    {
        List<JobVM> GetJobs(string userID);
        JobVM GetJob(int id); 
        Job PostNewJob(string userid, JobVM newJob);
        Job PutNewJob(int id, Job newJob);
        Job DeleteJob(int id);
    }
}
