using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.BuildSys
{
    internal static class CircuitBuilder
    {
        private static Dictionary<Guid, RegionModel> RegionList = [];
        private static Dictionary<Guid, NeuronModel> NeuronList = [];
        private static Dictionary<Guid, InputModel> InputList = [];
        private static Dictionary<Guid, OutputModel> OutputList = [];
        private static Dictionary<(Guid, Guid), Dendrite> Dendrites = [];
        private static uint IndexLevel = 0;

        private static bool RegisterNode(INodeModel node)
        {
            if (node is NeuronModel neuron)
            {
                if (NeuronList.ContainsKey(neuron.ID)) { return false; }
                NeuronList[neuron.ID] = neuron;
            } else if (node is RegionModel region)
            {
                if (RegionList.ContainsKey(region.ID)) { return false; }
                RegionList[region.ID] = region;
            } else if (node is InputModel input)
            {
                if (InputList.ContainsKey(input.ID)) { return false; }
                InputList[input.ID] = input;
            } else if (node is OutputModel output)
            {
                if (OutputList.ContainsKey(output.ID)) { return false; }
                OutputList[output.ID] = output;
            } else
            {
                throw new Exception($"Unable to register unknown node type: {node.ID} as {node.GetType()}");
            }

            return true;
        }

        private static bool RegisterDendrite(INodeModel source, Dendrite connection)
        {
            var key = (source.ID, connection.ID);
            if (Dendrites.ContainsKey(key)) { return false; }
            Dendrites[key] = connection;
            return true;
        }

        private static string BuildNeuron(NeuronModel neuron)
        {
            return 
                $"NRN:" +
                $"ID={neuron.ID})," +
                $"NAME={neuron.Name}," +
                $"BIAS={neuron.Bias}," +
                $"DECAY={neuron.Decay}," +
                $"CHARGE={neuron.Charge}," +
                $";";
        }

        private static string BuildRegion(RegionModel region)
        {
            return 
                $"REGION:" +
                $"ID={region.ID}," +
                $"NAME={region.Name}," +
                $";";
        }

        private static string BuildInput(InputModel input)
        {
            return 
                "INPUT:" +
                $"ID={input.ID}," +
                $"NAME={input.Name}," +
                $"";
        }

        private static string BuildOutput(OutputModel output)
        {
            return "";
        }

        private static string BuildHeader(ProjectState project)
        {
            return $"";
        }

        private static string BuildContent(ProjectState project)
        {
            var NL = Environment.NewLine;

            var content =
                $"[HEADER]{NL}" +
                $"{BuildHeader(project)}{NL}" +
                $"[REGION]{NL}" +
                $"{RegionList.Values.Select(x => BuildRegion(x) + NL)}" +
                $"[INPUT]{NL}" +
                $"{InputList.Values.Select(x => BuildInput(x) + NL)}" +
                $"[OUTPUT]{NL}" +
                $"{OutputList.Values.Select(x => BuildOutput(x) + NL)}" +
                $"[NEURONS]{NL}" +
                $"{NeuronList.Values.Select(x => BuildNeuron(x) + NL)}"
                ;

            return content;
        }

        private static void RegisterComponentRecursive(INodeModel node)
        {
            if (!RegisterNode(node)) { return; }

            if (node is NeuronModel neuron)
            {
                foreach(var ddr in neuron.Dendrites)
                {
                    RegisterDendrite(node, ddr);
                }
            } else if (node is OutputModel output)
            {
                foreach(var opr in output.Dendrites)
                {
                    RegisterDendrite(node, opr);
                }
            } else if (node is RegionModel region)
            {
                foreach(var rrg in region.Neurons) { RegisterNode(rrg); }
                foreach(var rrg in region.Inputs) { RegisterNode(rrg); }
                foreach(var rrg in region.Outputs) { RegisterNode(rrg); }
            }

            foreach(var con in node.Connections)
            {
                RegisterComponentRecursive(con);
            }
        }

        private static void Init()
        {
            NeuronList = [];
            RegionList = [];
            Dendrites = [];
            InputList = [];
            OutputList = [];
            IndexLevel = 0;
        }

        public static bool BuildCircuit(
            ProjectState project, RegionModel root, 
            string outputPath = "", string fileName = "")
        {
            Init();
            RegisterComponentRecursive(root);
            var content = BuildContent(project);

            outputPath = string.IsNullOrEmpty(outputPath) ?
                Util.FileIO.BuildPath : outputPath;
            fileName = string.IsNullOrEmpty(fileName) ?
                project.Name : fileName;
            
            fileName = Path.GetFileNameWithoutExtension(fileName) + ".ncr";
            var buildFile = Path.Join(outputPath, fileName);
            var success = Util.FileIO.SaveAs(buildFile, content);

            return success;
        }
    }
}
