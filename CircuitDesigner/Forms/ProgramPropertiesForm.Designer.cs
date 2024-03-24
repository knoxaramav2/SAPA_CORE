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
            SystemConfiguration = new TabPage();
            SystemPaths = new TabPage();
            SystemDefinitions = new TabPage();
            ((System.ComponentModel.ISupportInitialize)PropertiesContainer).BeginInit();
            PropertiesContainer.Panel1.SuspendLayout();
            PropertiesContainer.Panel2.SuspendLayout();
            PropertiesContainer.SuspendLayout();
            PropertyPages.SuspendLayout();
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
            PropertiesContainer.Size = new Size(800, 450);
            PropertiesContainer.SplitterDistance = 204;
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
            PropertiesTree.Size = new Size(204, 450);
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
            PropertyPages.Size = new Size(592, 450);
            PropertyPages.TabIndex = 0;
            // 
            // ProjectConfiguration
            // 
            ProjectConfiguration.Location = new Point(4, 24);
            ProjectConfiguration.Name = "ProjectConfiguration";
            ProjectConfiguration.Padding = new Padding(3);
            ProjectConfiguration.Size = new Size(584, 422);
            ProjectConfiguration.TabIndex = 0;
            ProjectConfiguration.Text = "Project Configuration";
            ProjectConfiguration.UseVisualStyleBackColor = true;
            // 
            // SystemConfiguration
            // 
            SystemConfiguration.Location = new Point(4, 24);
            SystemConfiguration.Name = "SystemConfiguration";
            SystemConfiguration.Padding = new Padding(3);
            SystemConfiguration.Size = new Size(584, 422);
            SystemConfiguration.TabIndex = 1;
            SystemConfiguration.Text = "System Configuration";
            SystemConfiguration.UseVisualStyleBackColor = true;
            // 
            // SystemPaths
            // 
            SystemPaths.Location = new Point(4, 24);
            SystemPaths.Name = "SystemPaths";
            SystemPaths.Padding = new Padding(3);
            SystemPaths.Size = new Size(584, 422);
            SystemPaths.TabIndex = 2;
            SystemPaths.Text = "System Paths";
            SystemPaths.UseVisualStyleBackColor = true;
            // 
            // SystemDefinitions
            // 
            SystemDefinitions.Location = new Point(4, 24);
            SystemDefinitions.Name = "SystemDefinitions";
            SystemDefinitions.Size = new Size(584, 422);
            SystemDefinitions.TabIndex = 3;
            SystemDefinitions.Text = "Definitions";
            SystemDefinitions.UseVisualStyleBackColor = true;
            // 
            // ProgramPropertiesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PropertiesContainer);
            Name = "ProgramPropertiesForm";
            Text = "Properties";
            PropertiesContainer.Panel1.ResumeLayout(false);
            PropertiesContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PropertiesContainer).EndInit();
            PropertiesContainer.ResumeLayout(false);
            PropertyPages.ResumeLayout(false);
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
    }
}