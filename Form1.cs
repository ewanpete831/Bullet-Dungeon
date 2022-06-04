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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setSize();
            ChangeScreen(this, new MenuScreen());
        }

        public void setSize()
        {
             this.Size = Screen.FromControl(this).Bounds.Size;
        }

        public static void ChangeScreen(object sender, UserControl next)

        {
            Form f; // will either be the sender or parent of sender 

            if (sender is Form)
            {
                f = (Form)sender;
            }
            else
            {
                UserControl current = (UserControl)sender;
                f = current.FindForm();
                f.Controls.Remove(current);
            }

            f.Controls.Add(next);

            next.Focus();
        }
    }
}
