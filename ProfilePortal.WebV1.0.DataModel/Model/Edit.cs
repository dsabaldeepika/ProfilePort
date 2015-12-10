using System;

namespace ProfilePort.DataModel
{
    public abstract class Edit
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
