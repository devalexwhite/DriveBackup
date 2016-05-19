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
    public partial class Form2 : Form
    {
        public string selectedDrive = null;
        public string[] bFiles = new string[100];
        public int index = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedDrive = "";
            selectDrive z1 = new selectDrive();
            z1.button2.Hide();
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
            label1.Text = "Drive selected:"+selectedDrive;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            int xx=0;
            String[] x = openFileDialog1.FileNames;
            if (x != null)
            {
                while (xx < openFileDialog1.FileNames.Length)
                {
                    bFiles[index] = x[xx];
                    xx++;
                    index++;
                }
                setList();
            }
           
        }
        public void setList()
        {
            this.listBox1.Items.Clear();
            int xx = 0;
            while (xx < index)
            {
                this.listBox1.Items.Add(Convert.ToString(this.bFiles[xx]));
                xx++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int xx=0;
            int sel=0;
            int ixx = 0;
            sel = listBox1.SelectedIndex;
            string[] temp = new string[100];
            while (xx < bFiles.Length)
            {
                if (xx != sel)
                {
                    temp[ixx] = bFiles[xx];
                    ixx++;
                }
                xx++;
            }
            xx = 0;
            while (xx < temp.Length)
            {
                bFiles[xx] = temp[xx];
                xx++;
            }
            index--;
            setList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool cont = true;
            if (selectedDrive == null)
            {
                MessageBox.Show("Please select a backup drive!");
                cont = false;
            }
            if (Convert.ToString(bFiles[0])==null)
            {
                MessageBox.Show("You must add at least one file to backup!");
                cont = false;
            }
            if (cont == true)
            {
                if (File.Exists(selectedDrive+"drivebackup.data"))
                {
                    MessageBox.Show("This drive has already been set up, please go to the \"Edit Backup Drive\" menu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                        Directory.CreateDirectory(selectedDrive + WindowsIdentity.GetCurrent().Name.ToString() + "\\");
                    using (StreamWriter sw = File.CreateText(selectedDrive+WindowsIdentity.GetCurrent().Name.ToString()+"\\driveBackup.data"))
                    {
                        int xx = 0;
                        while (xx < bFiles.Length)
                        {
                            sw.WriteLine(Convert.ToString(bFiles[xx]));
                            xx++;
                        }
                        sw.Close();
                    }
                    using (StreamWriter sw = File.CreateText(selectedDrive +WindowsIdentity.GetCurrent().Name.ToString()+"\\www.mark"))
                    {
                        sw.Close();
                    }
                    MessageBox.Show(selectedDrive+" is now registered as a backup drive!");
                    Program.a.aS("Added " + selectedDrive + " as backup drive");
                    this.Close();
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog a = new FolderBrowserDialog();
            a.ShowNewFolderButton = false;
            a.Description = "Select a directory";
            a.ShowDialog();
            string zz=null;
            zz = a.SelectedPath;
            if (zz != null)
            {
                zz = a.SelectedPath;
                bFiles[index] = zz + "*";
                index++;
                setList();
            }
        }

    }
}
