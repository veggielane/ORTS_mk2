using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Timing;
using System.Drawing;
namespace ORTS.Core.GameObjects
{
    public interface IGameObject : IHasMessageBus
    {
        Color TeamColour { get; set; }
        void Update(TickTime tickTime);
    }
}
