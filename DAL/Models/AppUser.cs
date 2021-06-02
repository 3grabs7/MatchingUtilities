using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Hobby> Hobbies { get; set; }
        public ICollection<Interest> Interests { get; set; }
        public Location Location { get; set; }

        [NotMapped]
        public double MatchingDistance { get; set; }
        [NotMapped]
        public int MatchingCount { get; set; }
        [NotMapped]
        public int MatchingScore { get; set; }
    }

    public class Location
    {
        public double Longitute { get; set; }
        public double Latidude { get; set; }
    }

}
