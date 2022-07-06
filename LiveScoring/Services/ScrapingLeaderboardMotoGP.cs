using HtmlAgilityPack;
using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class ScrapingLeaderboardMotoGP : IScrapingLeaderboard
    {
        private static Dictionary<Tuple<Sport, int, int>, string> links =
            new Dictionary<Tuple<Sport, int, int>, string>()
        {
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 0),
                    "https://www.motogp.com/it/gp-results"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 1),
                    "https://www.motogp.com/it/gp-results/2022/QAT/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 2),
                    "https://www.motogp.com/it/gp-results/2022/INA/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 3),
                    "https://www.motogp.com/it/gp-results/2022/ARG/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 4),
                    "https://www.motogp.com/it/gp-results/2022/AME/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 5),
                    "https://www.motogp.com/it/gp-results/2022/POR/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 6),
                    "https://www.motogp.com/it/gp-results/2022/POR/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 7),
                    "https://www.motogp.com/it/gp-results/2022/FRA/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 8),
                    "https://www.motogp.com/it/gp-results/2022/ITA/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 9),
                    "https://www.motogp.com/it/gp-results/2022/CAT/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 10),
                    "https://www.motogp.com/it/gp-results/2022/GER/MotoGP/RAC/Classification"
                },
                {
                    new Tuple<Sport, int, int>(Sport.MotoGP, 2022, 11),
                    "https://www.motogp.com/api/results-front/be/results-api/session/57d5d131-1ffc-4cb3-98dc-e5e1c0a5c105/classifications"
                }
        };
        public IList<RaceResult> GetLatestLeaderboard()
        {
            throw new NotImplementedException();
        }

        public IList<RaceResult> GetLeaderboard(int year, int round)
        {

            Tuple<Sport, int, int> key = new Tuple<Sport, int, int>(Sport.MotoGP, year, round);
            IList<RaceResult> leaderboard = new List<RaceResult>();

            if (links.ContainsKey(key))
            {
                // dowload the html page
                JsonDocument document = this.DownloaJson(links[key]);

                foreach (var classifiation in document.RootElement.GetProperty("classification").EnumerateArray())
                {

                    leaderboard.Add(new RaceResult()
                    {
                        Driver = new Driver()
                        {
                            Car = classifiation.GetProperty("team").GetProperty("name").GetString(),
                            FirstName = classifiation.GetProperty("rider").GetProperty("full_name").GetString().Split(" ")[0],
                            LastName = classifiation.GetProperty("rider").GetProperty("full_name").GetString().Split(" ")[1],
                            Number = classifiation.GetProperty("rider").GetProperty("legacy_id").GetInt32()
                        },
                        GridPosition = -1,
                        Laps = classifiation.GetProperty("total_laps").GetInt32(),
                        Points = classifiation.GetProperty("points").GetInt32(),
                        //Position = classifiation.GetProperty("position").GetInt32(),
                        //Out = classifiation.GetProperty("position").GetInt32() == 0,
                        FastestLap = TimeSpan.MaxValue
                    });
                }

                //foreach (var record in document.RootElement.GetProperty("records").EnumerateArray())
                //{
                //    if (record.GetProperty("type").GetString().Equals("fastestLap"))
                //    {
                //        leaderboard.Where(
                //            res => res.Driver.Number == record.GetProperty("rider").GetProperty("legacy_id").GetInt32()
                //            ).First().FastestLap = TimeSpan.Parse("00:" + record.GetProperty("rider").GetProperty("best_lap").GetProperty("time").GetString());
                //    }
                //}
            }
            else
            {
                throw new Exception("Leaderboard not found");
            }

            return leaderboard;
        }

        private JsonDocument DownloaJson(string url)
        {
            JsonDocument document = null;
            string result = "";

            using (HttpClient client = new HttpClient())
            {
                using (HttpContent response = client.GetAsync(url).Result.Content)
                {
                    result = response.ReadAsStringAsync().Result;
                }
            }

            document = JsonDocument.Parse(result);
            return document;
        }

        private async Task<HtmlDocument> DownloadHtmlPageAsync(string url)
        {
            HtmlDocument document = new HtmlDocument();
            string result = "";

            using (HttpClient client = new HttpClient())
            {
                using (HttpContent response = (await client.GetAsync(url)).Content)
                {
                    result = await response.ReadAsStringAsync();
                }
            }

            document.LoadHtml(result);
            return document;
        }
    }
}
