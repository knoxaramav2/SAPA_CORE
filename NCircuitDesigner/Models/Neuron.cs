using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCircuitDesigner.Models
{
    public class Neuron(string id, float bias=-10, float decay=.65f)
    {
        public string ID { get; set; } = id;
        public float Bias { get; set; } = bias;
        public float Decay { get; set; } = decay;
    }
}
