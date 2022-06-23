using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Models
{
    public class ScoringEvent
    {
        public int DriverNumber { get; set; }
        public string Event { get; set; }
        public int Score { get; set; }
    }
}
