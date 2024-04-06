using CircuitDesigner.Models;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //Extracted for testing
        public static string JsonSerialize<T>(T data)
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling=PreserveReferencesHandling.Objects };
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
    }

    internal static class GeomUtil
    {
        public static double Dist(Point p1, Point p2)
        {
            return Math.Sqrt(
                Math.Pow(p1.X - p2.X, 2) + 
                Math.Pow(p1.Y - p2.Y, 2)
                );
        }

        public static Point ADToCoord(double angle, double dist)
        {
            return new Point(
                (int)(Math.Cos(angle) * dist),
                (int)(Math.Sin(angle) * dist)
                );
        }

        public static double Angle(Point p1, Point p2 = new Point())
        {
            return Math.Atan2(p1.Y-p2.Y, p1.X-p2.X);
        }
    }

    internal static class AgnosticModelUtil
    {
        public static string AutoModelName<T>(string[] existing) where T : INodeModel
        {
            string rName = typeof(T) switch
            {
                Type a when a == typeof(InputModel) => "Input",
                Type a when a == typeof(OutputModel) => "Output",
                Type a when a == typeof(CircuitModel) => "Circuit",
                Type a when a == typeof(NeuronModel) => "Neuron",
                _ => throw new NotImplementedException(nameof(AutoModelName)),
            };
            string name = rName;

            uint i = 0;
            while (existing.Any(x => x.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                name = $"{rName} {i}";
                i++;
            }

            return name;
        }
    }
}
