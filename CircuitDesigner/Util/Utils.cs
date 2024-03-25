using Newtonsoft.Json;

namespace CircuitDesigner.Util
{
    internal static class FileUtil
    {
        public static string BaseUri = AppDomain.CurrentDomain.BaseDirectory;
        public static string ProjectsUri = Path.Join(BaseUri, "Projects");
        public static string ProgramDataUri = Path.Join(BaseUri, "ProgramData");
        public static string LogUri = Path.Join(BaseUri, "Logs");


        public const string ProjectExt = ".prj";
        public const string InternalDataExt = ".dat";
        public const string CircuitExt = ".snc";
        public const string LogExt = ".log";

        public static void AssurePath(string path)
        {
            while (!string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                path = Path.GetDirectoryName(path) ?? string.Empty;
            }

            Directory.CreateDirectory(path);
        }

        public static void AssureDirectories()
        {
            AssurePath(ProjectsUri);
            AssurePath(ProgramDataUri);
            AssurePath(LogUri);
        }
    
        public static bool PathExists(string path)
        {
            if (!string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                path = Path.GetDirectoryName(path) ?? "";
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }
            }          

            return Directory.Exists(path);
        }

        public static bool Save<T>(string path, T data)
        {
            var sj = JsonConvert.SerializeObject(data, Formatting.Indented);
            return Save(path, [sj]);
        }

        public static bool Save(string path, string[] data)
        {
            using StreamWriter sw = new(path);
            foreach(var line in data) { sw.Write(line); }
            return true;
        }

        public static T? Load<T>(string path) where T : class
        {
            if (!File.Exists(path)) { return null; }

            var serializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                TypeNameHandling = TypeNameHandling.All,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            using StreamReader sr = new(path);
            var content = sr.ReadToEnd(); 
            var ret = JsonConvert.DeserializeObject<T>(content, serializerSettings);
            
            return ret;
        }
    }

    internal static class Utils
    {

    }
}
