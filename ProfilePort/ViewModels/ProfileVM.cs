using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilePort.Adapters.Interfaces
{
      public class ProfileVM
    {
        public int ProfileId { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PicFile { get; set; }
        public bool IsLookingForJob { get; set; }
       // public decimal NoOfVisitors { get; set; }
    }
}
