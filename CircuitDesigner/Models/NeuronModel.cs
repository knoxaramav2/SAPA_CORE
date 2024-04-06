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


        public List<Transmitter> Transmitters { get; set; } = [];
        public float Bias { get; set; } = 0.0f;
        public float Decay { get; set; } = 0.95f;

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
        }

    }
}
