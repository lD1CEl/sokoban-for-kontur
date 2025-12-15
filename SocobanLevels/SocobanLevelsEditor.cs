using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class SocobanLevels : Form
    {
        public enum Cell {
            Empty,
            Wall,
            Box,
            Completed,
            Point,
            SocobanPlayer
        }

        public SocobanLevels()
        {
            InitializeComponent();
        }

        private void SocobanLevelsLoad(object sender, EventArgs e)
        {
            SocobanLevels.Cell[,] cell;
            LevelFile Level = new LevelFile("levels.txt");
            cell = Level.Load(1);
        }
    }
}
