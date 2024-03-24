using CircuitDesigner.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class Definitions
    {
        private static string __filepath = Path.Join(FileUtil.ProgramDataUri, $"definitions{FileUtil.InternalDataExt}");
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

        public void Save()
        {
            FileUtil.Save(__filepath, __instance);
        }
    }
}
