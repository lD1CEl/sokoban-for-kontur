using System;
using System.Collections.Generic;
using System.Linq;

namespace SocobanLevels
{
    // Презентер окна таблицы лидеров
    public class LeaderboardPresenter
    {
        private readonly ILeaderboardView _view;
        private readonly StatsManager _statsManager;
        private readonly string _playerName;

        // Конструктор
        public LeaderboardPresenter(ILeaderboardView view, string playerName)
        {
            _view = view;
            _playerName = playerName;
            _statsManager = new StatsManager();
            _view.ViewLoaded += OnViewLoaded;
            _view.LevelSelected += OnLevelSelected;
        }

        // Обработчик загрузки представления
        private void OnViewLoaded(object sender, EventArgs e)
        {
            LoadLeaderboard();
        }

        // Загрузка таблицы лидеров
        private void LoadLeaderboard()
        {
            _view.SetPlayerName(_playerName);
            var completedCount = _statsManager.GetCompletedLevelsCount(_playerName);
            _view.SetCompletedCount(completedCount);
            var allLevels = _statsManager.GetAllLevels();
            _view.SetLevels(allLevels);
            if (allLevels.Count > 0)
            {
                var entries = _statsManager.GetLeaderboardForLevel(allLevels[0]);
                _view.SetLeaderboardEntries(entries);
            }
        }

        // Обработчик выбора уровня для просмотра статистики
        private void OnLevelSelected(object sender, int levelNumber)
        {
            var entries = _statsManager.GetLeaderboardForLevel(levelNumber);
            _view.SetLeaderboardEntries(entries);
        }
    }
}
