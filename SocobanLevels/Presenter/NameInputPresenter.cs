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
        }

        private void OnNameEntered(object sender, string name)
        {
        }
    }
}
