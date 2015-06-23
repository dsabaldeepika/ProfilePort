using ProfilePort.Adapters.Interfaces;
using ProfilePort.Data;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.Adapters.DataAdapters
{
    public class Skills :ISkills
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        List<DataModel.Skill> ISkills.GetAllSkills()
        {
            return db.Skills.ToList();
        }

        DataModel.Skill ISkills.GetSkill(int id)
        {
            Skill model = new Skill();

            model = db.Skills
                           .Where(j => j.SkillId == id)
             
                           .FirstOrDefault();
            return model;
        }

        DataModel.Skill ISkills.PostNewSkill(string DashboardId, DataModel.Skill newSkill)
        {
            Skill MySkill = new Skill();
            MySkill.DashboardId = DashboardId;
            MySkill.Description = newSkill.Description;
            MySkill.Name = newSkill.Name;
            db.Skills.Add(MySkill);
            db.SaveChanges();

            return MySkill; 
            
        }

        DataModel.Skill ISkills.PutSkill(int id, DataModel.Skill newSkill)
        {
            Skill MySkill = new Skill();
            MySkill= db.Skills.Where(j => j.SkillId == id)
                         .FirstOrDefault();
            MySkill.SkillId = id;
            MySkill.Description = newSkill.Description;
            MySkill.Name = newSkill.Name;
           
            db.SaveChanges();

            return MySkill; 
        }

        DataModel.Skill ISkills.DeleteSkill(int id)
        {
            Skill MySkill = new Skill();
            MySkill = db.Skills.Where(j => j.SkillId == id)
                          .FirstOrDefault();
            db.Skills.Remove(MySkill);
            db.SaveChanges();
            return MySkill;
        }
    }
}