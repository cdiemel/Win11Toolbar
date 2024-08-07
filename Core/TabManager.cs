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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace Win11Toolbar
{
    internal class TabManager
    {
        public List<TabCommandPanel> TabViews;
        public Dictionary<string,TabCommandPanel> TabViewsDict;
        private List<Label> TabButtons;
        private string _activeTab;
        private Panel _mainPanel;
        private FlowLayoutPanel _internalFlowPanel;

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
                Debug.WriteLine($"ActiveTab:{value}");
                if (this._internalFlowPanel != null)
                {
                    //foreach (TabCommandPanel t in this.TabViews) { t.Panel.Hide(); }
                    if (this.TabViewsDict.TryGetValue(value, out TabCommandPanel tcp))
                    {
                        //tcp.Panel.Show();
                        //tcp.Build();
                        tcp.Build(this._internalFlowPanel);
                    }
                    else
                    {
                        //this.TabViews[0].Panel.Show();
                        //this.TabViews[0].Build();
                        this.TabViews[0].Build(this._internalFlowPanel);
                    }
                }
                this.InvalidateButtons();
            }
        }
        public Panel MainPanel
        {
            get
            {
                return this._mainPanel;
            }
            set
            {
                this._mainPanel = value;
                Debug.WriteLine($"_internalFlowPanel:{value.Name}");
                this._internalFlowPanel = this._buildInternalPanel();
                this._mainPanel.Controls.Add(this._internalFlowPanel);
                this._mainPanel.Controls.AddRange(this.TabButtons.ToArray());
                TabManager.Instance.ActiveTab = "TabButton1";
            }
        }
        /*
        public void AddTab(Panel MainPanel, string Path, Label TabButton)
        {
            Debug.WriteLine($"AddTab: {Path}");
            TabCommandPanel _tcp = new TabCommandPanel(MainPanel, Path);
            this.TabViews.Add(_tcp);
            this.TabViewsDict.Add(TabButton.Name, _tcp);
            this.TabButtons.Add(TabButton);
            this.MaxHeight = (this.MaxHeight > _tcp.Height) ? this.MaxHeight : _tcp.Height;
            this.MaxWidth = (this.MaxWidth > _tcp.Width) ? this.MaxWidth : _tcp.Width;
        }
        */
        public void AddTab(int Index, string Path)
        {
            Debug.WriteLine($"AddTab: {Path}");
            // Need to make TabButton
            //TabCommandPanel _tcp = new TabCommandPanel(this._internalFlowPanel, Path);
            TabCommandPanel _tcp = new TabCommandPanel( Path);
            Label b = this._buildTabButton(Index);
            if (this._internalFlowPanel != null) { this._internalFlowPanel.Controls.Add(b); }
            this.TabViews.Add(_tcp);
            if (this.TabViewsDict.ContainsKey(b.Name)) { this.TabViewsDict[""] = _tcp; }
            else { this.TabViewsDict.Add(b.Name, _tcp); }
            if (!this.TabButtons.Contains(b)) { this.TabButtons.Add(b); }
            this.MaxHeight = (this.MaxHeight > _tcp.Height) ? this.MaxHeight : _tcp.Height;
            this.MaxWidth = (this.MaxWidth > _tcp.Width) ? this.MaxWidth : _tcp.Width;
        }
        public void UpdateTab(int TabID, string ToolbarPath)
        {
            Debug.WriteLine($"UpdateTab: {TabID}|{ToolbarPath}|{this.TabViews.Count}");
            if (this.TabViews.Count < TabID) { throw new NotImplementedException(); }
            this.TabViews[TabID].UpdateFolder(ToolbarPath);
            if (this.TabButtons[TabID].Name == this.ActiveTab)
            {
                Console.WriteLine($"{TabID}|{ToolbarPath}|{this.TabViews.Count}");
                Console.WriteLine(this.TabButtons[TabID].Name);
                Console.WriteLine(this.ActiveTab);
                this.TabViews[TabID].Panel.Hide();
                this.TabViews[TabID].Build(this._internalFlowPanel);
                this.TabViews[TabID].Panel.Show();
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

        private FlowLayoutPanel _buildInternalPanel()
        {
            // 
            // InternalFlowPanel1
            // 
            FlowLayoutPanel InternalFlowPanel = new FlowLayoutPanel();
            InternalFlowPanel.BackColor = Utilities.GetTheme().Background;
            InternalFlowPanel.Location = new System.Drawing.Point(3, 25);
            InternalFlowPanel.Name = "InternalFlowPanel1";
            InternalFlowPanel.TabIndex = 0;
            return InternalFlowPanel;
        }

        private Label _buildTabButton(int Index)
        {
            Debug.WriteLine($"_buildTabButton");
            Label TabButton = new Label();
            TabButton.Location = new System.Drawing.Point(30*(Index-1), 0);
            TabButton.Size = new System.Drawing.Size(30, 20);
            TabButton.AutoSize = false;
            TabButton.ForeColor = System.Drawing.Color.FromArgb(156, 164, 169);
            TabButton.Name = $"TabButton{Index}";
            TabButton.TabIndex = Index;
            TabButton.Text = $"{Index}";
            TabButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            TabButton.Click += TabManager.TabButton_Click;
            TabButton.Paint += TabManager.TabButton_Paint;
            return TabButton;
        }

        public static void TabButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"TabButton_Click");
            Label s = (Label)sender;
            TabManager.Instance.ActiveTab = s.Name;
        }
        public static void TabButton_Paint(object sender, PaintEventArgs pe)
        {
            Debug.WriteLine($"TabButton_Paint");
            Label s = (Label)sender;
            Debug.WriteLine($"{s.Name}");
            if (s.Name != TabManager.Instance.ActiveTab) { return; }

            Graphics g = pe.Graphics;
            Brush b = new SolidBrush(Color.FromArgb(96, 205, 255));
            Rectangle r = new Rectangle(6, 18, 16, 2);
            g.FillRectangle(b, r);
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
        public TabCommandPanel(string Path)
        {
            this.ProcessFolder(Path);
        }
        public void Build(Panel MainPanel)
        {
            Debug.WriteLine($"Build:_internalFlowPanel");
            this.Panel = MainPanel;
            this.Panel.Controls.Clear();
            this.Panel.Dock = System.Windows.Forms.DockStyle.None;
            this.Panel.HorizontalScroll.Maximum = 0;
            this.Panel.AutoScroll = false;
            this.Panel.VerticalScroll.Visible = false;
            this.Panel.HorizontalScroll.Visible = false;
            this.Panel.AutoScroll = true;
            this.Panel.Height = this.Height;
            this.Panel.Width = this.Width;

            this.Build();
        }
        public void Build()
        {
            Debug.WriteLine($"Build");
            Debug.WriteLine("****** StartBuild");

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
            this.ProcessFolder(Path);
            //this.Build(this.Panel);
        }

        private void ProcessFolder(string path)
        {
            if (!Directory.Exists(path)) { return; }
            this._toolbarIcons.Clear();
            this._path = path;
            //Console.WriteLine($"ProcessFolder: {this.Panel.Height}");

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
        }
    }
}
