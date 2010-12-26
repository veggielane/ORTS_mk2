using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Primitives;

namespace ORTS.Core.GameObjects
{
    public interface IHasPosition
    {
        Vect3 Position { get; }
        Double Roll { get; }
        Double Pitch { get; }
        Double Yaw { get; }
    }
}
