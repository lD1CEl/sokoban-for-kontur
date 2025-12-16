using System;

namespace SocobanLevels
{
    public class LeaderboardEntry
    {
        public string PlayerName { get; set; }
        public int CompletedLevelsCount { get; set; }
        public int Steps { get; set; }
        public TimeSpan Time { get; set; }
    }
}
