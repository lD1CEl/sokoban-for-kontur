using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class StartForm : Form, IStartView
    {
        private StartPresenter _presenter;

        public event EventHandler StartGameRequested;

        public StartForm()
        {
            InitializeComponent();
            _presenter = new StartPresenter(this);
            this.Shown += (_, __) => AlignCenters();
            this.Resize += (_, __) => AlignCenters();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartGameRequested?.Invoke(this, EventArgs.Empty);
        }

        public void CloseView()
        {
            this.Close();
        }

        public void HideView()
        {
            this.Hide();
        }

        public void ShowView()
        {
            this.Show();
        }

        private void AlignCenters()
        {
            this.titleLabel.AutoSize = true;
            this.titleLabel.PerformLayout();
            int labelCenterX = this.titleLabel.Left + this.titleLabel.Width / 2;
            int newButtonLeft = labelCenterX - this.startButton.Width / 2;
            if (newButtonLeft < 0) newButtonLeft = 0;
            this.startButton.Left = newButtonLeft;
        }
    }
}
