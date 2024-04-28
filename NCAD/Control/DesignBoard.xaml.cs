using NCAD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
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

namespace NCAD.Control
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DesignBoard : UserControl
    {

        #region Setup
        public DesignBoard()
        {
            InitializeComponent();
            InitControls();
        }

        public void InitControls()
        {
            //var nrn = SpawnNeuron();
            //SetControl(nrn);
        }

        public void SetControl(IControlModel component)
        {
            switch (component)
            {
                case Circuit crc:
                    LoadCircuitProperties(crc);
                    LoadCircuitTools(crc);
                    break;
                case Neuron nrn:
                    LoadNeuronProperties(nrn);
                    LoadNeuronTools(nrn);
                    break;
                case Input input:
                    LoadInputProperties(input);
                    LoadInputProperties(input);
                    break;
                case Output output:
                    LoadOutputProperties(output);
                    LoadOutputTools(output);
                    break;
            }
        }

        #endregion

        #region control setup

        private INeuron SpawnNeuron()
        {
            return null;
        }

        private void LoadNeuronProperties(INeuron neruon)
        {

        }

        private void LoadNeuronTools(INeuron neruon)
        {

        }

        private void LoadCircuitProperties(ISubCircuit circuit)
        {

        }

        private void LoadCircuitTools(ISubCircuit circuit)
        {

        }

        private void LoadInputProperties(IInput input)
        {

        }

        private void LoadOutputTools(IInput input)
        {

        }

        private void LoadOutputProperties(IOutput output)
        {

        }

        private void LoadOutputTools(IOutput output)
        {

        }

        #endregion
    }
}
