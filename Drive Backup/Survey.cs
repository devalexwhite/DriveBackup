using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Drive_Backup
{
    public partial class Survey : Form
    {
        public Survey()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://sourceforge.net/donate/index.php?group_id=297641");
            Process.Start(sInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://awnet.homedns.org/limesurvey//index.php?sid=14188");
            Process.Start(sInfo);
        }
    }
}
