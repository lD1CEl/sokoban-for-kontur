using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SocobanLevels
{
    // Менеджер для управления статистикой игры
    public class StatsManager
    {
        private readonly string _statsFilePath;
        private List<LevelStats> _allStats;

        // Конструктор - загружает статистику из файла
        public StatsManager()
        {
            _statsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stats.json");
            LoadStats();
        }

        // Загрузка статистики из JSON файла
        private void LoadStats()
        {
            try
            {
                if (File.Exists(_statsFilePath))
                {
                    var json = File.ReadAllText(_statsFilePath);
                    _allStats = JsonSerializer.Deserialize<List<LevelStats>>(json) ?? new List<LevelStats>();
                }
                else
                {
                    _allStats = new List<LevelStats>();
                }
            }
            catch
            {
                _allStats = new List<LevelStats>();
            }
        }

        // Сохранение статистики в JSON файл
        private void SaveStats()
        {
            try
            {
                var json = JsonSerializer.Serialize(_allStats, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_statsFilePath, json);
            }
            catch
            {
            }
        }

        // Добавление записи о прохождении уровня
        public void AddLevelCompletion(int levelNumber, string playerName, int steps, TimeSpan time)
        {
            var stat = new LevelStats(levelNumber, playerName, steps, time);
            _allStats.Add(stat);
            SaveStats();
        }

        // Получение всей статистики игрока
        public List<LevelStats> GetPlayerStats(string playerName)
        {
            return _allStats
                .Where(s => s.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Получение количества пройденных уровней
        public int GetCompletedLevelsCount(string playerName)
        {
            return _allStats
                .Where(s => s.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.LevelNumber)
                .Distinct()
                .Count();
        }

        // Получение лучшего результата игрока на конкретном уровне
        public LevelStats GetBestResult(int levelNumber, string playerName)
        {
            return _allStats
                .Where(s => s.LevelNumber == levelNumber && s.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Steps)
                .ThenBy(s => s.Time)
                .FirstOrDefault();
        }

        // Получение списка всех уровней, которые кем-то пройдены
        public List<int> GetAllLevels()
        {
            return _allStats
                .Select(s => s.LevelNumber)
                .Distinct()
                .OrderBy(l => l)
                .ToList();
        }

        // Получение всей статистики для конкретного уровня
        public List<LevelStats> GetStatsForLevel(int levelNumber)
        {
            return _allStats
                .Where(s => s.LevelNumber == levelNumber)
                .OrderBy(s => s.Steps)
                .ThenBy(s => s.Time)
                .ToList();
        }

        // Получение таблицы лидеров для конкретного уровня
        public List<LeaderboardEntry> GetLeaderboardForLevel(int levelNumber)
        {
            var levelStats = _allStats
                .Where(s => s.LevelNumber == levelNumber)
                .GroupBy(s => s.PlayerName)
                .Select(g => g.OrderBy(s => s.Steps).ThenBy(s => s.Time).First())
                .OrderBy(s => s.Steps)
                .ThenBy(s => s.Time)
                .ToList();

            var entries = new List<LeaderboardEntry>();
            foreach (var stat in levelStats)
            {
                entries.Add(new LeaderboardEntry
                {
                    PlayerName = stat.PlayerName,
                    CompletedLevelsCount = GetCompletedLevelsCount(stat.PlayerName),
                    Steps = stat.Steps,
                    Time = stat.Time
                });
            }
            return entries;
        }

        // Получение списка пройденных игроком уровней
        public List<int> GetCompletedLevels(string playerName)
        {
            return _allStats
                .Where(s => s.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.LevelNumber)
                .Distinct()
                .OrderBy(l => l)
                .ToList();
        }
    }
}
