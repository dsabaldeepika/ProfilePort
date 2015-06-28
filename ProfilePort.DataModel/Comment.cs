using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ProfilePort.DataModel 
{
  public class Comment : Edit
    {
        public int CommentId { get; set; }
        public string To { get; set; }
        public int ToId { get; set; }
        public string From { get; set; }
        public int FromId { get; set; }
        public string Content { get; set; }
        public DateTime DateDeleted { get; set; }
        
        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }


    }
}
