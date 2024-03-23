using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{
    public partial class NeuronControl : NodeControl
    {
        public NeuronControl() : base()
        {
            
        }

        public NeuronControl(DesignBoard designer, string id) : base(designer)
        {
            InitializeComponent();
            Model = new NeuronModel(this, id);
        }
    }
}
