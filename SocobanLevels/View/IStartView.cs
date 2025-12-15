using System;

namespace SocobanLevels
{
    public interface IStartView
    {
        event EventHandler StartGameRequested;
        void CloseView();
        void HideView();
        void ShowView();
    }
}
