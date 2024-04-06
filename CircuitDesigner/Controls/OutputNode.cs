using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{
    internal partial class OutputNode : DesignNode
    {
        public new OutputModel Model { get; private set; }
        public new Guid ModelID { get { return Model.ID; } private set { Model.ID = value; } }
        public new Point Position { get { return Model.Pos; } private set { Model.Pos = Location = value; } }
        public new string ModelName { get { return Model.Name; } private set { Model.Name = value; } }

        public OutputNode(DesignBoard board, OutputModel model) : base(board, model)
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
        private void BindModel(OutputModel model)
        {
            Model = model;
        }

        private void OutputNode_Load(object sender, EventArgs e)
        {

        }

        private void OutputNode_Paint(object sender, PaintEventArgs e)
        {
            using SolidBrush brush = new(BackColor);
            using SolidBrush pen = new(Outline);
            const int THICK = 15;

            Point[] shape = [
                new(Width, 0),
                new(0, Height/2),
                new(Width, Height)
            ];
            RectangleF circ = new(0, (Height / 2) - (THICK / 2), THICK, THICK);

            e.Graphics.FillPolygon(brush, shape);
            e.Graphics.FillEllipse(pen, circ);
        }

        private void OutputNode_Move(object sender, EventArgs e)
        {
            MoveTo(Location);
        }

        public override void MoveTo(Point pos)
        {
            Position = pos;
        }
    }
}
