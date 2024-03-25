using CircuitDesigner.Models;
using System.Diagnostics.CodeAnalysis;

namespace CircuitDesigner.Controls
{
    internal partial class DesignBoard : UserControl
    {
        private CircuitModel RootCircuit;
        private List<DesignNode> Nodes;

        private int ZoomScale;

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

            if(ZoomScale+amount < MIN_SCALE && ZoomScale+amount > MAX_SCALE)
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

        private void DesignBoard_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
