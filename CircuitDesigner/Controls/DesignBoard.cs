using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using CircuitDesigner.Util;

namespace CircuitDesigner.Controls
{

    public partial class DesignBoard : UserControl
    {
        private NodeControl? Selection = null;
        private Point? DragOrigin = null;
        private Point GlobalOrigin;
        private readonly List<NodeControl> NodeControls = [];
        private int ScaleStep = 0;

        public DesignBoard()
        {
            InitializeComponent();
            GlobalOrigin = new Point(0, 0);
            MouseWheel += new MouseEventHandler(OnMouseWheel);
        }

        private void DesignContainer_Click(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                NewNode(e.Location);
            }
            else
            {

            }
        }

        private void NewNode(Point? pos = null)
        {
            Debug.WriteLine("NEW NODE");

            pos ??= new Point(Size.Width / 2, Size.Height / 2);

            var name = "TEST";
            var region = new Models.Region(name);
            var regCtrl = new RegionControl(this, region)
            {
                Location = (Point)pos
            };
            regCtrl.Location = new Point(
                regCtrl.Location.X - regCtrl.Width / 2,
                regCtrl.Location.Y - regCtrl.Height / 2
                );

            SetNodeScale(regCtrl, ScaleStep);

            DesignContainer.Controls.Add(regCtrl);
            NodeControls.Add(regCtrl);
        
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

        private void SelectRegion(RegionControl region)
        {

        }

        private void SelectNeuron(NeuronControl neuron)
        {

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

        private void Drag(Point pos)
        {
            if (DragOrigin == null) { return; }
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
        }

        private void SetNodeScale(NodeControl node, int steps)
        {
            //TODO Factor recursive scale results Fn(x0, s)=Fn(x0, Fn-1...)...
            const float STEP_COEF = 0.05f;
            var scale = 1 / (1+(-steps*STEP_COEF));
            node.Scale(new SizeF(scale, scale));
            Debug.WriteLine($"SCALE={scale} SCALE_STEP={ScaleStep} STEPS={steps}");
        }

        private void Zoom(bool zoomIn)
        {
            var steps = zoomIn ? 1 : -1;

            const int MIN_SCALE = -15;
            const int MAX_SCALE = 10;

            if (ScaleStep + steps < MIN_SCALE || ScaleStep + steps > MAX_SCALE) { return; }

            //Scale
            foreach(var node in NodeControls)
            {
                SetNodeScale(node, steps);
            }

            ScaleStep += steps;
            Debug.WriteLine($"STEP: {ScaleStep}, {steps}");

            //Position
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
                SelectNeuron(nrn);
            }
            else if (control is RegionControl reg)
            {
                SelectRegion(reg);
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
            ReleaseSelection();
        }

        public void DesignContainer_MouseDown(object sender, MouseEventArgs e)
        {
            var obj = sender is NodeControl ? sender as NodeControl : null;
            SetSelection(obj, e.Location);
        }

        private void DesignContainer_MouseLeave(object sender, EventArgs e)
        {
            ReleaseSelection();
        }
    }
}
