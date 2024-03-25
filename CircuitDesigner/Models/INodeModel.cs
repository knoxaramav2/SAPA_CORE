namespace CircuitDesigner.Models
{
    internal interface INodeModel
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public Point Pos { get; set; }
    }

}
