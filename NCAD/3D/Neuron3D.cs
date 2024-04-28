using NCAD.Model;

namespace NCAD._3D
{
    class Neuron3D : Control3D
    {
        public Neuron Model
        {
            get { return (Neuron)CModel; }
            private set { CModel = (IControlModel)value; }
        }

        public Neuron3D(Neuron model): base((IControlModel)model) { }

        public Neuron3D(): base((IControlModel)new Neuron())
        {

        }
    }
}
