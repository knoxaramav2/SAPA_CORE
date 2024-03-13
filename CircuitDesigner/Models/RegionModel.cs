﻿using CircuitDesigner.Controls;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    public class RegionModel : INodeModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public NodeTypes Type { get; set; } = NodeTypes.REGION;
        public NodeControl Host { get; set; }
        public List<INodeModel> Connections { get; set; } = [];

        public List<INodeModel> Neurons { get; set; } = [];
        public List<InputModel> Inputs { get; private set;} = [];
        public List<OutputModel> Outputs { get; private set; } = [];

        public RegionModel(NodeControl host, string id)
        {
            Name = id;
            Host = host;
        }

        [JsonProperty]
        public string Name { get; set; }

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
    }
}