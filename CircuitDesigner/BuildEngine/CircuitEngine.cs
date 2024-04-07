using CircuitDesigner.Controls;
using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CircuitDesigner.BuildEngine
{
    struct DendriteBridge
    {
        private List<Guid> Outputs = [];
        private List<Guid> Inputs = [];
        public DendriteBridge()
        {

        }
    }

    internal class CircuitEngine : ICircuitEngine
    {
        private ToolStripProgressBar? Progress;

        readonly ProjectState Project;

        readonly Dictionary<Guid, (string, INodeModel)> AllNodes = [];
        readonly List<InputModel> SysInputs = [];
        readonly List<OutputModel> SysOutputs = [];
        readonly Dictionary<Guid, List<INodeModel>> SubInputs = [];
        readonly Dictionary<Guid, List<INodeModel>> SubOutputs = [];
        readonly List<IDendriteModel> Dendrites = [];
        readonly Dictionary<Guid, (string, NeuronModel)> Neurons = [];
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
            string prj = $"#PROJECT={Project.ProjectName}";
            string cmps = $"COMPONENTS={Project.RootCircuit.ComponentCountRec()}";
            string inps = $"INPUTS={Project.RootCircuit.Inputs.Count}";
            string outs = $"OUTPUTS={Project.RootCircuit.Outputs.Count}";

            return
$@"{prj}
{cmps}
{inps}
{outs}

{ComposeInputs()}
{ComposeOutputs()}
{ComposeNeurons()}
{Compose(Project.RootCircuit)}
";
        }

        private string Compose(CircuitModel circuit)
        {
            Dictionary<Guid, int> idxDict = [];
            string ret = "$CIRCUIT\n";

            foreach (var ddr in Dendrites)
            {
                var ln = AllNodes[ddr.Sender.ID];
                var rn = AllNodes[ddr.Receiver.ID];
                ret += $"{ln.Item1}<{ddr.Weight}>{rn.Item1};";
            }

            return ret;
        }

        private string ComposeNeurons()
        {
            string ret = "$TABLE:NEURONS\n";

            for (var i = 0; i < Neurons.Count; ++i)
            {
                var neuron = Neurons.ElementAt(i).Value;
                ret += $"N{neuron.Item1}, {neuron.Item2.Name}, {neuron.Item2.Bias}, {neuron.Item2.Decay}\n";
            }

            return ret;
        }

        private string ComposeInputs()
        {
            string ret = "$TABLE:INPUTS\n";

            for (var i = 0; i < SysInputs.Count; ++i)
            {
                var input = SysInputs[i];
                ret += $"I{i}, {input.Name}, {input.Enabled}\n";
            }

            return ret;
        }

        private string ComposeOutputs()
        {
            string ret = "$TABLE:OUTPUTS\n";

            for (var i = 0; i < SysOutputs.Count; ++i)
            {
                var input = SysOutputs[i];
                ret += $"O{i}, {input.Name}, {input.Enabled}\n";
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

            AllNodes[input.ID] = ($"I{AllNodes.Count}", input);
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

            AllNodes[output.ID] = ($"O{AllNodes.Count}", output);
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

        private void RegisterNeuron(NeuronModel neuron, CircuitModel circuit)
        {
            var nid = $"N{AllNodes.Count}";
            Neurons[neuron.ID] = (nid, neuron);
            AllNodes[neuron.ID] = (nid, neuron);

            Progress?.PerformStep();
        }

        private void LogError(string msg, CircuitErrorCode errCode)
        {
            NCLogger.Log(msg, NCLogger.LogType.ERR, true);
            Errors.Add(new CircuitError(msg, errCode));
        }

        #endregion


    }
}
