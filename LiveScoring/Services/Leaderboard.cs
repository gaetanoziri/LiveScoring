using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class Leaderboard : ILeaderboard
    {
        private ScrapingLeaderboardFactory _factory;

        public Leaderboard(ScrapingLeaderboardFactory factory)
        {
            _factory = factory;
        }

        public IList<RaceResult> GetLatestLeaderboard(Sport sport)
        {
            return _factory.CreateIstance(sport).GetLatestLeaderboard();
        }

        public IList<RaceResult> GetLeaderboard(Sport sport, int year, int round)
        {
            return _factory.CreateIstance(sport).GetLeaderboard(year, round);
        }
    }
}
