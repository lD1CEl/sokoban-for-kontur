using System;

namespace SocobanLevels
{
    public interface IMainView
    {
        event EventHandler ViewLoaded;
        event EventHandler ViewClosed;
        event EventHandler<Direction> InputReceived;
        void RenderLevel(Cell[,] grid, int width, int height);
        void ShowError(string message);
        void ShowLevelCompleted();
        void CloseView();
        void ShowView();
        void UpdateMoveCounter(int moves);
    }
}
