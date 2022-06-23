using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class RandomLeaderboard : ILeaderboard
    {
        static int minLaps = 50;
        static int maxLaps = 70;

        static Dictionary<int, int> scoring = new Dictionary<int, int>()
        {
            { 1, 25 },
            { 2, 18 },
            { 3, 15 },
            { 4, 12 },
            { 5, 10 },
            { 6, 8 },
            { 7, 6 },
            { 8, 4 },
            { 9, 2 },
            { 10, 1 }
        };

        static readonly List<Driver> drivers = new List<Driver>()
        {
            new Driver() {Car = "Ferrari", FirstName = "Carlos", LastName = "Sainz", Number= 55},
            new Driver() {Car = "Ferrari", FirstName = "Charles", LastName = "Leclerc", Number= 16},
            new Driver() {Car = "Red Bull", FirstName = "Sergio", LastName = "Perez", Number= 11},
            new Driver() {Car = "Red Bull", FirstName = "Max", LastName = "Verstappen", Number= 1},
            new Driver() {Car = "McLaren", FirstName = "Daniel", LastName = "Ricciardo", Number= 33},
            new Driver() {Car = "McLaren", FirstName = "Lando", LastName = "Norris", Number= 14},
            new Driver() {Car = "Mercedes", FirstName = "Lewis", LastName = "Hamilton", Number= 4},
            new Driver() {Car = "Mercedes", FirstName = "George", LastName = "Russell", Number= 25}
        };

        public IList<RaceResult> GetLeaderboard(Sport sport, int gp)
        {
            //TODO: generate a random leaderboard using the GP as a seed

            List<RaceResult> standings = new List<RaceResult>();

            Random random = new Random(gp);

            int[] positions = Enumerable.Range(1, drivers.Count).OrderBy(x => random.Next()).ToArray();
            int[] gridPositions = Enumerable.Range(1, drivers.Count).OrderBy(x => random.Next()).ToArray();

            int laps = random.Next(minLaps, maxLaps);
            int outPos = random.Next(3, drivers.Count);

            for (int i = 0; i < drivers.Count; i++)
            {
                standings.Add(new RaceResult()
                {
                    Position = i + 1,
                    Driver = drivers[positions[i]-1],
                    Points = scoring.GetValueOrDefault(i + 1, 0),
                    GridPosition = gridPositions[i],
                    Laps = laps,
                    Out = i + 1 > outPos,
                    FastestLap = new TimeSpan(0, random.Next(1, 2), random.Next(0, 59))
                });
            }

            return standings;
        }
    }
}
