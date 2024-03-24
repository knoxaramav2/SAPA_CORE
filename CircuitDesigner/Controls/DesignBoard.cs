using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{
    internal partial class DesignBoard : UserControl
    {
        private CircuitModel CircuitModel;

        public DesignBoard()
        {
            InitializeComponent();
            var model = new CircuitModel("");
            LoadCircuit(model);
        }

        [MemberNotNull(nameof(CircuitModel))]
        public void LoadCircuit(CircuitModel model)
        {
            CircuitModel = model;
        }
    }
}
