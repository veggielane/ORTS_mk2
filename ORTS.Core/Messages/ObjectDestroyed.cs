using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.GameObjects;
using ORTS.Core.Timing;

namespace ORTS.Core.Messages
{
    public class ObjectDestroyed : BaseMessage, IObjectLifetimeNotification
    {
        public IGameObject GameObject { get; private set; }

        public ObjectDestroyed(IGameTime timeSent, IGameObject gameObject)
            : base(timeSent)
        {
            GameObject = gameObject;
        }
    }
}
