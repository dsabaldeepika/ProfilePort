using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ProfilePort.DataModel;

namespace ProfilePort.DataModel
{
   public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NoteContent { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
