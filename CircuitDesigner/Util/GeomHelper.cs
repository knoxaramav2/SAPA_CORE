using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Util
{
    internal static class GeomHelper
    {
        internal static double Dist(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        internal static Point ADToCoord(double angle, double dist)
        {
            return new Point(
                (int)(Math.Cos(angle) * dist),
                (int)(Math.Sin(angle) * dist)
                );
        }

        internal static double Angle(Point p1, Point p2 = new Point())
        {
            return Math.Atan2(p1.Y - p2.Y, p1.X - p2.X);
        }

    }

}
