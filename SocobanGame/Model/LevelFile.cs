using System;
using System.IO;
using System.Collections.Generic;

namespace SocobanLevels
{
    // Класс для работы с файлом уровней
    public class LevelFile
    {
        private readonly string _fileName;

        // Конструктор
        // <param name="fileName">Путь к файлу с уровнями</param>
        public LevelFile(string fileName)
        {
            _fileName = fileName;
        }

        // Получить список номеров доступных уровней
        // <returns>Список номеров уровней</returns>
        public List<int> GetLevelNumbers()
        {
            var levelNumbers = new List<int>();
            var lines = ReadFileLines();
            if (lines == null) return levelNumbers;

            int currentLineIndex = 0;
            while (currentLineIndex < lines.Length)
            {
                if (TryReadLevelHeader(lines[currentLineIndex], out int currentLevelNumber, out int width, out int height))
                {
                    levelNumbers.Add(currentLevelNumber);
                    currentLineIndex += height + 1;
                }
                else
                {
                    currentLineIndex++;
                }
            }
            return levelNumbers;
        }

        // Загрузить уровень по номеру
        // <param name="levelNumber">Номер уровня</param>
        // <returns>Сетка уровня</returns>
        public Cell[,] Load(int levelNumber)
        {
            var lines = ReadFileLines();
            if (lines == null) return null;

            int currentLineIndex = 0;
            while (currentLineIndex < lines.Length)
            {
                if (TryReadLevelHeader(lines[currentLineIndex], out int currentLevelNumber, out int width, out int height))
                {
                    if (currentLevelNumber == levelNumber)
                    {
                        return ParseLevelGrid(lines, currentLineIndex + 1, width, height);
                    }
                    currentLineIndex += height + 1;
                }
                else
                {
                    currentLineIndex++;
                }
            }
            
            return null;
        }

        // Чтение строк из файла
        // <returns>Массив строк</returns>
        private string[] ReadFileLines()
        {
            try
            {
                return File.ReadAllLines(_fileName);
            }
            catch
            {
                return null;
            }
        }

        // Попытка прочитать заголовок уровня (номер, ширина, высота)
        // <returns>true если заголовок успешно прочитан</returns>
        private bool TryReadLevelHeader(string line, out int levelNumber, out int width, out int height)
        {
            levelNumber = 0;
            width = 0;
            height = 0;
            
            if (string.IsNullOrWhiteSpace(line)) return false;

            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) return false;

            return int.TryParse(parts[0], out levelNumber) &&
                   int.TryParse(parts[1], out width) &&
                   int.TryParse(parts[2], out height);
        }

        // Разбор сетки уровня из строк файла
        private Cell[,] ParseLevelGrid(string[] lines, int startLineIndex, int width, int height)
        {
            var grid = new Cell[width, height];
            for (int y = 0; y < height; y++)
            {
                if (startLineIndex + y >= lines.Length) break;
                
                string line = lines[startLineIndex + y];
                for (int x = 0; x < width; x++)
                {
                    char symbol = (x < line.Length) ? line[x] : ' ';
                    grid[x, y] = LevelSymbolToCell(symbol);
                }
            }
            return grid;
        }

        // Преобразование клетки в символ
         private char CellToLevelSymbol(Cell cell)
        {
            switch (cell)
            {
                case Cell.Wall: return 'W';
                case Cell.Box: return 'B';
                case Cell.Point: return 'P';
                case Cell.Completed: return 'C';
                case Cell.SocobanPlayer: return 'S';
                case Cell.Empty: return ' ';
                default: return ' ';
            }
        }

        // Преобразование символа в клетку
        private Cell LevelSymbolToCell(char symbol)
        {
            switch (symbol)
            {
                case 'W': return Cell.Wall;
                case 'B': return Cell.Box;
                case 'P': return Cell.Point;
                case 'C': return Cell.Completed;
                case 'S': return Cell.SocobanPlayer;
                case ' ': return Cell.Empty;
                default: return Cell.Empty;
            }
        }
    }
}
