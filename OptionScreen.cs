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
    public partial class OptionScreen : UserControl
    {
        public OptionScreen()
        {
            InitializeComponent();
            this.Size = Screen.FromControl(this).Bounds.Size;
        }

        private void SetButtons()
        {
            if(Form1.steel == true)
            {
                steelButton.BackColor = Color.Green;
            }
            else
            {
                steelButton.BackColor = Color.Red;
            }
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new MenuScreen());
        }

        private void steelButton_Click(object sender, EventArgs e)
        {
            if(Form1.steel == false)
            {
                Form1.steel = true;
                steelButton.BackColor = Color.Green;
            }
            else
            {
                Form1.steel = false;
                steelButton.BackColor = Color.Red;
            }
        }
    }
}
