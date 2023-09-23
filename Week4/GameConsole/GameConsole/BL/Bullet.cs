using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConsole.BL
{
    public class Bullet
    {
        public int bulletX;
        public int bulletY;

        public Bullet(Player player, int bulletCount)
        {

        }

        public void createBullet(Player player, ref int bulletCount)
        {
            Bullet bullet = new Bullet(player, bulletCount);
            // creates bullets of player on right side
            bullet.bulletX = player.playerX + 6;
            bullet.bulletY = player.playerY + 1;
            player.playerBullets.Add(bullet);
            Console.SetCursorPosition(player.playerX + 6, player.playerY + 1);
            Console.Write(".");
            bulletCount++;
        }

        public void moveBullet(ref int bulletCount, char[,] maze, Player player)
        {
            // moves bullets of player on right side
            for (int i = 0; i < bulletCount; i++)
            {
                if (maze[player.playerBullets[i].bulletY, player.playerBullets[i].bulletX + 1] == '#')
                {
                    eraseBullet(ref player.playerBullets[i].bulletX, ref player.playerBullets[i].bulletY);
                }
                else
                {
                    eraseBullet(ref player.playerBullets[i].bulletX, ref player.playerBullets[i].bulletY);
                    player.playerBullets[i].bulletX = (player.playerBullets[i].bulletX + 1);
                    printBullet(ref player.playerBullets[i].bulletX, ref player.playerBullets[i].bulletY);
                }
            }
        }
        public void printBullet(ref int x, ref int y)
        {
            // prints bullets of player on either sides
            Console.SetCursorPosition(x, y);
            Console.Write(".");
        }
        public void eraseBullet(ref int x, ref int y)
        {
            // removes bullets of player on either sides
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
    }
    public class Bullet2
    {
        public int bulletXEnemy;
        public int bulletYEnemy;
        public Bullet2(Enemy enemy, int bulletCountEnemy)
        {

        }
        public void createBulletEnemy(Enemy enemy, ref int bulletCountEnemy)
        {
            Bullet2 bullet = new Bullet2(enemy, bulletCountEnemy);
            // creates bullets of enemy one
            bullet.bulletXEnemy = enemy.enemyOneX;
            bullet.bulletYEnemy = enemy.enemyOneY + 3;
            enemy.enemyBullets.Add(bullet);
            Console.SetCursorPosition(enemy.enemyOneX, enemy.enemyOneY + 3);
            Console.Write("+");
            bulletCountEnemy++;
        }
        public void moveBulletEnemy(Enemy enemy, ref int bulletCountEnemy, char[,] maze)
        {
            // moves bullets of enemy one
            for (int i = 0; i < bulletCountEnemy; i++)
            {
                if (maze[enemy.enemyBullets[i].bulletYEnemy + 1, enemy.enemyBullets[i].bulletXEnemy] == '#')
                {
                    eraseBullet(ref enemy.enemyBullets[i].bulletXEnemy, ref enemy.enemyBullets[i].bulletYEnemy);
                }
                else
                {
                    eraseBullet(ref enemy.enemyBullets[i].bulletXEnemy, ref enemy.enemyBullets[i].bulletYEnemy);
                    enemy.enemyBullets[i].bulletYEnemy = (enemy.enemyBullets[i].bulletYEnemy + 1);
                    printBulletEnemy(ref enemy.enemyBullets[i].bulletXEnemy, ref enemy.enemyBullets[i].bulletYEnemy);
                }
            }
        }
        public void printBulletEnemy(ref int x, ref int y)
        {
            // prints bullets of enemy one
            Console.SetCursorPosition(x, y);
            Console.Write("+");
        }
        public void eraseBullet(ref int x, ref int y)
        {
            // removes bullets of player on either sides
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
    }
}