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
    public partial class EndScreen : UserControl
    {
        public EndScreen()
        {
            InitializeComponent();
            SetupScreen();
        }

        private void SetupScreen()
        {
            if(Form1.win == true)
            {
                endLabel.Text = "Winner!";
            }
            else
            {
                endLabel.Text = "Game Over";
            }
            this.Size = Screen.FromControl(this).Bounds.Size;
            endLabel.Location = new System.Drawing.Point((this.Width / 2) - 340, 200);
            playButton.Location = new System.Drawing.Point((this.Width / 2) - 200, 400);
            menuButton.Location = new System.Drawing.Point((this.Width / 2) - 200, 600);
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new MenuScreen());
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new GameScreen());
        }
    }
}
