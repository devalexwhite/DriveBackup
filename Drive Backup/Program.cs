using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Drive_Backup
{
    static class Program
    {
        public static Form1 a;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            a = new Form1();
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool IsAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            if (IsAdmin == false)
            {
                MessageBox.Show("If you are running Vista/7 it is recommended you run this program as administrator", "Drive backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Run(a);

        }
    }
}
