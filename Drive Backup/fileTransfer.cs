using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
namespace Drive_Backup
{
    class Class1
    {
        public void transfer(bool rev)
        {
            string[] files = Class2.files;
            int num = Class2.fileCount;
            string date = Convert.ToString(DateTime.Today);
            date=date.Replace("/", ".");
            date=date.Substring(0,date.IndexOf(" "));
            string dest = "";
            if (rev == false)
            {
                dest = Class2.driveLetter + WindowsIdentity.GetCurrent().Name.ToString() + "\\Backups\\" + date + "\\";
            }
            else
            {
                using (StreamReader sr = File.OpenText(Class2.driveLetter + WindowsIdentity.GetCurrent().Name.ToString() + "\\revBackDir.data"))
                {
                    dest = sr.ReadLine() + date + "\\";
                    
                    sr.Close();
                }
            }
            if (Directory.Exists(dest))
            {
                Directory.Delete(dest, true);
            }
            Directory.CreateDirectory(dest);
            int x = 0;
            Form3 a = new Form3();
            a.Show();
            a.progressBar1.Minimum = 0;
            a.progressBar1.Maximum = num;
            a.progressBar1.Step = 1;
            while (x < num)
            {
                string name=null;
                name=files[x].Substring(files[x].LastIndexOf("\\")+1);
                string nn = dest + name;
                if (files[x] != "EOF")
                {
                    try
                    {
                        File.Copy(files[x], nn, true);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show("You do not have permission to access "+files[x].ToString(), "Drive Backup: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                x++;
                a.progressBar1.PerformStep();
            }
            a.Close();
        }
        public void readDB(bool load,bool rev)
        {
            string driveL = Class2.driveLetter;
            string path = "";
            if (rev==false)
            {
                path = driveL + WindowsIdentity.GetCurrent().Name.ToString() + "\\driveBackup.data";
            }
            else
            {
                path = driveL + WindowsIdentity.GetCurrent().Name.ToString() + "\\revBack.data";
            }
            using (StreamReader sr = File.OpenText(path))
            {
                String input;
                int x = 0;
                //string[] fils=null;
                System.Collections.ArrayList al = new System.Collections.ArrayList();
                while ((input = sr.ReadLine()) != "" && input!=null && input!=" ")
                {
                    if (input.Contains("*") && load==false)
                    {
                        string path1 = input.Substring(0,input.Length-1);
                        try
                        {
                            string[] filePaths = Directory.GetFiles(@path1, "*.*",
                                                  SearchOption.AllDirectories);


                            int x3 = 0;
                            while (x3 < filePaths.Length)
                            {
                                //fils[x] = filePaths[x3];
                                al.Add(filePaths[x3]);
                                x3++;
                                x++;
                            }
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            MessageBox.Show("You do not have permission to access " + path1, "Drive Backup: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if(!input.Contains("*") || load==true)
                    {
                        //fils[x] = input;
                        al.Add(input);
                        x++;
                    }
                }
                //fils[x++] = "EOF";
                al.Add("EOF");
                sr.Close();
                Class2.files = al.ToArray(typeof(string)) as string[];
                Class2.fileCount = x;
            }
        }
    }
}