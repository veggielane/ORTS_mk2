using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Drawing;

using ORTS.Core;
using ORTS.Core.Messages;
using ORTS.Core.Primitives;
using ORTS.Core.GameObjects;
using ORTS.Space.GameObjects;
namespace ORTS.Space
{
    public class SpaceGameObjectFactory : GameObjectFactory
    {
        public SpaceGameObjectFactory(MessageBus bus)
            : base(bus)
        {
        }
        
        public override void CreateGameObject(ObjectCreationRequest request)
        {
            if (request.ObjectType == typeof(TestUnit))
            {
                Random rnd = new Random();
                var item = new TestUnit(this.Bus) {
                    Velocity = new Vect3(rnd.Next(-10, 10) * rnd.NextDouble(), rnd.Next(-10, 10) * rnd.NextDouble(), rnd.Next(-10, 10) * rnd.NextDouble()),
                    TeamColour = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                    Selected = Convert.ToBoolean(rnd.Next(0, 1))
                };
                this.GameObjects.Add(item);
                Bus.Add(new ObjectCreated(request.TimeSent, item));
            }
            base.CreateGameObject(request);
        }

    }
}
