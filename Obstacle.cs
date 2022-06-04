using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullet_Dungeon
{
    class Obstacle
    {
        public int x, y, width, height, hp;
        public string type;
        
        public Obstacle(int _x, int _y, int _width, int _height, int _hp, string _type)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            hp = _hp;
            type = _type;
        }
    }
}
