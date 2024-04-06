using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal interface IDendriteModel
    {
        public INodeModel Receiver { get; set; }
        public INodeModel Sender { get; set; }
        public Guid ID { get; set; }

        public float Weight { get; set; }
    }

    class DendriteModel : IDendriteModel
    {
        public INodeModel Receiver { get; set; }
        public INodeModel Sender { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
        public float Weight { get; set; } = 1.0f;

        public DendriteModel() { }

        public DendriteModel(INodeModel sender, INodeModel receiver)
        {
            Sender = sender;
            Receiver = receiver;
        }
    }
}
