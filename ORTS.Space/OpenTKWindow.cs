using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ORTS.Core;
using ORTS.Core.Interfaces;
using ORTS.Core.GameObjects;
using ORTS.Core.Messages;
using ORTS.Space.GameObjects;
using ORTS.Space.Views;
using System.Collections.Concurrent;

namespace ORTS.Space
{
    public class OpenTKWindow : GameWindow
    {
        private Matrix4 cameraMatrix;
        private float[] mouseSpeed = new float[2];

        GameEngine Engine;
        public ConcurrentDictionary<IGameObject, IObjectView> Views { get; private set; }

        public OpenTKWindow(GameEngine engine)
            : base(800, 600, new GraphicsMode(32,24,0,8), "test")
        {
            VSync = VSyncMode.On;

            this.Views = new ConcurrentDictionary<IGameObject, IObjectView>();

            this.Engine = engine;
            engine.Bus.Filters.ObjectLifeTimeNotifications.OfType<ObjectCreated>().Subscribe(m => CreateView(m));
            engine.Bus.Filters.ObjectLifeTimeNotifications.OfType<ObjectDestroyed>().Subscribe(m => DestroyView(m));
            
            
            GL.Enable(EnableCap.DepthTest | EnableCap.PolygonSmooth);
            cameraMatrix = Matrix4.CreateTranslation(0f, -5f, -12f);
            engine.Bus.Add(new SystemMessage(engine.Timer.LastTickTime, "called from opentk"));
        }

        public void CreateView(ObjectCreated notification)
        {

                if (notification.GameObject is TestUnit)
                {
                    Views.TryAdd(notification.GameObject, new TestUnitView((TestUnit)notification.GameObject));
                }

        }

        public void DestroyView(ObjectDestroyed notification)
        {
            //Views.TryRemove(notification.GameObject);
            IObjectView value;
            Views.TryRemove(notification.GameObject, out value);
        }



        protected override void OnLoad(EventArgs e)
        {
            this.AttachEvents();
            base.OnLoad(e);
            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.Enable(EnableCap.DepthTest);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(new Color4(0.137f, 0.121f, 0.125f, 0f));

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            
            foreach (KeyValuePair<IGameObject, IObjectView> pair in this.Views)
            {
                GL.PushMatrix();
                if (pair.Value.Loaded) pair.Value.Render(cameraMatrix);
                GL.PopMatrix();
            }
            GL.LoadMatrix(ref cameraMatrix);
            this.SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.W])
            {
                cameraMatrix = Matrix4.Mult(cameraMatrix,
                             Matrix4.CreateTranslation(0f, 0f, 10f * (float)e.Time));
            }

            if (Keyboard[Key.S])
            {
                cameraMatrix = Matrix4.Mult(cameraMatrix,
                             Matrix4.CreateTranslation(0f, 0f, -10f * (float)e.Time));
            }

            if (Keyboard[Key.A])
            {
                cameraMatrix = Matrix4.Mult(cameraMatrix,
                                            Matrix4.CreateTranslation(10f * (float)e.Time, 0f, 0f));
            }

            if (Keyboard[Key.D])
            {
                cameraMatrix = Matrix4.Mult(cameraMatrix,
                                            Matrix4.CreateTranslation(-10f * (float)e.Time, 0f, 0f));
            }
            if (Keyboard[Key.V])
            {
                Engine.Bus.Add(new ObjectCreationRequest(Engine.Timer.LastTickTime, typeof(TestUnit)));
            }
            mouseSpeed[0] *= 0.9f;
            mouseSpeed[1] *= 0.9f;
            mouseSpeed[0] += Mouse.XDelta / 100f;
            mouseSpeed[1] += Mouse.YDelta / 100f;

            cameraMatrix = Matrix4.Mult(cameraMatrix, Matrix4.CreateRotationY(mouseSpeed[0] * (float)e.Time));
            cameraMatrix = Matrix4.Mult(cameraMatrix, Matrix4.CreateRotationX(mouseSpeed[1] * (float)e.Time));

            foreach (KeyValuePair<IGameObject, IObjectView> pair in this.Views)
            {
                pair.Value.Load();
                pair.Value.Update();
            }

        }
        protected override void OnResize(EventArgs e)
        {

            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 p = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 0.1f, 500.0f);
            GL.LoadMatrix(ref p);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 mv = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref mv);
            base.OnResize(e);
        }
        protected override void OnUnload(EventArgs e)
        {
          
        }
        private void AttachEvents()
        {
            Observable.FromEvent((EventHandler<KeyboardKeyEventArgs> ev) => new EventHandler<KeyboardKeyEventArgs>(ev),
                ev => Keyboard.KeyUp += ev,
                ev => Keyboard.KeyUp -= ev).Where(e => e.EventArgs.Key == Key.C).Subscribe(e =>
                {
                    Engine.Bus.Add(new ObjectCreationRequest(Engine.Timer.LastTickTime, typeof(TestUnit)));
                    Engine.Bus.Add(new SystemMessage(Engine.Timer.LastTickTime, Key.C.ToString() + " Pressed"));
                });
            Observable.FromEvent((EventHandler<KeyboardKeyEventArgs> ev) => new EventHandler<KeyboardKeyEventArgs>(ev),
                ev => Keyboard.KeyUp += ev,
                ev => Keyboard.KeyUp -= ev).Where(e => e.EventArgs.Key == Key.Escape).Subscribe(e =>
                {
                    Engine.Bus.Add(new SystemMessage(Engine.Timer.LastTickTime, e.EventArgs.Key.ToString() + " Pressed"));
                    Exit();
                });
            Mouse.WheelChanged += new EventHandler<MouseWheelEventArgs>(Mouse_WheelChanged);
        }

        void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
                cameraMatrix = Matrix4.Mult(cameraMatrix, Matrix4.CreateTranslation(0f, 0f, e.DeltaPrecise*5f));
        }
    }
}
