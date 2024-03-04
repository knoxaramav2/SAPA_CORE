
namespace CircuitDesigner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newProjectToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            RecentSavesDropdown = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            splitContainer1 = new SplitContainer();
            TabProperties = new TabControl();
            PropertiesTab = new TabPage();
            RegionProperties = new Panel();
            label2 = new Label();
            RegionConnectionsInput = new ComboBox();
            RegionNameInput = new TextBox();
            label1 = new Label();
            designBoard1 = new Controls.DesignBoard();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            TabProperties.SuspendLayout();
            PropertiesTab.SuspendLayout();
            RegionProperties.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(928, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newProjectToolStripMenuItem, openToolStripMenuItem, RecentSavesDropdown, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            newProjectToolStripMenuItem.Size = new Size(138, 22);
            newProjectToolStripMenuItem.Text = "New Project";
            newProjectToolStripMenuItem.Click += NewProjectToolStripItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(138, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenProjectToolStripItem_Click;
            // 
            // RecentSavesDropdown
            // 
            RecentSavesDropdown.Name = "RecentSavesDropdown";
            RecentSavesDropdown.Size = new Size(138, 22);
            RecentSavesDropdown.Text = "Recent";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(138, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += SaveProjectToolStripItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(138, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += SaveAsProjectToolStripItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(138, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 453);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(928, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(TabProperties);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(designBoard1);
            splitContainer1.Size = new Size(928, 429);
            splitContainer1.SplitterDistance = 251;
            splitContainer1.TabIndex = 2;
            // 
            // TabProperties
            // 
            TabProperties.Controls.Add(PropertiesTab);
            TabProperties.Dock = DockStyle.Fill;
            TabProperties.Location = new Point(0, 0);
            TabProperties.Name = "TabProperties";
            TabProperties.SelectedIndex = 0;
            TabProperties.Size = new Size(251, 429);
            TabProperties.TabIndex = 0;
            // 
            // PropertiesTab
            // 
            PropertiesTab.Controls.Add(RegionProperties);
            PropertiesTab.Location = new Point(4, 24);
            PropertiesTab.Name = "PropertiesTab";
            PropertiesTab.Padding = new Padding(3);
            PropertiesTab.Size = new Size(243, 401);
            PropertiesTab.TabIndex = 0;
            PropertiesTab.Text = "Properties";
            PropertiesTab.UseVisualStyleBackColor = true;
            // 
            // RegionProperties
            // 
            RegionProperties.BackColor = Color.DimGray;
            RegionProperties.Controls.Add(label2);
            RegionProperties.Controls.Add(RegionConnectionsInput);
            RegionProperties.Controls.Add(RegionNameInput);
            RegionProperties.Controls.Add(label1);
            RegionProperties.Dock = DockStyle.Fill;
            RegionProperties.Location = new Point(3, 3);
            RegionProperties.Name = "RegionProperties";
            RegionProperties.Size = new Size(237, 395);
            RegionProperties.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(5, 40);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 3;
            label2.Text = "Connections";
            // 
            // RegionConnectionsInput
            // 
            RegionConnectionsInput.FormattingEnabled = true;
            RegionConnectionsInput.Location = new Point(5, 58);
            RegionConnectionsInput.Name = "RegionConnectionsInput";
            RegionConnectionsInput.Size = new Size(229, 23);
            RegionConnectionsInput.TabIndex = 2;
            // 
            // RegionNameInput
            // 
            RegionNameInput.Location = new Point(90, 3);
            RegionNameInput.Name = "RegionNameInput";
            RegionNameInput.Size = new Size(144, 23);
            RegionNameInput.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(5, 6);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Region Name";
            // 
            // designBoard1
            // 
            designBoard1.BackColor = Color.Black;
            designBoard1.Dock = DockStyle.Fill;
            designBoard1.Location = new Point(0, 0);
            designBoard1.Name = "designBoard1";
            designBoard1.Size = new Size(673, 429);
            designBoard1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 475);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "SAPA Circuit Designer";
            FormClosing += ExitApplication;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            TabProperties.ResumeLayout(false);
            PropertiesTab.ResumeLayout(false);
            RegionProperties.ResumeLayout(false);
            RegionProperties.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem RecentSavesDropdown;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private TabControl TabProperties;
        private TabPage PropertiesTab;
        private Panel RegionProperties;
        private TextBox RegionNameInput;
        private Label label1;
        private Label label2;
        private ComboBox RegionConnectionsInput;
        private Controls.DesignBoard designBoard1;
    }
}
