using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Events
{
    internal class InterformEvents
    {
        public delegate void BroadcastModel(object? sender, INodeModel model);
        public delegate void ZoomTo(object sender, Point model);
    }
}
