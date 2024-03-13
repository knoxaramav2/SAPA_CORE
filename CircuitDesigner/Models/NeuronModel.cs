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
        public Guid ID { get; set; } = Guid.NewGuid();
        public NodeTypes Type { get; set; } = NodeTypes.NEURON;
        public NodeControl Host { get; set; } = host;
        List<INodeModel> INodeModel.Connections { get; set; } = [];

        [JsonProperty]
        public string Name { get; set; } = id ?? Guid.NewGuid().ToString();

        [JsonProperty]
        public List<Dendrite> Dendrites { get; set; } = [];

        [JsonProperty]
        public float Charge { get; set; } = 0.0f;
        [JsonProperty]
        public float Bias { get; set; } = 1.0f;

        public float Decay { get; set; } = 0.0f;

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
