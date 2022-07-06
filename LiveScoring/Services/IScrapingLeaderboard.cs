using LiveScoring.Models;
using System.Collections.Generic;

namespace LiveScoring.Services
{
    public interface IScrapingLeaderboard
    {
        IList<RaceResult> GetLeaderboard(int year, int round);

        IList<RaceResult> GetLatestLeaderboard();
    }
}
