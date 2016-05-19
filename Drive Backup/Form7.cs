using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Drive_Backup
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadString("http://awnet.homedns.org/wordpress/handler.php?01=" + this.textBox1.Text);
            }
            catch (Exception a)
            {
                MessageBox.Show("Failed to connect to web server, please visit http://awnet.homedns.org to register");
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
