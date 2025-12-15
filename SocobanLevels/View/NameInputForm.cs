using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class NameInputForm : Form, INameInputView
    {
        private NameInputPresenter _presenter;

        public event EventHandler<string> NameEntered;

        public NameInputForm()
        {
            InitializeComponent();
            _presenter = new NameInputPresenter(this);
            this.Shown += (_, __) => AlignControls();
            this.Resize += (_, __) => AlignControls();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                NameEntered?.Invoke(this, name);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите имя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void AlignControls()
        {
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            
            int totalHeight = promptLabel.Height + 20 + nameTextBox.Height + 20 + confirmButton.Height;
            int startY = centerY - (totalHeight / 2);

            promptLabel.Left = centerX - (promptLabel.Width / 2);
            promptLabel.Top = startY;

            nameTextBox.Left = centerX - (nameTextBox.Width / 2);
            nameTextBox.Top = promptLabel.Bottom + 20;

            confirmButton.Left = centerX - (confirmButton.Width / 2);
            confirmButton.Top = nameTextBox.Bottom + 20;
        }
    }
}
