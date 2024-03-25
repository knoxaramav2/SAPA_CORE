using CircuitDesigner.Util;

namespace CircuitDesigner.Models
{
    internal class Definitions
    {
        private static readonly string __filepath = Path.Join(FileUtil.ProgramDataUri, $"definitions{FileUtil.InternalDataExt}");
        private static Definitions? __instance = null;           
        
        public List<Transmitter> Transmitters { get; private set; }

        private Definitions()
        {
            Transmitters = [];
        }
        
        public static Definitions GetInstance()
        {
            __instance ??= Load();
            return __instance;
        }

        private static Definitions Load()
        {
            var defs = FileUtil.Load<Definitions>(__filepath);
            return defs ?? new();
        }

        public static void Save()
        {
            FileUtil.Save(__filepath, __instance);
        }
    }
}
