using System;

namespace SocobanLevels
{
    // Класс для представления записи в таблице лидеров
    public class LeaderboardEntry
    {
        public string PlayerName { get; set; }
        public int CompletedLevelsCount { get; set; }
        public int Steps { get; set; }
        public TimeSpan Time { get; set; }
    }
}
