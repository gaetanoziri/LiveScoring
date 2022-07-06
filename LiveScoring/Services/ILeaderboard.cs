using LiveScoring.Models;
using System.Collections.Generic;

namespace LiveScoring.Services
{
    public interface ILeaderboard
    {
        IList<RaceResult> GetLeaderboard(Sport sport, int year, int round);

        IList<RaceResult> GetLatestLeaderboard(Sport sport);
    }
}
