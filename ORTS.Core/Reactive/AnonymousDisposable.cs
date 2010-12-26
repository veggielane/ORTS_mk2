using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORTS.Core.Reactive
{
    public class AnonymousDisposable : IDisposable
    {
        Action dispose;
        public AnonymousDisposable(Action dispose)
        {
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose();
        }
    }
}
