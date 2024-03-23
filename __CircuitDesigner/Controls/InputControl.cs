using CircuitDesigner.Models;
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
    public partial class InputControl : NodeControl
    {
        public InputControl(DesignBoard designer, string id) : base(designer)
        {
            Model = new InputModel(this, id);
        }

        public InputControl()
        {
            InitializeComponent();
        }
    }
}
