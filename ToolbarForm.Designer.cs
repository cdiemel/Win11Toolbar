using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Win11Toolbar.Core;
using Button = System.Windows.Forms.Button;
using ListView = System.Windows.Forms.ListView;

namespace Win11Toolbar
{
    partial class ToobarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        //private System.Drawing.Color darkGrey = System.Drawing.Color.FromArgb(41, 48, 53);
        //private System.Drawing.Color lightGrey = System.Drawing.Color.FromArgb(56, 64, 69);
        //private System.Drawing.Color lightGrey = System.Drawing.Color.FromArgb(41, 48, 53);

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToobarForm));
            //this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.InternalFlowPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.InternalFlowPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.TabButton1 = new System.Windows.Forms.Label();
            this.TabButton2 = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.PictureBox();
            this.Tab1ListView = new System.Windows.Forms.ListView();
            this.BackgroundPanel = new System.Windows.Forms.Panel();
            this.BackgroundPanel.SuspendLayout();
            this.SuspendLayout();
            /*
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIcon.BalloonTipText = "Text";
            this.notifyIcon.BalloonTipTitle = "title";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += notifyIcon_Click;
            */
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(200, 0);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(20, 20);
            this.SettingsButton.TabIndex = 0;
            this.SettingsButton.TabStop = false;
            this.SettingsButton.Image = Utilities.Resize(global::Win11Toolbar.Properties.Resources.Settings, new Size(16, 16));
            this.SettingsButton.Click += SettingsButton_Click;
            // 
            // TabButton1
            // 
            this.TabButton1.Location = new System.Drawing.Point(0, 0);
            this.TabButton1.Size = new System.Drawing.Size(30, 20);
            this.TabButton1.AutoSize = false;
            this.TabButton1.ForeColor = System.Drawing.Color.FromArgb(156, 164, 169);
            this.TabButton1.Name = "TabButton1";
            this.TabButton1.TabIndex = 1;
            this.TabButton1.Text = "1";
            this.TabButton1.TextAlign = ContentAlignment.MiddleCenter;
            //this.TabButton1.BackColor = Color.Blue;
            this.TabButton1.Click += TabButton_Click;
            this.TabButton1.Paint += TabButton_Paint;
            // 
            // TabButton2
            // 
            this.TabButton2.Location = new System.Drawing.Point(30, 0);
            this.TabButton2.Size = new System.Drawing.Size(30, 20);
            this.TabButton2.AutoSize = false;
            this.TabButton2.ForeColor = System.Drawing.Color.FromArgb(156, 164, 169);
            this.TabButton2.Name = "TabButton2";
            this.TabButton2.TabIndex = 2;
            this.TabButton2.Text = "2";
            this.TabButton2.TextAlign = ContentAlignment.MiddleCenter;
            this.TabButton2.Click += TabButton_Click;
            this.TabButton2.Paint += TabButton_Paint;

            /*
            string[] paths = ConfigManager.Instance.ToolbarPaths;
            int _tabStartPoint = 5;
            int _tabWidth = 25;
            int _index = 1;
            foreach ( string path in paths )
            {
                // 
                // Create Panel
                this.InternalFlowPanel1.BackColor = Theme.Background;
                this.InternalFlowPanel1.Location = new System.Drawing.Point(5, 25);
                this.InternalFlowPanel1.Name = $"InternalFlowPanel{_index}";


                _tabStartPoint += _tabWidth
            }
            */
            // 
            // InternalFlowPanel1
            // 
            this.InternalFlowPanel1.BackColor = Theme.Background;
            this.InternalFlowPanel1.Location = new System.Drawing.Point(3, 25);
            this.InternalFlowPanel1.Name = "InternalFlowPanel1";
            //this.InternalFlowPanel1.Size = new System.Drawing.Size(215, 130);
            this.InternalFlowPanel1.TabIndex = 0;
            // 
            // InternalFlowPanel2
            // 
            this.InternalFlowPanel2.BackColor = Theme.Background;
            this.InternalFlowPanel2.BackColor = Color.LimeGreen;
            this.InternalFlowPanel2.Location = new System.Drawing.Point(3, 25);
            this.InternalFlowPanel2.Name = "InternalFlowPanel2";
            //this.InternalFlowPanel2.Size = new System.Drawing.Size(215, 130);
            this.InternalFlowPanel2.TabIndex = 0;
            this.InternalFlowPanel2.Hide();

            // v3.0
            TabManager.Instance.MainPanel = this.BackgroundPanel;
            //TabManager.Instance.AddTab(@"C:\Users\user\Desktop\Folder1");
            //TabManager.Instance.AddTab(@"C:\Users\user\Desktop\Folder2");
            //TabManager.Instance.ActiveTab = this.TabButton1.Name;

            int PanelHeightOffset = TabManager.Instance.MaxHeight - 130;
            int PanelWidthOffset = TabManager.Instance.MaxWidth - 215;

            this.InternalFlowPanel1.Size = new System.Drawing.Size(215 + PanelWidthOffset, 130 + PanelHeightOffset);
            this.InternalFlowPanel2.Size = new System.Drawing.Size(215 + PanelWidthOffset, 130 + PanelHeightOffset);
            this.SettingsButton.Location = new System.Drawing.Point(TabManager.Instance.MaxWidth-36, 3);


            // 
            // BackgroundPanel
            // 
            this.BackgroundPanel.BackColor = Theme.Background;
            //this.BackgroundPanel.BackColor = Color.OliveDrab;
            //this.BackgroundPanel.Controls.Add(this.TabButton1);
            //this.BackgroundPanel.Controls.Add(this.TabButton2);
            this.BackgroundPanel.Controls.Add(this.SettingsButton);
            this.BackgroundPanel.Controls.Add(this.InternalFlowPanel1);
            //this.BackgroundPanel.Controls.Add(this.InternalFlowPanel2);
            this.BackgroundPanel.Location = new System.Drawing.Point(7, 5);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new System.Drawing.Size(195 + PanelWidthOffset, 145 + PanelHeightOffset);
            this.BackgroundPanel.TabIndex = 1;

            // 
            // ToolbarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(TabManager.Instance.MaxWidth + 5, TabManager.Instance.MaxHeight + 35);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToobarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = new Point(-1000, -1000);
            this.Text = "ToobarForm";
            this.TopMost = true;
            this.Controls.Add(this.BackgroundPanel);
            this.Icon = Win11Toolbar.Properties.Resources.favicon2;

            this.Paint += MainWindow_Paint;

            this.BackgroundPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //private NotifyIcon notifyIcon;
        private FlowLayoutPanel InternalFlowPanel1;
        private FlowLayoutPanel InternalFlowPanel2;
        private Label TabButton1;
        private Label TabButton2;
        private Panel BackgroundPanel;
        private PictureBox SettingsButton;
        private ListView Tab1ListView;
    }
}

