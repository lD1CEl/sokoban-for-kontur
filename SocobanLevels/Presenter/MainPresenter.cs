using System;

namespace SocobanLevels
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly LevelFile _levelFile;
        private readonly int _levelNumber;
        private GameModel _gameModel;

        public MainPresenter(IMainView view, int levelNumber)
        {
            _view = view;
            _levelNumber = levelNumber;
            _view.ViewLoaded += OnViewLoaded;
            _view.InputReceived += OnInputReceived;
            _levelFile = new LevelFile("LevelList.txt");
        }

        private void OnViewLoaded(object sender, EventArgs e)
        {
            LoadLevel(_levelNumber);
        }

        private void OnInputReceived(object sender, Direction direction)
        {
            if (_gameModel != null)
            {
                _gameModel.Move(direction);
            }
        }

        public void LoadLevel(int levelNumber)
        {
            var grid = _levelFile.Load(levelNumber);
            if (grid != null)
            {
                _gameModel = new GameModel(grid);
                _gameModel.StateChanged += (s, e) => UpdateView();
                _gameModel.LevelCompleted += (s, e) => _view.ShowLevelCompleted();
                UpdateView();
            }
            else
            {
                _view.ShowError("Failed to load level.");
            }
        }

        private void UpdateView()
        {
            if (_gameModel != null)
            {
                _view.RenderLevel(_gameModel.Grid, _gameModel.Width, _gameModel.Height);
            }
        }
    }
}
