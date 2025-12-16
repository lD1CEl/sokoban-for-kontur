using System;

namespace SocobanLevels
{
    // Дизайнерская часть формы ввода имени
    partial class NameInputForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label promptLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button confirmButton;

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
            this.promptLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            InitializePromptLabel();
            InitializeNameTextBox();
            InitializeConfirmButton();
            InitializeForm();
            LoadBackgroundImage();
            LoadIcon();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Инициализация подсказки для ввода имени
        private void InitializePromptLabel()
        {
            this.promptLabel.AutoSize = true;
            this.promptLabel.BackColor = System.Drawing.Color.Transparent;
            this.promptLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.promptLabel.Location = new System.Drawing.Point(312, 300);
            this.promptLabel.Name = "promptLabel";
            this.promptLabel.Size = new System.Drawing.Size(400, 45);
            this.promptLabel.TabIndex = 0;
            this.promptLabel.Text = "Введите ваше имя:";
            this.promptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }

        // Инициализация поля ввода имени
        private void InitializeNameTextBox()
        {
            this.nameTextBox.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameTextBox.Location = new System.Drawing.Point(312, 360);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(400, 40);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        }

        // Инициализация кнопки подтверждения
        private void InitializeConfirmButton()
        {
            this.confirmButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.confirmButton.Location = new System.Drawing.Point(412, 420);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(200, 50);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.Text = "Далее";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
        }

        // Базовая настройка формы
        private void InitializeForm()
        {
            this.ClientSize = new System.Drawing.Size(1024, 1024);
            this.Controls.Add(this.promptLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.confirmButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "NameInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ввод имени";
        }

        // Загрузка фонового изображения
        private void LoadBackgroundImage()
        {
            try
            {
                var imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Background.png");
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
