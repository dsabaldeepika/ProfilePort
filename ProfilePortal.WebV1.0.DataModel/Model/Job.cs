using System;

namespace ProfilePort.DataModel
{
    public  class Job
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int YearsExperience { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }
    }
}
