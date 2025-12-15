using System;

namespace SocobanLevels
{
    public class StartPresenter
    {
        private readonly IStartView _view;

        public StartPresenter(IStartView view)
        {
            _view = view;
            _view.StartGameRequested += OnStartGameRequested;
        }

        private void OnStartGameRequested(object sender, EventArgs e)
        {
            _view.HideView();
            INameInputView nameInputView = new NameInputForm();
            ((System.Windows.Forms.Form)nameInputView).FormClosed += (s, args) => _view.ShowView();
            nameInputView.ShowView();
        }
    }
}
