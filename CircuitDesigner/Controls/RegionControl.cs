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
