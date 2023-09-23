using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.BL
{
    internal class Grid
    {
        public Cell[,] Cells;

        public Grid()
        {
        
        }
        public Grid(int row, int col)
        {
            Cells = new Cell[row, col];
        }
        
        public Cell getLeftCell(Cell cell)
        {
            if (cell.getY() > 0)
            {
                return (Cells[cell.getY(), cell.getY() - 1]);
            }
            return null;
        }
        public Cell getRightCell(Cell cell)
        {
            if (cell.getY() < Cells.GetLength(1) - 1)
            {
                return (Cells[cell.getX(), cell.getY() + 1]);
            }
            return null;
        }
        public Cell getUpCell(Cell cell)
        {
            if (cell.getY() > 0)
            {
                return (Cells[cell.getY() - 1, cell.getY()]);
            }
            return null;
        }
        public Cell getDownCell(Cell cell)
        {
            if (cell.getY() < Cells.GetLength(0) - 1)
            {
                return Cells[cell.getY() + 1, cell.getY()];
            }
            return null;
        }
        public Cell findPacman()
        {
            foreach (Cell cell in Cells)
            {
                if (cell.isPacmanPresent())
                {
                    return cell;
                }
            }
            return null;
        }
        public Cell findGhostCharacter(char ghostCharacter)
        {
            foreach (Cell cell in Cells)
            {
                if (cell.isGhostPresent(ghostCharacter, Cells))
                {
                    return cell;
                }
            }
            return null;
        }
        public bool isStoppingCondition()
        {
            Cell cell = findPacman();

            if (getLeftCell(cell) != null && getRightCell(cell) != null && getUpCell(cell) != null && getDownCell(cell) != null)
            {
                if (getLeftCell(cell).Value == 'P' || getRightCell(cell).Value == 'P' || getUpCell(cell).Value == 'P' || getDownCell(cell).Value == 'P')
                {
                    return true;
                }
            }
            return false;
        }
        public Cell GetCellAtPosition(int x, int y)
        {
            return Cells[y, x];
        }
        public void DrawGrid()
        {
            for (int row = 0; row < Cells.GetLength(0); row++)
            {
                for (int col = 0; col < Cells.GetLength(1) - 1; col++)
                {
                    Console.Write(Cells[row, col].getValue());
                }
                Console.WriteLine();
            }
        }

        public bool CheckRight()
        {
            Cell cell = Cells[PacMan.Y, PacMan.X + 1];
            char next = cell.Value;
            if (next == ' ')
            {
                return true;
            }
            return false;
        }

        public bool CheckLeft()
        {
            Cell cell = Cells[PacMan.Y, PacMan.X - 1];
            char next = cell.Value;
            if (next == ' ')
            {
                return true;
            }
            return false;
        }

        public bool CheckUp()
        {
            Cell cell = Cells[PacMan.Y - 1, PacMan.X];
            char next = cell.Value;
            if (next == ' ')
            {
                return true;
            }
            return false;
        }
        public bool CheckDown()
        {
            Cell cell = Cells[PacMan.Y + 1, PacMan.X];
            char next = cell.Value;
            if (next == ' ')
            {
                return true;
            }
            return false;
        }
    }
}
