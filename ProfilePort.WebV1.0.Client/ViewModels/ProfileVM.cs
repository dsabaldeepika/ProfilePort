using System;

namespace ProfilePort.Adapters.Interfaces
{
    public class ProfileVM:User
    {
        public int ProfileId { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PicFile { get; set; }
        public bool IsLookingForJob { get; set; }
       // public decimal NoOfVisitors { get; set; }
        public string UserID { get; set; }
    }
}
