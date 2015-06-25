using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilePort.ViewModels
{
    public class DashboardVM
    {
        public string Id { get; set; }

        public string  DashboardName { get; set; }
       
        public virtual List<Note> Notes { get; set; }

        
        public virtual List<Skill> Skills { get; set; }

        
        public virtual List<Message> Messages { get; set; }

        
        public virtual List<Favorite> Favorites { get; set; }

        
        public virtual List<Interest> Interests { get; set; }

        
        public virtual List<Education> Educations { get; set; }

        
        public virtual List<Job> Jobs { get; set; }

    }
}