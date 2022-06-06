using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bullet_Dungeon
{
    class Enemy
    {
        public int x, y, hp, bullets, speed, lastShot;
        public int size = 20;
        public string type;

        public Enemy(int _x, int _y, int _hp, string _type, int _speed)
        {
            x = _x;
            y = _y;
            hp = _hp;
            type = _type;
            speed = _speed;
        }

        public void Move(Player p, Size screenSize, List<Obstacle> obstacles)
        {
            double xDist = Math.Abs(x - p.x);
            double yDist = Math.Abs(y - p.y);
            Rectangle enemyRect = new Rectangle(x, y, size, size);

            int lastX = x;
            int lastY = y;

            if (xDist > yDist)
            {
                if (p.x < x)
                {
                    x -= speed;
                }
                else
                {
                    x += speed;
                }
            }
            else
            {
                if(p.y < y)
                {
                    y -= speed;
                }
                else
                {
                    y += speed;
                }
            }

            foreach(Obstacle o in obstacles)
            {
                Rectangle obstacleRect = new Rectangle(o.x, o.y, o.width, o.height);
                if (enemyRect.IntersectsWith(obstacleRect))
                {
                    y = lastY;
                    x = lastX;

                    if (x > obstacleRect.Left - size && x < obstacleRect.Right)
                    {
                        if (y < obstacleRect.Bottom || y > obstacleRect.Top - size)
                        {
                            OtherMove(xDist, yDist, p);
                        }
                    }
                    else if (y > obstacleRect.Top - size && y < obstacleRect.Bottom)
                    {
                        if (x < obstacleRect.Right || x > obstacleRect.Left - size)
                        {
                            OtherMove(xDist, yDist, p);
                        }
                    }
                }
            }
        }
        private void OtherMove(double xDist, double yDist, Player p)
        {
            if (xDist > yDist)
            {
                if (p.y < y)
                {
                    y -= speed;
                }
                else
                {
                    y += speed;
                }
            }
            else
            {
                if (p.x < x)
                {
                    x -= speed;
                }
                else
                {
                    x += speed;
                }
            }
        }
    }
}
