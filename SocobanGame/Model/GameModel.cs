using System;

namespace SocobanLevels
{
    // Модель игры - отвечает за логику игры Сокобан
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

        public int Width 
        { 
            get { return _width; } 
        }
        
        public int Height 
        { 
            get { return _height; } 
        }
        
        public Cell[,] Grid 
        { 
            get { return _grid; } 
        }
        
        public int MoveCount 
        { 
            get { return _moveCount; } 
        }
        
        public TimeSpan ElapsedTime 
        { 
            get { return DateTime.Now - _startTime; } 
        }

        public event EventHandler StateChanged;
        public event EventHandler LevelCompleted;

        // Конструктор модели игры
        // initialGrid - Начальная сетка уровня
        public GameModel(Cell[,] initialGrid)
        {
            _width = initialGrid.GetLength(0);
            _height = initialGrid.GetLength(1);
            _grid = new Cell[_width, _height];
            _targets = new bool[_width, _height];
            _startTime = DateTime.Now;

            InitializeGrid(initialGrid);
        }

        // Инициализация игрового поля и поиск позиции игрока
        // initialGrid - Начальная сетка
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

        // Движение игрока в указанном направлении
        // direction - Направление движения
        public void Move(Direction direction)
        {
            var dx = 0;
            var dy = 0;

            switch (direction)
            {
                case Direction.Up: dy = -1; break;
                case Direction.Down: dy = 1; break;
                case Direction.Left: dx = -1; break;
                case Direction.Right: dx = 1; break;
            }

            var newX = _playerX + dx;
            var newY = _playerY + dy;

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

        // Попытка толкнуть ящик
        // returns: true если ящик удалось толкнуть
        private bool TryPushBox(int boxX, int boxY, int dx, int dy)
        {
            var nextX = boxX + dx;
            var nextY = boxY + dy;

            if (!IsValidPosition(nextX, nextY)) return false;

            Cell nextCell = _grid[nextX, nextY];

            if (nextCell == Cell.Wall || nextCell == Cell.Box || nextCell == Cell.Completed)
            {
                return false;
            }

            _grid[nextX, nextY] = _targets[nextX, nextY] ? Cell.Completed : Cell.Box;
            return true;
        }

        // Перемещение игрока на новую позицию
        private void MovePlayer(int newX, int newY)
        {
            _grid[_playerX, _playerY] = _targets[_playerX, _playerY] ? Cell.Point : Cell.Empty;
            _playerX = newX;
            _playerY = newY;
            _grid[_playerX, _playerY] = Cell.SocobanPlayer;

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        // Проверка корректности позиции
        // returns: true если позиция в пределах поля
        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height;
        }

        // Проверка завершения уровня (все ящики на целях)
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
