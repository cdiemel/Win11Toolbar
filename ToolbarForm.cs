using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Win11Toolbar.Utilities;
using static Win11Toolbar.Utilities.Win11Theme;

namespace Win11Toolbar
{
    public partial class ToobarForm : Form
    {
        Utilities.Win11Theme Theme;
        public ToobarForm()
        {
            Theme = Utilities.GetTheme();

            Console.WriteLine(Theme.Background.ToString());

            InitializeComponent();
            this.LostFocus += Form1_LostFocus;
            //this.Hide();
        }

        /*
        private void notifyIcon_Click(object sender, System.EventArgs e)
        {
            Point cursPos = Utilities.GetCursorPosition();
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            int top = r.Height - this.Height - 10;
            int left = cursPos.X - (this.Width / 2);
            this.Top = top;
            this.Left = left;
            this.Show();
            this.Activate();
        }
        */
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ConfigurationForm cf = new ConfigurationForm();
            cf.Show();
        }

        private void Form1_LostFocus(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BackgroundPanel_Paint(object sender, PaintEventArgs pe)
        {
            Panel p = (Panel)sender;
            
            int CornerRadius = 8;
            Graphics g = pe.Graphics;
            Brush b = new SolidBrush(Theme.Background);
            TextureBrush tBrush = new TextureBrush(new Bitmap(1, 1));
            Rectangle r = new Rectangle(0, 0, this.Width - 7, this.Height - 7);
            using (var bmp = new Bitmap(r.Width, r.Height))
            using (var graphics = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(p.ClientRectangle.Location, Point.Empty, p.ClientRectangle.Size);
                tBrush = new TextureBrush((Image)bmp);
            }

            //CustomGraphics.FillRoundedRectangle(g, b, r, CornerRadius);
            CustomGraphics.FillRoundedRectangle(g, tBrush, r, CornerRadius);
        }

        private void MainWindow_Paint(object sender, PaintEventArgs pe)
        {
            Win11Toolbar.ToobarForm p = (Win11Toolbar.ToobarForm)sender;
            int CornerRadius = 8;
            Graphics g = pe.Graphics;
            Brush b = new SolidBrush(Theme.Background);
            Rectangle r = new Rectangle(0, 0, this.Width - 7, this.Height - 7);

            CustomGraphics.FillRoundedRectangle(g, b, r, CornerRadius);
            //CustomGraphics.FillRoundedRectangle(g, b, p.ClientRectangle, CornerRadius);

            /*
            Console.WriteLine(sender.ToString());
            Win11Toolbar.ToobarForm p = (Win11Toolbar.ToobarForm)sender;
            Console.WriteLine(p.ClientRectangle.ToString());
            Console.WriteLine(p.Location.ToString());

            int CornerRadius = 8;
            Graphics g = pe.Graphics;
            //Brush b = new SolidBrush(Theme.Background);
            TextureBrush tBrush = new TextureBrush(new Bitmap(1, 1));
            Rectangle r = new Rectangle(0, 0, this.Width - 7, this.Height - 7);
            using (var bmp = new Bitmap(r.Width, r.Height))
            using (var graphics = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(p.Location, Point.Empty, p.ClientRectangle.Size);
                Image bmp2 = Utilities.Blur(bmp, 1);
                tBrush = new TextureBrush(bmp2);
            }

            CustomGraphics.FillRoundedRectangle(g, tBrush, r, CornerRadius);
            */
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            Label s = (Label)sender;
            TabManager.Instance.ActiveTab = s.Name;
            //TabManager.Instance.InvalidateButtons();

        }
        private void TabButton_Paint(object sender, PaintEventArgs pe)
        {
            Label s = (Label)sender;
            if (s.Name != TabManager.Instance.ActiveTab) { return; }

            Graphics g = pe.Graphics;
            Brush b = new SolidBrush(Color.FromArgb(96, 205, 255));
            Rectangle r = new Rectangle(6, 18, 16, 2);
            g.FillRectangle(b, r);
        }
    }
}
