using System.Collections.Generic;

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

        [InverseProperty("Dashboard")]
        public virtual List<Comment> Comments { get; set; }


        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
