using System;

namespace SocobanLevels
{
    // Интерфейс окна ввода имени игрока
    public interface INameInputView
    {
        event EventHandler<string> NameEntered;
        void ShowView();
        void CloseView();
        void HideView();
    }
}
