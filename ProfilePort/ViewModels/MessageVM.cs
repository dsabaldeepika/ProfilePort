using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilePort.Adapters.Interfaces
{
   public class MessageVM
   {
       public int MessageId { get; set; }
       public string To { get; set; }
       public string ToId { get; set; }
       public string From { get; set; }
       public string FromId { get; set; }
       public string Subject { get; set; }
       public string Content { get; set; }
       public DateTime DateCreated { get; set; }
       public DateTime? DateRead { get; set; }


    }
}
