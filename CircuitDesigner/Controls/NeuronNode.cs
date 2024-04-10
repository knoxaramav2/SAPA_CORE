using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{


    internal partial class NeuronNode : DesignNode
    {
        public new NeuronModel Model { get; private set; }
        public new Guid ModelID { get { return Model.ID; } private set { Model.ID = value; } }
        public new Point Position { get { return Model.Pos; } private set { Model.Pos = Location = value; } }
        public new string ModelName { get { return Model.Name; } private set { Model.Name = value; } }

        public NeuronNode()
        {
            InitializeComponent();
            Model = new("---", IonState.DefaultInternalState());
        }

        public NeuronNode(DesignBoard board, NeuronModel model) : base(board, model)
        {
            InitializeComponent();
            BindModel(model);
            InitDrawing();
        }

        private void InitDrawing()
        {
            SelectColor = Color.Lime;
            UnselectColor = Color.DarkRed;
            BackColor = Color.LightCoral;
            SetSelectState(IsSelected);
        }

        [MemberNotNull(nameof(Model))]
        private void BindModel(NeuronModel model)
        {
            Model = model;
        }

        private void NeuronNode_Move(object sender, EventArgs e)
        {
            MoveTo(Location);
        }

        public override void MoveTo(Point pos)
        {
            Position = pos;
        }

        private void NeuronNode_Paint(object sender, PaintEventArgs e)
        {
            using SolidBrush brush = new(BackColor);
            using Pen pen = new(Outline, 4);
            var rect = new RectangleF(0, 0, Width, Height);
            e.Graphics.FillEllipse(brush, rect);
            e.Graphics.DrawEllipse(pen, rect);
        }
    }
}
