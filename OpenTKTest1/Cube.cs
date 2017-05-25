using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTKTest1
{
    class Cube
    {
        private int[] indicedata;
        private Vector3[] vertdata;
        private float size;
       

        public Cube(float size_)
        {
            size = size_;
        }

        public void Draw(float x,float y,float z) {
            vertdata = new Vector3[] {
                new Vector3((-0.5f*size)+x, (-0.5f*size)+y,  (-0.5f*size)+z),
                new Vector3((0.5f*size)+x, (-0.5f*size)+y,  (-0.5f*size)+z),
                new Vector3((0.5f*size)+x, (0.5f*size)+y,  (-0.5f*size)+z),
                new Vector3((-0.5f*size)+x, (0.5f*size)+y,  (-0.5f*size)+z),
                new Vector3((-0.5f*size)+x, (-0.5f*size)+y,  (0.5f*size)+z),
                new Vector3((0.5f*size)+x, (-0.5f*size)+y,  (0.5f*size)+z),
                new Vector3((0.5f*size)+x, (0.5f*size)+y,  (0.5f*size)+z),
                new Vector3((-0.5f*size)+x, (0.5f*size)+y,  (0.5f*size)+z),
            };
            indicedata = new int[]{
                //front
                0, 7, 3,
                0, 4, 7,
                //back
                1, 2, 6,
                6, 5, 1,
                //left
                0, 2, 1,
                0, 3, 2,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //bottom
                0, 1, 5,
                0, 5, 4
            };
            GL.Begin(BeginMode.Lines);
            foreach (int ind in indicedata) {
                GL.Vertex3(vertdata[ind]);
                if (ind == 0) {
                    GL.Color3(System.Drawing.Color.AliceBlue);
                }
                if (ind == 1)
                {
                    GL.Color3(System.Drawing.Color.Black);
                }
                if (ind == 2)
                {
                    GL.Color3(System.Drawing.Color.Chocolate);
                }
                else {
                    GL.Color3(System.Drawing.Color.Black);
                }
            }
            GL.End();


        }
    }
}
