using System.Collections.Generic;
using System.Drawing;
using static RubixCubeSolver.Objects.RubiksCubePiece.RubiksCubeColors;

namespace RubiksCubeSolver.NetObjects
{
    class RubiksCubeNet
    {
        /// <summary>
        /// Where this net lies on the form
        /// </summary>
        int[] position;
        const int padding = 52 * 3;

        /// Face 
        /// 0, 1, 2,
        /// 3, 4, 5,
        /// 6, 7, 8
        public List<NetFace> myFaces = new List<NetFace>()
        {
            new NetFace(new int[] { padding * 0, padding * 1 }, 
                //9,10,11,12,13,14,15,16,17
                new int[,] { { 15, 2 }, { 16, 1 }, { 17, 2 }, { 12, 1 }, { 13, 0 }, { 14, 1 }, { 9, 2 }, { 10, 1 }, { 11, 2 } }, 
                Yellow),
            new NetFace(new int[] { padding * 1, padding * 1 }, 
                //0,1,2,9,10,11,18,19,20
                new int[,] { { 0, 1 }, { 1, 0 }, { 2, 0 }, { 18, 0 }, { 19, 0 }, { 20, 0 }, { 9, 0 }, { 10, 0 }, { 11, 1 } }, 
                Red),
            new NetFace(new int[] { padding * 2, padding * 1 }, 
                //0,1,2,3,4,5,6,7,8,9
                new int[,] { { 0, 2 }, { 1, 1 }, { 2, 2 }, { 3, 1 }, { 4, 0 }, { 5, 1 }, { 6, 2 }, { 7, 1 }, { 8, 2 } }, 
                White),
            new NetFace(new int[] { padding * 2, padding * 0 },
                //0,3,6,9,12,15,18,21,23
                new int[,] { { 6, 1 }, { 3, 0 }, { 0, 0 }, { 23, 1 }, { 21, 0 }, { 18, 1 }, { 15, 0 }, { 12, 0 }, { 9, 1 } }, 
                Green),
            new NetFace(new int[] { padding * 2, padding * 2 }, 
                //2,5,8,11,14,17,20,22,25
                new int[,] { { 2, 1 }, { 5, 0 }, { 8, 0 }, { 20, 1 }, { 22, 0 }, { 25, 1 }, { 11, 0 }, { 14, 0 }, { 17, 1 } }, 
                Blue),
            new NetFace(new int[] { padding * 3, padding * 1 },
                //6,7,8,15,16,17,23,24,25
                new int[,] { { 8, 1 }, { 7, 0 }, { 6, 0 }, { 25, 0 }, { 24, 0 }, { 23, 0 }, { 17, 0 }, { 16, 0 }, { 15, 1 } },
                Orange)
           
        };

        /// <summary>
        /// The Rubiks Cube Net
        /// </summary>
        /// <param name="positionIn">The position of the tile (x,y)</param>
        public RubiksCubeNet(int[] positionIn)
        {
            position = positionIn;

            for (int i = 0; i < 6; i++)
            {
                myFaces[i].NewPosition(new int[] { myFaces[i].position[0] + position[0], myFaces[i].position[1] + position[1] });
            }

        }

        /// <summary>
        /// Convert the net, into a color matrix to update the 3D Cube with
        /// </summary>
        /// <returns>An int[] which can be used to update the Rubiks Cube colours with</returns>
        public int[] FormColorMatrix()
        {
            List<int[]> tempPieceColors = new List<int[]>()
            {         //0, 1, 2
            new int[] { 5, 2, 1 },  //0
            new int[] { 2, 1 },     //1
            new int[] { 2, 3, 1 },  //2
            new int[] { 5, 1 },     //3
            new int[] { 1 },        //4
            new int[] { 3, 1 },     //5
            new int[] { 4, 5, 1 },  //6
            new int[] { 4, 1 },     //7
            new int[] { 3, 4, 1 },  //8
            new int[] { 2, 5, 6 },  //9
            new int[] { 2, 6 },     //10
            new int[] { 3, 2, 6 },  //11
            new int[] { 5, 6 },     //12
            new int[] { 6 },        //13
            new int[] { 3, 6 },     //14
            new int[] { 5, 4, 6 },  //15
            new int[] { 4, 6 },     //16
            new int[] { 4, 3, 6 },  //17
            new int[] { 2, 5 },     //18
            new int[] { 2 },        //19
            new int[] { 2, 3 },     //20
            new int[] { 5 },        //21
            new int[] { 3 },        //22
            new int[] { 4, 5 },     //23
            new int[] { 4 },        //24
            new int[] { 4, 3 }      //25
        };
            const int Xaxis = 1;
            const int Yaxis = 2;
            const int rot90 = 1;
            const int rot270 = 2;
            const int rot180 = 3;

            /// Stage 1: Collect Colours
            for (int i = 0; i < 6; i++)
            {
                int rotation = 0;
                int reflection = 0;

                NetFace currentFace = myFaces[i];
                int[,] thisMapping = currentFace.mapping;

                /// Rotate mapping matrices to correctly change pieces
                int[] rotateIndexes;

                /// Reflect mapping matrices to correctly change pieces
                int[] reflectIndexes;

                switch (i)
                {
                    case 0: /// Yellow
                        rotation = rot270;
                        reflection = Yaxis;
                        break;

                    case 1: /// Red
                        rotation = rot90;
                        break;

                    case 2: /// White
                        rotation = rot270;
                        reflection = Yaxis;
                        break;

                    case 3: /// Green
                        rotation = rot180;
                        break;

                    case 4: /// Blue
                        //rotation = rot180;
                        break;

                    case 5: /// Orange
                        rotation = rot270;
                        break;
                }

                switch (rotation)
                {
                    /// Nothing
                    case 0:
                        rotateIndexes = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                        break;

                    /// Rotation by 90 degrees clockwise
                    case rot90:
                        rotateIndexes = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
                        break;

                    /// Rotation by 90 degrees anti-clockwise
                    case rot270:
                        rotateIndexes = new int[] { 2, 5, 8, 1, 4, 7, 0, 3, 6 };
                        break;

                    /// Rotation by 180 degrees
                    case rot180:
                        rotateIndexes = new int[] { 8, 7, 6, 5, 4, 3, 2, 1, 0 };
                        break;

                    default:
                        throw new System.Exception($"Invalid Rotation Amount Given: rotation = {rotation}");
                }

                switch (reflection)
                {
                    /// Nothing
                    case 0:
                        reflectIndexes = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                        break;

                    /// Reflect in the x-axis
                    case Xaxis:
                        reflectIndexes = new int[] { 6, 7, 8, 3, 4, 5, 0, 1, 2 };
                        break;

                    /// Reflect in the y-axis
                    case Yaxis:
                        reflectIndexes = new int[] { 2, 1, 0, 5, 4, 3, 8, 7, 6 };
                        break;

                    default:
                        throw new System.Exception($"Invalid Reflection Given: reflection = {reflection}");
                }

                /// Assign New Indexes to correct mapping
                for (int j = 0; j < 9; j++)
                {
                    tempPieceColors[thisMapping[reflectIndexes[rotateIndexes[j]], 0]][thisMapping[reflectIndexes[rotateIndexes[j]], 1]] = currentFace.myTiles[j].myColor;
                }
            }

            /// Stage 2: Create Matrix
            int[] allPieceColours = new int[54];
            int offset = 0;

            for (int i = 0; i < tempPieceColors.Count; i++)
            {
                int[] pieceColors = tempPieceColors[i];
                int numOfColors = pieceColors.Length;

                for (int j = 0; j < numOfColors; j++)
                {
                    allPieceColours[offset + j] = pieceColors[j];
                }

                offset += numOfColors;
            }

            return allPieceColours;

        }

        public string IsNetValid()
        {
            int state = 0;
            int[] colorCount = new int[] { 0, 0, 0, 0, 0, 0 };
            NetFace netFace;

            for (int i = 0; i < 6; i++)
            {
                netFace = myFaces[i];

                for (int j = 0; j < 9; j++)
                {
                    if (netFace.myTiles[j].netTile.BackColor == Color.CadetBlue)
                    {
                        state = 1;
                    }

                    colorCount[netFace.myTiles[j].myColor - 1]++;

                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (colorCount[i] != 9)
                {
                    state = 2;
                    break;
                }
            }

            switch (state)
            {
                case 0:
                    return "Valid";

                case 1:
                    return "Fill in every single tile first";

                case 2:
                    return "There must be 9 of each coloured tile";

                default:
                    throw new System.Exception("Invalid state given");
            }

        }

    }
}

