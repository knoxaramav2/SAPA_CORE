using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class CircuitModel : INodeModel
    {
        private const int DefaultInputSize = 4;
        //private const int DefaultOutputSize = 4;

        public string Name { get; set; }
        public Guid ID { get; set; }
        public Point Pos { get; set; }
        public double Scale { get; set; }

        public List<CircuitModel> SubCircuits { get; set; } = [];
        public List<InputModel> Inputs { get; set; } = [];

        public CircuitModel(string name, Point? pos=null) 
        {
            Name = name;
            ID = Guid.NewGuid();
            Pos = pos ?? new();

            for (var i = 0; i < DefaultInputSize; ++i)
            {
                var item = new InputModel($"Input {i + 1}", new Point(0, i * 5));

                Inputs.Add(item);
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

        public void SetScale(double scale)
        {
            Scale = scale;
        }
    }
}
