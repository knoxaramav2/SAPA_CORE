using System.Diagnostics;
using CircuitDesigner.Util;
using System.Drawing.Drawing2D;
using CircuitDesigner.Models;

namespace CircuitDesigner.Controls
{

    enum DesignViewMode
    {
        RegionView,
        NeuronView
    }

    class ViewData
    {
        public string ID { get; private set; }
        public DesignViewMode ViewMode { get; private set; }
        public List<NodeControl> Controls { get; } = [];
        public NodeControl? SelectedControl { get; private set; }

        public ViewData(string id, DesignViewMode viewMode)
        {
            ID = id;
            ViewMode = viewMode;
        }

        public bool AddControl(NodeControl node)
        {
            if (Controls.Contains(node)) { return false; }
            Controls.Add(node);
            return true;
        }

        public bool RemoveControl(NodeControl node)
        {
            return Controls.Remove(node);
        }
    }

    class ViewManager
    {
        private readonly Dictionary<string, ViewData> Views = [];
        public ViewData CurrentView { get; private set; }

        private const string ROOT_ID = "__ROOT__";

        public ViewManager()
        {
            CreateView(ROOT_ID, DesignViewMode.RegionView, true);
            if(CurrentView == null) { throw new Exception("Voodoo has occurred"); }
        }

        public ViewData? SwitchView(string id)
        {
            Views.TryGetValue(id, out ViewData? viewData);
            return viewData;
        }

        public bool CreateView(string id, DesignViewMode mode, bool switchTo=false)
        {
            if (string.IsNullOrEmpty(id) || Views.ContainsKey(id.ToUpper())) { return false; }

            var newView = new ViewData(id, mode);
            Views.Add(id.ToUpper(), newView);

            if (switchTo)
            {
                CurrentView = newView;
            }

            return true;
        }

        public bool DeleteView(string id)
        {
            return Views.Remove(id.ToUpper());
        }
    }

    public partial class DesignBoard : UserControl
    {
        private DesignViewMode ViewMode = DesignViewMode.RegionView;

        private NodeControl? Selection = null;
        private bool IsDragging = false;
        private Point? DragOrigin = null;
        private Point GlobalOrigin;
        private ViewManager ViewManager;
        
        private int ScaleStep = 0;

        //Graphics states
        private Bitmap BMBuffer;
        private Graphics GBuffer;

        public delegate void NodeSelectEventHandler(object sender, INodeModel nodeControl);
        public event NodeSelectEventHandler? NeuronUpdated = null;
        public event NodeSelectEventHandler? RegionUpdated = null;

        public delegate void FocusRegionViewEventHandler(object sender, RegionModel model);
        public event FocusRegionViewEventHandler? RegionViewExpanded = null;

        public delegate void FocusNeuronViewEventHandler(object sender, RegionModel model);

        public DesignBoard()
        {
            InitializeComponent();
            GlobalOrigin = new Point(0, 0);
            MouseWheel += new MouseEventHandler(OnMouseWheel);
            HandleCreated += new EventHandler(Init);
            BMBuffer = new(1, 1);
            GBuffer = Graphics.FromImage(BMBuffer);
            ViewManager = new();
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
                NewNode(false);
            }
            else
            {
                IsDragging = true;
            }
        }

        private void NewNode(bool center)
        {
            var pos = center ?
                new Point(Width / 2, Height / 2) :
                PointToClient(MousePosition);

            var regCtrl = new RegionControl(this);
            SetNodeScale(regCtrl, ScaleStep);
            regCtrl.Location = new Point
            {
                X = pos.X - regCtrl.Width/2,
                Y = pos.Y - regCtrl.Height/2
            };

            DesignContainer.Controls.Add(regCtrl);
            ViewManager.CurrentView.AddControl(regCtrl);

            PaintNodes();
        }

        private void DesignBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && ModifierKeys.HasFlag(Keys.Shift))
            {
                NewNode(true);
            }
        }

        public void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            Zoom(e.Delta > 0);
        }

        //State
        private void SwitchViewMode(DesignViewMode mode, string? id)
        {
            if (mode == DesignViewMode.RegionView)
            {

            } else if (mode == DesignViewMode.NeuronView)
            {

            }
            // Other modes?
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

                foreach (NodeControl ctrl in ViewManager.CurrentView.Controls)
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
            foreach (var node in ViewManager.CurrentView.Controls)
            {
                SetNodeScale(node, steps);
            }

            ScaleStep += steps;

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
            
            foreach(var node in ViewManager.CurrentView.Controls)
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


    }
}
