using CircuitDesigner.Models;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;

namespace CircuitDesigner.Controls
{
    internal partial class InputNode : DesignNode
    {
        InputModel Model;

        public Point Position
        {
            get
            {
                return Model.Pos;
            }

            set {
                Model.Pos = value;
                Location = value;
            }
        }

        public InputNode(DesignBoard board, InputModel model) : base(board, model.ID)
        {
            InitializeComponent();
            BindModel(model);
        }

        [MemberNotNull(nameof(Model))]
        private void BindModel(InputModel model)
        {
            Model = model;
        }

        private void InputNode_Load(object sender, EventArgs e)
        {

        }

        private void InputNode_Paint(object sender, PaintEventArgs e)
        {
            var dPath = new GraphicsPath();
            dPath.AddPolygon(new Point[]
            {
                new(0, 0),
                new(Width, Height/2),
                new(0, Height)
            });

            dPath.AddEllipse(new RectangleF(
                Width - 3, Height / 2, 3, 3
                ));

            e.Graphics.FillPath(Brushes.LightBlue, dPath);
        }

        private void InputNode_Resize(object sender, EventArgs e)
        {

        }

        private void InputNode_Move(object sender, EventArgs e)
        {
            MoveTo(Location);
        }

        public override void MoveTo(Point pos)
        {
            Position = pos;
        }
    }
}
