using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullet_Dungeon
{
    class Consumable
    {
        public int x, y;
        public string type;
        public int size = 20;

        public Consumable(int _x, int _y, string _type)
        {
            x = _x;
            y = _y;
            type = _type;
        }

    }
}
