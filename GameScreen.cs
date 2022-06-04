using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullet_Dungeon
{
    public partial class GameScreen : UserControl
    {
        Player p1 = new Player(0, 0);

        SolidBrush playerBrush = new SolidBrush(Color.Red);
        SolidBrush playerBulletBrush = new SolidBrush(Color.Purple);
        SolidBrush boxBrush = new SolidBrush(Color.Brown);
        Pen testPen = new Pen(Color.White);
        Font testFont = new Font("Times New Roman", 12);

        string ammoText;

        int ammo;
        int shotInterval;
        bool reloading;
        int reloadTimer;

        bool upPressed, downPressed, rightPressed, leftPressed;

        List<Bullet> playerBullets = new List<Bullet>();
        List<Obstacle> levelObstacles = new List<Obstacle>();

        public GameScreen()
        {
            InitializeComponent();
            this.Size = Screen.FromControl(this).Bounds.Size;
            OnStart();
        }

        private void OnStart()
        {
            p1.x = 100;
            p1.y = 400;

            ammo = 10;

            levelObstacles.Add(new Obstacle(100, 250, 60, 50, 3, "box"));
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    upPressed = true;
                    break;
                case Keys.S:
                    downPressed = true;
                    break;
                case Keys.A:
                    leftPressed = true;
                    break;
                case Keys.D:
                    rightPressed = true;
                    break;
                case Keys.R:
                    Reload();
                    break;
            }
        }
        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    upPressed = false;
                    break;
                case Keys.S:
                    downPressed = false;
                    break;
                case Keys.A:
                    leftPressed = false;
                    break;
                case Keys.D:
                    rightPressed = false;
                    break;
            }
        }

        private void GameScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (ammo > 0 && shotInterval == 0 && reloading == false)
            {
                playerBullets.Add(new Bullet(p1.x, p1.y, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                shotInterval = 6;
                ammo -= 1;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if(shotInterval > 0)
            {
                shotInterval--;
            }
            if(reloadTimer > 0)
            {
                reloadTimer--;
            }
            else
            {
                reloading = false;
            }
            if(reloadTimer == 1)
            {
                ammo = 10;
            }


            p1.Move(upPressed, downPressed, leftPressed, rightPressed, this.Size, levelObstacles);
            

            foreach (Bullet b in playerBullets)
            {
                b.Move();
            }

            for (int i = 0; i < playerBullets.Count; i++)
            {
                if (playerBullets[i].x < 0 || playerBullets[i].y < 0 || playerBullets[i].x > ClientSize.Width || playerBullets[i].y > ClientSize.Height)
                {
                    playerBullets.RemoveAt(i);
                }
            }

            for (int i = 0; i < playerBullets.Count; i++)
            {
                foreach (Obstacle o in levelObstacles)
                {
                    if (playerBullets[i].HitObstacle(o))
                    {
                        o.hp--;
                        playerBullets.RemoveAt(i);
                    }
                }
            }

            for(int i = 0; i < levelObstacles.Count; i++)
            {
                if(levelObstacles[i].hp < 1)
                {
                    levelObstacles.RemoveAt(i);
                }
            }

            Refresh();

        }

        private void Reload()
        {
            reloading = true;
            ammoText = "Reloading";
            reloadTimer = 50;
        }


        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            if(reloading == false)
            {
                ammoText = ammo.ToString();
            }


            e.Graphics.FillRectangle(playerBrush, p1.x, p1.y, p1.size, p1.size);
            e.Graphics.DrawString($"{ammoText}", testFont, playerBrush, 0, 0);

            foreach (Bullet b in playerBullets)
            {
                e.Graphics.FillEllipse(playerBulletBrush, b.x - 1, b.y - 1, b.size + 2, b.size + 2);

            }

            foreach (Obstacle o in levelObstacles)
            {
                e.Graphics.FillRectangle(boxBrush, o.x, o.y, o.width, o.height);
                e.Graphics.DrawString($"{o.hp}", testFont, playerBrush, o.x, o.y);
            }

        }
    }
}

