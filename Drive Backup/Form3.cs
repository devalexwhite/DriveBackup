using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Drive_Backup
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            int posx = Screen.PrimaryScreen.WorkingArea.Width - 209;
            int posy = Screen.PrimaryScreen.WorkingArea.Height - 67;
            this.Left = posx;
            this.Top = posy;
              
        }
    }
}
