namespace CircuitDesigner
{
    partial class SapaDesigner
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
            statusStrip1 = new StatusStrip();
            ToolStripProgressBar = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            ToolStripStatusText = new ToolStripStatusLabel();
            ToolStripProjectName = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            MenuStripFile = new ToolStripMenuItem();
            ToolStripNew = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            ToolStripSave = new ToolStripMenuItem();
            ToolStripSaveAs = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            ToolStripOpen = new ToolStripMenuItem();
            ToolStripRecent = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            ToolStripExit = new ToolStripMenuItem();
            MenuStripSettings = new ToolStripMenuItem();
            ToolStripProperties = new ToolStripMenuItem();
            ToolStripProgramSettings = new ToolStripMenuItem();
            MenuStripBuild = new ToolStripMenuItem();
            ToolStripVerify = new ToolStripMenuItem();
            ToolStripBuild = new ToolStripMenuItem();
            ToolStripAbout = new ToolStripMenuItem();
            AboutMenuItem = new ToolStripMenuItem();
            SplitContainer1 = new SplitContainer();
            CircuitTree = new TreeView();
            ProjectNameLabel = new TextBox();
            label1 = new Label();
            SplitContainer2 = new SplitContainer();
            DesignBoard = new Controls.DesignBoard();
            SplitContainer3 = new SplitContainer();
            splitContainer4 = new SplitContainer();
            InputListGroup = new GroupBox();
            InputsList = new CheckedListBox();
            OutputListGroup = new GroupBox();
            OutputsList = new CheckedListBox();
            PropertiesTabs = new TabControl();
            RegionProperties = new TabPage();
            NeuronProperties = new TabPage();
            InputProperties = new TabPage();
            InputPropertiesInputsList = new ListBox();
            InputPropertiesName = new Label();
            OutputProperties = new TabPage();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
            SplitContainer1.Panel1.SuspendLayout();
            SplitContainer1.Panel2.SuspendLayout();
            SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer2).BeginInit();
            SplitContainer2.Panel1.SuspendLayout();
            SplitContainer2.Panel2.SuspendLayout();
            SplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer3).BeginInit();
            SplitContainer3.Panel1.SuspendLayout();
            SplitContainer3.Panel2.SuspendLayout();
            SplitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            InputListGroup.SuspendLayout();
            OutputListGroup.SuspendLayout();
            PropertiesTabs.SuspendLayout();
            InputProperties.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { ToolStripProgressBar, toolStripStatusLabel1, ToolStripStatusText, ToolStripProjectName });
            statusStrip1.Location = new Point(0, 560);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(757, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // ToolStripProgressBar
            // 
            ToolStripProgressBar.Name = "ToolStripProgressBar";
            ToolStripProgressBar.Size = new Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // ToolStripStatusText
            // 
            ToolStripStatusText.Name = "ToolStripStatusText";
            ToolStripStatusText.Size = new Size(17, 17);
            ToolStripStatusText.Text = "--";
            // 
            // ToolStripProjectName
            // 
            ToolStripProjectName.Name = "ToolStripProjectName";
            ToolStripProjectName.RightToLeft = RightToLeft.No;
            ToolStripProjectName.Size = new Size(623, 17);
            ToolStripProjectName.Spring = true;
            ToolStripProjectName.Text = "Unnamed Project";
            ToolStripProjectName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuStripFile, MenuStripSettings, MenuStripBuild, ToolStripAbout });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(757, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "MenuStrip";
            // 
            // MenuStripFile
            // 
            MenuStripFile.DropDownItems.AddRange(new ToolStripItem[] { ToolStripNew, toolStripSeparator3, ToolStripSave, ToolStripSaveAs, toolStripSeparator2, ToolStripOpen, ToolStripRecent, toolStripSeparator1, ToolStripExit });
            MenuStripFile.Name = "MenuStripFile";
            MenuStripFile.Size = new Size(37, 20);
            MenuStripFile.Text = "File";
            // 
            // ToolStripNew
            // 
            ToolStripNew.Name = "ToolStripNew";
            ToolStripNew.Size = new Size(114, 22);
            ToolStripNew.Text = "New";
            ToolStripNew.Click += ToolStripNew_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(111, 6);
            // 
            // ToolStripSave
            // 
            ToolStripSave.Name = "ToolStripSave";
            ToolStripSave.Size = new Size(114, 22);
            ToolStripSave.Text = "Save";
            ToolStripSave.Click += ToolStripSave_Click;
            // 
            // ToolStripSaveAs
            // 
            ToolStripSaveAs.Name = "ToolStripSaveAs";
            ToolStripSaveAs.Size = new Size(114, 22);
            ToolStripSaveAs.Text = "Save As";
            ToolStripSaveAs.Click += ToolStripSaveAs_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(111, 6);
            // 
            // ToolStripOpen
            // 
            ToolStripOpen.Name = "ToolStripOpen";
            ToolStripOpen.Size = new Size(114, 22);
            ToolStripOpen.Text = "Open";
            ToolStripOpen.Click += ToolStripOpen_Click;
            // 
            // ToolStripRecent
            // 
            ToolStripRecent.Name = "ToolStripRecent";
            ToolStripRecent.Size = new Size(114, 22);
            ToolStripRecent.Text = "Recent";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(111, 6);
            // 
            // ToolStripExit
            // 
            ToolStripExit.Name = "ToolStripExit";
            ToolStripExit.Size = new Size(114, 22);
            ToolStripExit.Text = "Exit";
            ToolStripExit.Click += ToolStripExit_Click;
            // 
            // MenuStripSettings
            // 
            MenuStripSettings.DropDownItems.AddRange(new ToolStripItem[] { ToolStripProperties, ToolStripProgramSettings });
            MenuStripSettings.Name = "MenuStripSettings";
            MenuStripSettings.Size = new Size(61, 20);
            MenuStripSettings.Text = "Settings";
            // 
            // ToolStripProperties
            // 
            ToolStripProperties.Name = "ToolStripProperties";
            ToolStripProperties.Size = new Size(165, 22);
            ToolStripProperties.Text = "Properties";
            ToolStripProperties.Click += ToolStripProperties_Click;
            // 
            // ToolStripProgramSettings
            // 
            ToolStripProgramSettings.Name = "ToolStripProgramSettings";
            ToolStripProgramSettings.Size = new Size(165, 22);
            ToolStripProgramSettings.Text = "Program Settings";
            // 
            // MenuStripBuild
            // 
            MenuStripBuild.DropDownItems.AddRange(new ToolStripItem[] { ToolStripVerify, ToolStripBuild });
            MenuStripBuild.Name = "MenuStripBuild";
            MenuStripBuild.Size = new Size(46, 20);
            MenuStripBuild.Text = "Build";
            // 
            // ToolStripVerify
            // 
            ToolStripVerify.Name = "ToolStripVerify";
            ToolStripVerify.Size = new Size(103, 22);
            ToolStripVerify.Text = "Verify";
            // 
            // ToolStripBuild
            // 
            ToolStripBuild.Name = "ToolStripBuild";
            ToolStripBuild.Size = new Size(103, 22);
            ToolStripBuild.Text = "Build";
            // 
            // ToolStripAbout
            // 
            ToolStripAbout.DropDownItems.AddRange(new ToolStripItem[] { AboutMenuItem });
            ToolStripAbout.Name = "ToolStripAbout";
            ToolStripAbout.Size = new Size(52, 20);
            ToolStripAbout.Text = "About";
            // 
            // AboutMenuItem
            // 
            AboutMenuItem.Name = "AboutMenuItem";
            AboutMenuItem.Size = new Size(107, 22);
            AboutMenuItem.Text = "About";
            // 
            // SplitContainer1
            // 
            SplitContainer1.BorderStyle = BorderStyle.FixedSingle;
            SplitContainer1.Dock = DockStyle.Fill;
            SplitContainer1.Location = new Point(0, 24);
            SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            SplitContainer1.Panel1.Controls.Add(CircuitTree);
            SplitContainer1.Panel1.Controls.Add(ProjectNameLabel);
            SplitContainer1.Panel1.Controls.Add(label1);
            // 
            // SplitContainer1.Panel2
            // 
            SplitContainer1.Panel2.Controls.Add(SplitContainer2);
            SplitContainer1.Size = new Size(757, 536);
            SplitContainer1.SplitterDistance = 130;
            SplitContainer1.TabIndex = 2;
            // 
            // CircuitTree
            // 
            CircuitTree.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CircuitTree.Location = new Point(3, 321);
            CircuitTree.Name = "CircuitTree";
            CircuitTree.Size = new Size(122, 209);
            CircuitTree.TabIndex = 3;
            // 
            // ProjectNameLabel
            // 
            ProjectNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProjectNameLabel.BackColor = SystemColors.Control;
            ProjectNameLabel.BorderStyle = BorderStyle.None;
            ProjectNameLabel.Enabled = false;
            ProjectNameLabel.Location = new Point(62, 11);
            ProjectNameLabel.Name = "ProjectNameLabel";
            ProjectNameLabel.Size = new Size(63, 16);
            ProjectNameLabel.TabIndex = 2;
            ProjectNameLabel.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 1;
            label1.Text = "Project";
            // 
            // SplitContainer2
            // 
            SplitContainer2.BorderStyle = BorderStyle.FixedSingle;
            SplitContainer2.Dock = DockStyle.Fill;
            SplitContainer2.Location = new Point(0, 0);
            SplitContainer2.Name = "SplitContainer2";
            // 
            // SplitContainer2.Panel1
            // 
            SplitContainer2.Panel1.Controls.Add(DesignBoard);
            // 
            // SplitContainer2.Panel2
            // 
            SplitContainer2.Panel2.Controls.Add(SplitContainer3);
            SplitContainer2.Size = new Size(623, 536);
            SplitContainer2.SplitterDistance = 411;
            SplitContainer2.TabIndex = 0;
            // 
            // DesignBoard
            // 
            DesignBoard.BackColor = Color.Black;
            DesignBoard.Dock = DockStyle.Fill;
            DesignBoard.Location = new Point(0, 0);
            DesignBoard.Name = "DesignBoard";
            DesignBoard.Size = new Size(409, 534);
            DesignBoard.TabIndex = 0;
            // 
            // SplitContainer3
            // 
            SplitContainer3.BorderStyle = BorderStyle.FixedSingle;
            SplitContainer3.Dock = DockStyle.Fill;
            SplitContainer3.Location = new Point(0, 0);
            SplitContainer3.Name = "SplitContainer3";
            SplitContainer3.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer3.Panel1
            // 
            SplitContainer3.Panel1.Controls.Add(splitContainer4);
            // 
            // SplitContainer3.Panel2
            // 
            SplitContainer3.Panel2.Controls.Add(PropertiesTabs);
            SplitContainer3.Size = new Size(208, 536);
            SplitContainer3.SplitterDistance = 367;
            SplitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(InputListGroup);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(OutputListGroup);
            splitContainer4.Size = new Size(206, 365);
            splitContainer4.SplitterDistance = 157;
            splitContainer4.TabIndex = 0;
            // 
            // InputListGroup
            // 
            InputListGroup.AutoSize = true;
            InputListGroup.Controls.Add(InputsList);
            InputListGroup.Dock = DockStyle.Fill;
            InputListGroup.Location = new Point(0, 0);
            InputListGroup.Name = "InputListGroup";
            InputListGroup.Size = new Size(206, 157);
            InputListGroup.TabIndex = 5;
            InputListGroup.TabStop = false;
            InputListGroup.Text = "Inputs";
            // 
            // InputsList
            // 
            InputsList.Dock = DockStyle.Fill;
            InputsList.FormattingEnabled = true;
            InputsList.Location = new Point(3, 19);
            InputsList.Name = "InputsList";
            InputsList.Size = new Size(200, 135);
            InputsList.TabIndex = 0;
            InputsList.DoubleClick += OnZoomableDoubleClick;
            InputsList.KeyUp += InputsList_KeyUp;
            // 
            // OutputListGroup
            // 
            OutputListGroup.Controls.Add(OutputsList);
            OutputListGroup.Dock = DockStyle.Fill;
            OutputListGroup.Location = new Point(0, 0);
            OutputListGroup.Name = "OutputListGroup";
            OutputListGroup.Size = new Size(206, 204);
            OutputListGroup.TabIndex = 6;
            OutputListGroup.TabStop = false;
            OutputListGroup.Text = "Outputs";
            // 
            // OutputsList
            // 
            OutputsList.Dock = DockStyle.Fill;
            OutputsList.FormattingEnabled = true;
            OutputsList.Location = new Point(3, 19);
            OutputsList.Name = "OutputsList";
            OutputsList.Size = new Size(200, 182);
            OutputsList.TabIndex = 0;
            OutputsList.DoubleClick += OnZoomableDoubleClick;
            OutputsList.KeyUp += OutputsLists_KeyUp;
            // 
            // PropertiesTabs
            // 
            PropertiesTabs.Controls.Add(RegionProperties);
            PropertiesTabs.Controls.Add(NeuronProperties);
            PropertiesTabs.Controls.Add(InputProperties);
            PropertiesTabs.Controls.Add(OutputProperties);
            PropertiesTabs.Dock = DockStyle.Fill;
            PropertiesTabs.Location = new Point(0, 0);
            PropertiesTabs.Name = "PropertiesTabs";
            PropertiesTabs.SelectedIndex = 0;
            PropertiesTabs.Size = new Size(206, 163);
            PropertiesTabs.TabIndex = 0;
            // 
            // RegionProperties
            // 
            RegionProperties.Location = new Point(4, 24);
            RegionProperties.Name = "RegionProperties";
            RegionProperties.Padding = new Padding(3);
            RegionProperties.Size = new Size(198, 135);
            RegionProperties.TabIndex = 0;
            RegionProperties.Tag = "RegionTab";
            RegionProperties.Text = "Region Properties";
            RegionProperties.UseVisualStyleBackColor = true;
            // 
            // NeuronProperties
            // 
            NeuronProperties.Location = new Point(4, 24);
            NeuronProperties.Name = "NeuronProperties";
            NeuronProperties.Padding = new Padding(3);
            NeuronProperties.Size = new Size(198, 135);
            NeuronProperties.TabIndex = 1;
            NeuronProperties.Tag = "NeuronTag";
            NeuronProperties.Text = "Neuron Properties";
            NeuronProperties.UseVisualStyleBackColor = true;
            // 
            // InputProperties
            // 
            InputProperties.AutoScroll = true;
            InputProperties.Controls.Add(InputPropertiesInputsList);
            InputProperties.Controls.Add(InputPropertiesName);
            InputProperties.Location = new Point(4, 24);
            InputProperties.Name = "InputProperties";
            InputProperties.Padding = new Padding(3);
            InputProperties.Size = new Size(198, 135);
            InputProperties.TabIndex = 2;
            InputProperties.Tag = "InputTag";
            InputProperties.Text = "Input Properties";
            InputProperties.UseVisualStyleBackColor = true;
            // 
            // InputPropertiesInputsList
            // 
            InputPropertiesInputsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InputPropertiesInputsList.FormattingEnabled = true;
            InputPropertiesInputsList.ItemHeight = 15;
            InputPropertiesInputsList.Location = new Point(6, 21);
            InputPropertiesInputsList.Name = "InputPropertiesInputsList";
            InputPropertiesInputsList.Size = new Size(285, 94);
            InputPropertiesInputsList.TabIndex = 1;
            // 
            // InputPropertiesName
            // 
            InputPropertiesName.AutoSize = true;
            InputPropertiesName.Location = new Point(6, 3);
            InputPropertiesName.Name = "InputPropertiesName";
            InputPropertiesName.Size = new Size(22, 15);
            InputPropertiesName.TabIndex = 0;
            InputPropertiesName.Text = "---";
            // 
            // OutputProperties
            // 
            OutputProperties.Location = new Point(4, 24);
            OutputProperties.Name = "OutputProperties";
            OutputProperties.Padding = new Padding(3);
            OutputProperties.Size = new Size(198, 135);
            OutputProperties.TabIndex = 3;
            OutputProperties.Tag = "OutputTag";
            OutputProperties.Text = "Output Properties";
            OutputProperties.UseVisualStyleBackColor = true;
            // 
            // SapaDesigner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 582);
            Controls.Add(SplitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "SapaDesigner";
            Text = "SAPA Designer";
            FormClosing += SapaDesigner_FormClosing;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            SplitContainer1.Panel1.ResumeLayout(false);
            SplitContainer1.Panel1.PerformLayout();
            SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
            SplitContainer1.ResumeLayout(false);
            SplitContainer2.Panel1.ResumeLayout(false);
            SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer2).EndInit();
            SplitContainer2.ResumeLayout(false);
            SplitContainer3.Panel1.ResumeLayout(false);
            SplitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer3).EndInit();
            SplitContainer3.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel1.PerformLayout();
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            InputListGroup.ResumeLayout(false);
            OutputListGroup.ResumeLayout(false);
            PropertiesTabs.ResumeLayout(false);
            InputProperties.ResumeLayout(false);
            InputProperties.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem MenuStripFile;
        private ToolStripMenuItem MenuStripSettings;
        private ToolStripMenuItem MenuStripBuild;
        private ToolStripMenuItem ToolStripAbout;
        private ToolStripMenuItem ToolStripSave;
        private ToolStripMenuItem ToolStripSaveAs;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem ToolStripOpen;
        private ToolStripMenuItem ToolStripRecent;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem ToolStripExit;
        private ToolStripMenuItem ToolStripProperties;
        private ToolStripMenuItem ToolStripProgramSettings;
        private ToolStripMenuItem ToolStripVerify;
        private ToolStripMenuItem ToolStripBuild;
        private ToolStripMenuItem AboutMenuItem;
        private ToolStripProgressBar ToolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel ToolStripProjectName;
        private ToolStripStatusLabel ToolStripStatusText;
        private SplitContainer SplitContainer1;
        private SplitContainer SplitContainer2;
        private SplitContainer SplitContainer3;
        private TabControl PropertiesTabs;
        private TabPage RegionProperties;
        private TabPage NeuronProperties;
        private Label label1;
        private TextBox ProjectNameLabel;
        private ToolStripMenuItem ToolStripNew;
        private ToolStripSeparator toolStripSeparator3;
        private TreeView CircuitTree;
        private Controls.DesignBoard DesignBoard;
        private TabPage InputProperties;
        private TabPage OutputProperties;
        private Label InputPropertiesName;
        private ListBox InputPropertiesInputsList;
        private SplitContainer splitContainer4;
        private GroupBox OutputListGroup;
        private CheckedListBox OutputsList;
        private GroupBox InputListGroup;
        private CheckedListBox InputsList;
    }
}
