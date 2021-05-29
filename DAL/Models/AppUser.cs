using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Hobby> Hobbies { get; set; }
        public ICollection<Interest> Interests { get; set; }
    }
}
