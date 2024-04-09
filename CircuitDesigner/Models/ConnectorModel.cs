using System.Text.Json.Serialization;

namespace CircuitDesigner.Models
{
    internal class ConnectorModel : INodeModel
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public Point Pos { get; set; }
        public bool Enabled { get; set; }
        public float Decay { get; set; } = 0.0f;

        //public List<INodeModel> Connections { get; set; }

        [JsonConstructor]
        public ConnectorModel() { }

        public ConnectorModel(string name, Point? pos = null)
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();

            //Connections = [];
            Enabled = true;
        }

        //public bool InsertConnection(INodeModel model)
        //{
        //    if (Connections.FirstOrDefault(x => x.ID == model.ID) != null) { return false; }
        //    Connections.Add(model);
        //    return true;
        //}

        //public bool RemoveConnection(INodeModel model)
        //{
        //    return Connections.Remove(model);
        //}
    }
}
