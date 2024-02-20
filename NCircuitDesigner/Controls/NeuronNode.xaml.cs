using NCircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NCircuitDesigner
{
    /// <summary>
    /// Interaction logic for NeuronNode.xaml
    /// </summary>
    public partial class NeuronNode : UserControl
    {
        public Neuron Data;
        public NeuronNode()
        {
            InitializeComponent();
            Data = new Neuron(Guid.NewGuid().ToString());
        }
    }
}
