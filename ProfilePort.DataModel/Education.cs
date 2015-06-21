using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

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
    

        [Required]
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public string Description { get; set; }
    }
}
