using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Drawing2D;
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

        public event BroadcastModel? BroadcastModel;

        #endregion

        #region Form Data
        public DesignBoard()
        {
            InitializeComponent();
            SetupEvents();
            InitGraphics();
            LoadCircuit();
        }

        [MemberNotNull([nameof(GBuffer), nameof(BMBuffer)])]
        private void InitGraphics()
        {
            SetDrawBuffer();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
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
        public void LoadCircuit(CircuitModel? model = null, bool asNew=false)
        {
            ClearAll();

            RootCircuit = model ?? new("New Circuit", setupIO: true);
            Nodes = [];

            foreach (var input in RootCircuit.Inputs)
            {
                CreateControl(input);
            }

            foreach (var output in RootCircuit.Outputs)
            {
                CreateControl(output);
            }

            foreach (var circuit in RootCircuit.SubCircuits)
            {
                CreateControl(circuit);
            }

            foreach(var neuron in RootCircuit.Neurons)
            {
                CreateControl(neuron);
            }

            //ZoomTo(RootCircuit.Pos, 0);


            UpdateDrawing();
        }

        private DesignNode? CreateControl(INodeModel model, Point? pos = null)
        {
            var loc = pos ?? model.Pos;

            DesignNode node;

            if (model is InputModel input)
            {
                node = new InputNode(this, input);
            }
            else if (model is OutputModel output)
            {
                node = new OutputNode(this, output);
            } else if (model is CircuitModel circuit)
            {
                node = new CircuitNode(this, circuit);
            } else if (model is NeuronModel neuron)
            {
                node = new NeuronNode(this, neuron);
            } else
            {
                throw new NotImplementedException(nameof(CreateControl));
            }

            node.MoveTo(loc);
            AddNode(node);
            ResetSelections();

            UpdateDrawing();

            return node;
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
            SetDrawBuffer();
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

            if (ModifierKeys.HasFlag(Keys.Shift) && SelectedNode == null)
            {
                var ctrl = ModifierKeys.HasFlag(Keys.Control);
                var alt = ModifierKeys.HasFlag(Keys.Alt);

                INodeModel? model = null;

                if (!ctrl && !alt)//Neuron
                {
                    model = new NeuronModel(
                        AgnosticModelUtil.AutoModelName<NeuronModel>(RootCircuit.Neurons.Select
                        (x => x.Name).ToArray()), RootCircuit.Ions);
                } else if (ctrl && !alt)
                {
                    model = new CircuitModel(
                        AgnosticModelUtil.AutoModelName<CircuitModel>(RootCircuit.SubCircuits.Select
                        (x => x.Name).ToArray()), setupIO: true);
                }
                else if (!ctrl && alt)
                {
                    model = new InputModel(
                        AgnosticModelUtil.AutoModelName<InputModel>(RootCircuit.SubCircuits.Select
                        (x => x.Name).ToArray()));
                }
                else if (ctrl && alt)
                {
                    model = new OutputModel(
                        AgnosticModelUtil.AutoModelName<OutputModel>(RootCircuit.SubCircuits.Select
                        (x => x.Name).ToArray()));
                }

                if (model != null)
                {
                    UpdateControl(model, e.Location);
                }
            }
        }

        private void LinkNode(DesignNode node1, DesignNode node2)
        {
            if (node1 is OutputNode || node2 is InputNode) { return; }

            Debug.WriteLine($"LINK {node1.ModelID} -> {node2.ModelID}");
            if (!RootCircuit.AddConnection(node1.ModelID, node2.ModelID))
            {
                RootCircuit.RemoveConnection(node1.ModelID, node2.ModelID);
            }

            UpdateDrawing();
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
            switch (e.KeyCode)
            {
                case Keys.Delete: DeleteNode(SelectedNode); break;
            }
        }

        private void DesignBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(BMBuffer, 0, 0);
            //base.OnPaint(e);
        }

        private void DesignBoard_Resize(object sender, EventArgs e)
        {
            SetDrawBuffer();
            UpdateDrawing();
        }

        #endregion

        #region Drawing

        [MemberNotNull([nameof(GBuffer), nameof(BMBuffer)])]
        private void SetDrawBuffer()
        {
            BMBuffer = new Bitmap(Width, Height);
            GBuffer = Graphics.FromImage(BMBuffer);
            GBuffer.Clear(Color.Transparent);
            DoubleBuffered = true;
        }

        private void UpdateDrawing()
        {
            DrawConnectionLines();
            Refresh();
        }

        private void DrawConnectionLines()
        {
            var dendrites = RootCircuit.Dendrites;

            using var gradBrush = new LinearGradientBrush(new RectangleF(0, 0, 5, 5),
                Color.LightCyan, Color.Magenta, 0f);
            using var pen = new Pen(gradBrush);

            const float CURVE = 0.15f;

            GBuffer.Clear(Color.Transparent);

            foreach (var dendrite in dendrites)
            {
                var src = Nodes.FirstOrDefault(x => x.ModelID == dendrite.Sender.ID);
                var dst = Nodes.FirstOrDefault(x => x.ModelID == dendrite.Receiver.ID);

                if (src == null || dst == null) { continue; }

                var srcP = new Point
                {
                    X = src.Location.X + src.Width / 2,
                    Y = src.Location.Y + src.Height / 2
                };

                var dstP = new Point
                {
                    X = dst.Location.X + dst.Width / 2,
                    Y = dst.Location.Y + dst.Height / 2
                };

                var dist = GeomUtil.Dist(srcP, dstP) * CURVE;
                var lp1 = new Point((int)(srcP.X + dist), (int)(srcP.Y - dist));
                var lp2 = new Point((int)(dstP.X - dist), (int)(dstP.Y + dist));
                GBuffer.DrawBezier(pen, srcP, lp1, lp2, dstP);
            }
        }

        #endregion

        #region Helpers

        private void AddNode(DesignNode node)
        {
            Nodes.Add(node);
            Controls.Add(node);
        }

        private void RemoveNode(DesignNode node)
        {
            Nodes.Remove(node);
            Controls.Remove(node);
        }

        private void SetSelection(DesignNode? node, Point pos)
        {
            ReleaseSelection();

            DragOrigin = pos;

            if (node != null)
            {
                node.SetSelectState(true);
                SelectedNode = node;

                var model = RootCircuit.SearchByID(node.ModelID);
                if (model != null)
                {
                    BroadcastModel?.Invoke(node, model);
                }
            } else
            {
                BroadcastModel?.Invoke(null, RootCircuit);
            }

            UpdateDrawing();
        }

        private void ReleaseSelection()
        {
            SelectedNode?.SetSelectState(false);
            DragOrigin = null;
            SelectedNode = null;

            UpdateDrawing();
        }

        private void Drag(Point pos)
        {
            if (!IsDragging || DragOrigin == null) { return; }
            var origin = (Point)DragOrigin;
            var deltaPos = pos.Sub(origin);

            if (SelectedNode == null)
            {
                RootCircuit.Pos = RootCircuit.Pos.Add(deltaPos);

                foreach (DesignNode node in Controls)
                {
                    node.Location = node.Location.Add(deltaPos);
                }

                DragOrigin = pos;
            }
            else
            {
                SelectedNode.Location = SelectedNode.Location.Add(deltaPos);
            }

            UpdateDrawing();
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

            UpdateDrawing();
            //Debug.WriteLine($"ZOOM {amount}");
        }

        private static void SetNodeScale(DesignNode node, int amnt)
        {
            const float STEP_COEF = 0.95f;
            var scale = (float)Math.Pow(1/STEP_COEF, amnt);
            node.Scale(new SizeF(scale, scale));
        }

        private void DeleteNode(DesignNode? node)
        {
            if (node == null) { return; }
            var connections = RootCircuit.ListConnections(node.ModelID);

        }

        #endregion

        #region Public

        public void ZoomTo(Point pos, int zoom=0)
        {
            IsDragging = true;

            DragOrigin = pos;
            Drag(new Point(Width / 2, Height / 2));

            IsDragging = false;

            Zoom(zoom);
        }

        public void ZoomTo(Guid id)
        {
            var pos = RootCircuit.SearchByID(id)?.Pos;
            if (pos != null)
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
                RemoveNode(node);
            }
            else
            {
                DesignNode? node = null;

                if (model is InputModel input)
                {
                    pos ??= new Point(0, Height / 2);
                    node = new InputNode(this, input);
                }
                else if (model is OutputModel output)
                {
                    node = new OutputNode(this, output);
                    pos ??= new Point(Width - node.Width, Height / 2);
                    node.Location = pos.Value;
                } else if (model is NeuronModel neuron)
                {
                    node = new NeuronNode(this, neuron);
                    pos = pos == null ?
                        new Point((Width / 2) - (node.Width / 2), (Height / 2) - (node.Height / 2)) :
                        new Point(pos.Value.X - (node.Width / 2), pos.Value.Y - (node.Height / 2));
                } else if (model is CircuitModel circuit)
                {
                    node = new CircuitNode(this, circuit);
                    pos = pos == null ?
                        new Point((Width / 2) - (node.Width / 2), (Height / 2) - (node.Height / 2)) :
                        new Point(pos.Value.X - (node.Width / 2), pos.Value.Y - (node.Height / 2));
                }
                else
                {
                    throw new NotImplementedException(nameof(UpdateControl));
                }
            
                if (node != null)
                {
                    node.Location = pos.Value;
                    RootCircuit.AddComponent(model);
                    AddNode(node);
                }
            }
        }

        public void RepositionComponents()
        {
            var inputs = RootCircuit.Inputs;
            var outputs = RootCircuit.Outputs;

            var numIn = RootCircuit.Inputs.Count;
            var numOut = RootCircuit.Outputs.Count;

            var inNodes = Nodes.Where(x => inputs.Any(y => y.ID == x.ModelID)).ToList();
            var outNodes = Nodes.Where(x => outputs.Any(y => y.ID == x.ModelID)).ToList();

            var inHeight = inNodes.FirstOrDefault()?.Height ?? 0;
            var outHeight = outNodes.FirstOrDefault()?.Height ?? 0;
            var outWidth = outNodes.FirstOrDefault()?.Width ?? 0;

            var diY = (Height-inHeight/2) / (numIn+1);
            var doY = (Height-outHeight/2) / (numOut+1);

            inNodes.Sort((x, y) => x.ModelName.CompareTo(y.ModelName));
            outNodes.Sort((x, y) => x.ModelName.CompareTo(y.ModelName));

            for(var i = 0; i < inNodes.Count; ++i)
            {
                var node = inNodes[i];
                if (node == null) { continue; }
                node.Position = new Point(0, diY*(i+1)-(inHeight/2));
            }

            for (var i = 0; i < outNodes.Count; ++i)
            {
                var node = outNodes[i];
                if (node == null) { continue; }
                node.Position = new Point(Width-outWidth, doY * (i + 1) - (outHeight / 2));
            }

            UpdateDrawing();
        }

        public void RecalculateNeuron(CircuitModel circuit)
        {
            if(SelectedNode is not NeuronNode neuron) { return; }
            var model = neuron.Model;
            model.RecalculateIonicState(circuit.Ions);
            Debug.WriteLine($"REST={model.RestingPotential}");
            BroadcastModel?.Invoke(this, model);
        }
        #endregion
    }
}
