using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCube : CompositeGameObject
    {
        List<GameMaster.IGameObject> theObjects;

        int[] colors;

        readonly List<int[]> correctColors = new List<int[]>()
        { 
            new int[] { 5, 2, 1 }, 
            new int[] { 2, 1 }, 
            new int[] { 2, 3, 1 }, 
            new int[] { 5, 1 }, 
            new int[] { 1 }, 
            new int[] { 3, 1 }, 
            new int[] { 4, 5, 1 }, 
            new int[] { 4, 1 }, 
            new int[] { 3, 4, 1 }, 
            new int[] { 2, 5, 6 }, 
            new int[] { 2, 6 }, 
            new int[] { 3, 2, 6 }, 
            new int[] { 5, 6 }, 
            new int[] { 6 }, 
            new int[] { 3, 6 }, 
            new int[] { 5, 4, 6 }, 
            new int[] { 4, 6 }, 
            new int[] { 4, 3, 6 }, 
            new int[] { 2, 5 }, 
            new int[] { 2 }, 
            new int[] { 2, 3 }, 
            new int[] { 5 }, 
            new int[] { 3 }, 
            new int[] { 4, 5 }, 
            new int[] { 4 }, 
            new int[] { 4, 3 } 
        };

        public enum Slices
        {
            Top = 1,
            Bottom,
            Front,
            Left,
            Back,
            Right
            
        }

        public enum Turns
        {
            U = 1,
            Ur,
            D,
            Dr,
            R,
            Rr,
            L,
            Lr,
            F,
            Fr,
            B,
            Br
        }

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
                
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, 1.0f, angles: new float[] { 180.0f, 180.0f, 0.0f }, position: new Vector3(-1.0f, 0.0f, 1.0f)),

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
                #region .
                /*
                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 1.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, 1.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { -90.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, 0.0f, -1.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, -90.0f }, position: new Vector3(1.0f, 0.0f, 0.0f)),

                new RubiksCubeSlice(shader, 1.0f, angles: new float[] { 0.0f, 0.0f, 90.0f }, position: new Vector3(-1.0f, 0.0f, 0.0f))
                //*/
                #endregion

                //*
                new RubiksCubeSlice(shader, 1.0f, position: new Vector3(0.0f, 1.0f, 0.0f), angles: new float[] { 0.0f, 0.0f, 0.0f }, true),

                new RubiksCubeSlice(shader, 1.0f, position: new Vector3(0.0f, -1.0f, 0.0f), angles: new float[] { 180.0f, 0.0f, 0.0f }, true),

                new RubiksCubeSlice(shader, 1.0f, position: new Vector3(0.0f, 0.0f, 1.0f), angles: new float[] { 90.0f, 0.0f, 0.0f }, true),

                new RubiksCubeSlice(shader, 1.0f, position: new Vector3(0.0f, 0.0f, -1.0f), angles: new float[] { -90.0f, 0.0f, 0.0f }, true),

                new RubiksCubeSlice(shader, 1.0f, position: new Vector3(1.0f, 0.0f, 0.0f), angles: new float[] { 90.0f, 0.0f, -90.0f }, true),

                new RubiksCubeSlice(shader, 1.0f, position: new Vector3(-1.0f, 0.0f, 0.0f), angles: new float[] { -90.0f, 0.0f, 90.0f }, true)
                //*/
                #endregion

            };

            setGameObjects(theObjects);

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

                for (int i = 0; i < theColors.Length; i++)
                {
                    theColors[i] = colorsIn[offset + i];
                }

                rubiksCubePiece.UpdateColors(theColors);

                offset += theColors.Length;
            }

        }

        int prevFrameTime = 0;
        float[] storedAngles;
        bool animationFirstFrame = true;
        /// <summary>
        /// Rotate a slice of the Rubik's Cube
        /// </summary>
        /// <param name="sliceNumber">There are 6 possible faces to turn</param>
        /// <param name="direction">Either clockwise or anticlockwise (not same for every slice)</param>
        /// <param name="speed">Animation speed</param>
        public bool RotateSlice(int sliceNumber, int direction, float speed = 1.0f)
        {
            /// The slice
            RubiksCubeSlice slice;

            /// The indicies of the pieces that will be hidden
            int[] slicePieces;

            /// A property of slices to indicate if they are on the left or right side of the cube
            bool leftright = false;
            bool frontback = false;
            int directionCorrection = 1;

            /// Select the correct slice and pieces of question (using sliceNumber)
            /// Top = 1, 
            /// Bottom = 2,
            /// Front = 3,
            /// Left = 4,
            /// Back = 5,
            /// Right = 6
            switch (sliceNumber)
            {
                case 1:
                    slice = (RubiksCubeSlice)theObjects[26];
                    slicePieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                    break;

                case 2:
                    //slicePieces = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }; // Original Logical Order, from top left to right, then down and repeat
                    slicePieces = new int[] { 15, 16, 17, 12, 13, 14, 9, 10, 11 };
                    slice = (RubiksCubeSlice)theObjects[27];
                    break;

                case 3:
                    //slicePieces = new int[] { 0, 1, 2, 18, 19, 20, 9, 10, 11 };
                    slicePieces = new int[] { 9, 10, 11, 18, 19, 20, 0, 1, 2 };
                    slice = (RubiksCubeSlice)theObjects[28];
                    frontback = true;
                    break;

                case 5:
                    //slicePieces = new int[] { 6, 7, 8, 23, 24, 25, 15, 16, 17 };
                    slicePieces = new int[] { 6, 7, 8, 23, 24, 25, 15, 16, 17 };
                    slice = (RubiksCubeSlice)theObjects[29];
                    frontback = true;
                    break;

                case 6:
                    //slicePieces = new int[] { 2, 5, 8, 20, 22, 25, 11, 14, 17 };
                    slicePieces = new int[] { 11, 14, 17, 20, 22, 25, 2, 5, 8 };
                    slice = (RubiksCubeSlice)theObjects[30];
                    leftright = true;
                    break;

                case 4:
                    slicePieces = new int[] { 0, 3, 6, 18, 21, 23, 9, 12, 15 };
                    slice = (RubiksCubeSlice)theObjects[31];
                    leftright = true;
                    directionCorrection = -1;
                    break;

                default:
                    throw new System.Exception($"Slice Number must be inbetween 1-6\nGiven Slice Number: {sliceNumber}");
            }

            /// If this is the first call of the function, and no animation has been playing
            if (animationFirstFrame)
            {
                /// Set the colors of the slice to the pieces of the cube its replacing
                slice.UpdateColors(getColorsOfPieces(slicePieces));

                /// Rearrange the colors on the frontback slice
                if (frontback)
                {
                    slice.RearrangePieceColors(new int[] { 1, 2, 3, 5, 6, 7 }, true, false);
                    slice.RearrangePieceColors(new int[] { 0, 2, 6, 8 }, false, true);
                    slice.RearrangePieceColors(new int[] { 0, 8 }, true, false);
                }

                else if (leftright)
                {
                    //*
                    slice.RearrangePieceColors(new int[] { 2, 6 }, true, false);
                    slice.RearrangePieceColors(new int[] { 0, 1, 2, 6, 7, 8 }, false, true);
                    slice.RearrangePieceColors(new int[] { 0, 8 }, true, false);
                    //*/
                }

                /// Hide each piece in slicePieces
                foreach (int index in slicePieces)
                {
                    ((RubiksCubePiece)theObjects[index]).setHide(true);
                }

                /// Store initial values of angles of slice for resetting the slice
                storedAngles = (float[])slice.getAngles().Clone();

                /// Show the slice
                slice.setHide(false);

                /// Set the time at which this function was first activated, to allow for calculation of the amount of time passed
                prevFrameTime = Game.frameTime;

                /// Prevent this if statement from occuring until the next time a slice needs to be rotated
                animationFirstFrame = false;
            }

            /// The amount of time passed, in frames
            int framesPassed = Game.frameTime - prevFrameTime;

            /// The amount to rotate the slice by, then clamp the values
            float rotationAmount = framesPassed * speed * direction;
            rotationAmount = MathHelper.Clamp(rotationAmount, -90.0f, 90.0f);

            /// If the slice has not rotated by 90 degrees yet
            if (System.Math.Abs(rotationAmount) < 90)
            {
                /// Rotate the slice

                if (leftright)
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
                /// Save colour information of each piece to a temporary array.
                int[][] colorInfo = new int[9][];
                for (int i = 0; i < slicePieces.Length; i++)
                {
                    RubiksCubePiece piece = (RubiksCubePiece)theObjects[slicePieces[i]];
                    //colorInfo[i] = piece.getColors();
                    colorInfo[i] = (int[])piece.getColors().Clone();
                }

                /// This array holds the indexes of pieces rotated by 90 degrees (if they were initially in order)
                /// Corner pieces:  0, 2, 6, 8  =>  6, 0, 8, 2
                /// Edge Pieces:    1, 3, 5, 7  =>  3, 7, 1, 5
                /// Centre Piece:       4       =>      4
                int[] rotateIndexes;

                switch (direction * directionCorrection)
                {
                    /// Rotation by 90 degrees clockwise
                    case 1:
                        rotateIndexes = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
                        break;

                    /// Rotation by 90 degrees Anti-Clockwise
                    case -1:
                        rotateIndexes = new int[] { 2, 5, 8, 1, 4, 7, 0, 3, 6 };
                        break;

                    default:
                        throw new System.Exception($"Invalid Direction Given: direction = {direction}");
                }
                

                /// Assign New Colors to the hidden pieces
                for (int i = 0; i < 9; i++)
                {
                    ((RubiksCubePiece)theObjects[slicePieces[i]]).UpdateColors(colorInfo[rotateIndexes[i]]);
                }

                /// Rearrange colors on the pieces on the cube
                if (frontback)
                {
                    RearrangePieceColors(new int[] { slicePieces[0], slicePieces[2], slicePieces[6], slicePieces[8] }, true, true);
                    RearrangePieceColors(new int[] { slicePieces[0], slicePieces[8] }, true, true);
                }

                else if (leftright)
                {
                    RearrangePieceColors(new int[] { slicePieces[0], slicePieces[1], slicePieces[3], slicePieces[5], slicePieces[7], slicePieces[8] }, false, true);
                    RearrangePieceColors(new int[] { slicePieces[2], slicePieces[6] }, true, true);
                    RearrangePieceColors(new int[] { slicePieces[0], slicePieces[8] }, true, false);
                }

                /// Show the cube and hide the slices 
                ShowCube();

                /// Reset angles back to initial values
                slice.setAngles(new float[] { storedAngles[0], storedAngles[1], storedAngles[2] });

                /// For next time the function is called
                animationFirstFrame = true;

                #region .
                /*
                 
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

                //*/
                #endregion

                /// Animation Complete
                return true;
            }

            #region .
            /*
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

                //*/
            #endregion
        }

        /// This function is not really an animation, but more of a translation
        /// The animation it uses is above
        public bool TurnSlice(int turn, float speed = 1.0f)
        {
            bool animationDone;

            switch (turn)
            {
                case (int)Turns.U:
                    animationDone = RotateSlice((int)Slices.Top, -1, speed);
                    break;

                case (int)Turns.Ur:
                    animationDone = RotateSlice((int)Slices.Top, 1, speed);
                    break;

                case (int)Turns.D:
                    animationDone = RotateSlice((int)Slices.Bottom, -1, speed);
                    break;

                case (int)Turns.Dr:
                    animationDone = RotateSlice((int)Slices.Bottom, 1, speed);
                    break;

                case (int)Turns.R:
                    animationDone = RotateSlice((int)Slices.Right, -1, speed);
                    break;

                case (int)Turns.Rr:
                    animationDone = RotateSlice((int)Slices.Right, 1, speed);
                    break;

                case (int)Turns.L:
                    animationDone = RotateSlice((int)Slices.Left, 1, speed);
                    break;

                case (int)Turns.Lr:
                    animationDone = RotateSlice((int)Slices.Left, -1, speed);
                    break;

                case (int)Turns.F:
                    animationDone = RotateSlice((int)Slices.Front, -1, speed);
                    break;

                case (int)Turns.Fr:
                    animationDone = RotateSlice((int)Slices.Front, 1, speed);
                    break;

                case (int)Turns.B:
                    animationDone = RotateSlice((int)Slices.Back, -1, speed);
                    break;

                case (int)Turns.Br:
                    animationDone = RotateSlice((int)Slices.Back, 1, speed);
                    break;

                default:
                    throw new System.Exception($"The given turn was not valid. Given Turn: {turn}");
            }

            return animationDone;
        }

        int currentIndex1 = 0;
        bool aSliceTurned = true;
        public bool ExcecuteTurns(int[] turnsIn, float speed = 1.0f)
        {
            /// Do not run anything if there are no turns
            if (turnsIn == null)
            {
                return true;
            }

            #region .
            /// Initial State
            /*
            if (currentIndex1 == -1)
            {
                currentIndex1 = 0;
            }
            //*/
            #endregion

            /// While the function has not completed the last turn
            if (currentIndex1 != turnsIn.Length)
            {
                int turn = turnsIn[currentIndex1];

                if (turn == 0)
                {
                    throw new System.Exception("Invalid Turn. Given turn: 0");
                }

                aSliceTurned = TurnSlice(turn, speed);

                /// Once the slice has turned
                if (aSliceTurned)
                {
                    currentIndex1++;
                }

                /// Animation NOT Complete
                return false;

            }

            /// The function has completed all the turns
            else
            {
                /// Reset Index
                currentIndex1 = 0;

                /// Animation Complete
                return true;
            }
        }

        public int[] Algorithm(int algorithmNumber)
        {
            int[] algorithmTurns;
            int defaultFaceWhichFace = (int)Slices.Front;

            switch (algorithmNumber)
            {
                case 1:
                    algorithmTurns = new int[] { (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr, (int)Turns.Ur, (int)Turns.Fr, (int)Turns.U, (int)Turns.F };
                    break;

                case 2:
                    algorithmTurns = new int[] { (int)Turns.Ur, (int)Turns.Fr, (int)Turns.U, (int)Turns.F, (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr };
                    defaultFaceWhichFace = (int)Slices.Left;
                    break;

                case 3:
                    algorithmTurns = new int[] { (int)Turns.F, (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.Ur, (int)Turns.Fr };
                    break;

                case 4:
                    algorithmTurns = new int[] { (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.U, (int)Turns.R, (int)Turns.U, (int)Turns.U, (int)Turns.Rr };
                    break;

                case 5:
                    algorithmTurns = new int[] { (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Lr, (int)Turns.U, (int)Turns.Rr, (int)Turns.Ur, (int)Turns.L };
                    break;

                case 6:
                    algorithmTurns = new int[] { (int)Turns.Rr, (int)Turns.D, (int)Turns.R, (int)Turns.Dr };
                    break;

                default:
                    throw new System.Exception($" Invalid Algorithm Number. Algorithm Number Given: {algorithmNumber} ");
            }

            algorithmTurns = TranslateTurns(algorithmTurns, defaultFaceWhichFace);

            return algorithmTurns;

        }

        /// This is really just setting ExcecuteTurns, with random turns
        int[] randomTurns = null;
        const int numberOfRandomTurns = 30;
        bool isScrambleComplete = true;
        System.Random rnd = new System.Random();
        public bool ScrambleCube(float speed = 1.0f, bool hideCube = false)
        {
            /// Initial State: isScrambleComplete = true
            if (isScrambleComplete)
            {
                /// To scramble cube quickly
                setHide(hideCube);

                randomTurns = new int[numberOfRandomTurns];

                /// Next part requires initial value, since no forbidden turn can be generated yet
                randomTurns[0] = rnd.Next(1, 13);
                
                int nextTurn;

                for (int i = 0; i < numberOfRandomTurns - 1; i++)
                {
                    /// Each turn has a numerical id, defined by the enum above
                    /// Note, the possible values here are 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
                    /// But there are 12 turns. It's this way because the reverse version of the last turn cannot be chosen again (or we effectively have wasted 2 turns for scrambling)
                    nextTurn = rnd.Next(1, 12);

                    /// This part is here to stop U and Ur pairs, etc.
                    
                    /// If last added turn is U...
                    int turn = randomTurns[i];

                    /// ...then forbiddenTurn will be Ur
                    int forbiddenTurn;

                    /// If turn is either U, D, R, L, F or B (see enum for specific integer values)
                    if (turn % 2 == 1)
                    {
                        /// forbiddenTurn = Ur, Dr, Rr, Lr, Fr or Br
                        forbiddenTurn = turn + 1;
                    }
                    /// Else turn is either Ur, Dr, Rr, Lr, Fr or Br
                    else
                    {
                        /// forbiddenTurn = U, D, R, L, F or B
                        forbiddenTurn = turn - 1;
                    }

                    /// Effectively, for list of turns:  U,  Ur,  D,  Dr,  R,  Rr,  L,  Lr,  F,  Fr,  B,  Br
                    ///                             id:  1,  2,   3,  4,   5,  6,   7,  8,   9,  10,  11, 12
                    /// Let turn = Lr
                    /// then forbiddenTurn = L
                    /// so to make sure L cannot be chosen
                    /// if nextTurn is the same as or over the index of L, like nextTurn = 7, nextTurn++, so we can effectively omit this as an option
                    /// 
                    /// Let turn = L
                    /// then forbiddenTurn = Lr
                    /// so to make sure Lr cannot be chosen
                    /// if nextTurn is the same as or over the index of Lr, like nextTurn = 8, nextTurn++, so we can effectively omit this as an option


                    if (nextTurn >= forbiddenTurn)
                    {
                        nextTurn++;
                    }

                    randomTurns[i + 1] = nextTurn;
                    
                    /// isScrambleComplete = false <= Not needed as the next part will automatically do this
                }

                /// Solve Cube must start from beginning again
                step = 1;
            }

            /// Excecute animation
            isScrambleComplete = ExcecuteTurns(randomTurns, speed);

            /// If complete with all turns
            if (hideCube && isScrambleComplete)
            {
                setHide(false);
            }

            return isScrambleComplete;
        }

        public List<int[]> getColorsGroupedByPiece()
        {
            List<int[]> grandListOfColors = new List<int[]>();

            /// For each piece, obtain colors
            for (int k = 0; k < theObjects.Count - 6; k++)
            {
                RubiksCubePiece rubiksCubePiece = (RubiksCubePiece)theObjects[k];

                grandListOfColors.Add(rubiksCubePiece.getColors());
            }

            return grandListOfColors;
        }

        public int FindPieceIndexByColor(int[] pieceColors)
        {
            /// The identifying criteria for the piece we are looking for is:
            ///     * The piece contains the same colors - BUT not necessarily in the same order
            List<int[]> correctColors = getColorsGroupedByPiece();

            int[] piecesToSearchFrom;

            /// The pieces to check from depends on pieceColors.Length
            switch (pieceColors.Length)
            {
                /*
                case 1:
                    piecesToSearchFrom = new int[] { 4, 13, 19, 21, 22, 24 };
                    break;
                //*/

                case 2:
                    piecesToSearchFrom = new int[] { 1, 3, 5, 7, 10, 12, 14, 16, 18, 20, 23, 25 };
                    break;

                case 3:
                    piecesToSearchFrom = new int[] { 0, 2, 6, 8, 9, 11, 15, 17 };
                    break;

                default:
                    /// There's no reason to contain one color, as this is the centre piece of the face, which also dictates which color that face is going to be. (i.e. these pieces with one color should never move)
                    /// Also, any more than 3 colors, and then the piece is not valid.
                    throw new System.Exception($"pieceColors can only contain 2 or 3 colors. Given number of colors: {pieceColors.Length}");
            }

            /// Find our piece of interest
            for (int j = 0; j < piecesToSearchFrom.Length; j++)
            {
                int pieceIndex = piecesToSearchFrom[j];
                int[] edgePieceColors = correctColors[pieceIndex];

                /// The number of matching colors in edgePieceColors and pieceColors
                int numberOfMatchingColors = 0;
                
                /// This loop cycles through pieceColors
                for (int i = 0; i < pieceColors.Length; i++)
                {
                    int colorToCheck = pieceColors[i];

                    /// As long as One of the colors in edgePieceColors == colorToCheck
                    /// This loop cycles through edgePieceColors
                    for (int k = 0; k < edgePieceColors.Length; k++)
                    {
                        /// Check if colorToCheck is in edgePieceColors
                        if (colorToCheck == edgePieceColors[k])
                        {
                            numberOfMatchingColors++;
                        }
                    }

                }

                if (numberOfMatchingColors != pieceColors.Length)
                {
                    continue;
                }

                /// By this point, pieceIndex holds the index of the piece we are after
                return pieceIndex;

            }

            throw new System.Exception($"No piece with the colors in pieceColors was found. pieceColors: {pieceColors}");
        }

        private int[] TranslateTurns(int[] untranslatedTurns, int facingWhichFace = (int)Slices.Front)
        {
            int[] translatedTurns;

            /// Translate the facingWhichFace information
            int[][] translation = null;

            switch (facingWhichFace)
            {
                case (int)Slices.Front:
                    break;

                case (int)Slices.Left:

                    translation = new int[][]
                    {
                        new int[] { (int)Turns.F, (int)Turns.L },
                        new int[] { (int)Turns.Fr, (int)Turns.Lr },

                        new int[] { (int)Turns.L, (int)Turns.B },
                        new int[] { (int)Turns.Lr, (int)Turns.Br },

                        new int[] { (int)Turns.B, (int)Turns.R },
                        new int[] { (int)Turns.Br, (int)Turns.Rr },

                        new int[] { (int)Turns.R, (int)Turns.F },
                        new int[] { (int)Turns.Rr, (int)Turns.Fr }
                    };

                    break;

                case (int)Slices.Right:

                    translation = new int[][]
                    {
                        new int[] { (int)Turns.F, (int)Turns.R },
                        new int[] { (int)Turns.Fr, (int)Turns.Rr },

                        new int[] { (int)Turns.L, (int)Turns.F },
                        new int[] { (int)Turns.Lr, (int)Turns.Fr },

                        new int[] { (int)Turns.B, (int)Turns.L },
                        new int[] { (int)Turns.Br, (int)Turns.Lr },

                        new int[] { (int)Turns.R, (int)Turns.B },
                        new int[] { (int)Turns.Rr, (int)Turns.Br }
                    };

                    break;

                case (int)Slices.Back:

                    translation = new int[][]
                    {
                        new int[] { (int)Turns.F, (int)Turns.B },
                        new int[] { (int)Turns.Fr, (int)Turns.Br },

                        new int[] { (int)Turns.L, (int)Turns.R },
                        new int[] { (int)Turns.Lr, (int)Turns.Rr },

                        new int[] { (int)Turns.B, (int)Turns.F },
                        new int[] { (int)Turns.Br, (int)Turns.Fr },

                        new int[] { (int)Turns.R, (int)Turns.L },
                        new int[] { (int)Turns.Rr, (int)Turns.Lr }
                    };

                    break;

                default:
                    throw new System.Exception($"Invalid facingWhichFace. Can only face the Front, Left, Back or Right. Given facingWhichFace: {facingWhichFace}");

            }

            /// Translate (or just add if no translation is required)
            /// If translation is required
            if (translation != null)
            {
                translatedTurns = new int[untranslatedTurns.Length];

                for (int i = 0; i < translatedTurns.Length; i++)
                {
                    int turn = untranslatedTurns[i];

                    if (turn == (int)Turns.U || turn == (int)Turns.Ur || turn == (int)Turns.D || turn == (int)Turns.Dr)
                        translatedTurns[i] = turn;

                    for (int j = 0; j < translation.Length; j++)
                    {
                        if (turn == translation[j][0])
                        {
                            translatedTurns[i] = translation[j][1];
                            break;
                        }
                    }

                }
            }

            else
            {
                translatedTurns = (int[])untranslatedTurns.Clone();
            }

            return translatedTurns;
        }

        private (int row, int face) AnalyseIndex(int indexIn)
        {
            int row;
            int face = -1;

            /// Integer Divide to find out which row this index is on.
            /// I constructed theObjects, so that indexes:
            ///     0-8 consist of pieces on the top row
            ///     9-17 consist of pieces on the bottom row
            ///     18-25 consist of pieces on the middle row
            switch (indexIn / 9)
            {
                /// Top Row
                case 0:
                    row = 0;
                    break;

                /// Bottom Row
                case 1:
                    row = 1;
                    break;

                /// Middle Row
                case 2:
                    row = 2;
                    break;

                default:
                    throw new System.Exception($"Index given is not of a piece. indexIn: {indexIn}");
            }

            /// The indexes of all the pieces, grouped by which face they belong to: Either Red, Green, Orange or Blue. White and Yellow not options, due to nature of Layer by Layer Algorithm
            /// Each sub array, holds info [top-middle of face, top-right, middle-middle, middle-right, bottom-middle, bottom-right]
            /// The reason there is no [top-left,  middle-left,  bottom-left] is because otherwise, there would be overlapping indexes over faces. Removing these removes overlapping indexes, so there is no confusion in the identification
            int[][] verticalFaces = new int[][]
            {
                /// Red Face = 0
                new int[] { 1, 2, 19, 20, 10, 11 }, 

                /// Green Face = 1
                new int[] { 3, 0, 21, 18, 12, 9 },

                /// Orange Face = 2
                new int[] { 7, 6, 24, 23, 16, 15 },

                /// Blue Face = 3
                new int[] { 5, 8, 22, 25, 14, 17 }
            };

            /// Identify which face the piece lies on
            bool found = false;
            for (int i = 0; i < 4 && !found; i++)
            {
                int[] faceOfInterest = verticalFaces[i];

                for (int j = 0; j < 6; j++)
                {
                    if (faceOfInterest[j] == indexIn)
                    {   
                        /// Assign which face middle/right this index is on.
                        face = i + 3;

                        /// Leave search loops
                        found = true;
                        break;
                    }
                    
                }
                
            }
            if (!found)
            {
                throw new System.Exception("Face could not be identified");
            }

            System.Console.WriteLine($"row: {row}\nface: {face}");

            return (row, face);
        }

        List<int> turnsToExcecute = null;
        bool free = true;
        int prevFace = 3;
        int step7Piece = -1;
        int face7;
        int step = 1;
        bool isStepComplete = true;
        bool isCubeSolved = true;
        public bool SolveCube(float speed = 1.0f)
        { 
            /// If not busy with any animations, then add turns to complete
            if (free)
            {
                /// Reset turns and checks
                isStepComplete = true;

                /// Reset the turns to excecute
                turnsToExcecute = new List<int>();

                /// Get the colors of the cube
                List<int[]> currentColors = getColorsGroupedByPiece();
                System.Console.WriteLine($"\n\n\nstep: {step}");

                switch (step) 
                {
                    /// Initial State
                    case 1:
                        #region Obtain Yellow Cross
                        /// Step 1: Obtain a yellow cross at the bottom of the cube, the 4 edge pieces used to form the yellow cross, should also have matching colors with their respective faces

                        /// Yellow Centre Piece Index: 13
                        /// Orange-Yellow: 16
                        /// Blue-Yellow: 14
                        /// Green-Yellow: 12
                        /// Red-Yellow: 10

                        /// In the order of the face we are checking - (2-coloured Edge Pieces Yellow and) 
                        ///                             Red, Green, Orange, Blue
                        int[] piecesToCheck = new int[] { 10, 12, 16, 14 };

                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            /// If the piece is not in place
                            if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1]))
                            {
                                /// The cube is not solved, do not return true
                                isStepComplete = false;

                                int itsIndex = FindPieceIndexByColor(correctColors[piece]);
                                (int row, int face) = AnalyseIndex(itsIndex);

                                /// If the piece is not in its place
                                if (itsIndex != piece)
                                {
                                    /// First take it to the top row
                                    switch (row)
                                    {
                                        /// case 0: Top Row, Do nothing

                                        /// Bottom Row
                                        case 1:
                                            turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Fr, (int)Turns.Fr }, face));
                                            break;

                                        /// Middle Row
                                        case 2:
                                            turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Fr }, face));
                                            break;
                                    }

                                    /// Red, Green, Orange, Blue
                                    /// +3 so the value allocates to a slice (3 = Red / Front Slice in enum, etc.)
                                    int correctFace = i + 3;

                                    /// Carry out U turns, until piece is on correct face
                                    /// Take piece towards its matching centre color
                                    int numberOfTurns = correctFace - face;
                                    switch (numberOfTurns)
                                    {
                                        case -3:
                                            turnsToExcecute.Add((int)Turns.U);
                                            break;

                                        case -2:
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                            break;

                                        case -1:
                                            turnsToExcecute.Add((int)Turns.Ur);
                                            break;

                                        case 1:
                                            turnsToExcecute.Add((int)Turns.U);
                                            break;

                                        case 2:
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                            break;

                                        case 3:
                                            turnsToExcecute.Add((int)Turns.Ur);
                                            break;
                                    }

                                    /// Turn piece from top row to bottom row
                                    turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.F, (int)Turns.F }, correctFace));

                                }

                                /// If the piece is incorrectly orientated
                                else if (currentColors[piece][0] == correctColors[piece][1] && currentColors[piece][1] == correctColors[piece][0])
                                {
                                    turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Fr, (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.F, (int)Turns.F }, face));
                                }

                                /// Solve each piece one at a time
                                break;
                            }
                        }
                        #endregion
                        break;

                    case 2:
                        #region Slot Yellow Corner Pieces

                        /// In the order of the face we are checking 
                        piecesToCheck = new int[] { 11, 9, 15, 17 };

                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            /// 3-Front, 4-Left, 5-Back, 6-Right
                            /// 3-Red, 4-Green, 5-Orange, 6-Blue
                            int correctFace = i + 3;

                            /// If the piece where Color-Color-Yellow is supposed to be, is not Color-Color-Yellow or is incorrectly orientatated
                            if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1] && currentColors[piece][2] == correctColors[piece][2]))
                            {
                                /// The cube is not solved, do not return true
                                isStepComplete = false;

                                int itsIndex = FindPieceIndexByColor(correctColors[piece]);
                                (int row, int face) = AnalyseIndex(itsIndex);

                                /// If Color-Yellow is incorrectly orientated
                                if (itsIndex == piece)
                                {
                                    /// Set turns to recorrect orientation
                                    turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.Ur, (int)Turns.Ur, (int)Turns.Fr, (int)Turns.U, (int)Turns.F }, correctFace));

                                    break;
                                }

                                else if (row == 1)
                                {
                                    /// Take piece to top layer
                                    turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.Ur }, face));
                                }

                                else
                                {
                                    /// The piece of interest will be in the top layer by this point

                                    /// This is required to make sure that for the next steps, find index delivers valid information, as any turns made can invalidate the information
                                    if (turnsToExcecute.Count != 0)
                                    {
                                        break;
                                    }

                                    /// Take piece towards its matching centre color
                                    int numberOfTurns = correctFace - face;

                                    switch (numberOfTurns)
                                    {
                                        case -3:
                                            turnsToExcecute.Add((int)Turns.U);
                                            break;

                                        case -2:
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                            break;

                                        case -1:
                                            turnsToExcecute.Add((int)Turns.Ur);
                                            break;

                                        case 1:
                                            turnsToExcecute.Add((int)Turns.U);
                                            break;

                                        case 2:
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                            break;

                                        case 3:
                                            turnsToExcecute.Add((int)Turns.Ur);
                                            break;
                                    }

                                    /// Turn piece back to the bottom
                                    turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr }, correctFace));

                                }

                                break;
                            }
                        }

                        #endregion
                        break;

                    case 3:
                        #region Slot Middle Pieces into Place

                        /// In the order of the face we are checking
                        piecesToCheck = new int[] { 20, 18, 23, 25 };

                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            /// 3-Front, 4-Left, 5-Back, 6-Right
                            /// 3-Red, 4-Green, 5-Orange, 6-Blue
                            int correctFace = i + 3;

                            /// If the piece where Color-Color-Yellow is supposed to be, is not Color-Color-Yellow or is incorrectly orientatated
                            if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1]))
                            {
                                isStepComplete = false;

                                /// Find the piece
                                /// Our face of interest is the face which has this corner piece on its right side
                                int itsIndex = FindPieceIndexByColor(correctColors[piece]);
                                (int row, int face) = AnalyseIndex(itsIndex);

                                /// If Color-Yellow is incorrectly orientated
                                if (itsIndex == piece)
                                {
                                    /// Set turns to recorrect orientation
                                    turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), correctFace));
                                    turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                    turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), correctFace));

                                    break;
                                }

                                else
                                {
                                    switch (row)
                                    {
                                        /// Middle Layer
                                        case 2:
                                            turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), face));
                                            break;
                                    }

                                    /// The piece of interest will be in the top layer by this point

                                    /// This is required to make sure that for the next steps, find index delivers valid information, as any turns made can invalidate the information
                                    if (turnsToExcecute.Count != 0)
                                    {
                                        break;
                                    }

                                    /// Take piece towards its matching centre color
                                    int numberOfTurns = correctFace - face;

                                    switch (numberOfTurns)
                                    {
                                        case -3:
                                            turnsToExcecute.Add((int)Turns.U);
                                            break;

                                        case -2:
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                            break;

                                        case -1:
                                            turnsToExcecute.Add((int)Turns.Ur);
                                            break;

                                        case 1:
                                            turnsToExcecute.Add((int)Turns.U);
                                            break;

                                        case 2:
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                            break;

                                        case 3:
                                            turnsToExcecute.Add((int)Turns.Ur);
                                            break;
                                    }

                                    /// Turn piece back to the bottom
                                    turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), correctFace));

                                    break;
                                }

                            }

                            //System.Console.WriteLine("Piece Solved");
                        }

                        #endregion
                        break;

                    case 4:
                        #region Obtain White Cross

                        /// In the order of the face we are checking
                        piecesToCheck = new int[] { 1, 3, 5, 7 };

                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            /// 3-Front, 4-Left, 5-Back, 6-Right
                            /// 3-Red, 4-Green, 5-Orange, 6-Blue
                            int faceOfInterest;

                            /// If the top color is not White (we do not have to bother about the red, green, orange or blue face here)
                            if (!(currentColors[piece][1] == (int)RubiksCubePiece.RubiksCubeColors.White))
                            {
                                isStepComplete = false;

                                /// As we do not worry about the colors here, all we need to do is to check which pieces are white, as this influences the faceOfInterest

                                /// If the required pieces are not white, then figure out which pieces have white on top.
                                int[] piecesToCheck2 = new int[] { 0, 1, 2, 3, 5, 6, 7, 8 };
                                bool[] isWhite = new bool[8];

                                for (int j = 0; j < piecesToCheck2.Length; j++)
                                {
                                    int piece2 = piecesToCheck2[j];

                                    isWhite[j] = (currentColors[piece2][currentColors[piece2].Length - 1] == (int)RubiksCubePiece.RubiksCubeColors.White);

                                }

                                /// First, find a Horizontal Or Vertical White Line On the Top Layer
                                /// Horizontal Line
                                if (isWhite[3] && isWhite[4])
                                {
                                    faceOfInterest = (int)Slices.Front;

                                    #region .
                                    /// This is used to prevent a softlock
                                    /*
                                    if (Game.rnd.Next(0, 2) == 0)
                                    {
                                        faceOfInterest = (int)Slices.Left;
                                    }

                                    else
                                    {
                                        faceOfInterest = (int)Slices.Right;
                                    }
                                    //*/
                                    #endregion
                                }

                                /// Vertical Line
                                else if (isWhite[1] && isWhite[6])
                                {
                                    faceOfInterest = (int)Slices.Left;

                                    #region .
                                    /*
                                    /// This is used to prevent a softlock
                                    if (Game.rnd.Next(0, 2) == 0)
                                    {
                                        faceOfInterest = (int)Slices.Front;
                                    }

                                    else
                                    {
                                        faceOfInterest = (int)Slices.Back;
                                    }
                                    //*/
                                    #endregion
                                }

                                /// Otherwise, look for a diagonal white line (which points towards your right shoulder)

                                /// Full Diagonal Lines
                                else if (isWhite[0] && isWhite[7])
                                {
                                    faceOfInterest = (int)Slices.Left;
                                }

                                else if (isWhite[2] && isWhite[5])
                                {
                                    faceOfInterest = (int)Slices.Front;
                                }

                                else
                                {
                                    /// This is the systematic selecting of the next face, to prevent softlock
                                    faceOfInterest = (prevFace - 2) % 4 + 3;
                                }

                                turnsToExcecute.AddRange(TranslateTurns(Algorithm(3), faceOfInterest));
                                prevFace = faceOfInterest;

                                break;

                            }
                        }

                        #endregion
                        break;

                    case 5:
                        #region Position White Edge In Their Correct Position

                        /// In the order of the face we are checking
                        piecesToCheck = new int[] { 1, 3, 5, 7 };

                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            /// 3-Front, 4-Left, 5-Back, 6-Right
                            /// 3-Red, 4-Green, 5-Orange, 6-Blue
                            int correctFace = i + 3;

                            System.Console.WriteLine($"correctFace: {correctFace}");

                            /// If the White-Color Piece is not in the correct position
                            if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1]))
                            {
                                isStepComplete = false;

                                /// We must rotate the pieces such that this algorithm will be able to correct this

                                /// The algorithm will not solve the cube if 1 set of opposite colors are correctly positioned, and the other set is not, so make sure this is not the case (if it is, then carry out the algorithm so it is not, then carry out the next checks).
                                
                                /// First, position red-white correctly
                                int itsIndex = FindPieceIndexByColor(new int[] { (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White });
                                
                                /// Technically I do not need the row information anymore, since the remaining pieces must be on the top row
                                (int row, int face) = AnalyseIndex(itsIndex);

                                /// Take piece towards its matching centre color
                                int numberOfTurns = 3 - face;

                                switch (numberOfTurns)
                                {
                                    case -3:
                                        turnsToExcecute.Add((int)Turns.U);
                                        break;

                                    case -2:
                                        turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                        break;

                                    case -1:
                                        turnsToExcecute.Add((int)Turns.Ur);
                                        break;

                                    case 1:
                                        turnsToExcecute.Add((int)Turns.U);
                                        break;

                                    case 2:
                                        turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                        break;

                                    case 3:
                                        turnsToExcecute.Add((int)Turns.Ur);
                                        break;
                                }

                                if (turnsToExcecute.Count != 0)
                                {
                                    break;
                                }

                                /// Now red is in position, check if 1 opposite pair is matching, and 1 pair in not matching
                                /// Technically, if the function has reached this point, then we know that not all the pieces are in the correct position yet, but we do know that red is definitely in place. So all we need to check is if orange is in place, and green is not

                                bool isThereAMatchingOppositePair = false;
                                int faceOfInterest = (int)Slices.Front;

                                if (currentColors[7][0] == (int)RubiksCubePiece.RubiksCubeColors.Orange && currentColors[3][0] != (int)RubiksCubePiece.RubiksCubeColors.Green)
                                {
                                    /// If there is, do not bother with the next checks, and do the algorithm (once) before doing them
                                    isThereAMatchingOppositePair = true;
                                }

                                /// If the above is not true, then carry out Up turns, so the algorithm can then solve this step
                                if (!isThereAMatchingOppositePair)
                                {
                                    /// Now the algorithm will not solve the cube, if 2 adjacent colors are in place
                                    /// If this is the case, do an Up or Up Reverse Turn before using the algorithm (this ensures that only one color is in place)

                                    /// Red is in position
                                    /// Therefore, check if either green or blue is in position
                                    /// If Blue is in position, do an Up Turn, to get Orange only into position
                                    if (currentColors[5][0] == (int)RubiksCubePiece.RubiksCubeColors.Blue)
                                    {
                                        turnsToExcecute.Add((int)Turns.U);
                                        faceOfInterest = (int)Slices.Back;
                                    }

                                    /// If Green is in position, do an Up Turn, to get Blue only into position
                                    else if (currentColors[3][0] == (int)RubiksCubePiece.RubiksCubeColors.Green)
                                    {
                                        turnsToExcecute.Add((int)Turns.U);
                                        faceOfInterest = (int)Slices.Right;
                                    }

                                    /// If Green is in the blue side, for optimisation, get Green to its correct position
                                    else if (currentColors[5][0] == (int)RubiksCubePiece.RubiksCubeColors.Green)
                                    {
                                        turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                        faceOfInterest = (int)Slices.Left;
                                    }
                                }

                                /// If none of the above are true (i.e. the only white piece is the top central white piece), then simpily carry out the algorithm, as even after 1 excecution in no specific direction, it is garunteed that at least one of the above will become true
                                turnsToExcecute.AddRange(TranslateTurns(Algorithm(4), faceOfInterest));

                                break;

                            }
                        }

                        #endregion
                        break;

                    case 6:
                        #region Position White Corner Pieces Into Their Correct Position

                        bool excecuteTurns = false;

                        /// In the order of the face we are checking
                        piecesToCheck = new int[] { 2, 0, 6, 8 };

                        /// This loop is iterating thorugh each piece, to test for if the pieces are in the correct position
                        /// After the if statement below has been triggered, i is no longer needed
                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            int itsIndex = FindPieceIndexByColor(correctColors[piece]);

                            /// If the pieces are not in the correct position
                            if (piece != itsIndex)
                            {
                                isStepComplete = false;

                                /// The reason for this loop iterating for twice the length of pieces, is to prioritize the pieces in correct orientation and position over pieces just in the correct position.
                                /// The 2 iterations through have different critera/conditions to determine what to prioritize
                                for (int j = 0; j < piecesToCheck.Length * 2; j++)
                                {
                                    int piece2 = piecesToCheck[j % 4];
                                    itsIndex = FindPieceIndexByColor(correctColors[piece2]);

                                    /// If j/4 = 1, then find a piece of correct position and orientation
                                    /// If j/4 = 2, then find a piece of just correct position
                                    switch (j / 4)
                                    {
                                        case 0:
                                            /// Prioritize pieces in the correct position and orientation over pieces just in the correct position
                                            if (currentColors[piece2][0] == correctColors[piece2][0] && currentColors[piece2][1] == correctColors[piece2][1] && currentColors[piece2][2] == correctColors[piece2][2])
                                            {
                                                excecuteTurns = true;
                                            }
                                            break;

                                        case 1:

                                            /// If no piece is in correct orientaion and position, then just find ONE piece in the correct position (both this and above makes this step quicker)
                                            if (piece2 == itsIndex)
                                            {
                                                excecuteTurns = true;
                                            }
                                            break;
                                    }

                                    if (excecuteTurns)
                                    {
                                        int faceOfInterest = (j % 4) + 3;
                                        turnsToExcecute.AddRange(TranslateTurns(Algorithm(5), faceOfInterest));

                                        break;
                                    }

                                    else
                                    {
                                        turnsToExcecute.AddRange(TranslateTurns(Algorithm(5), (int)Slices.Front));
                                    }

                                }

                                break;
                            }
                        }

                        #endregion
                        break;

                    case 7:
                        #region Orientate White Corner Pieces Into Their Correct Orientation and Solve the Cube

                        /// In the order of the face we are checking
                        piecesToCheck = new int[] { 2, 0, 6, 8 };

                        /// Check to see if all the corner pieces are correctly orientated
                        for (int i = 0; i < piecesToCheck.Length; i++)
                        {
                            int piece = piecesToCheck[i];

                            /// If white is not at the top
                            if (currentColors[piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                            {
                                isStepComplete = false;
                                break;
                            }
                        }

                        /// If all the corner pieces are correct, then skip this step
                        if (isStepComplete)
                        {
                            /// By this point, the top slice has all correctly orientated pieces, so just make sure that the final U turns are carried out so all the pieces are in their fully solved

                            /// First, position red-white correctly
                            int itsIndex = FindPieceIndexByColor(new int[] { (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White });

                            /// Technically I do not need the row information anymore, since the remaining pieces must be on the top row
                            (int row, int face) = AnalyseIndex(itsIndex);

                            /// Take piece towards its matching centre color
                            int numberOfTurns = 3 - face;

                            switch (numberOfTurns)
                            {
                                case -3:
                                    turnsToExcecute.Add((int)Turns.U);
                                    break;

                                case -2:
                                    turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                    break;

                                case -1:
                                    turnsToExcecute.Add((int)Turns.Ur);
                                    break;

                                case 1:
                                    turnsToExcecute.Add((int)Turns.U);
                                    break;

                                case 2:
                                    turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                    break;

                                case 3:
                                    turnsToExcecute.Add((int)Turns.Ur);
                                    break;
                            }

                            /// Break out of this step
                            break;
                        }

                        /// By this point, there is at least two pieces, that are not correctly orientated
                        /// Find the correct face and corner to begin working with
                        if (step7Piece == -1)
                        {
                            for (int j = 0; j < piecesToCheck.Length; j++)
                            {
                                step7Piece = piecesToCheck[j];

                                /// If white is not at the top
                                if (currentColors[step7Piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                                {
                                    /// Preserve the value of face
                                    face7 = j + 3;

                                    /// Preserve the value of step7Piece
                                    break;
                                }
                            }
                        }

                        /// If the top color of the top-right corner piece on the face the algorithm started executing on, is not white
                        if (currentColors[step7Piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                        {
                            turnsToExcecute.AddRange(TranslateTurns(Algorithm(6), face7));
                        }

                        else
                        {
                            /// There will always be at least 2 or more incorrectly orientated pieces
                            turnsToExcecute.Add((int)Turns.U);
                        }

                        #endregion
                        break;
                }

                /// Increment step
                if (isStepComplete)
                {
                    step++;

                    if (step >= 9)
                    {
                        isCubeSolved = true;
                        step = 1;
                    }
                }
                else
                {
                    isCubeSolved = false;
                }

            }

            /// Excecute Turns
            if (speed == 90.0f)
            {
                free = ExcecuteTurns(turnsToExcecute.ToArray(), 90.0f);
                return isCubeSolved;
            }

            else if (speed > 80.0f)
            {
                free = ExcecuteTurns(turnsToExcecute.ToArray(), speed);
                return isStepComplete;
            }

            else 
            {
                free = ExcecuteTurns(turnsToExcecute.ToArray(), speed);
                return free;
            }

        }

        public void HidePiece(int pieceIndex)
        {
            ((RubiksCubePiece)theObjects[pieceIndex]).setHide(true);
        }

        public void ShowCube()
        {
            for (int i = 0; i < 26; i++)
            {
                ((RubiksCubePiece)theObjects[i]).setHide(false);
            }

            for (int i = 26; i < 32; i++)
            {
                ((RubiksCubeSlice)theObjects[i]).setHide(true);
            }
        }

        void RearrangePieceColors(int[] pieceIndexes, bool swapFrontWithRightTiles = false, bool swapFrontWithTopTiles = false)
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

