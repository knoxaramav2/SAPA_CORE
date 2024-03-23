using System.Diagnostics;
using CircuitDesigner.Util;
using System.Drawing.Drawing2D;
using CircuitDesigner.Models;
using System.Diagnostics.Contracts;

namespace CircuitDesigner.Controls
{
    public partial class DesignBoard : UserControl
    {
        private ViewData? _view = null;
        public ViewData View
        {
            get
            {
                return _view ?? new ViewData();
            }
            set
            {
                _view = value;
            }
        }
        private bool IsDragging = false;
        private Point? DragOrigin = null;

        private int ScaleStep = 0;

        //Graphics states
        private Bitmap BMBuffer;
        private Graphics GBuffer;

        public event NodeSelectEventHandler? NodeSelected = null;
        public event NodeCreatedEventHandler? NodeCreated = null;
        public event NodeDeletedEventHandler? NodeDeleted = null;
        public event RegionEnterEventHandler? RegionEnter = null;
        public event RegionExitEventHandler? RegionExit = null;

        public DesignBoard()
        {
            InitializeComponent();
            MouseWheel += new MouseEventHandler(OnMouseWheel);
            HandleCreated += new EventHandler(Init);
            BMBuffer = new(1, 1);
            GBuffer = Graphics.FromImage(BMBuffer);
        }

        public void Init(object? sender, EventArgs e)
        {
            BMBuffer = new Bitmap(Width, Height);
            GBuffer = Graphics.FromImage(BMBuffer);
            GBuffer.Clear(Color.Transparent);
            DoubleBuffered = true;
        }

        //CTRL, ALT
        private static NodeTypes DetermineNewNodeType(bool secondary, bool altSecondary)
        {
            var ret = NodeTypes.NEURON;

            if (secondary && !altSecondary) { ret = NodeTypes.REGION; }
            else if (!secondary && altSecondary) { ret = NodeTypes.INPUT; }
            else if (secondary && altSecondary) { ret = NodeTypes.OUTPUT; }

            return ret;
        }

        private void DesignContainer_Click(object sender, MouseEventArgs e)
        {
            Focus();
            var shift = ModifierKeys.HasFlag(Keys.Shift);
            var ctrl = ModifierKeys.HasFlag(Keys.Control);
            var alt = ModifierKeys.HasFlag(Keys.Alt);

            if (shift)
            {
                var newType = DetermineNewNodeType(ctrl, alt);
                NewNode(newType, $"New {newType.NodeTypeName()}", false);
            }
            else
            {
                IsDragging = true;
            }
        }

        private void NewNode(NodeTypes type, string name, bool center)
        {
            var pos = center ?
                new Point(Width / 2, Height / 2) :
                PointToClient(MousePosition);

            NodeControl node;
            var container = View.Node as RegionControl;

            switch (type)
            {
                case NodeTypes.REGION:
                    node = new RegionControl(this, container, name);
                    ((RegionControl)node).EnterRegion += OnRegionEnter;
                    ((RegionControl)node).ExitRegion += OnRegionExit;
                    break;
                case NodeTypes.INPUT:
                    node = new InputControl(this, name); break;
                case NodeTypes.OUTPUT:
                    node = new OutputControl(this, name); break;
                case NodeTypes.NEURON:
                    node = new NeuronControl(this, name); break;
                default: throw new Exception($"Illegal node type: {type}");
            }

            SetNodeScale(node, ScaleStep);
            node.Location = new Point
            {
                X = pos.X - node.Width / 2,
                Y = pos.Y - node.Height / 2
            };

            node.DeleteNode += OnDeleteNode;

            DesignContainer.Controls.Add(node);
            View.AddControl(node);
            OnCreateNode(node);

            PaintNodes();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Debug.WriteLine($"KU: {e.KeyCode}");
            var shift = ModifierKeys.HasFlag(Keys.Shift);
            var ctrl = ModifierKeys.HasFlag(Keys.Control);
            var alt = ModifierKeys.HasFlag(Keys.Alt);

            switch (e.KeyCode)
            {
                case Keys.N:
                    if (shift)
                    {
                        var newType = DetermineNewNodeType(ctrl, alt);
                        NewNode(newType, $"New {newType.NodeTypeName()}", true);
                    }
                    break;
                case Keys.Delete:
                    if (View.Selected != null)
                    {
                        OnDeleteNode(this, View.Selected.Model);
                        View.UnselectControl();
                    }
                    break;
            }

            base.OnKeyUp(e);
        }

        public void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            Zoom(e.Delta > 0);
        }

        //State
        public void SwitchContext(ViewData data)
        {
            DesignContainer.Controls.Clear();
            View = data;
            DesignContainer.Controls.AddRange(data.Controls.ToArray());
        }

        public void DesignContainer_MouseMove(object sender, MouseEventArgs e)
        {
            Drag(e.Location);
        }

        private void LinkNodes(NodeControl n0, NodeControl n1)
        {
            Debug.WriteLine($"LINK {n0.Model?.Name} -> {n1.Model?.Name}");

            if (n0.Connections.Contains(n1) || n1.Connections.Contains(n0))
            {
                n0.DetachNode(n1);
                n1.DetachNode(n0);
            }
            else
            {
                n0.AttachNode(n1);
            }

            OnUpdateNode();
            PaintNodes();
        }

        private void Drag(Point pos)
        {
            if (!IsDragging || DragOrigin == null) { return; }
            var origin = (Point)DragOrigin;
            var deltaPos = pos.Sub(origin);

            if (View.Selected == null)
            {
                View.GlobalOrigin = View.GlobalOrigin.Add(deltaPos);

                foreach (NodeControl ctrl in View.Controls)
                {
                    ctrl.Location = ctrl.Location.Add(deltaPos);
                }

                DragOrigin = pos;

            }
            else
            {
                View.Selected.Location = View.Selected.Location.Add(deltaPos);
            }

            PaintNodes();
        }

        private static void SetNodeScale(NodeControl node, int steps)
        {
            const float STEP_COEF = 0.95f;
            var scale = (float)Math.Pow(1 / STEP_COEF, steps);
            node.Scale(new SizeF(scale, scale));
        }

        private void Zoom(bool zoomIn)
        {
            var steps = zoomIn ? 1 : -1;

            const int MIN_SCALE = -25;
            const int MAX_SCALE = 15;

            if (ScaleStep + steps < MIN_SCALE || ScaleStep + steps > MAX_SCALE) { return; }

            //Scale
            foreach (var node in View.Controls)
            {
                SetNodeScale(node, steps);
            }

            ScaleStep += steps;

            //Position

            PaintNodes();
        }

        private void SetSelection(NodeControl? control, Point pos)
        {
            DragOrigin = pos;

            if (control == null)
            {
                View.UnselectControl();
            }
            else
            {
                control.BackColor = Color.Red;
                View.SelectControl(control);
            }

            //OnUpdateNode();
        }

        private void ReleaseSelection()
        {
            if (View.Selected != null)
            {
                View.Selected.BackColor = Color.White;
            }

            DragOrigin = null;
            View.UnselectControl();
        }

        public void DesignContainer_MouseUp(object sender, MouseEventArgs e)
        {
            //ReleaseSelection();
            IsDragging = false;
        }

        public void DesignContainer_MouseDown(object sender, MouseEventArgs e)
        {
            var obj = sender is NodeControl ? sender as NodeControl : null;

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (obj != null && View.Selected != null && View.Selected != obj)
                {
                    LinkNodes(View.Selected, obj);
                }
            }
            else
            {
                ReleaseSelection();
                SetSelection(obj, e.Location);
                IsDragging = true;
            }
        }

        private void DesignContainer_MouseLeave(object sender, EventArgs e)
        {
            //ReleaseSelection();
            IsDragging = false;
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(BMBuffer, 0, 0);
            base.OnPaint(e);
        }

        private void DrawConnectionLines()
        {
            using var gradBrush = new LinearGradientBrush(new RectangleF(0, 0, 5, 5), Color.Red, Color.Yellow, 0f);
            using var pen = new Pen(gradBrush);
            var curve = .15f;

            GBuffer.Clear(Color.Transparent);

            foreach (var node in View.Controls)
            {
                var dests = node.Model?.Connections;
                if (dests == null) { continue; }

                var srcP = new Point
                {
                    X = node.Location.X + node.Width / 2,
                    Y = node.Location.Y + node.Height / 2
                };

                foreach (var dest in dests)
                {
                    var destP = new Point
                    {
                        X = dest.Host.Location.X + dest.Host.Width / 2,
                        Y = dest.Host.Location.Y + dest.Host.Height / 2
                    };
                    var dist = GeomHelper.Dist(srcP, destP) * curve;
                    var lp1 = new Point((int)(srcP.X + dist), (int)(srcP.Y - dist));
                    var lp2 = new Point((int)(destP.X - dist), (int)(destP.Y + dist));
                    GBuffer.DrawBezier(pen, srcP, lp1, lp2, destP);
                }
            }
        }

        private void PaintNodes()
        {
            DrawConnectionLines();
            Refresh();
        }

        //Events
        private void OnUpdateNode()
        {
            NodeSelected?.Invoke(this, View.Selected?.Model);
        }

        private void OnCreateNode(NodeControl node)
        {
            NodeCreated?.Invoke(this, node);
        }

        public void OnDeleteNode(object sender, INodeModel model)
        {
            var ctrl = View.Selected;
            NodeDeleted?.Invoke(this, model);
            DesignContainer.Controls.Remove(ctrl);
            ctrl?.Invalidate();
        }

        public void OnRegionEnter(object sender, RegionModel model)
        {
            RegionEnter?.Invoke(sender, model);
        }

        public void OnRegionExit(object sender, RegionModel model)
        {
            RegionExit?.Invoke(this, model);
        }

        private void DesignContainer_Click(object sender, EventArgs e)
        {
            Focus();
        }
    }
}
