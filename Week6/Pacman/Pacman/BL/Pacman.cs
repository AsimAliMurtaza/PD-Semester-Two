using EZInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.BL
{
    internal class PacMan
    {
        public static int X;
        public static int Y;
        public Grid Maze = new Grid();
        public PacMan(int x, int y, Grid maze)
        {
            X = x;
            Y = y;
            this.Maze = maze;
        }
        public void DrawPacman()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write('P');
            Maze.Cells[Y, X].Value = 'P';
        }
        public void RemovePacman()
        {
            Cell CurrentCell = Maze.GetCellAtPosition(X, Y);
            CurrentCell.SetValue(' ');
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }

        public void MovePacman(Grid mazeGrid)
        {
            if (EZInput.Keyboard.IsKeyPressed(Key.UpArrow) && mazeGrid.CheckUp())
            {
                Y--;
            }
            if (EZInput.Keyboard.IsKeyPressed(Key.DownArrow) && mazeGrid.CheckDown())
            {
                Y++;
            }
            if (EZInput.Keyboard.IsKeyPressed(Key.RightArrow) && mazeGrid.CheckRight())
            {
                X++;
            }
            if (EZInput.Keyboard.IsKeyPressed(Key.LeftArrow) && mazeGrid.CheckLeft())
            {
                X--;
            }
        }
    }
}
