using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{
    public partial class NodeControl : UserControl
    {
        private readonly DesignBoard? Designer;
        public INodeModel? Model;
        public List<NodeControl> Connections { get; set; } = [];

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

        public void AttachNode(NodeControl node)
        {
            if (Connections.Contains(node) || node.Model == null) { return; }
            Connections.Add(node);
            Model?.Attach(node.Model);
        }

        public void DetachNode(NodeControl node)
        {
            if (!Connections.Remove(node) || node.Model == null) { return; }
            Model?.Detach(node.Model);
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
