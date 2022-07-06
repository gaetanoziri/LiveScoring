using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class ScrapingLeaderboardMotoGP : IScrapingLeaderboard
    {
        public IList<RaceResult> GetLatestLeaderboard()
        {
            throw new NotImplementedException();
        }

        public IList<RaceResult> GetLeaderboard(int year, int round)
        {
            throw new NotImplementedException();
        }
    }
}
