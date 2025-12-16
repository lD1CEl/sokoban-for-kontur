using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SocobanLevels
{
    public partial class SocobanLevels : Form, IMainView
    {
        private MainPresenter _presenter;
        private TableLayoutPanel _gridPanel;
        private Dictionary<Cell, Image> _images;
        private Label _moveCounterLabel;
        private int _levelGridWidth = 0;
        private int _levelGridHeight = 0;
        private int _baseCellSize = 32;
        private bool _resizingProgrammatically = false;

        public event EventHandler ViewLoaded;
        public event EventHandler ViewClosed;
        public event EventHandler<Direction> InputReceived;

        private readonly string _playerName;

    // Конструктор главного окна игры
        public SocobanLevels(int levelNumber, string playerName = "Player")
        {
            InitializeComponent();
            InitializeGridPanel();
            InitializeMoveCounter();
            LoadImages();
            _playerName = playerName;
            _presenter = new MainPresenter(this, levelNumber, _playerName);
            this.KeyPreview = true;
        }

    // Инициализация панели для отображения игровой сетки
        private void InitializeGridPanel()
        {
            _gridPanel = new TableLayoutPanel();
            _gridPanel.Dock = DockStyle.Fill;
            _gridPanel.BackColor = Color.White;
            _gridPanel.Margin = new Padding(0);
            this.Controls.Add(_gridPanel);
        }

    // Инициализация счетчика ходов
        private void InitializeMoveCounter()
        {
            _moveCounterLabel = new Label();
            _moveCounterLabel.AutoSize = true;
            _moveCounterLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            _moveCounterLabel.ForeColor = Color.White;
            _moveCounterLabel.BackColor = Color.FromArgb(180, 0, 0, 0);
            _moveCounterLabel.Padding = new Padding(10, 5, 10, 5);
            _moveCounterLabel.Text = "????: 0";
            _moveCounterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _moveCounterLabel.Location = new Point(this.ClientSize.Width - _moveCounterLabel.PreferredWidth - 10, 10);
            this.Controls.Add(_moveCounterLabel);
            _moveCounterLabel.BringToFront();
        }

    // Загрузка изображений для игровых элементов
        private void LoadImages()
        {
            _images = new Dictionary<Cell, Image>();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            
            _images[Cell.Wall] = LoadImage(Path.Combine(basePath, "Wall.png"));
            _images[Cell.Box] = LoadImage(Path.Combine(basePath, "Box.png"));
            _images[Cell.Point] = LoadImage(Path.Combine(basePath, "Point.png"));
            _images[Cell.Completed] = LoadImage(Path.Combine(basePath, "Completed.png"));
            _images[Cell.SocobanPlayer] = LoadImage(Path.Combine(basePath, "SocobanPlayer.png"));
            _images[Cell.Empty] = LoadImage(Path.Combine(basePath, "Empty.png"));
        }

    // Загрузка одного изображения по пути
        private Image LoadImage(string path)
        {
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return null;
        }

    // Обработка нажатий клавиш для управления игрой
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up: InputReceived?.Invoke(this, Direction.Up); return true;
                case Keys.Down: InputReceived?.Invoke(this, Direction.Down); return true;
                case Keys.Left: InputReceived?.Invoke(this, Direction.Left); return true;
                case Keys.Right: InputReceived?.Invoke(this, Direction.Right); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    // Обработчик события загрузки формы
        private void SocobanLevelsLoad(object sender, EventArgs e)
        {
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

    // Обработчик закрытия формы
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ViewClosed?.Invoke(this, EventArgs.Empty);
        }

    // Отрисовка игрового уровня на экране
        public void RenderLevel(Cell[,] grid, int width, int height)
        {
            int maxWidth = 1920;
            int maxHeight = 1080;
            int minCellSize = 32;
            
            double targetAspect = 16.0 / 10.0;
            double levelAspect = (double)width / height;
            
            int paddedWidth = width;
            int paddedHeight = height;
            
            if (Math.Abs(levelAspect - targetAspect) > 0.01)
            {
                if (levelAspect < targetAspect)
                {
                    paddedWidth = (int)Math.Ceiling(height * targetAspect);
                }
                else
                {
                    paddedHeight = (int)Math.Ceiling(width / targetAspect);
                }
            }
            
            int cellSize = Math.Min(maxWidth / paddedWidth, maxHeight / paddedHeight);
            cellSize = Math.Max(cellSize, minCellSize);

            int newWidth = paddedWidth * cellSize;
            int newHeight = paddedHeight * cellSize;

            if (this.ClientSize.Width != newWidth || this.ClientSize.Height != newHeight)
            {
                _resizingProgrammatically = true;
                this.ClientSize = new Size(newWidth, newHeight);
                this.CenterToScreen();
                _resizingProgrammatically = false;
            }

            _levelGridWidth = paddedWidth;
            _levelGridHeight = paddedHeight;
            _baseCellSize = cellSize;
            
            try
            {
                this.MinimumSize = new Size(paddedWidth * minCellSize, paddedHeight * minCellSize);
            }
            catch { }

            _gridPanel.SuspendLayout();
            
            paddedWidth = _levelGridWidth;
            paddedHeight = _levelGridHeight;
            int offsetX = (paddedWidth - width) / 2;
            int offsetY = (paddedHeight - height) / 2;
            
            if (_gridPanel.RowCount != paddedHeight || _gridPanel.ColumnCount != paddedWidth)
            {
                _gridPanel.Controls.Clear();
                _gridPanel.RowStyles.Clear();
                _gridPanel.ColumnStyles.Clear();
                
                _gridPanel.RowCount = paddedHeight;
                _gridPanel.ColumnCount = paddedWidth;

                float percentWidth = 100f / paddedWidth;
                float percentHeight = 100f / paddedHeight;

                for (int i = 0; i < paddedWidth; i++)
                {
                    _gridPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percentWidth));
                }
                for (int i = 0; i < paddedHeight; i++)
                {
                    _gridPanel.RowStyles.Add(new RowStyle(SizeType.Percent, percentHeight));
                }

                for (int y = 0; y < paddedHeight; y++)
                {
                    for (int x = 0; x < paddedWidth; x++)
                    {
                        var pictureBox = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Margin = new Padding(0)
                        };
                        _gridPanel.Controls.Add(pictureBox, x, y);
                    }
                }
            }

            for (int y = 0; y < paddedHeight; y++)
            {
                for (int x = 0; x < paddedWidth; x++)
                {
                    var control = _gridPanel.GetControlFromPosition(x, y) as PictureBox;
                    if (control != null)
                    {
                        int levelX = x - offsetX;
                        int levelY = y - offsetY;
                        
                        if (levelX >= 0 && levelX < width && levelY >= 0 && levelY < height)
                        {
                            var cell = grid[levelX, levelY];
                            control.Image = _images.ContainsKey(cell) ? _images[cell] : null;
                        }
                        else
                        {
                            control.Image = _images[Cell.Empty];
                        }
                    }
                }
            }
            
                _gridPanel.ResumeLayout();
            }

            // Обработчик изменения размера окна
            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                if (_resizingProgrammatically) return;
                if (_levelGridWidth <= 0 || _levelGridHeight <= 0) return;

                
                int maxWidth = 1920;
                int maxHeight = 1080;
                int minCellSize = 32;

                int availableW = Math.Min(this.ClientSize.Width, maxWidth);
                int availableH = Math.Min(this.ClientSize.Height, maxHeight);

                int cellSizeByW = Math.Max(minCellSize, availableW / _levelGridWidth);
                int cellSizeByH = Math.Max(minCellSize, availableH / _levelGridHeight);

                int cellSize = Math.Min(cellSizeByW, cellSizeByH);

                int targetW = _levelGridWidth * cellSize;
                int targetH = _levelGridHeight * cellSize;

                if (this.ClientSize.Width != targetW || this.ClientSize.Height != targetH)
                {
                    _resizingProgrammatically = true;
                    this.ClientSize = new Size(targetW, targetH);
                    _resizingProgrammatically = false;
                }
            }

    // Показать сообщение о завершении уровня
        public void ShowLevelCompleted()
        {
            MessageBox.Show("??????? ???????!", "???????????", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    // Показать сообщение об ошибке
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    // Закрыть форму
        public void CloseView()
        {
            this.Close();
        }

    // Показать форму
        public void ShowView()
        {
            this.Show();
        }

    // Обновить счетчик ходов
        public void UpdateMoveCounter(int moves)
        {
            if (_moveCounterLabel != null)
            {
                _moveCounterLabel.Text = $"????: {moves}";
            }
        }
    }
}

