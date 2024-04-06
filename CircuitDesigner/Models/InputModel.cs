using System.Text.Json.Serialization;

namespace CircuitDesigner.Models
{
    internal class InputModel 
        : ConnectorModel
    {
        [JsonConstructor]
        public InputModel() { }

        public InputModel(string name, Point? pos = null) : base(name, pos)
        {

        }
    }
}
