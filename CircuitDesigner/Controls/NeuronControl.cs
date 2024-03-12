using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{
    public partial class NeuronControl : NodeControl
    {
        //public new NeuronModel Model;

        public NeuronControl() : base()
        {
            Model = new NeuronModel(this, Guid.NewGuid().ToString());
        }

        public NeuronControl(DesignBoard designer, NeuronModel model) : base(designer)
        {
            InitializeComponent();
            //Host = (DesignBoard)Parent;
            Model = model;
        }
    }
}
