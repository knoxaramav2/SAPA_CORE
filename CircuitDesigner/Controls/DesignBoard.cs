using System.Diagnostics;
using CircuitDesigner.Util;
using System.Drawing.Drawing2D;
using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{

    public partial class DesignBoard : UserControl
    {
        private NodeControl? Selection = null;
        private bool IsDragging = false;
        private Point? DragOrigin = null;
        private Point GlobalOrigin;
        private readonly List<NodeControl> NodeControls = [];
        private int ScaleStep = 0;

        //Graphics states
        private Bitmap BMBuffer;
        private Graphics GBuffer;

        public delegate void NodeSelectEventHandler(object sender, INodeModel nodeControl);
        public event NodeSelectEventHandler? NeuronUpdated = null;
        public event NodeSelectEventHandler? RegionUpdated = null;

        public DesignBoard()
        {
            InitializeComponent();
            GlobalOrigin = new Point(0, 0);
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

        private void DesignContainer_Click(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (Selection == null)
                {
                    NewNode(e.Location);
                }
            }
            else
            {
                IsDragging = true;
            }
        }

        private void NewNode(Point? pos = null)
        {
            Debug.WriteLine("NEW NODE");

            pos ??= new Point(Size.Width / 2, Size.Height / 2);

            var regCtrl = new RegionControl(this)
            {
                Location = (Point)pos
            };

            SetNodeScale(regCtrl, ScaleStep);

            regCtrl.Location = new Point(
                regCtrl.Location.X - regCtrl.Width / 2,
                regCtrl.Location.Y - regCtrl.Height / 2
                );

            DesignContainer.Controls.Add(regCtrl);
            NodeControls.Add(regCtrl);

            PaintNodes();
        }

        private void DesignBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && ModifierKeys.HasFlag(Keys.Shift))
            {
                NewNode();
            }
        }

        //Queuery
        private NodeControl? FirstAt(Point pos)
        {
            var ret = NodeControls.FirstOrDefault(
                x => x.Bounds.Contains(pos));

            return ret;
        }

        private void OnUpdateNode()
        {
            if (Selection == null) { return; }
            if (Selection is RegionControl control) { OnUpdateRegion(control); }
            else if (Selection is NeuronControl control1) { OnUpdateNeuron(control1); }
        }

        private void OnUpdateRegion(RegionControl region)
        {
            RegionUpdated?.Invoke(this, region.Model);
        }

        private void OnUpdateNeuron(NeuronControl neuron)
        {
            NeuronUpdated?.Invoke(this, neuron.Model);
        }

        public void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            Zoom(e.Delta > 0);
        }

        //Control Comms

        public void DesignContainer_MouseMove(object sender, MouseEventArgs e)
        {
            Drag(e.Location);
        }

        private void LinkNodes(NodeControl n0, NodeControl n1)
        {
            Debug.WriteLine($"LINK {n0.Model?.ID} -> {n1.Model?.ID}");

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

            if (Selection == null)
            {
                GlobalOrigin = GlobalOrigin.Add(deltaPos);

                foreach (NodeControl ctrl in NodeControls)
                {
                    ctrl.Location = ctrl.Location.Add(deltaPos);
                }

                DragOrigin = pos;

            }
            else
            {
                Selection.Location = Selection.Location.Add(deltaPos);
            }

            PaintNodes();
        }

        private void SetNodeScale(NodeControl node, int steps)
        {
            //TODO Factor recursive scale results Fn(x0, s)=Fn(x0, Fn-1...)...
            const float STEP_COEF = 0.05f;
            var scale = 1 / (1 + (-steps * STEP_COEF));
            node.Scale(new SizeF(scale, scale));
            Debug.WriteLine($"SCALE={scale} SCALE_STEP={ScaleStep} STEPS={steps}");
        }

        private void Zoom(bool zoomIn)
        {
            var steps = zoomIn ? 1 : -1;

            const int MIN_SCALE = -25;
            const int MAX_SCALE = 15;

            if (ScaleStep + steps < MIN_SCALE || ScaleStep + steps > MAX_SCALE) { return; }

            //Scale
            foreach (var node in NodeControls)
            {
                SetNodeScale(node, steps);
            }

            ScaleStep += steps;
            Debug.WriteLine($"STEP: {ScaleStep}, {steps}");

            //Position

            PaintNodes();
        }

        private void SetSelection(NodeControl? control, Point pos)
        {
            if (control != null)
            {
                control.BackColor = Color.Red;
            }

            Selection = control;
            DragOrigin = pos;

            if (control is NeuronControl nrn)
            {
                OnUpdateNeuron(nrn);
            }
            else if (control is RegionControl reg)
            {
                OnUpdateRegion(reg);
            }

        }

        private void ReleaseSelection()
        {

            if (Selection != null)
            {
                Selection.BackColor = Color.White;
            }

            DragOrigin = null;
            Selection = null;
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
                if (obj != null && Selection != null && Selection != obj)
                {
                    LinkNodes(Selection, obj);
                }
            }
            else
            {
                if (Selection != null) { ReleaseSelection(); }
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

        private void PaintNodes()
        {
            var pen = new Pen(Color.Red, 3f);
            var curve = .15f;

            GBuffer.Clear(Color.Transparent);
            
            foreach(var node in NodeControls)
            {
                var dests = node.Model?.Connections;
                if (dests == null) { continue; }

                var srcP = new Point
                {
                    X = node.Location.X + node.Width / 2,
                    Y = node.Location.Y + node.Height / 2
                };

                foreach(var dest in dests)
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

            Refresh();
        }


        //Events


    }
}
