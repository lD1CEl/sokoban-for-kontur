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

        public LevelSelectionForm()
        {
            InitializeComponent();
            LoadButtonBackground();
            _presenter = new LevelSelectionPresenter(this);
            this.Load += (s, e) => _presenter.Initialize();
        }

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

        public void SetLevels(List<int> levels)
        {
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
