using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{
    public partial class RegionControl : NodeControl
    {
        public RegionControl(string? id = null) : base()
        {
        }

        public RegionControl(DesignBoard designer, string id) : base(designer)
        {
            InitializeComponent();
            Model = new RegionModel(this, id);
        }

        public bool AddNode(string id, NodeTypes type)
        {


            return true;
        }

        public bool RemoveNode(string id, NodeTypes type)
        {

            return true;
        }
    }
}
