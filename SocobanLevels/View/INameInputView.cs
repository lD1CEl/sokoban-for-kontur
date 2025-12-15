using System;

namespace SocobanLevels
{
    public interface INameInputView
    {
        event EventHandler<string> NameEntered;
        void ShowView();
        void CloseView();
        void HideView();
    }
}
