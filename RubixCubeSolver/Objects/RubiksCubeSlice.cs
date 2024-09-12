using OpenTK;
using System.Collections.Generic;
using System.Linq;

namespace RubixCubeSolver.Objects
{
    class RubiksCubeSlice : CompositeGameObject
    {
        List<GameMaster.IGameObject> theObjects;

        int[] colors = new int[21];

        public RubiksCubeSlice(Shader shader, float scale = 1.0f, Vector3? position = null, float[] angles = null, float[] invertRot = null) : base(scale, position, angles, invertRot)
        {
            theObjects = new List<GameMaster.IGameObject>()
            {
                /// White Face
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, 1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f)),

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, -1.0f)),
                //*/
            };

            setGameObjects(theObjects);

        }

        public int[] getColors()
        {
            List<int> theColors = new List<int>();

            /// For each piece, obtain colors
            for (int k = 0; k < theObjects.Count; k++)
            {
                RubiksCubePiece rubiksCubePiece = (RubiksCubePiece)theObjects[k];

                theColors.AddRange(rubiksCubePiece.getColors());
            }

            colors = theColors.ToArray();

            return colors;
        }

        /// <summary>
        /// Updates the colors, changing the colors of the tiles
        /// </summary>
        public void UpdateColors(int[] colors, bool leftRightSide = false, bool frontBackSide = false)
        {
            //*
            if (colors.Length != 21)
            {
                throw new System.Exception($"Incorrect number of colors given. There should be 21, what was given has {colors.Length}");
            }
            //*/

            int offset = 0;

            foreach (RubiksCubePiece rubiksCubePiece in theObjects)
            {
                int[] theColors = new int[rubiksCubePiece.getNumberOfColors()];

                for (int i = 0; i < theColors.Length; i++)
                {
                    theColors[i] = colors[offset + i];
                }

                rubiksCubePiece.UpdateColors(theColors);

                offset += theColors.Length;

            }

            if (leftRightSide)
            {
                //*
                RearrangePiecesColors(new int[] { 3, 5 }, false, true);
                RearrangePiecesColors(new int[] { 0, 8 }, true, true);

                RearrangePiecesColors(new int[] { 2, 6 }, false, true);
                RearrangePiecesColors(new int[] { 2, 6 }, true, false);
                //*/
            }

            else if (frontBackSide)
            {
                RearrangePiecesColors(new int[] { 1, 3, 5, 7 }, false, true);

                RearrangePiecesColors(new int[] { 2, 6 }, true, true);

                RearrangePiecesColors(new int[] { 0, 8 }, false, true);
                RearrangePiecesColors(new int[] { 0, 8 }, true, false);
            }

        }

        void RearrangePiecesColors(int[] pieceIndexes, bool swapFrontWithRightTiles = false, bool swapFrontWithTopTiles = false)
        {
            foreach (int index in pieceIndexes)
            {
                RearrangePieceColors(index, swapFrontWithRightTiles, swapFrontWithTopTiles);
            }
        }
        void RearrangePieceColors(int pieceIndex, bool swapFrontWithRightTiles = false, bool swapFrontWithTopTiles = false)
        {
            int[] theColors;
            theColors = (int[])((RubiksCubePiece)theObjects[pieceIndex]).getColors().Clone();

            if (swapFrontWithRightTiles)
            {
                int temp = theColors[0];
                theColors[0] = theColors[1];
                theColors[1] = temp;
            }

            if (swapFrontWithTopTiles)
            {
                int temp = theColors[0];
                theColors[0] = theColors[theColors.Length - 1];
                theColors[theColors.Length - 1] = temp;
            }

            ((RubiksCubePiece)theObjects[pieceIndex]).UpdateColors(theColors);
        }

    }

}
