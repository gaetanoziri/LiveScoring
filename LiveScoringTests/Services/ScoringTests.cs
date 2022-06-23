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
    public class ScoringTests
    {
        [TestMethod()]
        public void CalculateScoreTest()
        {
            //Arrange
            int gp = 20;
            List<Driver> team = new List<Driver>()
            {
                new Driver() {Car = "Red Bull", FirstName = "Sergio", LastName = "Perez", Number = 11},
                new Driver() {Car = "McLaren", FirstName = "Daniel", LastName = "Ricciardo", Number= 33},
                new Driver() {Car = "Mercedes", FirstName = "George", LastName = "Russell", Number= 25}
            };
            int scorePere =  8 + 0 + 0 + 1 - 4 + 1 + 1 + 0;
            int scoreRicc = 15 + 1 + 0 + 1 + 2 + 0 + 0 + 1;
            int scoreRuss = 12 + 0 + 5 + 1 - 3 + 1 + 1 + 1;

            IScoring scoringService = new Scoring();
            ILeaderboard leaderboardService = new RandomLeaderboard();

            //Act
            int score = scoringService.CalculateScore(
                leaderboardService.GetLeaderboard(Sport.F1, gp), team);

            //Assert
            Assert.AreEqual(scorePere + scoreRicc + scoreRuss, score);


            //+1 punto per ogni punto guadagnato dai propri piloti
            //+1 punto per il pilota che fa segnare il giro veloce
            //+5 punti per il pilota che conquista la pole position
            //+1 punti per il pilota che si classifica tra i primi sei nelle qualifiche
            //+1 punto per ogni posizione guadagnata in gara rispetto alla griglia di partenza
            //+1 punto per ogni pilota che batte il proprio compagno in qualifica
            //+1 punto per ogni pilota che batte il proprio compagno in gara
            //+1 punto per ogni pilota che termina la gara
        }
    }
}