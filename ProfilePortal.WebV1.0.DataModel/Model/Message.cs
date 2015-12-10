using System;

namespace ProfilePort.DataModel
{
    public class Message : Edit
    {
        public int MessageId { get; set; }
        public string To { get; set; }
        public int ToId { get; set; }
        public string From { get; set; }
        public int FromId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
       // public DateTime DateCreated { get; set; }
        public DateTime? DateRead { get; set; }


        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }


    }
}
