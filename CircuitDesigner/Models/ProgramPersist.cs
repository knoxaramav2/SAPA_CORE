using CircuitDesigner.Util;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    internal class ProgramPersist
    {
        [JsonProperty]
        public DateTime LastSaveDate { get; set; }

        [JsonProperty]
        public List<(string, DateTime)> Saves { get; set; } = [];

        [JsonProperty]
        public DesignMode Mode { get; set; }

        public ProgramPersist()
        {
            LastSaveDate = DateTime.Now;
            Mode = DesignMode.RegionMode;
        }

        public void UpdateProjectInfo(ProjectState projectState)
        {
            var previous = Saves.FirstOrDefault(x => x.Item1.Equals(
                projectState.FullPath,
                StringComparison.OrdinalIgnoreCase));
            
            if (previous != default)
            {
                Saves.Remove(previous);
            }

            Saves.Insert(0, (projectState.FullPath, DateTime.Now));
        }

        public void Save()
        {
            var path = Path.Combine(FileIO.ProgramPath, "persist.json");
            FileIO.SaveAs(path, this);
        }

        public static ProgramPersist? FromFile()
        {
            var path = Path.Combine(FileIO.ProgramPath, "persist.json");
            return FileIO.LoadAs<ProgramPersist>(path);
        }
    }
}
