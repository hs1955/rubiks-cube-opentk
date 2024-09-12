using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class CompositeTest : CompositeGameObject
    {
        public CompositeTest(Shader shader, int version, float scale = 1.0f, Vector3? position = null, float[] angles = null, float[] invertRot = null) : base(scale, position, angles)
        {
            switch (version)
            {
                case 1:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)),

                        new Cube(shader, scale: 1.3f, position: new Vector3(1.0f), color: new Vector3(1f, 0.1f, 0.1f))

                    });
                    break;

                case 2:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new Plane(shader, color: new Vector3(0.96f)),
                        new Plane(shader, scale: 1.3f, position: new Vector3(-1.0f, 1.0f, 1.0f), color: new Vector3(0.9f, 0.0f, 0.0f)),
                        new Plane(shader, scale: 0.8f, position: new Vector3(0.0f, 1.0f, 0.0f), color: new Vector3(0.0f, 0.0f, 0.9f)),
                        new Plane(shader, scale: 0.5f, position: new Vector3(1.0f, 1.0f, -1.0f), color: new Vector3(1.0f, 0.3f, 0.0f)),
                        new Plane(shader, scale: 1.1f, position: new Vector3(-1.0f, -1.0f, 1.0f), color: new Vector3(0.0f, 0.5f, 0.0f)),
                        new Plane(shader, scale: 0.6f, position: new Vector3(-1.0f, 0.0f, -1.0f), color: new Vector3(1.0f, 0.7f, 0.0f))

                    });
                    break;

                case 3:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new Plane(shader, color: new Vector3(0.96f)),
                        new Plane(shader, scale: 1.3f, position: new Vector3(-1.0f, 1.0f, 0.0f), color: new Vector3(0.9f, 0.0f, 0.0f)),
                        new Plane(shader, scale: 0.8f, position: new Vector3(0.0f, 1.0f, 0.0f), color: new Vector3(0.0f, 0.0f, 0.9f)),
                        new Plane(shader, scale: 0.5f, position: new Vector3(1.0f, 1.0f, 0.0f), color: new Vector3(1.0f, 0.3f, 0.0f)),
                        new Plane(shader, scale: 1.1f, position: new Vector3(-1.0f, -1.0f, 0.0f), color: new Vector3(0.0f, 0.5f, 0.0f)),
                        new Plane(shader, scale: 0.6f, position: new Vector3(-1.0f, 0.0f, 0.0f), color: new Vector3(1.0f, 0.7f, 0.0f))

                    });
                    break;
                         
                case 4:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new RubiksCube(shader, 1.0f, position: new Vector3(-2.0f, 0.0f, 0.0f)),
                        new RubiksCube(shader, 1.0f, position: new Vector3(2.0f, 0.0f, 0.0f))

                    }); 
                    break;

                case 5:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, 1.0f)),

                        new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 1.0f))

                    });
                    break;

                case 6:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, 1.0f)),

                        new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 1.0f)),

                        new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, 1.0f))

                    });
                    break;
            }

        }

    }
}
