using CircuitDesigner.Models;
using CircuitDesigner.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CircuitDesigner.Tests
{
    public class SerialTest
    {
        [Fact]
        public void SerializeCircuitTest()
        {
            CircuitModel circuit = new("TestCircuit", setupIO:true);

            //Verify position data persists
            circuit.Inputs[0].Pos.Add(new Point(5, 5));

            var serial = FileUtil.JsonSerialize(circuit);
            var circuit2 = FileUtil.JsonDeSerialize<CircuitModel>(serial);

            Assert.NotNull(circuit2);
            Assert.Empty(circuit2.SubCircuits);
            Assert.Equal(circuit.Inputs.Count, circuit2.Inputs.Count);
            Assert.Equal(circuit.Outputs.Count, circuit2.Outputs.Count);
            Assert.Equal(circuit.Inputs[0].Pos, circuit2.Inputs[0].Pos);
        }

        [Fact]
        public void SerializeProjectTest()
        {
            var project = ProjectState.LoadOrDefault();
            var circuit = project.CurrentCircuit;
            circuit.Inputs[0].Pos.Add(new Point(5, 5));
            var serial = FileUtil.JsonSerialize(project);
            var project2 = FileUtil.JsonDeSerialize<ProjectState>(serial);
            var circuit2 = project2.CurrentCircuit;

            Assert.NotNull(project2);
            Assert.Equal(project.ProjectName, project2.ProjectName);
            Assert.Equal(project.CurrentCircuit.ID, project2.CurrentCircuit.ID);
            Assert.Equal(circuit.Inputs[0].Pos, circuit2.Inputs[0].Pos);
        }

    }
}
