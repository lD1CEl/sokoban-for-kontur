using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class SocobanLevels : Form
    {
        enum Cell {
            Empty,
            Wall,
            Box,
            Done,
            Point,
            Player
        }

        public SocobanLevels()
        {
            InitializeComponent();
        }
    }
}
