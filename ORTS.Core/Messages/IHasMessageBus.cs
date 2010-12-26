using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Messages;

namespace ORTS.Core
{
    public interface IHasMessageBus
    {
        MessageBus Bus { get; }
    }
}
