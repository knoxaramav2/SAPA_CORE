using CircuitDesigner.Controls;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Permissions;
using System.Xml.Linq;

namespace CircuitDesigner.Models
{
    public struct Dendrite
    {
        public readonly string Name
        {
            get
            {
                return Target.Name;
            }

            set
            {
                Target.Name = value;
            }
        }

        public readonly Guid ID
        {
            get { return Target.ID; }
            set { Target.ID = value; }
        }

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
        public NodeControl Host { get; set; }
        public List<INodeModel> Connections { get; set; } = [];

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

        //Input connections
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
            if (Connections.Contains(node)) { return false; }
            Connections.Add(node);
            
            if (node is NeuronModel neuron)
            {
                neuron.SetPreDendrite(this);
            }

            return true;            
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
            } else
            {
                var rem = Dendrites.FirstOrDefault(x => x.Target == node);
                Dendrites.Remove(rem);
            }
            
            return true;
        }

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

        #endregion

        #region INotifedPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
