using CircuitDesigner.Util;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    internal class ProjectState
    {
        private const string DefaultProjectName = "__default__";

        [JsonProperty]
        public string ProjectName { get; internal set; } = "";
        [JsonProperty]
        public string ProjectDir { get; internal set; } = "";


        public List<Transmitter> Transmitters { get; set; } = [];


        public CircuitModel RootModel { get; set; }

        public CircuitModel CurrentCircuit { get; set; }

        private string FilePath()
        {
            return Path.Join(ProjectDir, $"{ProjectName}{FileUtil.ProjectExt}");
        }

        public ProjectState(string? projectPath = null)
        {
            RootModel = new("Root");
            CurrentCircuit = RootModel;
            projectPath ??= Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");
            Rename(projectPath);
        }

        public void Rename(string newPath)
        {            
            ProjectName = Path.GetFileNameWithoutExtension(newPath);
            ProjectDir = Path.GetDirectoryName(newPath) ?? FileUtil.ProjectsUri;
        }

        public bool IsDefaultProject() { return ProjectName == DefaultProjectName; }

        public static ProjectState LoadOrDefault(string? path = null)
        {
            path ??= Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");

            var ret = FileUtil.Load<ProjectState>(path);

            if (ret == null)
            {
                NCLogger.Log($"Unable to load project at {path}", NCLogger.LogType.WRN);
                ret = new ProjectState();
            }

            return ret;
        }

        public CircuitModel NavigateToCircuit(Guid id)
        {
            if (RootModel.ID == id) { return RootModel; }

            var ret = RootModel.SearchSubCircuits(id);
            return ret ?? RootModel;
        }

        public void Save()
        {
            if (IsDefaultProject() || string.IsNullOrEmpty(ProjectName)) { return; }
            FileUtil.Save(FilePath(), this);
        }
    }
}
