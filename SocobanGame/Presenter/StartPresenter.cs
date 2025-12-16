using System;

namespace SocobanLevels
{
    // Презентер стартового окна
    public class StartPresenter
    {
        private readonly IStartView _view;
        
        private string _currentPlayerName 
        { 
            get { return Session.PlayerName; } 
        }

        // Конструктор
        // <param name="view">Представление</param>
        public StartPresenter(IStartView view)
        {
            _view = view;
            _view.StartGameRequested += OnStartGameRequested;
            _view.LeaderboardRequested += OnLeaderboardRequested;
        }

        // Обработчик запроса на начало игры
        private void OnStartGameRequested(object sender, EventArgs e)
        {
            _view.HideView();
            ILevelSelectionView levelSelectionView = new LevelSelectionForm(_currentPlayerName);
            ((System.Windows.Forms.Form)levelSelectionView).FormClosed += (s, args) => _view.ShowView();
            levelSelectionView.ShowView();
        }

        // Обработчик запроса на открытие таблицы лидеров
        private void OnLeaderboardRequested(object sender, EventArgs e)
        {
            _view.HideView();
            var leaderboardForm = new LeaderboardForm(_currentPlayerName);
            leaderboardForm.FormClosed += (s, args) => _view.ShowView();
            leaderboardForm.ShowDialog();
        }
    }
}
