namespace CircuitDesigner.Forms
{
    partial class ProgramPropertiesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TreeNode treeNode1 = new TreeNode("Configuration");
            TreeNode treeNode2 = new TreeNode("Project", new TreeNode[] { treeNode1 });
            TreeNode treeNode3 = new TreeNode("Configuration");
            TreeNode treeNode4 = new TreeNode("Paths");
            TreeNode treeNode5 = new TreeNode("Definitions");
            TreeNode treeNode6 = new TreeNode("System", new TreeNode[] { treeNode3, treeNode4, treeNode5 });
            PropertiesContainer = new SplitContainer();
            PropertiesTree = new TreeView();
            PropertyPages = new TabControl();
            ProjectConfiguration = new TabPage();
            PathUpdateBtn = new Button();
            ProjectPathLabel = new Label();
            ProjectNameInput = new TextBox();
            label1 = new Label();
            SystemConfiguration = new TabPage();
            SystemPaths = new TabPage();
            SystemDefinitions = new TabPage();
            TransmitterGroup = new GroupBox();
            TransmitterTable = new DataGridView();
            AddTransmitterBtn = new Button();
            DeleteTransmitterBtn = new Button();
            EditTransmitterBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)PropertiesContainer).BeginInit();
            PropertiesContainer.Panel1.SuspendLayout();
            PropertiesContainer.Panel2.SuspendLayout();
            PropertiesContainer.SuspendLayout();
            PropertyPages.SuspendLayout();
            ProjectConfiguration.SuspendLayout();
            SystemDefinitions.SuspendLayout();
            TransmitterGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TransmitterTable).BeginInit();
            SuspendLayout();
            // 
            // PropertiesContainer
            // 
            PropertiesContainer.Dock = DockStyle.Fill;
            PropertiesContainer.Location = new Point(0, 0);
            PropertiesContainer.Name = "PropertiesContainer";
            // 
            // PropertiesContainer.Panel1
            // 
            PropertiesContainer.Panel1.AutoScroll = true;
            PropertiesContainer.Panel1.Controls.Add(PropertiesTree);
            // 
            // PropertiesContainer.Panel2
            // 
            PropertiesContainer.Panel2.AutoScroll = true;
            PropertiesContainer.Panel2.Controls.Add(PropertyPages);
            PropertiesContainer.Size = new Size(662, 450);
            PropertiesContainer.SplitterDistance = 168;
            PropertiesContainer.TabIndex = 0;
            // 
            // PropertiesTree
            // 
            PropertiesTree.Dock = DockStyle.Fill;
            PropertiesTree.Location = new Point(0, 0);
            PropertiesTree.Name = "PropertiesTree";
            treeNode1.Name = "ProjectConfigNode";
            treeNode1.Text = "Configuration";
            treeNode2.Name = "ProjectRoot";
            treeNode2.Text = "Project";
            treeNode3.Name = "SystemConfigNode";
            treeNode3.Text = "Configuration";
            treeNode4.Name = "PathsNode";
            treeNode4.Text = "Paths";
            treeNode5.Name = "DefinitionsNode";
            treeNode5.Text = "Definitions";
            treeNode6.Name = "SystemRoot";
            treeNode6.Text = "System";
            PropertiesTree.Nodes.AddRange(new TreeNode[] { treeNode2, treeNode6 });
            PropertiesTree.Size = new Size(168, 450);
            PropertiesTree.TabIndex = 0;
            PropertiesTree.AfterSelect += PropertiesTree_AfterSelect;
            PropertiesTree.TabIndexChanged += PropertiesTree_TabIndexChanged;
            // 
            // PropertyPages
            // 
            PropertyPages.Controls.Add(ProjectConfiguration);
            PropertyPages.Controls.Add(SystemConfiguration);
            PropertyPages.Controls.Add(SystemPaths);
            PropertyPages.Controls.Add(SystemDefinitions);
            PropertyPages.Dock = DockStyle.Fill;
            PropertyPages.Location = new Point(0, 0);
            PropertyPages.Name = "PropertyPages";
            PropertyPages.SelectedIndex = 0;
            PropertyPages.Size = new Size(490, 450);
            PropertyPages.TabIndex = 0;
            // 
            // ProjectConfiguration
            // 
            ProjectConfiguration.Controls.Add(PathUpdateBtn);
            ProjectConfiguration.Controls.Add(ProjectPathLabel);
            ProjectConfiguration.Controls.Add(ProjectNameInput);
            ProjectConfiguration.Controls.Add(label1);
            ProjectConfiguration.Location = new Point(4, 24);
            ProjectConfiguration.Name = "ProjectConfiguration";
            ProjectConfiguration.Padding = new Padding(3);
            ProjectConfiguration.Size = new Size(482, 422);
            ProjectConfiguration.TabIndex = 0;
            ProjectConfiguration.Text = "Project Configuration";
            ProjectConfiguration.UseVisualStyleBackColor = true;
            // 
            // PathUpdateBtn
            // 
            PathUpdateBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PathUpdateBtn.Enabled = false;
            PathUpdateBtn.Location = new Point(439, 36);
            PathUpdateBtn.Name = "PathUpdateBtn";
            PathUpdateBtn.Size = new Size(35, 23);
            PathUpdateBtn.TabIndex = 3;
            PathUpdateBtn.Text = "...";
            PathUpdateBtn.UseVisualStyleBackColor = true;
            PathUpdateBtn.Click += PathUpdateBtn_Click;
            // 
            // ProjectPathLabel
            // 
            ProjectPathLabel.AutoSize = true;
            ProjectPathLabel.Location = new Point(6, 40);
            ProjectPathLabel.Name = "ProjectPathLabel";
            ProjectPathLabel.Size = new Size(22, 15);
            ProjectPathLabel.TabIndex = 2;
            ProjectPathLabel.Text = "---";
            // 
            // ProjectNameInput
            // 
            ProjectNameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProjectNameInput.Enabled = false;
            ProjectNameInput.Location = new Point(91, 6);
            ProjectNameInput.Name = "ProjectNameInput";
            ProjectNameInput.Size = new Size(383, 23);
            ProjectNameInput.TabIndex = 1;
            ProjectNameInput.TextChanged += ProjectNameInput_TextChanged;
            ProjectNameInput.Validating += ProjectNameInput_Validating;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 9);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Project Name";
            // 
            // SystemConfiguration
            // 
            SystemConfiguration.Location = new Point(4, 24);
            SystemConfiguration.Name = "SystemConfiguration";
            SystemConfiguration.Padding = new Padding(3);
            SystemConfiguration.Size = new Size(482, 422);
            SystemConfiguration.TabIndex = 1;
            SystemConfiguration.Text = "System Configuration";
            SystemConfiguration.UseVisualStyleBackColor = true;
            // 
            // SystemPaths
            // 
            SystemPaths.Location = new Point(4, 24);
            SystemPaths.Name = "SystemPaths";
            SystemPaths.Padding = new Padding(3);
            SystemPaths.Size = new Size(482, 422);
            SystemPaths.TabIndex = 2;
            SystemPaths.Text = "System Paths";
            SystemPaths.UseVisualStyleBackColor = true;
            // 
            // SystemDefinitions
            // 
            SystemDefinitions.Controls.Add(TransmitterGroup);
            SystemDefinitions.Location = new Point(4, 24);
            SystemDefinitions.Name = "SystemDefinitions";
            SystemDefinitions.Size = new Size(482, 422);
            SystemDefinitions.TabIndex = 3;
            SystemDefinitions.Text = "Definitions";
            SystemDefinitions.UseVisualStyleBackColor = true;
            // 
            // TransmitterGroup
            // 
            TransmitterGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TransmitterGroup.Controls.Add(TransmitterTable);
            TransmitterGroup.Controls.Add(AddTransmitterBtn);
            TransmitterGroup.Controls.Add(DeleteTransmitterBtn);
            TransmitterGroup.Controls.Add(EditTransmitterBtn);
            TransmitterGroup.Location = new Point(8, 8);
            TransmitterGroup.Name = "TransmitterGroup";
            TransmitterGroup.Size = new Size(466, 265);
            TransmitterGroup.TabIndex = 0;
            TransmitterGroup.TabStop = false;
            TransmitterGroup.Text = "Transmitters";
            // 
            // TransmitterTable
            // 
            TransmitterTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TransmitterTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TransmitterTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            TransmitterTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TransmitterTable.Location = new Point(3, 19);
            TransmitterTable.MultiSelect = false;
            TransmitterTable.Name = "TransmitterTable";
            TransmitterTable.Size = new Size(457, 203);
            TransmitterTable.TabIndex = 3;
            // 
            // AddTransmitterBtn
            // 
            AddTransmitterBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AddTransmitterBtn.Location = new Point(223, 228);
            AddTransmitterBtn.Name = "AddTransmitterBtn";
            AddTransmitterBtn.Size = new Size(75, 29);
            AddTransmitterBtn.TabIndex = 1;
            AddTransmitterBtn.Text = "Add";
            AddTransmitterBtn.UseVisualStyleBackColor = true;
            AddTransmitterBtn.Click += TransmitterAdd_Click;
            // 
            // DeleteTransmitterBtn
            // 
            DeleteTransmitterBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DeleteTransmitterBtn.Location = new Point(304, 228);
            DeleteTransmitterBtn.Name = "DeleteTransmitterBtn";
            DeleteTransmitterBtn.Size = new Size(75, 29);
            DeleteTransmitterBtn.TabIndex = 2;
            DeleteTransmitterBtn.Text = "Delete";
            DeleteTransmitterBtn.UseVisualStyleBackColor = true;
            DeleteTransmitterBtn.Click += TransmitterDelete_Click;
            // 
            // EditTransmitterBtn
            // 
            EditTransmitterBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            EditTransmitterBtn.Location = new Point(385, 228);
            EditTransmitterBtn.Name = "EditTransmitterBtn";
            EditTransmitterBtn.Size = new Size(75, 29);
            EditTransmitterBtn.TabIndex = 1;
            EditTransmitterBtn.Text = "Edit";
            EditTransmitterBtn.UseVisualStyleBackColor = true;
            EditTransmitterBtn.Click += TransmitterEdit_Click;
            // 
            // ProgramPropertiesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 450);
            Controls.Add(PropertiesContainer);
            Name = "ProgramPropertiesForm";
            Text = "Properties";
            FormClosing += ProgramPropertiesForm_FormClosing;
            PropertiesContainer.Panel1.ResumeLayout(false);
            PropertiesContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PropertiesContainer).EndInit();
            PropertiesContainer.ResumeLayout(false);
            PropertyPages.ResumeLayout(false);
            ProjectConfiguration.ResumeLayout(false);
            ProjectConfiguration.PerformLayout();
            SystemDefinitions.ResumeLayout(false);
            TransmitterGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TransmitterTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer PropertiesContainer;
        private TreeView PropertiesTree;
        private TabControl PropertyPages;
        private TabPage ProjectConfiguration;
        private TabPage SystemConfiguration;
        private TabPage SystemPaths;
        private TabPage SystemDefinitions;
        private GroupBox TransmitterGroup;
        private Button AddTransmitterBtn;
        private Button DeleteTransmitterBtn;
        private Button EditTransmitterBtn;
        private DataGridView TransmitterTable;
        private Label label1;
        private TextBox ProjectNameInput;
        private Label ProjectPathLabel;
        private Button PathUpdateBtn;
    }
}