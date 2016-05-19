using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;

namespace Drive_Backup
{

    public partial class Form6 : Form
    {
        public DriveInfo[] allDrives = DriveInfo.GetDrives();
        private string[] driveA = new string[20];
        private string[] driveB = new string[20];
        public string drive = "";
        public Form6()
        {
            InitializeComponent();
            populate();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
        private void populate()
        {
            int x1 = 0;
            int x2 = 0;
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == System.IO.DriveType.Removable && d.Name != null)
                {
                    if (File.Exists(d.Name + WindowsIdentity.GetCurrent().Name.ToString() + "\\driveBackup.data"))
                    {
                        driveA[x1] = d.VolumeLabel + " (" + d.Name + ")";
                        x1++;
                    }
                    if (File.Exists(d.Name + WindowsIdentity.GetCurrent().Name.ToString() + "\\revBack.data"))
                    {
                        driveB[x2] = d.VolumeLabel + " (" + d.Name + ")";
                        x2++;
                    }
                }
            }
            if (x1 != 0)
            {
                foreach (string v in driveA)
                {
                    if (v != null)
                    {
                        listBox1.Items.Add(v);
                    }
                }
            }
            else 
            { 
                listBox1.Items.Add("No Backup Drives found!");
                listBox1.Enabled = false;
                tabControl1.SelectTab("tabPage2");
            }
            if (x2 != 0)
            {
                foreach (string v in driveB)
                {
                    if (v != null)
                    {
                        listBox2.Items.Add(v);
                    }
                }
            }
            else 
            { 
                listBox2.Items.Add("No Reverse Backup Drives found!");
                listBox2.Enabled = false;
                tabControl1.SelectTab("tabPage1");
            }
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0 && listBox1.SelectedIndex!=-1)
            {
                string selectedDrive = listBox1.GetItemText(listBox1.SelectedItem);
                selectedDrive = selectedDrive.Substring(selectedDrive.IndexOf("(")+1, 3);
                if (selectedDrive != "")
                {
                        Form2 z = new Form2();
                        z.Show();
                        z.selectedDrive = selectedDrive;
                        Class2.driveLetter = selectedDrive;
                        Class1 x = new Class1();
                        x.readDB(true, false);
                        int x1 = 0;
                        z.bFiles = new string[100];
                        while (Class2.files[x1] != "EOF")
                        {
                            z.bFiles[x1] = Class2.files[x1];
                            x1++;
                        }
                        File.Delete(selectedDrive + WindowsIdentity.GetCurrent().Name.ToString() + "\\www.mark");
                        File.Delete(selectedDrive + WindowsIdentity.GetCurrent().Name.ToString() + "\\driveBackup.data");
                        z.button3.Hide();
                        z.listBox1.Items.Clear();
                        int xx = 0;
                        while (xx < x1)
                        {
                            z.listBox1.Items.Add(Convert.ToString(z.bFiles[xx]));
                            xx++;
                        }
                        z.listBox1.Refresh();
                        z.index = x1;
                        z.label1.Text = "Drive Selected:" + selectedDrive;
                        z.button2.Hide();
                        this.Close();
                }
            }
            else if (tabControl1.SelectedIndex == 1 && listBox2.SelectedIndex != -1)
            {
                // :( :( :( :(
                string selectedDrive = listBox2.GetItemText(listBox2.SelectedItem);
                selectedDrive = selectedDrive.Substring(selectedDrive.IndexOf("(") + 1, 3);
                Form4 z1 = new Form4();
                z1.drive = selectedDrive;
                Class1 z2 = new Class1();
                Class2.driveLetter = selectedDrive;
                z2.readDB(true, true);
                z1.bFiles = Class2.files;
                z1.label1.Text = "Selected Drive:" + selectedDrive;
                int xx = 0;
                z1.index = 0;
                while (z1.bFiles[xx] != "EOF")
                {
                    z1.index++;
                    xx++;
                }
                
                z1.setList();
                z1.button1.Hide();
                z1.Show();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedDrive = "";
            bool a = true;
            if (tabControl1.SelectedIndex == 0 && listBox1.SelectedIndex != -1)
            {
                selectedDrive = listBox1.GetItemText(listBox1.SelectedItem);
                selectedDrive = selectedDrive.Substring(selectedDrive.IndexOf("(") + 1, 3);
                a = true;
            }
            else if (tabControl1.SelectedIndex == 1 && listBox2.SelectedIndex != -1)
            {
                selectedDrive = listBox2.GetItemText(listBox2.SelectedItem);
                selectedDrive = selectedDrive.Substring(selectedDrive.IndexOf("(") + 1, 3);
                a = false;
            }
            else
            {
                MessageBox.Show("You must select a drive to remove");
            }
            if (selectedDrive != "")
            {
                DialogResult result = MessageBox.Show("This drive will no longer be registered with this program, continue?", "Drive Backup", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Enabled = false;
                    if (Directory.Exists(selectedDrive + WindowsIdentity.GetCurrent().Name.ToString() + "\\" + "Backups\\"))
                    {
                        Directory.Move(selectedDrive + WindowsIdentity.GetCurrent().Name.ToString() + "\\" + "Backups\\", selectedDrive+"Backups\\");
                    }
                    Directory.Delete(selectedDrive + WindowsIdentity.GetCurrent().Name.ToString() + "\\", true);
                    Properties.Settings.Default.warning = true;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("This drive is no longer associated with drive backup. All backup data (if existing) on the drive has been moved to " + selectedDrive+"\\Backups\\");
                    this.Close();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}