using System;

namespace SocobanLevels
{
    partial class LevelSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel levelsPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.levelsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();

            InitializeLevelsPanel();
            InitializeForm();
            LoadBackgroundImage();
            LoadIcon();

            this.ResumeLayout(false);
        }

        private void InitializeLevelsPanel()
        {
            this.levelsPanel.BackColor = System.Drawing.Color.Transparent;
            this.levelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelsPanel.Location = new System.Drawing.Point(0, 0);
            this.levelsPanel.Name = "levelsPanel";
            this.levelsPanel.Padding = new System.Windows.Forms.Padding(50);
            this.levelsPanel.Size = new System.Drawing.Size(1024, 1024);
            this.levelsPanel.TabIndex = 0;
            this.levelsPanel.AutoScroll = true;
            this.levelsPanel.WrapContents = true;
        }

        private void InitializeForm()
        {
            this.ClientSize = new System.Drawing.Size(1024, 1024);
            this.Controls.Add(this.levelsPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "LevelSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор уровня";
        }

        private void LoadBackgroundImage()
        {
            try
            {
                var imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Background.png");
                if (System.IO.File.Exists(imgPath))
                {
                    this.BackgroundImage = System.Drawing.Image.FromFile(imgPath);
                    this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                }
            }
            catch { }
        }

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
