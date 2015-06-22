using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace ProfilePort.Adapters.DataAdapters
{
    public class Educations : IEducations
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        List<DataModel.Education> IEducations.GetEducation(string UserId)
        {
            return db.Educations.Where(m => m.UserId == UserId).ToList();

        }

        DataModel.Education IEducations.GetEducation(int id)
        {
            return db.Educations.Where(m => m.EducationId == id).FirstOrDefault();
        }

        DataModel.Education IEducations.PostNewEducation(DataModel.Education m)
        {
            Education Education = new Education();
                      Education.Activities= m.Activities;
                      Education.DatesAttended= m.DatesAttended;
                      Education.Description= m.Description;
                      Education.Grade= m.Grade;
                      Education.FieldofStudy= m.FieldofStudy;
                      Education.School = m.School;
                      db.Educations.Add(Education);
            db.SaveChanges();
            return m;
        }
        DataModel.Education IEducations.PutEducation(int id, DataModel.Education m)
        {
            Education Education = new Education();
            Education = db.Educations.Where(p => p.EducationId == id).FirstOrDefault();
            
            Education.Activities = m.Activities;
            Education.DatesAttended = m.DatesAttended;
            Education.Description = m.Description;
            Education.Grade = m.Grade;
            Education.FieldofStudy = m.FieldofStudy;
            Education.School = m.School;
          
            db.SaveChanges();
            return m;

        }

        DataModel.Education IEducations.DeleteEducation(int id)
        {
            Education Education = new Education();
            Education = db.Educations.Where(p => p.EducationId == id).FirstOrDefault();

            db.Educations.Remove(Education);
            db.SaveChanges();
            return Education;
            
        }
    }
}