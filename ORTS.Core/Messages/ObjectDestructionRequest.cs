using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Timing;
using ORTS.Core.GameObjects;

namespace ORTS.Core.Messages
{

    public class ObjectDestructionRequest : BaseMessage, IObjectLifetimeRequest
    {
        public IGameObject GameObject { get; private set; }

        public ObjectDestructionRequest(IGameTime timeSent, IGameObject gameObject)
        :base(timeSent)
        {
            GameObject = gameObject;
        }
    }
}
