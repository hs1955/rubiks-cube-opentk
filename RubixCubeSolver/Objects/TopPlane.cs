using OpenTK;
using System.Collections.Generic;
using System.Xml;

namespace RubixCubeSolver.Objects
{
    class TopPlane : GameObject
    {
        /// This is an array of vertices, each representing a corner of a triangle, which make up the faces of the cube
        /// Using triangles byfar the most efficient, compatible, and up-to-date method for rendering shapes

        /// All vertices which make up each triangle (no repeat vertices)
        private static float[] vertices =
        {
            /// Front Face
            0.5f, 0.0f,  0.5f,      /// Top right
            0.5f, 0.0f,  -0.5f,     /// Bottom right
            -0.5f, 0.0f, -0.5f,     /// Bottom left
            -0.5f, 0.0f, 0.5f,      /// Top left

        };

        private static uint[] indices =
        {  /// Note that we start from 0                                     

            /// Front Face
            1, 0, 3,    /// 1st triangle
            1, 2, 3,    /// 2nd triangle

        };

        /// The default color is red
        private static Vector3 myColor = new Vector3(1.0f, 0.0f, 0.0f); /// R G B

        public TopPlane(Shader shader, float scale = 1.0f, Vector3? position = null, Vector3? color = null, float[] angles = null, float[] invertRot = null, int[] swapAngles = null) : base(vertices, indices, shader, scale, position, color, angles, invertRot, swapAngles)
        {
            /// Set to the color specified, unless it's null, in which case the default color is red
            setColor(color ?? myColor);
        }

    }
}
