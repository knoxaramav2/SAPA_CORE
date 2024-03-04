using NCircuitDesigner.Models;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using NCircuitDesigner.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using System.Diagnostics;
using System.ComponentModel.Design;

namespace NCircuitDesigner
{
    /// <summary>
    /// Interaction logic for NeuronNode.xaml
    /// </summary>
    public partial class NeuronNode : UserControl
    {
        public string ID { get; set; }
        public Neuron Data { get; set; }
        private Point _tOffset { get; set; }
        private EditorControl Designer { get; set; }

        public NeuronNode(EditorControl designer, Point pos, int scale = 1)
        {
            InitializeComponent();
            ID = Guid.NewGuid().ToString();
            Data = new Neuron(Guid.NewGuid().ToString(), pos, scale);
            Designer = designer;
            DataContext = Data;
        }

        private void StackPanel_MouseLeave(object sender, 
            System.Windows.Input.MouseEventArgs e)
        {
            Designer?.ReleaseSelected();
        }

        private void StackPanel_MouseUp(object sender, 
            System.Windows.Input.MouseButtonEventArgs e)
        {
            Designer?.ReleaseSelected();
        }

        private void StackPanel_MouseMove(object sender, 
            System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void CircuitNode_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Debug.WriteLine("DOWN NODE");
            Designer?.ApplySelected(this);
        }
    }
}
