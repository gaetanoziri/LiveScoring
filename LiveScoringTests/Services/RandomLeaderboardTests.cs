using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiveScoring.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveScoring.Models;

namespace LiveScoring.Services.Tests
{
    [TestClass()]
    public class RandomLeaderboardTests
    {
        [TestMethod()]
        public void GetLeaderboardTest()
        {
            ILeaderboard leaderboard = new RandomLeaderboard();
            foreach (var result in leaderboard.GetLeaderboard(Sport.F1, 20)){
                Console.WriteLine(result);
            }
        }
    }
}