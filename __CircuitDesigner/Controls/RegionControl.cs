using CircuitDesigner.Models;
using System.Diagnostics;
using System.Windows.Forms.Design;

namespace CircuitDesigner.Controls
{
    public partial class RegionControl : NodeControl
    {
        public RegionControl() : base()
        {
        }

        public RegionControl(DesignBoard designer, RegionControl? parent, string name) : base(designer)
        {
            InitializeComponent();
            Model = new RegionModel(this, name);
            ParentRegion = parent;
        }

        public event RegionEnterEventHandler? EnterRegion = null;
        private void NodeLabel_DoubleClick(object sender, EventArgs e)
        {
            EnterRegion?.Invoke(this, (RegionModel)Model);
        }

        public event RegionExitEventHandler? ExitRegion = null;
        private void NodeLabel_Esc(object sender, EventArgs e)
        {
            ExitRegion?.Invoke(this, (RegionModel)Model);
        }
    }
}
