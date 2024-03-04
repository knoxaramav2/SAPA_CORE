using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public class Region(string? id = null)
    {
        [JsonProperty]
        public string ID { get; set; } = id ?? Guid.NewGuid().ToString();

        [JsonProperty]
        public List<Neuron> Neurons { get; set; } = [];
    }
}
