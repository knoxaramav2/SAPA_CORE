using Newtonsoft.Json;

namespace CircuitDesigner.Util
{
    internal static class FileIO
    {
        internal static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        internal static string ProjectPath = Path.Join(BasePath, "Projects");
        internal static string ProgramPath = Path.Join(BasePath, "Program");

        internal static void AssurePath(string path)
        {
            while (!string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                path = Path.GetDirectoryName(path) ?? string.Empty;
            }

            Directory.CreateDirectory(path);
        }

        internal static T? LoadAs<T>(string path)
        {
            if (!File.Exists(path)) { return default; }
            using StreamReader sr = new(path);
            var content = sr.ReadToEnd();
            sr.Close();
            var ret = JsonConvert.DeserializeObject<T>(content);
            return ret ?? default;
        }

        internal static bool SaveAs<T>(string path, T obj)
        {
            AssurePath(path);
            var sj = JsonConvert.SerializeObject(obj, Formatting.Indented);
            using StreamWriter wr = new(path);
            wr.WriteLine(sj);
            wr.Close();
            return true;
        }
    }

}

