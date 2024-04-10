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
        public IonState Ions { get; set; }
        public float Threshold { get; set; } = -70.0f;
        //public float Decay { get; set; } = 0.75f;
        //public float Capacitance { get; set; } = 0.9f;
        public float Resistance { get; set; } = 200;

        public float RestingPotential { get; private set; } = 0.0f;

        [JsonConstructor]
        public NeuronModel() {
            Name = string.Empty;
            ID = Guid.Empty;
            Pos = new();
            Ions = IonState.DefaultInternalState();
            //RecalculateIonicState();
        }

        public NeuronModel(string name, IonState ionState, Point? pos = null)
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();
            //TODO replace with default transmitter setting
            Transmitters = Definitions.TransmittersListInst();
            if (Transmitters.Count > 0) { Transmitters[0].Item1 = true; }
            Ions = IonState.DefaultInternalState();
            RecalculateIonicState(ionState);
        }

        public void RecalculateIonicState(IonState ionState)
        {
            RestingPotential = CalcRestingPotential(ionState.Na, ionState.K, ionState.Ca, ionState.Cl);
        }

        public float CalcRestingPotential(
            Sodium na,
            Potassium k,
            Calcium ca,
            Chloride cl
            )
        {
            int parts = Ions.K.Parts + Ions.Na.Parts + Ions.Ca.Parts + Ions.Cl.Parts;
            float kq = Ion.ConcentrationCharge(Ion.IonType.POTASSIUM, Ions.K.Concentration, k.Concentration);
            float kp = ((float)Ions.K.Parts / parts);
            float naq = Ion.ConcentrationCharge(Ion.IonType.SODIUM, Ions.Na.Concentration, na.Concentration);
            float np = ((float)Ions.Na.Parts / parts);
            float clq = Ion.ConcentrationCharge(Ion.IonType.CHLORIDE, Ions.Cl.Concentration, cl.Concentration);
            float clp = ((float)Ions.Cl.Parts / parts);
            float caq = Ion.ConcentrationCharge(Ion.IonType.CALCIUM, Ions.Ca.Concentration, ca.Concentration);
            float cap = ((float)Ions.Ca.Parts / parts);

            float vk = kq * kp;
            float vna = naq * np;
            float vcl = clq * clp;
            float vca = caq * cap;

            return vk + vna + vcl + vca;
        }
    }
}
