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
        static ConfigManager cm;
        static TabManager tm;
        static Win11Toolbar.ToobarForm _toolbarForm;
        static Win11Toolbar.ConfigurationForm _configForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NotifyIconForm());

            cm = ConfigManager.Instance;
            tm = TabManager.Instance;
            _toolbarForm = new Win11Toolbar.ToobarForm();
            _configForm = new Win11Toolbar.ConfigurationForm();

            _toolbarForm.Hide();
            _toolbarForm.LostFocus += _toolbarForm_LostFocus;
            NotifyIcon _notifiyIcon = new NotifyIcon();
            _notifiyIcon.MouseClick += _notifiyIcon_MouseClick;
            _notifiyIcon.Visible = true;
            _notifiyIcon.Icon = Win11Toolbar.Properties.Resources.turtle_shell;
            Application.Run();
        }

        private static void _notifiyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cursPos = Utilities.GetCursorPosition();
                if (_toolbarForm.form_X == 0)
                {
                    _toolbarForm.form_X = cursPos.X - (_toolbarForm.Width / 2);
                }
                _toolbarForm.Show();
            }
            else if (e.Button == MouseButtons.Right)
            {
                _configForm.Show();
            }
        }

        private static void _toolbarForm_LostFocus(object sender, EventArgs e)
        {
            Win11Toolbar.ToobarForm tf = (Win11Toolbar.ToobarForm)sender;
            tf.Hide();
        }
    }
}
