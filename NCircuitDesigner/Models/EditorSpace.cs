using System.Drawing;

namespace NCircuitDesigner.Models
{
    class EditorSpace
    {
        private readonly List<NControl> controls = [];
        public Point Center { get; set; }
        public float Scale { get; set; }

        public EditorSpace()
        {
            Center = new Point(0, 0);
            Scale = 1;
        }

    }
}
