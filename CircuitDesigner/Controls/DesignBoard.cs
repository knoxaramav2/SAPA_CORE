using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircuitDesigner.Controls
{
    internal partial class DesignBoard : UserControl
    {
        private CircuitModel RootCircuit;
        private List<DesignNode> Nodes;

        #region Form Data
        public DesignBoard()
        {
            InitializeComponent();
            SetupEvents();
            var model = new CircuitModel("");
            LoadCircuit(model);
        }

        private void SetupEvents()
        {
            MouseWheel += OnMouseWheel;
        }

        private void ClearAll()
        {
            Controls.Clear();
        }

        [MemberNotNull(nameof(RootCircuit))]
        [MemberNotNull(nameof(Nodes))]
        public void LoadCircuit(CircuitModel model)
        {
            ClearAll();

            RootCircuit = model;
            Nodes = new();

            int num_inputs = RootCircuit.Inputs.Count;
            int input_dy = Height / (num_inputs + 1);

            int y_offset = input_dy;
            foreach (var input in RootCircuit.Inputs)
            {
                CreateControl(input, new Point(0, y_offset));
                y_offset += input_dy;
            }
        }

        private void CreateControl(INodeModel model, Point pos)
        {
            if (model is InputModel input) 
            {
                var node = new InputNode(input);
                pos = new Point(pos.X, pos.Y-node.Height/2);
                Controls.Add(node);
                node.MoveTo(pos);
            }
        }
        #endregion

        #region Events

        private void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            Zoom(e.Delta);
        }

        #endregion

        #region Helpers

        private void Zoom(int amount)
        {
            const int MIN_SCALE = -25;
            const int MAX_SCALE = 15;

            if (amount == 0) { return; }
            amount = Math.Clamp(amount, -1, 1);

            foreach (var node in Nodes)
            {
                SetNodeScale(node, amount);
            }
        }

        private static void SetNodeScale(DesignNode node, int amnt)
        {

        }

        #endregion

        private void DesignBoard_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
