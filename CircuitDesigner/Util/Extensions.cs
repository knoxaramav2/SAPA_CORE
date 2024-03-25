using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Util
{
    public static class Extensions
    {
        public static Point Add(this Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point Sub(this Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

    }
}
