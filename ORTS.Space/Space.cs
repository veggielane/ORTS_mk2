using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core;
using ORTS.OpenTK;
namespace ORTS.Space
{
    public class Space
    {
        public Space()
        {
            using (GameEngine Engine = new GameEngine())
            {
                Engine.Renderer = new OpenTKRenderer();
                Engine.Renderer.Setup();
                Engine.Start();
            }
        }
    }
}
