﻿namespace CircuitDesigner
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
            CircuitProperties = new TabPage();
            CircuitPropertiesName = new Label();
            NeuronProperties = new TabPage();
            NeuronNameInput = new TextBox();
            NeuronTransmittersInput = new CheckedListBox();
            label4 = new Label();
            NeuronDecayInput = new TextBox();
            label3 = new Label();
            NeuronBiasInput = new TextBox();
            label2 = new Label();
            NeuronPropertiesName = new Label();
            InputProperties = new TabPage();
            InputDecayInput = new TextBox();
            label5 = new Label();
            InputPropertiesInputsList = new ListBox();
            InputPropertiesName = new Label();
            OutputProperties = new TabPage();
            OutputDecayInput = new TextBox();
            label6 = new Label();
            OutputPropertiesName = new Label();
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
            CircuitProperties.SuspendLayout();
            NeuronProperties.SuspendLayout();
            InputProperties.SuspendLayout();
            OutputProperties.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { ToolStripProgressBar, toolStripStatusLabel1, ToolStripStatusText, ToolStripProjectName });
            statusStrip1.Location = new Point(0, 560);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1023, 22);
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
            ToolStripProjectName.Size = new Size(889, 17);
            ToolStripProjectName.Spring = true;
            ToolStripProjectName.Text = "Unnamed Project";
            ToolStripProjectName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuStripFile, MenuStripSettings, MenuStripBuild, ToolStripAbout });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1023, 24);
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
            ToolStripVerify.Click += ToolStripVerify_Click;
            // 
            // ToolStripBuild
            // 
            ToolStripBuild.Name = "ToolStripBuild";
            ToolStripBuild.Size = new Size(103, 22);
            ToolStripBuild.Text = "Build";
            ToolStripBuild.Click += ToolStripBuild_Click;
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
            SplitContainer1.Size = new Size(1023, 536);
            SplitContainer1.SplitterDistance = 174;
            SplitContainer1.TabIndex = 2;
            // 
            // CircuitTree
            // 
            CircuitTree.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CircuitTree.Location = new Point(3, 321);
            CircuitTree.Name = "CircuitTree";
            CircuitTree.Size = new Size(166, 209);
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
            ProjectNameLabel.Size = new Size(107, 16);
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
            SplitContainer2.Size = new Size(845, 536);
            SplitContainer2.SplitterDistance = 430;
            SplitContainer2.TabIndex = 0;
            // 
            // DesignBoard
            // 
            DesignBoard.BackColor = Color.Black;
            DesignBoard.Dock = DockStyle.Fill;
            DesignBoard.Location = new Point(0, 0);
            DesignBoard.Name = "DesignBoard";
            DesignBoard.Size = new Size(428, 534);
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
            SplitContainer3.Size = new Size(411, 536);
            SplitContainer3.SplitterDistance = 315;
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
            splitContainer4.Size = new Size(409, 313);
            splitContainer4.SplitterDistance = 134;
            splitContainer4.TabIndex = 0;
            // 
            // InputListGroup
            // 
            InputListGroup.AutoSize = true;
            InputListGroup.Controls.Add(InputsList);
            InputListGroup.Dock = DockStyle.Fill;
            InputListGroup.Location = new Point(0, 0);
            InputListGroup.Name = "InputListGroup";
            InputListGroup.Size = new Size(409, 134);
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
            InputsList.Size = new Size(403, 112);
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
            OutputListGroup.Size = new Size(409, 175);
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
            OutputsList.Size = new Size(403, 153);
            OutputsList.TabIndex = 0;
            OutputsList.DoubleClick += OnZoomableDoubleClick;
            OutputsList.KeyUp += OutputsLists_KeyUp;
            // 
            // PropertiesTabs
            // 
            PropertiesTabs.Controls.Add(CircuitProperties);
            PropertiesTabs.Controls.Add(NeuronProperties);
            PropertiesTabs.Controls.Add(InputProperties);
            PropertiesTabs.Controls.Add(OutputProperties);
            PropertiesTabs.Dock = DockStyle.Fill;
            PropertiesTabs.Location = new Point(0, 0);
            PropertiesTabs.Name = "PropertiesTabs";
            PropertiesTabs.SelectedIndex = 0;
            PropertiesTabs.Size = new Size(409, 215);
            PropertiesTabs.TabIndex = 0;
            // 
            // CircuitProperties
            // 
            CircuitProperties.Controls.Add(CircuitPropertiesName);
            CircuitProperties.Location = new Point(4, 24);
            CircuitProperties.Name = "CircuitProperties";
            CircuitProperties.Padding = new Padding(3);
            CircuitProperties.Size = new Size(401, 187);
            CircuitProperties.TabIndex = 0;
            CircuitProperties.Tag = "CircuitTab";
            CircuitProperties.Text = "Circuit Properties";
            CircuitProperties.UseVisualStyleBackColor = true;
            // 
            // CircuitPropertiesName
            // 
            CircuitPropertiesName.AutoSize = true;
            CircuitPropertiesName.Location = new Point(6, 3);
            CircuitPropertiesName.Name = "CircuitPropertiesName";
            CircuitPropertiesName.Size = new Size(22, 15);
            CircuitPropertiesName.TabIndex = 0;
            CircuitPropertiesName.Text = "---";
            // 
            // NeuronProperties
            // 
            NeuronProperties.AutoScroll = true;
            NeuronProperties.Controls.Add(NeuronNameInput);
            NeuronProperties.Controls.Add(NeuronTransmittersInput);
            NeuronProperties.Controls.Add(label4);
            NeuronProperties.Controls.Add(NeuronDecayInput);
            NeuronProperties.Controls.Add(label3);
            NeuronProperties.Controls.Add(NeuronBiasInput);
            NeuronProperties.Controls.Add(label2);
            NeuronProperties.Controls.Add(NeuronPropertiesName);
            NeuronProperties.Location = new Point(4, 24);
            NeuronProperties.Name = "NeuronProperties";
            NeuronProperties.Padding = new Padding(3);
            NeuronProperties.Size = new Size(401, 187);
            NeuronProperties.TabIndex = 1;
            NeuronProperties.Tag = "NeuronTag";
            NeuronProperties.Text = "Neuron Properties";
            NeuronProperties.UseVisualStyleBackColor = true;
            // 
            // NeuronNameInput
            // 
            NeuronNameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronNameInput.Location = new Point(58, 3);
            NeuronNameInput.Name = "NeuronNameInput";
            NeuronNameInput.Size = new Size(320, 23);
            NeuronNameInput.TabIndex = 8;
            // 
            // NeuronTransmittersInput
            // 
            NeuronTransmittersInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NeuronTransmittersInput.FormattingEnabled = true;
            NeuronTransmittersInput.Location = new Point(58, 105);
            NeuronTransmittersInput.Name = "NeuronTransmittersInput";
            NeuronTransmittersInput.Size = new Size(320, 76);
            NeuronTransmittersInput.TabIndex = 7;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(307, 87);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 6;
            label4.Text = "Transmitters";
            // 
            // NeuronDecayInput
            // 
            NeuronDecayInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronDecayInput.Location = new Point(58, 61);
            NeuronDecayInput.Name = "NeuronDecayInput";
            NeuronDecayInput.Size = new Size(320, 23);
            NeuronDecayInput.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 61);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(39, 15);
            label3.TabIndex = 4;
            label3.Text = "Decay";
            // 
            // NeuronBiasInput
            // 
            NeuronBiasInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronBiasInput.Location = new Point(58, 32);
            NeuronBiasInput.Name = "NeuronBiasInput";
            NeuronBiasInput.Size = new Size(320, 23);
            NeuronBiasInput.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 32);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 2;
            label2.Text = "Bias";
            // 
            // NeuronPropertiesName
            // 
            NeuronPropertiesName.AutoSize = true;
            NeuronPropertiesName.Location = new Point(3, 6);
            NeuronPropertiesName.Name = "NeuronPropertiesName";
            NeuronPropertiesName.Size = new Size(39, 15);
            NeuronPropertiesName.TabIndex = 1;
            NeuronPropertiesName.Text = "Name";
            // 
            // InputProperties
            // 
            InputProperties.AutoScroll = true;
            InputProperties.Controls.Add(InputDecayInput);
            InputProperties.Controls.Add(label5);
            InputProperties.Controls.Add(InputPropertiesInputsList);
            InputProperties.Controls.Add(InputPropertiesName);
            InputProperties.Location = new Point(4, 24);
            InputProperties.Name = "InputProperties";
            InputProperties.Padding = new Padding(3);
            InputProperties.Size = new Size(401, 187);
            InputProperties.TabIndex = 2;
            InputProperties.Tag = "InputTag";
            InputProperties.Text = "Input Properties";
            InputProperties.UseVisualStyleBackColor = true;
            // 
            // InputDecayInput
            // 
            InputDecayInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            InputDecayInput.Location = new Point(51, 17);
            InputDecayInput.Name = "InputDecayInput";
            InputDecayInput.Size = new Size(343, 23);
            InputDecayInput.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 20);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.No;
            label5.Size = new Size(39, 15);
            label5.TabIndex = 6;
            label5.Text = "Decay";
            // 
            // InputPropertiesInputsList
            // 
            InputPropertiesInputsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InputPropertiesInputsList.FormattingEnabled = true;
            InputPropertiesInputsList.ItemHeight = 15;
            InputPropertiesInputsList.Location = new Point(3, 87);
            InputPropertiesInputsList.Name = "InputPropertiesInputsList";
            InputPropertiesInputsList.Size = new Size(530, 94);
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
            OutputProperties.Controls.Add(OutputDecayInput);
            OutputProperties.Controls.Add(label6);
            OutputProperties.Controls.Add(OutputPropertiesName);
            OutputProperties.Location = new Point(4, 24);
            OutputProperties.Name = "OutputProperties";
            OutputProperties.Padding = new Padding(3);
            OutputProperties.Size = new Size(401, 187);
            OutputProperties.TabIndex = 3;
            OutputProperties.Tag = "OutputTag";
            OutputProperties.Text = "Output Properties";
            OutputProperties.UseVisualStyleBackColor = true;
            // 
            // OutputDecayInput
            // 
            OutputDecayInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            OutputDecayInput.Location = new Point(48, 18);
            OutputDecayInput.Name = "OutputDecayInput";
            OutputDecayInput.Size = new Size(346, 23);
            OutputDecayInput.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 21);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.No;
            label6.Size = new Size(39, 15);
            label6.TabIndex = 8;
            label6.Text = "Decay";
            // 
            // OutputPropertiesName
            // 
            OutputPropertiesName.AutoSize = true;
            OutputPropertiesName.Location = new Point(6, 3);
            OutputPropertiesName.Name = "OutputPropertiesName";
            OutputPropertiesName.Size = new Size(22, 15);
            OutputPropertiesName.TabIndex = 2;
            OutputPropertiesName.Text = "---";
            // 
            // SapaDesigner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 582);
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
            CircuitProperties.ResumeLayout(false);
            CircuitProperties.PerformLayout();
            NeuronProperties.ResumeLayout(false);
            NeuronProperties.PerformLayout();
            InputProperties.ResumeLayout(false);
            InputProperties.PerformLayout();
            OutputProperties.ResumeLayout(false);
            OutputProperties.PerformLayout();
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
        private TabPage CircuitProperties;
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
        private Label CircuitPropertiesName;
        private Label NeuronPropertiesName;
        private Label OutputPropertiesName;
        private TextBox NeuronDecayInput;
        private Label label3;
        private TextBox NeuronBiasInput;
        private Label label2;
        private Label label4;
        private CheckedListBox NeuronTransmittersInput;
        private TextBox NeuronNameInput;
        private TextBox InputDecayInput;
        private Label label5;
        private TextBox OutputDecayInput;
        private Label label6;
    }
}
