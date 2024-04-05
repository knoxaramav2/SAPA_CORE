using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal class OutputModel(string name, Point? pos=null)
        : ConnectorModel(name, pos)
    {
    }
}
