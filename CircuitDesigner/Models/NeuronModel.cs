using CircuitDesigner.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class NeuronModel : INodeModel
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public Point Pos { get; set; }


        public List<Pair<bool, Transmitter>> Transmitters { get; set; } = [];
        public IonState Ions;
        public float Bias { get; set; } = 1.0f;
        public float Decay { get; set; } = 0.75f;

        public float RestingPotential { get; private set; } = 0.0f;

        [JsonConstructor]
        public NeuronModel() {
            Name = string.Empty;
            ID = Guid.Empty;
            Pos = new();
            Ions = IonState.DefaultInternalState();
            RecalculateIonicState();
        }

        public NeuronModel(string name, Point? pos = null)
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();
            //TODO replace with default transmitter setting
            Transmitters = Definitions.TransmittersListInst();
            if (Transmitters.Count > 0) { Transmitters[0].Item1 = true; }
            Ions = IonState.DefaultInternalState();
            RecalculateIonicState();
        }

        public void RecalculateIonicState()
        {
            RestingPotential = CalcRestingPotential(Ions.Na, Ions.K, Ions.Ca, Ions.Cl);
        }

        private float CalcRestingPotential(
            Sodium na,
            Potassium k,
            Calcium ca,
            Chloride cl
            )
        {
            int parts = na.Parts + k.Parts + ca.Parts + cl.Parts;
            float kq = k.Concentraion * (k.Parts/parts);
            float naq = na.Concentraion * (na.Parts/parts);
            float clq = cl.Concentraion * (cl.Parts/parts);
            float caq = ca.Concentraion * (ca.Parts/parts);

            return kq + naq + clq + caq;
        }
    }
}
