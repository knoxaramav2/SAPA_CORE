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
        public float Bias { get; set; } = 1.0f;
        public float Decay { get; set; } = 0.75f;

        [JsonConstructor]
        public NeuronModel() {
            Name = string.Empty;
            ID = Guid.Empty;
            Pos = new();
        }

        public NeuronModel(string name, Point? pos = null)
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();
            //TODO replace with default transmitter setting
            Transmitters = Definitions.TransmittersListInst();
            if (Transmitters.Count > 0) { Transmitters[0].Item1 = true; }
        }

    }
}
