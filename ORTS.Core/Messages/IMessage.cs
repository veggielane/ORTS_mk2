using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Timing;

namespace ORTS.Core.Messages
{
    public interface IMessage
    {
        IGameTime TimeSent { get; }
    }
}
