using LiveScoring.Models;
using System.Collections.Generic;

namespace LiveScoring.Services
{
    public interface IScoring
    {
        int CalculateScore(IList<RaceResult> standing, IList<Driver> team);
        Dictionary<int, IList<ScoringEvent>> CalculateDetaildScore(IList<RaceResult> standing, IList<Driver> team);
    }
}
