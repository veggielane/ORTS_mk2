using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ORTS.Core;
using ORTS.Core.Interfaces;
using ORTS.Core.GameObjects;
namespace ORTS.OpenTK
{
    public class OpenTKRenderer: IRenderer
    {
        public void Setup()
        {

        }
        public void Start(Object threadContext)
        {
            using (OpenTKWindow p = new OpenTKWindow())
            {
                p.Run();
            }
        }

        public void Stop()
        {
            
        }
    }
    public class OpenTKWindow : GameWindow
    {
        public OpenTKWindow() : base(1024, 768)
        {
            GL.Enable(EnableCap.DepthTest|EnableCap.PolygonSmooth);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            foreach (IHasGeometry GameObject in GameEngine.State.GameObjects.OfType<IHasGeometry>())
            {
              // Render(GameObject);
            }
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
                Exit();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

    }
}
