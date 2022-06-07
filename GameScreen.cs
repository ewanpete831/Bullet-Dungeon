using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bullet_Dungeon
{
    public partial class GameScreen : UserControl
    {
        Player p1 = new Player(0, 0);

        SolidBrush playerBrush = new SolidBrush(Color.Red);
        SolidBrush playerBulletBrush = new SolidBrush(Color.Purple);
        SolidBrush boxBrush = new SolidBrush(Color.Brown);
        SolidBrush testBrush = new SolidBrush(Color.White);
        SolidBrush enemyBrush = new SolidBrush(Color.Yellow);
        SolidBrush enemyBulletBrush = new SolidBrush(Color.Orange);
        SolidBrush wallBrush = new SolidBrush(Color.Gray);
        Pen testPen = new Pen(Color.White);
        Font testFont = new Font("Times New Roman", 12);

        string ammoText;

        int health;

        int tempInvuln;
        int ammo;
        int shotInterval;
        bool reloading;
        int reloadTimer;
        int dodgeTimer;

        bool upPressed, downPressed, rightPressed, leftPressed;
        bool lastUp, lastDown, lastRight, lastLeft;

        List<Bullet> playerBullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();
        List<Obstacle> levelObstacles = new List<Obstacle>();
        List<Enemy> enemies = new List<Enemy>();

        public GameScreen()
        {
            InitializeComponent();
            this.Size = Screen.FromControl(this).Bounds.Size;
            OnStart();
        }

        private void OnStart()
        {
            health = 3;
            p1.x = 100;
            p1.y = 400;
            p1.invulnerable = false;

            ammo = 10;

            levelObstacles.Add(new Obstacle(100, 250, 70, 20, 3, "box"));
            levelObstacles.Add(new Obstacle(400, 250, 10, 100, 3, "box"));
            levelObstacles.Add(new Obstacle(150, 386, 60, 50, 3, "box"));
            levelObstacles.Add(new Obstacle(320, 300, 60, 80, 1, "wall"));

            levelObstacles.Add(new Obstacle(0, 0, Form1.screenWidth, 30, 1, "wall"));
            levelObstacles.Add(new Obstacle(0, 0, 30, Form1.screenHeight, 1, "wall"));
            levelObstacles.Add(new Obstacle(0, Form1.screenHeight - 30, Form1.screenWidth, 30, 1, "wall"));
            levelObstacles.Add(new Obstacle(Form1.screenWidth - 30, 0, 30, Form1.screenHeight, 1, "wall"));


            for (int i = 0; i < 5; i++)
            {
                AddEnemy("regular", i * 50, 150);
            }

            AddEnemy("shotgun", 100, 700);
            AddEnemy("shotgun", 200, 700);
            AddEnemy("shotgun", 300, 700);
            AddEnemy("shotgun", 400, 700);

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
            if (e.Button == MouseButtons.Left)
            {
                if (ammo > 0 && shotInterval == 0 && reloading == false)
                {
                    playerBullets.Add(new Bullet(p1.x, p1.y, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y, 0));
                    shotInterval = 6;
                    ammo -= 1;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Dodge();
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            #region timers
            if (shotInterval > 0)
            {
                shotInterval--;
            }
            if (reloadTimer > 0)
            {
                reloadTimer--;
            }
            else
            {
                reloading = false;
            }
            if (reloadTimer == 1)
            {
                ammo = 10;
            }
            if (dodgeTimer > 0)
            {
                dodgeTimer--;
            }
            if (dodgeTimer == 1)
            {
                p1.invulnerable = false;
            }
            foreach (Enemy r in enemies)
            {
                r.lastShot++;
            }
            if(tempInvuln > 0)
            {
                tempInvuln--;
            }
            if(tempInvuln == 1)
            {
                p1.invulnerable = false;
            }
            #endregion

            #region movement
            //move player
            if (p1.invulnerable == false)
            {
                p1.Move(upPressed, downPressed, leftPressed, rightPressed, this.Size, levelObstacles);
            }
            else
            {
                p1.Move(lastUp, lastDown, lastLeft, lastRight, this.Size, levelObstacles);
            }

            //move enemies
            foreach (Enemy r in enemies)
            {
                r.Move(p1, this.Size, levelObstacles, enemies);
            }

            //move bullets
            foreach (Bullet b in playerBullets)
            {
                b.Move();
            }
            foreach (Bullet b in enemyBullets)
            {
                b.Move();
            }
            #endregion

            //bullets hitting obstacles
            #region
            foreach (Obstacle o in levelObstacles)
            {
                for (int i = 0; i < playerBullets.Count; i++)
                {
                    if (playerBullets[i].HitObstacle(o))
                    {
                        if (o.type == "box")
                        {
                            o.hp--;
                            playerBullets.RemoveAt(i);
                        }
                        if (o.type == "wall")
                        {
                            playerBullets.RemoveAt(i);
                        }
                    }
                }
                for (int i = 0; i < enemyBullets.Count; i++)
                {
                    if (enemyBullets[i].HitObstacle(o))
                    {
                        int idRemoved = enemyBullets[i].creator;
                        try
                        {
                            enemies[idRemoved - 1].bullets--;
                        }
                        catch { }
                        enemyBullets.RemoveAt(i);
                    }
                }
            }

            for (int i = 0; i < levelObstacles.Count; i++)
            {
                if (levelObstacles[i].hp < 1)
                {
                    levelObstacles.RemoveAt(i);
                }
            }
            #endregion

            //bullets hitting enemies / players
            #region
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (enemyBullets[i].HitPlayer(p1))
                {
                    if (p1.invulnerable == false && tempInvuln == 0)
                    {
                        tempInvuln = 20;
                        int idRemoved = enemyBullets[i].creator;
                        health--;
                        try
                        {
                            enemies[idRemoved - 1].bullets--;
                        }
                        catch { }
                        enemyBullets.RemoveAt(i);
                    }
                }
            }

            foreach (Enemy r in enemies)
            {
                for (int i = 0; i < playerBullets.Count; i++)
                {
                    if (playerBullets[i].HitEnemy(r))
                    {
                        r.hp--;
                        playerBullets.RemoveAt(i);
                    }
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].hp < 1)
                {
                    enemies.RemoveAt(i);
                }
            }
            #endregion

            //enemies shooting
            #region
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].type == "regular")
                {
                    if (enemies[i].bullets < 4 && enemies[i].lastShot > 15)
                    {
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x, p1.y, i + 1));
                        enemies[i].bullets++;
                        enemies[i].lastShot = 0;
                    }
                }
                if(enemies[i].type == "shotgun")
                {
                    if(enemies[i].lastShot > 50)
                    {
                        double xDist = Math.Abs(enemies[i].x - p1.x);
                        double yDist = Math.Abs(enemies[i].y - p1.y);

                        if (xDist < yDist)
                        {
                            enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x - 20, p1.y, i + 1));
                            enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x, p1.y, i + 1));
                            enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x + 20, p1.y, i + 1));
                        }
                        else
                        {
                            enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x, p1.y - 20, i + 1));
                            enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x, p1.y, i + 1));
                            enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, p1.x, p1.y + 20, i + 1));
                        }
                        enemies[i].bullets += 3;
                        enemies[i].lastShot = 0;
                    }
                }
            }
            #endregion

            if (enemies.Count < 1)
            {
                Thread.Sleep(1000);
                Application.Exit();
            }


            Refresh();
        }

        private void AddEnemy(string type, int x, int y)
        {
            if (type == "regular")
            {
                enemies.Add(new Enemy(x, y, 4, "regular", 2));
            }
            if(type == "shotgun")
            {
                enemies.Add(new Enemy(x, y, 6, "shotgun", 3));
            }


        }
        private void Dodge()
        {
            lastLeft = leftPressed;
            lastUp = upPressed;
            lastRight = rightPressed;
            lastDown = downPressed;
            p1.invulnerable = true;
            dodgeTimer = 20;
        }
        private void Reload()
        {
            reloading = true;
            ammoText = "Reloading";
            reloadTimer = 50;
        }


        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            if (reloading == false)
            {
                ammoText = ammo.ToString();
            }

            if (p1.invulnerable == true)
            {
                playerBrush.Color = Color.White;
            }
            else
            {
                playerBrush.Color = Color.Red;
            }

            e.Graphics.FillRectangle(playerBrush, p1.x, p1.y, p1.size, p1.size);




            foreach (Bullet b in playerBullets)
            {
                e.Graphics.FillEllipse(playerBulletBrush, b.x - 1, b.y - 1, b.size + 2, b.size + 2);
            }

            foreach (Bullet b in enemyBullets)
            {
                e.Graphics.FillEllipse(enemyBulletBrush, b.x - 1, b.y - 1, b.size + 2, b.size + 2);
            }

            foreach (Obstacle o in levelObstacles)
            {
                if (o.type == "box")
                {
                    e.Graphics.FillRectangle(boxBrush, o.x, o.y, o.width, o.height);
                    e.Graphics.DrawString($"{o.hp}", testFont, testBrush, o.x, o.y);
                }
                if (o.type == "wall")
                {
                    e.Graphics.FillRectangle(wallBrush, o.x, o.y, o.width, o.height);
                }
            }

            foreach (Enemy r in enemies)
            {
                e.Graphics.FillRectangle(enemyBrush, r.x, r.y, r.size, r.size);
                e.Graphics.DrawString($"{r.hp}, {r.bullets}", testFont, playerBrush, r.x, r.y);
            }



            e.Graphics.DrawString($"{ammoText}", testFont, testBrush, 0, 0);
            e.Graphics.DrawString($"{health}", testFont, testBrush, 0, 20);

            e.Graphics.DrawString($"{Form1.screenWidth}, {Form1.screenHeight}", testFont, testBrush, 0, 40);

        }
    }
}

