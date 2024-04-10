using CircuitDesigner.Controls;
using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CircuitDesigner.BuildEngine
{
    internal class CircuitEngine : ICircuitEngine
    {
        private ToolStripProgressBar? Progress;

        readonly ProjectState Project;

        readonly Dictionary<Guid, (int, INodeModel)> AllNodes = [];
        readonly List<InputModel> SysInputs = [];
        readonly List<OutputModel> SysOutputs = [];
        readonly Dictionary<Guid, List<INodeModel>> SubInputs = [];
        readonly Dictionary<Guid, List<INodeModel>> SubOutputs = [];
        readonly List<IDendriteModel> Dendrites = [];
        readonly Dictionary<Guid, (int, int, CircuitModel)> Circuits = [];
        readonly Dictionary<Guid, (int, int, NeuronModel, CircuitModel)> Neurons = [];
        readonly Dictionary<(int, float, int, float, int, float, int, float), (int, IonState)> Ions = [];
        private List<CircuitError> Errors = [];

        public CircuitEngine(ProjectState project, ToolStripProgressBar? progress)
        {
            Project = project;
            if(progress != null)
            {
                Progress = progress;
                Progress.Minimum = 0;
                Progress.Maximum = Project.RootCircuit.ComponentCountRec();
                Progress.Step = 1;
                Progress.ForeColor = default;
            }
        }

        #region interface
        public string? Build()
        {
            Analyse();
            ResolveCircuit(Project.RootCircuit);
            if (Errors.Count > 0) { return null; }
            return Compose();
        }

        public CircuitError[] Verify()
        {
            Analyse();
            ResolveCircuit(Project.RootCircuit);

            return [];
        }
        #endregion

        #region private

        private string Compose()
        {
            return
$@"#PROJECT={Project.ProjectName}
#COMPONENTS={Project.RootCircuit.ComponentCountRec()}
#INPUTS={Project.RootCircuit.Inputs.Count}
#OUTPUTS={Project.RootCircuit.Outputs.Count}
#CIRCUITS={Project.RootCircuit.CountCircuits()}
#IONS={Ions.Count}

{ComposeInputs()}
{ComposeOutputs()}
{ComposeIons()}
{ComposeCircuits()}
{ComposeNeurons()}
{Compose(Project.RootCircuit)}
";
        }

        private string Compose(CircuitModel circuit)
        {
            Dictionary<Guid, int> idxDict = [];
            string ret = "NETWORK\n";

            foreach (var ddr in Dendrites)
            {
                var ln = AllNodes[ddr.Sender.ID];
                var rn = AllNodes[ddr.Receiver.ID];
                ret += $"{ln.Item1}<{ddr.Weight}>{rn.Item1};";
            }

            return ret;
        }

        private string ComposeCircuits()
        {
            string ret = $"TABLE:CIRCUIT\n";

            for(var i = 0; i < Circuits.Count; ++i)
            {
                var data = Circuits.ElementAt(i);
                var idx = data.Value.Item1;
                var iidx = data.Value.Item2;
                var circ = data.Value.Item3;
                ret += $"{idx},{iidx},{circ.Name}\n";
            }

            return ret;
        }

        private string ComposeIons()
        {
            string ret = "$TABLE:IONS\n";

            foreach(var ion in Ions.Values)
            {
                var iid = ion.Item1;
                var dat = ion.Item2;
                ret += $"{iid}," +
                    $"{dat.Na.Parts},{dat.Na.Concentration}," +
                    $"{dat.K.Parts},{dat.K.Concentration}," +
                    $"{dat.Ca.Parts},{dat.Ca.Concentration}," +
                    $"{dat.Cl.Parts},{dat.Cl.Concentration}\n";
            }

            return ret;
        }

        private string ComposeNeurons()
        {
            string ret = "$TABLE:NEURONS\n";
            for (var i = 0; i < Neurons.Count; ++i)
            {
                var neuron = Neurons.ElementAt(i).Value;
                var idx = neuron.Item1;
                var iid = neuron.Item2;
                var data = neuron.Item3;
                var circuit = neuron.Item4;
                Circuits.TryGetValue(circuit.ID, out var ctple);
                var ciid = ctple.Item2;
                UInt64 ntb = 0;
                if (data.Transmitters.Count >= 64)
                {
                    LogError($"Neuron({idx}) {data.Name} transmitter list out of range. ({data.Transmitters.Count}/64)", CircuitErrorCode.OutOfSpec);
                }
                for (var j = 0; j < data.Transmitters.Count() && j < 64; ++j)
                {
                    ntb |= (uint)(data.Transmitters[j].Item1?1:0)<<j;
                }
                
                data.RecalculateIonicState(circuit.Ions);
                //Index, Name, charge, threshold, resistance, resting potential, transmitters, refactory
                ret += $"{idx},{iid},{ciid},{data.Name},0,{data.Threshold},{data.Resistance},{data.RestingPotential},{ntb},False\n";
            }

            return ret;
        }

        private string ComposeInputs()
        {
            string ret = "$TABLE:INPUTS\n";

            for (var i = 0; i < SysInputs.Count; ++i)
            {
                var input = SysInputs[i];
                var idx = AllNodes[input.ID].Item1;
                ret += $"{idx},{input.Name},{input.Enabled},{input.Decay}\n";
            }

            return ret;
        }

        private string ComposeOutputs()
        {
            string ret = "$TABLE:OUTPUTS\n";

            for (var i = 0; i < SysOutputs.Count; ++i)
            {
                var output = SysOutputs[i];
                var idx = AllNodes[output.ID].Item1;
                ret += $"{idx},{output.Name},{output.Enabled},{output.Decay}\n";
            }

            return ret;
        }

        private void Analyse()
        {
            ConstructCircuit(Project.RootCircuit);
        }

        private void ResolveCircuit(CircuitModel circuit)
        {
            if(Progress != null && circuit == Project.RootCircuit)
            {
                Progress.Value = 0;
                Progress.Maximum = circuit.CountCircuits();
            }

            foreach(var subcirc in circuit.SubCircuits) { ResolveCircuit(subcirc); }

            if (circuit == Project.RootCircuit) { Progress?.PerformStep(); return; }

            foreach(var input in circuit.Inputs)
            {
                var feeds = Dendrites.Where(x => x.Receiver == input);
                var trans = Dendrites.Where(x => x.Sender == input);

                foreach(var feed in feeds)
                {
                    foreach(var tran in trans)
                    {
                        RegisterDendrite(new DendriteModel(feed.Sender, tran.Receiver), circuit);
                        Dendrites.Remove(tran);
                    }
                    Dendrites.Remove(feed);
                }
                AllNodes.Remove(input.ID);
                SubInputs.Remove(input.ID);
            }

            foreach (var output in circuit.Outputs)
            {
                var feeds = Dendrites.Where(x => x.Receiver == output);
                var trans = Dendrites.Where(x => x.Sender == output);

                foreach (var feed in feeds)
                {
                    foreach (var tran in trans)
                    {
                        RegisterDendrite(new DendriteModel(feed.Sender, tran.Receiver), circuit);
                        Dendrites.Remove(tran);
                    }
                    Dendrites.Remove(feed);
                }
                AllNodes.Remove(output.ID);
                SubInputs.Remove(output.ID);
            }

            Progress?.PerformStep();
        }

        private void ConstructCircuit(CircuitModel circuit)
        {
            var iid = RegisterIon(circuit.Ions);
            Circuits.Add(circuit.ID, (Circuits.Count, iid, circuit));
            foreach(var neuron in circuit.Neurons) { RegisterNeuron(neuron, circuit); }
            foreach(var input in circuit.Inputs) { RegisterInput(input, circuit); }
            foreach(var output in circuit.Outputs) { RegisterOutput(output, circuit); }
            foreach(var ddr in circuit.Dendrites) { RegisterDendrite(ddr, circuit); }
            foreach(var subcirc in circuit.SubCircuits) { ConstructCircuit(subcirc); }
            Progress?.PerformStep();
        }

        private void RegisterInput(InputModel input, CircuitModel circuit)
        {
            if (circuit == Project.RootCircuit)
            {
                SysInputs.Add(input);
            } else
            {
                SubInputs[input.ID] = [];
            }

            AllNodes[input.ID] = (AllNodes.Count, input);
            Progress?.PerformStep();
        }

        private void RegisterOutput(OutputModel output, CircuitModel circuit)
        {
            if (circuit == Project.RootCircuit)
            {
                SysOutputs.Add(output);
            }
            else
            {
                SubOutputs[output.ID] = [];
            }

            AllNodes[output.ID] = (AllNodes.Count, output);
            Progress?.PerformStep();
        }

        private void RegisterDendrite(IDendriteModel dendrite, CircuitModel circuit)
        {
            Dendrites.Add(dendrite);
            if (dendrite.Sender is InputModel input)
            {
                if (circuit != Project.RootCircuit)
                {
                    SubInputs[input.ID].Add(dendrite.Receiver);
                }
            }

            if (dendrite.Receiver is OutputModel output)
            {
                if(circuit != Project.RootCircuit)
                {
                    SubOutputs[output.ID].Add(dendrite.Sender);
                }
            }

            Progress?.PerformStep();
        }

        private int RegisterIon(IonState state)
        {
            int iid;
            var key = (
                state.Na.Parts, state.Na.Concentration,
                state.K.Parts, state.K.Concentration,
                state.Ca.Parts, state.Ca.Concentration,
                state.Cl.Parts, state.Cl.Concentration
                );

            if(Ions.TryGetValue(key, out var val))
            {
                iid = val.Item1;
            } else
            {
                iid = Ions.Count;
                Ions.Add(key, (iid, state));
            }

            return iid;
        }

        private void RegisterNeuron(NeuronModel neuron, CircuitModel circuit)
        {
            var nid = AllNodes.Count;
            var iid = RegisterIon(neuron.Ions);
            Neurons[neuron.ID] = (nid, iid, neuron, circuit);
            AllNodes[neuron.ID] = (nid, neuron);
            Progress?.PerformStep();
        }

        private void LogError(string msg, CircuitErrorCode errCode)
        {
            NCLogger.Log(msg, NCLogger.LogType.ERR, true);
            Errors.Add(new CircuitError(msg, errCode));
            if(Progress != null) { Progress.ForeColor = Color.Red; }
        }

        #endregion


    }
}
