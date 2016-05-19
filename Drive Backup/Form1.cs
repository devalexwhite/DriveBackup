using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dolinay;
using System.Security.Principal;
using System.IO;
using System.Diagnostics;
using System.Net;

        //____________________________
        //| Created By:Alex White    | 
        //| Date:2009                |
        //| http://awnet.homedns.org |
        //----------------------------

namespace Drive_Backup
{
    public partial class Form1 : Form
    {
        int appVersion = 01;
        string drivex = null;
        bool addey = false;
        private DriveDetector driveDetector = null;
        public Form1()
        {
            InitializeComponent();
            checkUpdates();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            aS("Program started");
            this.Resize += new EventHandler(Form1_Resize);
            notifyIcon1.BalloonTipClicked += new EventHandler(NotifyIcon1_BalloonTipClicked);
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            this.Show();
            driveDetector = new DriveDetector();
            driveDetector.DeviceArrived += new DriveDetectorEventHandler(
                OnDriveArrived);
            if (Properties.Settings.Default.runner == true)
            {

                this.ShowInTaskbar = true;
                WindowState = FormWindowState.Normal;
                this.Show();
                Form5 b = new Form5();
                b.Show();
                b.ShowInTaskbar = true;
                b.Focus();
            }
            if (Properties.Settings.Default.register == true && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                Form7 a = new Form7();
                a.Show();
                Properties.Settings.Default.register = false;
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.runs += 1;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.runs == 5)
            {
                Survey a1 = new Survey();
                a1.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //version check
        private void checkUpdates()
        {
            bool connection = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (connection)
            {
                string scriptLoc = "http://awnet.homedns.org/versionCheck.php?app=driveBackup&vers=" + Convert.ToString(appVersion);
                string result = null;
                try
                {
                    WebClient client = new WebClient();
                    result = client.DownloadString(scriptLoc);
                    if (result.Contains("TRUE"))
                    {
                        MessageBox.Show("An updated version of Drive Backup is avaible at http://awnet.homedns.org");
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }
        //Handels drive discovery
        private void OnDriveArrived(object sender, DriveDetectorEventArgs e)
        {
            string drive = driveDetector.HookedDrive;
            if (drive != "")
            {
                cRD(drive);
            }
            e.HookQueryRemove = true;
        }
        //Checks if drive has been marked for backup
        private void cRD(string drive)
        {
            if (!File.Exists(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\www.mark"))
            {
                aS("Non intialized drive detected");
                notifyIcon1.BalloonTipTitle = "Drive Backup";
                notifyIcon1.BalloonTipText = "The current user is not configured to backup with this drive. Click here to solve this problem";
                notifyIcon1.ShowBalloonTip(500);
                addey = true;
                drivex = drive;
            }
            else if (File.Exists(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\driveBackup.data"))
            {
                aS("Backup drive found");
                notifyIcon1.BalloonTipTitle = "Drive Backup";
                notifyIcon1.BalloonTipText = "Backup drive found, starting backup";
                notifyIcon1.ShowBalloonTip(500);
                Class1 a = new Class1();
                Class2.driveLetter = drive;
                aS("Backup started");
                a.readDB(false, false);
                a.transfer(false);
                aS("Backup finished");
            }
            if (File.Exists(drive + WindowsIdentity.GetCurrent().Name.ToString() + "\\revBack.data"))
            {
                aS("Reverse backup drive found!");
                notifyIcon1.BalloonTipTitle = "Drive Backup";
                notifyIcon1.BalloonTipText = "Backing up USB drive";
                notifyIcon1.ShowBalloonTip(500);
                aS("Backup started");
                Class1 a = new Class1();
                Class2.driveLetter = drive;
                a.readDB(false, true);
                a.transfer(true);
                aS("Backup finished");

            }
        }
        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (addey == false)
            {
                Show();
                WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                Refresh();
            }
            else
            {
                Form2 a = new Form2();
                a.Show();
                a.selectedDrive = drivex;
                a.label1.Text = "Selected drive:" + drivex;
            }

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            Refresh();
        }
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {

                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            Refresh();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.Close();
            }
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            this.ShowInTaskbar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addSelector a = new addSelector();
            a.Show();
        }
        public void aS(string message)
        {
            string old = textBox1.Text;
            if (textBox1.Text != "")
            {
                textBox1.Text = old + Environment.NewLine + DateTime.Now.ToString() + ": " + message;
            }
            else
            {
                textBox1.Text = DateTime.Now.ToString() + ": " + message;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void addDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSelector a = new addSelector();
            a.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ee = textBox1.Text;
            MessageBox.Show("Created by Alex White 2009 (http://awnet.homedns.org). Uses code created by Jan Dolinay (http://www.codeproject.com/KB/system/DriveDetector.aspx) liscensed under the CPOL    V.21001", "Drive Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form6 z9 = new Form6();
            z9.Show();
        }

        private void configureDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 z9 = new Form6();
            z9.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://awnet.homedns.org");
            Process.Start(sInfo);
        }
        private void Closer(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure you want to exit", "Drive Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Hide();
            this.ShowInTaskbar = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click_2(object sender, EventArgs e)
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
            }
        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            this.ShowInTaskbar = false;
            e.Cancel = true;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://awnet.homedns.org");
            Process.Start(sInfo);

        }
    }
}
