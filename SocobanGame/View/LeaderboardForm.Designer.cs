using System;
using System.Drawing;
using System.Windows.Forms;

namespace SocobanLevels
{
    // Дизайнерская часть формы таблицы лидеров
    partial class LeaderboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label playerNameLabel;
        private Label completedCountLabel;
        private ListView statsListView;
        private ComboBox levelComboBox;
        private Button closeButton;

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
            this.playerNameLabel = new Label();
            this.completedCountLabel = new Label();
            this.statsListView = new ListView();
            this.closeButton = new Button();
            this.SuspendLayout();

            InitializePlayerNameLabel();
            InitializeCompletedCountLabel();
            InitializeStatsListView();
            InitializeLevelComboBox();
            InitializeCloseButton();
            InitializeForm();
            LoadBackgroundImage();
            LoadIcon();

            this.ResumeLayout(false);
        }

        // Инициализация метки имени игрока
        private void InitializePlayerNameLabel()
        {
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.playerNameLabel.ForeColor = Color.White;
            this.playerNameLabel.BackColor = Color.Transparent;
            this.playerNameLabel.Location = new Point(50, 50);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new Size(200, 30);
            this.playerNameLabel.Text = "Игрок:";
        }

        // Инициализация метки количества пройденных уровней
        private void InitializeCompletedCountLabel()
        {
            this.completedCountLabel.AutoSize = true;
            this.completedCountLabel.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.completedCountLabel.ForeColor = Color.White;
            this.completedCountLabel.BackColor = Color.Transparent;
            this.completedCountLabel.Location = new Point(550, 50);
            this.completedCountLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.completedCountLabel.Name = "completedCountLabel";
            this.completedCountLabel.Size = new Size(200, 25);
            this.completedCountLabel.Text = "Пройдено уровней: 0";
        }

        // Инициализация списка статистики
        private void InitializeStatsListView()
        {
            this.statsListView.Location = new Point(50, 140);
            this.statsListView.Name = "statsListView";
            this.statsListView.Size = new Size(924, 750);
            this.statsListView.TabIndex = 0;
            this.statsListView.View = View.Details;
            this.statsListView.FullRowSelect = true;
            this.statsListView.GridLines = true;
            this.statsListView.Font = new Font("Segoe UI", 12F);

            this.statsListView.Columns.Add("Игрок", 250);
            this.statsListView.Columns.Add("Пройденных уровней", 250);
            this.statsListView.Columns.Add("Шаги", 200);
            this.statsListView.Columns.Add("Время", 200);
        }

        // Инициализация выбора уровня
        private void InitializeLevelComboBox()
        {
            this.levelComboBox = new ComboBox();
            this.levelComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.levelComboBox.Location = new Point(50, 110);
            this.levelComboBox.Size = new Size(200, 30);
            this.levelComboBox.SelectedIndexChanged += (s, e) =>
            {
                if (this.levelComboBox.SelectedItem is int level)
                {
                    LevelSelected?.Invoke(this, level);
                }
            };
            this.Controls.Add(this.levelComboBox);
        }

        // Инициализация кнопки закрытия
        private void InitializeCloseButton()
        {
            this.closeButton.Location = new Point(412, 910);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new Size(200, 60);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Закрыть";
            this.closeButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.closeButton.ForeColor = Color.White;
            this.closeButton.BackColor = Color.FromArgb(100, 0, 0, 0);
            this.closeButton.FlatStyle = FlatStyle.Flat;
            this.closeButton.FlatAppearance.BorderSize = 2;
            this.closeButton.FlatAppearance.BorderColor = Color.White;
            this.closeButton.Click += (s, e) => this.Close();
        }

        // Базовая настройка формы
        private void InitializeForm()
        {
            this.ClientSize = new Size(1024, 1024);
            this.Controls.Add(this.playerNameLabel);
            this.Controls.Add(this.completedCountLabel);
            this.Controls.Add(this.statsListView);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "LeaderboardForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Таблица лидеров";
        }

        // Загрузка фонового изображения
        private void LoadBackgroundImage()
        {
            try
            {
                var imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Background.png");
                if (System.IO.File.Exists(imgPath))
                {
                    this.BackgroundImage = Image.FromFile(imgPath);
                    this.BackgroundImageLayout = ImageLayout.Center;
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
                    this.Icon = new Icon(icoPath);
                }
            }
            catch { }
        }
    }
}
