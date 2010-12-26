using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Primitives;

namespace ORTS.Core.GameObjects
{
    public interface IHasVelocity:IHasPosition
    {
        Vect3 Velocity { get; set; }
    }
}
