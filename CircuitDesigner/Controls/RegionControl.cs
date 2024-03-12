using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{
    public partial class RegionControl : NodeControl
    {
        //public new RegionModel Model;

        public RegionControl(string? id = null) : base()
        {
            Parent = this;
            id ??= Guid.NewGuid().ToString();
            Model = new RegionModel(this, id);
        }

        public RegionControl(DesignBoard designer, string? id = null) : base(designer)
        {
            InitializeComponent();
            Model = new RegionModel(this, id);
        }
    }
}
