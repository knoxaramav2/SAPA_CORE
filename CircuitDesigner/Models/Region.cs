using Newtonsoft.Json;

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
