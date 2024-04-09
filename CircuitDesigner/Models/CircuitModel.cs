using CircuitDesigner.Controls;
using CircuitDesigner.Util;
using Newtonsoft.Json;

namespace CircuitDesigner.Models
{
    internal class CircuitModel : INodeModel
    {
        private const int DefaultInputSize = 4;
        private const int DefaultOutputSize = 4;

        public string Name { get; set; }
        public Guid ID { get; set; }
        public Point Pos { get; set; }
        public int Scale { get; private set; }

        [JsonProperty]
        public IonState Ions { get; private set; }
        [JsonProperty]
        public List<CircuitModel> SubCircuits { get; private set; } = [];
        [JsonProperty]
        public List<InputModel> Inputs { get; private set; } = [];
        [JsonProperty]
        public List<OutputModel> Outputs { get; private set; } = [];
        [JsonProperty]
        public List<NeuronModel> Neurons { get; private set; } = [];

        [JsonProperty(IsReference =true)]
        public List<DendriteModel> Dendrites { get; private set; } = [];
        [JsonConstructor]
        public CircuitModel()
        {
            Name = string.Empty;
            ID = Guid.Empty;
            Pos = new();
        }

        public CircuitModel(string name, bool setupIO=false)
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = new();
            if (setupIO)
            {
                InitIO(DefaultInputSize, DefaultOutputSize);
            }
            Ions = IonState.DefaultExternalState();
        }

        public CircuitModel(
            string name, Point? pos = null,
            uint inputNodes = 0, uint outputNodes = 0
            ) 
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();            
            InitIO(inputNodes, outputNodes);
        }

        private void InitIO(uint inputNodes = 0, uint outputNodes = 0)
        {
            for (var i = 0; i < inputNodes; ++i)
            {
                var modName = $"Input {i+1}";
                AddComponent(new InputModel(modName));
            }

            for (var i = 0; i < outputNodes; ++i)
            {
                var modName = $"Output {i + 1}";
                AddComponent(new OutputModel(modName));
            }
        }

        public int ComponentCount()
        {
            return
                SubCircuits.Count +
                Inputs.Count +
                Outputs.Count +
                Neurons.Count;
        }

        public int ComponentCountRec()
        {
            var ret = ComponentCount();
            foreach(var subcirc in SubCircuits)
            {
                ret += subcirc.ComponentCountRec();
            }

            return ret;
        }

        public int CountCircuits()
        {
            var ret = 1;

            foreach(var circ in SubCircuits)
            {
                ret += circ.CountCircuits();
            }

            return ret;
        }

        public void BasicStartup()
        {
            for (var i = 0; i < DefaultInputSize; ++i)
            {
                var item = new InputModel($"Input {i + 1}", new Point(0, i * 5));
                Inputs.Add(item);
            }

            for(var i = 0; i < DefaultOutputSize; ++i)
            {
                var item = new OutputModel($"Output {i+1}", new Point(0, i*5));
                Outputs.Add(item);
            }
        }

        public CircuitModel? SearchSubCircuits(Guid id)
        {
            if (ID == id) { return this; }

            CircuitModel? ret = null;

            foreach(var node in  SubCircuits)
            {
                ret = node.SearchSubCircuits(id);
                if (ret != null) { break; }
            }

            return ret;
        }

        public void SetScale(int scale)
        {
            Scale = scale;
        }
    
        public INodeModel? SearchByID(Guid id)
        {
            return
                (INodeModel?)SubCircuits.FirstOrDefault(x => x.ID == id) ??
                (INodeModel?)Inputs.FirstOrDefault(x => x.ID == id) ??
                (INodeModel?)Outputs.FirstOrDefault(x => x.ID == id) ??
                (INodeModel?)Neurons.FirstOrDefault(x => x.ID == id);
        }


        #region Component methods

        public bool AddComponent(INodeModel model)
        {
            switch (model)
            {
                case InputModel input: Inputs.Add(input); break;
                case OutputModel output: Outputs.Add(output); break;
                case CircuitModel subcirc: SubCircuits.Add(subcirc); break;
                case NeuronModel neuron: Neurons.Add(neuron); break;
                default: throw new NotImplementedException();
            }

            return true;
        }

        public bool RemoveComponent(INodeModel model)
        {
            return true;
        }

        public bool AddConnection(Guid senderID, Guid receiverID)
        {
            var sender = SearchByID(senderID);
            var receiver = SearchByID(receiverID);

            if (sender == null || receiver == null)
            {
                throw new Exception("One or connectors not found:" +
                    $"S={senderID} " +
                    $"R={receiverID}");
            }

            //Prevent duplicates and n0 loop
            if (Dendrites.Any(x => x.Sender == sender && x.Receiver == receiver) ||
                Dendrites.Any(x => x.Sender == receiver && x.Receiver == sender))
            {
                return false;
            }

            var dendrite = new DendriteModel(sender, receiver);
            Dendrites.Add(dendrite);

            return true;
        }

        public bool RemoveConnection(Guid id1, Guid id2)
        {
            var conn = Dendrites.FirstOrDefault(x => x.Sender.ID == id1 && x.Receiver.ID == id2) ??
                Dendrites.FirstOrDefault(x => x.Sender.ID == id2 && x.Receiver.ID == id1);

            if (conn == null) { return false; }
            Dendrites.Remove(conn);

            return true;
        }

        public IEnumerable<IDendriteModel> ListConnections(Guid id)
        {
            var model = SearchByID(id);
            var dendrites = Dendrites.Where(x => x.Sender.ID == id)
                .Concat(Dendrites.Where(x => x.Receiver.ID == id));
            return dendrites;
        }
        #endregion

    }
}
