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
            string path = "C:\\Users\\Asim Ali\\source\\repos\\GameConsole\\GameConsole\\obj\\Debug\\maze.txt";

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

            while (choice != 3)
            {
                Player player = new Player();
                Enemy enemy = new Enemy();
                player.playerX = 3;
                player.playerY = 15;
                player.healthPlayer = 100;
                enemy.enemyOneX = 40;
                enemy.enemyOneY = 3;
                enemy.healthEnemyOne = 200;


                List<Bullet> playerBullets = new List<Bullet>();
                List<Bullet2> enemyBullets = new List<Bullet2>();


                menu();
                choice = option();

                if (choice == 1)
                {
                    readMazeFromFile(ref path, ref maze);
                    gameRunning = true;
                    printMaze(ref maze);

                    while (gameRunning)
                    {
                        Thread.Sleep(10);
                        printPlayer(ref player, ref playerMove);
                        
                        if (EZInput.Keyboard.IsKeyPressed(Key.UpArrow))
                        {
                            moveUp(ref player, ref playerMove, ref maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.DownArrow))
                        {
                            moveDown(ref player, ref playerMove, ref maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.LeftArrow))
                        {
                            moveLeft(ref player, ref playerMove, ref maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.RightArrow))
                        {
                            moveRight(ref player, ref playerMove, ref maze);
                        }
                        if (EZInput.Keyboard.IsKeyPressed(Key.Space))
                        {
                            createBullet(ref player, ref playerBullets, ref bulletCount);
                        }
                        if(trigger<=0)
                        {
                            if(enemy.healthEnemyOne>=0)
                            {
                                createBulletEnemy(ref enemy, ref enemyBullets, ref bulletCountEnemy);
                                trigger = 5;
                            }
                        }
                        trigger--;

                        if(enemy.healthEnemyOne>0)
                        {
                            printEnemyOne(ref enemy.enemyOneX, ref enemy.enemyOneY, ref enemyOneMove);
                            moveEnemyOne(ref directionEnemyOne, ref enemy, ref enemyOneMove);
                            printHealthEnemyOne(ref enemy.healthEnemyOne);
                            playerCollision(ref player, ref enemy);
                            collisionWithEnemyBullet(ref bulletCountEnemy, ref enemyBullets, ref player);
                            collisionWithPlayerBullet(ref bulletCount, ref playerBullets, ref score, ref enemy);
                        }
                        else if(enemy.healthEnemyOne <=0)
                        {
                            removeCharEnemy(ref enemy.enemyOneY, ref enemy.enemyOneY);
                        }
                        moveBulletEnemy(ref enemyBullets, ref bulletCountEnemy, ref maze);
                        moveBullet(ref playerBullets, ref bulletCount, ref maze, ref player);
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
        static void printMaze(ref char[,] maze)
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
            Console.WriteLine("Mechanoid Health: {0} {1}", healthEnemyOne, empty);
        }
        static void printPlayer(ref Player player, ref char[,] playerMove)
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
        static void printEnemyOne(ref int x, ref int y, ref char[,] enemyOneMove)
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

        static void moveUp(ref Player player, ref char[,] playerMove, ref char[,] maze)
        {
            //makes player go up
            bool isFlag = false;
            
            for(int i = 0;i < 6;i++)
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
        static void moveDown(ref Player player, ref char[,] playerMove, ref char[,] maze)
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
        static void moveRight(ref Player player, ref char[,] playerMove, ref char[,] maze)
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
        static void moveLeft(ref Player player, ref char[,] playerMove, ref char[,] maze)
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
        static void moveEnemyOne(ref string directionEnemyOne, ref Enemy enemy, ref char[,] enemyOneMove)
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
        static void removeChar(ref int x, ref int y)
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
        static void removeCharEnemy(ref int x, ref int y)
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

        static void printBullet(ref int x, ref int y)
        {
            // prints bullets of player on either sides
            Console.SetCursorPosition(x, y);
            Console.Write(".");
        }
        static void eraseBullet(ref int x, ref int y)
        {
            // removes bullets of player on either sides
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        static void createBullet(ref Player player, ref List<Bullet> playerBullets, ref int bulletCount)
        {
            Bullet bullet = new Bullet();   
        // creates bullets of player on right side
            bullet.bulletX = player.playerX + 6;
            bullet.bulletY = player.playerY + 1;
            playerBullets.Add(bullet);
            Console.SetCursorPosition(player.playerX + 6, player.playerY + 1);
            Console.Write(".");
            bulletCount++;
        }
        static void moveBullet(ref List<Bullet> playerBullets, ref int bulletCount, ref char[,] maze, ref Player player)
        {
            // moves bullets of player on right side
            for (int i = 0; i < bulletCount; i++)
            {
                if (maze[playerBullets[i].bulletY, playerBullets[i].bulletX + 1] == '#')
                {
                    eraseBullet(ref playerBullets[i].bulletX, ref playerBullets[i].bulletY);
                }
                else
                {
                    eraseBullet(ref playerBullets[i].bulletX, ref playerBullets[i].bulletY);
                    playerBullets[i].bulletX = (playerBullets[i].bulletX + 1);
                    printBullet(ref playerBullets[i].bulletX, ref playerBullets[i].bulletY);
                }
            }
        }

        static void createBulletEnemy(ref Enemy enemy, ref List<Bullet2> enemyBullets, ref int bulletCountEnemy)
        {
            Bullet2 bullet = new Bullet2();
            // creates bullets of enemy one
            bullet.bulletXEnemy = enemy.enemyOneX;
            bullet.bulletYEnemy = enemy.enemyOneY + 3;
            enemyBullets.Add(bullet);
            Console.SetCursorPosition(enemy.enemyOneX, enemy.enemyOneY + 3);
            Console.Write("+");
            bulletCountEnemy++;
        }
        static void moveBulletEnemy(ref List<Bullet2> enemyBullets, ref int bulletCountEnemy, ref char[,] maze)
        {
            // moves bullets of enemy one
            for (int i = 0; i < bulletCountEnemy; i++)
            {
                if (maze[enemyBullets[i].bulletYEnemy + 1, enemyBullets[i].bulletXEnemy] == '#')
                {
                    eraseBullet(ref enemyBullets[i].bulletXEnemy,ref enemyBullets[i].bulletYEnemy);
                }
                else
                {
                    eraseBullet(ref enemyBullets[i].bulletXEnemy, ref enemyBullets[i].bulletYEnemy);
                    enemyBullets[i].bulletYEnemy = (enemyBullets[i].bulletYEnemy + 1);
                    printBulletEnemy(ref enemyBullets[i].bulletXEnemy, ref enemyBullets[i].bulletYEnemy);
                }
            }
        }
        static void printBulletEnemy(ref int x, ref int y)
        {
            // prints bullets of enemy one
            Console.SetCursorPosition(x, y);
            Console.Write("+");
        }
        static void readMazeFromFile(ref string filePath, ref char[,] maze)
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

        static void playerCollision(ref Player player, ref Enemy enemy)
        {
            //detects collision of player with enemies and deducts health
            if (((enemy.enemyOneX >= player.playerX && enemy.enemyOneX <= player.playerX + 6) && (enemy.enemyOneY >= player.playerY && enemy.enemyOneY <= player.playerY + 3)) || ((enemy.enemyOneX + 6 >= player.playerX && enemy.enemyOneX + 6 <= player.playerX + 6) && (enemy.enemyOneY >= player.playerY && enemy.enemyOneY <= player.playerY + 3)) || ((enemy.enemyOneX >= player.playerX && enemy.enemyOneX <= player.playerX + 6) && (enemy.enemyOneY + 3 >= player.playerY && enemy.enemyOneY + 3 <= player.playerY + 3)) || ((enemy.enemyOneX + 6 >= player.playerX && enemy.enemyOneX + 6 <= player.playerX + 6) && (enemy.enemyOneY + 3 >= player.playerY && enemy.enemyOneY + 3 <= player.playerY + 3)))
            {
                player.healthPlayer--;
            }
        }
        static void collisionWithPlayerBullet(ref int bulletCount, ref List<Bullet> playerBullets, ref int score, ref Enemy enemy)
        {
            for(int i = 0; i < bulletCount;  i++)
            {
                if ((playerBullets[i].bulletX + 1 == enemy.enemyOneX && playerBullets[i].bulletY == enemy.enemyOneY) || (playerBullets[i].bulletX + 1 == enemy.enemyOneX && playerBullets[i].bulletY == enemy.enemyOneY + 1) || (playerBullets[i].bulletX + 1 == enemy.enemyOneX && playerBullets[i].bulletY == enemy.enemyOneY + 2))
                {
                    score++;
                    enemy.healthEnemyOne -= 50;
                    eraseBullet(ref playerBullets[i].bulletX, ref playerBullets[i].bulletY);
                }
            }
        }
        static void collisionWithEnemyBullet(ref int bulletCountEnemy, ref List<Bullet2> enemyBullets, ref Player player)
        {
            for (int i = 0; i < bulletCountEnemy; i++)
            {
             
                if ((enemyBullets[i].bulletXEnemy == player.playerX && enemyBullets[i].bulletYEnemy + 1 == player.playerY) || (enemyBullets[i].bulletXEnemy == player.playerX + 1 && enemyBullets[i].bulletYEnemy + 1 == player.playerY) || (enemyBullets[i].bulletXEnemy == player.playerX + 2 && enemyBullets[i].bulletYEnemy == player.playerY) || (enemyBullets[i].bulletXEnemy == player.playerX + 3 && enemyBullets[i].bulletYEnemy == player.playerY) || (enemyBullets[i].bulletXEnemy == player.playerX + 4 && enemyBullets[i].bulletYEnemy == player.playerY))
                {
                    player.healthPlayer--;
                    eraseBullet(ref enemyBullets[i].bulletXEnemy, ref enemyBullets[i].bulletYEnemy);
                }
             
            }
        }
    }
}
