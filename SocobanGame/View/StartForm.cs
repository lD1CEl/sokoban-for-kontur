using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class StartForm : Form, IStartView
    {
        private StartPresenter _presenter;

        public event EventHandler StartGameRequested;
        public event EventHandler LeaderboardRequested;

        // Конструктор формы стартового окна
        public StartForm()
        {
            InitializeComponent();
            _presenter = new StartPresenter(this);
            this.Shown += (_, __) => AlignCenters();
            this.Resize += (_, __) => AlignCenters();
        }

        // Обработчик нажатия кнопки начала игры
        private void startButton_Click(object sender, EventArgs e)
        {
            StartGameRequested?.Invoke(this, EventArgs.Empty);
        }

        // Обработчик нажатия кнопки таблицы лидеров
        private void leaderboardButton_Click(object sender, EventArgs e)
        {
            LeaderboardRequested?.Invoke(this, EventArgs.Empty);
        }

        // Закрыть форму
        public void CloseView()
        {
            this.Close();
        }

        // Скрыть форму
        public void HideView()
        {
            this.Hide();
        }

        // Показать форму
        public void ShowView()
        {
            this.Show();
        }

        // Выравнивание элементов по центру
        private void AlignCenters()
        {
            this.titleLabel.AutoSize = true;
            this.titleLabel.PerformLayout();
            int labelCenterX = this.titleLabel.Left + this.titleLabel.Width / 2;
            int newButtonLeft = labelCenterX - this.startButton.Width / 2;
            if (newButtonLeft < 0) newButtonLeft = 0;
            this.startButton.Left = newButtonLeft;
            this.leaderboardButton.Left = newButtonLeft;
        }
    }
}
