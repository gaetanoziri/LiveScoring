using HtmlAgilityPack;
using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class ScrapingLeaderboard : ILeaderboard
    {
        private static Dictionary<Tuple<Sport, int>, string> links = 
            new Dictionary<Tuple<Sport, int>, string>()
        {
                { 
                    new Tuple<Sport, int>(Sport.FE, 0),
                    "https://www.fiaformulae.com/en/results/race-results/"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 1),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210201"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 2),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210202"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 3),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210203"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 4),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210204"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 5),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210205"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 6),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210206"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 7),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210207"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 8),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210208"
                },
                {
                    new Tuple<Sport, int>(Sport.FE, 9),
                    "https://www.fiaformulae.com/en/results/race-results/?championship=2022021&race=20210209"
                }
        };

        public IList<RaceResult> GetLatestLeaderboard(Sport sport)
        {
            return this.GetLeaderboard(sport, 0);
        }

        public IList<RaceResult> GetLeaderboard(Sport sport, int gp)
        {
            Tuple<Sport, int> key = new Tuple<Sport, int>(sport, gp);
            IList<RaceResult> leaderboard = new List<RaceResult>();

            if (links.ContainsKey(key))
            {
                // dowload the html page
                HtmlDocument document = this.DownloadHtmlPage(links[key]);
                var tableBody = document.DocumentNode.SelectSingleNode(
                    "//div[@class=\"standings-table standings-table--race-results\"]/table/tbody"
                    );

                IEnumerable<HtmlNode> rows = tableBody.Descendants("tr");
                for (int i = 0; i < rows.Count(); i+=2)
                {
                    HtmlDocument row1 = new HtmlDocument();
                    row1.LoadHtml(rows.ElementAt(i).OuterHtml);
                    HtmlNode rowNode1 = row1.DocumentNode;

                    var fastetLapString = rowNode1.SelectNodes("//span[@class=\"stat__value\"]").ElementAt(1).InnerText;
                    TimeSpan fastestLap;
                    if (!TimeSpan.TryParse("00:0" + fastetLapString, out fastestLap))
                    {fastestLap = TimeSpan.MaxValue;}
                    int pos = Convert.ToInt32(rowNode1.SelectSingleNode("//div[@class=\"pos\"]/span").InnerText);
                    //get data from rows[i] and rows[i+1]
                    leaderboard.Add(new RaceResult() {
                        Driver = new Driver()
                        {
                            Car = rowNode1.SelectSingleNode(
                                "//span[@class=\"team__name\"]").InnerText,
                            FirstName = rowNode1.SelectSingleNode(
                                "//div[@class=\"driver__fname\"]").InnerText,
                            LastName = rowNode1.SelectSingleNode(
                                "//div[@class=\"driver__lname\"]/span[@class=\"full\"]").InnerText,
                            Number = Convert.ToInt32(
                                rowNode1.SelectSingleNode(
                                    "//div[@class=\"driver__number\"]").InnerText.Substring(1))
                        },
                        GridPosition = Convert.ToInt32(
                                rowNode1.SelectNodes(
                            "//span[@class=\"stat__value\"]").ElementAt(0).InnerText),
                        Laps = 0,
                        Points = Convert.ToInt32(
                                rowNode1.SelectSingleNode(
                            "//div[@class=\"points\"]").InnerText),
                        Position = pos == 0 ? (i+2)/2 : pos,
                        Out = rowNode1.SelectSingleNode("//span[@class=\"stat-indicator stat-indicator--dnf\"]") != null   ,
                        FastestLap = fastestLap
                    });
                }
            } else
            {
                throw new Exception("Leaderboard not found");
            }

            return leaderboard;
        }

        private HtmlDocument DownloadHtmlPage(string url)
        {
            HtmlDocument document = new HtmlDocument();
            string result = "";

            using (HttpClient client = new HttpClient())
            {
                using(HttpContent response = client.GetAsync(url).Result.Content)
                {
                    result = response.ReadAsStringAsync().Result;
                }
            }

            document.LoadHtml(result);
            return document;
        }

        private async Task<HtmlDocument> DownloadHtmlPageAsync(string url)
        {
            HtmlDocument document = new HtmlDocument();
            string result = "";

            using (HttpClient client = new HttpClient())
            {
                using (HttpContent response  = (await client.GetAsync(url)).Content)
                {
                    result = await response.ReadAsStringAsync();
                }
            }

            document.LoadHtml(result);
            return document;
        }
    }
}
