using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace OpenTKTest2.Shapes
{  
    class Rect
    {
        public readonly float width;
        public readonly float height;
        public readonly float depth;
        public readonly Color color;

        public readonly Vector3 position;

        public Rect(Vector3 position_,float width_,float height_,float depth_,Color color_) {
            color = color_;
            width = width_;
            height = height_;
            depth = depth_;
            position = position_;
            
        }
        public Vector3[] GetVerts() {
            Vector3[] verts = new Vector3[] {
                new Vector3((0+position.X)      ,(0+position.Y)       ,(0+position.Z)),
                new Vector3((0+position.X)+width,(0+position.Y)       ,(0+position.Z)),
                new Vector3((0+position.X)      ,(0+position.Y)-height,(0+position.Z)),
                new Vector3((0+position.X)+width,(0+position.Y)-height,(0+position.Z)),
                new Vector3((0+position.X)      ,(0+position.Y)       ,(0+position.Z)+depth),
                new Vector3((0+position.X)+width,(0+position.Y)       ,(0+position.Z)+depth),
                new Vector3((0+position.X)      ,(0+position.Y)-height,(0+position.Z)+depth),
                new Vector3((0+position.X)+width,(0+position.Y)-height,(0+position.Z)+depth),
            };
            return verts;
        }
        public int[] GetEdges() {
            int[] edges = new int[] {
                0,1,3,2, //backs
                
                5,1,3,7, //right

                4,5,7,6, //front

                0,4,6,2, //left

                0,1,5,4, //top
                
                2,3,7,6  //bottom

            };
            return edges;

        }
        public void Draw() {
            GL.Color3(color);
            GL.Material(MaterialFace.Front, MaterialParameter.AmbientAndDiffuse, new float[] { 6.0f, 6.0f, 6.0f, 6.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 6.0f, 6.0f, 6.0f, 6.0f });

            GL.Begin(BeginMode.Quads);
            foreach (int edge in GetEdges()) {
                GL.Vertex3(GetVerts()[edge]);
                GL.Normal3(GetVerts()[edge]);
                
                
            }
            GL.End();
           
        }
    }
}
