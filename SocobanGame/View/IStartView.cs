using System;

namespace SocobanLevels
{
    // Интерфейс стартового окна
    public interface IStartView
    {
        event EventHandler StartGameRequested;
        event EventHandler LeaderboardRequested;
        void CloseView();
        void HideView();
        void ShowView();
    }
}
