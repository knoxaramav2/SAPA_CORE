using CircuitDesigner.Controls;
using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CircuitDesigner
{
    public partial class Form1 : Form
    {
        private ProjectState? ProjectState = null;
        private ProgramPersist PersistState;
        public ViewManager ViewManager;

        #region FILE MANAGEMENT

        private void SaveAll()
        {
            SaveProject();
        }

        private void NewProject()
        {
            SaveAll();
            var path = FileIO.ProjectPath;
            FileIO.AssurePath(path);

            SaveFileDialog dlg = new()
            {
                Title = "New Circuit",
                Filter = "Circuit | *.scd",
                InitialDirectory = path
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var filePath = dlg.FileName;
                NewProject(filePath);
            }
        }

        private void NewProject(string path)
        {
            ProjectState = new ProjectState(path);
            PersistState.UpdateProjectInfo(ProjectState);
            SaveAll();
        }

        private void LoadProject()
        {
            SaveAll();
            var path = FileIO.ProjectPath;
            FileIO.AssurePath(path);

            OpenFileDialog dlg = new()
            {
                Title = "Load Circuit",
                Filter = "Circuit | *.scd",
                InitialDirectory = path
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var filePath = dlg.FileName;
                LoadProject(filePath);
            }

            var title = ProjectState?.Name != null ? " - " + ProjectState?.Name : "";
            SetTitle(title);

            CurrentIDInput.Text = ProjectState?.Name ?? "";
        }

        private void LoadProject(string path)
        {
            ProjectState = ProjectState.FromFile(path);
            if (ProjectState == null)
            {
                //TODO ERROR
                return;
            }
            PersistState.UpdateProjectInfo(ProjectState);
            SaveAll();

            SetControlStates(true);
        }

        private void SaveProject(string? path = null)
        {
            PersistState.Save();
            ProjectState?.Save();

            CurrentIDInput.Text = ProjectState?.Name ?? "";
        }

        private void SetControlStates(bool enable = false)
        {
            saveAsToolStripMenuItem.Enabled = enable;
            saveToolStripMenuItem.Enabled = enable;
            CurrentIDInput.Enabled = enable;
        }

        private void NewProjectToolStripItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void OpenProjectToolStripItem_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void RecentProjectToolStripItem_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void SaveProjectToolStripItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void SaveAsProjectToolStripItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Form Control
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Form1()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();
            InitOnLoad();
            InitAppearance();
        }

        private void SetTitle(string? projectName = "", string currentView = "")
        {
            projectName ??= ProjectState?.Name != null ? " - " + ProjectState?.Name : "";
            currentView = string.IsNullOrEmpty(currentView) ? "" : $"({currentView})";
            Text = $"SAPA Circuit Designer {projectName}{currentView}";
        }

        private void InitAppearance()
        {
            AddInputButton.FlatAppearance.BorderSize = 0;
            RemoveInputButton.FlatAppearance.BorderSize = 0;
            AddOutputButton.FlatAppearance.BorderSize = 0;
            RemoveOutputButton.FlatAppearance.BorderSize = 0;
        }

        private void InitOnLoad()
        {
            ViewManager = new(designBoard);
            PersistState = ProgramPersist.FromFile() ?? new ProgramPersist();
            SetControlStates(ProjectState != null);
            LoadView(ViewManager.RootView);
            SetBindings();
        }

        private void SetBindings()
        {
            UpdateViewsBinding();
            UpdateRegionBindings();
            UpdateNeuronBindings();
        }

        private void UpdateRegionBindings()
        {
            if (ViewManager.GetFocused() is RegionModel model)
            {
                SetListBinding(RegionConnectionsDropdown, nameof(INodeModel.Name), nameof(INodeModel.ID), model.Connections);
                SetListBinding(RegionInputsList, nameof(INodeModel.Name), nameof(INodeModel.ID), model.Inputs);
                SetListBinding(RegionOutputsList, nameof(INodeModel.Name), nameof(INodeModel.ID), model.Outputs);
                SetBinding(RegionNameInput, model, nameof(model.Name));
            }
        }

        private void AddViewNode(ViewData root, Guid? parentID = null)
        {
            if (ViewTree.Nodes.Find(root.ID.ToString(), true).Length > 0) { return; }

            var node = new TreeNode
            {
                Name = root.ID.ToString(),
                Text = root.Name
            };

            if (parentID == null)
            {
                ViewTree.Nodes.Add(node);
            }
            else
            {
                var parent = ViewTree.Nodes.Find(parentID.ToString() ?? "", true);
                if (parent.Length != 1)
                {
                    throw new Exception($"Node ID mismatch: Found ({parent.Length}) results for {parentID}");
                }

                parent[0].Nodes.Add(node);
            }
        }

        private void RemoveViewNode(Guid id)
        {
            var node = ViewTree.Nodes.Find(id.ToString() ?? "", true);
            if (node.Length != 1)
            {
                throw new Exception($"Node ID mismatch: Found ({node.Length}) results for {id}");
            }

            node[0].Nodes.Remove(node[0]);
        }

        private void UpdateViewsBinding()
        {
            //var selected = ViewTree.SelectedNode;
            ViewTree.Nodes.Clear();
            foreach (var item in ViewManager.ListViews())
            {
                AddViewNode(item, item.ParentView?.ID);
            }
        }

        private static void SetBinding(Control control, INodeModel model, string memberName, DataSourceUpdateMode mode = DataSourceUpdateMode.OnValidation)
        {
            var nameBinding = new Binding("Text", model, memberName, true, mode);
            control.DataBindings.Clear();
            control.DataBindings.Add(nameBinding);
        }

        private static void SetListBinding<T>(ListControl control, string dspName, string valName, List<T> dataSource)
        {
            control.DisplayMember = dspName;
            control.ValueMember = valName;
            control.DataSource = dataSource;
        }

        private void UpdateNeuronBindings()
        {
            if (ViewManager.GetFocused() is NeuronModel model)
            {
                SetListBinding(NeuronInputsList, nameof(Dendrite.Name), nameof(Dendrite.ID), model.Dendrites);
                SetListBinding(NeuronOutputsList, nameof(INodeModel.Name), nameof(INodeModel.ID), model.Connections);

                SetBinding(NeuronNameInput, model, nameof(model.Name));
                SetBinding(NeuronChargeInput, model, nameof(model.Charge));
                SetBinding(NeuronDecayInput, model, nameof(model.Decay));
                SetBinding(NeuronBiasInput, model, nameof(model.Bias));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCallback();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void VerifyMenuButton_Click(object sender, EventArgs e)
        {
            Verify();
        }

        private void BuildMenuButton_Click(object sender, EventArgs e)
        {
            Build();
        }

        private bool SwitchToView(Guid viewId)
        {
            var current = ViewManager.SwitchView(viewId);
            if (current == null)
            {

                Debug.WriteLine($"View for ID={viewId} not found");
                return false;
            }

            LoadView(current);
            return true;
        }

        private void LoadView(ViewData view)
        {
            SetTabPage(PropertyTabs, RegionTab, true);
            SetTabPage(PropertyTabs, NeuronTab, true);
            UpdateRegionTab(null);
            UpdateNeuronTab(null);
            UpdateProjectTab();
            designBoard.SwitchContext(ViewManager.CurrentView);
            SetTitle(ProjectState?.Name ?? "", ViewManager.CurrentView.Name);
        }

        private void UpdateRegionTab(RegionModel? model)
        {
            UpdateRegionBindings();
        }

        private void UpdateNeuronTab(NeuronModel? model)
        {
            UpdateNeuronBindings();
        }

        private void UpdateProjectTab()
        {
            var view = ViewManager.CurrentView;
            CurrentIDInput.Text = view.Name;
            CurrentViewNameLabel.Text = view.Name;
        }
        #endregion

        #region Data Update
        private static void SetTabPage(TabControl control, TabPage page, bool enabled)
        {
            if (!enabled)
            {
                control.TabPages.Remove(page);
            }
            else if (!control.TabPages.Contains(page))
            {
                control.TabPages.Add(page);
            }
        }

        private void SplitContainer1_Panel2_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        #endregion

        #region Callbacks
        protected void OnNodeUpdated(object sender, INodeModel? model)
        {
            if (model == null) { return; }
            if (model is NeuronModel neuron) { UpdateNeuronTab(neuron); }
            else if (model is RegionModel region) { UpdateRegionTab(region); }
        }

        protected void OnNodeCreated(object sender, NodeControl node)
        {
            var current = ViewManager.CurrentView;
            if (node is RegionControl region)
            {
                var view = ViewManager.CreateView(region, current, false);

                AddViewNode(view, view.ParentView?.ID);
            }
        }

        protected void OnNodeRemoved(object sender, INodeModel model)
        {
            ViewManager.DeleteView(model.ID);

            if (model is RegionModel region)
            {
                RemoveViewNode(region.ID);
            }
        }

        protected void OnRegionEnter(object sender, RegionModel model)
        {
            SwitchToView(model.ID);
        }

        protected void OnRegionExit(object sender, RegionModel model)
        {
            var parentId = model.Host.ParentRegion?.ModelID;
            if (parentId == null)
            {
                Debug.WriteLine($"View {model.Name} has no parent");
                return;
            }

            SwitchToView(parentId.Value);
        }

        private void InitCallback()
        {
            designBoard.NodeSelected += OnNodeUpdated;
            designBoard.NodeCreated += OnNodeCreated;
            designBoard.NodeDeleted += OnNodeRemoved;
            designBoard.RegionEnter += OnRegionEnter;
            designBoard.RegionExit += OnRegionExit;
        }

        private void CurrentIDInput_Validating(object sender, CancelEventArgs e)
        {
            if (ProjectState == null || CurrentIDInput.Text == "") { e.Cancel = true; return; }
            ProjectState.Name = CurrentIDInput.Text;
        }

        private void RegionIDInput_Validating(object sender, CancelEventArgs e)
        {
            if (RegionNameInput.Text == "") { e.Cancel = true; return; }
            ViewManager.SetRegionName(RegionNameInput.Text);
        }

        private void NeuronIDInput_Validating(object sender, CancelEventArgs e)
        {
            if (NeuronNameInput.Text == ""
                || ViewManager.CurrentView.Selected is not NeuronControl neuron) { e.Cancel = true; return; }
        }

        private void NeuronChargeInput_Validating(object sender, CancelEventArgs e)
        {
            if (NeuronChargeInput.Text == "") { e.Cancel = true; return; }
        }

        private void NeuronDecayInput_Validating(object sender, CancelEventArgs e)
        {
            if (NeuronDecayInput.Text == "") { e.Cancel = true; return; }
        }

        private void NeuronBiasInput_Validating(object sender, CancelEventArgs e)
        {
            if (NeuronBiasInput.Text == "") { e.Cancel = true; return; }
        }

        private void ViewTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                var root = ViewTree.Nodes.Find(ViewManager.RootView.ID.ToString(), true);
                ViewTree.SelectedNode = root[0];
                return;
            }

            var id = new Guid(e.Node.Name);
            SwitchToView(id);
        }

        #endregion

        #region Build

        public static bool Verify()
        {
            return true;
        }

        public void Build()
        {
            Verify();
        }

        #endregion

    }

}
