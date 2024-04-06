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
