﻿using CircuitDesigner.Controls;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CircuitDesigner.Models
{
    public class RegionModel : INodeModel
    {
        public RegionModel(NodeControl host, string name)
        {
            Host = host;
            Name = name;
        }

        #region Model Definitions
        public Guid ID { get; set; } = Guid.NewGuid();
        public NodeControl Host { get; set; }
        public List<INodeModel> Connections { get; set; } = [];

        public List<INodeModel> Neurons { get; set; } = [];
        public List<InputModel> Inputs { get; private set;} = [];
        public List<OutputModel> Outputs { get; private set; } = [];

        private string _name = string.Empty;
        public string Name { 
            get { return _name; }
            set { 
                _name = value;
                Host.NodeLabel.Text = _name;
                NotifyPropertyChanged(nameof(Name)); } 
        }

        public bool AddNode(INodeModel model)
        {
            if (model is NeuronModel neuron)
            {
                if (Neurons.Any(x => x.Name.Equals(neuron.Name, StringComparison.OrdinalIgnoreCase))) { return false; }
                Neurons.Add(neuron);
            } else if (model is InputModel input)
            {
                if (Inputs.Any(x => x.Name.Equals(input.Name, StringComparison.OrdinalIgnoreCase))) { return false; }
                Inputs.Add(input);
            } else if (model is  OutputModel output)
            {
                if (Outputs.Any(x => x.Name.Equals(output.Name, StringComparison.OrdinalIgnoreCase))) { return false; }
                Outputs.Add(output);
            } else
            {
                return false;
            }

            return true;
        }

        public bool RemoveNode(INodeModel model)
        {
            if (model is NeuronModel neuron) { return Neurons.Remove(neuron); }
            else if (model is InputModel input) { return Inputs.Remove(input); }
            else if (model is OutputModel output) { return Outputs.Remove(output); }
            else { return false; }
        }

        public void Invalidate()
        {
            foreach (var conn in Connections) { conn.Detach(this); }
            Connections.Clear();

            foreach(var conn in Neurons) { conn.Detach(this); }
            Neurons.Clear();

            foreach (var conn in Inputs) { conn.Detach(this); }
            Inputs.Clear();

            foreach (var conn in Outputs) { conn.Detach(this); }
            Outputs.Clear();

        }

        #endregion

        #region INotifedPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
