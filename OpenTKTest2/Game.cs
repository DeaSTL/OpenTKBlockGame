using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace OpenTKTest2
{
    class Game : GameWindow
    {
        
        public double AngleX { get; private set; }
        public double AngleZ { get; private set; }
        public double AngleY { get; private set; }

        public double PositionX { get; private set; }
        public double PositionY { get; private set; }
        public double PositionZ { get; private set; }

        public double XSlope { get; private set; }
        public double YSlope { get; private set; }

        public double Pitch { get; private set;}

        public Random rand = new Random();

        Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
        Shapes.Rect[,] rects;
        public int CubeArraySize = 50;

        public Game(int width, int height, string v) : base(width, height)
        {
            Title = v;
        }
        private double ToRadians(double angle) {
            return Math.PI * angle / 180.0;
        }
        public Shapes.Rect[,] GetCubeArray() {
            Shapes.Rect[,] rects = new Shapes.Rect[CubeArraySize, CubeArraySize];
            for (int i = 0; i < CubeArraySize; i++) {
                for (int j = 0; j < CubeArraySize; j++) {
                    Shapes.Rect rect = new Shapes.Rect(
                    new Vector3(i, 0, j), 1, 1, 1,
                    Color.FromArgb(rand.Next() % 255, 
                                   rand.Next() % 255, 
                                   rand.Next() % 255));
                    rects[i, j] = rect;

                }
            }
            return rects;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            
           
            rects = GetCubeArray();




            // Enable Light 0 and set its parameters.
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1.0f, 1.0f, -0.5f });
           

            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 3.0f, 3.0f, 3.0f, 3.0f });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 3.0f, 3.0f, 3.0f, 3.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 3.0f, 3.0f, 3.0f, 3.0f });

            GL.LightModel(LightModelParameter.LightModelAmbient, 1);
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);

            //Use GL.Material to set your object's material parameters.
            
           
           
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            PreDraw();
            //render code here
            for (int i = 0; i < CubeArraySize; i++) {
                for (int j = 0; j < CubeArraySize; j++) {
                    rects[i,j].Draw();
                }
            }
            PostDraw();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            XSlope = (Math.Sin(ToRadians(AngleY)));
            YSlope = (Math.Cos(ToRadians(AngleY)));

            if (Keyboard[Key.Escape])
                Exit();
            if (Keyboard[Key.A])
            {
                PositionX -= 0.2f * (float)YSlope;
                PositionZ -= 0.2f * (float)XSlope;
            }
            if (Keyboard[Key.D])
            {
                PositionX += 0.2f * (float)YSlope;
                PositionZ += 0.2f * (float)XSlope;
            }
            if (Keyboard[Key.W])
            {
                PositionX += 0.2f * (float)XSlope;
                PositionZ -= 0.2f * (float)YSlope;
            }
            if (Keyboard[Key.S])
            {
                PositionX -= 0.2f * (float)XSlope;
                PositionZ += 0.2f * (float)YSlope;

            }
            if (Keyboard[Key.Space])
            {
                PositionY -= 0.4f;

            }
            if (Keyboard[Key.ShiftLeft])
            {
                PositionY += 0.4f;

            }
            if (Keyboard[Key.Q])
            {
                AngleY -= 1f;

            }
            if (Keyboard[Key.E])
            {
                AngleY += 1f;

            }
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { (float)PositionX, (float)PositionY, (float)PositionZ });
            //Console.WriteLine("YAngle" + AngleY);
            

        }
        
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            AngleY = e.X / 10;

            Pitch = e.Y/10;
            
            if (AngleY >= 0 && AngleY <= 90)
            {
                AngleZ = 0;
                AngleX = -Pitch;
            }
            if(AngleY >= 90 && AngleY <= 180) {
                AngleZ = 0;
                AngleX = -Pitch;
            }
            if (AngleY >= 180 && AngleY <= 270)
            {
                AngleX = 0;
                AngleZ = -Pitch;
            }
            if (AngleY >= 270 && AngleY <= 360)
            {
                AngleX = 0;
                AngleZ = -Pitch;
            }




        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }
        private void PreDraw() {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.PushMatrix();


            GL.Rotate(AngleX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(AngleY, 0.0f, 1.0f, 0.0f);
            GL.Rotate(AngleZ, 0.0f, 0.0f, 1.0f);
            
            GL.Translate(PositionX, PositionY, PositionZ);
        }
        private void PostDraw() {
            GL.PopMatrix();
            SwapBuffers();
        }

    }
}
