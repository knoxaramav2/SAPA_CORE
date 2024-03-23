using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Data;

namespace CircuitDesigner
{
    public partial class SapaDesigner : Form
    {
        readonly ProgramPersist PersistState = ProgramPersist.Load();
        ProjectState ProjectState = ProjectState.LoadOrDefault();

        #region Form Updates

        #region Bindings



        #endregion

        public SapaDesigner()
        {
            InitializeComponent();
            InitSystem();
        }

        private void InitSystem()
        {
            FileUtil.AssureDirectories();
            UpdateStatus();
            UpdateProjectLabel();
        }

        private void UpdateControlEnabledStates()
        {
            var enabled = !ProjectState.IsDefaultProject();

            ToolStripSave.Enabled = enabled;
            ToolStripSaveAs.Enabled = enabled;
            MenuStripBuild.Enabled = enabled;
        }

        private void UpdateRecentsList()
        {
            var recents = PersistState.GetRecents();
            var items = new ToolStripMenuItem[recents.Count];

            for(var i = 0; i < items.Length; i++)
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

        private void UpdateStatus()
        {
            ToolStripProgressBar.Value = 0;
            ToolStripStatusText.Text = "Ready";
            UpdateControlEnabledStates();
            UpdateRecentsList();
        }

        private void UpdateProjectLabel()
        {
            ProjectNameLabel.Text = ProjectState.ProjectName;
            ToolStripProjectName.Text = ProjectState.ProjectName;
        }

        private void UpdateProject(ProjectState? state=null)
        {
            if (state != null) { ProjectState = state; }
            PersistState.SetRecent(ProjectState.ProjectName, ProjectState.ProjectDir);
            UpdateProjectLabel();
            UpdateStatus();
            PersistState.Save();
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

        private void Open(string? path=null)
        {
            if (string.IsNullOrEmpty(path))
            {
                var basePath = FileUtil.ProjectsUri;
                path = FileDialog("Open Circuit", $"Circuit | *{FileUtil.ProjectExt}", basePath, false);
            }

            if (string.IsNullOrEmpty(path)) { return; }

            UpdateProject(ProjectState.LoadOrDefault(path));
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
            UpdateProject(project);
        }

        private void New()
        {
            var basePath = FileUtil.ProjectsUri;
            var newPath = FileDialog("New Circuit", $"Circuit | *{FileUtil.ProjectExt}", basePath, true);
            if (string.IsNullOrEmpty(newPath)) { return; }

            UpdateProject(new ProjectState(newPath));
            Save();
        }

        #endregion

        #region Form Events
        private void SapaDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save();
        }

        private void ToolStripSave_Click(object sender, EventArgs e) { Save(); }

        private void ToolStripSaveAs_Click(object sender, EventArgs e) { Save(saveAs: true); }

        private void ToolStripOpen_Click(object sender, EventArgs e) { Open(); }

        private void ToolStripRecent_Click(object sender, EventArgs e)
        {

        }

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

        #endregion
    }
}
