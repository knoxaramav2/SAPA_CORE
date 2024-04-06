using CircuitDesigner.Models;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;

namespace CircuitDesigner.Controls
{
    internal partial class InputNode : DesignNode
    {
        public new InputModel Model { get; private set; }
        public new Guid ModelID { get { return Model.ID; } private set { Model.ID = value; } }
        public new Point Position { get { return Model.Pos; } private set { Model.Pos = Location = value; } }
        public new string ModelName { get { return Model.Name; } private set { Model.Name = value; } }

        public InputNode(DesignBoard board, InputModel model) : base(board, model)
        {
            InitializeComponent();
            BindModel(model);
            InitDrawing();
        }

        private void InitDrawing()
        {
            SelectColor = Color.Lime;
            UnselectColor = Color.SteelBlue;
            BackColor = Color.LightSteelBlue;
            SetSelectState(IsSelected);
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
            using SolidBrush brush = new(BackColor);
            using SolidBrush pen = new(Outline);
            const int THICK = 15;

            Point[] shape = [
                new(0,0),
                new(Width, Height/2),
                new(0, Height)
            ];
            RectangleF circ = new(Width-THICK, (Height/2)-(THICK/2), THICK, THICK);
            
            e.Graphics.FillPolygon(brush, shape);
            e.Graphics.FillEllipse(pen, circ);
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
