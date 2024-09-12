using OpenTK;
using System;
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

        /// <summary>
        /// Holds all the current values of colors which the cube uses to display the correct colors
        /// </summary>
        int[] colors = new int[54];

        /// <summary>
        /// Holds all correct arrangment of colors, with which a solved cube will have. Each set of colors is grouped by piece.
        /// </summary>
        readonly List<int[]> correctColors = new List<int[]> 
        {
            new int[] { 3, 2, 1 },
            new int[] { 2, 1 },
            new int[] { 2, 5, 1 },
            new int[] { 3, 1 },
            new int[] { 1 },
            new int[] { 5, 1 },
            new int[] { 4, 3, 1 },
            new int[] { 4, 1 },
            new int[] { 5, 4, 1 },
            new int[] { 4, 5, 6 },
            new int[] { 4, 6 },
            new int[] { 3, 4, 6 },
            new int[] { 5, 6 },
            new int[] { 6 },
            new int[] { 3, 6 },
            new int[] { 5, 2, 6 },
            new int[] { 2, 6 },
            new int[] { 2, 3, 6 },
            new int[] { 2, 3 },
            new int[] { 2 },
            new int[] { 2, 5 },
            new int[] { 3 },
            new int[] { 5 },
            new int[] { 4, 3 },
            new int[] { 4 },
            new int[] { 4, 5 }
        };

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
                //*
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
                //*/

                /*
                /// Index: 9
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, position: new Vector3(-1.0f, -1.0f, 1.0f), angles: new float[] { 180.0f, 180.0f, 0.0f }),

                /// Index: 10
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Red, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 180.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 1.0f)),

                /// Index: 11
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, 1.0f)),

                /// Index: 12
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, -1.0f, 0.0f)),

                /// Index: 13
                new RubiksCubePiece(shader, 0, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, 0.0f)),

                /// Index: 14
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Blue, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 90.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, 0.0f)),

                /// Index: 15
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Green, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, -90.0f, 0.0f }, position: new Vector3(-1.0f, -1.0f, -1.0f)),

                /// Index: 16
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, 0, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(0.0f, -1.0f, -1.0f)),

                /// Index: 17
                new RubiksCubePiece(shader, (int)RubiksCubePiece.RubiksCubeColors.Orange, (int)RubiksCubePiece.RubiksCubeColors.Blue, (int)RubiksCubePiece.RubiksCubeColors.Yellow, 1.0f, angles: new float[] { 180.0f, 0.0f, 0.0f }, position: new Vector3(1.0f, -1.0f, -1.0f)),

                //*/

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

        public int[] getColorsOfFace(int face)
        {
            /// Where all the colors will be stored
            List<int> theColors = new List<int>();

            /// The pieces that will be involved
            int[] slicePieces;

            switch (face)
            {
                case (int)Slices.Top:
                    slicePieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                    //leftRightSide = true;
                    //directionCorrection = -1;
                    break;

                case (int)Slices.Bottom:
                    //slicePieces = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                    slicePieces = new int[] { 11, 10, 9, 14, 13, 12, 17, 16, 15 };
                    break;

                case (int)Slices.Right:
                    //slicePieces = new int[] { 2, 5, 8, 20, 22, 25, 9, 12, 15 };
                    slicePieces = new int[] { 2, 20, 15, 5, 22, 12, 8, 25, 9 };
                    break;

                case (int)Slices.Left:
                    //slicePieces = new int[] { 0, 18, 17, 3, 21, 14, 6, 23, 11 };
                    slicePieces = new int[] { 17, 18, 0, 14, 21, 3, 11, 23, 6 };

                    //slicePieces = new int[] { 6, 23, 11, 3, 21, 14, 0, 18, 17, };
                    break;

                case (int)Slices.Front:
                    //slicePieces = new int[] { 0, 1, 2, 18, 19, 20, 17, 16, 15 };
                    slicePieces = new int[] { 17, 16, 15, 18, 19, 20, 0, 1, 2 };
                    break;

                case (int)Slices.Back:
                    slicePieces = new int[] { 6, 7, 8, 23, 24, 25, 11, 10, 9 };
                    break;

                default:
                    throw new System.Exception($"Face must be inbetween 1-6\nGiven Face: {face}");
            }

            /// For each piece, obtain colors
            for (int k = 0; k < slicePieces.Length; k++)
            {
                RubiksCubePiece rubiksCubePiece = (RubiksCubePiece)theObjects[slicePieces[k]];

                theColors.AddRange(rubiksCubePiece.getColors());
            }

            return theColors.ToArray();
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

        /// ###### ###### ###### ANIMATIONS ###### ###### ######
        /// 
        /// --- --- Every animation must have 3 states --- --- 
        /// 1) First Time the function is called    (Set Variable firstFrame = false, or currentIndex = 0, if a sequence of animations are carried out)
        /// 2) During the animation                 (Animation is NOT DONE: return false)
        /// 3) Once the animation has finished      (Set Variable firstFrame = true, or currentIndex = -1) (Animation is DONE: return true)
        /// 
        /// Since every animation function is repeatedly called during animation, 
        /// animation functions will also need their to store their own state from which they will continue excecution
        /// This includes:
        /// *** A variable which stores whether the functions is being called on the 1st frame or not
        /// *** Arguments of methods it uses, if they change
        /// *** Return Output of methods it uses (note if these methods are animations, they should not become the return output of this animation function
        ///     --- using this output along with the sub functions firstFrame variable, it's state can be obtained)

        float animationSpeed = 20.0f;
        int prevFrameTime = 0;
        int framesPassed;
        float[] storedAngles;
        bool animationFirstFrame1 = true;
        public bool RotateSlice(int sliceNumber, int direction, float? speedIn = null)
        {
            if (!(direction == 1 || direction == -1))
            {
                throw new System.Exception($"Direction must be 1 or -1.\nGiven Direction: {direction}");
            }

            float speed = speedIn ?? animationSpeed;

            speed = MathHelper.Clamp(speed, 0.00001f, 90.0f);

            RubiksCubeSlice slice;
            int[] slicePieces = new int[9];

            switch (sliceNumber)
            {
                case (int)Slices.Top:
                    slice = (RubiksCubeSlice)theObjects[26];
                    break;

                case (int)Slices.Bottom:
                    slice = (RubiksCubeSlice)theObjects[27];
                    break;

                case (int)Slices.Right:
                    slice = (RubiksCubeSlice)theObjects[28];
                    break;

                case (int)Slices.Left:
                    slice = (RubiksCubeSlice)theObjects[29];
                    break;

                case (int)Slices.Front:
                    slice = (RubiksCubeSlice)theObjects[30];
                    break;

                case (int)Slices.Back:
                    slice = (RubiksCubeSlice)theObjects[31];
                    break;

                default:
                    throw new System.Exception($"Slice Number must be inbetween 1-6\nGiven Slice Number: {sliceNumber}");
            }

            if (animationFirstFrame1)
            {                
                prevFrameTime = Game.frameTime;
                animationFirstFrame1 = false;
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
                    case (int)Slices.Top:
                        slicePieces = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                        //leftRightSide = true;
                        //directionCorrection = -1;
                        break;

                    case (int)Slices.Bottom:
                        //slicePieces = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                        slicePieces = new int[] { 11, 10, 9, 14, 13, 12, 17, 16, 15 };
                        break;

                    case (int)Slices.Right:
                        //slicePieces = new int[] { 2, 5, 8, 20, 22, 25, 9, 12, 15 };
                        slicePieces = new int[] { 2, 20, 15, 5, 22, 12, 8, 25, 9 };
                        leftRightSide = true;
                        break;
                        
                    case (int)Slices.Left:
                        //slicePieces = new int[] { 0, 18, 17, 3, 21, 14, 6, 23, 11 };
                        slicePieces = new int[] { 17, 18, 0, 14, 21, 3, 11, 23, 6 };
                        leftRightSide = true;
                        directionCorrection = -1;

                        //slicePieces = new int[] { 6, 23, 11, 3, 21, 14, 0, 18, 17, };
                        break;

                    case (int)Slices.Front:
                        //slicePieces = new int[] { 0, 1, 2, 18, 19, 20, 17, 16, 15 };
                        slicePieces = new int[] { 17, 16, 15, 18, 19, 20, 0, 1, 2 };
                        frontBackSide = true;
                        break;

                    case (int)Slices.Back:
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
                animationFirstFrame1 = true;

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

        /// This function is not really an animation, but more of a translation
        /// The animation it uses is above
        public bool TurnSlice(int turn, float? speed = null)
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

        int currentIndex1 = -1;
        int[] turns1 = null;
        int facingWhichFace;
        bool subAnimationDone1 = true;
        public bool ExcecuteTurns(int[] turnsIn, float? speed = null, int facingWhichFaceIn = (int)Slices.Front)
        {
            if (turnsIn == null)
            {
                return true;
            }

            /// Initial State
            if (currentIndex1 == -1)
            {
                currentIndex1 = 0;
                facingWhichFace = facingWhichFaceIn;

                turns1 = TranslateTurns(turnsIn, facingWhichFaceIn);
            }

            /// While the function has not completed the last turn
            if (currentIndex1 != turns1.Length)
            {
                /// Though this should never happen, if it does, crash as this is leads to unecessary ambiguity in data
                if (facingWhichFaceIn != facingWhichFace)
                {
                    throw new System.Exception($"Unexpected change to facingWhichFace. Remembered direction: {facingWhichFace}. New and Conflicting Direction: {facingWhichFaceIn}");
                }

                int turn = turns1[currentIndex1];

                if (turn == 0)
                {
                    throw new System.Exception("Invalid Turn. Given turn: 0");
                }

                subAnimationDone1 = TurnSlice(turn, speed);

                /// I.E. If the subAnimation is in third state
                if (subAnimationDone1 && animationFirstFrame1)
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
                currentIndex1 = -1;

                /// Reset Random Turns
                turns1 = null;

                /// Animation Complete
                return true;
            }
        }

        /// Specialised ExcecuteTurns
        /// For these, often the turns are preset before entering them into an ExcecuteTurns
        /// These functions they:
        /// *** Need to initialize and store their turns externally (using something similar to firstFrame)
        /// *** return ExcecuteTurns(turns, facingWhichFace);

        /// This is really just setting ExcecuteTurns, with random turns
        int[] randomTurns = null;
        const int numberOfRandomTurns = 30;
        bool subAnimationDone2 = true;
        public bool ScrambleCube(float? speed = null, bool hideCube = false)
        {
            #region OLD CODE
            /*
            int[] remainingColors = new int[] { 9, 9, 9, 9, 9, 9 };
            List<int> bannedColors = new List<int>();

            int[] numberOfColorsForEachPiece = new int[]
            {
                3, 2, 3, 2, 1, 2, 3, 2, 3,
                3, 2, 3, 2, 1, 2, 3, 2, 3,
                2, 1, 2, 1, 1, 2, 1, 2
            };

            /// Assemble scrambled colors
            /// For each set of colors to obtain
            foreach (int numberOfColors in numberOfColorsForEachPiece)
            {
                List<int> chosenColors = new List<int>();
                for (int i = 0; i < numberOfColors; i++)
                {
                    int colorOfChoice = Game.rnd.Next(1, 7);

                    /// Some rules which must be obeyed
                    
                    /// Cannot add a color the piece already contains
                    bool condition1 = chosenColors.Contains(colorOfChoice);

                    /// Cannot add a color, once 9 of those tiles have already been added
                    bool condition2 = bannedColors.Contains(colorOfChoice);

                    /// White and Yellow can never be on the same piece
                    bool condition3 =
                        (colorOfChoice == (int)RubiksCubePiece.RubiksCubeColors.White) && bannedColors.Contains((int)RubiksCubePiece.RubiksCubeColors.Yellow) ||
                        (colorOfChoice == (int)RubiksCubePiece.RubiksCubeColors.Yellow) && bannedColors.Contains((int)RubiksCubePiece.RubiksCubeColors.White);

                    /// Green and Blue can never be on the same piece
                    bool condition4 =
                        (colorOfChoice == (int)RubiksCubePiece.RubiksCubeColors.Green) && bannedColors.Contains((int)RubiksCubePiece.RubiksCubeColors.Blue) ||
                        (colorOfChoice == (int)RubiksCubePiece.RubiksCubeColors.Blue) && bannedColors.Contains((int)RubiksCubePiece.RubiksCubeColors.Green);

                    /// Red and Orange can never be on the same piece
                    bool condition5 =
                        (colorOfChoice == (int)RubiksCubePiece.RubiksCubeColors.Red) && bannedColors.Contains((int)RubiksCubePiece.RubiksCubeColors.Orange) ||
                        (colorOfChoice == (int)RubiksCubePiece.RubiksCubeColors.Orange) && bannedColors.Contains((int)RubiksCubePiece.RubiksCubeColors.Red);

                    /// Must preserve correct orientation for corner pieces (use correct pieces to acheive this)
                    bool condition6;

                    /// USE APPROACH 2 OF 20 RANDOM TURNS

                    while (condition1 || condition2 || condition3 || condition4 || condition5 || condition6)
                    {
                        /// Increment through the colors, until one is available
                        colorOfChoice = (colorOfChoice % 6) + 1;
                    }

                    /// Add the color
                    chosenColors.Add(colorOfChoice);

                    /// Reduce the number of these colors that are allowed
                    remainingColors[colorOfChoice - 1]--;

                    /// If there are no colors of this kind left
                    if (remainingColors[colorOfChoice - 1] == 0)
                    {
                        /// Do not allow the color
                        bannedColors.Add(colorOfChoice);
                    }

                }

                /// Add the colors to the main set of colors for the cube
                scrambledColors.AddRange(chosenColors);

            }

            /// Update the cube with the scrambled colors
            UpdateColors(scrambledColors.ToArray());

            //*/
            #endregion

            #region Animation
            /*
            /// Inital State
            if (currentIndex2 == -1)
            {
                currentIndex2 = 0;
            }

            /// During Animation
            if (currentIndex2 != randomTurns.Length)
            {
                subAnimationDone2 = ;

                return false;
            }

            /// After Animation
            else
            {
                return true;
            }
            //*/
            #endregion

            if (subAnimationDone2)
            {
                setHide(hideCube);
                step = 0;

                randomTurns = new int[numberOfRandomTurns];

                for (int i = 0; i < randomTurns.Length; i++)
                {
                    randomTurns[i] = Game.rnd.Next(1, 13);

                    /// This part is here to stop useless turns / Turn and Turn Reverse pairs
                    if (i > 0)
                    {
                        /// This will be the turn reverse of the current turn
                        int forbiddenTurn;
                        int turn = randomTurns[i];

                        /// I.E. if turn is either U, D, R, L, F, B
                        if (turn % 2 == 1)
                        {
                            forbiddenTurn = turn + 1;
                        }
                        else
                        {
                            forbiddenTurn = turn - 1;
                        }

                        if (randomTurns[i - 1] == forbiddenTurn)
                        {
                            /// This part ensures that the chances of choosing another turn are even and not biased
                            List<int> allowedTurns = new List<int>();

                            for (int j = 1; j < 13; j++)
                            {
                                if (j != forbiddenTurn)
                                {
                                    allowedTurns.Add(j);
                                }
                            }

                            randomTurns[i] = allowedTurns[Game.rnd.Next(0, allowedTurns.Count)];
                            
                        }
                    }
                }
            }

            subAnimationDone2 = ExcecuteTurns(randomTurns, speed);

            if (subAnimationDone2)
            {
                setHide(false);
            }

            return subAnimationDone2;
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
                    //animationDone = ExcecuteTurns(new int[] { (int)Turns.Ur, (int)Turns.F, (int)Turns.U, (int)Turns.Fr, (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr });
                    algorithmTurns = new int[] { (int)Turns.Ur, (int)Turns.Fr, (int)Turns.U, (int)Turns.F, (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr };
                    defaultFaceWhichFace = (int)Slices.Left;
                    break;

                case 3:
                    //animationDone = ExcecuteTurns(new int[] { (int)Turns.Ur, (int)Turns.F, (int)Turns.U, (int)Turns.Fr, (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr });
                    algorithmTurns = new int[] { (int)Turns.F, (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.Ur, (int)Turns.Fr };
                    break;

                case 4:
                    algorithmTurns = new int[] { (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.U, (int)Turns.R, (int)Turns.U, (int)Turns.U, (int)Turns.Rr };
                    break;

                case 5:
                    /*
                    algorithmTurns = new int[] { (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Lr, (int)Turns.U, (int)Turns.Rr, (int)Turns.Ur, (int)Turns.L };
                    defaultFaceWhichFace = (int)Slices.Left;
                    //*/
                    algorithmTurns = new int[] { (int)Turns.Ur, (int)Turns.Lr, (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.L, (int)Turns.U, (int)Turns.Rr };
                    break;

                case 6:
                    algorithmTurns = new int[] { (int)Turns.L, (int)Turns.D, (int)Turns.Lr, (int)Turns.Dr };
                    break;

                default:
                    throw new Exception($" Invalid Algorithm Number. Algorithm Number Given: {algorithmNumber} ");
            }

            #region Translation
            /*

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
                for (int i = 0; i < algorithmTurns.Length; i++)
                {
                    int turn = algorithmTurns[i];

                    for (int j = 0; j < translation.Length; j++)
                    {
                        if (turn == translation[j][0])
                        {
                            algorithmTurns[i] = translation[j][1];
                            break;
                        }
                    }

                }
            }

            //*/
            #endregion

            /// Translate the facingWhichFace information

            /// Allowed slices: F, L, B, R
            /// Allowed values: 3, 4, 5, 6
            /// Group:          0, 1, 2, 3
            //int faceToFace1 = (((facingWhichFace - 3) + (defaultFaceWhichFace - 3)) % 4) + 3;

            //faceToFace1 = ((facingWhichFace + defaultFaceWhichFace - 6) % 4) + 3;

            algorithmTurns = TranslateTurns(algorithmTurns, defaultFaceWhichFace);

            return algorithmTurns;

        }

        //int faceToFace2;
        List<int> turnsToExcecute = null;
        bool free = true;
        int step = 0;
        //bool[] stepsInHyperSpeed = new bool[] { true, true, true, true, true, true, true, true, false, false, false };
        bool[] stepsInHyperSpeed = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
        bool debug = true;
        int step4FaceDirection = (int)Slices.Front;
        int nextFaceAround3 = (int)Slices.Front;
        int step7Piece = 0;
        bool firstTimeStep7 = true;
        
        int[] unsolvedColors;
        List<int> turnsToSolveCube;
        List<int> optimizedTurns;

        public bool SolveCube(float? speed = null)
        {
            /// If this function is not busy completing an animation, then Select step to complete
            if (free)
            {
                /// Get the colors of the cube
                List<int[]> currentColors = getColorsGroupedByPiece();
                turnsToExcecute = new List<int>();

                /// For any of the steps to solving the cube, the first step is to check if that step is required or not - so when none of the steps are required, then the cube is solved

                bool isCubeSolved = true;

                for (int i = 0; i < correctColors.Count && isCubeSolved; i++)
                {
                    for (int j = 0; j < currentColors[i].Length && isCubeSolved; j++)
                    {
                        if (currentColors[i][j] != correctColors[i][j])
                        {
                            isCubeSolved = false;
                            break;
                        }
                    }
                }

                if (!isCubeSolved && free || step == 9)
                {
                    bool stepComplete = true;

                    switch (step)
                    {
                        case 0:
                            #region Store Unsolved Cube Colors (for optimization)
                            unsolvedColors = getColors();
                            turnsToSolveCube = new List<int>();                            
                            #endregion
                            break;

                        case 1:
                            #region Obtain Yellow Cross
                            /// Step 1: Obtain a yellow cross at the bottom of the cube, the 4 edge pieces used to form the yellow cross, should also have matching colors with their respective faces

                            /// Yellow Centre Piece Index: 13
                            /// Orange-Yellow: 10
                            /// Blue-Yellow: 12
                            /// Green-Yellow: 14
                            /// Red-Yellow: 16

                            //int[] piecesToCheck = new int[] { 10, 12, 14, 16 };

                            /// In the order of the face we are checking (which is dictated by faceToFace2)
                            int[] piecesToCheck = new int[] { 16, 14, 10, 12 };

                            for (int i = 0; i < piecesToCheck.Length; i++)
                            {
                                int piece = piecesToCheck[i];

                                /// 3-Front, 4-Left, 5-Back, 6-Right
                                /// 3-Red, 4-Green, 5-Orange, 6-Blue
                                int faceOfInterest = i + 3;

                                /// If the piece where Color-Yellow is supposed to be, is not Color-Yellow or is incorrectly orientatated
                                if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1]))
                                {
                                    stepComplete = false;

                                    /// If Color-Yellow is incorrectly orientated
                                    if (currentColors[piece][0] == correctColors[piece][1] && currentColors[piece][1] == correctColors[piece][0])
                                    {
                                        /// Set turns to recorrect orientation
                                        turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Fr, (int)Turns.R, (int)Turns.U, (int)Turns.Rr, (int)Turns.F, (int)Turns.F }, faceOfInterest));

                                        break;
                                    }

                                    //*
                                    /// Then Color-Yellow is in the incorrect position, and piece is some other piece
                                    else
                                    {
                                        /// There are 3 possible layers that the piece can be on.
                                        /// The Top White Layer
                                        /// The Middle Layer
                                        /// The Bottom Layer
                                        /// 
                                        /// Ideally, we want these pieces to be on the top layer, as editting their positions there would not disturb the bottom layer
                                        /// In the top and bottom layers, the pieces of interest will be above/bottom of the centre piece, which is fine
                                        /// In the middle, they will be to the sides of the centre piece, so one of the first priorities is to get the middle pieces to the bottom layer

                                        /// But firstly, we must find the piece
                                        /// Find index of piece, by checking all the pieces to find the ones with our colors of interest (correctColors[piece])
                                        /// Deduce which layer it is in from there (based of its index)
                                        /// React accordingly
                                        /// (May want to i--; to recheck for orientation)

                                        /// The indices of all the edge pieces
                                        /// NOTE: For each multiple of 4 we increment by, the layer of interest changes: Top, Bottom, Middle
                                        ///     The Pieces are also in the order (for correct colors) Red, Green, Orange, Blue

                                        #region OLD CODE
                                        /*
                                        int[] edgePieces = new int[] { 1, 3, 7, 5, 16, 14, 10, 12, 18, 20, 23, 25 };

                                        int itsIndex = -1;
                                        faceOfInterest = -1;
                                        int integerDivideForLayer = -1;
                                        int goalFace = -1;

                                        /// Find our piece of interest
                                        for (int j = 0; j < edgePieces.Length; j++)
                                        {
                                            int index = edgePieces[j];
                                            int[] edgePieceColors = currentColors[index];

                                            /// Checks if the piece has the same colors as the piece we are after
                                            bool isThisThePieceOfInterest = (edgePieceColors[0] == correctColors[piece][0] && edgePieceColors[1] == correctColors[piece][1]) || (edgePieceColors[0] == correctColors[piece][1] && edgePieceColors[1] == correctColors[piece][0]);

                                            if (isThisThePieceOfInterest)
                                            {
                                                /// 3-Front, 4-Left, 5-Back, 6-Right
                                                /// 3-Red Face, 4-Green Face, 5-Orange Face, 6-Blue Face
                                                faceOfInterest = (j % 4) + 3;
                                                integerDivideForLayer = j / 4;
                                                itsIndex = index;

                                                int goalColor = -1;
                                                if (edgePieceColors[0] != (int)RubiksCubePiece.RubiksCubeColors.Yellow)
                                                {
                                                    goalColor = edgePieceColors[0];
                                                }
                                                else if (edgePieceColors[1] != (int)RubiksCubePiece.RubiksCubeColors.Yellow)
                                                {
                                                    goalColor = edgePieceColors[1];
                                                }

                                                /// Obtain the face that the goal color is on
                                                /// 3-Front, 4-Left, 5-Back, 6-Right
                                                /// 3-Red, 4-Green, 5-Orange, 6-Blue

                                                switch (goalColor)
                                                {
                                                    case (int)RubiksCubePiece.RubiksCubeColors.Red:
                                                        goalFace = 3;
                                                        break;

                                                    case (int)RubiksCubePiece.RubiksCubeColors.Green:
                                                        goalFace = 4;
                                                        break;

                                                    case (int)RubiksCubePiece.RubiksCubeColors.Orange:
                                                        goalFace = 5;
                                                        break;

                                                    case (int)RubiksCubePiece.RubiksCubeColors.Blue:
                                                        goalFace = 6;
                                                        break;

                                                    default:
                                                        throw new Exception($"goalColor is not valid. Given goalColor: {goalColor}");
                                                }

                                                break;
                                            }

                                        }
                                        //*/
                                        #endregion

                                        int itsIndex;
                                        int integerDivideForLayer;
                                        int goalFace;

                                        FindPieceIndexByColor(correctColors[piece], out itsIndex, out faceOfInterest, out integerDivideForLayer, out goalFace);

                                        /// Carry out integer divide, to figure out if the piece is in the top, bottom or middle layer
                                        switch (integerDivideForLayer)
                                        {
                                            /// Bottom Layer: Take the piece to the top layer, so further editting does not disrupt
                                            case 1:
                                                turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Fr, (int)Turns.Fr }, faceOfInterest));
                                                break;

                                            /// Middle Layer
                                            case 2:
                                                /// If the piece is on the left side of the Front/Back Face
                                                if (itsIndex == 18)
                                                {
                                                    turnsToExcecute.AddRange( new int[] { (int)Turns.F, (int)Turns.U, (int)Turns.Fr } );
                                                }

                                                else if (itsIndex == 20)
                                                {
                                                    //turnsToExcecute.Add((int)Turns.Fr);
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.Fr, (int)Turns.U, (int)Turns.F });
                                                }

                                                else if (itsIndex == 23)
                                                {
                                                    //turnsToExcecute.Add((int)Turns.Br);
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.Br, (int)Turns.U, (int)Turns.B });
                                                }

                                                else if (itsIndex == 25)
                                                {
                                                    //turnsToExcecute.Add((int)Turns.B);
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.B, (int)Turns.U, (int)Turns.Br });
                                                }

                                                break;

                                        }

                                        /// Excecute Turns if there are any as the next part only works when the piece is in the top layer
                                        if (turnsToExcecute.Count != 0)
                                        {
                                            break;
                                        }

                                        /// The piece of interest will be in the top layer by this point
                                        /// Top Layer

                                        /// Take piece towards its matching centre color
                                        int numberOfTurns = goalFace - faceOfInterest;

                                        //*
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
                                        //*/

                                        /// Turn piece back to the bottom
                                        //turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.F, (int)Turns.F }, faceOfInterest));

                                        /// If any turns were peformed before, then there is a piece in the top layer, so bring it down again
                                        turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.F, (int)Turns.F }, goalFace));

                                        break;

                                    }
                                    //*/
                                }
                            }
                            #endregion
                            break;

                        case 2:
                            #region Slot Yellow Corner Pieces

                            /// In the order of the face we are checking (which is dictated by faceToFace2)
                            piecesToCheck = new int[] { 17, 11, 9, 15 };

                            for (int i = 0; i < piecesToCheck.Length; i++)
                            {
                                int piece = piecesToCheck[i];

                                /// 3-Front, 4-Left, 5-Back, 6-Right
                                /// 3-Red, 4-Green, 5-Orange, 6-Blue
                                //int faceOfInterest = (i % 4) + 3;
                                int faceOfInterest;

                                /// If the piece where Color-Color-Yellow is supposed to be, is not Color-Color-Yellow or is incorrectly orientatated
                                if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1] && currentColors[piece][2] == correctColors[piece][2]))
                                {
                                    stepComplete = false;

                                    /// Find the piece
                                    /// Our faceOfInterest is the face which has this corner piece on its left side
                                    FindPieceIndexByColor(correctColors[piece], out int itsIndex, out faceOfInterest, out int integerDivideForLayer, out int goalFace);

                                    /// If Color-Yellow is incorrectly orientated
                                    if (itsIndex == piece)
                                    {
                                        /// Set turns to recorrect orientation
                                        turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Lr, (int)Turns.Ur, (int)Turns.L, (int)Turns.U, (int)Turns.U, (int)Turns.F, (int)Turns.Ur, (int)Turns.Fr }, faceOfInterest));

                                        break;
                                    }

                                    else
                                    {
                                        /// Carry out integer divide, to figure out if the piece is in the top, bottom or middle layer
                                        switch (integerDivideForLayer)
                                        {
                                            /// Bottom Layer: Take the piece to the top layer, so further editting does not disrupt
                                            case 1:
                                                /*
                                                //turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Fr, (int)Turns.Fr, (int)Turns.U, (int)Turns.F, (int)Turns.F }, faceOfInterest));
                                                if (itsIndex == 9)
                                                {
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.B, (int)Turns.U, (int)Turns.Br });
                                                }

                                                else if (itsIndex == 11)
                                                {
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.Br, (int)Turns.Ur, (int)Turns.B });
                                                }

                                                else if (itsIndex == 15)
                                                {
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.Fr, (int)Turns.Ur, (int)Turns.F });
                                                }

                                                else if (itsIndex == 17)
                                                {
                                                    turnsToExcecute.AddRange(new int[] { (int)Turns.F, (int)Turns.U, (int)Turns.Fr });
                                                }
                                                //*/
                                                turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.Lr, (int)Turns.U, (int)Turns.L }, faceOfInterest));

                                                break;

                                                /*
                                                /// Top Layer
                                                case 0:

                                                    break;
                                                //*/
                                        }

                                        /// The piece of interest will be in the top layer by this point

                                        /// This is required to make sure that for the next steps, find index delivers valid information, as any turns made can invalidate the information
                                        if (turnsToExcecute.Count != 0)
                                        {
                                            break;
                                        }

                                        /*
                                        /// Take piece towards its matching centre color
                                        int numberOfTurns = Math.Abs(goalFace - faceOfInterest);

                                        switch (numberOfTurns)
                                        {
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
                                        //*/

                                        /// Take piece towards its matching centre color
                                        int numberOfTurns = goalFace - faceOfInterest;

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

                                        /*
                                        switch (numberOfTurns)
                                        {
                                            case -3:
                                                turnsToExcecute.Add((int)Turns.Ur);
                                                break;

                                            case -2:
                                                turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                                break;

                                            case -1:
                                                turnsToExcecute.Add((int)Turns.U);
                                                break;

                                            case 1:
                                                turnsToExcecute.Add((int)Turns.Ur);
                                                break;

                                            case 2:
                                                turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                                break;

                                            case 3:
                                                turnsToExcecute.Add((int)Turns.U);
                                                break;
                                        }
                                        //*/
                                        /// Turn piece back to the bottom
                                        turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.U, (int)Turns.F, (int)Turns.Ur, (int)Turns.Fr }, goalFace));

                                        /*
                                        if (itsIndex == 0)
                                        {
                                            turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.U, (int)Turns.F, (int)Turns.Ur, (int)Turns.Fr }, goalFace));
                                        }

                                        else if (itsIndex == 2)
                                        {
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr });
                                        }

                                        else if (itsIndex == 8)
                                        {
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.Br, (int)Turns.Ur, (int)Turns.B });
                                        }

                                        else if (itsIndex == 6)
                                        {
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.B, (int)Turns.Ur, (int)Turns.Br });
                                        }
                                        //*/
                                        break;
                                    }

                                }
                            }

                            #endregion
                            break;

                        case 3:
                            #region Slot Middle Pieces into Place

                            /// In the order of the face we are checking
                            piecesToCheck = new int[] { 18, 23, 25, 20 };

                            for (int i = 0; i < piecesToCheck.Length; i++)
                            {
                                int piece = piecesToCheck[i];

                                /// 3-Front, 4-Left, 5-Back, 6-Right
                                /// 3-Red, 4-Green, 5-Orange, 6-Blue
                                //int faceOfInterest = (i % 4) + 3;
                                int faceOfInterest;

                                /// If the piece where Color-Color-Yellow is supposed to be, is not Color-Color-Yellow or is incorrectly orientatated
                                if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1]))
                                {
                                    stepComplete = false;

                                    /// Find the piece
                                    /// Our faceOfInterest is the face which has this corner piece on its left side
                                    FindPieceIndexByColor(correctColors[piece], out int itsIndex, out faceOfInterest, out int integerDivideForLayer, out int goalFace);
                                    int nextFaceAround = (faceOfInterest - 2) % 4 + 3;

                                    /// If Color-Yellow is incorrectly orientated
                                    if (itsIndex == piece)
                                    {
                                        //int nextFace = ((faceOfInterest - 3) + 1) % 4 + 3;

                                        /// Set turns to recorrect orientation
                                        turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), nextFaceAround));
                                        turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                        turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), nextFaceAround));

                                        break;
                                    }

                                    else
                                    {
                                        /// Carry out integer divide, to figure out if the piece is in the top, bottom or middle layer
                                        switch (integerDivideForLayer)
                                        {
                                            /// Middle Layer
                                            case 2:
                                                //turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), faceOfInterest));
                                                turnsToExcecute.AddRange(TranslateTurns(Algorithm(1), nextFaceAround));
                                                break;
                                        }

                                        /// The piece of interest will be in the top layer by this point

                                        /// This is required to make sure that for the next steps, find index delivers valid information, as any turns made can invalidate the information
                                        if (turnsToExcecute.Count != 0)
                                        {
                                            break;
                                        }

                                        /*
                                        /// Take piece towards its matching centre color
                                        int numberOfTurns = Math.Abs(goalFace - faceOfInterest);

                                        switch (numberOfTurns)
                                        {
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
                                        //*/

                                        /// Take piece towards its matching centre color
                                        int numberOfTurns = goalFace - faceOfInterest;

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

                                        /*
                                        switch (numberOfTurns)
                                        {
                                            case -3:
                                                turnsToExcecute.Add((int)Turns.Ur);
                                                break;

                                            case -2:
                                                turnsToExcecute.AddRange(new int[] { (int)Turns.Ur, (int)Turns.Ur });
                                                break;

                                            case -1:
                                                turnsToExcecute.Add((int)Turns.U);
                                                break;

                                            case 1:
                                                turnsToExcecute.Add((int)Turns.Ur);
                                                break;

                                            case 2:
                                                turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.U });
                                                break;

                                            case 3:
                                                turnsToExcecute.Add((int)Turns.U);
                                                break;
                                        }
                                        //*/
                                        /// Turn piece back to the bottom
                                        turnsToExcecute.AddRange(TranslateTurns(Algorithm(2), goalFace));

                                        /*
                                        if (itsIndex == 0)
                                        {
                                            turnsToExcecute.AddRange(TranslateTurns(new int[] { (int)Turns.U, (int)Turns.F, (int)Turns.Ur, (int)Turns.Fr }, goalFace));
                                        }

                                        else if (itsIndex == 2)
                                        {
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.U, (int)Turns.R, (int)Turns.Ur, (int)Turns.Rr });
                                        }

                                        else if (itsIndex == 8)
                                        {
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.Br, (int)Turns.Ur, (int)Turns.B });
                                        }

                                        else if (itsIndex == 6)
                                        {
                                            turnsToExcecute.AddRange(new int[] { (int)Turns.B, (int)Turns.Ur, (int)Turns.Br });
                                        }
                                        //*/
                                        break;
                                    }

                                }
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
                                //int faceOfInterest = (i % 4) + 3;
                                //int faceOfInterest = (int)Slices.Front;
                                int faceOfInterest;

                                /// If the top color is not White (we do not have to bother about the red, green, orange or blue face here)
                                //if (!(currentColors[piece][1] == (int)RubiksCubePiece.RubiksCubeColors.White))
                                if (!(currentColors[piece][1] == (int)RubiksCubePiece.RubiksCubeColors.White))
                                {
                                    stepComplete = false;

                                    /// As we do not worry about the colors here, all we need to do is to check which pieces are white, as this influences the faceOfInterest
                                    
                                    /// If the required pieces are not white, then figure out which pieces have white on top.
                                    int[] piecesToCheck2 = new int[] { 0, 6, 8, 2, 1, 3, 5, 7 };
                                    bool[] isWhite = new bool[8];

                                    for (int j = 0; j < piecesToCheck2.Length; j++)
                                    {
                                        int piece2 = piecesToCheck2[j];

                                        if (currentColors[piece2][currentColors[piece2].Length - 1] == (int)RubiksCubePiece.RubiksCubeColors.White)
                                        {
                                            isWhite[j] = true;
                                        }
                                        else
                                        {
                                            isWhite[j] = false;
                                        }
                                    }

                                    /// First, find a Horizontal Or Vertical White Line On the Top Layer
                                    if (isWhite[4] && isWhite[7])
                                    {
                                        /// This is used to prevent a softlock
                                        if (Game.rnd.Next(0,2) == 0)
                                        {
                                            faceOfInterest = (int)Slices.Left;
                                        }

                                        else
                                        {
                                            faceOfInterest = (int)Slices.Right;
                                        }
                                        
                                    }

                                    else if (isWhite[5] && isWhite[6])
                                    {
                                        /// This is used to prevent a softlock
                                        if (Game.rnd.Next(0, 2) == 0)
                                        {
                                            faceOfInterest = (int)Slices.Front;
                                        }

                                        else
                                        {
                                            faceOfInterest = (int)Slices.Back;
                                        }
                                    }

                                    /// Otherwise, look for a diagonal white line (which points towards your right shoulder) (prioritize a full line of 3 pieces to a diagonal line of 2 pieces)

                                    /// Full Diagonal Lines
                                    else if (isWhite[1] && isWhite[3])
                                    {
                                        faceOfInterest = (int)Slices.Left;
                                    }

                                    else if (isWhite[2] && isWhite[0])
                                    {
                                        faceOfInterest = (int)Slices.Back;
                                    }

                                    else
                                    {
                                        //step4FaceDirection = (((step4FaceDirection - 3) + 1) % 4) + 3;
                                        step4FaceDirection = ((step4FaceDirection - 2) % 4) + 3;

                                        faceOfInterest = step4FaceDirection;
                                    }

                                    /// Part(s) of a Diagonal Line
                                    /// 0, 6, 8, 2
                                    /*
                                    else if (isWhite[0])
                                    {
                                        faceOfInterest = (int)Slices.Right;
                                    }

                                    else if (isWhite[1])
                                    {
                                        faceOfInterest = (int)Slices.Front;
                                    }

                                    else if (isWhite[2])
                                    {
                                        faceOfInterest = (int)Slices.Left;
                                    }

                                    else if (isWhite[3])
                                    {
                                        faceOfInterest = (int)Slices.Back;
                                    }
                                    //*/
                                    /// If none of the above are true (i.e. the only white piece is the top central white piece), then simpily carry out the algorithm, as even after 1 excecution in no specific direction, it is garunteed that at least one of the above will become true
                                    turnsToExcecute.AddRange(TranslateTurns(Algorithm(3), faceOfInterest));

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
                                //int faceOfInterest = (i % 4) + 3;
                                //int faceOfInterest = (int)Slices.Front;

                                /// If the White-Color Piece is not in the correct position
                                if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1]))
                                {
                                    stepComplete = false;

                                    /// We must rotate the pieces such that this algorithm will be able to correct this

                                    /// The algorithm will not solve the cube is 1 set of opposite colors are correctly positioned, and the other set is not, so make sure this is not the case (if it is, then carry out the algorithm so it is not, then carry out the next checks).
                                    /// First, position red-white correctly

                                    FindPieceIndexByColor(new int[] { (int)RubiksCubePiece.RubiksCubeColors.Red, (int)RubiksCubePiece.RubiksCubeColors.White }, out int itsIndex, out int faceOfInterest, out int IntegerDivideForLayer, out int goalFace);

                                    /// Take piece towards its matching centre color
                                    int numberOfTurns = goalFace - faceOfInterest;

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
                                            turnsToExcecute.Add( (int)Turns.U );
                                            faceOfInterest = (int)Slices.Back;
                                        }

                                        /// If Green is in position, do an Up Turn, to get Blue only into position
                                        else if (currentColors[3][0] == (int)RubiksCubePiece.RubiksCubeColors.Green)
                                        {
                                            turnsToExcecute.Add( (int)Turns.U );
                                            faceOfInterest = (int)Slices.Right;
                                        }

                                        /// If Green is in the blue side, for optimisation, get Green to its correct position
                                        else if (currentColors[5][0] == (int)RubiksCubePiece.RubiksCubeColors.Green)
                                        {
                                            turnsToExcecute.AddRange( new int[] { (int)Turns.U, (int)Turns.U } );
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
                            piecesToCheck = new int[] { 0, 6, 8, 2 };

                            //int nextFaceAround2 = (int)Slices.Front;

                            for (int j = 0; j < piecesToCheck.Length; j++)
                            {
                                int piece = piecesToCheck[j];

                                FindPieceIndexByColor(correctColors[piece], out int itsIndex, out int faceOfInterest, out int integerDivideForLayer, out int goalFace);

                                /// If the pieces are not in the correct position
                                if (piece != itsIndex)
                                {
                                    stepComplete = false;

                                    for (int i = 0; i < piecesToCheck.Length * 2; i++)
                                    {
                                        int piece2 = piecesToCheck[i % 4];

                                        if (piece == piece2)
                                        {
                                            continue;
                                        }

                                        //nextFaceAround2 = (i + 1) % 4 + 3;
                                        FindPieceIndexByColor(correctColors[piece2], out itsIndex, out faceOfInterest, out integerDivideForLayer, out goalFace);

                                        switch (i / 4)
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
                                            break;
                                        }

                                    }

                                    turnsToExcecute.AddRange(TranslateTurns(Algorithm(5), faceOfInterest));
                                    break;
                                }
                            }

                            #endregion
                            break;

                        case 7:
                            #region Orientate White Corner Pieces Into Their Correct Orientation and Solve the Cube

                            /*
                            for (int j = 0; j < piecesToCheck.Length; j++)
                            {
                                step7Piece = piecesToCheck[j];

                                if (firstTimeStep7)
                                {
                                    //nextFaceAround3 = ((j + 1) % 4) + 3;
                                    nextFaceAround3 = (j % 4) + 3;
                                }

                                /// If the pieces are not in the correct orientation
                                //if (!(currentColors[piece][0] == correctColors[piece][0] && currentColors[piece][1] == correctColors[piece][1] && currentColors[piece][2] == correctColors[piece][2]))
                                
                                /// If white is not at the top
                                if (currentColors[step7Piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                                {
                                    stepComplete = false;
                                    firstTimeStep7 = false;

                                    turnsToExcecute.AddRange(TranslateTurns(Algorithm(6), nextFaceAround3));
                                    break;
                                }

                                else
                                {
                                    if (!firstTimeStep7)
                                    {
                                        /// There will always be at least 2 or more incorrectly orientated pieces
                                        turnsToExcecute.Add((int)Turns.U);
                                        break;
                                    }
                                }
                            }
                            //*/

                            /// In the order of the face we are checking
                            piecesToCheck = new int[] { 0, 6, 8, 2 };

                            for (int i = 0; i < piecesToCheck.Length; i++)
                            {
                                int piece = piecesToCheck[i];

                                /// If white is not at the top
                                if (currentColors[piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                                {
                                    stepComplete = false;
                                    break;
                                }
                            }

                            if (stepComplete)
                            {
                                break;
                            }

                            /// Find the correct face and corner to begin working with
                            if (firstTimeStep7)
                            {
                                for (int j = 0; j < piecesToCheck.Length; j++)
                                {
                                    step7Piece = piecesToCheck[j];

                                    nextFaceAround3 = (j % 4) + 3;

                                    /// If white is not at the top
                                    if (currentColors[step7Piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                                    {
                                        firstTimeStep7 = false;

                                        break;
                                    }
                                }
                            }

                            if (currentColors[step7Piece][2] != (int)RubiksCubePiece.RubiksCubeColors.White)
                            {
                                firstTimeStep7 = false;

                                turnsToExcecute.AddRange(TranslateTurns(Algorithm(6), nextFaceAround3));
                            }

                            else
                            {
                                /// There will always be at least 2 or more incorrectly orientated pieces
                                turnsToExcecute.Add((int)Turns.U);
                            }

                            #endregion
                            break;

                        case 8:
                            #region If the top layer is not correctly orientated, then orientate it correctly (it is solved)

                            FindPieceIndexByColor(correctColors[1], out int itsIndex8, out int faceOfInterest8, out int integerDivideForLayer8, out int goalFace8);

                            /// Take piece towards its matching centre color
                            int numberOfTurns8 = goalFace8 - faceOfInterest8;

                            switch (numberOfTurns8)
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

                            #endregion
                            break;

                        case 9:
                            #region Optimize Turns
                            /*
                            optimizedTurns = turnsToSolveCube;

                            int[][] optimizations = new int[][]
                            {
                                new int[] { (int)Turns.F, (int)Turns.F, (int)Turns.F },
                                new int[] { (int)Turns.Fr },

                                new int[] { (int)Turns.Fr, (int)Turns.Fr, (int)Turns.Fr },
                                new int[] { (int)Turns.F },

                                new int[] { (int)Turns.R, (int)Turns.R, (int)Turns.R },
                                new int[] { (int)Turns.Rr },

                                new int[] { (int)Turns.Rr, (int)Turns.Rr, (int)Turns.Rr },
                                new int[] { (int)Turns.R },

                                new int[] { (int)Turns.L, (int)Turns.L, (int)Turns.L },
                                new int[] { (int)Turns.Lr },

                                new int[] { (int)Turns.Lr, (int)Turns.Lr, (int)Turns.Lr },
                                new int[] { (int)Turns.L },

                                new int[] { (int)Turns.B, (int)Turns.B, (int)Turns.B },
                                new int[] { (int)Turns.Br },

                                new int[] { (int)Turns.Br, (int)Turns.Br, (int)Turns.Br },
                                new int[] { (int)Turns.B },

                                new int[] { (int)Turns.U, (int)Turns.U, (int)Turns.U },
                                new int[] { (int)Turns.Ur },

                                new int[] { (int)Turns.Ur, (int)Turns.Ur, (int)Turns.Ur },
                                new int[] { (int)Turns.U },

                                new int[] { (int)Turns.D, (int)Turns.D, (int)Turns.D },
                                new int[] { (int)Turns.Dr },

                                new int[] { (int)Turns.Dr, (int)Turns.Dr, (int)Turns.Dr },
                                new int[] { (int)Turns.D },

                                new int[] { (int)Turns.U, (int)Turns.Ur },
                                new int[] { },

                                new int[] { (int)Turns.D, (int)Turns.Dr },
                                new int[] { },

                                new int[] { (int)Turns.R, (int)Turns.Rr },
                                new int[] { },

                                new int[] { (int)Turns.L, (int)Turns.Lr },
                                new int[] { },

                                new int[] { (int)Turns.F, (int)Turns.Fr },
                                new int[] { },

                                new int[] { (int)Turns.B, (int)Turns.Br },
                                new int[] { },

                            };

                            for (int i = 0; i < optimizedTurns.Count; i++)
                            {
                                int indexOfElementToCheck = i;

                                for (int j = 0; j < optimizations.Length / 2; j+=2)
                                {
                                    int[] unoptimizedTurnsToCheckFor = optimizations[j];
                                    int[] replacementTurns = optimizations[j + 1];

                                    bool isOptimizationAvailable = true;

                                    for (int k = 0; k < unoptimizedTurnsToCheckFor.Length; k++)
                                    {
                                        if (unoptimizedTurnsToCheckFor[k] == optimizedTurns[indexOfElementToCheck])
                                        {
                                            if (indexOfElementToCheck + 1 < optimizedTurns.Count)
                                            {
                                                indexOfElementToCheck++;
                                                continue;
                                            }

                                            else
                                            {
                                                isOptimizationAvailable = false;
                                                indexOfElementToCheck = i;
                                                break;
                                            }
                                        }

                                        else
                                        {
                                            isOptimizationAvailable = false;
                                            indexOfElementToCheck = i;
                                            break;
                                        }
                                    }

                                    if (isOptimizationAvailable)
                                    {
                                        /// Replace those turns with the optimized turns
                                        for (int k = 0; k < unoptimizedTurnsToCheckFor.Length; k++)
                                        {
                                            optimizedTurns.RemoveAt(i);
                                        }

                                        for (int k = replacementTurns.Length - 1; k >= 0; k--)
                                        {
                                            optimizedTurns.Insert(i, replacementTurns[k]);
                                        }

                                        i = 0;
                                        break;
                                    }
                                }
                            }
                            

                            UpdateColors(unsolvedColors);
                            turnsToExcecute.AddRange(optimizedTurns);

                            //*/

                            #endregion
                            break;

                        case 10:
                            if (!isCubeSolved) stepComplete = false;
                            break;

                        case 11:
                            Console.Clear();
                            Console.WriteLine("All steps complete.");
                            return true;
                    }

                    if (stepComplete)
                    {
                        step++;

                        if (debug)
                        {
                            Console.Clear();
                            Console.WriteLine($"step: {step}");
                        }
                        
                    }

                    /// If no changes were made, but the cube is still unsolved, throw an a error
                    /*
                    if (turnsToExcecute.Count == 0)
                    {
                        //throw new System.Exception("Nothing more can be done to solve the cube.");
                        Console.Clear();
                        Console.WriteLine("Nothing more can be done to solve the cube.");
                        return true;
                    }
                    //*/

                }
                //*
                else
                {
                    /// If the cube is solved, then do not lock controls
                    return true;
                }
                //*/
            }
            
            if (stepsInHyperSpeed[step - 1])
            {
                speed = 90.0f;
                debug = false;
            }

            int[] turnsToExcecuteArray = turnsToExcecute.ToArray();

            free = ExcecuteTurns(turnsToExcecuteArray, speed);

            turnsToSolveCube.AddRange(turnsToExcecuteArray);

            //*
            if (stepsInHyperSpeed[step - 1])
            {
                return false;
            }
            //*
            else
            {
                //debug = true;
                return free;
            }
            //*/

            return false;
        }

        /// ###### ###### ###### ###### ###### ###### ######

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

        int[] TranslateTurns(int[] untranslatedTurns, int facingWhichFace = (int)Slices.Front)
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
                    throw new Exception($"Invalid facingWhichFace. Can only face the Front, Left, Back or Right. Given facingWhichFace: {facingWhichFace}");

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

        string prevColorsString = "";
        void FindPieceIndexByColor(int[] pieceColors, out int itsIndex, out int faceOfInterest, out int integerDivideForLayer, out int goalFace)
        {
            /// DEBUG INFORMATION
            if (debug)
            {
                string currentColorsString = "[ ";

                for (int i = 0; i < pieceColors.Length; i++)
                {
                    currentColorsString += $"{pieceColors[i]}, ";
                }

                currentColorsString += "]";

                if (prevColorsString != currentColorsString)
                {
                    Console.Clear();
                    Console.WriteLine(currentColorsString);
                    prevColorsString = currentColorsString;
                }
            }

            /// The identifying criteria for the piece we are looking for is:
            ///     * The piece contains the same colors - BUT not necessarily in the same order

            List<int[]> currentColors = getColorsGroupedByPiece();

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
                    piecesToSearchFrom = new int[] { 1, 3, 7, 5, 16, 14, 10, 12, 18, 23, 25, 20 };
                    break;

                case 3:
                    //piecesToSearchFrom = new int[] { 0, 2, 6, 8, 17, 11, 9, 15 };
                    //piecesToSearchFrom = new int[] { 0, 2, 6, 8, 17, 15, 11, 9 };
                    piecesToSearchFrom = new int[] { 0, 6, 8, 2, 17, 11, 9, 15 };
                    break;

                default:
                    /// There's no reason to contain one color, as this is the centre piece of the face, which also dictates which color that face is going to be. (i.e. these pieces with one color should never move)
                    /// Also, any more than 3 colors, and then the piece is not valid.
                    throw new Exception($"pieceColors can only contain 2 or 3 colors. Given number of colors: {pieceColors.Length}");
            }

            /// If null, then search from all the pieces available
            /*
            if (piecesToSearchFromIn == null)
            {
                piecesToSearchFrom = new int[26];

                for (int i = 0; i < piecesToSearchFrom.Length; i++)
                {
                    piecesToSearchFrom[i] = i;
                }
            }
            //*/

            itsIndex = -1;
            faceOfInterest = -1;
            integerDivideForLayer = -1;
            goalFace = -1;

            /// Find our piece of interest
            for (int j = 0; j < piecesToSearchFrom.Length; j++)
            {
                int index = piecesToSearchFrom[j];
                int[] edgePieceColors = currentColors[index];

                /// If the piece does not have the same number of colors as the piece we are after, then this piece definitely is not what we are after
                if (edgePieceColors.Length != pieceColors.Length)
                {
                    throw new Exception("");
                }

                /// Checks if the piece has the same colors as the piece we are after
                //bool isThisThePieceOfInterest = (edgePieceColors[0] == pieceColors[0] && edgePieceColors[1] == pieceColors[1]) || (edgePieceColors[0] == pieceColors[1] && edgePieceColors[1] == pieceColors[0]);

                bool isThisThePieceOfInterest = true;

                /// Check if the colors in edgePieceColors are also in pieceColors
                /// This loop cycles through pieceColors
                int numberOfMatchingColors = 0;
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
                            //isThisThePieceOfInterest = true;
                            numberOfMatchingColors++;
                        }
                    }

                }

                /// After the above embedded loop finishes, numberOfMatchingColors will hold the number of matching colors
                /// If all colors match/pair with another in this piece, then this piece is the one
                /*
                if (numberOfMatchingColors == pieceColors.Length)
                {
                    isThisThePieceOfInterest = true;
                }

                /// Otherwise, this piece is not the one, and the next piece should be checked
                else
                {
                    //numberOfMatchingColors = 0;
                    continue;
                    //isThisThePieceOfInterest = false;
                }
                //*/

                if (numberOfMatchingColors != pieceColors.Length)
                {
                    continue;
                }

                /// By this point, index holds the index of the piece we are after

                if (isThisThePieceOfInterest)
                {
                    /// 3-Front, 4-Left, 5-Back, 6-Right
                    /// 3-Red Face, 4-Green Face, 5-Orange Face, 6-Blue Face
                    faceOfInterest = (j % 4) + 3;
                    integerDivideForLayer = j / 4;
                    itsIndex = index;

                    int goalColor = 10;
                    /*
                    if (edgePieceColors[0] != (int)RubiksCubePiece.RubiksCubeColors.Yellow && edgePieceColors[0] != (int)RubiksCubePiece.RubiksCubeColors.White)
                    {
                        goalColor = edgePieceColors[0];
                    }
                    else if (edgePieceColors[1] != (int)RubiksCubePiece.RubiksCubeColors.Yellow && edgePieceColors[1] != (int)RubiksCubePiece.RubiksCubeColors.White)
                    {
                        goalColor = edgePieceColors[1];
                    }
                    //*/

                    /// Since the colors on every cube are not always in a standard position, to figure out the goal color, a precedence of colors is needed, to determine the goal face we are after
                    /// So a piece with both red and green on, cannot have both green and red as the goal color, so red takes presedence over green
                    /// The only exception to this rule is red and blue, as this combo will normally let red take precedence
                    /// 
                    /// In The Order: Red, Green, Orange, Blue
                    for (int i = 0; i < pieceColors.Length; i++)
                    {
                        int colorToCheck = pieceColors[i];

                        /// Blue takes precedence over Red. This is the only exception to the above order.
                        if ((goalColor == (int)RubiksCubePiece.RubiksCubeColors.Red && colorToCheck == (int)RubiksCubePiece.RubiksCubeColors.Blue) || (goalColor == (int)RubiksCubePiece.RubiksCubeColors.Blue && colorToCheck == (int)RubiksCubePiece.RubiksCubeColors.Red))
                        {
                            goalColor = (int)RubiksCubePiece.RubiksCubeColors.Blue;
                            break;
                        }

                        else if (!(colorToCheck == (int)RubiksCubePiece.RubiksCubeColors.White || colorToCheck == (int)RubiksCubePiece.RubiksCubeColors.Yellow) && goalColor > colorToCheck)
                        {
                            goalColor = colorToCheck;
                        }

                    }


                    /// Obtain the face that the goal color is on
                    /// 3-Front, 4-Left, 5-Back, 6-Right
                    /// 3-Red, 4-Green, 5-Orange, 6-Blue

                    switch (goalColor)
                    {
                        case (int)RubiksCubePiece.RubiksCubeColors.Red:
                            goalFace = 3;
                            break;

                        case (int)RubiksCubePiece.RubiksCubeColors.Green:
                            goalFace = 4;
                            break;

                        case (int)RubiksCubePiece.RubiksCubeColors.Orange:
                            goalFace = 5;
                            break;

                        case (int)RubiksCubePiece.RubiksCubeColors.Blue:
                            goalFace = 6;
                            break;

                        default:
                            throw new Exception($"goalColor is not valid. Given goalColor: {goalColor}");
                    }

                    return;
                }
            }

            throw new Exception("No piece with the colors in pieceColors was found.");
        }

        public void ResetCube()
        {
            int offset = 0;
            int[] newColors = new int[54];

            foreach (int[] setOfColors in correctColors)
            {
                for (int i = 0; i < setOfColors.Length; i++)
                {
                    newColors[offset + i] = setOfColors[i];
                }

                offset += setOfColors.Length;
            }

            step = 0;
            UpdateColors(newColors);
        }
    }
}
