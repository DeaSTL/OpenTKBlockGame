using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace OpenTKTest1
{
    class MeshWorld
    {
        private float[,] vertecies;
        public int width, height;

        public MeshWorld(int width_,int height_) {
            width = width_;
            height = height_;
            vertecies = Generate(width,height);


        }
        public void Draw() {
            
            for (int i = 0; i < width; i++) {
                for (int j = width-1; j > 0; j--)
                {
                    //GL.Vertex3();
                    
                    GL.Begin(BeginMode.Quads);
                    GL.Color3(Color.Black);
                    GL.Vertex3(0+i, (float)Math.Floor((vertecies[i, j] * 30)) - 10, 0+j);
                    GL.Color3(Color.White);
                    GL.Vertex3(1+i, (float)Math.Floor((vertecies[i, j] * 30)) - 10, 0+j);
                    
                    GL.Vertex3(1+i, (float)Math.Floor((vertecies[i, j] * 30)) - 10, 1+j);
                    GL.Vertex3(0 + i, (float)Math.Floor((vertecies[i, j] * 30)) - 10, 1 + j);
                    GL.End();

                }
            }
            
        }

        private float[,] Generate(int width,int height) {
            FastNoise myNoise = new FastNoise(); // Create a FastNoise object
            myNoise.SetNoiseType(FastNoise.NoiseType.Perlin); // Set the desired noise type
            myNoise.SetSeed(new Random().Next());
            float[,] heightMap = new float[width, height]; // 2D heightmap to create terrain

            for (int x = 0; x< width; x++)
            {
                for (int y = 0; y< height; y++)
                {
                    heightMap[x,y] = myNoise.GetNoise(x, y);
                    //Console.WriteLine(myNoise.GetNoise(x, y)*10);
                }
            }
         
            return heightMap;
        }
    }
}
