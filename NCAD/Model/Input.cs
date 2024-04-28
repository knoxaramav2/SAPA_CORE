using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NCAD.Model
{
    internal interface IInput : IControlModel
    {

    }

    internal class Input : IInput
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IInput Copy()
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
