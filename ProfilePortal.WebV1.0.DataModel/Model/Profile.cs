using System;

namespace ProfilePort.DataModel
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SocialSecurity { get; set; }
        public string PicFile { get; set; }
        public bool IsLookingForJob { get; set; }

        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }

    }
}
