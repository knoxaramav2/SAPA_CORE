using CircuitDesigner.Controls;
using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace CircuitDesigner
{
    public partial class Form1 : Form
    {
        private ProjectState? ProjectState = null;
        private ProgramPersist PersistState;
        public ViewManager ViewManager;

        //FILE MANAGEMENT
        private void InitOnLoad()
        {
            ViewManager = new(designBoard);
            PersistState = ProgramPersist.FromFile() ?? new ProgramPersist();
            SetControlStates(ProjectState != null);
            LoadAsMode(PersistState.Mode);
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
            if (ViewManager.CurrentView.Selected is RegionControl view)
            {
                RegionConnectionsDropdown.DisplayMember = nameof(view.ModelName);
                RegionConnectionsDropdown.ValueMember = nameof(view.ModelID);
                RegionConnectionsDropdown.DataSource = view.Connections;

                RegionConnectionsDropdown.Text = string.Empty;
                var idx = RegionConnectionsDropdown.Items.Count - 1;
                RegionConnectionsDropdown.SelectedIndex = idx;

            }

            if (ViewManager.GetFocused() is RegionModel model)
            {
                RegionInputsList.DisplayMember = nameof(model.Name);
                RegionInputsList.ValueMember = nameof(model.ID);
                RegionInputsList.DataSource = model.Inputs;

                RegionOutputsList.DisplayMember = nameof(model.Name);
                RegionOutputsList.ValueMember = nameof(model.ID);
                RegionOutputsList.DataSource = model.Inputs;

                SetBinding(RegionNameInput, model, nameof(model.Name));
            }
        }

        private void UpdateViewsBinding()
        {
            ViewsDropDown.ComboBox.DisplayMember = nameof(NodeControl.ModelName);
            ViewsDropDown.ComboBox.ValueMember = nameof(NodeControl.ModelID);
            ViewsDropDown.ComboBox.DataSource = ViewManager.CurrentView.Controls;
        }

        private void SetBinding(Control control, INodeModel model, string memberName, DataSourceUpdateMode mode=DataSourceUpdateMode.OnValidation)
        {
            var nameBinding = new Binding("Text", model, memberName, true, mode);
            control.DataBindings.Clear();
            control.DataBindings.Add(nameBinding);
        }

        private static void SetListBinding<T>(ListBox control, string dspName, string valName, List<T>dataSource)
        {
            control.DisplayMember = "Name";//dspName;
            control.ValueMember = "ID";// valName;
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
            SetTitle(ProjectState?.Name ?? "");

            CurrentIDInput.Text = ProjectState?.Name ?? "";
        }

        private void SetTitle(string projectName = "", string currentView = "")
        {
            projectName = ProjectState?.Name != null ? " - " + ProjectState?.Name : "";
            currentView = string.IsNullOrEmpty(currentView) ? "" : $"({currentView})";
            Text = $"SAPA Circuit Designer {projectName}{currentView}";
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


        //Form Control
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Form1()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();
            InitOnLoad();
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

        private bool SwitchToView(Guid viewId)
        {
            var current = ViewManager.SwitchView(viewId);
            if (current == null)
            {

                Debug.WriteLine($"View for ID={viewId} not found");
                return false;
            }

            LoadAsMode(current.ViewMode);
            return true;
        }

        private void LoadAsMode(DesignMode mode)
        {
            switch (mode)
            {
                case Models.DesignMode.SystemMode:
                    SetTabPage(PropertyTabs, RegionTab, true);
                    SetTabPage(PropertyTabs, NeuronTab, false);
                    UpdateRegionTab(null);
                    break;
                case Models.DesignMode.CircuitMode:
                    SetTabPage(PropertyTabs, RegionTab, true);
                    SetTabPage(PropertyTabs, NeuronTab, true);
                    UpdateNeuronTab(null);
                    break;
                default:
                    SetTabPage(PropertyTabs, NeuronTab, false);
                    SetTabPage(PropertyTabs, RegionTab, false);
                    break;
            }

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
            CurrentViewNameLabel.Text = view.ViewMode.ToString();
        }

        //Data Update
        private void SetTabPage(TabControl control, TabPage page, bool enabled)
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

        private void UpdateToDropdown(ComboBox dropdown, string[] values)
        {
            dropdown.Items.Clear();

            if (values.Length > 0)
            {
                dropdown.Items.AddRange(values);
                var oldIdx = dropdown.SelectedIndex;
                var numItems = dropdown.Items.Count;

                if (numItems > 0 && numItems >= oldIdx)
                {
                    dropdown.SelectedIndex = numItems - 1;
                }
                else
                {
                    dropdown.SelectedIndex = oldIdx;
                }
            }
        }

        private void UpdateToLabel()
        {

        }

        private void UpdateToNumeric(NumericUpDown updown, int value)
        {
            updown.Value = value;
        }

        private void UpdateToTextField(TextBox textfield, string value)
        {
            textfield.Text = value;
        }

        private void UpdateToListBox(ListBox list, string[] values)
        {
            list.Items.Clear();

            if (values.Length > 0)
            {
                list.Items.AddRange(values);
                var oldIdx = list.SelectedIndex;
                var numItems = list.Items.Count;

                if (numItems > 0 && numItems >= oldIdx)
                {
                    list.SelectedIndex = numItems - 1;
                }
                else
                {
                    list.SelectedIndex = oldIdx;
                }
            }
        }

        private void UpdateFromDowndown()
        {

        }

        private void UpdateFromTextField()
        {

        }

        private void UpdateFromNumeric(NumericUpDown updown, ref int value)
        {
            value = (int)updown.Value;
        }

        //Callbacks
        protected void OnNodeUpdated(object sender, INodeModel? model)
        {
            switch (ViewManager.CurrentView.ViewMode)
            {
                case Models.DesignMode.CircuitMode:
                    UpdateNeuronTab((NeuronModel?)model);
                    break;
                case Models.DesignMode.SystemMode:
                    if (model != null)
                    {

                    }
                    UpdateRegionTab((RegionModel?)model);
                    break;
                default: throw new Exception("Invalid update mode");
            }
        }

        protected void OnNodeCreated(object sender, NodeControl node)
        {
            var current = ViewManager.CurrentView;
            if (node is RegionControl region)
            {
                ViewManager.CreateView(region, current, Models.DesignMode.CircuitMode, false);
            }
        }

        protected void OnNodeRemoved(object sender, INodeModel model)
        {
            switch (ViewManager.CurrentView.ViewMode)
            {
                case Models.DesignMode.CircuitMode: break;
                case Models.DesignMode.SystemMode: break;
                default: throw new Exception("Invalid delete mode");
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

        private void ViewsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewsDropDown.SelectedItem is ViewData view && ViewManager.SwitchView(view.ID) != null)
            {
                LoadAsMode(Models.DesignMode.CircuitMode);
            }
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

    }

}
