using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Drive_Backup
{
    public partial class selectDrive : Form
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        private string[] drives = new string[20];
        public string drive = "";
        public selectDrive()
        {
            InitializeComponent();
            if (populate() == 0)
            {
                this.DialogResult = DialogResult.No;
            }
            else
            {
                this.listBox1.SelectedIndex = 0;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private int populate()
        {
            int index = 0;
            foreach (DriveInfo d in allDrives)
            {
                try
                {
                    if (d.DriveType == System.IO.DriveType.Removable && d.Name != null)
                    {
                        drives[index] = d.VolumeLabel + " (" + d.Name + ")";
                        index++;
                    }
                }
                catch (System.IO.IOException)
                {
                }
                }
            if (index == 0)
            {
                MessageBox.Show("No USB drives found!");
                this.DialogResult = DialogResult.No;
            }
            else
            {
                foreach (string v in drives)
                {
                    if (v != null)
                    {
                        listBox1.Items.Add(v);
                    }
                }
            }
            return index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex!=-1)
            {
                drive = drives[listBox1.SelectedIndex];
                drive = drive.Substring(drive.IndexOf("(") + 1, 3);
                this.DialogResult = DialogResult.OK;
            }
            else { MessageBox.Show("Please select a drive!"); }
        }

        private void selectDrive_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.No;
        }
    }
}
