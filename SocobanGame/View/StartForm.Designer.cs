using System;

namespace SocobanLevels
{
    // Дизайнерская часть формы стартового экрана
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button leaderboardButton;
        private System.Windows.Forms.Label titleLabel;

        // Освобождение ресурсов формы
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Инициализация компонентов формы
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.startButton = new System.Windows.Forms.Button();
            this.leaderboardButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            InitializeTitleLabel();
            InitializeStartButton();
            InitializeLeaderboardButton();
            InitializeForm();
            LoadBackgroundImage();
            LoadIcon();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Инициализация заголовка
        private void InitializeTitleLabel()
        {
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(460, 70);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(300, 86);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Socoban";
        }

        // Инициализация кнопки начала игры
        private void InitializeStartButton()
        {
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startButton.Location = new System.Drawing.Point(550, 250);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(300, 80);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Начать игру";
            this.startButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
        }

        // Инициализация кнопки открытия таблицы лидеров
        private void InitializeLeaderboardButton()
        {
            this.leaderboardButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.leaderboardButton.Location = new System.Drawing.Point(550, 360);
            this.leaderboardButton.Name = "leaderboardButton";
            this.leaderboardButton.Size = new System.Drawing.Size(300, 80);
            this.leaderboardButton.TabIndex = 3;
            this.leaderboardButton.Text = "Таблица лидеров";
            this.leaderboardButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.leaderboardButton.UseVisualStyleBackColor = true;
            this.leaderboardButton.Click += new System.EventHandler(this.leaderboardButton_Click);
        }

        // Базовая настройка формы
        private void InitializeForm()
        {
            this.ClientSize = new System.Drawing.Size(1024, 1024);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.leaderboardButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Socoban";
        }

        // Загрузка фонового изображения
        private void LoadBackgroundImage()
        {
            try
            {
                var imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StartMenu.png");
                if (System.IO.File.Exists(imgPath))
                {
                    this.BackgroundImage = System.Drawing.Image.FromFile(imgPath);
                    this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
            }
            catch { }
        }

        // Загрузка иконки приложения
        private void LoadIcon()
        {
            try
            {
                var icoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wall.ico");
                if (System.IO.File.Exists(icoPath))
                {
                    this.Icon = new System.Drawing.Icon(icoPath);
                }
            }
            catch { }
        }
    }
}
