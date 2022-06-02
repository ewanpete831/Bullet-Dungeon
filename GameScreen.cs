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
        Player p1 = new Player(100, 400);
        SolidBrush playerBrush = new SolidBrush(Color.Red);
        bool upPressed, downPressed, rightPressed, leftPressed;

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch(e.KeyCode)
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

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            p1.Move(upPressed, downPressed, leftPressed, rightPressed);

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(playerBrush, p1.x, p1.y, p1.size, p1.size);
        }
    }
}
