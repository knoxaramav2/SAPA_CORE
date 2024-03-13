using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public interface INodeModel
    {
        internal string ID { get; set; }
        public NodeControl Host { get; protected set; }
        public List<INodeModel> Connections { get; protected set; }
        public bool Attach(INodeModel node)
        {
            if (Connections.Contains(node)) return false;
            Connections.Add(node);
            return true;
        }

        public bool Detach(INodeModel node)
        {
            return Connections.Remove(node);
        }
    }
}
