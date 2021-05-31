using System.Collections.Generic;

namespace DAL.Models
{
    public class MatchData : Entity
    {
        public AppUser User1 { get; set; }
        public AppUser User2 { get; set; }
        public ICollection<Interest> MatchingInterests { get; set; }
        public ICollection<Hobby> MatchingHobbies { get; set; }
        public int MatchScore { get; set; }
        public int ActivityScore { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
