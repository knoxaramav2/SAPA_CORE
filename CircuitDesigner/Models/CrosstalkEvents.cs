using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    public delegate void NodeSelectEventHandler(object sender, INodeModel? node);
    public delegate void NodeCreatedEventHandler(object sender, INodeModel node);
    public delegate void NodeDeletedEventHandler(object sender, INodeModel node);

    public delegate void RegionEnterEventHandler(object sender, RegionModel node);
    public delegate void RegionExitEventHandler(object sender, RegionModel node);
}
