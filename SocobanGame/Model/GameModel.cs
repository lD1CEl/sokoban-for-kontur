using System;

namespace SocobanLevels
{
    public class GameModel
    {
        private readonly int _width;
        private readonly int _height;
        private Cell[,] _grid;
        private bool[,] _targets;
        private int _playerX;
        private int _playerY;
        private int _moveCount;
        private DateTime _startTime;

        public int Width => _width;
        public int Height => _height;
        public Cell[,] Grid => _grid;
        public int MoveCount => _moveCount;
        public TimeSpan ElapsedTime => DateTime.Now - _startTime;

        public event EventHandler StateChanged;
        public event EventHandler LevelCompleted;

        public GameModel(Cell[,] initialGrid)
        {
            _width = initialGrid.GetLength(0);
            _height = initialGrid.GetLength(1);
            _grid = new Cell[_width, _height];
            _targets = new bool[_width, _height];
            _startTime = DateTime.Now;

            InitializeGrid(initialGrid);
        }

        private void InitializeGrid(Cell[,] initialGrid)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Cell cell = initialGrid[x, y];
                    _grid[x, y] = cell;

                    if (cell == Cell.Point || cell == Cell.Completed)
                    {
                        _targets[x, y] = true;
                    }

                    if (cell == Cell.SocobanPlayer)
                    {
                        _playerX = x;
                        _playerY = y;
                    }
                }
            }
        }

        public void Move(Direction direction)
        {
            int dx = 0;
            int dy = 0;

            switch (direction)
            {
                case Direction.Up: dy = -1; break;
                case Direction.Down: dy = 1; break;
                case Direction.Left: dx = -1; break;
                case Direction.Right: dx = 1; break;
            }

            int newX = _playerX + dx;
            int newY = _playerY + dy;

            if (!IsValidPosition(newX, newY)) return;

            Cell targetCell = _grid[newX, newY];

            if (targetCell == Cell.Wall) return;

            if (targetCell == Cell.Box || targetCell == Cell.Completed)
            {
                if (TryPushBox(newX, newY, dx, dy))
                {
                    _moveCount++;
                    MovePlayer(newX, newY);
                    CheckCompletion();
                }
            }
            else
            {
                _moveCount++;
                MovePlayer(newX, newY);
            }
        }

        private bool TryPushBox(int boxX, int boxY, int dx, int dy)
        {
            int nextX = boxX + dx;
            int nextY = boxY + dy;

            if (!IsValidPosition(nextX, nextY)) return false;

            Cell nextCell = _grid[nextX, nextY];

            if (nextCell == Cell.Wall || nextCell == Cell.Box || nextCell == Cell.Completed)
            {
                return false;
            }

            _grid[nextX, nextY] = _targets[nextX, nextY] ? Cell.Completed : Cell.Box;
            return true;
        }

        private void MovePlayer(int newX, int newY)
        {
            _grid[_playerX, _playerY] = _targets[_playerX, _playerY] ? Cell.Point : Cell.Empty;
            _playerX = newX;
            _playerY = newY;
            _grid[_playerX, _playerY] = Cell.SocobanPlayer;

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height;
        }

        private void CheckCompletion()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_targets[x, y] && _grid[x, y] != Cell.Completed && _grid[x, y] != Cell.SocobanPlayer)
                    {
                        if (_grid[x, y] != Cell.Completed) return;
                    }
                }
            }
            LevelCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
