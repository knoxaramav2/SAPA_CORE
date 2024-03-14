using CircuitDesigner.Controls;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CircuitDesigner.Models
{
    public struct Dendrite
    {
        public NeuronModel Target { get; set; }
        public float Weight { get; set; }
    }

    public class NeuronModel : INodeModel
    {
        #region Model definitions
        public NeuronModel(NodeControl host, string name)
        {
            Host = host;
            Name = name;
        }
        public Guid ID { get; set; } = Guid.NewGuid();
        public NodeTypes Type { get; set; } = NodeTypes.NEURON;
        public NodeControl Host { get; set; }
        List<INodeModel> INodeModel.Connections { get; set; } = [];

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { 
                _name = value;
                Host.NodeLabel.Text = _name;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public List<Dendrite> Dendrites { get; set; } = [];

        private float _charge = 0.0f;
        public float Charge
        {
            get { return _charge; }
            set { _charge = value; NotifyPropertyChanged(nameof(Charge)); }
        }

        private float _bias = 1.0f;
        public float Bias
        {
            get { return _bias; }
            set { _bias = value; NotifyPropertyChanged(nameof(Bias)); }
        }

        private float _decay = 0.0f;
        public float Decay
        {
            get { return _decay; }
            set { _decay = value; NotifyPropertyChanged(nameof(Decay)); }
        }

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

        #endregion

        #region INotifedPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
