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
        OutputModel Model;

        public Point Position
        {
            get
            {
                return Model.Pos;
            }

            set
            {
                Model.Pos = value;
                Location = value;
            }
        }

        public OutputNode()
        {
            InitializeComponent();
            Model = new("---");
        }

        public OutputNode(DesignBoard board, OutputModel model) : base(board, model.ID)
        {
            InitializeComponent();
            BindModel(model);
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
            var dPath = new GraphicsPath();
            dPath.AddPolygon(new Point[]
            {
                new(Width, 0),
                new(0, Height/2),
                new(Width, Height)
            });

            dPath.AddEllipse(new RectangleF(
                -3, Height / 2, 3, 3
                ));

            e.Graphics.FillPath(Brushes.LightBlue, dPath);
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
