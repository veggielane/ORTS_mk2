using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.Interfaces;
using ORTS.Core.GameObjects;
using ORTS.Space.GameObjects;
using ORTS.Core.Messages;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;
using OpenTK.Graphics.OpenGL;
using ORTS.OpenTK;
using ORTS.OpenTK.Shapes;
namespace ORTS.Space.Views
{
    public class TestUnitView:IObjectView
    {

        struct Vbo { public int VboID, EboID, NumElements; }
        Vbo vbo = new Vbo();


        readonly static short[] CubeElements = new short[]
        {
            0, 1, 2, 2, 3, 0, // front face
            3, 2, 6, 6, 7, 3, // top face
            7, 6, 5, 5, 4, 7, // back face
            4, 0, 3, 3, 7, 4, // left face
            0, 1, 5, 5, 4, 0, // bottom face
            1, 5, 6, 6, 2, 1, // right face
        };
        VertexPositionColour[] CubeVertices;

        private TestUnit unit;
        public bool Loaded { get; set; }

        public TestUnitView(TestUnit unit)
        {
            this.unit = unit;
        }
        public void Load()
        {

            if (!Loaded)
            {
                CubeVertices = new VertexPositionColour[]{
                    new VertexPositionColour(-1.0f, -1.0f,  1.0f, unit.TeamColour),
                    new VertexPositionColour( 1.0f, -1.0f,  1.0f, unit.TeamColour),
                    new VertexPositionColour( 1.0f,  1.0f,  1.0f, unit.TeamColour),
                    new VertexPositionColour(-1.0f,  1.0f,  1.0f, unit.TeamColour),
                    new VertexPositionColour(-1.0f, -1.0f, -1.0f, unit.TeamColour),
                    new VertexPositionColour( 1.0f, -1.0f, -1.0f, unit.TeamColour), 
                    new VertexPositionColour( 1.0f,  1.0f, -1.0f, unit.TeamColour),
                    new VertexPositionColour(-1.0f,  1.0f, -1.0f, unit.TeamColour) 
                };
                vbo = LoadVBO(CubeVertices, CubeElements);
                Loaded = true;
            }
        }
        public void Update()
        {
            
        }
        public void Render(Matrix4 cameraMatrix)
        {
            GL.Translate(unit.Position.ToVector3());
            GL.Rotate(unit.Roll, Vector3d.UnitZ);
            GL.Rotate(unit.Pitch, Vector3d.UnitX);
            GL.Rotate(unit.Yaw, Vector3d.UnitY);
            Draw(vbo);
            GL.Translate(-2f, -2f, - 0f);
            if (unit.Selected)
            {
                GL.Color3(Color.Red);
            }
            else
            {
                GL.Color3(Color.Green);
            }
            GL.Begin(BeginMode.LineLoop);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 4, 0);
            GL.Vertex3(4, 4, 0);
            GL.Vertex3(4, 0, 0);
            GL.End();
            //DrawCube();
        }
        private void DrawCube()
        {
            GL.Begin(BeginMode.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }
        Vbo LoadVBO<TVertex>(TVertex[] vertices, short[] elements) where TVertex : struct
        {
            Vbo handle = new Vbo();
            int size;
            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * BlittableValueType.StrideOf(vertices)), vertices,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (vertices.Length * BlittableValueType.StrideOf(vertices) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");

            GL.GenBuffers(1, out handle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(elements.Length * sizeof(short)), elements,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (elements.Length * sizeof(short) != size)
                throw new ApplicationException("Element data not uploaded correctly");

            handle.NumElements = elements.Length;
            return handle;
        }
        void Draw(Vbo handle)
        {
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(CubeVertices), new IntPtr(0));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, BlittableValueType.StrideOf(CubeVertices), new IntPtr(12));
            GL.DrawElements(BeginMode.Triangles, handle.NumElements, DrawElementsType.UnsignedShort, IntPtr.Zero);
        }
    }
}
