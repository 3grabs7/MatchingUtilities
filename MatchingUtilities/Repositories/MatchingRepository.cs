using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingUtilities.Repositories
{
    public class MatchingRepository : IMatchingRepository
    {
        public int CalculateMatchScore<TOption>(AppUser currentUser, AppUser matchedUser)
        {
            var score = 0;

            if (typeof(TOption) == typeof(Interest))
            {
                foreach (var interest in currentUser.Interests)
                {
                    if (matchedUser.Interests.Contains(interest))
                    {
                        score += 100 - (currentUser.Interests.ToList().IndexOf(interest) * 10);
                    }
                }
            }

            if (typeof(TOption) == typeof(Hobby))
            {
                foreach (var hobby in currentUser.Hobbies)
                {
                    if (matchedUser.Hobbies.Contains(hobby))
                    {
                        score += 100 - (currentUser.Hobbies.ToList().IndexOf(hobby) * 10);
                    }
                }
            }

            return score;
        }

        public IEnumerable<AppUser> Match(AppUser currentUser, IEnumerable<AppUser> users, int returnCount, MatchingOption matchingOption = MatchingOption.Interest)
        {
            if (matchingOption == MatchingOption.Interest)
            {
                return users.GroupBy(g => currentUser.Interests.Where(i => g.Interests.Contains(i)).Count()).SelectMany(s => s);
            }
            if (matchingOption == MatchingOption.Hobby)
            {
                return users.GroupBy(g => currentUser.Hobbies.Where(h => g.Hobbies.Contains(h)).Count()).SelectMany(s => s);
            }
            return users.GroupBy(g =>
                currentUser.Interests.Where(i => g.Interests.Contains(i)).Count() +
                currentUser.Hobbies.Where(h => g.Hobbies.Contains(h)).Count())
                .SelectMany(s => s);
        }
    }
}
