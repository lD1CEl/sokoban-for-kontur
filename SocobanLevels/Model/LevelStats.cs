using System;

namespace SocobanLevels
{
    public class LevelStats
    {
        public int LevelNumber { get; set; }
        public string PlayerName { get; set; }
        public int Steps { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CompletedAt { get; set; }

        public LevelStats()
        {
        }

        public LevelStats(int levelNumber, string playerName, int steps, TimeSpan time)
        {
            LevelNumber = levelNumber;
            PlayerName = playerName;
            Steps = steps;
            Time = time;
            CompletedAt = DateTime.Now;
        }
    }
}
