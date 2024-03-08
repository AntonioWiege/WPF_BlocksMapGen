using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WPFGettingInto
{
    internal static class ExtensionsCustom
    {
        public static Point3D Add(this Point3D original, Point3D toAdd)
        {
            original.X += toAdd.X;
            original.Y += toAdd.Y;
            original.Z += toAdd.Z;
            return original;
        }
    }
}
