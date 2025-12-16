using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    // Презентер окна ввода имени
    public class NameInputPresenter
    {
        private readonly INameInputView _view;

        // Конструктор
        // <param name="view">Представление</param>
        public NameInputPresenter(INameInputView view)
        {
            _view = view;
        }

        private void OnNameEntered(object sender, string name)
        {
        }
    }
}
