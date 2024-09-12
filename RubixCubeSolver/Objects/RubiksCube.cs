using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCube : CompositeGameObject
    {
        List<GameMaster.IGameObject> theObjects;

        public RubiksCube(Shader shader, float scale = 1.0f, Vector3? position = null, float[] angles = null, float[] invertRot = null) : base(scale, position, angles)
        {
            theObjects = new List<GameMaster.IGameObject>()
            {
                #region Rubiks Cube Pieces

                #region White Face
                
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, -1.0f)),
                
                #endregion

                #region Yellow Face

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, position: new Vector3(-1.0f, -1.0f, 1.0f), angles: new float[] { 180.0f, 180.0f, 0.0f }),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 180.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, -1.0f)),

                #endregion

                #region Pieces inbetween white and yellow faces
                
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 0, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f)),

                //new RubiksCubePiece(shader, 0, 0, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 0, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, 0, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, -1.0f))

                #endregion
                
                #endregion
                
                #region Rubiks Cube Slices
                /// These are the slices that are required for animating the rotations.
                /// These start of invisible
                
                /// 27th Object
                /*
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { -90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, -90.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 90.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f))
                //*/
                #endregion

            };

            setGameObjects(theObjects);

        }

    }
}
