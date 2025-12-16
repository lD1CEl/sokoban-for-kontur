using System;
using System.Collections.Generic;

namespace SocobanLevels
{
    // Интерфейс окна выбора уровня
    public interface ILevelSelectionView
    {
        event EventHandler<int> LevelSelected;
        void SetLevels(List<int> levels);
        void ShowView();
        void HideView();
        void CloseView();
    }
}
