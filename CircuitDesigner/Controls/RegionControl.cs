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
    public partial class RegionControl : NodeControl
    {
        public Models.Region Model { get; set; }

        public RegionControl() : base()
        {
            Model = new();
        }

        public RegionControl(DesignBoard designer, Models.Region model) : base(designer)
        {
            InitializeComponent();
            Model = model;
        }
    }
}
