using OpenTK;

namespace RubixCubeSolver.Objects
{
    class Cube : GameObject
    {
        /// This is an array of vertices, each representing a corner of a triangle, which make up the faces of the cube
        /// Using triangles byfar the most efficient, compatible, and up-to-date method for rendering shapes

        /// All vertices which make up each triangle (no repeat vertices)
        private static float[] vertices =
            {
                /// Front Face
                0.5f,  0.5f, 0.5f,      /// Top right
                0.5f, -0.5f, 0.5f,      /// Bottom right
                -0.5f, -0.5f, 0.5f,     /// Bottom left
                -0.5f,  0.5f, 0.5f,     /// Top left

                /// Back Face
                0.5f,  0.5f, -0.5f,     /// Top right
                0.5f, -0.5f, -0.5f,     /// Bottom right
                -0.5f, -0.5f, -0.5f,    /// Bottom left
                -0.5f,  0.5f, -0.5f,    /// Top left

            };

        /*
        /// The vertices are now used to draw cubes.
        /// For this example, we aren't using texture coordinates.
        /// You could add textures vertices with lighting here
        
        private new readonly float[] vertices =
        {
            // Position
            -0.5f, -0.5f, -0.5f, // Front face
             0.5f, -0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f, -0.5f,  0.5f, // Back face
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,

            -0.5f,  0.5f,  0.5f, // Left face
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,

             0.5f,  0.5f,  0.5f, // Right face
             0.5f,  0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,

            -0.5f, -0.5f, -0.5f, // Bottom face
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f,  0.5f, -0.5f, // Top face
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f
        };
        //*/

        /*Each set of 3 represent which of the above vertices make up a triangle to be drawn
        *        7 ________
        *         /       /|4
        *        /|      / |
        *    3  /_|6____/__|5
        *      | /      |0 /
        *      |/       | /
        *      |________|/
        *     2          1
        */

        private static uint[] indices = 
            {  /// Note that we start from 0                                     

                /// Front Face
                1, 0, 3,    /// 1st triangle
                1, 2, 3,    /// 2nd triangle

                /// Back Face
                5, 4, 7,
                5, 6, 7,
                                                     
                /// Right Face
                0, 1, 5,
                0, 4, 5,

                /// Left Face
                3, 2, 6,
                3, 7, 6,

                /// Top Face
                0, 3, 7,
                0, 4, 7,

                /// Bottom Face
                1, 2, 6,
                1, 5, 6

            };

        /// --- ADDITIONAL FIELDS
        //private static Vector3 color = new Vector3(1.0f, 0.9f, 0.0f); //R G B

        public Cube(float scale = 1.0f, Vector3? position = null, Vector3? color = null) : base(vertices, indices, scale, position, color) { }

    }
}
