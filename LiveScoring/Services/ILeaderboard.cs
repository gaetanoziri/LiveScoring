using LiveScoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Services
{
    public interface ILeaderboard
    {
        IList<RaceResult> GetLeaderboard(Sport sport, int gp);
    }
}
