using System.Drawing;
using System.Windows.Forms;
using Win11Toolbar.Core;

namespace Win11Toolbar
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.BackPanel = new System.Windows.Forms.Panel();
            this.Tab2FolderPathLabel = new System.Windows.Forms.Label();
            this.Tab1FolderPathLabel = new System.Windows.Forms.Label();
            this.Tab2SelectButton = new System.Windows.Forms.Button();
            this.Tab1SelectButton = new System.Windows.Forms.Button();
            this.Tab2FolderLabel = new System.Windows.Forms.Label();
            this.Tab1FolderLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TopGrabPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.BackPanel.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.TopGrabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackPanel
            // 
            //this.BackPanel.BackColor = Utilities.GetTheme().Background;
            this.BackPanel.BackColor = Color.Transparent;
            this.BackPanel.Controls.Add(this.SaveButton);
            this.BackPanel.Controls.Add(this.TopGrabPanel);
            this.BackPanel.Controls.Add(this.ContentPanel);
            this.BackPanel.Location = new System.Drawing.Point(0, 0);
            this.BackPanel.Name = "BackPanel";
            this.BackPanel.Size = new System.Drawing.Size(700, 350);
            this.BackPanel.Paint += BackPanel_Paint;
            // 
            // ContentPanel
            // 
            //this.ContentPanel.BackColor = Utilities.GetTheme().Highlight;
            this.ContentPanel.BackColor = Color.Transparent;
            this.ContentPanel.Controls.Add(this.Tab2FolderPathLabel);
            this.ContentPanel.Controls.Add(this.Tab1FolderPathLabel);
            this.ContentPanel.Controls.Add(this.Tab2SelectButton);
            this.ContentPanel.Controls.Add(this.Tab1SelectButton);
            this.ContentPanel.Controls.Add(this.Tab2FolderLabel);
            this.ContentPanel.Controls.Add(this.Tab1FolderLabel);
            this.ContentPanel.Location = new System.Drawing.Point(10, 30);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(680, 111);
            this.ContentPanel.TabIndex = 3;
            this.ContentPanel.Paint += ContentPanel_Paint;
            // 
            // Tab2FolderPathLabel
            // 
            this.Tab2FolderPathLabel.ForeColor = Utilities.GetTheme().Text;
            this.Tab2FolderPathLabel.Location = new System.Drawing.Point(111, 40);
            this.Tab2FolderPathLabel.Name = "Tab2FolderPathLabel";
            this.Tab2FolderPathLabel.Size = new System.Drawing.Size(400, 30);
            this.Tab2FolderPathLabel.TabIndex = 8;
            this.Tab2FolderPathLabel.Text = @"C:\\Users\Default\Desktop\Epic";
            this.Tab2FolderPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Tab1FolderPathLabel
            // 
            this.Tab1FolderPathLabel.ForeColor = Utilities.GetTheme().Text;
            this.Tab1FolderPathLabel.Location = new System.Drawing.Point(111, 5);
            this.Tab1FolderPathLabel.Name = "Tab1FolderPathLabel";
            this.Tab1FolderPathLabel.Size = new System.Drawing.Size(400, 30);
            this.Tab1FolderPathLabel.TabIndex = 7;
            this.Tab1FolderPathLabel.Text = @"C:\Users\Default\Desktop\PS";
            this.Tab1FolderPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            string[] paths = ConfigManager.Instance.ToolbarPaths;
            if (paths.Length > 1)
            {
                this.Tab1FolderPathLabel.Text = paths[0];
            }
            if (paths.Length > 2)
            {
                this.Tab2FolderPathLabel.Text = paths[1];
            }


            // 
            // Tab1SelectButton
            // 
            //this.Tab1SelectButton.BackColor = Utilities.GetTheme().Highlight;
            this.Tab1SelectButton.ForeColor = Utilities.GetTheme().Text;
            this.Tab1SelectButton.FlatAppearance.BorderSize = 0;
            this.Tab1SelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tab1SelectButton.Location = new System.Drawing.Point(570, 5);
            this.Tab1SelectButton.Name = "Tab1SelectButton";
            this.Tab1SelectButton.Size = new System.Drawing.Size(100, 30);
            this.Tab1SelectButton.TabIndex = 3;
            this.Tab1SelectButton.Text = "Select Folder";
            this.Tab1SelectButton.UseVisualStyleBackColor = false;
            this.Tab1SelectButton.Paint += AllButton_Paint;
            this.Tab1SelectButton.Click += (o, e) => { FolderSelectButton_Click(o, e, 1); };
            // 
            // Tab2SelectButton
            // 
            //this.Tab2SelectButton.BackColor = Utilities.GetTheme().Highlight;
            this.Tab2SelectButton.ForeColor = Utilities.GetTheme().Text;
            this.Tab2SelectButton.FlatAppearance.BorderSize = 0;
            this.Tab2SelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tab2SelectButton.Location = new System.Drawing.Point(570, 40);
            this.Tab2SelectButton.Name = "Tab2SelectButton";
            this.Tab2SelectButton.Size = new System.Drawing.Size(100, 30);
            this.Tab2SelectButton.TabIndex = 6;
            this.Tab2SelectButton.Text = "Select Folder";
            this.Tab2SelectButton.UseVisualStyleBackColor = false;
            this.Tab2SelectButton.Paint += AllButton_Paint;
            this.Tab2SelectButton.Click += (o,e) => { FolderSelectButton_Click(o,e,2); } ;
            // 
            // Tab2FolderLabel
            // 
            this.Tab2FolderLabel.ForeColor = Utilities.GetTheme().Text;
            this.Tab2FolderLabel.Location = new System.Drawing.Point(5, 40);
            this.Tab2FolderLabel.Name = "Tab2FolderLabel";
            this.Tab2FolderLabel.Size = new System.Drawing.Size(100, 30);
            this.Tab2FolderLabel.TabIndex = 3;
            this.Tab2FolderLabel.Text = "Tab 2 Folder";
            this.Tab2FolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tab1FolderLabel
            // 
            this.Tab1FolderLabel.ForeColor = Utilities.GetTheme().Text;
            this.Tab1FolderLabel.Location = new System.Drawing.Point(5, 5);
            this.Tab1FolderLabel.Name = "Tab1FolderLabel";
            this.Tab1FolderLabel.Size = new System.Drawing.Size(100, 30);
            this.Tab1FolderLabel.TabIndex = 1;
            this.Tab1FolderLabel.Text = "Tab 1 Folder";
            this.Tab1FolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopGrabPanel
            // 
            //this.TopGrabPanel.BackColor = Utilities.GetTheme().Background;
            this.TopGrabPanel.BackColor = Color.Transparent;
            this.TopGrabPanel.Controls.Add(this.CloseButton);
            //this.TopGrabPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopGrabPanel.Location = new System.Drawing.Point(0, 0);
            this.TopGrabPanel.Name = "TopGrabPanel";
            this.TopGrabPanel.Size = new System.Drawing.Size(this.BackPanel.Width, 25);
            this.TopGrabPanel.TabIndex = 4;
            this.TopGrabPanel.MouseDown += TopGrabPanel_MouseDown;
            this.TopGrabPanel.MouseUp += TopGrabPanel_MouseUp;
            this.TopGrabPanel.MouseMove += TopGrabPanel_MouseMove;
            this.TopGrabPanel.Paint += TopGrabPanel_Paint;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = Color.Transparent;
            this.CloseButton.MouseEnter += (o, e) => { this._closeHover = true; this.CloseButton.Invalidate(); };
            this.CloseButton.MouseLeave += (o, e) => { this._closeHover = false; this.CloseButton.Invalidate(); };
            this.CloseButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(38, 24);
            this.CloseButton.Location = new System.Drawing.Point(this.TopGrabPanel.Width - this.CloseButton.Width-2, 1);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Click += (o, e) => { this.Close(); };
            this.CloseButton.Paint += CloseButton_Paint;
            // 
            // SaveButton
            // 
            //this.SaveButton.BackColor = Utilities.GetTheme().Highlight;
            this.SaveButton.ForeColor = Utilities.GetTheme().Text;
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(570, 308);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(100, 30);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Paint += AllButton_Paint;
            this.SaveButton.Click += SaveButton_Click;
            // 
            // ConfigurationForm
            //
            //this.BackColor = Utilities.GetTheme().Background;
            this.BackColor = Color.Fuchsia;
            this.TransparencyKey = Color.Fuchsia;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 350);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Paint += ConfigurationForm_Paint;
            this.Controls.Add(this.BackPanel);
            this.ContentPanel.ResumeLayout(false);
            this.TopGrabPanel.ResumeLayout(false);
            this.BackPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.Panel BackPanel;
        private System.Windows.Forms.Label Tab2FolderPathLabel;
        private System.Windows.Forms.Label Tab1FolderPathLabel;
        private System.Windows.Forms.Button Tab2SelectButton;
        private System.Windows.Forms.Button Tab1SelectButton;
        private System.Windows.Forms.Label Tab2FolderLabel;
        private System.Windows.Forms.Label Tab1FolderLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel TopGrabPanel;
        private System.Windows.Forms.Label CloseButton;
        private System.Windows.Forms.Button SaveButton;
    }
}