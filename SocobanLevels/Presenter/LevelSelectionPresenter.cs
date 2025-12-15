using System;
using System.Collections.Generic;

namespace SocobanLevels
{
    public class LevelSelectionPresenter
    {
        private readonly ILevelSelectionView _view;
        private readonly LevelFile _levelFile;

        public LevelSelectionPresenter(ILevelSelectionView view)
        {
            _view = view;
            _levelFile = new LevelFile("LevelList.txt");
            _view.LevelSelected += OnLevelSelected;
        }

        public void Initialize()
        {
            List<int> levels = _levelFile.GetLevelNumbers();
            _view.SetLevels(levels);
        }

        private void OnLevelSelected(object sender, int levelNumber)
        {
            _view.HideView();
            IMainView gameView = new SocobanLevels(levelNumber);
            ((System.Windows.Forms.Form)gameView).FormClosed += (s, args) => 
            {
                _view.ShowView();
            };
            gameView.ShowView();
        }
    }
}
