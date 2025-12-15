namespace SocobanLevels
{
    public class LevelFile
    {
        string fName;
        public LevelFile(string fName)
        {
            this.fName = fName;
        }

        public SocobanLevels.Cell[,] Load(int levelNumber)
        {
            SocobanLevels.Cell[,] cell = null;
            string[] lines;
            try
            {
                lines = File.ReadAllLines(fName);
            } catch
            {
                return cell;
            }
            
            int curr = 0;
            int currLevelNumber;
            int width;
            int height;
            while (curr < lines.Length)
            {
                ReadLinesH(lines[curr], out currLevelNumber, out width, out height);

                if (levelNumber == currLevelNumber)
                {
                    cell = new SocobanLevels.Cell[width, height];
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                           cell[x, y] =  LevelSimbolsToCell(lines[curr + 1 + y][x]);
                        }
                    }    

                    break;
                }

                else
                {
                    curr += height + 1;
                }
            }
            
            return cell;
        }
        
        private void ReadLinesH (string lines, out int levelNumber, out int width, out int height)
        {
            string[] parts = lines.Split();
            levelNumber = 0;
            width = 0;
            height = 0;

            if (parts.Length != 3)
            {
                return;
            }
            int.TryParse(parts[0], out levelNumber);
            int.TryParse(parts[1], out width);
            int.TryParse(parts[2], out height);
        }
        
        public void Save(int levelNumber, int width, int height)
        {
            
        }

         private char CellToLevelSimbols(SocobanLevels.Cell cell)
        {
            switch (cell)
            {
                case SocobanLevels.Cell.Wall:
                     return 'W';
                case SocobanLevels.Cell.Box:
                    return 'B';
                case SocobanLevels.Cell.Point:
                    return 'P';
                case SocobanLevels.Cell.Completed:
                    return 'C';
                case SocobanLevels.Cell.SocobanPlayer:
                    return 'S';
                case SocobanLevels.Cell.Empty:
                    return ' ';
                default:
                    return ' ';
            }
        }
        private SocobanLevels.Cell LevelSimbolsToCell(char simbol)
        {
            switch (simbol)
            {
                case 'W':
                     return SocobanLevels.Cell.Wall;
                case 'B':
                    return SocobanLevels.Cell.Box;
                case 'P':
                    return SocobanLevels.Cell.Point;
                case 'C':
                    return SocobanLevels.Cell.Completed;
                case 'S':
                    return SocobanLevels.Cell.SocobanPlayer;
                case ' ':
                    return SocobanLevels.Cell.Empty;
                default:
                    return SocobanLevels.Cell.Empty;
            }
        }
    }

}