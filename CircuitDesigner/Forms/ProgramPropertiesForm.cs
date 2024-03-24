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

        private DataTable TransmitterSource;

        internal ProgramPropertiesForm(ProgramPersist program, ProjectState? project = null)
        {
            InitializeComponent();

            InitProgramSettings(program);
            InitProjectSettings(project);
            UpdateTransmitterList();

            InitPages();
        }

        public void SetSelectedPage(string name)
        {
            PropertyPages.TabPages.Clear();

            foreach (var item in PropertiesTree.Nodes.OfType<TreeNode>())
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

            foreach (var node in PropertiesTree.Nodes.OfType<TreeNode>())
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ProgramPropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Project?.Save();
            Program.Save();
            Definitions.GetInstance().Save();
        }

        [MemberNotNull(nameof(TransmitterSource))]
        private void UpdateTransmitterList()
        {
            const string NameCol = "Name";
            const string ChargeMultCol = "Q Coeff";
            const string EffectCol = "Effect";

            var defs = Definitions.GetInstance();

            if(TransmitterSource == null)
            {
                TransmitterSource = new();
                TransmitterSource.Columns.Add(NameCol);
                TransmitterSource.Columns.Add(ChargeMultCol);
                TransmitterSource.Columns.Add(EffectCol);

                
            } else
            {
                TransmitterSource.Clear();
            }

            foreach(var trans in defs.Transmitters)
            {
                var row = TransmitterSource.NewRow();

                row[NameCol] = trans.Name;
                row[ChargeMultCol] = trans.ChargeMultipler;
                row[EffectCol] = Enum.GetName(trans.Effect);

                TransmitterSource.Rows.Add(row);
            }

            TransmitterTable.DataSource = TransmitterSource;
        }

        private void TransmitterAdd_Click(object sender, EventArgs e)
        {
            UpdateTransmitterList();
        }

        private void TransmitterDelete_Click(object sender, EventArgs e)
        {
            UpdateTransmitterList();
        }

        private void TransmitterEdit_Click(object sender, EventArgs e)
        {
            UpdateTransmitterList();
        }
    }
}
