using System.Diagnostics;
using CircuitDesigner.Models;
using CircuitDesigner.Util;

namespace CircuitDesigner.Controls
{
    public partial class DesignBoard : UserControl
    {
        private NodeControl? Selection = null;
        private Point? DragOrigin = null;
        private Point GlobalOrigin;
        private readonly List<NodeControl> NodeControls = [];

        public DesignBoard()
        {
            InitializeComponent();
            GlobalOrigin = new Point(0, 0);
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
            Debug.WriteLine($"ORG: {origin}");

            if (Selection == null)
            {
                Debug.WriteLine($"MOVE GLBL: {deltaPos}");
                GlobalOrigin = GlobalOrigin.Add(deltaPos);

                foreach (NodeControl ctrl in NodeControls)
                {
                    ctrl.Location = ctrl.Location.Add(deltaPos);
                }

                DragOrigin = pos;

            } else
            {
                Debug.WriteLine($"MOVE LCAL: {deltaPos}");
                Selection.Location = Selection.Location.Add(deltaPos);
            }
        }

        private void SetSelection(NodeControl? control, Point pos)
        {
            Selection = control;
            DragOrigin = pos;
        }

        private void ReleaseSelection()
        {
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
