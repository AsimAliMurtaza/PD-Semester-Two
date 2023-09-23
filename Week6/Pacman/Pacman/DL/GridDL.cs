using Pacman.BL;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.DL
{
    internal class GridDL
    {
        public static void LoadMazeFromFile(string path, Cell[,] Cells)
        {
            string[] lines = File.ReadAllLines(path);
            for (int row = 0; row < lines.Length; row++)
            {
                string line = lines[row];
                for (int col = 0; col < line.Length; col++)
                {
                    Cells[row, col] = new Cell(line[col], row, col);
                }
            }
        }
    }
}
