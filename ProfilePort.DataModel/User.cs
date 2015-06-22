using Microsoft.AspNet.Identity.EntityFramework;
using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.DataModel
{
    public class User : IdentityUser
    {
        //public async Task<ClaimsIdentity> GenerateApplicationUserIdentityAsync(ApplicationUserManager<ApplicationApplicationUser> manager, string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var ApplicationUserIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    // Add custom ApplicationUser claims here
        //    return ApplicationUserIdentity;
        //}
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string MiddleName{ get; set;}
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set;}
        public bool IsDeleted { get; set; }

        [InverseProperty("User")]
        public virtual List<Note> Notes { get; set; }

        [InverseProperty("User")]
        public virtual List<Skill> Skills { get; set; }

        [InverseProperty("User")]
        public virtual List<Message> Messages { get; set; }

        [InverseProperty("User")]
        public virtual List<Favorite> Favorites { get; set; }

        [InverseProperty("User")]
        public virtual List<Interest> Interests { get; set; }

        [InverseProperty("User")]
        public virtual List<Education> Educations { get; set; }

        [InverseProperty("User")]
        public virtual List<Job> Jobs { get; set; }




    }
}
