using CircuitDesigner.Util;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    internal class ProjectState
    {
        private const string DefaultProjectName = "__default__";
        private const string RootCircuitName = "Root";

        [JsonProperty]
        public string ProjectName { get; internal set; } = "";
        [JsonProperty]
        public string ProjectDir { get; internal set; } = "";

        [JsonProperty]
        public List<Transmitter> Transmitters { get; set; } = [];

        [JsonProperty]
        public CircuitModel RootModel { get; private set; }

        [JsonIgnore]
        public CircuitModel CurrentCircuit { get; private set; }

        [JsonProperty]
        public Guid CurrentID { get; set; }

        private string FilePath()
        {
            return Path.Join(ProjectDir, $"{ProjectName}{FileUtil.ProjectExt}");
        }

        
        public ProjectState()
        {
            RootModel = new(RootCircuitName);
            CurrentCircuit = RootModel;
            CurrentID = CurrentCircuit.ID;
            var projectPath = Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");
            Rename(projectPath);
        }

        [JsonConstructor]
        public ProjectState(string? projectPath = null)
        {
            RootModel = new(RootCircuitName);
            CurrentCircuit = RootModel;
            CurrentID = CurrentCircuit.ID;
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
            else
            {
                ret.NavigateToCircuit(ret.CurrentID);
            }

            return ret;
        }

        public CircuitModel NavigateToCircuit(Guid id)
        {
            CircuitModel ret = RootModel;

            if (RootModel.ID != id) 
            {
                ret = RootModel.SearchSubCircuits(id) ?? RootModel;
                CurrentID = ret.ID;
            }

            CurrentCircuit = ret;
            return ret;
        }

        public void Save()
        {
            if (IsDefaultProject() || string.IsNullOrEmpty(ProjectName)) { return; }
            FileUtil.Save(FilePath(), this);
        }
    }
}
