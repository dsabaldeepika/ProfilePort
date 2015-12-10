using System;
using Newtonsoft.Json;

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
