using CircuitDesigner.Util;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    internal class ProjectState(string filePath)
    {
        [JsonIgnore]
        public string Name { get; set; } = Path.GetFileNameWithoutExtension(filePath);
        [JsonIgnore]
        public string FilePath { get; set; } = Path.GetDirectoryName(filePath) ?? FileIO.ProjectPath;
        [JsonIgnore]
        public string FullPath { get { return Path.Combine(FilePath, Name); } }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime LastSaveDate { get; set; }

        public uint NodeCounter { get; set; } = 0;

        public void Save()
        {
            var path = Path.Combine(FilePath, $"{Name}.scd");
            LastSaveDate = DateTime.Now;
            FileIO.SaveAs(path, this);
        }

        public static ProjectState? FromFile(string path)
        {
            return FileIO.LoadAs<ProjectState>(path);
        }
    }
}
