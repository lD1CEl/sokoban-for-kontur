using System;

namespace SocobanLevels
{
    // Класс для хранения статистики прохождения уровня
    public class LevelStats
    {
        public int LevelNumber { get; set; }
        public string PlayerName { get; set; }
        public int Steps { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CompletedAt { get; set; }

        // Конструктор по умолчанию
        public LevelStats()
        {
        }

        // Конструктор с параметрами для создания записи статистики
        // <param name="levelNumber">Номер уровня</param>
        // <param name="playerName">Имя игрока</param>
        // <param name="steps">Количество шагов</param>
        // <param name="time">Время прохождения</param>
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
