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
        public List<Bullet> playerBullets = new List<Bullet>();
        public Player()
        {
            playerX = 3;
            playerY = 15;
            healthPlayer = 100;
        }
        public Player(Player player, char[,] playerMove, char[,] maze)
        {
            
        }
        public void moveRight(Player player, char[,] playerMove, char[,] maze)
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
                printPlayer(player, playerMove);
            }
        }
        public void moveLeft(Player player, char[,] playerMove, char[,] maze)
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
                printPlayer(player, playerMove);
            }
        }
        public void moveUp(Player player, char[,] playerMove, char[,] maze)
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
                printPlayer(player, playerMove);
            }
        }
        public void moveDown(Player player, char[,] playerMove, char[,] maze)
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
                printPlayer(player, playerMove);
            }
        }
        public void printPlayer( Player player, char[,] playerMove)
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
        public List<Bullet2> enemyBullets = new List<Bullet2>();

        public Enemy()
        {
            enemyOneX = 40;
            enemyOneY = 3;
            healthEnemyOne = 50;
        }
        public Enemy(string directionEnemyOne, Enemy enemy, char[,] enemyOneMove)
        {

        }
        public void moveEnemyOne(ref string directionEnemyOne, ref Enemy enemy, char[,] enemyOneMove)
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
                printEnemyOne(ref enemy.enemyOneX, ref enemy.enemyOneY, enemyOneMove);
            }
            if (directionEnemyOne == "left")
            {
                if (enemy.enemyOneX <= 10)
                {
                    directionEnemyOne = "right";
                }
                removeCharEnemy(ref enemy.enemyOneX, ref enemy.enemyOneY);
                enemy.enemyOneX--;
                printEnemyOne(ref enemy.enemyOneX, ref enemy.enemyOneY, enemyOneMove);
            }
        }
        public void printEnemyOne(ref int x, ref int y, char[,] enemyOneMove)
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