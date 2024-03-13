using CircuitDesigner.Controls;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    public class RegionModel(NodeControl host, string? id = null) : INodeModel
    {
        public NodeControl Host { get; set; } = host;
        public List<INodeModel> Connections { get; set; } = [];

        [JsonProperty]
        public string ID { get; set; } = id ?? Guid.NewGuid().ToString();
    }
}
