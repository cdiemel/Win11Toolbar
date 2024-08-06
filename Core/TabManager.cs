using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using Image = System.Drawing.Image;

namespace Win11Toolbar
{
    internal class TabManager
    {
        public List<TabCommandPanel> TabViews;
        public Dictionary<string,TabCommandPanel> TabViewsDict;
        private List<Label> TabButtons;
        private string _activeTab;

        public int MaxHeight = 130;
        public int MaxWidth = 215;

        //
        // Start Singleton Magice
        //
        private static TabManager instance;
        public static TabManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TabManager();
                return instance;
            }
        }
        //
        // End Singleton Magic
        //
        private TabManager()
        {
            this.TabViews = new List<TabCommandPanel>();
            this.TabViewsDict = new Dictionary<string, TabCommandPanel>();
            this.TabButtons = new List<Label>();
        }
        public string ActiveTab
        {
            get
            {
                return this._activeTab;
            }
            set
            {
                this._activeTab = value;
                //foreach (TabCommandPanel t in this.TabViews) { t.Panel.Hide(); }
                if (this.TabViewsDict.TryGetValue(value, out TabCommandPanel tcp))
                {
                    tcp.Panel.Show();
                    tcp.Build();
                } else
                {
                    this.TabViews[0].Panel.Show();
                    this.TabViews[0].Build();
                }
                this.InvalidateButtons();
            }
        }
        public void AddTab(Panel MainPanel, string Path, Label TabButton)
        {
            TabCommandPanel _tcp = new TabCommandPanel(MainPanel, Path);
            this.TabViews.Add(_tcp);
            this.TabViewsDict.Add(TabButton.Name, _tcp);
            this.TabButtons.Add(TabButton);
            this.MaxHeight = (this.MaxHeight > _tcp.Height) ? this.MaxHeight : _tcp.Height;
            this.MaxWidth = (this.MaxWidth > _tcp.Width) ? this.MaxWidth : _tcp.Width;
        }
        public void UpdateTab(int TabID, string ToolbarPath)
        {
            Console.WriteLine($"{TabID}|{ToolbarPath}|{this.TabViews.Count}");
            if (this.TabViews.Count < TabID) { throw new NotImplementedException(); }
            this.TabViews[TabID].UpdateFolder(ToolbarPath);
            if (this.TabButtons[TabID].Name == this.ActiveTab)
            {
                Console.WriteLine($"{TabID}|{ToolbarPath}|{this.TabViews.Count}");
                Console.WriteLine(this.TabButtons[TabID].Name);
                Console.WriteLine(this.ActiveTab);
                this.TabViews[TabID].Panel.Hide();
                this.TabViews[TabID].Build();
                this.TabViews[TabID].Panel.Hide();
                this.ActiveTab = this._activeTab;
            }
        }
        public void InvalidateButtons()
        {
            foreach (Label l in this.TabButtons)
            {
                l.Invalidate();
            }
        }
    }
    internal class TabCommandPanel
    {
        public Panel Panel;
        public int Height;
        public int Width;
        private Size _iconSize = new Size(16, 16);
        private List<FlowLayoutPanel> _items = new List<FlowLayoutPanel>();
        private string _path = "";
        private Dictionary<string, Image> _toolbarIcons = new Dictionary<string, Image>();

        private Font _font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular);

        private Utilities.Win11Theme _theme = Utilities.GetTheme();

        public TabCommandPanel(Panel MainPanel)
        {
            this.Panel = MainPanel;
            //this.ProcessFolder();
        }
        public TabCommandPanel(Panel MainPanel, string Path)
        {
            this.Panel = MainPanel;
            this.ProcessFolder(Path);
        }
        public void Build()
        {
            Debug.WriteLine("****** StartBuild");
            this.Panel.Controls.Clear();
            foreach (KeyValuePair<string,Image> icon in this._toolbarIcons)
            {

                Console.WriteLine(icon.Key);

                FlowLayoutPanel flp = new FlowLayoutPanel();
                PictureBox pb = new PictureBox();
                Label nameLabel = new Label();

                flp.Height = 24;
                flp.Width = this.Width;
                flp.BackColor = _theme.Background;
                flp.Margin = new Padding(0);


                pb.Image = icon.Value;
                pb.Size = new Size(20, 24);
                pb.Margin = new Padding(4, 3, 0, 2);
                pb.MouseLeave += (o, e) => { flp.BackColor = _theme.Background; };
                pb.MouseEnter += (o, e) => { flp.BackColor = _theme.Highlight; };

                nameLabel.Text = Path.GetFileNameWithoutExtension(icon.Key);
                nameLabel.AutoEllipsis = true;
                nameLabel.Size = new Size(flp.Width - 24, 24);
                nameLabel.Size = new Size(this.Width - 24, 24);
                nameLabel.AutoSize = false;
                nameLabel.Margin = new Padding(0);
                nameLabel.Font = this._font;
                nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                nameLabel.ForeColor = _theme.Text;
                nameLabel.Click += (o, e) => {
                    Process p = Process.Start(icon.Key);
                };
                nameLabel.MouseLeave += (o, e) => { flp.BackColor = _theme.Background; };
                nameLabel.MouseEnter += (o, e) => { flp.BackColor = _theme.Highlight; };

                flp.Controls.Add(pb);
                flp.Controls.Add(nameLabel);
                this.Panel.Controls.Add(flp);
            }
            Debug.WriteLine("****** EndBuild");
            /*
            foreach (string file in files)
            {
                Console.WriteLine(file);
                // Get the name
                string name = Path.GetFileNameWithoutExtension(file);

                // Get the icon/image
                Image i = Utilities.Exe2Icon2Image(file, this._iconSize);

                FlowLayoutPanel flp = new FlowLayoutPanel();
                PictureBox pb = new PictureBox();
                Label nameLabel = new Label();

                flp.Height = 24;
                flp.Width = this.Width;
                flp.BackColor = _theme.Background;
                flp.Margin = new Padding(0);


                pb.Image = i;
                pb.Size = new Size(20, 24);
                pb.Margin = new Padding(4, 3, 0, 2);
                pb.MouseLeave += (o, e) => { flp.BackColor = _theme.Background; };
                pb.MouseEnter += (o, e) => { flp.BackColor = _theme.Highlight; };

                nameLabel.Text = name;
                nameLabel.AutoEllipsis = true;
                nameLabel.Size = new Size(flp.Width - 24, 24);
                nameLabel.Size = new Size(this.Width - 24, 24);
                nameLabel.AutoSize = false;
                nameLabel.Margin = new Padding(0);
                nameLabel.Font = this._font;
                nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                nameLabel.ForeColor = _theme.Text;
                nameLabel.Click += (o, e) => {
                    Console.WriteLine(file);
                    Process p = Process.Start(file);
                };
                nameLabel.MouseLeave += (o, e) => { flp.BackColor = _theme.Background; };
                nameLabel.MouseEnter += (o, e) => { flp.BackColor = _theme.Highlight; };

                flp.Controls.Add(pb);
                flp.Controls.Add(nameLabel);
                this.Panel.Controls.Add(flp);
            }
            //*/
        }
        public void UpdateFolder(string Path)
        {
            this.Panel.Controls.Clear();
            this.ProcessFolder(Path);
            this.Build();
        }

        private void ProcessFolder(string path)
        {
            if (!Directory.Exists(path)) { return; }
            this._toolbarIcons.Clear();
            this._path = path;

            this.Panel.Dock = System.Windows.Forms.DockStyle.None;
            this.Panel.HorizontalScroll.Maximum = 0;
            this.Panel.AutoScroll = false;
            this.Panel.VerticalScroll.Visible = false;
            this.Panel.HorizontalScroll.Visible = false;
            this.Panel.AutoScroll = true;

            Console.WriteLine($"ProcessFolder: {this.Panel.Height}");

            string[] files = Directory.GetFiles(path);
            int _rows = 0;
            int _maxWidth = 215;

            foreach (string file in files)
            {
                //Console.WriteLine(file);
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(Path.GetFileNameWithoutExtension(file), this._font);
                    //Console.WriteLine(size.Width);
                    //Console.WriteLine(((int)size.Width) + 1);
                    int _rndWid = ((int)size.Width) + 1;
                    _maxWidth = _rndWid > _maxWidth ? _rndWid : _maxWidth;
                }
                // Get the icon/image
                Image i = Utilities.Exe2Icon2Image(file, this._iconSize);
                this._toolbarIcons.Add(file, i);
                _rows++;
            }

            this.Height = (_rows) * 25;
            this.Width = _maxWidth + 20;
            this.Panel.Height = this.Height;
            this.Panel.Width = this.Width;
        }
    }
}
