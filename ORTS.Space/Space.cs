using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core;
using ORTS.Core.Interfaces;
using ORTS.Core.Timing;
using ORTS.Core.GameObjects;
using ORTS.Core.Primitives;
using ORTS.Core.Messages;
using ORTS.OpenTK;
using System.Diagnostics;
using System.ComponentModel;
using Ninject;
using Ninject.Modules;
using ORTS.Space.GameObjects;
using ORTS.Space.Views;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ORTS.Space
{
    public class Space
    {
        public Space()
        {
            var kernal = new StandardKernel(new TestModule());
            using (var engine = kernal.Get<GameEngine>())
            {
                //engine.Renderer = new OpenTKRenderer();
                //engine.Renderer.Setup();
                //ThreadPool.QueueUserWorkItem(Renderer.Start);
                engine.Timer.TimerMessages.Subscribe(m =>
                {
                   // Console.WriteLine("{0} - {1}".fmt(m.CurrentTickTime.GameTimeElapsed.ToString(), m.Message));
                });

                engine.Timer.Subscribe(t =>
                {
                  // Console.WriteLine("Doing stuff.");
                });

                Setup(engine);

                engine.Bus.OfType<SystemMessage>().Subscribe(m => Console.WriteLine("{0} SYSTEM - {1}", m.TimeSent.ToString(), m.Message));
                engine.Bus.Add(new SystemMessage(engine.Timer.LastTickTime, "OpenTK starting."));
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state) {
                    using (OpenTKWindow p = new OpenTKWindow(engine))
                    {
                       // p.AddView(typeof(TestUnit), new TestUnitView());
                        p.Run();
                    }
                }), null);

               
                bool finish = false;
                while (!finish)
                {
                    engine.Start();

                    Console.ReadKey(true);

                    engine.Stop();

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        finish = true;
                }

                Console.WriteLine("Timer should have stopped.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);

            }
        }
        private void Setup(GameEngine engine)
        {
            engine.Timer.Subscribe(t =>
            {/*
               * foreach (var item in engine.MapItems().OfType<TempItem>())
                {
                    Console.WriteLine(item.ToString());
                }*/
            });

        }

    }
    public class TestModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<GameEngine>().ToSelf();
            Kernel.Bind<ObservableTimer>().To<AsyncObservableTimer>();
            Kernel.Bind<MessageBus>().ToSelf().InSingletonScope();
            Kernel.Bind<GameObjectFactory>().To<SpaceGameObjectFactory>().InSingletonScope();
            Kernel.Bind<BusFilters>().ToSelf();
        }
    }
    /*
    class TempItem : IMapGO
    {
        public MessageBus Bus { get; private set; }
        public ObservableTimer Timer { get; private set; }
        public Vect3 Position { get; set; }
        public Vect3 Orientation { get; set; }
        public Vect3 Velocity { get; set; }

        public TempItem(MessageBus bus, ObservableTimer timer)
        {
            Bus = bus;
            Timer = timer;
            Position = new Vect3();
            Velocity = new Vect3();
            Bus.Add(new SystemMessage(timer.LastTickTime,"Map Object Created"));
        }

        public void Update(TickTime tickTime)
        {
            Position = Position + (Velocity * tickTime.GameTimeDelta.TotalSeconds);
        }

        public override string ToString()
        {
            return "TempItem - {{Pos:{0}}}".fmt(Position);
        }
    }*/
}