using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.BL
{
    internal class Cell
    {
        public char Value;
        public int X;
        public int Y;

        public Cell()
        {
        
        }

        public Cell(char value, int x, int y)
        {
            Value = value;
            X = x;
            Y = y;
        }

        public char getValue()
        {
            return Value;
        }

        public void SetValue(char value)
        {
            Value = value;
        }

        public int getX()
        {
            return X;
        }
        public int getY()
        {
            return Y;
        }
        public bool isPacmanPresent()
        {
            return true;
        }
        public bool isGhostPresent(char ghostCharacter, Cell[,] cell)
        {
            if (cell[X, Y].Value == ghostCharacter)
            {
                return true;
            }
            return false;
        }
    }
}
