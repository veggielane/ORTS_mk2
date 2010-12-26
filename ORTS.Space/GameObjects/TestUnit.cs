using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core;
using ORTS.Core.Primitives;
using ORTS.Core.Messages;
using ORTS.Core.GameObjects;
using ORTS.Core.Timing;
using System.Drawing;
namespace ORTS.Space.GameObjects
{
    public class TestUnit : IMapGO,IHasGeometry,IHasVelocity,IHasSelect
    {
        public MessageBus Bus { get; private set; }
        public Color TeamColour { get; set; }

        public Vect3 Position { get; set; }
        public Double Roll { get; set; }
        public Double Pitch { get; set; }
        public Double Yaw { get; set; }

        public Vect3 Velocity { get; set; }

        public bool Selected { get; set; }

        public TestUnit(MessageBus bus)
        {
            Bus = bus;
            Position = new Vect3();
            Velocity = new Vect3();
            Roll = 0.0;
            Pitch = 0.0;
            Yaw = 0.0;
            Selected = false;
        }

        public void Update(TickTime tickTime)
        {
            Position = Position + (Velocity * tickTime.GameTimeDelta.TotalSeconds);
            Roll += 1.0;
            Pitch += 1.0;
            Yaw += 1.0;
        }

        public override string ToString()
        {
            return "TestUnit - {{Pos:{0}}}".fmt(Position);
        }

        

    }
}
