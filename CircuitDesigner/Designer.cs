using CircuitDesigner.Models;
using CircuitDesigner.Util;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

namespace CircuitDesigner
{
    public partial class Form1 : Form
    {
        private ProjectState? ProjectState = null;
        private ProgramPersist PersistState;

        //FILE MANAGEMENT
        private void InitOnLoad()
        {
            PersistState = ProgramPersist.FromFile() ?? new ProgramPersist();
            SetControlStates(ProjectState != null);
        }

        private void SaveAll()
        {
            PersistState.Save();
            ProjectState?.Save();
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

        private void SetControlStates(bool enable = false)
        {
            RegionProperties.Enabled = enable;
            saveAsToolStripMenuItem.Enabled = enable;
            saveToolStripMenuItem.Enabled = enable;
            //DesignPanel.Enabled = enable;
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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            SaveAll();
        }

        //Designer Control

    }
}
