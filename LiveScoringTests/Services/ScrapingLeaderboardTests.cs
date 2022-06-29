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
            ILeaderboard leaderboard = new ScrapingLeaderboard();
            leaderboard.GetLeaderboard(Models.Sport.FE, 1);
        }
    }
}