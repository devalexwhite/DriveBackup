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
    
    public partial class Form4 : Form
    {
        public string drive = null;
        public string[] bFiles = new string[100];
       
        public int index = 0;
       
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                listBox1.Items.Add("You must disable \"Backup entire drive\" to add files/directories");
                groupBox1.Enabled = false;
            }
            else if (checkBox1.Checked == false)
            {
                listBox1.Items.Clear();
                groupBox1.Enabled = true;
            }
        }
        public void setList()
        {
            this.listBox1.Items.Clear();
            int xx = 0;
            while (xx < index && bFiles[xx]!="EOF")
            {
                this.listBox1.Items.Add(Convert.ToString(this.bFiles[xx]));
                xx++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = drive;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            int xx=0;
            String[] x = openFileDialog1.FileNames;
            if (x != null && x[0]!="openFileDialog1")
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

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog a = new FolderBrowserDialog();
            a.SelectedPath = drive;
            a.ShowNewFolderButton = false;
            a.Description = "Select a directory";
            a.ShowDialog();
            string zz = null;
            zz = a.SelectedPath;
            if (zz != null)
            {
                bFiles[index] = zz + "*";
                index++;
                setList();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int xx = 0;
            int sel = 0;
            int ixx = 0;
            sel = listBox1.SelectedIndex;
            string[] temp = new string[100];
            while (xx<bFiles.Length)
            {
                if (xx != sel)
                {
                    temp[ixx] = bFiles[xx];
                    ixx++;
                }
                xx++;
            }
            xx = 0;
            while (xx < bFiles.Length)
            {
                bFiles[xx] = temp[xx];
                xx++;
            }
            index--;
            setList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog a = new FolderBrowserDialog();
            a.ShowNewFolderButton = false;
            a.Description = "Select a directory to store backups";
            a.SelectedPath = "c:\\";
            a.ShowDialog();
            string g1 = a.SelectedPath;
            string folder="";
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.Name.Contains(drive))
                {
                    folder = d.VolumeLabel;
                }
            }
            bool cont = true;
            if (Convert.ToString(bFiles[0]) == null && checkBox1.Checked==false)
            {
                MessageBox.Show("You must add at least one file to backup!");
                cont = false;
            }
            if (checkBox1.Checked == true)
            {
                
                string g2 = drive;
                g2+="*";
                string[] g3 = new string[100];
                g3.CopyTo(bFiles, 0);
                bFiles[0] = g2;
                index=1;
            }
            if (cont == true)
            {
                if (File.Exists(drive + WindowsIdentity.GetCurrent().Name.ToString()+ "revBack.data"))
                {
                    MessageBox.Show("This drive has already been set up, please go to the \"Edit Backup Drive\" menu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Directory.CreateDirectory(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\");
                    using (StreamWriter sw = File.CreateText(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\revBack.data"))
                    {
                        int xx = 0;
                        while (xx < bFiles.Length)
                        {
                            sw.WriteLine(Convert.ToString(bFiles[xx]));
                            xx++;
                        }
                        sw.Close();
                    }
                    using (StreamWriter sw = File.CreateText(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\www.mark"))
                    {
                        sw.Close();
                    }
                    using (StreamWriter sw = File.CreateText(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\revBackDir.data"))
                    {
                        if (g1.LastIndexOf("\\") != g1.Length)
                        {
                            g1+="\\";
                        }
                        g1 += folder + " backups" + "\\";
                        sw.WriteLine(g1);
                        sw.Close();
                    }

                    MessageBox.Show(drive + " will now be backed up whenever it is inserted");
                    Program.a.aS(drive + " is now a reverse backup drive");
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
