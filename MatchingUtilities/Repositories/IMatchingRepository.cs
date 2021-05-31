using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingUtilities.Repositories
{
    public interface IMatchingRepository
    {
        IEnumerable<AppUser> Match(AppUser currentUser, IEnumerable<AppUser> users, int returnCount, MatchingOption matchingOption = MatchingOption.Interest);
        int CalculateMatchScore<TMatchType>(AppUser currentUser, AppUser matchedUser);
        int CalculateMatchScore(int index, int mIndex);
    }

    public enum MatchingOption
    {
        Interest, Hobby, InterestAndHobby
    }
}
