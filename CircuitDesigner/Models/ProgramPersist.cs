using CircuitDesigner.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class ProgramSettings
    {
        public string RootPath { get; set; }

        public ProgramSettings()
        {
            RootPath = FileUtil.ProgramDataUri;
        }
    }

    internal class ProgramPersist
    {
        private static readonly string FilePath =
            Path.Join(FileUtil.ProgramDataUri, "Persist"+FileUtil.InternalDataExt);

        [JsonProperty]
        protected List<(string, string)> Recents { get; private set; } = [];

        public DateTime LastSave { get; set; }

        public ProgramSettings Settings { get; set; }

        public ProgramPersist()
        {
            LastSave = DateTime.Now;
            Settings = new();
        }

        public List<(string, string)> GetRecents()
        {
            Recents = Recents.Where(x => File.Exists(x.Item1)).ToList();
            return Recents;
        }

        public void SetRecent(string projectName, string projectPath)
        {
            var filePath = Path.Join(projectPath, $"{projectName}{FileUtil.ProjectExt}");

            var exists = Recents.Any(x => 
                x.Item1.Equals(filePath, StringComparison.OrdinalIgnoreCase));

            (string, string) item;

            if (exists) {
                item = Recents.First(x => x.Item1.Equals(filePath, StringComparison.OrdinalIgnoreCase));
                Recents.Remove(item); 
            }
            else { item = (filePath, projectName); }

            Recents.Insert(0, item);
        }

        public static ProgramPersist Load()
        {
            var ret = FileUtil.Load<ProgramPersist>(FilePath);

            if (ret == null)
            {
                ret = new();
                ret.Save();
            }

            return  ret;
        }

        public void Save()
        {
            LastSave = DateTime.Now;
            FileUtil.Save(FilePath ,this);
        }
    }
}
