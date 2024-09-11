using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixCubeSolver.Objects
{
    class Cube
    {
        //------PROPERTIES
        //This is an array of vertices, each representing a corner of a triangle, which make up the faces of the cube
        //Using triangles byfar the most efficient, compatible, and up-to-date method for rendering shapes

        //All vertices which make up each triangle (no repeat vertices)
        float[] vertices = 
            {
                //Front Face
                0.5f,  0.5f, 0.5f,  // top right
                0.5f, -0.5f, 0.5f,  // bottom right
                -0.5f, -0.5f, 0.5f,  // bottom left
                -0.5f,  0.5f, 0.5f,  // top left

                //Back Face
                0.5f,  0.5f, -0.5f,  // top right
                0.5f, -0.5f, -0.5f,  // bottom right
                -0.5f, -0.5f, -0.5f,  // bottom left
                -0.5f,  0.5f, -0.5f,  // top left

            };

        public float[] getVertices()
        {
            return vertices;
        }
        //No set method: Variable Security

        //Each set of 3 represent which of the above vertices make up a triangle to be drawn
        uint[] indices = 
            {  // note that we start from 0

                //Front Face
                1, 0, 3,    // first triangle
                1, 2, 3,    // second triangle

                //Back Face
                5, 4, 7,
                5, 6, 7,

                //Right Face
                0, 1, 5,
                0, 4, 5,

                //Left Face
                3, 2, 6,
                3, 7, 6,

                //Top Face
                0, 3, 7,
                0, 4, 7,

                //Bottom Face
                1, 2, 6,
                1, 5, 6

            };

        public uint[] getIndices()
        {
            return indices;
        }
        //No set method


        //------METHODS
        public float[] getVariedVertices(double sensitivity = 1)
        {
            Random rnd = new Random();
            float[] variedVertices = new float[vertices.Length];

            if (sensitivity < 1)
                sensitivity = 1;

            for (int i = 0; i < vertices.Length; i++)
            {
                variedVertices[i] = vertices[i] + Convert.ToSingle((rnd.NextDouble() - 0.5f) * 2 / sensitivity);
            }

            return variedVertices;
        }

    }
}
