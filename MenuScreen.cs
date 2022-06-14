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
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
            this.Size = Screen.FromControl(this).Bounds.Size;
            Setup();
        }

        private void Setup()
        {
            playButton.Location = new System.Drawing.Point(this.Width / 2 - 300, this.Height / 2 - 100);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new GameScreen());
        }
    }
}
