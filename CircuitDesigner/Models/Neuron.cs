using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    public struct Dendrite
    {
        public Neuron Target { get; set; }
        public float Weight { get; set; }
    }

    public class Neuron(string? id = null)
    {
        [JsonProperty]
        public string ID { get; set; } = id ?? Guid.NewGuid().ToString();

        [JsonProperty]
        public List<Dendrite> Connections { get; set; } = [];

        [JsonProperty]
        float Charge { get; set; }
        [JsonProperty]
        float Bias { get; set; }

    }
}
