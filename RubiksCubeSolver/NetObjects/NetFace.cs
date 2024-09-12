using System;
using System.Collections.Generic;
using System.Linq;
using static RubixCubeSolver.Objects.RubiksCubePiece;
using RubiksCubeSolver;

namespace RubiksCubeSolver.NetObjects
{
    class NetFace
    {
        /// <summary>
        /// Where this face is located
        /// </summary>
        public int[] position { get; private set; }
        const int padding = 52;
        
        /// <summary>
        /// How each of its tiles are mapped onto the full 3D Rubiks Cube
        /// </summary>
        public int[,] mapping { get; private set; }

        public List<NetTile> myTiles = new List<NetTile>()
        {
            new NetTile(new int[] { padding * 0, padding * 0 }),
            new NetTile(new int[] { padding * 1, padding * 0 }),
            new NetTile(new int[] { padding * 2, padding * 0 }),
            new NetTile(new int[] { padding * 0, padding * 1 }),
            new NetTile(new int[] { padding * 1, padding * 1 }),
            new NetTile(new int[] { padding * 2, padding * 1 }),
            new NetTile(new int[] { padding * 0, padding * 2 }),
            new NetTile(new int[] { padding * 1, padding * 2 }),
            new NetTile(new int[] { padding * 2, padding * 2 }),
        };

        /// <summary>
        /// A single face forming part of a Rubiks Cube Net
        /// </summary>
        /// <param name="positionIn">The position of the tile (x,y)</param>
        /// <param name="defaultColor">If not 0, then fix the colour of the tile to one of the colors on the Rubik's Cube</param>
        public NetFace(int[] positionIn, int[,] mappingIn, RubiksCubeColors defaultColor = 0)
        {
            NewPosition(positionIn);

            mapping = (int[,])mappingIn.Clone();

            if (defaultColor != 0)
            {
                myTiles[4].SetPermanentColor(defaultColor);
            }

        }

        public void NewPosition(int[] positionIn)
        {
            position = (int[])positionIn.Clone();

            for (int i = 0; i < 9; i++)
            {
                myTiles[i].netTile.Location = new System.Drawing.Point(padding * (i % 3) + position[0], padding * (i / 3) + position[1]);
            }
        }

    }
}

