using System;

namespace ProfilePort.DataModel
{
    public class Education 
    {
        public int EducationId { get; set; }

        public string School { get; set; }
        public DateTime? DatesAttended { get; set; }
        public string FieldofStudy { get; set; }
        public Char Grade{ get; set; }
        public string Activities { get; set; }
        public string Description { get; set; }


        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }
    }
}
