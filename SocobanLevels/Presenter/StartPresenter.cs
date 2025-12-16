using System;

namespace SocobanLevels
{
    public class StartPresenter
    {
        private readonly IStartView _view;
        private string _currentPlayerName => Session.PlayerName;

        public StartPresenter(IStartView view)
        {
            _view = view;
            _view.StartGameRequested += OnStartGameRequested;
            _view.LeaderboardRequested += OnLeaderboardRequested;
        }

        private void OnStartGameRequested(object sender, EventArgs e)
        {
            _view.HideView();
            ILevelSelectionView levelSelectionView = new LevelSelectionForm(_currentPlayerName);
            ((System.Windows.Forms.Form)levelSelectionView).FormClosed += (s, args) => _view.ShowView();
            levelSelectionView.ShowView();
        }

        private void OnLeaderboardRequested(object sender, EventArgs e)
        {
            _view.HideView();
            var leaderboardForm = new LeaderboardForm(_currentPlayerName);
            leaderboardForm.FormClosed += (s, args) => _view.ShowView();
            leaderboardForm.ShowDialog();
        }
    }
}
