namespace CircuitDesigner.Controls
{
    internal partial class DesignNode : UserControl
    {
        readonly DesignBoard HostBoard;
        public readonly Guid ModelID;

        public DesignNode(DesignBoard board, Guid modelID)
        {
            InitializeComponent();
            HostBoard = board;
            ModelID = modelID;
        }

        public virtual void MoveTo(Point pos) { }

        #region Events
        private void DesignNode_MouseDown(object sender, MouseEventArgs e)
        {
            HostBoard.DesignBoard_MouseDown(this, e);
        }

        private void DesignNode_MouseMove(object sender, MouseEventArgs e)
        {
            HostBoard.DesignBoard_MouseMove(this, e);
        }

        private void DesignNode_MouseUp(object sender, MouseEventArgs e)
        {
            HostBoard.DesignBoard_MouseUp(this, e);
        }

        private void DesignNode_KeyUp(object sender, KeyEventArgs e)
        {
            HostBoard.DesignBoard_KeyUp(this, e);
        }
        #endregion
    }
}
