using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
namespace ORTS.Core.Interfaces
{
    public interface IObjectView
    {
        bool Loaded { get; set; }
        void Load();
        void Update();
        void Render(Matrix4 cameraMatrix);
    }
}
