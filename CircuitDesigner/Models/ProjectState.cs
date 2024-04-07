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

        [JsonIgnore]
        public List<Transmitter> Transmitters { get; set; } = [];

        [JsonProperty]
        public CircuitModel RootCircuit { get; private set; }

        [JsonProperty]
        public CircuitModel CurrentCircuit { get; private set; }

        //[JsonProperty]
        //public Guid CurrentID { get; set; }

        private string FilePath()
        {
            return Path.Join(ProjectDir, $"{ProjectName}{FileUtil.ProjectExt}");
        }

        [JsonConstructor]
        public ProjectState()
        {
            RootCircuit = new(RootCircuitName, setupIO: false);
            CurrentCircuit = RootCircuit;
            var projectPath = Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");
            Transmitters = Definitions.GetInstance().Transmitters;
            Rename(projectPath);
        }

        public ProjectState(bool populateDefaults)
        {
            RootCircuit = new(RootCircuitName, setupIO:populateDefaults);
            CurrentCircuit = RootCircuit;
            var projectPath = Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");
            Transmitters = Definitions.GetInstance().Transmitters;
            Rename(projectPath);
        }

        public ProjectState(string? projectPath = null)
        {
            RootCircuit = new(RootCircuitName, setupIO:true);
            CurrentCircuit = RootCircuit;
            projectPath ??= Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");
            Transmitters = Definitions.GetInstance().Transmitters;
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
                ret = new ProjectState(true);
            }
            else
            {
                ret.NavigateToCircuit(ret.CurrentCircuit.ID);
            }

            return ret;
        }

        public CircuitModel NavigateToCircuit(Guid id)
        {
            CircuitModel ret = RootCircuit;

            if (RootCircuit.ID != id) 
            {
                ret = RootCircuit.SearchSubCircuits(id) ?? RootCircuit;
                //CurrentID = ret.ID;
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
