using CircuitDesigner.Models;

namespace CircuitDesigner.Util
{
    public static class Extensions
    {
        //GEOM
        public static Point Add(this Point a, Point b)
        {
            return new Point(a.X+b.X, a.Y + b.Y);
        }

        public static Point Sub(this Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        //TYPES
        public static string NodeTypeName(this NodeTypes type)
        {
            return type.ToString();
            //return Enum.GetName(typeof(NodeTypes), type) ?? "";
        }
    }
}
