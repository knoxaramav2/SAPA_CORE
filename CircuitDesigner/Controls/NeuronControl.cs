using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{
    public partial class NeuronControl : NodeControl
    {
        public Models.Neuron Model { get; set; }

        public NeuronControl() : base()
        {
            Model = new(Guid.NewGuid().ToString());
        }

        public NeuronControl(DesignBoard designer, Models.Neuron model) : base(designer)
        {
            InitializeComponent();
            //Host = (DesignBoard)Parent;
            Model = model;
        }
    }
}
