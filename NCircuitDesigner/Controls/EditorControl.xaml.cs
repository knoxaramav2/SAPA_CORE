using NCircuitDesigner.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace NCircuitDesigner.Controls
{
    /// <summary>
    /// Interaction logic for EditorControl.xaml
    /// </summary>
    public partial class EditorControl : UserControl
    {
        public ObservableCollection<NeuronNode> CircuitControls { get; set; }
        private NeuronNode? Selected { get; set; }

        int _scale;
        Point _center;
        bool _drag;
        Point _drag_offset;

        public EditorControl()
        {
            InitializeComponent();
            CircuitControls = [];
            Selected = null;
            _scale = 1;
            _center = new Point(0, 0);
            _drag = false;
        }

        public void NewNeuron(Point pos, int scale)
        {
            Debug.WriteLine("SCLICK");
            NeuronNode neuronNode = new(this, pos, scale);

            EditorCanvas.Children.Add(neuronNode);
            CircuitControls.Add(neuronNode);
            TranslateLocal(neuronNode, pos, _center);
        }

        private void EditorCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseSelected();
            Point pos = Mouse.GetPosition(this);
            pos = new Point(pos.X+_center.X, pos.Y+_center.Y);
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                NewNeuron(pos, _scale);
            }
        }

        private void EditorCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var amount = e.Delta < 0 ? -1 : 1;
            _scale = Math.Clamp(_scale+amount, 0, 8);

            foreach(var ctrl in CircuitControls)
            {
                ctrl.Data.SetScale(amount);
            }
        }

        private void EditorCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_drag)
            {
                var pos = e.GetPosition(this);
                if (Selected == null)
                {
                    TranslateGlobal(pos);
                } else
                {
                    TranslateLocal(Selected, pos, _drag_offset);
                }
            }
        }

        private void TranslateLocal(NeuronNode local, Point dp, Point offset)
        {
            local.Data.X = (int)((dp.X - offset.X)+local.Width/2);
            local.Data.Y = (int)((dp.Y - offset.Y));

            if (local.RenderTransform is not TranslateTransform trans)
            {
                trans = new TranslateTransform();
                local.RenderTransform = trans;
            }

            trans.X = local.Data.X - local.Width/2;
            trans.Y = local.Data.Y + local.Height/2;
        }

        private void TranslateGlobal(Point mpos)
        {
            //Debug.WriteLine($"MOVE: {_drag}");
            
            foreach (var node in CircuitControls)
            {
                if (node.RenderTransform is not TranslateTransform trans)
                {
                    trans = new TranslateTransform();
                    node.RenderTransform = trans;
                }

                trans.X = (mpos.X - _drag_offset.X) + node.Data.X - node.Width/2;
                trans.Y = (mpos.Y - _drag_offset.Y) + node.Data.Y + node.Height/2;
            }

            var cb = CenterBlock;
            if (cb.RenderTransform is not TranslateTransform ctrans)
            {
                ctrans = new TranslateTransform();
                cb.RenderTransform = ctrans;
            }

            ctrans.X = (mpos.X - _drag_offset.X) + _center.X;
            ctrans.Y = (mpos.Y - _drag_offset.Y) + _center.Y;

            Debug.WriteLine($"OFFSET = {ctrans.X}, {ctrans.Y}");
        }

        private void ApplyTranslateGlobal(Point curr)
        {
            _center.X += (curr.X - _drag_offset.X);
            _center.Y += (curr.Y - _drag_offset.Y);

            foreach (var node in CircuitControls)
            {
                if (node.RenderTransform is not TranslateTransform trans)
                {
                    trans = new TranslateTransform();
                    node.RenderTransform = trans;
                }

                node.Data.X = (int)((curr.X - _drag_offset.X) + node.Data.X);
                node.Data.Y = (int)((curr.Y - _drag_offset.Y) + node.Data.Y);
            }

            var cb = CenterBlock;
            if (cb.RenderTransform is not TranslateTransform ctrans)
            {
                ctrans = new TranslateTransform();
                cb.RenderTransform = ctrans;
            }

            Debug.WriteLine($"APPLY");
        }

        private void EditorCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            _drag = false;
        }

        private void EditorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _drag = true;
            var dragCtrl = sender as UserControl;
            dragCtrl?.ReleaseMouseCapture();
            _drag_offset = Mouse.GetPosition(this);
            Debug.WriteLine("SET OFFSET");
        }

        private void EditorCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _drag = false;
            ApplyTranslateGlobal(e.GetPosition(this));
        }

        public void ApplySelected(NeuronNode node)
        {
            Debug.WriteLine("SELECT");
            Selected = node;
            _drag = true;
        }

        public void ReleaseSelected()
        {
            if (Selected == null) { return; }
            Debug.WriteLine("DE-SELECT");
            ApplyLocalTransform(Selected, Mouse.GetPosition(this));
            Selected = null;
            _drag = false;
        }

        public void ApplyLocalTransform(NeuronNode node, Point mpos)
        {
            if (Selected?.RenderTransform is not TranslateTransform trans)
            {
                trans = new TranslateTransform();
                node.RenderTransform = trans;
            }

            trans.X = (mpos.X - _drag_offset.X) + node.Data.X - node.Width / 2;
            trans.Y = (mpos.Y - _drag_offset.Y) + node.Data.Y + node.Height / 2;
        }
    }
}
