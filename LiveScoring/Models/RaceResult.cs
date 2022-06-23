using System;

namespace LiveScoring.Models
{
    public class RaceResult
    {
        public int Position { get; set; }
        public Driver Driver { get; set; }
        public int Points { get; set; }
        public int GridPosition { get; set; }
        public TimeSpan FastestLap { get; set; }
        public int Laps { get; set; }
        public bool Out { get; set; }

        public override string ToString()
        {
            return $"Position {Position}\tDriver {Driver}\t Points {Points}\t GridPosition {GridPosition}\t Laps {Laps} \t FastestLap {FastestLap} \t Out {Out}";
        }
    }
}
