using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win11Toolbar.Core;
using Win11Toolbar.Properties;

namespace Win11Toolbar
{
    internal static class Program
    {
        static ConfigManager cm = ConfigManager.Instance;
        static TabManager tm = TabManager.Instance;
        static Win11Toolbar.ToobarForm _toolbarForm = new Win11Toolbar.ToobarForm();
        static Win11Toolbar.ConfigurationForm _configForm = new Win11Toolbar.ConfigurationForm();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NotifyIconForm());

            NotifyIcon _notifiyIcon = new NotifyIcon();
            //_notifiyIcon.Click += new EventHandler(_notifiyIcon_Click);
            _notifiyIcon.MouseClick += _notifiyIcon_MouseClick;
            _notifiyIcon.Visible = true;
            _notifiyIcon.Icon = Win11Toolbar.Properties.Resources.turtle_shell;
            Application.Run();
        }

        private static void _notifiyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _toolbarForm.Show();
            }
            else if (e.Button == MouseButtons.Right)
            {
                _configForm.Show();
            }
            throw new NotImplementedException();
        }

        /*
        public static Icon GetIcons()
        {
            string path = @"C:\Users\casdiem2\Desktop\PS";
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
            
            return System.Drawing.Icon.ExtractAssociatedIcon(files[0]);
        }
        */
    }
}
