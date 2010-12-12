using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Interfaces;
using System.Threading;

namespace ORTS.Core
{
    public class GameEngine: IDisposable
    {
        public IRenderer Renderer { get; set; }
        public IPhysics Physics { get; set; }
        public static GameState State { get; set; }

        public bool IsRunning { get; private set; }
        ManualResetEvent[] waitHandles;

        public GameEngine()
        {
            IsRunning = false;
            waitHandles = new ManualResetEvent[]{ new ManualResetEvent(false)};
            Initialise();
        }

        protected virtual void Initialise()
        {
            State = new GameState();
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                ThreadPool.QueueUserWorkItem(Renderer.Start);
            }
        }
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                waitHandles[0].Set();
            }
        }
        public void Update()
        {
        
        }

        public virtual void Dispose()
        {
            Stop();
        }
    }
}
