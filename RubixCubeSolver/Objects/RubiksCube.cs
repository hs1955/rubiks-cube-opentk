using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCube : CompositeGameObject
    {
        public RubiksCube(Shader shader, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>()
            {
                /// White Face
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 180.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 180.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: -90.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 1.0f, -1.0f)),
                
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 90.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: -90.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 90.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 1.0f, 1.0f)),

                /// Yellow Face
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 90.0f, verticalAngle: 180.0f, position: new Vector3(-1.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 0.0f, verticalAngle: 180.0f, position: new Vector3(0.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 0.0f, verticalAngle: 180.0f, position: new Vector3(1.0f, -1.0f, -1.0f)),
                
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 90.0f, verticalAngle: 180.0f, position: new Vector3(-1.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 0.0f, verticalAngle: 180.0f, position: new Vector3(0.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: -90.0f, verticalAngle: 180.0f, position: new Vector3(1.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 180.0f, verticalAngle: 180.0f, position: new Vector3(-1.0f, -1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: 180.0f, verticalAngle: 180.0f, position: new Vector3(0.0f, -1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, horizontalAngle: -90.0f, verticalAngle: 180.0f, position: new Vector3(1.0f, -1.0f, 1.0f)),

                /// Pieces inbetween white and yellow faces
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, horizontalAngle: 180.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, 0, 1.0f, horizontalAngle: 180.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, 1.0f, horizontalAngle: -90.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 0, 1.0f, horizontalAngle: 90.0f, verticalAngle: 0.0f, position: new Vector3(-1.0f, 0.0f, 0.0f)),

                //new RubiksCubePiece(shader, 0, 0, 0, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 0, 1.0f, horizontalAngle: -90.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, horizontalAngle: 180.0f, verticalAngle: 180.0f, position: new Vector3(-1.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, 0, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(0.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 1.0f, horizontalAngle: 0.0f, verticalAngle: 0.0f, position: new Vector3(1.0f, 0.0f, 1.0f)),

                /// KEEP IN MIND, MAY NOT HAVE TO DEFINE ANGLES AT ALL HERE, BUT INSTEAD 6 FACE COLORS (May be less efficient)

            });

        }

    }
}
