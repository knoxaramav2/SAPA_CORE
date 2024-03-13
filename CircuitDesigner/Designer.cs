using CircuitDesigner.Controls;
using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Diagnostics;
using System.Reflection;
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
            ViewManager = new();
            ViewsDropDown.Items.AddRange(ViewManager.ListViewIds());
            ViewsDropDown.SelectedIndex = 0;

            PersistState = ProgramPersist.FromFile() ?? new ProgramPersist();
            SetControlStates(ProjectState != null);
            LoadAsMode(PersistState.Mode);
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
            Text = $"SAPA Circuit Designer {title}";
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


        //Form Control
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Form1()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();

            //InputsGroup.CollapseHeader.Text = "Inputs";
            //OutputsGroup.CollapseHeader.Text = "Outputs";

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
        }

        private void UpdateRegionTab(RegionModel? model)
        {
            UpdateToTextField(RegionIDInput, model?.Name ?? "");
            UpdateToDropdown(RegionConnectionsDropdown,
                model?.Connections.Select(x => x.Name).ToArray() ?? []
                );
            UpdateToListBox(InputsList, model?.Inputs.Select(x => x.Name).ToArray() ?? []);
            UpdateToListBox(OutputsList, model?.Outputs.Select(x => x.Name).ToArray() ?? []);
        }

        private void UpdateNeuronTab(NeuronModel? model)
        {
            UpdateToTextField(NeuronIDInput, model?.Name ?? "");
            UpdateToDropdown(NeuronConnectionsDropdown,
                model?.Dendrites.Select(x => x.Target.Name).ToArray() ?? []
                );
            UpdateToTextField(NeuronChargeInput, model?.Charge.ToString() ?? "");
            UpdateToTextField(NeuronDecayInput, model?.Decay.ToString() ?? "");
            UpdateToTextField(NeuronBiasInput, model?.Bias.ToString() ?? "");
        }

        private void UpdateProjectTab()
        {
            var view = ViewManager.CurrentView;
            CurrentIDInput.Text = view.ID;
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
                        ViewManager.CreateView(model.Name, Models.DesignMode.SystemMode);
                    }
                    UpdateRegionTab((RegionModel?)model);
                    break;
                default: throw new Exception("Invalid update mode");
            }
        }

        protected void OnNodeCreated(object sender, INodeModel model)
        {
            //System.Windows.Forms.MenuItem item;
            switch (ViewManager.CurrentView.ViewMode)
            {
                case Models.DesignMode.CircuitMode:
                    break;
                case Models.DesignMode.SystemMode:
                    ViewsDropDown.Items.Add(model.Name);
                    break;
                default: throw new Exception("Invalid create mode");
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

        private void InitCallback()
        {
            designBoard.NodeSelected += OnNodeUpdated;
            designBoard.NodeCreated += OnNodeCreated;
            designBoard.NodeDeleted += OnNodeRemoved;
        }

        private void ViewsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var regionName = ViewsDropDown.SelectedItem?.ToString() ?? "";
            var view = ViewManager.SwitchView(regionName);

            if (view != null)
            {
                LoadAsMode(Models.DesignMode.CircuitMode);
            }
        }

        private void CurrentIDInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ProjectState == null || CurrentIDInput.Text == "") { e.Cancel = true; return; }
            ProjectState.Name = CurrentIDInput.Text;
        }

        private void RegionIDInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (RegionIDInput.Text == "") { e.Cancel = true; return; }

            var oldId = ViewManager.GetRegionID();
            ViewManager.SetRegionID(RegionIDInput.Text);

            var oldIdx = ViewsDropDown.SelectedIndex;
            ViewsDropDown.Items.RemoveAt(oldIdx);
            ViewsDropDown.Items.Insert(oldIdx, RegionIDInput.Text);
        }

        private void NeuronIDInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NeuronIDInput.Text == ""
                || ViewManager.CurrentView.Selected is not NeuronControl neuron) { e.Cancel = true; return; }
        }

        private void NeuronChargeInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NeuronChargeInput.Text == "") { e.Cancel = true; return; }
        }

        private void NeuronDecayInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NeuronDecayInput.Text == "") { e.Cancel = true; return; }
        }

        private void NeuronBiasInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NeuronBiasInput.Text == "") { e.Cancel = true; return; }
        }

        private void collapseGroup1_Load(object sender, EventArgs e)
        {

        }

        private void collapseGroup1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
