using OpenTK;
using System.Collections.Generic;
using System.Linq;

namespace RubixCubeSolver.Objects
{
    class RubiksCube : CompositeGameObject
    {
        List<GameMaster.IGameObject> theObjects = new List<GameMaster.IGameObject>();

        public enum Slices
        {
            Top = 1,
            Bottom,
            
        }

        int[] colors = new int[54];

        public RubiksCube(Shader shader, float scale = 1.0f, Vector3? position = null, float[] angles = null, float[] invertRot = null) : base(scale, position, angles, invertRot)
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

                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, 0.0f, -1.0f)),

                #endregion
                
                #endregion
                
                #region Rubiks Cube Slices
                /// These are the slices that are required for animating the rotations.
                /// These start of invisible
                
                /// 27th Object
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 0.0f)),
                
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),
                
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),
                
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { -90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f)),
                
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, -90.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 90.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f))
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

                theColors.AddRange(rubiksCubePiece.getColors());
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
        public bool RotateSlice(int sliceNumber, int direction, float speed = 0.2f)
        {
            if (!(direction == 1 || direction == -1))
            {
                throw new System.Exception($"Direction must be 1 or -1.\nGiven Direction: {direction}");
            }

            RubiksCubeSlice slice;
            float[] angleInfo = new float[3];
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

            framesPassed = Game.frameTime - prevFrameTime;
            
            float rotationAmount = framesPassed * speed * direction;
            MathHelper.Clamp(rotationAmount, -90.0f, 90.0f);

            if (framesPassed == 0 || System.Math.Abs(rotationAmount) == 90)
            {
                switch (sliceNumber)
                {
                    case 1:
                        slicePieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                        break;

                    case 2:
                        slicePieces = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;

                    case 6:

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
                slice.UpdateColors(newColors);

                slice.setHide(false);

                switch (sliceNumber)
                {
                    case 1:
                        slicePieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                        break;

                    case 2:
                        slicePieces = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;

                    case 6:

                        break;

                    default:
                        throw new System.Exception($"Slice Number must be inbetween 1-6\nGiven Slice Number: {sliceNumber}");
                }

                ObjectsHide(slicePieces, true);
                angleInfo = slice.getAngles();
                storedAngles = (float[])angleInfo.Clone();
            }

            if (System.Math.Abs(rotationAmount) < 90)
            {
                slice.setAngles(new float[] { storedAngles[0] + angleInfo[0], storedAngles[1] + rotationAmount, storedAngles[2] + angleInfo[2] });

                /// Animation NOT Complete
                return false;
            }

            else
            {
                ResetSlices();
              
                int[] newOrderOfPieces = new int[9];

                /// Rotate Pieces by 90 or -90 degrees (by changing their positions in the matrix)
                if (direction == 1)
                {
                    newOrderOfPieces = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
                }
                
                else if (direction == -1)
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

                /// Update Colors
                UpdateColors(getColorsOfPieces(newCubePieces));

                /// Show hidden objects again
                switch (sliceNumber)
                {
                    case 1:
                        ObjectsHide(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, false);
                        break;

                    case 2:
                        ObjectsHide(new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }, false);
                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;

                    case 6:

                        break;

                    default:
                        throw new System.Exception($"Slice Number must be inbetween 1-6\nGiven Slice Number: {sliceNumber}");
                }

                slice.setHide(true);
                slice.setAngles(new float[] { angleInfo[0], storedAngles[1], angleInfo[2] });
                
                /// For next time the function is called
                animationFirstFrame = true;

                /// Animation Complete
                return true;
            }
        }
        public void ResetSlices()
        {
            framesPassed = 0;
        }


    }
}
