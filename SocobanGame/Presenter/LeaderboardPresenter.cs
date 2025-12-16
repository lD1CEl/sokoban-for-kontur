using System;
using System.Collections.Generic;
using System.Linq;

namespace SocobanLevels
{
    public class LeaderboardPresenter
    {
        private readonly ILeaderboardView _view;
        private readonly StatsManager _statsManager;
        private readonly string _playerName;

        public LeaderboardPresenter(ILeaderboardView view, string playerName)
        {
            _view = view;
            _playerName = playerName;
            _statsManager = new StatsManager();
            _view.ViewLoaded += OnViewLoaded;
            _view.LevelSelected += OnLevelSelected;
        }

        private void OnViewLoaded(object sender, EventArgs e)
        {
            LoadLeaderboard();
        }

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

        private void OnLevelSelected(object sender, int levelNumber)
        {
            var entries = _statsManager.GetLeaderboardForLevel(levelNumber);
            _view.SetLeaderboardEntries(entries);
        }
    }
}
