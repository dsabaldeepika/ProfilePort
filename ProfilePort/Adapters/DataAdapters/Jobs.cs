using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Jobs : IJobs
    {
        ApplicationDbContext db = new ApplicationDbContext();

        List<JobVM> IJobs.GetJobs(string userID)
        {
            List<JobVM> model = new List<JobVM>();
            User user = db.Users.Where(u => u.Id == userID).FirstOrDefault();

            if (userID != null) 
            {
                List<Job> jobs = db.Jobs.ToList();
                foreach (Job job in jobs)
                {
                    JobVM vm = new JobVM();
                    vm.JobId = job.JobId;
                    vm.JobTitle = job.JobTitle;
                    vm.Description = job.Description;
                    vm.StartDate = job.StartDate;
                    vm.YearsExperience = vm.YearsExperience;
                   
                    model.Add(vm);
                }
            }
              return model;
        }

        JobVM IJobs.GetJob(int id)
        {

            Job job = new Job();

            job = db.Jobs.Where(j => j.JobId == id).FirstOrDefault();

            JobVM vm = new JobVM();
            vm.JobId = job.JobId;
            vm.JobTitle = job.JobTitle;
            vm.Description = job.Description;
            vm.StartDate = job.StartDate;
            vm.YearsExperience = vm.YearsExperience;

            return vm;

        }

        DataModel.Job IJobs.PostNewJob(string userid, JobVM newjob)
        {
            Job job = new Job();
            job.JobTitle = newjob.JobTitle;
            job.Description = newjob.Description;
            job.YearsExperience = newjob.YearsExperience;
         
            job.StartDate = DateTime.Now;
            job.EndDate = DateTime.Now;
            job.UserId = userid;
            db.Jobs.Add(job);
            db.SaveChanges();

            return job; 
        }

        DataModel.Job IJobs.PutNewJob(int id, DataModel.Job newjob)
        {
            db.Jobs.Where(j => j.JobId == id).FirstOrDefault();
            Job job = new Job();
            job.JobId = newjob.JobId;
            job.JobTitle = newjob.JobTitle;
            job.Description = newjob.Description;
            job.YearsExperience = newjob.YearsExperience;
            job.StartDate = DateTime.Now;
            job.EndDate = DateTime.Now;
            job.UserId = newjob.UserId;
            db.SaveChanges();
            return newjob;
        }


        DataModel.Job IJobs.DeleteJob(int id)
        {
            Job job = new Job();
            job = db.Jobs
                            .Where(j => j.JobId == id).FirstOrDefault();
            db.Jobs.Remove(job);
            db.SaveChanges();
            return db.Jobs.FirstOrDefault();
        }
    }
}