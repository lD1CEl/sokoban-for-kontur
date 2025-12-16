using System;
using System.Collections.Generic;

namespace SocobanLevels
{
    // Презентер окна выбора уровня
    public class LevelSelectionPresenter
    {
        private readonly ILevelSelectionView _view;
        private readonly LevelFile _levelFile;
        private readonly string _playerName;

        // Конструктор
        public LevelSelectionPresenter(ILevelSelectionView view, string playerName = "Player")
        {
            _view = view;
            _playerName = playerName;
            _levelFile = new LevelFile("LevelList.txt");
            _view.LevelSelected += OnLevelSelected;
        }

        // Инициализация - загрузка списка уровней
        public void Initialize()
        {
            var levels = _levelFile.GetLevelNumbers();
            _view.SetLevels(levels);
        }

        // Обработчик выбора уровня
        private void OnLevelSelected(object sender, int levelNumber)
        {
            _view.HideView();
            var gameView = new SocobanLevels(levelNumber, _playerName);
            ((System.Windows.Forms.Form)gameView).FormClosed += (s, args) => 
            {
                _view.ShowView();
            };
            gameView.ShowView();
        }
    }
}
