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
        public InputControl(string id) : base()
        {
            Model = new InputModel(this, id);
        }

        public InputControl()
        {
            InitializeComponent();
        }
    }
}
