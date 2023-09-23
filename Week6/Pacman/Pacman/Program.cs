using Pacman.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EZInput;
using Pacman.DL;

namespace Pacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "maze.txt";
            bool gameRunning = true;

            Grid mazeGrid = new Grid(20, 60);
            GridDL.LoadMazeFromFile(path, mazeGrid.Cells);

            PacMan pacman = new PacMan(9, 1, mazeGrid);

            Ghost ghostOne = new Ghost(12, 3, 'G', "right", ' ', 0.6F, mazeGrid);
            Ghost ghostTwo = new Ghost(5, 10, 'G', "up", ' ', 0.6F, mazeGrid);
            Ghost ghostThree = new Ghost(8, 8, 'S', "chase", ' ', 0.1F, mazeGrid);

            List<Ghost> Ghosts = new List<Ghost>();
            Ghosts.Add(ghostOne);
            Ghosts.Add(ghostTwo);
            Ghosts.Add(ghostThree);

            mazeGrid.DrawGrid();
            pacman.DrawPacman();
            
            while (gameRunning)
            {
                Thread.Sleep(90);

                pacman.RemovePacman();
                pacman.MovePacman(mazeGrid);
                pacman.DrawPacman();

                foreach (Ghost ghost in Ghosts)
                {
                    ghost.removeGhost();
                    ghost.move();
                    ghost.drawGhost();

                    if (mazeGrid.isStoppingCondition())
                    {
                        gameRunning = false;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}