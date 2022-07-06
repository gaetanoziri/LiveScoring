using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public class ScrapingLeaderboardFactory
    {
        IScrapingLeaderboard _scrapingF1;
        IScrapingLeaderboard _scrapingMotoGP;
        IScrapingLeaderboard _scrapingFE;

        public ScrapingLeaderboardFactory(
            IEnumerable<IScrapingLeaderboard> scrapings)
        {
            _scrapingF1 = scrapings.ElementAt(0);
            _scrapingMotoGP = scrapings.ElementAt(1);
            _scrapingFE = scrapings.ElementAt(2);
        }
        
        public IScrapingLeaderboard CreateIstance(Sport sport)
        {
            IScrapingLeaderboard istance = null;
            switch (sport)
            {
                case Sport.F1:
                    istance = _scrapingF1;
                    break;
                case Sport.MotoGP:
                    istance = _scrapingMotoGP;
                    break;
                case Sport.FE:
                    istance = _scrapingFE;
                    break;
                default:
                    break;
            }
            return istance;
        }
    }
}
