using DAL.Models;
using GeoCoordinatePortable;
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


        public IEnumerable<AppUser> MatchLocation(AppUser currentUser,
            IEnumerable<AppUser> users,
            int returnCount,
            MatchingOption matchingOption = MatchingOption.Interest)
        {
            // Check users location
            var currentUserLocation = new GeoCoordinate(currentUser.Location.Latidude, currentUser.Location.Longitute);
            foreach (var user in users)
            {
                // Add distance from current user for each user to match against
                user.MatchingDistance = new GeoCoordinate(user.Location.Latidude, user.Location.Longitute)
                    .GetDistanceTo(currentUserLocation);

                // Count matchings by provided matching options
                if (matchingOption == MatchingOption.Hobby)
                {
                    user.MatchingCount = user.Hobbies.Where(h => currentUser.Hobbies.Contains(h)).Count();
                }

                if (matchingOption == MatchingOption.Interest)
                {
                    user.MatchingCount = user.Interests.Where(h => currentUser.Interests.Contains(h)).Count();
                }

                if (matchingOption == MatchingOption.InterestAndHobby)
                {
                    user.MatchingCount = user.Hobbies.Where(h => currentUser.Hobbies.Contains(h)).Count() +
                        user.Interests.Where(h => currentUser.Interests.Contains(h)).Count();
                }

                // Calculate matching score based on number of matches and distance
                user.MatchingScore = (user.MatchingCount * 1000) - (int)user.MatchingDistance;
            }

            // Return <returnCount> numbers of users ordered by highest matching score
            return users.OrderByDescending(o => o.MatchingScore).Take(returnCount);
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
