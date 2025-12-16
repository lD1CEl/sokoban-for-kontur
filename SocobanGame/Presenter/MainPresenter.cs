using System;

namespace SocobanLevels
{
    // Презентер главного окна игры - управляет логикой игры и взаимодействием с представлением
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly LevelFile _levelFile;
        private readonly int _levelNumber;
        private readonly string _playerName;
        private readonly StatsManager _statsManager;
        private GameModel _gameModel;

        // Конструктор презентера
        // <param name="view">Представление</param>
        // <param name="levelNumber">Номер уровня</param>
        // <param name="playerName">Имя игрока</param>
        public MainPresenter(IMainView view, int levelNumber, string playerName = "Player")
        {
            _view = view;
            _levelNumber = levelNumber;
            _playerName = playerName;
            _statsManager = new StatsManager();
            _view.ViewLoaded += OnViewLoaded;
            _view.InputReceived += OnInputReceived;
            _levelFile = new LevelFile("LevelList.txt");
        }

        // Обработчик загрузки представления
        private void OnViewLoaded(object sender, EventArgs e)
        {
            LoadLevel(_levelNumber);
        }

        // Обработчик ввода от пользователя
        private void OnInputReceived(object sender, Direction direction)
        {
            if (_gameModel != null)
            {
                _gameModel.Move(direction);
            }
        }

        // Загрузка уровня
        // <param name="levelNumber">Номер уровня</param>
        public void LoadLevel(int levelNumber)
        {
            var grid = _levelFile.Load(levelNumber);
            if (grid != null)
            {
                _gameModel = new GameModel(grid);
                _gameModel.StateChanged += (s, e) => UpdateView();
                _gameModel.LevelCompleted += OnLevelCompleted;
                UpdateView();
            }
            else
            {
                _view.ShowError("Failed to load level.");
            }
        }

        // Обработчик завершения уровня
        private void OnLevelCompleted(object sender, EventArgs e)
        {
            if (_gameModel != null)
            {
                _statsManager.AddLevelCompletion(_levelNumber, _playerName, _gameModel.MoveCount, _gameModel.ElapsedTime);
            }
            _view.ShowLevelCompleted();
        }

        // Обновление представления
        private void UpdateView()
        {
            if (_gameModel != null)
            {
                _view.RenderLevel(_gameModel.Grid, _gameModel.Width, _gameModel.Height);
                _view.UpdateMoveCounter(_gameModel.MoveCount);
            }
        }
    }
}
