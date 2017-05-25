using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using LibNoise.Builder;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ClooN;

namespace OpenTKTest1
{
    class Game : GameWindow
    {
        private float PositionX = 0;
        private float PositionY = 0;
        private float PositionZ = 0;

        private float AngleX = 0;
        private float AngleZ = 0;
        float[] values;

      

        Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);

        public Game()
            : base()
        {
            
            this.CursorVisible = false;
        }

        public Game(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title)
        {
        }
        Vector3 mouse_delta;
        MouseState current, previous;

     

        void UpdateMouse()
        {
            current = Mouse.GetState();
            if (current != previous)
            {
                // Mouse state has changed
                mouse_delta = new Vector3(
                   current.X - previous.X,
                   current.Y - previous.Y,
                   current.WheelPrecise - previous.WheelPrecise);
            }
            else
            {
                mouse_delta = Vector3.Zero;
            }
            previous = current;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            int noiseSize = 40;
            float seed = 4532;
            Single3[] input = new Single3[noiseSize * noiseSize];
            for (int y = 0; y < noiseSize; y++)
            {
                for (int x = 0; x < noiseSize; x++)
                {
                    input[x + noiseSize * y] = new Single3((float)x / noiseSize * 2 - 1, (float)y / noiseSize * 2 - 1, 0.0f);
                }
            }

            var module = Noise.FractalBrownianMotion(8, 4f, 2f, 0.5f) * 0.5f + 0.5f;
            var program = new NoiseProgram(module);
            program.Compile();
            float[] values = program.GetValues(input,ref seed);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
            if (Keyboard[Key.A]) {
                PositionX -= 0.1f;
            }
            if (Keyboard[Key.D])
            {
                PositionX += 0.1f;
            }
            if (Keyboard[Key.W])
            {
                PositionZ -= 0.1f;
            }
            if (Keyboard[Key.S])
            {
                PositionZ += 0.1f;
               
            }
            
            
            
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            AngleX = e.X/10;
            AngleZ = -e.Y/10;
            UpdateMouse();
            
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.PushMatrix();


            GL.Rotate(AngleX, 0.0f, 1.0f, 0.0f);
            GL.Rotate(AngleZ, 1.0f, 0.0f, 0.0f);
            GL.Translate(PositionX, 0, PositionZ);
            
            

            //GL.Begin(BeginMode.Triangles);
            
            //GL.Color3(1.0f, 1.0f,a 0.0f); GL.Vertex3(-1.0f + PositionX, -1.0f + PositionY, 4.0f + PositionZ);
            //GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f + PositionX, -1.0f + PositionY, 4.0f + PositionZ);
            //GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f + PositionX, 1.0f + PositionY, 4.0f + PositionZ);
          
            //GL.End();
          
            foreach()       
            new Cube(0.2f).Draw((float)pos,-2,j * 0.2f);
          
            GL.PopMatrix();




            //Console.WriteLine(modelview.ExtractRotation().ToString());

            SwapBuffers();
        }
    }
}
