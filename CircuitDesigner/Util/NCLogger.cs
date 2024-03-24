using System.Diagnostics;

namespace CircuitDesigner.Util
{
    internal static class NCLogger
    {
        private static readonly List<string> LogStack = [];
        private const int MAX_LOG_SIZE = 50;

        public enum LogType { ERR, WRN, INFO }

        private static readonly string LogPath = Path.Join(FileUtil.LogUri, $"LOG_{DateTime.Now:dd/MM/yy}{FileUtil.LogExt}");

        public static void Log(string msg, LogType logType)
        {
            var currentTime = DateTime.Now.ToString("HH:mm:ss");
            var errTxt = Enum.GetName(logType);
            var text = $"{errTxt} {currentTime}:: {msg}";
            Debug.WriteLine(text);
            LogStack.Add(text);

            if (LogStack.Count >= MAX_LOG_SIZE) { Flush(); }
        }

        public static void Flush()
        {
            if (LogStack.Count == 0) { return; }

            using StreamWriter wr = new(LogPath, true);
            foreach(var line in LogStack)
            {
                wr.WriteLine(line);
            }

            LogStack.Clear();
        }
    }
}
