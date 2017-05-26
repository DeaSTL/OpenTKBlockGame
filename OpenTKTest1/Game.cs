using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;


namespace OpenTKTest1
{
    class Game : GameWindow
    {
        private float PositionX = 0;
        private float PositionY = 0;
        private float PositionZ = 0;

        private float AngleX = 0;
        private float AngleZ = 0;
        private float AngleY = 0;

        private int current_frame = 0;

        private MeshWorld mesh = new MeshWorld(64, 64);




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
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1.0f, 1.0f, -0.5f });



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
                PositionX -= 0.2f;
            }
            if (Keyboard[Key.D])
            {
                PositionX += 0.2f;
            }
            if (Keyboard[Key.W])
            {
                PositionZ -= 0.2f;
            }
            if (Keyboard[Key.S])
            {
                PositionZ += 0.2f;

            }
            if (Keyboard[Key.Space])
            {
                PositionY -= 0.4f;

            }
            if (Keyboard[Key.ShiftLeft])
            {
                PositionY += 0.4f;

            }

            current_frame += 1;




        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            
            AngleX = e.X / 10;

            AngleZ = -e.Y/10;
            
            UpdateMouse();
            if(current_frame %100 == 1)
            {
                
                Console.WriteLine("-----------------");
                Console.WriteLine("AngleX:" + AngleX);
                Console.WriteLine("AngleY:" + AngleY);
                Console.WriteLine("AngleZ:" + AngleZ);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
            }
            //Console.WriteLine(Process.GetCurrentProcess().StartTime.Millisecond);
            


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
            GL.Rotate(AngleY, 0.0f, 0.0f, 1.0f);
            GL.Translate(PositionX,PositionY, PositionZ);



            GL.Begin(BeginMode.Triangles);

            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f + PositionX, -1.0f + PositionY, 4.0f + PositionZ);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f + PositionX, -1.0f + PositionY, 4.0f + PositionZ);
            GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f + PositionX, 1.0f + PositionY, 4.0f + PositionZ);

            GL.End();
            mesh.Draw();
            /*
            for (int i = 0; i < 40; i++) {
                for (int j = 0; j < 40; j++)
                {
                    new Cube(0.2f).Draw(i * 0.2f, -2, j * 0.2f);
                }
            } 
            */
            
          
            GL.PopMatrix();




            //Console.WriteLine(modelview.ExtractRotation().ToString());

            SwapBuffers();
        }
    }
}
