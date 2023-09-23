using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConsole.BL
{
    public class Player
    {
        public int playerX;
        public int playerY;
        public int healthPlayer;
        public Player()
        {
            playerX = 3;
            playerY = 15;
            healthPlayer = 100;
        }
        public Player(Player player, char[,] playerMove, char[,] maze)
        {
            
        }
        public void moveRight(ref Player player, ref char[,] playerMove, ref char[,] maze)
        {
            //makes player go right
            bool isFlag = false;
            for (int i = 0; i < 3; i++)
            {
                if (maze[player.playerY + i, player.playerX + 6] == '#')
                {
                    isFlag = true;
                }
            }
            if (!(isFlag))
            {
                removeChar(ref player.playerX, ref player.playerY);
                player.playerX++;
                printPlayer(ref player, ref playerMove);
            }
        }
        public void moveLeft(ref Player player, ref char[,] playerMove, ref char[,] maze)
        {
            //makes player go left
            bool isFlag = false;
            for (int i = 0; i < 3; i++)
            {
                if (maze[player.playerY + i, player.playerX - 1] == '#')
                {
                    isFlag = true;
                }
            }
            if (!(isFlag))
            {
                removeChar(ref player.playerX, ref player.playerY);
                player.playerX--;
                printPlayer(ref player, ref playerMove);
            }
        }
        public void moveUp(ref Player player, ref char[,] playerMove, ref char[,] maze)
        {
            //makes player go up
            bool isFlag = false;

            for (int i = 0; i < 6; i++)
            {
                if (maze[player.playerY - 1, player.playerX + i] == '#')
                {
                    isFlag = true;
                }
            }
            if (!(isFlag))
            {
                removeChar(ref player.playerX, ref player.playerY);
                player.playerY--;
                printPlayer(ref player, ref playerMove);
            }
        }
        public void moveDown(ref Player player, ref char[,] playerMove, ref char[,] maze)
        {
            //makes player go down
            bool isFlag = false;
            for (int i = 0; i < 6; i++)
            {
                if (maze[player.playerY + 3, player.playerX + i] == '#')
                {
                    isFlag = true;
                }
            }
            if (!(isFlag))
            {
                removeChar(ref player.playerX, ref player.playerY);
                player.playerY++;
                printPlayer(ref player, ref playerMove);
            }
        }
        public void printPlayer(ref Player player, ref char[,] playerMove)
        {
            //prints player
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(player.playerX, player.playerY + i);
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(playerMove[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void removeChar(ref int x, ref int y)
        {
            //removes player and enemies
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
    public class Enemy
    {
        public int enemyOneX;
        public int enemyOneY;
        public int healthEnemyOne;

        public Enemy()
        {
            enemyOneX = 40;
            enemyOneY = 3;
            healthEnemyOne = 50;
        }
        public Enemy(string directionEnemyOne, Enemy enemy, char[,] enemyOneMove)
        {

        }
        public void moveEnemyOne(ref string directionEnemyOne, ref Enemy enemy, ref char[,] enemyOneMove)
        {
            //moves enemy one depending on direction
            if (directionEnemyOne == "right") // enemy one movement
            {
                if (enemy.enemyOneX >= 40)
                {
                    directionEnemyOne = "left";
                }
                removeCharEnemy(ref enemy.enemyOneX, ref enemy.enemyOneY);
                enemy.enemyOneX++;
                printEnemyOne(ref enemy.enemyOneX, ref enemy.enemyOneY, ref enemyOneMove);
            }
            if (directionEnemyOne == "left")
            {
                if (enemy.enemyOneX <= 10)
                {
                    directionEnemyOne = "right";
                }
                removeCharEnemy(ref enemy.enemyOneX, ref enemy.enemyOneY);
                enemy.enemyOneX--;
                printEnemyOne(ref enemy.enemyOneX, ref enemy.enemyOneY, ref enemyOneMove);
            }
        }
        public void printEnemyOne(ref int x, ref int y, ref char[,] enemyOneMove)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(enemyOneMove[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void removeCharEnemy(ref int x, ref int y)
        {
            //removes player and enemies
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}