using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static CircuitDesigner.Events.InterformEvents;

namespace CircuitDesigner.Controls
{
    internal partial class DesignBoard : UserControl
    {
        private CircuitModel RootCircuit;
        private List<DesignNode> Nodes;

        private Point? DragOrigin = null;
        private bool IsDragging = false;
        private DesignNode? SelectedNode = null;

        private Bitmap BMBuffer;
        private Graphics GBuffer;

        #region Custom Events

        public event BroadcastModel BroadcastModel;

        #endregion

        #region Form Data
        public DesignBoard()
        {
            InitializeComponent();
            SetupEvents();
            LoadCircuit();
            InitGraphics();
        }

        [MemberNotNull([nameof(GBuffer), nameof(BMBuffer)])]
        private void InitGraphics()
        {
            BMBuffer = new(1, 1);
            GBuffer = Graphics.FromImage(BMBuffer);
        }

        private void SetupEvents()
        {
            MouseWheel += OnMouseWheel;
            HandleCreated += OnHandleCreated;
        }

        private void ClearAll()
        {
            Controls.Clear();
        }

        [MemberNotNull(nameof(RootCircuit))]
        [MemberNotNull(nameof(Nodes))]
        public void LoadCircuit(CircuitModel? model = null)
        {
            ClearAll();

            RootCircuit = model ?? new();
            Nodes = [];

            foreach (var input in RootCircuit.Inputs)
            {
                CreateControl(input);
            }
        }

        private void CreateControl(INodeModel model, Point? pos=null)
        {
            var loc = pos ?? model.Pos;

            DesignNode node;

            if (model is InputModel input)
            {
                node = new InputNode(this, input);
            } else
            {
                throw new NotImplementedException(nameof(CreateControl));
            }

            node.MoveTo(loc);
            Controls.Add(node);

            ResetSelections();
        }

        private void ResetSelections()
        {
            DragOrigin = null;
            IsDragging = false;
            SelectedNode = null;
        }
        #endregion

        #region Events

        private void OnHandleCreated(object? sender, EventArgs e)
        {
            BMBuffer = new Bitmap(Width, Height);
            GBuffer = Graphics.FromImage(BMBuffer);
            GBuffer.Clear(Color.Transparent);
            DoubleBuffered = true;
        }

        private void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            Zoom(e.Delta);
        }

        public void DesignBoard_MouseMove(object sender, MouseEventArgs e)
        {
            Drag(e.Location);
        }

        public void DesignBoard_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }

        private static void LinkNode(DesignNode node1, DesignNode node2)
        {

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

        private void DesignBoard_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(BMBuffer, 0, 0);
            //base.OnPaint(e);
        }
        #endregion

        #region Helpers
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

                var model = RootCircuit.SearchByID(node.ModelID);
                if (model != null)
                {
                    BroadcastModel?.Invoke(node, model);
                }
                
            }
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

            if (RootCircuit.Scale + amount < MIN_SCALE && RootCircuit.Scale + amount > MAX_SCALE) { return; }

            RootCircuit.SetScale(RootCircuit.Scale + amount);

            foreach (var node in Nodes)
            {
                SetNodeScale(node, amount);
            }

            Debug.WriteLine($"ZOOM {amount}");
        }

        private static void SetNodeScale(DesignNode node, int amnt)
        {

        }

        private void PaintNodes()
        {
            DrawConnectionLines();
            Refresh();
        }

        private void DrawConnectionLines()
        {

        }
        
        #endregion

        #region Public

        public void ZoomTo(Point pos)
        {
            IsDragging = true;

            DragOrigin = pos;
            Drag(new Point(Width/2, Height/2));
            
            IsDragging = false;
        }

        public void ZoomTo(Guid id)
        {
            var pos = RootCircuit.SearchByID(id)?.Pos;
            if(pos != null)
            {
                ZoomTo(pos.Value);
            }
        }

        public void UpdateControl(INodeModel model, Point? pos = null, bool delete = false)
        {
            Debug.WriteLine($"NAME={model.Name} POS={pos} | DEL={delete}");
            if (delete)
            {
                var node = Nodes.FirstOrDefault(x => x.ModelID == model.ID);
                if (node == null) { return; }
                Controls.Remove(node);
                Nodes.Remove(node);
            } else
            {
                if (model is InputModel input)
                {
                    pos ??= new Point(0, Height / 2);
                    var node = new InputNode(this, input)
                    {
                        Location = pos.Value
                    };
                    RootCircuit.Inputs.Add(input);
                    Nodes.Add(node);
                    Controls.Add(node);
                } else
                {
                    throw new NotImplementedException(nameof(UpdateControl));
                }
            }
        }

        #endregion

    }
}
