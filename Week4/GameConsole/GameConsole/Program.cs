using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EZInput;
using GameConsole.BL;

namespace GameConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "D:\\UET BS-CS\\SEMESTER 02\\PD\\Week3\\GameConsole\\GameConsole\\obj\\Debug\\maze.txt";

            int score = 0;
            int choice = 0;
            int trigger = 0;
            string directionEnemyOne = "right";

            int bulletCount = 0;
            int bulletCountEnemy = 0;

            char[,] playerMove = {
                { ' ', ' ', 'A', ' ', ' ', ' '},
                { '<', '[', 'A', ']', '>', ' '},
                { ' ', '/', ' ', '\\', ' ', ' '}
                };

            char[,] enemyOneMove = {
                { ' ', ' ', 'M', ' ', ' ', ' '},
                { '<', '/', '|', '\\', '>', ' '},
                { ' ', 'T', ' ', 'T', ' ', ' '}
                };

            char[,] maze = new char[46,162];

            bool gameRunning = true;
            int enemyDead = 1;

            while (choice != 3)
            {
                menu();
                choice = option();

                if (choice == 1)
                {
                    readMazeFromFile(ref path, maze);

                    Player player = new Player();
                    Player p = new Player(player, playerMove, maze);

                    Enemy enemy = new Enemy();
                    Enemy e = new Enemy(directionEnemyOne, enemy, enemyOneMove);

                    Bullet b = new Bullet(player, bulletCount);
                    Bullet2 en = new Bullet2(enemy, bulletCountEnemy);

                    gameRunning = true;
                    printMaze(maze);

                    while (gameRunning)
                    {
                        Thread.Sleep(10);
                        p.printPlayer(player, playerMove);
                        
                        if (EZInput.Keyboard.IsKeyPressed(Key.UpArrow))
                        {
                            p.moveUp(player, playerMove, maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.DownArrow))
                        {
                            p.moveDown(player, playerMove, maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.LeftArrow))
                        {
                            p.moveLeft(player, playerMove, maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.RightArrow))
                        {
                            p.moveRight(player, playerMove, maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.Space))
                        {
                            b.createBullet(player, ref bulletCount);
                        }
                        if(trigger<=0)
                        {
                            if(enemy.healthEnemyOne>0)
                            {
                                en.createBulletEnemy(enemy, ref bulletCountEnemy);
                                trigger = 5;
                            }
                        }
                        trigger--;
                        
                        if(enemyDead == 1)
                        {
                            e.printEnemyOne(ref enemy.enemyOneX, ref enemy.enemyOneY, enemyOneMove);
                            e.moveEnemyOne(ref directionEnemyOne, ref enemy, enemyOneMove);
                            printHealthEnemyOne(ref enemy.healthEnemyOne);
                            playerCollision(player, enemy);
                            collisionWithEnemyBullet(ref bulletCountEnemy, enemy, player);
                            collisionWithPlayerBullet(ref bulletCount, player, ref score, enemy);
                        }
                        if(enemy.healthEnemyOne<=0 && enemyDead == 1)
                        {
                            e.removeCharEnemy(ref enemy.enemyOneY, ref enemy.enemyOneY);
                            enemyDead = 3;
                        }
                        en.moveBulletEnemy(enemy, ref bulletCountEnemy, maze);
                        b.moveBullet(ref bulletCount, maze, player);
                        printCounters(ref player.healthPlayer, ref score);
                    }
                }
            }
        }
        static void menu()
        {
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Options");
            Console.WriteLine("3. Exit");
        }
        static int option()
        {
            int option = 0;
            Console.Write("Enter a choice: ");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static void printMaze(char[,] maze)
        {
            for (int i = 0; i < 46; i++)
            {
                Console.SetCursorPosition(0, 0 + i);
                for (int j = 0; j < 162; j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void printCounters(ref int healthPlayer, ref int score)
        {
            string empty = " ";
            //prints healths of all characters
            Console.SetCursorPosition(165, 3);
            Console.WriteLine("Score: {0} {1}", score, empty);
            Console.SetCursorPosition(165, 5);
            Console.WriteLine("Atlas Health: {0} {1}", healthPlayer, empty);
        }
        static void printHealthEnemyOne(ref int healthEnemyOne)
        {
            string empty = " ";
            Console.SetCursorPosition(165, 7);
            if(healthEnemyOne > 2)
            {
                Console.WriteLine("Mechanoid Health: {0} {1}", healthEnemyOne, empty);
            }
            else
            {
                Console.WriteLine("Mechanoid is Dead!!!");
            }
        }
        static void readMazeFromFile(ref string filePath, char[,] maze)
        {
            if (File.Exists(filePath)) 
            {
                string[] line = File.ReadAllLines(filePath);
                int rows = line.Length;
                int columns = line[0].Length;
                char[,] tempArr = new char[rows, columns];

                for(int i = 0; i < rows; i++)
                {
                    for(int j = 0; j < columns; j++)
                    {
                        tempArr[i,j]= line[i][j];
                        maze[i,j]= tempArr[i,j];
                    }
                }
            }
        }
        static void playerCollision( Player player, Enemy enemy)
        {
            //detects collision of player with enemies and deducts health
            if (((enemy.enemyOneX >= player.playerX && enemy.enemyOneX <= player.playerX + 6) && (enemy.enemyOneY >= player.playerY && enemy.enemyOneY <= player.playerY + 3)) || ((enemy.enemyOneX + 6 >= player.playerX && enemy.enemyOneX + 6 <= player.playerX + 6) && (enemy.enemyOneY >= player.playerY && enemy.enemyOneY <= player.playerY + 3)) || ((enemy.enemyOneX >= player.playerX && enemy.enemyOneX <= player.playerX + 6) && (enemy.enemyOneY + 3 >= player.playerY && enemy.enemyOneY + 3 <= player.playerY + 3)) || ((enemy.enemyOneX + 6 >= player.playerX && enemy.enemyOneX + 6 <= player.playerX + 6) && (enemy.enemyOneY + 3 >= player.playerY && enemy.enemyOneY + 3 <= player.playerY + 3)))
            {
                player.healthPlayer--;
            }
        }
        static void eraseBullet(ref int x, ref int y)
        {
            // removes bullets of player on either sides
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        static void collisionWithPlayerBullet(ref int bulletCount, Player player, ref int score, Enemy enemy)
        {
            for(int i = 0; i < bulletCount;  i++)
            {
                if ((player.playerBullets[i].bulletX + 1 == enemy.enemyOneX && player.playerBullets[i].bulletY == enemy.enemyOneY) || (player.playerBullets[i].bulletX + 1 == enemy.enemyOneX && player.playerBullets[i].bulletY == enemy.enemyOneY + 1) || (player.playerBullets[i].bulletX + 1 == enemy.enemyOneX && player.playerBullets[i].bulletY == enemy.enemyOneY + 2))
                {
                    score++;
                    enemy.healthEnemyOne -= 2;
                    eraseBullet(ref player.playerBullets[i].bulletX, ref player.playerBullets[i].bulletY);
                }
            }
        }
        static void collisionWithEnemyBullet(ref int bulletCountEnemy, Enemy enemy, Player player)
        {
            for (int i = 0; i < bulletCountEnemy; i++)
            {
             
                if ((enemy.enemyBullets[i].bulletXEnemy == player.playerX && enemy.enemyBullets[i].bulletYEnemy + 1 == player.playerY) || (enemy.enemyBullets[i].bulletXEnemy == player.playerX + 1 && enemy.enemyBullets[i].bulletYEnemy + 1 == player.playerY) || (enemy.enemyBullets[i].bulletXEnemy == player.playerX + 2 && enemy.enemyBullets[i].bulletYEnemy == player.playerY) || (enemy.enemyBullets[i].bulletXEnemy == player.playerX + 3 && enemy.enemyBullets[i].bulletYEnemy == player.playerY) || (enemy.enemyBullets[i].bulletXEnemy == player.playerX + 4 && enemy.enemyBullets[i].bulletYEnemy == player.playerY))
                {
                    player.healthPlayer--;
                    eraseBullet(ref enemy.enemyBullets[i].bulletXEnemy, ref enemy.enemyBullets[i].bulletYEnemy);
                } 
            }
        }
    }
}