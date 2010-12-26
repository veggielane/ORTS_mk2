using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Interfaces;
using ORTS.Core.Timing;
using ORTS.Core.Messages;
using ORTS.Core.GameObjects;
using System.Threading;

namespace ORTS.Core
{
    public class GameEngine : IHasMessageBus, IDisposable
    {
        public ObservableTimer Timer { get; private set; }
        public MessageBus Bus { get; private set; }
        public GameObjectFactory ObjectFactory { get; private set; }
        public bool IsRunning { get; private set; }

        public IEnumerable<IMapGO> MapItems()
        {
            return ObjectFactory.GameObjects.OfType<IMapGO>();
        }

        public GameEngine(ObservableTimer timer, MessageBus bus, GameObjectFactory objectFactory)
        {
            Timer = timer;
            Bus = bus;
            ObjectFactory = objectFactory;
            IsRunning = false;
            Initialise();
        }

        protected virtual void Initialise()
        {
            Timer.Subscribe(t => this.Update(t));
            Timer.SubSample(5).Subscribe(t => Bus.SendAll());
        }

        public void Update(TickTime tickTime)
        {
            foreach (var item in ObjectFactory.GameObjects)
            {
                item.Update(tickTime);
            }
        }

        public void Start()
        {
            if (!IsRunning)
            {
                Bus.Add(new SystemMessage(Timer.LastTickTime, "Engine starting."));
                Timer.Start();
                IsRunning = true;
                Bus.Add(new SystemMessage(Timer.LastTickTime, "Engine started."));
              }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                Bus.Add(new SystemMessage(Timer.LastTickTime, "Engine stopping."));
                IsRunning = false;
                Timer.Stop();
                Bus.Add(new SystemMessage(Timer.LastTickTime, "Engine stopped."));
                
            }
        }

        public virtual void Dispose()
        {
            Stop();
        }
    }
    /*
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
     * */
}
