using System;
using System.Windows.Forms;

namespace SocobanLevels
{
    public partial class SocobanEditor : Form
    {
        enum Cell {
            Empty,
            Wall,
            Box,
            Done,
            Point,
            Player
        }

        public SocobanEditor()
        {
            InitializeComponent();
            InitializeEditor();
        }

        public void InitializeEditor()
        {
            // Initialization code here
        }
    }
}
using Sistem;

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
            Player }
        public void InitializeEditor()
        {
            // Initialization code here
        }
    }
}