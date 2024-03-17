using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public interface INodeModel : INotifyPropertyChanged
    {
        public NodeTypes Type { get; protected set; }
        public Guid ID { get; internal set; }
        public string Name { get; set; }
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
