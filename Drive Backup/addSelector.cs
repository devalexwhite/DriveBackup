using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.IO;

namespace Drive_Backup
{
    public partial class addSelector : Form
    {
        bool hidden = false;
        public addSelector()
        {
            InitializeComponent();
            radioButton1.Select();
            moveUp();
        }
        private void moveDown()
        {
            if (hidden == true)
            {
                textBox1.Show();
                groupBox1.Height += 80;
                this.Height += 80;
                this.button1.Top += 80;
                this.button2.Top += 80;
            }
            hidden = false;
        }
        private void moveUp()
        {
            if (hidden == false)
            {
                textBox1.Hide();
                groupBox1.Height -= 80;
                this.Height -= 80;
                this.button1.Top -= 80;
                this.button2.Top -= 80;
            }
            hidden = true;
        }
        private void addSelector_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            moveDown();
            textBox1.Text="A computer to USB drive allows you to select files on your computer that will be backed up everytime the drive is inserted";
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            moveDown();
            textBox1.Text = "A USB to computer drive backs up files on your USB drive to your computer each time the drive is inserted";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                string selectedDrive = "";
                selectDrive z1 = new selectDrive();
                int index = 0;
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    if (d.DriveType == System.IO.DriveType.Removable && d.Name != null)
                    {
                        index++;
                    }
                }
                if (index != 0)
                {
                    if (z1.ShowDialog(this) == DialogResult.OK)
                    {
                        selectedDrive = z1.drive;
                    }
                    z1.Dispose();
                }
                if (selectedDrive != "")
                {
                    Form2 a = new Form2();
                    a.Show();
                    a.selectedDrive = selectedDrive;
                    a.label1.Text = "Drive selected:" + selectedDrive;
                    a.label1.Refresh();
                    this.Close();
                }
            }
            else
            {
                //Reverse Drive Backup routine
                //  -Select entire drive or directories
                //  -When drive inserted, backup entire drive to selected directory
                //  -Use identity file on drive (search on insert method)

                //Select Drive
                string selectedDrive = "";
                selectDrive z1 = new selectDrive();
                int index = 0;
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    if (d.DriveType == System.IO.DriveType.Removable && d.Name != null)
                    {
                        index++;
                    }
                }
                if (index != 0)
                {
                    if (z1.ShowDialog(this) == DialogResult.OK)
                    {
                        selectedDrive = z1.drive;
                    }
                    z1.Dispose();
                }
                if (selectedDrive != "")
                {
                    Form4 parent = new Form4();
                    parent.Show();
                    string aa = null;
                    if (selectedDrive != "" && selectedDrive.Length != 3)
                    {
                        aa = selectedDrive.Remove(3);
                    }
                    else
                    {
                        aa = selectedDrive;
                    }
                    selectedDrive = aa;
                    parent.drive = selectedDrive;
                    parent.label1.Text = "Selected Drive:" + selectedDrive;
                    this.Close();
                }
            }
        }
    }
}
