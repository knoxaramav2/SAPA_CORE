using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CircuitDesigner.Forms
{
    public partial class ProgramPropertiesForm : Form
    {
        ProjectState? Project = null;
        ProgramPersist Program;

        private TabPage[] TabPageReference;
        private List<TreeNode> TreeNodeReference;

        internal ProgramPropertiesForm(ProgramPersist program, ProjectState? project = null)
        {
            InitializeComponent();

            InitProgramSettings(program);
            InitProjectSettings(project);
            InitPages();
        }

        public void SetSelectedPage(string name)
        {
            PropertyPages.TabPages.Clear();

            foreach(var item in PropertiesTree.Nodes.OfType<TreeNode>())
            {
                Debug.WriteLine($"{item.Name} | {item.Text} ");
            }

            var node =
                //PropertiesTree.Nodes.OfType<TreeNode>().FirstOrDefault(x => x.Name == name) ??
                PropertiesTree.Nodes.Find(name, true).FirstOrDefault();

            if (node == null) { return; }

            string key = string.Empty;

            switch (node.Name)
            {
                case "ProjectRoot":
                case "ProjectConfigNode":
                    key = "ProjectConfiguration";
                    break;
                case "SystemRoot":
                case "SystemConfigNode":
                    key = "SystemConfiguration";
                    break;
                case "PathsNode": key = "SystemPaths"; break;
                case "DefinitionsNode": key = "SystemDefinitions"; break;
                default: return;
            }

            var tab = TabPageReference.FirstOrDefault(x => x.Name == key);
            if (tab == null) { return; }
            PropertyPages.TabPages.Add(tab);
        }

        [MemberNotNull(nameof(TabPageReference))]
        [MemberNotNull(nameof(TreeNodeReference))]
        private void InitPages()
        {
            TabPageReference = PropertyPages.TabPages.OfType<TabPage>().ToArray();
            TreeNodeReference = [];

            foreach(var node in PropertiesTree.Nodes.OfType<TreeNode>())
            {
                TreeNodeReference.Add(node);
                TreeNodeReference.AddRange(node.Nodes.OfType<TreeNode>());
            }

            SetSelectedPage("SystemRoot");
        }

        private void InitProjectSettings(ProjectState? project)
        {
            Project = project;
            if (Project == null)
            {
                var root = PropertiesTree.Nodes.OfType<TreeNode>().FirstOrDefault(x => x.Name == "ProjectRoot");
                if (root == null)
                {
                    return;
                }
                PropertiesTree.Nodes.Remove(root);
            }
        }

        [MemberNotNull(nameof(Program))]
        private void InitProgramSettings(ProgramPersist program)
        {
            Program = program;
        }

        private void PropertiesTree_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void PropertiesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selected = PropertiesTree.SelectedNode;
            if (selected == null) { return; }

            SetSelectedPage(selected.Name);
        }
    }
}
