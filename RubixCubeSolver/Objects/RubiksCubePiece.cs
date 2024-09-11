using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCubePiece : CompositeGameObject
    {
        static Shader shader = null;

        static List<GameObject> gameObjects = new List<GameObject> { new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)) };

        /// --- ADDITIONAL FIELDS
        //private static Vector3 color = new Vector3(0.1f, 0.1f, 0.1f); //R G B

        public RubiksCubePiece(float scale = 1.0f, Vector3? position = null) : base(gameObjects, scale, position) { }

    }
}
