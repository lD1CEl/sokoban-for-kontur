using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class LevelSelectionForm : Form, ILevelSelectionView
    {
        private LevelSelectionPresenter _presenter;
        private Image _buttonBackground;

        public event EventHandler<int> LevelSelected;

        private readonly string _playerName;

    // Конструктор формы выбора уровня
        public LevelSelectionForm(string playerName = "Player")
        {
            InitializeComponent();
            LoadButtonBackground();
            _playerName = playerName;
            _presenter = new LevelSelectionPresenter(this, _playerName);
            this.Load += (s, e) => _presenter.Initialize();
        }

    // Загрузка фонового изображения для кнопок
        private void LoadButtonBackground()
        {
            try
            {
                var imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Box.png");
                if (System.IO.File.Exists(imgPath))
                {
                    _buttonBackground = Image.FromFile(imgPath);
                }
            }
            catch { }
        }

    // Установить список доступных уровней
        public void SetLevels(List<int> levels)
        {
            levelsPanel.SuspendLayout();
            levelsPanel.Controls.Clear();

            int buttonSize = 150;
            int margin = 10;
            int itemFullWidth = buttonSize + (margin * 2);
            int panelWidth = levelsPanel.ClientSize.Width;
            
            
            int columns = Math.Max(1, panelWidth / itemFullWidth);
            int totalContentWidth = columns * itemFullWidth;
            int sidePadding = (panelWidth - totalContentWidth) / 2;
            
            if (sidePadding < 0) sidePadding = 0;

            levelsPanel.Padding = new Padding(sidePadding, 50, sidePadding, 50);

            int rows = (int)Math.Ceiling(levels.Count / (double)columns);
            int contentHeight = rows * itemFullWidth + levelsPanel.Padding.Vertical;
            int contentWidth = columns * itemFullWidth + levelsPanel.Padding.Horizontal;
            levelsPanel.AutoScrollMinSize = new Size(
                Math.Max(contentWidth, levelsPanel.ClientSize.Width),
                Math.Max(contentHeight, levelsPanel.ClientSize.Height));

            foreach (var level in levels)
            {
                var btn = new Button();
                btn.Text = level.ToString();
                btn.Size = new Size(buttonSize, buttonSize);
                btn.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                if (_buttonBackground != null)
                {
                    btn.BackgroundImage = _buttonBackground;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Margin = new Padding(margin);
                btn.Tag = level;
                btn.Click += (s, e) => LevelSelected?.Invoke(this, (int)((Button)s).Tag);
                levelsPanel.Controls.Add(btn);
            }

            levelsPanel.ResumeLayout();
        }

    // Показать форму
        public void ShowView()
        {
            this.Show();
        }

    // Скрыть форму
        public void HideView()
        {
            this.Hide();
        }

    // Закрыть форму
        public void CloseView()
        {
            this.Close();
        }
    }
}
