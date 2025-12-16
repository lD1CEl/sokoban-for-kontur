using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    // Главный класс приложения
    static class Program
    {
        // Точка входа в приложение
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartupContext());
        }
    }

    // Контекст запуска приложения - управляет потоком окон
    public class StartupContext : ApplicationContext
    {
        private readonly NameInputForm _nameForm;
        private StartForm _startForm;

        // Конструктор - инициализирует первое окно ввода имени
        public StartupContext()
        {
            _nameForm = new NameInputForm();
            _nameForm.NameEntered += OnNameEntered;
            _nameForm.FormClosed += (_, __) => TryExitIfNoForms();
            _nameForm.Show();
        }

        // Обработчик ввода имени игрока
        private void OnNameEntered(object sender, string name)
        {
            Session.PlayerName = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
            _nameForm.Close();
            _startForm = new StartForm();
            _startForm.FormClosed += (_, __) => TryExitIfNoForms();
            _startForm.Show();
        }

        // Попытка завершить приложение если все окна закрыты
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
