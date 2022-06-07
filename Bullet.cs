using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Bullet_Dungeon
{
    class Bullet
    {
        public int x, y;
        public int size = 13;
        double xSpeed, ySpeed;
        double bulletSpeed = 11;
        public int creator;

        public Bullet(int _x, int _y, int targetX, int targetY, int _creator)
        {
            x = _x;
            y = _y;

            creator = _creator;

            double changeX = (targetX - x);
            double changeY = (targetY - y);
            double ratio = Math.Abs(changeX / changeY);

            double angle = Math.Atan(ratio);

            if (changeX < 0 && changeY < 0)
            {
                xSpeed = bulletSpeed * -1 * (Math.Sin(angle));
                ySpeed = bulletSpeed * -1 * (Math.Cos(angle));
            }
            else if (changeX > 0 && changeY < 0)
            {
                xSpeed = bulletSpeed * (Math.Sin(angle));
                ySpeed = bulletSpeed * -1 * (Math.Cos(angle));
            }
            else if (changeX < 0 && changeY > 0)
            {
                xSpeed = bulletSpeed * -1 * (Math.Sin(angle));
                ySpeed = bulletSpeed * (Math.Cos(angle));
            }
            else if (changeX > 0 && changeY > 0)
            {
                xSpeed = bulletSpeed * (Math.Sin(angle));
                ySpeed = bulletSpeed * (Math.Cos(angle));
            }
            else
            {
                ySpeed = -13;
                xSpeed = 0;
            }
            xSpeed = Math.Round(xSpeed);
            ySpeed = Math.Round(ySpeed);
        }

        public void Move()
        {
            x += Convert.ToInt32(xSpeed);
            y += Convert.ToInt32(ySpeed);
        }

        public bool HitObstacle(Obstacle o)
        {
            Rectangle obstacleRect = new Rectangle(o.x, o.y, o.width, o.height);
            Rectangle bulletRect = new Rectangle(x, y, size, size);

            if (bulletRect.IntersectsWith(obstacleRect))
            {
                return true;
            }
            return false;
        }

        public bool HitEnemy(Enemy r)
        {
            Rectangle enemyRect = new Rectangle(r.x, r.y, r.size, r.size);
            Rectangle bulletRect = new Rectangle(x, y, size, size);

            if (bulletRect.IntersectsWith(enemyRect))
            {
                return true;
            }
            return false;
        }

        public bool HitPlayer(Player p)
        {
            Rectangle playerRect = new Rectangle(p.x, p.y, p.size, p.size);
            Rectangle bulletRect = new Rectangle(x, y, size, size);

            if (bulletRect.IntersectsWith(playerRect))
            {
                return true;
            }
            return false;
        }
    }
}
