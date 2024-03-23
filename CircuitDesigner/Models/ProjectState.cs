using CircuitDesigner.Util;

namespace CircuitDesigner.Models
{
    internal class ProjectState
    {
        private const string DefaultProjectName = "__default__";


        public string ProjectName { get; private set; } = "";
        public string ProjectDir { get; private set; } = "";


        private string FilePath()
        {
            return Path.Join(ProjectDir, $"{ProjectName}{FileUtil.ProjectExt}");
        }

        public ProjectState(string? projectPath = null)
        {
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
            if (path == null)
            {
                path = Path.Join(FileUtil.ProjectsUri, $"{DefaultProjectName}{FileUtil.ProjectExt}");
                var ret = FileUtil.Load<ProjectState>(path);

                if (ret == null)
                {
                    NCLogger.Log($"Unable to load project at {path}", NCLogger.LogType.WRN);
                    ret = new ProjectState();
                }

                return ret;
            }
            else
            {
                return new ProjectState(path);
            }
        }

        public void Save()
        {
            if (IsDefaultProject() || string.IsNullOrEmpty(ProjectName)) { return; }
            FileUtil.Save(FilePath(), this);
        }
    }
}
