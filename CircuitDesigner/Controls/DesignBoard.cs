using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Diagnostics.CodeAnalysis;

namespace CircuitDesigner.Controls
{
    internal partial class DesignBoard : UserControl
    {
        private CircuitModel RootCircuit;
        private List<DesignNode> Nodes;

        private int ZoomScale;
        private Point? DragOrigin = null;
        private bool IsDragging = false;
        private DesignNode? SelectedNode = null;

        #region Form Data
        public DesignBoard()
        {
            InitializeComponent();
            SetupEvents();
            var model = new CircuitModel("");
            ZoomScale = 0;
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
            Nodes = [];

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
                var node = new InputNode(this, input);
                pos = new Point(pos.X, pos.Y - node.Height / 2);
                Controls.Add(node);
                node.MoveTo(pos);
            }

            ResetSelections();
        }

        private void ResetSelections()
        {
            DragOrigin = null;
            IsDragging = false;
            SelectedNode = null;
            ZoomScale = 0;
        }
        #endregion

        #region Events

        private void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            Zoom(e.Delta);
        }

        private void DesignBoard_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region Helpers

        private void Drag(Point pos)
        {
            if (!IsDragging || DragOrigin == null) { return; }
            var origin = (Point)DragOrigin;
            var deltaPos = pos.Sub(origin);

            if(SelectedNode == null)
            {
                RootCircuit.Pos = RootCircuit.Pos.Add(deltaPos);

                foreach(DesignNode node in Controls)
                {
                    node.Location = node.Location.Add(deltaPos);
                }

                DragOrigin = pos;
            } else
            {
                SelectedNode.Location = SelectedNode.Location.Add(deltaPos);
            }
        }

        private void Zoom(int amount)
        {
            const int MIN_SCALE = -25;
            const int MAX_SCALE = 15;

            if (amount == 0) { return; }
            amount = Math.Clamp(amount, -1, 1);

            if (ZoomScale + amount < MIN_SCALE && ZoomScale + amount > MAX_SCALE)
            {
                return;
            }

            ZoomScale += amount;

            foreach (var node in Nodes)
            {
                SetNodeScale(node, amount);
            }
        }

        private static void SetNodeScale(DesignNode node, int amnt)
        {

        }

        #endregion


        public void DesignBoard_MouseMove(object sender, MouseEventArgs e)
        {
            Drag(e.Location);
        }

        public void DesignBoard_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }

        private void LinkNode(DesignNode node1, DesignNode node2)
        {

        }

        private void ReleaseSelection()
        {
            if (SelectedNode != null)
            {
                SelectedNode.BackColor = default;
            }

            DragOrigin = null;
            SelectedNode = null;
        }

        private void SetSelection(DesignNode? node, Point pos)
        {
            ReleaseSelection();

            DragOrigin = pos;

            if (node == null)
            {

            }
            else
            {
                node.BackColor = Color.LightGreen;
                SelectedNode = node;
            }
        }

        public void DesignBoard_MouseDown(object sender, MouseEventArgs e)
        {
            var obj = sender is DesignNode ? sender as DesignNode : null;

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (obj != null && SelectedNode != null && SelectedNode != obj)
                {
                    LinkNode(SelectedNode, obj);
                }
            }
            else
            {
                SetSelection(obj, e.Location);
                IsDragging = true;
            }
        }

        public void DesignBoard_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
