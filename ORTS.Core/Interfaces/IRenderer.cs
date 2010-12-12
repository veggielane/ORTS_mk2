using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core;
using System.Threading;
namespace ORTS.Core.Interfaces
{
    public interface IRenderer
    {
        void Setup();
        void Start(Object threadContext);
        void Stop();
    }
}
