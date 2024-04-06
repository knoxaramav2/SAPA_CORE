using CircuitDesigner.Models;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace CircuitDesigner.Controls
{
    internal partial class DesignNode: UserControl, INodeModelWindow
    {
        readonly DesignBoard HostBoard;
        public INodeModel Model { get; }
        public Guid ModelID { get { return Model.ID; } }
        public string ModelName { get { return Model.Name; } }
        public Point Position
        {
            get { return Model.Pos; }

            set
            {
                Model.Pos = value;
                Location = value;
            }
        }
        protected Color Outline = default;
        protected Color SelectColor = default;
        protected Color UnselectColor = default;
        protected new Color BackColor = default;
        protected bool IsSelected = false;

        public DesignNode()
        {
            InitializeComponent();
        }

        public DesignNode(DesignBoard board, INodeModel model)
        {
            InitializeComponent();
            HostBoard = board;
            Model = model;
            InitDrawing();
        }

        private void InitDrawing()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.Opaque, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            DoubleBuffered = false;
        }

        public virtual void MoveTo(Point pos) { }

        public void SetSelectState(bool selected)
        {
            IsSelected = selected;
            Outline = selected ? SelectColor : UnselectColor;
        }

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

        protected override CreateParams CreateParams
        {
            get
            {
                var prms = base.CreateParams;
                prms.ExStyle = prms.ExStyle | 0x20;
                return prms;
            }
        }
        #endregion
    }

    internal interface INodeModelWindow
    {
        INodeModel Model { get; }
        string ModelName { get; }
        Guid ModelID { get; }
        Point Position { get; }
    }
}
