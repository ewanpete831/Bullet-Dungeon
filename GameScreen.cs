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
using System.Xml;

namespace Bullet_Dungeon
{
    public partial class GameScreen : UserControl
    {
        Player p1 = new Player(0, 0);

        SolidBrush playerBrush = new SolidBrush(Color.Red);
        SolidBrush playerBulletBrush = new SolidBrush(Color.Purple);

        SolidBrush boxBrush = new SolidBrush(Color.Brown);
        SolidBrush wallBrush = new SolidBrush(Color.Gray);
        SolidBrush testBrush = new SolidBrush(Color.White);

        SolidBrush regEnemyBrush = new SolidBrush(Color.Yellow);
        SolidBrush shotgunBrush = new SolidBrush(Color.Goldenrod);
        SolidBrush turretBrush = new SolidBrush(Color.DarkGoldenrod);
        SolidBrush enemyBulletBrush = new SolidBrush(Color.Orange);
        SolidBrush bossBrush = new SolidBrush(Color.Aquamarine);

        Pen testPen = new Pen(Color.White);
        Font testFont = new Font("Times New Roman", 12);
        Font ammoFont = new Font("Comic Sans MS", 24);

        Random bossRandom = new Random();
        Image[] steel = new Image[3];



        int health;
        int level;

        int bossSpeed;
        bool bossRight;
        bool bossShooting;
        int bossPhase;
        int lastBossAtk;
        int bossShootCount;

        int fadeTimer;
        int maxAmmo;
        int tempInvuln;
        int ammo;
        int shotInterval;
        bool reloading;
        int reloadTimer;
        int dodgeTimer;
        int sprite = 1;
        int spriteSwitch;

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
            steel[0] = Properties.Resources.Steel1;
            steel[1] = Properties.Resources.Steel2;
            steel[2] = Properties.Resources.Steel3;
            OnStart();
        }

        private void OnStart()
        {
            health = 4;
            level = 4;

            p1.invulnerable = false;
            bossRight = false;
            bossSpeed = 2;
            bossPhase = 1;

            spriteSwitch = 0;

            ammo = 10;
            maxAmmo = 10;

            p1.x = 100;
            p1.y = Form1.screenHeight - 100;

            LoadLevel(level);
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
            if (tempInvuln > 0)
            {
                tempInvuln--;
            }
            if (tempInvuln == 1)
            {
                p1.invulnerable = false;
            }
            if (fadeTimer > 0)
            {
                fadeTimer -= 10;
            }
            spriteSwitch++;
            if (spriteSwitch > 4)
            {
                spriteSwitch = 0;
                if (sprite == 2)
                {
                    sprite = 0;
                }
                else
                {
                    sprite++;
                }
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
                if (r.type == "Boss")
                {
                    if (!bossRight)
                    {
                        r.x -= bossSpeed;
                        if (r.x < 100)
                        {
                            bossRight = true;
                        }
                    }
                    else
                    {
                        r.x += bossSpeed;
                        if (r.x > 1500)
                        {
                            bossRight = false;
                        }
                    }
                }
                else if (r.type != "turret")
                {
                    r.Move(p1, this.Size, levelObstacles);
                }
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
                if (enemies[i].type == "shotgun")
                {
                    if (enemies[i].lastShot > 50)
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
                if (enemies[i].type == "turret")
                {
                    if (enemies[i].lastShot > 30)
                    {
                        int centreX = enemies[i].x + (enemies[i].size / 2);
                        int centreY = enemies[i].y + (enemies[i].size / 2);

                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX + 1, centreY - 2000, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX + 2000, centreY - 2000, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX + 2000, centreY + 1, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX + 2000, centreY + 2000, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX + 1, centreY + 2000, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX - 2000, centreY + 2000, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX - 2000, centreY + 1, i + 1));
                        enemyBullets.Add(new Bullet(enemies[i].x, enemies[i].y, centreX - 2000, centreY - 2000, i + 1));
                        enemies[i].bullets += 8;
                        enemies[i].lastShot = 0;
                    }
                }
                if (enemies[i].type == "Boss")
                {
                    BossMove(enemies[i]);
                }
            }
            #endregion



            if (enemies.Count < 1)
            {
                level++;
                LoadLevel(level);
                enemyBullets.Clear();
                playerBullets.Clear();
                p1.x = 100;
                p1.y = Form1.screenHeight - 100;
            }

            Refresh();
        }

        private void BossMove(Enemy b)
        {
            if (b.hp < 50)
            {
                bossPhase = 3;
            }
            else if (b.hp < 125)
            {
                bossPhase = 2;
            }

            switch (bossPhase)
            {
                case 1:
                    if (b.lastShot > 100 && !bossShooting)
                    {
                        int attack = bossRandom.Next(1, 4);
                        switch (attack)
                        {
                            case 1:
                                AddEnemy("weak", b.x + 10, b.y + b.size + 20);
                                AddEnemy("weak", b.x + b.size - 10, b.y + b.size + 20);
                                break;
                            case 2:
                                enemyBullets.Add(new Bullet(b.x, b.y + b.size, p1.x, p1.y, 1));
                                enemyBullets.Add(new Bullet(b.x + (b.size / 2), b.y + b.size, p1.x, p1.y, 1));
                                enemyBullets.Add(new Bullet(b.x + b.size, b.y + b.size, p1.x, p1.y, 1));
                                b.bullets += 3;
                                bossShooting = true;
                                lastBossAtk = 2;
                                bossShootCount = 9;
                                break;
                            case 3:
                                for (int i = 0; i < 19; i++)
                                {
                                    enemyBullets.Add(new Bullet(i * 100, 35, (i * 100) + 1, 450, 1));
                                    b.bullets++;
                                }
                                bossShooting = true;
                                lastBossAtk = 3;
                                bossShootCount = 12;
                                break;
                        }
                        b.lastShot = 0;
                    }
                    else if (bossShooting)
                    {
                        switch (lastBossAtk)
                        {
                            case 2:
                                if (b.lastShot > 10 && bossShootCount > 0)
                                {
                                    enemyBullets.Add(new Bullet(b.x, b.y + b.size, p1.x, p1.y, 1));
                                    enemyBullets.Add(new Bullet(b.x + (b.size / 2), b.y + b.size, p1.x, p1.y, 1));
                                    enemyBullets.Add(new Bullet(b.x + b.size, b.y + b.size, p1.x, p1.y, 1));
                                    b.bullets += 3;
                                    b.lastShot = 0;
                                    bossShootCount--;
                                }
                                else if (bossShootCount == 0)
                                {
                                    bossShooting = false;
                                }
                                break;
                            case 3:
                                if (b.lastShot > 3 && bossShootCount > 0)
                                {
                                    for (int i = 0; i < 19; i++)
                                    {
                                        enemyBullets.Add(new Bullet(i * 100, 35, (i * 100) + 1, 450, 1));
                                        b.bullets++;
                                    }
                                    b.lastShot = 0;
                                    bossShootCount--;
                                }
                                else if (bossShootCount == 0)
                                {
                                    bossShooting = false;
                                }
                                break;
                        }
                    }
                    break;
                case 2:

                    break;
                case 3:

                    break;
            }
        }

        private void AddEnemy(string type, int x, int y)
        {
            switch (type)
            {
                case "regular":
                    enemies.Add(new Enemy(x, y, 4, "regular", 2));
                    break;
                case "shotgun":
                    enemies.Add(new Enemy(x, y, 6, "shotgun", 3));
                    break;
                case "turret":
                    enemies.Add(new Enemy(x, y, 10, "turret", 0));
                    break;
                case "Boss":
                    enemies.Add(new Enemy(x, y, 200, "Boss", 2));
                    enemies[enemies.Count - 1].size = 320;
                    break;
                case "weak":
                    enemies.Add(new Enemy(x, y, 2, "regular", 2));
                    break;
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
            if (ammo < maxAmmo)
            {
                reloading = true;
                reloadTimer = 50;
            }
        }

        private void LoadLevel(int level)
        {
            XmlReader reader = XmlReader.Create($"Level{level}.xml");

            levelObstacles.Clear();
            //walls
            levelObstacles.Add(new Obstacle(0, 0, Form1.screenWidth, 30, 1, "wall"));
            levelObstacles.Add(new Obstacle(0, 0, 30, Form1.screenHeight, 1, "wall"));
            levelObstacles.Add(new Obstacle(0, Form1.screenHeight - 30, Form1.screenWidth, 30, 1, "wall"));
            levelObstacles.Add(new Obstacle(Form1.screenWidth - 30, 0, 30, Form1.screenHeight, 1, "wall"));

            enemies.Clear();
            string x, y, width, height, hp, type;

            while (reader.Read())
            {
                reader.ReadToFollowing("x");
                x = reader.ReadString();

                reader.ReadToFollowing("y");
                y = reader.ReadString();

                reader.ReadToFollowing("width");
                width = reader.ReadString();

                reader.ReadToFollowing("height");
                height = reader.ReadString();

                reader.ReadToFollowing("hp");
                hp = reader.ReadString();

                reader.ReadToFollowing("type");
                type = reader.ReadString();

                if (x != "")
                {
                    levelObstacles.Add(new Obstacle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width),
                        Convert.ToInt32(height), Convert.ToInt32(hp), type));
                }
            }
            reader.Close();

            XmlReader enemyReader = XmlReader.Create($"Level{level}.xml");
            string enemyType, enemyX, enemyY;

            while (enemyReader.Read())
            {
                enemyReader.ReadToFollowing("enemyType");
                enemyType = enemyReader.ReadString();

                enemyReader.ReadToFollowing("enemyX");
                enemyX = enemyReader.ReadString();

                enemyReader.ReadToFollowing("enemyY");
                enemyY = enemyReader.ReadString();

                if (enemyType != "")
                {
                    AddEnemy(enemyType, Convert.ToInt32(enemyX), Convert.ToInt32(enemyY));
                }
            }
            enemyReader.Close();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
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
                switch (r.type)
                {
                    case "regular":
                        e.Graphics.FillRectangle(regEnemyBrush, r.x, r.y, r.size, r.size);
                        break;
                    case "shotgun":
                        e.Graphics.FillRectangle(shotgunBrush, r.x, r.y, r.size, r.size);
                        break;
                    case "turret":
                        e.Graphics.FillRectangle(turretBrush, r.x, r.y, r.size, r.size);
                        break;
                    case "Boss":
                        e.Graphics.DrawImage(steel[sprite], r.x, r.y, 320, 320);
                        break;
                }
                e.Graphics.DrawString($"{r.hp}, {r.bullets}", testFont, playerBrush, r.x, r.y);
            }


            for (int i = 0; i < health; i++)
            {
                e.Graphics.FillRectangle(playerBrush, (35 * i) + 5, 5, 30, 30);
            }
            if (reloading)
            {
                e.Graphics.DrawString("Reloading", ammoFont, testBrush, Form1.screenWidth - 155, Form1.screenHeight - 50);
            }
            else
            {
                for (int i = 0; i < ammo; i++)
                {
                    e.Graphics.FillRectangle(testBrush, Form1.screenWidth - 50, Form1.screenHeight - (25 * (i + 1)), 40, 20);
                }
            }
        }
    }
}

