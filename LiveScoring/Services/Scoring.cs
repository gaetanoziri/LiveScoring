using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class Scoring : IScoring
    {
        public Dictionary<int, IList<ScoringEvent>> CalculateDetaildScore(IList<RaceResult> standing, IList<Driver> team)
        {
            Dictionary<int, IList<ScoringEvent>> score = new Dictionary<int, IList<ScoringEvent>>();

            foreach (var driver in team)
            {
                List<ScoringEvent> driverScorings = new List<ScoringEvent>();
                // logica di calcolo che deve popolare una lista List<ScoringEvent>

                // Prendo il risultato ottenuto dal pilota in gara
                RaceResult driverResult = standing.First(
                    raceResult => raceResult.Driver.Number == driver.Number
                    );

                // Prendo il risultato del compagno di squadra
                RaceResult mateResult = standing.First(
                    raceResult => raceResult.Driver.Car.Equals(driver.Car)
                    && raceResult.Driver.Number != driver.Number
                    );

                // Prendo il pilota che ha fatto il giro veloce in assoluto
                RaceResult fastestLapResult = standing.OrderBy(
                    raceResult => raceResult.FastestLap
                    ).First();

                //+1 punto per ogni punto guadagnato dai propri piloti
                driverScorings.Add(
                    new ScoringEvent() {
                        DriverNumber = driver.Number,
                        Event = "Race points", 
                        Score = driverResult.Points
                    });

                //+1 punto per il pilota che fa segnare il giro veloce in assoluto
                if (fastestLapResult.Driver.Number == driver.Number)
                {
                    driverScorings.Add(
                        new ScoringEvent()
                        {
                            DriverNumber = driver.Number,
                            Event = "Fastest Lap",
                            Score = 1
                        });
                }

                //+5 punti per il pilota che conquista la pole position
                if (driverResult.GridPosition == 1)
                {
                    driverScorings.Add(
                        new ScoringEvent()
                        {
                            DriverNumber = driver.Number,
                            Event = "Pole position",
                            Score = 5
                        });
                }

                //+1 punti per il pilota che si classifica tra i primi sei nelle qualifiche
                if (driverResult.GridPosition <= 6)
                {
                    driverScorings.Add(
                        new ScoringEvent()
                        {
                            DriverNumber = driver.Number,
                            Event = "Top six quali",
                            Score = 1
                        });
                }

                //+1 punto per ogni posizione guadagnata in gara rispetto alla griglia di partenza

                driverScorings.Add(
                    new ScoringEvent()
                    {
                        DriverNumber = driver.Number,
                        Event = "Overtake",
                        Score = driverResult.GridPosition - driverResult.Position
                    });

                //+1 punto per ogni pilota che batte il proprio compagno in qualifica
                if (driverResult.GridPosition < mateResult.GridPosition)
                {
                    driverScorings.Add(
                      new ScoringEvent()
                      {
                          DriverNumber = driver.Number,
                          Event = "H2H Quali",
                          Score = 1
                      });
                }

                //+1 punto per ogni pilota che batte il proprio compagno in gara
                if (driverResult.Position < mateResult.Position)
                {
                    driverScorings.Add(
                      new ScoringEvent()
                      {
                          DriverNumber = driver.Number,
                          Event = "H2H Race",
                          Score = 1
                      });
                }

                //+1 punto per ogni pilota che termina la gara
                if (!driverResult.Out)
                {
                    driverScorings.Add(
                      new ScoringEvent()
                      {
                          DriverNumber = driver.Number,
                          Event = "Race completed",
                          Score = 1
                      });
                }

                score.Add(driver.Number, driverScorings);
            }

            return score;
        }

        public int CalculateScore(IList<RaceResult> standing, IList<Driver> team)
        {
            int score = 0;
            foreach (var driver in team)
            {
                // Prendo il risultato ottenuto dal pilota in gara
                RaceResult driverResult = standing.First(
                    raceResult => raceResult.Driver.Number == driver.Number
                    );

                // Prendo il risultato del compagno di squadra
                RaceResult mateResult = standing.First(
                    raceResult => raceResult.Driver.Car.Equals(driver.Car)
                    && raceResult.Driver.Number != driver.Number
                    );

                // Prendo il pilota che ha fatto il giro veloce in assoluto
                RaceResult fastestLapResult = standing.OrderBy(
                    raceResult => raceResult.FastestLap
                    ).First();

                //+1 punto per ogni punto guadagnato dai propri piloti
                score += driverResult.Points;

                //+1 punto per il pilota che fa segnare il giro veloce in assoluto
                if (fastestLapResult.Driver.Number == driver.Number)
                { score += 1; }

                //+5 punti per il pilota che conquista la pole position
                if (driverResult.GridPosition == 1)
                { score += 5; }
 
                //+1 punti per il pilota che si classifica tra i primi sei nelle qualifiche
                if (driverResult.GridPosition <= 6)
                {  score += 1; }

                //+1 punto per ogni posizione guadagnata in gara rispetto alla griglia di partenza
                score += driverResult.GridPosition - driverResult.Position;

                //+1 punto per ogni pilota che batte il proprio compagno in qualifica
                if (driverResult.GridPosition < mateResult.GridPosition)
                { score += 1; }

                //+1 punto per ogni pilota che batte il proprio compagno in gara
                if (driverResult.Position < mateResult.Position)
                { score += 1;}

                //+1 punto per ogni pilota che termina la gara
                if (!driverResult.Out)
                { score += 1;}
            }
            return score;
        }
    }
}
