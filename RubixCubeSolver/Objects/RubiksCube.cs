using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCube : CompositeGameObject
    {
        List<GameMaster.IGameObject> theObjects = new List<GameMaster.IGameObject>();

        public enum Slices
        {
            Top = 1,
            Bottom,
            Right,
            Left,
            Front,
            Back
        }

        int[] colors = new int[54];

        public RubiksCube(Shader shader, float scale = 1.0f, Vector3? position = null, float[] angles = null, float[] invertRot = null) : base(scale, position, angles, invertRot)
        {
            theObjects = new List<GameMaster.IGameObject>()
            {
                #region Rubiks Cube Pieces

                #region White Face

                /// Index: 0
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, 1.0f)),
                
                /// Index: 1
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 1.0f)),

                /// Index: 2
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, 1.0f)),

                /// Index: 3
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, 0.0f)),
                
                /// Index: 4
                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 0.0f)),

                /// Index: 5
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, 0.0f)),

                /// Index: 6
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(-1.0f, 1.0f, -1.0f)),

                /// Index: 7
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, -1.0f)),

                /// Index: 8
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.White, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 1.0f, -1.0f)),
                
                #endregion

                #region Yellow Face

                /// Index: 9
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, -1.0f)),

                /// Index: 10
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, -1.0f)),

                /// Index: 11
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, -1.0f, -1.0f)),

                /// Index: 12
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, 0.0f)),

                /// Index: 13
                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),

                /// Index: 14
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, -1.0f, 0.0f)),

                /// Index: 15
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, 1.0f)),

                /// Index: 16
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 180.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 1.0f)),

                /// Index: 17
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, position: new Vector3(-1.0f, -1.0f, 1.0f), angles: new float[] { 180.0f, 180.0f, 0.0f }),

                #endregion

                #region Pieces inbetween white and yellow faces
                
                /// Index: 18
                //new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 1.0f)),
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 180.0f }, position: new Vector3(-1.0f, 0.0f, 1.0f)),

                /// Index: 19
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),

                /// Index: 20
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, 1.0f)),
                
                /// Index: 21
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 0, 1.0f, angles: new float[] { 0.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f)),

                //new RubiksCubePiece(shader, 0, 0, 0, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 0.0f)),

                /// Index: 22
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 0, 1.0f, angles: new float[] { 0.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                /// Index: 23
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 1.0f, angles: new float[] { 0.0f, 180.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, -1.0f)),

                /// Index: 24
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, 0, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f)),

                /// Index: 25
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, -1.0f)),

                #endregion
                
                #endregion
                
                #region Rubiks Cube Slices
                /// These are the slices that are required for animating the rotations.
                /// These start of invisible
                
                //*
                /// Index: 26
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 0.0f)),
                
                /// Index: 27
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),
                
                /// Index: 28
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, -90.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                /// Index: 29
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 90.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f)),

                /// Index: 30
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),
                
                /// Index: 31
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { -90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f))
                //*/

                #endregion

            };

            /// Hide the slices
            for (int i = theObjects.Count - 6; i < theObjects.Count; i++)
            {
                ((RubiksCubeSlice)theObjects[i]).setHide(true);
            }

            setGameObjects(theObjects);

            getColors();
            
        }

        public int[] getColors()
        {
            List<int> theColors = new List<int>();

            /// For each piece, obtain colors
            for (int k = 0; k < theObjects.Count - 6; k++)
            {
                RubiksCubePiece rubiksCubePiece = (RubiksCubePiece)theObjects[k];

                theColors.AddRange(rubiksCubePiece.getColors());
            }

            colors = theColors.ToArray();

            return colors;

        }

        public int[] getColorsOfPieces(int[] indexes)
        {
            List<int> theColors = new List<int>();

            /// For each piece, obtain colors
            foreach (int index in indexes)
            {
                RubiksCubePiece rubiksCubePiece = (RubiksCubePiece)theObjects[index];

                int[] itsColors = rubiksCubePiece.getColors();

                theColors.AddRange(itsColors);
            }

            int[] piecesColors = theColors.ToArray();

            return piecesColors;

        }

        /// <summary>
        /// Updates the colors, changing the colors of the tiles
        /// </summary>
        public void UpdateColors(int[] colorsIn)
        {
            /// Checks to see if the given data is valid
            if (colorsIn.Length != 54)
            {
                throw new System.Exception($"Incorrect number of colors given. There should be 54, what was given has {colors.Length}");
            }

            int offset = 0;

            for (int k = 0; k < theObjects.Count - 6; k++)
            {
                RubiksCubePiece rubiksCubePiece = (RubiksCubePiece)theObjects[k];
            
                int[] theColors = new int[rubiksCubePiece.getNumberOfColors()];

                for (int i = 0; i < theColors.Length ; i++)
                {
                    theColors[i] = colorsIn[offset + i];
                }

                rubiksCubePiece.UpdateColors(theColors);

                offset += theColors.Length;
            }

        }

        int prevFrameTime = 0;
        int framesPassed;
        float[] storedAngles;
        bool animationFirstFrame = true;
        public bool RotateSlice(int sliceNumber, int direction, float speed = 2.0f)
        {
            if (!(direction == 1 || direction == -1))
            {
                throw new System.Exception($"Direction must be 1 or -1.\nGiven Direction: {direction}");
            }
            speed = MathHelper.Clamp(speed, 0.00001f, 90.0f);

            RubiksCubeSlice slice;
            int[] slicePieces = new int[9];

            switch (sliceNumber)
            {
                case 1:
                    slice = (RubiksCubeSlice)theObjects[26];
                    break;

                case 2:
                    slice = (RubiksCubeSlice)theObjects[27];
                    break;

                case 3:
                    slice = (RubiksCubeSlice)theObjects[28];
                    break;

                case 4:
                    slice = (RubiksCubeSlice)theObjects[29];
                    break;

                case 5:
                    slice = (RubiksCubeSlice)theObjects[30];
                    break;

                case 6:
                    slice = (RubiksCubeSlice)theObjects[31];
                    break;

                default:
                    throw new System.Exception($"Slice Number must be inbetween 1-6\nGiven Slice Number: {sliceNumber}");
            }

            if (animationFirstFrame)
            {                
                prevFrameTime = Game.frameTime;
                animationFirstFrame = false;
            }

            /// A measure of how many frames have passed since the beginning of the rotation
            framesPassed = Game.frameTime - prevFrameTime;
            
            /// The amount to rotate the slice by
            float rotationAmount = framesPassed * speed * direction;
            rotationAmount = MathHelper.Clamp(rotationAmount, -90.0f, 90.0f);

            /// Required for some slices for correct colors and rotation
            int directionCorrection = 1;
            bool leftRightSide = false;
            bool frontBackSide = false;

            if (framesPassed == 0 || System.Math.Abs(rotationAmount) <= 90)
            {
                switch (sliceNumber)
                {
                    case 1:
                        slicePieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                        //leftRightSide = true;
                        //directionCorrection = -1;
                        break;

                    case 2:
                        //slicePieces = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                        slicePieces = new int[] { 11, 10, 9, 14, 13, 12, 17, 16, 15 };
                        break;

                    case 3:
                        //slicePieces = new int[] { 2, 5, 8, 20, 22, 25, 9, 12, 15 };
                        slicePieces = new int[] { 2, 20, 15, 5, 22, 12, 8, 25, 9 };
                        leftRightSide = true;
                        break;
                        
                    case 4:
                        //slicePieces = new int[] { 0, 18, 17, 3, 21, 14, 6, 23, 11 };
                        slicePieces = new int[] { 17, 18, 0, 14, 21, 3, 11, 23, 6 };
                        leftRightSide = true;
                        directionCorrection = -1;

                        //slicePieces = new int[] { 6, 23, 11, 3, 21, 14, 0, 18, 17, };
                        break;

                    case 5:
                        //slicePieces = new int[] { 0, 1, 2, 18, 19, 20, 17, 16, 15 };
                        slicePieces = new int[] { 17, 16, 15, 18, 19, 20, 0, 1, 2 };
                        frontBackSide = true;
                        break;

                    case 6:
                        slicePieces = new int[] { 6, 7, 8, 23, 24, 25, 11, 10, 9 };
                        frontBackSide = true;
                        break;

                    default:
                        throw new System.Exception($"Slice Number must be inbetween 1-6\nGiven Slice Number: {sliceNumber}");
                }
            }

            if (framesPassed == 0)
            {
                /// Fetch the colors
                int[] newColors = getColorsOfPieces(slicePieces);

                /// Update slice accordingly
                slice.UpdateColors(newColors, leftRightSide, frontBackSide);

                slice.setHide(false);

                ObjectsHide(slicePieces, true);
                storedAngles = (float[])slice.getAngles().Clone();
            }

            if (System.Math.Abs(rotationAmount) < 90)
            {
                if (leftRightSide)
                {
                    slice.setAngles(new float[] { storedAngles[0] + rotationAmount, storedAngles[1], storedAngles[2] });
                }

                else
                {
                    slice.setAngles(new float[] { storedAngles[0], storedAngles[1] + rotationAmount, storedAngles[2] });
                }

                /// Animation NOT Complete
                return false;
            }

            else
            {
                /// Reset the slices and cube
                framesPassed = 0;

                /// For next time the function is called
                animationFirstFrame = true;

                int[] newOrderOfPieces = new int[9];

                /// Rotate Pieces by 90 or -90 degrees (by changing their positions in the matrix)
                if (direction * directionCorrection == 1)
                {
                    newOrderOfPieces = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
                }
                
                else if (direction * directionCorrection == -1)
                {
                    newOrderOfPieces = new int[] { 2, 5, 8, 1, 4, 7, 0, 3, 6 };
                }

                /// Get the indices of the old arrangement of pieces (i.e. non of them have moved yet, so just have an array with all their indices in their positions)
                int[] newCubePieces = new int[26];
                for (int i = 0; i < 26; i++)
                {
                    newCubePieces[i] = i;
                }

                /// Get the indexes of the pieces we want, and place them in the new order, and then replace them in the big cube
                for (int i = 0; i < 9; i++)
                {
                    /// Replace the piece of the cube, with the newPiece
                    newCubePieces[slicePieces[i]] = slicePieces[newOrderOfPieces[i]];
                }

                if (leftRightSide)
                {
                    //RearrangePiecesColors(new int[] { slicePieces[0], slicePieces[1], slicePieces[2], slicePieces[3], slicePieces[5], slicePieces[6], slicePieces[7], slicePieces[8] }, false, true);

                    RearrangePiecesColors(new int[] { slicePieces[1], slicePieces[3], slicePieces[5], slicePieces[7] }, false, true);
                    RearrangePiecesColors(new int[] { slicePieces[0], slicePieces[8] }, false, true);
                    RearrangePiecesColors(new int[] { slicePieces[0], slicePieces[2], slicePieces[6], slicePieces[8] }, true, false);
                    RearrangePiecesColors(new int[] { slicePieces[2], slicePieces[6] }, false, true);
                }

                if (frontBackSide)
                {
                    RearrangePiecesColors(new int[] { slicePieces[2], slicePieces[6] }, false, true);
                    RearrangePiecesColors(new int[] { slicePieces[0], slicePieces[2], slicePieces[6], slicePieces[8] }, true, false);
                    RearrangePiecesColors(new int[] { slicePieces[0], slicePieces[8] }, false, true);
                }

                /// Update Colors
                UpdateColors(getColorsOfPieces(newCubePieces));

                /// Show hidden objects again
                ObjectsHide(slicePieces, false);

                slice.setHide(true);
                slice.setAngles(new float[] { storedAngles[0], storedAngles[1], storedAngles[2] });

                /// Animation Complete
                return true;
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
