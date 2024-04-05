using CircuitDesigner.Controls;
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
        public List<CircuitModel> SubCircuits { get; private set; } = [];
        [JsonProperty]
        public List<InputModel> Inputs { get; private set; } = [];
        [JsonProperty]
        public List<OutputModel> Outputs { get; private set; } = [];
        [JsonProperty]
        public List<IDendriteModel> Dendrites { get; private set; } = [];
        [JsonConstructor]
        public CircuitModel()
        {
            Name = string.Empty;
            ID = Guid.Empty;
            Pos = new();
        }

        public CircuitModel(string name, Point? pos=null) 
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();
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
                (INodeModel?)Outputs.FirstOrDefault(x => x.ID == id);
        }


        #region Component methods

        public bool AddComponent(INodeModel model)
        {
            switch (model)
            {
                case InputModel input: Inputs.Add(input); break;
                case OutputModel output: Outputs.Add(output); break;
                case CircuitModel subcirc: SubCircuits.Add(subcirc); break;
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

        #endregion

    }
}
