using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiveScoring.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoring.Services.Tests
{
    [TestClass()]
    public class ScrapingLeaderboardTests
    {
        [TestMethod()]
        public void GetLeaderboardTest()
        {
            ILeaderboard leaderboard = new Leaderboard(
                new ScrapingLeaderboardFactory(
                    new List<IScrapingLeaderboard>()
                    { 
                        new ScrapingLeaderboardF1(), 
                        new ScrapingLeaderboardMotoGP(), 
                        new ScrapingLeaderboardFE() 
                    }
                )
            );
            leaderboard.GetLeaderboard(Models.Sport.MotoGP, 2022, 1);
        }
    }
}