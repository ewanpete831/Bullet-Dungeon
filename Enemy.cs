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
                            OtherMove(xDist, yDist, p, obstacleRect);
                        }
                    }
                    else if (y > obstacleRect.Top - size && y < obstacleRect.Bottom)
                    {
                        if (x < obstacleRect.Right || x > obstacleRect.Left - size)
                        {
                            OtherMove(xDist, yDist, p, obstacleRect);
                        }
                    }
                }
            }


        }
        private void OtherMove(double xDist, double yDist, Player p, Rectangle oRect)
        {
            if (xDist > yDist)
            {
                if (p.y < y)
                {
                    int nextY = y - speed;
                    Rectangle nextPos = new Rectangle(x, nextY, size, size);
                    if(nextPos.IntersectsWith(oRect))
                    {
                        y += speed;
                    }
                    else
                    {
                        y -= speed;
                    }
                }
                else
                {
                    int nextY = y + speed;
                    Rectangle nextPos = new Rectangle(x, nextY, size, size);
                    if (nextPos.IntersectsWith(oRect))
                    {
                        y -= speed;
                    }
                    else
                    {
                        y += speed;
                    }
                }
            }
            else
            {
                if (p.x < x)
                {
                    int nextX = x - speed;
                    Rectangle nextPos = new Rectangle(nextX, y, size, size);
                    if (nextPos.IntersectsWith(oRect))
                    {
                       x += speed;
                    }
                    else
                    {
                        x -= speed;
                    }
                }
                else
                {
                    int nextX = x + speed;
                    Rectangle nextPos = new Rectangle(nextX, y, size, size);
                    if (nextPos.IntersectsWith(oRect))
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
}
