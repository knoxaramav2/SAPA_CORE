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
        string INodeModel.ID { get; set; } = id;
        NodeControl INodeModel.Host { get; set; } = host;
        List<INodeModel> INodeModel.Connections { get; set; } = [];
    }
}
