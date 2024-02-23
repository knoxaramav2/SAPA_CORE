using System.Windows;

namespace NCircuitDesigner.Models
{
    public class Neuron(string id, Point loc, int scale, 
        float bias = -10, float decay = .65f)
        : NControl(loc, new Point(scale*BaseScale, scale * BaseScale))
    {
        private const int BaseScale = 75;
        public string ID { get; set; } = id;
        public float Bias { get; set; } = bias;
        public float Decay { get; set; } = decay;
        private readonly List<NControl> connections = [];

        public void AddConnection(NControl control)
        {
            connections.Add(control);
        }

        public void RemoveConnection(NControl control) {
            connections.Remove(control);
        }
    }
}
