using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class OutputModel(NodeControl host, string id) : INodeModel
    {
        string INodeModel.ID { get; set; } = id;
        NodeControl INodeModel.Host { get; set; } = host;
        List<INodeModel> INodeModel.Connections { get; set; } = [];
    }
}
