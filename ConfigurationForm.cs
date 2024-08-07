using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win11Toolbar.Core;

namespace Win11Toolbar
{
    public partial class ConfigurationForm : Form
    {
        private bool _closeHover = false;
        private bool mouseDown;
        private Point lastLocation;
        private Rectangle rl;
        private Rectangle rr;
        private Rectangle rt;
        private Rectangle rb;
        public ConfigurationForm()
        {
            InitializeComponent();
            this.Icon = Utilities.Bitmap2Icon(global::Win11Toolbar.Properties.Resources.Settings);
        }
        /*
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        */

        private void ConfigurationForm_Paint(object sender, PaintEventArgs pe)
        {
            if (!mouseDown)
            {
                Win11Toolbar.ConfigurationForm s = (Win11Toolbar.ConfigurationForm)sender;
                Graphics g = pe.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                TextureBrush tBrush = new TextureBrush(new Bitmap(1, 1));
                Rectangle r = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
                using (var bmp = new Bitmap(r.Width, r.Height))
                using (var graphics = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(this.Location, Point.Empty, s.ClientRectangle.Size);
                    tBrush = new TextureBrush((Image)bmp);
                }
                //g.FillRectangle(tBrush, s.ClientRectangle);
            }
        }

        //
        // PAINT OFFSETS
        //
        private int off_X = 1;
        private int off_Y = 1;
        private int off_H = -1;
        private int off_W = -1;
        private int pen_S = 1;


        private void BackPanel_Paint(object sender, PaintEventArgs pe)
        {
            Panel s = (Panel)sender;
            s.BackColor = Color.Transparent;
            int CornerRadius = 8;
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b_back = new SolidBrush(Utilities.GetTheme().Background);
            Rectangle r_back = new Rectangle(1, 1, s.ClientRectangle.Width-2, s.ClientRectangle.Height-2);
            CustomGraphics.FillRoundedRectangle(g, b_back, r_back, CornerRadius);

            //
            //
            Pen p_back = new Pen(Utilities.GetTheme().Highlight, pen_S);
            //// new Padding(Left,Top,Right,Bottom)
            //Padding sides = new Padding(1, 1, 1, 1);
            //// new Padding(TopLeft,TopRight,BottomRight,BottomLeft)
            //Padding corners = new Padding(16, 16, 8, 8);
            //CustomGraphics.DrawPartialRoundedBorder(g, p_back, new Rectangle(r_back.X + off_X, r_back.Y + off_Y, r_back.Width + off_W, r_back.Height + off_H), sides, corners);
            //CustomGraphics.DrawRoundedRectangle(g, p_back, new Rectangle(r_back.X + off_X, r_back.Y + off_Y, r_back.Width + off_W, r_back.Height + off_H),8);
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs pe)
        {
            Panel s = (Panel)sender;
            //s.BackColor = Color.Transparent;
            int CornerRadius = 4;
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b_back = new SolidBrush(Utilities.GetTheme().Highlight);
            Rectangle r_back = new Rectangle(0, 0, s.ClientRectangle.Width - 1, s.ClientRectangle.Height - 1);
            CustomGraphics.FillRoundedRectangle(g, b_back, r_back, CornerRadius);
        }


        private void TopGrabPanel_Paint(object sender, PaintEventArgs pe)
        {
            Panel s = (Panel)sender;
            //s.BackColor = Color.Transparent;
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = new SolidBrush(Utilities.GetTheme().Background);
            Rectangle r = new Rectangle(0, 0, s.Width, s.Height);

            CustomGraphics.FillRoundedRectangle(g, b, r, 8,8,0,0);

            //
            //
            Pen p_back = new Pen(Utilities.GetTheme().Highlight, pen_S);
            // new Padding(Left,Top,Right,Bottom)
            Padding sides = new Padding(1, 1, 1, -1);
            // new Padding(TopLeft,TopRight,BottomRight,BottomLeft)
            Padding corners = new Padding(8, 8, -1, -1);
            //CustomGraphics.DrawPartialRoundedBorder(g, p_back, new Rectangle(r.X + off_X, r.Y + off_Y, r.Width + off_W, r.Height + off_H), sides, corners);
        }

        private void CloseButton_Paint(object sender, PaintEventArgs pe)
        {
            Label s = (Label)sender;
            Graphics g = pe.Graphics;
            //s.BackColor = Color.Transparent;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = new SolidBrush(Utilities.GetTheme().Background);
            if(_closeHover) { b = new SolidBrush(Color.FromArgb(196, 43, 28)); }
            Rectangle r = new Rectangle(-1, -1, s.Width+1, s.Height+1);
            CustomGraphics.FillRoundedRectangle(g, b, r, 0, 8, 0, 0);


            Font f = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Brush fb = new SolidBrush(Utilities.GetTheme().Text);
            g.DrawString("X", f, fb, new Point(7,5));

            //
            //
            Pen p_back = new Pen(Utilities.GetTheme().Background, pen_S);
            if (_closeHover) { p_back = new Pen(Color.FromArgb(196, 43, 28), pen_S); }
            // new Padding(Left,Top,Right,Bottom)
            Padding sides = new Padding(1, 1, 1, 1);
            // new Padding(TopLeft,TopRight,BottomRight,BottomLeft)
            Padding corners = new Padding(-1, 8, -1, -1);
            //CustomGraphics.DrawPartialRoundedBorder(g, p_back, new Rectangle(r.X + off_X-1, r.Y + off_Y-1, r.Width + off_W-1, r.Height + off_H), sides, corners);

        }

        private void AllButton_Paint(object sender, PaintEventArgs pe)
        {
            Button s = (Button)sender;
            //s.BackColor = Color.Transparent;
            int radius = 4;
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush b = new SolidBrush(Color.CadetBlue);
            Rectangle r = new Rectangle(0, 0, s.Width-1, s.Height-1);
            CustomGraphics.FillRoundedRectangle(g, b, r, radius);


            Font f = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Brush fb = new SolidBrush(Utilities.GetTheme().Text);
            g.DrawString(s.Text, f, fb, new Point(4, 5));
        }

        //
        // Begin Drag-n-Move
        //
        private void TopGrabPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
            rl = new Rectangle(0, -2, 5, this.Height+4);
            rr = new Rectangle(this.Width - 5, -2, 5, this.Height+4);
            rt = new Rectangle(-2, 0, this.Width+4, 5);
            rb = new Rectangle(-2, this.Height - 5, this.Width+4, 5);
        }

        private void TopGrabPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                //this.Invalidate(rl);
                //this.Invalidate(rr);
                //this.Invalidate(rt);
                //this.Invalidate(rb);
                this.Update();
            }
        }
        private void TopGrabPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            //this.Invalidate(false);
            this.Invalidate(rl);
            this.Invalidate(rr);
            this.Invalidate(rt);
            this.Invalidate(rb);
            this.Update();
        }
        //
        // End Drag-n-Move
        //

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            string p1 = this.Tab1FolderPathLabel.Text;
            string p2 = this.Tab2FolderPathLabel.Text;
            ConfigManager.Instance.ToolbarPaths = new string[] { p1, p2 };
            ConfigManager.Instance.UpdateConfig();
            this.Close();
        }

        private void FolderSelectButton_Click(object sender, System.EventArgs e, int Tab)
        {
            Button s = (Button)sender;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.MyDocuments;
            fbd.ShowDialog();
            Debug.WriteLine(fbd.SelectedPath);
            Control[] _fpLabels = this.Controls.Find($"Tab{Tab}FolderPathLabel",true);
            Debug.WriteLine(_fpLabels.Length);
            _fpLabels[0].Text = fbd.SelectedPath;
            ConfigManager.Instance.ToolbarPaths[Tab] = fbd.SelectedPath;
        }
    }

}
