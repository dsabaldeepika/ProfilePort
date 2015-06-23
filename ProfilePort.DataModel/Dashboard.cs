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

        [InverseProperty("User")]
        public virtual List<Note> Notes { get; set; }

        [InverseProperty("User")]
        public virtual List<Skill> Skills { get; set; }

        [InverseProperty("User")]
        public virtual List<Message> Messages { get; set; }

        [InverseProperty("User")]
        public virtual List<Favorite> Favorites { get; set; }

        [InverseProperty("User")]
        public virtual List<Interest> Interests { get; set; }

        [InverseProperty("User")]
        public virtual List<Education> Educations { get; set; }

        [InverseProperty("User")]
        public virtual List<Job> Jobs { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
