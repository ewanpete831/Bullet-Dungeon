using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullet_Dungeon
{
    class Enemy
    {
        public int x, y, hp, bullets, speed;
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

        public void Move(Player p)
        {
            double xDist = Math.Abs(x - p.x);
            int yDist = Math.Abs(y - p.y);

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
        }
    }
}
