using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class OutputModel
        : ConnectorModel
    {
        [JsonConstructor]
        public OutputModel() { }
        public OutputModel(string name, Point? pos = null) : base(name, pos) { }
    }
}
