using System;

namespace SocobanLevels
{
    public interface IStartView
    {
        event EventHandler StartGameRequested;
        event EventHandler LeaderboardRequested;
        void CloseView();
        void HideView();
        void ShowView();
    }
}
