using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public class NameInputPresenter
    {
        private readonly INameInputView _view;

        public NameInputPresenter(INameInputView view)
        {
            _view = view;
            _view.NameEntered += OnNameEntered;
        }

        private void OnNameEntered(object sender, string name)
        {
            _view.HideView();
            ILevelSelectionView levelSelectionView = new LevelSelectionForm();
            ((Form)levelSelectionView).FormClosed += (s, args) => _view.ShowView();
            levelSelectionView.ShowView();
        }
    }
}
