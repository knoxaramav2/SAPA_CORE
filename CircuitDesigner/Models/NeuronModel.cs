using CircuitDesigner.Controls;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    public struct Dendrite
    {
        public NeuronModel Target { get; set; }
        public float Weight { get; set; }
    }

    public class NeuronModel(NodeControl host, string? id = null) : INodeModel
    {
        public NodeControl Host { get; set; } = host;
        List<INodeModel> INodeModel.Connections { get; set; } = [];

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
