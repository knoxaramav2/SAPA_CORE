using CircuitDesigner.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CircuitDesigner.Models
{
    public class OutputModel : INodeModel
    {
        #region Model definitions
        public OutputModel(NodeControl host, string name)
        {
            Host = host;
            Name = name;
        }

        public Guid ID { get; set; } = Guid.NewGuid();

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Host.NodeLabel.Text = _name;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public NodeControl Host { get; set; }
        public List<INodeModel> Connections { get; set; } = [];

        //Input connections
        public List<Dendrite> Dendrites { get; set; } = [];

        public void Invalidate()
        {
            foreach (var conn in Connections) { conn.Detach(this); }
            Connections.Clear();

            foreach (var ddr in Dendrites)
            {
                ddr.Target.Detach(this);
            }
            Dendrites.Clear();
        }

        public bool SetPreDendrite(INodeModel model)
        {
            var dendrite = new Dendrite
            {
                Target = (NeuronModel)model,
                Weight = 1.0f
            };

            if (Dendrites.Any(x => x.Target == model)) { return false; }
            Dendrites.Add(dendrite);
            return true;
        }

        public bool Detach(INodeModel node) 
        {
            if (Connections.Contains(node))
            {
                Connections.Remove(node);
                node.Detach(this);
            }
            else
            {
                var rem = Dendrites.FirstOrDefault(x => x.Target == node);
                Dendrites.Remove(rem);
            }

            return true;
        }
        #endregion

        #region INotifedPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

    }
}
