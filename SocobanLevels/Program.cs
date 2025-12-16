using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartupContext());
        }
    }

    public class StartupContext : ApplicationContext
    {
        private readonly NameInputForm _nameForm;
        private StartForm _startForm;

        public StartupContext()
        {
            _nameForm = new NameInputForm();
            _nameForm.NameEntered += OnNameEntered;
            _nameForm.FormClosed += (_, __) => TryExitIfNoForms();
            _nameForm.Show();
        }

        private void OnNameEntered(object sender, string name)
        {
            Session.PlayerName = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
            _nameForm.Close();
            _startForm = new StartForm();
            _startForm.FormClosed += (_, __) => TryExitIfNoForms();
            _startForm.Show();
        }

        private void TryExitIfNoForms()
        {
            if (_startForm == null || _startForm.IsDisposed)
            {
                if (_nameForm == null || _nameForm.IsDisposed)
                {
                    ExitThread();
                }
            }
        }
    }
}
