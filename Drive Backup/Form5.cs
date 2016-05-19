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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                Properties.Settings.Default.runner = false;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
