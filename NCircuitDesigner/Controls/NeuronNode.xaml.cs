using NCircuitDesigner.Models;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using NCircuitDesigner.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using System.Diagnostics;

namespace NCircuitDesigner
{
    /// <summary>
    /// Interaction logic for NeuronNode.xaml
    /// </summary>
    public partial class NeuronNode : UserControl
    {
        public Neuron Data { get; set; }
        private Point _tOffset { get; set; }
        private Canvas Canvas { get; set; }

        public NeuronNode(Canvas canvas, Point pos, int scale = 1)
        {
            InitializeComponent();
            Data = new Neuron(Guid.NewGuid().ToString(), pos, scale);
            Canvas = canvas;
            DataContext = Data;
        }

        private void StackPanel_MouseLeave(object sender, 
            System.Windows.Input.MouseEventArgs e)
        {
            var parent = Parent as EditorControl;
            parent?.ReleaseSelected();
        }

        private void StackPanel_MouseUp(object sender, 
            System.Windows.Input.MouseButtonEventArgs e)
        {
            var parent = Parent as EditorControl;
            parent?.ReleaseSelected();
        }

        private void StackPanel_MouseMove(object sender, 
            System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void CircuitNode_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Debug.WriteLine("DOWN NODE");
            var parent = Parent as EditorControl;
            parent?.ApplySelected(this);
        }
    }
}
