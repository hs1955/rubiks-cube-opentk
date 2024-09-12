using OpenTK;
using System.Collections.Generic;
using System.Xml;

namespace RubixCubeSolver.Objects
{
    class Plane : GameObject
    {
        /// This is an array of vertices, each representing a corner of a triangle, which make up the faces of the cube
        /// Using triangles byfar the most efficient, compatible, and up-to-date method for rendering shapes

        /// All vertices which make up each triangle (no repeat vertices)
        private static float[] vertices =
        {
            /// Front Face
            0.5f,  0.5f, 0.0f,      /// Top right
            0.5f, -0.5f, 0.0f,      /// Bottom right
            -0.5f, -0.5f, 0.0f,     /// Bottom left
            -0.5f,  0.5f, 0.0f,     /// Top left

            0.0f, 0.0f, 0.0f,       /// Filler Vertices to prevent rendering bugs
            0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f,

        };

        private static uint[] indices =
        {  /// Note that we start from 0                                     

            /// Front Face
            1, 0, 3,    /// 1st triangle
            1, 2, 3,    /// 2nd triangle

            1, 1, 1,    /// Filler Indices
            1, 1, 1,

            1, 1, 1,
            1, 1, 1,

            1, 1, 1,
            1, 1, 1,

        };

        /// The default color is red
        private static Vector3 myColor = new Vector3(1.0f, 0.0f, 0.0f); /// R G B

        public Plane(Shader shader, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null, Vector3? color = null, bool switchYForZ = false) : base(vertices, indices, shader, scale, horizontalAngle, verticalAngle, position, color, switchYForZ)
        {
            /// Set to the color specified, unless it's null, in which case the default color is red
            setColor(color ?? myColor);
        }

    }
}
