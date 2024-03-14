
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
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            FileMenuItem = new ToolStripMenuItem();
            newProjectToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            RecentSavesDropdown = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            SettingsMenuItem = new ToolStripMenuItem();
            AboutMenuItem = new ToolStripMenuItem();
            ViewsDropDown = new ToolStripComboBox();
            viewDataBindingSource1 = new BindingSource(components);
            viewDataBindingSource = new BindingSource(components);
            statusStrip1 = new StatusStrip();
            SplitContainer = new SplitContainer();
            PropertyTabs = new TabControl();
            ProjectTab = new TabPage();
            CurrentViewNameLabel = new Label();
            label9 = new Label();
            CurrentIDInput = new TextBox();
            label8 = new Label();
            RegionTab = new TabPage();
            OutputsList = new ListBox();
            label11 = new Label();
            InputsList = new ListBox();
            label10 = new Label();
            RegionConnectionsDropdown = new ComboBox();
            label2 = new Label();
            RegionNameInput = new TextBox();
            label1 = new Label();
            NeuronTab = new TabPage();
            NeuronBiasInput = new TextBox();
            label7 = new Label();
            NeuronDecayInput = new TextBox();
            label6 = new Label();
            NeuronChargeInput = new TextBox();
            label5 = new Label();
            NeuronConnectionsDropdown = new ComboBox();
            label4 = new Label();
            NeuronIDInput = new TextBox();
            label3 = new Label();
            designBoard = new Controls.DesignBoard();
            regionModelBindingSource = new BindingSource(components);
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)viewDataBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDataBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SplitContainer).BeginInit();
            SplitContainer.Panel1.SuspendLayout();
            SplitContainer.Panel2.SuspendLayout();
            SplitContainer.SuspendLayout();
            PropertyTabs.SuspendLayout();
            ProjectTab.SuspendLayout();
            RegionTab.SuspendLayout();
            NeuronTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)regionModelBindingSource).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileMenuItem, SettingsMenuItem, AboutMenuItem, ViewsDropDown });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(928, 27);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newProjectToolStripMenuItem, openToolStripMenuItem, RecentSavesDropdown, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            FileMenuItem.Name = "FileMenuItem";
            FileMenuItem.Size = new Size(37, 23);
            FileMenuItem.Text = "File";
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
            // SettingsMenuItem
            // 
            SettingsMenuItem.Name = "SettingsMenuItem";
            SettingsMenuItem.Size = new Size(61, 23);
            SettingsMenuItem.Text = "Settings";
            // 
            // AboutMenuItem
            // 
            AboutMenuItem.Name = "AboutMenuItem";
            AboutMenuItem.Size = new Size(52, 23);
            AboutMenuItem.Text = "About";
            // 
            // ViewsDropDown
            // 
            ViewsDropDown.DataBindings.Add(new Binding("SelectedItem", viewDataBindingSource1, "Name", true));
            ViewsDropDown.Name = "ViewsDropDown";
            ViewsDropDown.Size = new Size(121, 23);
            ViewsDropDown.SelectedIndexChanged += ViewsDropDown_SelectedIndexChanged;
            // 
            // viewDataBindingSource1
            // 
            viewDataBindingSource1.DataSource = typeof(Models.ViewData);
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 453);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(928, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // SplitContainer
            // 
            SplitContainer.Dock = DockStyle.Fill;
            SplitContainer.Location = new Point(0, 27);
            SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            SplitContainer.Panel1.Controls.Add(PropertyTabs);
            // 
            // SplitContainer.Panel2
            // 
            SplitContainer.Panel2.Controls.Add(designBoard);
            SplitContainer.Panel2.ControlAdded += SplitContainer1_Panel2_ControlAdded;
            SplitContainer.Size = new Size(928, 426);
            SplitContainer.SplitterDistance = 243;
            SplitContainer.TabIndex = 2;
            // 
            // PropertyTabs
            // 
            PropertyTabs.Controls.Add(ProjectTab);
            PropertyTabs.Controls.Add(RegionTab);
            PropertyTabs.Controls.Add(NeuronTab);
            PropertyTabs.Dock = DockStyle.Fill;
            PropertyTabs.Location = new Point(0, 0);
            PropertyTabs.Name = "PropertyTabs";
            PropertyTabs.SelectedIndex = 0;
            PropertyTabs.Size = new Size(243, 426);
            PropertyTabs.TabIndex = 0;
            // 
            // ProjectTab
            // 
            ProjectTab.Controls.Add(CurrentViewNameLabel);
            ProjectTab.Controls.Add(label9);
            ProjectTab.Controls.Add(CurrentIDInput);
            ProjectTab.Controls.Add(label8);
            ProjectTab.Location = new Point(4, 24);
            ProjectTab.Name = "ProjectTab";
            ProjectTab.Padding = new Padding(3);
            ProjectTab.RightToLeft = RightToLeft.No;
            ProjectTab.Size = new Size(235, 398);
            ProjectTab.TabIndex = 0;
            ProjectTab.Text = "Project";
            ProjectTab.UseVisualStyleBackColor = true;
            // 
            // CurrentViewNameLabel
            // 
            CurrentViewNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CurrentViewNameLabel.ImageAlign = ContentAlignment.MiddleRight;
            CurrentViewNameLabel.Location = new Point(80, 38);
            CurrentViewNameLabel.Name = "CurrentViewNameLabel";
            CurrentViewNameLabel.Size = new Size(149, 15);
            CurrentViewNameLabel.TabIndex = 3;
            CurrentViewNameLabel.Text = "--";
            CurrentViewNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(8, 38);
            label9.Name = "label9";
            label9.Size = new Size(66, 15);
            label9.TabIndex = 2;
            label9.Text = "View Mode";
            // 
            // CurrentIDInput
            // 
            CurrentIDInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CurrentIDInput.Location = new Point(61, 6);
            CurrentIDInput.Name = "CurrentIDInput";
            CurrentIDInput.Size = new Size(168, 23);
            CurrentIDInput.TabIndex = 1;
            CurrentIDInput.Validating += CurrentIDInput_Validating;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 9);
            label8.Name = "label8";
            label8.Size = new Size(39, 15);
            label8.TabIndex = 0;
            label8.Text = "Name";
            // 
            // RegionTab
            // 
            RegionTab.Controls.Add(OutputsList);
            RegionTab.Controls.Add(label11);
            RegionTab.Controls.Add(InputsList);
            RegionTab.Controls.Add(label10);
            RegionTab.Controls.Add(RegionConnectionsDropdown);
            RegionTab.Controls.Add(label2);
            RegionTab.Controls.Add(RegionNameInput);
            RegionTab.Controls.Add(label1);
            RegionTab.Location = new Point(4, 24);
            RegionTab.Name = "RegionTab";
            RegionTab.Padding = new Padding(3);
            RegionTab.Size = new Size(235, 398);
            RegionTab.TabIndex = 1;
            RegionTab.Text = "Region";
            RegionTab.UseVisualStyleBackColor = true;
            // 
            // OutputsList
            // 
            OutputsList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            OutputsList.FormattingEnabled = true;
            OutputsList.ItemHeight = 15;
            OutputsList.Location = new Point(57, 167);
            OutputsList.Name = "OutputsList";
            OutputsList.Size = new Size(171, 64);
            OutputsList.TabIndex = 7;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(7, 167);
            label11.Name = "label11";
            label11.Size = new Size(50, 15);
            label11.TabIndex = 6;
            label11.Text = "Outputs";
            // 
            // InputsList
            // 
            InputsList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            InputsList.FormattingEnabled = true;
            InputsList.ItemHeight = 15;
            InputsList.Location = new Point(58, 92);
            InputsList.Name = "InputsList";
            InputsList.Size = new Size(171, 64);
            InputsList.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 92);
            label10.Name = "label10";
            label10.Size = new Size(40, 15);
            label10.TabIndex = 4;
            label10.Text = "Inputs";
            // 
            // RegionConnectionsDropdown
            // 
            RegionConnectionsDropdown.DataBindings.Add(new Binding("DataContext", regionModelBindingSource, "Name", true));
            RegionConnectionsDropdown.DataBindings.Add(new Binding("SelectedItem", regionModelBindingSource, "Name", true));
            RegionConnectionsDropdown.DataBindings.Add(new Binding("SelectedValue", regionModelBindingSource, "ID", true));
            RegionConnectionsDropdown.FormattingEnabled = true;
            RegionConnectionsDropdown.Location = new Point(8, 50);
            RegionConnectionsDropdown.Name = "RegionConnectionsDropdown";
            RegionConnectionsDropdown.Size = new Size(221, 23);
            RegionConnectionsDropdown.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(88, 32);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 2;
            label2.Text = "Connections";
            // 
            // RegionNameInput
            // 
            RegionNameInput.DataBindings.Add(new Binding("DataContext", regionModelBindingSource, "Name", true));
            RegionNameInput.DataBindings.Add(new Binding("Text", regionModelBindingSource, "Name", true));
            RegionNameInput.Location = new Point(53, 6);
            RegionNameInput.Name = "RegionNameInput";
            RegionNameInput.Size = new Size(176, 23);
            RegionNameInput.TabIndex = 1;
            RegionNameInput.Validating += RegionIDInput_Validating;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // NeuronTab
            // 
            NeuronTab.Controls.Add(NeuronBiasInput);
            NeuronTab.Controls.Add(label7);
            NeuronTab.Controls.Add(NeuronDecayInput);
            NeuronTab.Controls.Add(label6);
            NeuronTab.Controls.Add(NeuronChargeInput);
            NeuronTab.Controls.Add(label5);
            NeuronTab.Controls.Add(NeuronConnectionsDropdown);
            NeuronTab.Controls.Add(label4);
            NeuronTab.Controls.Add(NeuronIDInput);
            NeuronTab.Controls.Add(label3);
            NeuronTab.Location = new Point(4, 24);
            NeuronTab.Name = "NeuronTab";
            NeuronTab.Size = new Size(235, 398);
            NeuronTab.TabIndex = 2;
            NeuronTab.Text = "Neuron";
            NeuronTab.UseVisualStyleBackColor = true;
            // 
            // NeuronBiasInput
            // 
            NeuronBiasInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronBiasInput.Location = new Point(52, 163);
            NeuronBiasInput.Name = "NeuronBiasInput";
            NeuronBiasInput.Size = new Size(180, 23);
            NeuronBiasInput.TabIndex = 11;
            NeuronBiasInput.Validating += NeuronBiasInput_Validating;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 166);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 10;
            label7.Text = "Bias";
            // 
            // NeuronDecayInput
            // 
            NeuronDecayInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronDecayInput.Location = new Point(52, 134);
            NeuronDecayInput.Name = "NeuronDecayInput";
            NeuronDecayInput.Size = new Size(180, 23);
            NeuronDecayInput.TabIndex = 9;
            NeuronDecayInput.Validating += NeuronDecayInput_Validating;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 137);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 8;
            label6.Text = "Decay";
            // 
            // NeuronChargeInput
            // 
            NeuronChargeInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronChargeInput.Location = new Point(52, 105);
            NeuronChargeInput.Name = "NeuronChargeInput";
            NeuronChargeInput.Size = new Size(180, 23);
            NeuronChargeInput.TabIndex = 7;
            NeuronChargeInput.Validating += NeuronChargeInput_Validating;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 108);
            label5.Name = "label5";
            label5.Size = new Size(45, 15);
            label5.TabIndex = 6;
            label5.Text = "Charge";
            // 
            // NeuronConnectionsDropdown
            // 
            NeuronConnectionsDropdown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronConnectionsDropdown.FormattingEnabled = true;
            NeuronConnectionsDropdown.Location = new Point(8, 59);
            NeuronConnectionsDropdown.Name = "NeuronConnectionsDropdown";
            NeuronConnectionsDropdown.Size = new Size(224, 23);
            NeuronConnectionsDropdown.TabIndex = 5;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(78, 41);
            label4.Name = "label4";
            label4.Size = new Size(74, 15);
            label4.TabIndex = 4;
            label4.Text = "Connections";
            // 
            // NeuronIDInput
            // 
            NeuronIDInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NeuronIDInput.Location = new Point(52, 7);
            NeuronIDInput.Name = "NeuronIDInput";
            NeuronIDInput.Size = new Size(180, 23);
            NeuronIDInput.TabIndex = 1;
            NeuronIDInput.Validating += NeuronIDInput_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 10);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 0;
            label3.Text = "Name";
            // 
            // designBoard
            // 
            designBoard.BackColor = Color.Black;
            designBoard.Dock = DockStyle.Fill;
            designBoard.Location = new Point(0, 0);
            designBoard.Name = "designBoard";
            designBoard.Size = new Size(681, 426);
            designBoard.TabIndex = 0;
            // 
            // regionModelBindingSource
            // 
            regionModelBindingSource.DataSource = typeof(Models.RegionModel);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 475);
            Controls.Add(SplitContainer);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "SAPA Circuit Designer";
            FormClosing += ExitApplication;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)viewDataBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDataBindingSource).EndInit();
            SplitContainer.Panel1.ResumeLayout(false);
            SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer).EndInit();
            SplitContainer.ResumeLayout(false);
            PropertyTabs.ResumeLayout(false);
            ProjectTab.ResumeLayout(false);
            ProjectTab.PerformLayout();
            RegionTab.ResumeLayout(false);
            RegionTab.PerformLayout();
            NeuronTab.ResumeLayout(false);
            NeuronTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)regionModelBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem RecentSavesDropdown;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem SettingsMenuItem;
        private ToolStripMenuItem AboutMenuItem;
        private StatusStrip statusStrip1;
        private SplitContainer SplitContainer;
        private Controls.DesignBoard designBoard;
        private ToolStripComboBox ViewsDropDown;
        private TabControl PropertyTabs;
        private TabPage ProjectTab;
        private TabPage RegionTab;
        private TabPage NeuronTab;
        private TextBox RegionNameInput;
        private Label label1;
        private ComboBox RegionConnectionsDropdown;
        private Label label2;
        private TextBox NeuronIDInput;
        private Label label3;
        private TextBox NeuronBiasInput;
        private Label label7;
        private TextBox NeuronDecayInput;
        private Label label6;
        private TextBox NeuronChargeInput;
        private Label label5;
        private ComboBox NeuronConnectionsDropdown;
        private Label label4;
        private Label label8;
        private Label CurrentViewNameLabel;
        private Label label9;
        private TextBox CurrentIDInput;
        private ListBox InputsList;
        private Label label10;
        private ListBox OutputsList;
        private Label label11;
        private BindingSource viewDataBindingSource;
        private BindingSource viewDataBindingSource1;
        private BindingSource regionModelBindingSource;
    }
}
