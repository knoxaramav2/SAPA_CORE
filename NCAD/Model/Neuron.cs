using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NCAD.Model
{
    public interface INeuron : IControlModel, IIonic
    {

    }

    public class Neuron : INeuron
    {
        public float INaC { get; set; } = 142f;
        public float IKC { get; set; } = 5f;
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public INeuron Copy()
        {
            throw new NotImplementedException();
        }

        public void Scale(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
