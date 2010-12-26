using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.GameObjects;
using ORTS.Core.Timing;

namespace ORTS.Core.Messages
{
    public class ObjectCreated:BaseMessage,IObjectLifetimeNotification
    {
        public IGameObject GameObject { get; private set; }

        public ObjectCreated(IGameTime timeSent, IGameObject gameObject)
        :base(timeSent)
        {
            GameObject = gameObject;
        }
    }
}
