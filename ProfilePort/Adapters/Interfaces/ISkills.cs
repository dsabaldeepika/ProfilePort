using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
     public interface ISkills
    {
        List<Skill> GetAllSkills();
        Skill GetSkill(int id);
        Skill PostNewSkill( string UserId, Skill newSkill);
        Skill PutSkill(int id, Skill newSkill);
        Skill DeleteSkill(int id);
    
    }
}
