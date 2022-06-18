using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bullet_Dungeon
{
    class Player
    {
        public int x, y;
        public int size = 20;
        int straightSpeed = 8;
        int diagSpeed = 6;
        public bool invulnerable;

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public void Move(bool up, bool down, bool left, bool right, Size screenSize, List<Obstacle> obstacles)
        {
            #region speed
            int speed;
            int lastX = x;
            int lastY = y;
            if (up && !down && !left && !right)
            {
                speed = straightSpeed;
            }
            else if (!up && down && !left && !right)
            {
                speed = straightSpeed;
            }
            else if (!up && !down && left && !right)
            {
                speed = straightSpeed;
            }
            else if (!up && !down && !left && right)
            {
                speed = straightSpeed;
            }
            else
            {
                speed = diagSpeed;
            }

            #endregion
            #region direction
            if (up)
            {
                y -= speed;
            }
            if (down)
            {
                y += speed;
            }
            if (left)
            {
                x -= speed;
            }
            if (right)
            {
                x += speed;
            }
            #endregion

            if (x < 0 || x > screenSize.Width - size)
            {
                x = lastX;
            }
            if (y < 0 || y > screenSize.Height - size)
            {
                y = lastY;
            }

            foreach (Obstacle o in obstacles)
            {
                Rectangle obstacleRect = new Rectangle(o.x, o.y, o.width, o.height);
                Rectangle playerRect = new Rectangle(x, y, size, size);

                if (playerRect.IntersectsWith(obstacleRect))
                {
                    if (x > obstacleRect.Left - size && x < obstacleRect.Right)
                    {
                        if (y < obstacleRect.Bottom || y > obstacleRect.Top - size)
                        {
                            y = lastY;
                        }
                    }
                    if (y > obstacleRect.Top - size && y < obstacleRect.Bottom)
                    {
                        if (x < obstacleRect.Right || x > obstacleRect.Left - size)
                        {
                            x = lastX;
                            if(up)
                            {
                                y -= diagSpeed;
                            }
                            if(down)
                            {
                                y += diagSpeed;
                            }
                        }
                    }
                }
            }
        }
        public bool Collect(Consumable c)
        {
            Rectangle pRect = new Rectangle(x, y, size, size);
            Rectangle cRect = new Rectangle(c.x, c.y, c.size, c.size);

            if(pRect.IntersectsWith(cRect))
            {
                return true;
            }
            return false;
        }
    }
}
