using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class LeaderboardForm : Form, ILeaderboardView
    {
        private LeaderboardPresenter _presenter;

        public event EventHandler ViewLoaded;
        public event EventHandler ViewClosed;
        public event EventHandler<int> LevelSelected;

        public LeaderboardForm(string playerName)
        {
            InitializeComponent();
            _presenter = new LeaderboardPresenter(this, playerName);
            this.Load += (s, e) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            this.FormClosed += (s, e) => ViewClosed?.Invoke(this, EventArgs.Empty);
        }

        public void SetPlayerName(string playerName)
        {
            playerNameLabel.Text = $"Игрок: {playerName}";
        }

        public void SetCompletedCount(int count)
        {
            completedCountLabel.Text = $"Пройдено уровней: {count}";
        }

        public void SetLevels(List<int> levels)
        {
            if (levelComboBox == null) return;
            levelComboBox.Items.Clear();
            foreach (var lvl in levels)
            {
                levelComboBox.Items.Add(lvl);
            }
            if (levelComboBox.Items.Count > 0)
                levelComboBox.SelectedIndex = 0;
        }

        public void SetLeaderboardEntries(List<LeaderboardEntry> entries)
        {
            statsListView.Items.Clear();
            foreach (var entry in entries)
            {
                var item = new ListViewItem(new[]
                {
                    entry.PlayerName,
                    entry.CompletedLevelsCount.ToString(),
                    entry.Steps.ToString(),
                    FormatTime(entry.Time)
                });
                item.Tag = entry;
                statsListView.Items.Add(item);
            }
        }

        private string FormatTime(TimeSpan time)
        {
            if (time.TotalHours >= 1)
                return $"{(int)time.TotalHours:D2}:{time.Minutes:D2}:{time.Seconds:D2}";
            else
                return $"{time.Minutes:D2}:{time.Seconds:D2}";
        }

        public void ShowView()
        {
            this.Show();
        }

        public void HideView()
        {
            this.Hide();
        }

        public void CloseView()
        {
            this.Close();
        }
    }
}
