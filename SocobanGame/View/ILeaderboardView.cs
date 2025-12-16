using System;
using System.Collections.Generic;

namespace SocobanLevels
{
    // Интерфейс окна таблицы лидеров
    public interface ILeaderboardView
    {
        event EventHandler ViewLoaded;
        event EventHandler ViewClosed;
        event EventHandler<int> LevelSelected;
        void SetPlayerName(string playerName);
        void SetCompletedCount(int count);
        void SetLevels(List<int> levels);
        void SetLeaderboardEntries(List<LeaderboardEntry> entries);
        void ShowView();
        void HideView();
        void CloseView();
    }
}
