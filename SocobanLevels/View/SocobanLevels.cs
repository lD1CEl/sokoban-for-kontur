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

        public event EventHandler ViewLoaded;
        public event EventHandler ViewClosed;
        public event EventHandler<Direction> InputReceived;

        public SocobanLevels(int levelNumber)
        {
            InitializeComponent();
            InitializeGridPanel();
            LoadImages();
            _presenter = new MainPresenter(this, levelNumber);
            this.KeyPreview = true;
        }

        private void InitializeGridPanel()
        {
            _gridPanel = new TableLayoutPanel();
            _gridPanel.Dock = DockStyle.Fill;
            _gridPanel.BackColor = Color.White;
            _gridPanel.Margin = new Padding(0);
            this.Controls.Add(_gridPanel);
        }

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

        private Image LoadImage(string path)
        {
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return null;
        }

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

        private void SocobanLevelsLoad(object sender, EventArgs e)
        {
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ViewClosed?.Invoke(this, EventArgs.Empty);
        }

        public void RenderLevel(Cell[,] grid, int width, int height)
        {
            
            int maxDimension = 1024;
            int minCellSize = 32;
            int cellSize = Math.Min(maxDimension / width, maxDimension / height);
            cellSize = Math.Max(cellSize, minCellSize);

            int newWidth = width * cellSize;
            int newHeight = height * cellSize;

            if (this.ClientSize.Width != newWidth || this.ClientSize.Height != newHeight)
            {
                this.ClientSize = new Size(newWidth, newHeight);
                this.CenterToScreen();
            }

            _gridPanel.SuspendLayout();
            
            if (_gridPanel.RowCount != height || _gridPanel.ColumnCount != width)
            {
                _gridPanel.Controls.Clear();
                _gridPanel.RowStyles.Clear();
                _gridPanel.ColumnStyles.Clear();
                
                _gridPanel.RowCount = height;
                _gridPanel.ColumnCount = width;

                float percentWidth = 100f / width;
                float percentHeight = 100f / height;

                for (int i = 0; i < width; i++)
                {
                    _gridPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percentWidth));
                }
                for (int i = 0; i < height; i++)
                {
                    _gridPanel.RowStyles.Add(new RowStyle(SizeType.Percent, percentHeight));
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
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

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var cell = grid[x, y];
                    var control = _gridPanel.GetControlFromPosition(x, y) as PictureBox;
                    if (control != null)
                    {
                        control.Image = _images.ContainsKey(cell) ? _images[cell] : null;
                    }
                }
            }
            
            _gridPanel.ResumeLayout();
        }

        public void ShowLevelCompleted()
        {
            MessageBox.Show("Уровень пройден!", "Поздравляем", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseView()
        {
            this.Close();
        }

        public void ShowView()
        {
            this.Show();
        }
    }
}
