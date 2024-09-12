using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCube : CompositeGameObject
    {
        /// No Tile (Shader shader, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null, Vector3? color = null) : base(vertices, indices, shader, scale, horizontalAngle, verticalAngle, position, color)
        public RubiksCube(Shader shader, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>()
            {
                //new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 1.0f, 1.0f)),
                //new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 1.0f, 1.0f)),

                /// White Face
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: -90.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: -90.0f, verticalAngle: 90.0f, position: new Vector3(0.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: 0.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 180.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 0.0f, verticalAngle: 90.0f, position: new Vector3(0.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: 0.0f, verticalAngle: 90.0f, position: new Vector3(1.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 90.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Orange, 1.0f, horizontalAngle: 90.0f, verticalAngle: 90.0f, position: new Vector3(0.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.White, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: 90.0f, verticalAngle: 90.0f, position: new Vector3(1.0f, 1.0f, 1.0f)),

                /// Yellow Face
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: -90.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: -90.0f, verticalAngle: 90.0f, position: new Vector3(0.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: 0.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 180.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 0.0f, verticalAngle: 90.0f, position: new Vector3(0.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: 0.0f, verticalAngle: 90.0f, position: new Vector3(1.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 90.0f, verticalAngle: 90.0f, position: new Vector3(-1.0f, -1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Orange, 1.0f, horizontalAngle: 90.0f, verticalAngle: 90.0f, position: new Vector3(0.0f, -1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Yellow, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: 90.0f, verticalAngle: 90.0f, position: new Vector3(1.0f, -1.0f, 1.0f)),

                /// Pieces inbetween white and yellow
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 180.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: 180.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, 1.0f, horizontalAngle: -90.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 90.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: -90.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 1.0f, horizontalAngle: 0.0f, verticalAngle: 180.0f, position: new Vector3(-1.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 0.0f, 1.0f)),

                /// KEEP IN MIND, MAY NOT HAVE TO DEFINE ANGLES AT ALL HERE, BUT INSTEAD 6 FACE COLORS (May be less efficient)

            });

        }

    }
}
