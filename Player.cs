using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullet_Dungeon
{ 
    class Player
    {
        public int x, y;
        public int size = 20;
        int straightSpeed = 8;
        int diagSpeed = 6;

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public void Move(bool up, bool down, bool left, bool right)
        {
            int speed;
            if(up && !down && !left && !right)
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



            if (up)
            {
                y -= speed;
            }
           if(down)
            {
                y += speed;
            }
           if(left)
            {
                x -= speed;
            }
           if(right)
            {
                x += speed;
            }
        }
    }
}
