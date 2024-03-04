using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{
    public partial class NodeControl : UserControl
    {
        private readonly DesignBoard? Designer;

        public NodeControl()
        {
            InitializeComponent();
            Designer = null;
        }

        public NodeControl(DesignBoard designer)
        {
            InitializeComponent();
            Designer = designer;
        }

        private void NodeLabel_MouseUp(object sender, MouseEventArgs e)
        {
            Designer?.DesignContainer_MouseUp(this, e);
        }

        private void NodeLabel_MouseMove(object sender, MouseEventArgs e)
        {
            Designer?.DesignContainer_MouseMove(this, e);
        }

        private void NodeLabel_MouseDown(object sender, MouseEventArgs e)
        {
            Designer?.DesignContainer_MouseDown(this, e);
        }
    }
}
