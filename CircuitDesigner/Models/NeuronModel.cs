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
        public List<Dendrite> Dendrites { get; set; } = [];

        [JsonProperty]
        float Charge { get; set; }
        [JsonProperty]
        float Bias { get; set; }

        public bool Attach(INodeModel node)
        {
            if (((INodeModel)this).Attach(node))
            {
                var dendrite = new Dendrite
                {
                    Target = (NeuronModel)node,
                    Weight = 1.0f
                };
                Dendrites.Add(dendrite);
                return true;
            }
            return false;
        }

        public bool Detach(INodeModel node)
        {
            if (((INodeModel)this).Detach(node))
            {
                var dendrite = Dendrites.FirstOrDefault(x => x.Target == node);
                return Dendrites.Remove(dendrite);
            }

            return false;
        }
    }
}
