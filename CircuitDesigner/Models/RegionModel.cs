using CircuitDesigner.Controls;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    public class RegionModel(NodeControl host, string? id = null) : INodeModel
    {
        private const ushort DefaultInputsSize = 8;
        private const ushort DefaultOutputSize = 8;

        public NodeControl Host { get; set; } = host;
        public List<INodeModel> Connections { get; set; } = [];

        public List<INodeModel?> Inputs { get; private set;} = Enumerable.Repeat<INodeModel?>(null, DefaultInputsSize).ToList();
        public List<INodeModel?> Outputs { get; private set; } = Enumerable.Repeat<INodeModel?>(null, DefaultOutputSize).ToList();

        [JsonProperty]
        public string ID { get; set; } = id ?? Guid.NewGuid().ToString();

        private void ResizeList(List<INodeModel?> list, ushort size)
        {
            if (list.Count == size) { return; }
            if (list.Count > size)
            {
                list = list.GetRange(0, size);
            }
            else
            {
                for (var i = list.Count; i < size; ++i) { list.Add(null); }
            }
        }

        public void ResizeInputs(ushort size)
        {
            ResizeList(Inputs, size);
        }

        public void ResizeOutputs(ushort size)
        {
            ResizeList(Outputs, size);
        }

        public bool SetInput(INodeModel input, ushort index)
        {
            if (index >= Inputs.Count) { return false; }
            Inputs[index] = input;
            return true;
        }

        public bool SetOutput(INodeModel output, ushort index)
        {
            if (index >= Outputs.Count) { return false; }
            Outputs[index] = output;
            return true;
        }
    }
}
