using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.BL
{
    internal class Ghost
    {
        public int X;
        public int Y;
        public float speed;
        public char previousItem;
        public float deltaChange;
        public string ghostDirection;
        public char ghostCharacter;
        public Grid Grid = new Grid();

        public Ghost(int x, int y, char ghostCharacter, string ghostDirection, char previousItem, float speed, Grid mazeGrid)
        {
            this.X = x;
            this.Y = y;
            this.ghostCharacter = ghostCharacter;
            this.speed = speed;
            this.previousItem = previousItem;
            this.ghostDirection = ghostDirection;
            this.Grid = mazeGrid;
        }

        public void setDirection(string ghostDirection)
        {
            this.ghostDirection = ghostDirection;
        }
        public string getDirection()
        {
            return this.ghostDirection;
        }
        public void removeGhost()
        {
            Cell currentCell = Grid.GetCellAtPosition(X, Y);
            currentCell.SetValue(previousItem);
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }
        public void drawGhost()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(ghostCharacter);
            Grid.Cells[Y, X].Value = ghostCharacter;
        }
        public char getCharacter()
        {
            return ghostCharacter;
        }
        public void ChangeDelta()
        {
            deltaChange += speed;
        }
        public float getDelta()
        {
            return deltaChange;
        }
        public void setDeltaZero()
        {
            deltaChange = 0;
        }
        public void move()
        {
            ChangeDelta();
            if (Math.Floor(getDelta()) == 1)
            {
                if (ghostDirection == "left" || ghostDirection == "right")
                {
                    moveHorizontal();
                }
                else if (ghostDirection == "up" || ghostDirection == "down")
                {
                    moveVertical();
                }
                else if (ghostDirection == "chase")
                {
                    smartEnemy();
                }
                setDeltaZero();
            }
        }
        public void moveHorizontal()
        {
            if (ghostDirection == "left")
            {
                char NextChar = Grid.Cells[Y, X - 1].Value;
                if (NextChar == ' ')
                {
                    X -= 1;
                }
                else
                {
                    ghostDirection = "right";
                }
            }
            else if (ghostDirection == "right")
            {
                char NextChar = Grid.Cells[Y, X + 1].Value;
                if (NextChar == ' ')
                {
                    X += 1;
                }
                else
                {
                    ghostDirection = "left";
                }
            }
        }
        public void moveVertical()
        {
            if (ghostDirection == "up")
            {
                char NextChar = Grid.Cells[Y - 1, X].Value;
                if (NextChar == ' ')
                {
                    Y -= 1;
                }
                else
                {
                    ghostDirection = "down";
                }
            }
            else if (ghostDirection == "down")
            {
                char NextChar = Grid.Cells[Y + 1, X].Value;
                if (NextChar == ' ')
                {
                    Y += 1;
                }
                else
                {
                    ghostDirection = "up";
                }
            }
        }

        public void smartEnemy()
        {
            if (this.X > PacMan.X)
            {
                bool isFlag = false;
                if (!(isFlag))
                {
                    removeGhost();
                    X--;
                    drawGhost();
                }
            }
            if (Y > PacMan.Y)
            {
                bool isFlag = false;
            
                if (!(isFlag))
                {
                    removeGhost();
                    Y--;
                    drawGhost();
                }
            }
            if (X < PacMan.X)
            {
                bool isFlag = false;
                if (!(isFlag))
                {
                    removeGhost();
                    X++;
                    drawGhost();
                }
            }
            if (Y < PacMan.Y)
            {
                bool isFlag = false;
                if (!(isFlag))
                {
                    removeGhost();
                    Y++;
                    drawGhost();
                }
            }
        }
    }
}
