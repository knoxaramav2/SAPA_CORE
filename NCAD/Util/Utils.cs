using Newtonsoft.Json;
using System.IO;

namespace NCAD.Util
{
    internal static class FileUtil
    {
        public static string BaseUri = AppDomain.CurrentDomain.BaseDirectory;
        public static string ProjectsUri = Path.Join(BaseUri, "Projects");
        public static string ProgramDataUri = Path.Join(BaseUri, "ProgramData");
        public static string BuildUri = Path.Join(BaseUri, "Build");
        public static string LogUri = Path.Join(BaseUri, "Logs");
        public static string ModelUri = Path.Join(BaseUri, "Models");

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
            AssurePath(BuildUri);
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

        //Extracted for testing
        public static string JsonSerialize<T>(T data)
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        }

        //Extracted for testing
        public static T? JsonDeSerialize<T>(string content)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                TypeNameHandling = TypeNameHandling.All,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            return JsonConvert.DeserializeObject<T>(content, serializerSettings);
        }

        public static bool Save<T>(string path, T data)
        {
            return Save(path, [JsonSerialize(data)]);
        }

        public static bool Save(string path, string data) { return Save(path, [data]); }

        public static bool Save(string path, string[] data)
        {
            using StreamWriter sw = new(path);
            foreach (var line in data) { sw.Write(line); }
            return true;
        }

        public static T? Load<T>(string path) where T : class
        {
            if (!File.Exists(path)) { return null; }

            using StreamReader sr = new(path);
            var content = sr.ReadToEnd();
            var ret = JsonDeSerialize<T>(content);

            return ret;
        }
    
        public static string[] ListFiles(string dir, string pattern)
        {
            if (!Path.Exists(dir)) { return []; }
            return Directory.GetFiles(dir, pattern);
        }
    }
}
