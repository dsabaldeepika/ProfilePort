using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilePort.Adapters.Interfaces
{
     public class JobVM
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int YearsExperience { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
