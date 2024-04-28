using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace NCAD.Util
{
    public static class Extensions
    {
        public static Vector3D Dot(this Vector3D self, Vector3D val)
        {
            return new Vector3D(
                (self.X * val.X) + (self.X * self.Y) + (self.X * self.Z),
                (self.Y * val.X) + (self.Y * self.Y) + (self.Y * self.Z),
                (self.Z * val.X) + (self.Z * self.Y) + (self.Z * self.Z)
                );
        }

        public static double Mag(this Vector3D self)
        {
            return Math.Sqrt(Math.Pow(self.X ,2) + Math.Pow(self.Y , 2) + Math.Pow(self.Z , 2));
        }
    }
}
