using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ProfilePort.DataModel;

namespace ProfilePort.DataModel
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PicFile { get; set; }
        public bool IsLookingForJob { get; set; }
        
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }



    }
}
