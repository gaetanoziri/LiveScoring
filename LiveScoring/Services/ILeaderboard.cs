using LiveScoring.Models;
using System.Collections.Generic;

namespace LiveScoring.Services
{
    public interface ILeaderboard
    {
        IList<RaceResult> GetLeaderboard(Sport sport, int gp);

        IList<RaceResult> GetLatestLeaderboard(Sport sport);
    }
}
