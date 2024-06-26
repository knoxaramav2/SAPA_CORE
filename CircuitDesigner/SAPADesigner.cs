using CircuitDesigner.BuildEngine;
using CircuitDesigner.Forms;
using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static CircuitDesigner.Util.NCLogger;

namespace CircuitDesigner
{
    public partial class SapaDesigner : Form
    {
        readonly ProgramPersist PersistState = ProgramPersist.Load();
        ProjectState ProjectState = ProjectState.LoadOrDefault();
        readonly List<(int, Guid)> InputListIds = [];
        readonly List<(int, Guid)> OutputListIds = [];
        private INodeModel? LastUpdateModel = null;

        TabPage[] PropertiesReference;

        #region Form Updates

        #region Bindings

        internal void BindText(TextBox tbox, object model, string name)
        {
            tbox.DataBindings.Clear();
            tbox.DataBindings.Add("Text", model, name);
        }
        internal void BindTransmitterList(CheckedListBox cbox, NeuronModel model, string name)
        {
            ((ListBox)cbox).DataSource = model.Transmitters.Select(x => x.Item2.Name).ToList();
            //cbox.CheckedItems = model.Transmitters;
        }

        #endregion

        public SapaDesigner()
        {
            InitializeComponent();
            InitSystem();
            SetupComponents();
        }

        private void InitSystem()
        {
            FileUtil.AssureDirectories();
            UpdateProject(ProjectState, true);
            InitEvents();
        }

        [MemberNotNull(nameof(PropertiesReference))]
        private void SetupComponents()
        {
            PropertiesReference = PropertiesTabs.TabPages.OfType<TabPage>().ToArray();
            PropertiesTabs.TabPages.Clear();
        }

        private void InitEvents()
        {
            DesignBoard.BroadcastModel += UpdateModelProperties;
        }

        private void UpdateControlEnabledStates()
        {
            var enabled = !ProjectState.IsDefaultProject();

            ToolStripSave.Enabled = enabled;
            MenuStripBuild.Enabled = enabled;
        }

        private void UpdateRecentsList()
        {
            var recents = PersistState.GetRecents();
            var items = new ToolStripMenuItem[recents.Count];

            for (var i = 0; i < items.Length; i++)
            {
                var dataTuple = recents[i];
                var item = new ToolStripMenuItem
                {
                    Name = dataTuple.Item1,
                    Text = dataTuple.Item2,
                };

                item.Click += RecentItemSelected;
                items[i] = item;
            }

            ToolStripRecent.DropDownItems.Clear();
            ToolStripRecent.DropDownItems.AddRange(items);
        }

        private void UpdateCircuitTree()
        {
            CircuitTree.Nodes.Clear();

            var root = ProjectState.RootCircuit;
            AddCircuitTreeLeaf(null, root);
        }

        private void AddCircuitTreeLeaf(TreeNode? parent, CircuitModel model)
        {
            var node = new TreeNode
            {
                Name = model.ID.ToString(),
                Text = model.Name
            };

            if (parent == null) { CircuitTree.Nodes.Add(node); }
            else
            { parent.Nodes.Add(node); }


        }

        private void UpdateStatus()
        {
            ToolStripProgressBar.Value = 0;
            ToolStripStatusText.Text = "Ready";
            UpdateControlEnabledStates();
            UpdateRecentsList();
            UpdateCircuitTree();
            UpdateInputOutputList();
        }

        private void UpdateProjectLabel()
        {
            ProjectNameLabel.Text = ProjectState.ProjectName;
            ToolStripProjectName.Text = ProjectState.ProjectName;
        }

        private void UpdateProject(ProjectState? state = null, bool organizeComponents = false)
        {
            if (state != null) { ProjectState = state; }
            PersistState.SetRecent(ProjectState.ProjectName, ProjectState.ProjectDir);

            UpdateProjectLabel();
            UpdateStatus();
            NavigateToCircuit(ProjectState.RootCircuit.ID);
            if (organizeComponents)
            {
                DesignBoard.RepositionComponents();
            }

            PersistState.Save();
        }

        private void NavigateToCircuit(Guid id)
        {
            var model = ProjectState.NavigateToCircuit(id);
            DesignBoard.LoadCircuit(model);
        }

        private void UpdateInputOutputList()
        {
            var inputs = ProjectState.CurrentCircuit.Inputs;
            var outputs = ProjectState.CurrentCircuit.Outputs;

            InputsList.Items.Clear();
            OutputsList.Items.Clear();

            InputsList.ItemCheck -= InputsList_ItemCheck;
            OutputsList.ItemCheck -= OutputsList_ItemCheck;

            InputListGroup.Text = $"Inputs ({inputs.Count})";
            OutputListGroup.Text = $"Outputs ({outputs.Count})";

            foreach (var input in inputs)
            {
                var id = InputsList.Items.Add(input.Name, input.Enabled);
                InputListIds.Add((id, input.ID));
            }

            foreach (var output in outputs)
            {
                var id = OutputsList.Items.Add(output.Name, output.Enabled);
                OutputListIds.Add((id, output.ID));
            }

            InputsList.ItemCheck += InputsList_ItemCheck;
            OutputsList.ItemCheck += OutputsList_ItemCheck;
        }

        #endregion

        #region File Handling

        private static string FileDialog(string title, string filter, string basePath, bool isSave)
        {
            string filePath = string.Empty;
            FileDialog dlg;

            if (isSave)
            {
                dlg = new SaveFileDialog
                {
                    Title = title,
                    Filter = filter,
                    InitialDirectory = basePath
                };
            }
            else
            {
                dlg = new OpenFileDialog
                {
                    Title = title,
                    Filter = filter,
                    InitialDirectory = basePath
                };
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName;
            }

            return filePath;
        }

        private void Open(string? path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                var basePath = FileUtil.ProjectsUri;
                path = FileDialog("Open Circuit", $"Circuit | *{FileUtil.ProjectExt}", basePath, false);
            }

            if (string.IsNullOrEmpty(path)) { return; }

            UpdateProject(ProjectState.LoadOrDefault(path), false);
        }

        private void Save(string? path = null, bool saveAs = false)
        {
            var project = ProjectState;

            if (!string.IsNullOrEmpty(path) || saveAs || ProjectState.IsDefaultProject())
            {
                var basePath = path ?? FileUtil.ProjectsUri;
                var savePath = FileDialog("Open Circuit", $"Circuit | *{FileUtil.ProjectExt}", basePath, true);
                if (string.IsNullOrEmpty(savePath)) { return; }
                if (saveAs) { ProjectState.Rename(savePath); }
                else { project = ProjectState.LoadOrDefault(savePath); }
            }

            ProjectState.Save();
            PersistState.Save();
            Definitions.Save();

            UpdateProject(project, false);
        }

        private void New()
        {
            var basePath = FileUtil.ProjectsUri;
            var newPath = FileDialog("New Circuit", $"Circuit | *{FileUtil.ProjectExt}", basePath, true);
            if (string.IsNullOrEmpty(newPath)) { return; }

            UpdateProject(new ProjectState(newPath), true);
            DesignBoard.RepositionComponents();
            Save();
        }

        #endregion

        #region Events
        private void SapaDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProjectState.IsDefaultProject()) { return; }
            Save();
        }

        private void ToolStripSave_Click(object sender, EventArgs e) { Save(); }

        private void ToolStripSaveAs_Click(object sender, EventArgs e) { Save(saveAs: true); }

        private void ToolStripOpen_Click(object sender, EventArgs e) { Open(); }

        private void ToolStripExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void RecentItemSelected(object? sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem muItem) { return; }
            Open(muItem.Name);
        }

        private void ToolStripProperties_Click(object sender, EventArgs e)
        {
            (new ProgramPropertiesForm(PersistState, ProjectState.IsDefaultProject() ? null : ProjectState)).ShowDialog();
        }

        private void InputsList_KeyUp(object sender, KeyEventArgs e)
        {
            var inputs = ProjectState.CurrentCircuit.Inputs;

            switch (e.KeyCode)
            {
                case Keys.Insert:
                    UpdateModel<InputModel>(
                    new InputModel(AgnosticModelUtil.AutoModelName<InputModel>
                        (inputs.Select(x => x.Name).ToArray())));
                    break;
                case Keys.Delete:
                    var idx = InputsList.SelectedIndex;
                    var selected = InputListIds.First(x => x.Item1 == idx);
                    var model = inputs.FirstOrDefault(x => x.ID == selected.Item2);
                    if (model == null)
                    {
                        Log($"Input {selected} not found", LogType.WRN);
                        return;
                    }
                    UpdateModel<InputModel>(model, null, true);
                    break;
                default: return;
            }

            UpdateInputOutputList();
        }

        private void OutputsLists_KeyUp(object sender, KeyEventArgs e)
        {
            var outputs = ProjectState.CurrentCircuit.Outputs;

            switch (e.KeyCode)
            {
                case Keys.Insert:
                    UpdateModel<OutputModel>(
                    new OutputModel(AgnosticModelUtil.AutoModelName<OutputModel>
                        (outputs.Select(x => x.Name).ToArray())));
                    break;
                case Keys.Delete:
                    var idx = OutputsList.SelectedIndex;
                    var selected = OutputListIds.First(x => x.Item1 == idx);
                    var model = outputs.FirstOrDefault(x => x.ID == selected.Item2);
                    if (model == null)
                    {
                        Log($"Input {selected} not found", LogType.WRN);
                        return;
                    }
                    UpdateModel<InputModel>(model, null, true);
                    break;
                default: return;
            }

            UpdateInputOutputList();
        }

        private void InputsList_DoubleClick(object sender, EventArgs e)
        {
            var inputs = ProjectState.CurrentCircuit.Inputs;
            var selected = (string?)InputsList.SelectedItem;
            if (selected == null) { return; }

            var connector = inputs.FirstOrDefault(x => x.Name.Equals(selected, StringComparison.OrdinalIgnoreCase));
            if (connector == null) { return; }

            using InputOutputForm dlg = new(connector, inputs);
            if (dlg.ShowDialog() == DialogResult.Cancel)
            { return; }

            connector.Name = dlg.ConnectorName;
            connector.Enabled = dlg.Enabled;

            UpdateInputOutputList();
        }

        private void OutputsLists_DoubleClick(object sender, EventArgs e)
        {
            UpdateInputOutputList();
        }

        private void InputsList_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            e.NewValue = e.CurrentValue;
        }

        private void OutputsList_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            e.NewValue = e.CurrentValue;
        }

        private void UpdateModelProperties(object? sender, INodeModel model)
        {
            LastUpdateModel = model;
            var currTab = PropertiesTabs.SelectedTab;
            TabPage? selTab = null;

            if (model is InputModel input)
            {
                InputPropertiesName.Text = input.Name;
                selTab = PropertiesReference.FirstOrDefault(x => (string?)x.Tag == "InputTag");
                BindText(InputDecayInput, input, nameof(input.Decay));
            }
            else if (model is OutputModel output)
            {
                OutputPropertiesName.Text = output.Name;
                selTab = PropertiesReference.FirstOrDefault(x => (string?)x.Tag == "OutputTag");
                BindText(OutputDecayInput, output, nameof(output.Decay));
            }
            else if (model is NeuronModel neuron)
            {
                BindText(NeuronNameInput, neuron, nameof(neuron.Name));
                BindText(NeuronThresholdInput, neuron, nameof(neuron.Threshold));
                BindText(NeuronResistanceInput, neuron, nameof(neuron.Resistance));
                BindTransmitterList(NeuronTransmittersInput, neuron, nameof(neuron.Transmitters));
                UpdateTransmitterList(NeuronTransmittersInput, neuron);
                BindText(NaMolInput_Nrn, neuron.Ions.Na, nameof(neuron.Ions.Na.Concentration));
                BindText(NaPartInput_Nrn, neuron.Ions.Na, nameof(neuron.Ions.Na.Parts));
                BindText(KMolInput_Nrn, neuron.Ions.K, nameof(neuron.Ions.K.Concentration));
                BindText(KPartInput_Nrn, neuron.Ions.K, nameof(neuron.Ions.K.Parts));
                BindText(ClMolInput_Nrn, neuron.Ions.Cl, nameof(neuron.Ions.Cl.Concentration));
                BindText(ClPartInput_Nrn, neuron.Ions.Cl, nameof(neuron.Ions.Cl.Parts));
                BindText(CaMolInput_Nrn, neuron.Ions.Ca, nameof(neuron.Ions.Ca.Concentration));
                BindText(CaPartInput_Nrn, neuron.Ions.Ca, nameof(neuron.Ions.Ca.Parts));
                NrnRestVLabel.Text = $"{neuron.RestingPotential:0.0000}";
                selTab = PropertiesReference.FirstOrDefault(x => (string?)x.Tag == "NeuronTag");
            }
            else if (model is CircuitModel circuit)
            {
                CircuitPropertiesName.Text = circuit.Name;
                selTab = PropertiesReference.FirstOrDefault(x => (string?)x.Tag == "CircuitTab");
                BindText(NaMolInput_Circ, circuit.Ions.Na, nameof(circuit.Ions.Na.Concentration));
                BindText(NaPartInput_Circ, circuit.Ions.Na, nameof(circuit.Ions.Na.Parts));
                BindText(KMolInput_Circ, circuit.Ions.K, nameof(circuit.Ions.K.Concentration));
                BindText(KPartInput_Circ, circuit.Ions.K, nameof(circuit.Ions.K.Parts));
                BindText(ClMolInput_Circ, circuit.Ions.Cl, nameof(circuit.Ions.Cl.Concentration));
                BindText(ClPartInput_Circ, circuit.Ions.Cl, nameof(circuit.Ions.Cl.Parts));
                BindText(CaMolInput_Circ, circuit.Ions.Ca, nameof(circuit.Ions.Ca.Concentration));
                BindText(CaPartInput_Circ, circuit.Ions.Ca, nameof(circuit.Ions.Ca.Parts));
            }

            if (selTab == null || currTab == selTab) { return; }
            PropertiesTabs.TabPages.Clear();
            PropertiesTabs.TabPages.Add(selTab);
        }

        internal void OnZoomableDoubleClick(object sender, EventArgs e)
        {
            if (sender is not CheckedListBox) { return; }
            var ioList = (CheckedListBox)sender;
            var idList = ioList == InputsList ? InputListIds : OutputListIds;

            var idx = ioList.SelectedIndex;
            var sel = idList.First(x => x.Item1 == idx);
            DesignBoard.ZoomTo(sel.Item2);
        }

        private void ToolStripBuild_Click(object sender, EventArgs e)
        {
            ToolStripStatusText.Text = $"Building";

            var engine = new CircuitEngine(ProjectState, ToolStripProgressBar);
            var buildString = engine.Build();
            if (buildString == null)
            {
                ToolStripStatusText.Text = $"Build Failure";
                MessageBox.Show("Build errors, see log", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ToolStripStatusText.Text = $"Build Success";
            var path = Path.Join(FileUtil.BuildUri, $"{ProjectState.ProjectName}{FileUtil.CircuitExt}");

            FileUtil.Save(path, buildString);
        }

        private void ToolStripVerify_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Helpers

        private void UpdateTransmitterList(CheckedListBox listbox, NeuronModel model)
        {
            for (var i = 0; i < listbox.Items.Count; i++)
            {
                listbox.SetItemChecked(i, model.Transmitters[i].Item1);
            }
        }

        private void UpdateModel<T>(INodeModel model, Point? pos = null, bool delete = false)
        {
            DesignBoard.UpdateControl(model, pos, delete);
        }

        #endregion

        #region Exposed

        internal void UpdateSelected(INodeModel model)
        {

        }

        #endregion

        private void NrnIonChanged(object sender, EventArgs e)
        {
            DesignBoard.RecalculateNeuron(ProjectState.CurrentCircuit);
        }

        private void NeuronNameInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
