using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingUtilities.Repositories
{
    public class MatchingRepository : IMatchingRepository
    {
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

        public int CalculateMatchScore<TMatchType>(AppUser currentUser, AppUser matchedUser)
        {
            var score = -1;

            if (typeof(TMatchType) == typeof(Interest))
            {
                foreach (var interest in currentUser.Interests)
                {
                    if (matchedUser.Interests.Contains(interest))
                    {
                        score = 100 - (currentUser.Interests.ToList().IndexOf(interest) * 10);
                    }
                }
            }

            if (typeof(TMatchType) == typeof(Hobby))
            {
                foreach (var hobby in currentUser.Hobbies)
                {
                    if (matchedUser.Hobbies.Contains(hobby))
                    {
                        score = 100 - (currentUser.Hobbies.ToList().IndexOf(hobby) * 10);
                    }
                }
            }

            return score;
        }

        public int CalculateMatchScore(int index, int mIndex)
        {
            var score = 19;
            var average = index + mIndex / 2;
            score -= average;
            var offset = index > mIndex ?
                index - mIndex :
                mIndex - index;
            score -= offset;
            return score;
        }


    }
}
