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

        public Bullet(Player player, List<Bullet> playerBullets, int bulletCount)
        {

        }

        public void createBullet(ref Player player, ref List<Bullet> playerBullets, ref int bulletCount)
        {
            Bullet bullet = new Bullet(player, playerBullets, bulletCount);
            // creates bullets of player on right side
            bullet.bulletX = player.playerX + 6;
            bullet.bulletY = player.playerY + 1;
            playerBullets.Add(bullet);
            Console.SetCursorPosition(player.playerX + 6, player.playerY + 1);
            Console.Write(".");
            bulletCount++;
        }

        public void moveBullet(ref List<Bullet> playerBullets, ref int bulletCount, ref char[,] maze, ref Player player)
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
        public Bullet2(Enemy enemy, List<Bullet2> enemyBullets, int bulletCountEnemy)
        {

        }
        public void createBulletEnemy(ref Enemy enemy, ref List<Bullet2> enemyBullets, ref int bulletCountEnemy)
        {
            Bullet2 bullet = new Bullet2(enemy, enemyBullets, bulletCountEnemy);
            // creates bullets of enemy one
            bullet.bulletXEnemy = enemy.enemyOneX;
            bullet.bulletYEnemy = enemy.enemyOneY + 3;
            enemyBullets.Add(bullet);
            Console.SetCursorPosition(enemy.enemyOneX, enemy.enemyOneY + 3);
            Console.Write("+");
            bulletCountEnemy++;
        }
        public void moveBulletEnemy(ref List<Bullet2> enemyBullets, ref int bulletCountEnemy, ref char[,] maze)
        {
            // moves bullets of enemy one
            for (int i = 0; i < bulletCountEnemy; i++)
            {
                if (maze[enemyBullets[i].bulletYEnemy + 1, enemyBullets[i].bulletXEnemy] == '#')
                {
                    eraseBullet(ref enemyBullets[i].bulletXEnemy, ref enemyBullets[i].bulletYEnemy);
                }
                else
                {
                    eraseBullet(ref enemyBullets[i].bulletXEnemy, ref enemyBullets[i].bulletYEnemy);
                    enemyBullets[i].bulletYEnemy = (enemyBullets[i].bulletYEnemy + 1);
                    printBulletEnemy(ref enemyBullets[i].bulletXEnemy, ref enemyBullets[i].bulletYEnemy);
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