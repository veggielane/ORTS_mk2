using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using ORTS.Core.Primitives;
namespace ORTS.OpenTK
{
    public static class Vect3Extensions
    {
        public static Vector3 ToVector3(this Vect3 v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        }
    }
}
