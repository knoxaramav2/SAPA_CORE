using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public class InputModel(NodeControl host, string id) : INodeModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public NodeTypes Type { get; set; } = NodeTypes.INPUT;
        public string Name { get; set; } = id;
        public NodeControl Host { get; set; } = host;
        public List<INodeModel> Connections { get; set; } = [];
    }
}
