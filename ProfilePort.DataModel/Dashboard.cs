using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.DataModel
{
   public class Dashboard 
    {
        public string Id { get; set; }
       
        public string DashboardName { get; set; }
    
        [InverseProperty("Dashboard")]
        public virtual List<Note> Notes { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Skill> Skills { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Message> Messages { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Favorite> Favorites { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Interest> Interests { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Education> Educations { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Job> Jobs { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Layout> Layout { get; set; }

        [InverseProperty("Dashboard")]
        public virtual List<Profile> Profile { get; set; }
        
       //public virtual Profile Profile { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
