using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var game = new SocobanLevels();
            game.FormClosed += (s, args) => this.Close();
            game.Show();
            this.Hide();
        }
    }
}
