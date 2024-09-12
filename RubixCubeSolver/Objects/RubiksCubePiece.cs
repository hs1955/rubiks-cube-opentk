using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCubePiece : CompositeGameObject
    {
        readonly Vector3 blackColor = new Vector3(0.05f);
        const float tileSize = 0.9f;
        const float disFrmBcking = 0.03f;
        int[] colors;

        public enum RubiksCubeColors
        {
            White = 1,
            Red,
            Blue,
            Orange,
            Green,
            Yellow
        }

        public RubiksCubePiece(Shader shader, int frontColor = 0, int rightColor = 0, int topColor = 0, float scale = 1.0f, float[] angles = null, Vector3? position = null) : base(scale, position, angles)
        {
            List<GameMaster.IGameObject> theObjects = new List<GameMaster.IGameObject>();

            List<int> theColors = new List<int>();

            if (frontColor != 0)
            {
                theObjects.Add(new Plane(shader, tileSize, new float[] { 0.0f, 0.0f, 0.0f },  new Vector3(0.0f, 0.0f, 0.50f + disFrmBcking), ConvertRubiksCubeEnumToColor(frontColor)));
                theObjects.Add(new Plane(shader, 1.0f, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.0f, 0.0f, 0.50f), blackColor));
                theColors.Add(frontColor);
            }

            if (rightColor != 0)
            {
                theObjects.Add(new SidePlane(shader, tileSize, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.50f + disFrmBcking, 0.0f, 0.0f), ConvertRubiksCubeEnumToColor(rightColor)));
                theObjects.Add(new SidePlane(shader, 1.0f, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.50f, 0.0f, 0.0f), blackColor));
                theColors.Add(rightColor);
            }

            if (topColor != 0)
            {
                theObjects.Add(new TopPlane(shader, tileSize, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.0f, 0.50f + disFrmBcking, 0.0f), ConvertRubiksCubeEnumToColor(topColor)));
                theObjects.Add(new TopPlane(shader, 1.0f, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.0f, 0.50f, 0.0f), blackColor));
                theColors.Add(topColor);
            }

            colors = theColors.ToArray();

            setGameObjects(theObjects);

        }

        public int[] getColors()
        {
            return colors;
        }
        public int getNumberOfColors()
        {
            return colors.Length;
        }
        public void UpdateColors(int[] colorsIn)
        {
            if (colorsIn.Length != colors.Length)
            {
                throw new System.Exception($"Incorrect Number of Colors given. \nGiven {colorsIn.Length} colors, \nbut {colors.Length} colors required.");
            }

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = colorsIn[i];
                ((GameObject)getGameObjects()[i * 2]).setColor(ConvertRubiksCubeEnumToColor(colorsIn[i]));
            }

        }

        public Vector3 ConvertRubiksCubeEnumToColor(int color)
        {
            Vector3 myColor;

            switch (color)
            {
                case 1:
                    myColor = new Vector3(0.96f); /// White
                    break;

                case 2:
                    myColor = new Vector3(0.9f, 0.0f, 0.0f); /// Red
                    break;

                case 3:
                    myColor = new Vector3(0.0f, 0.0f, 0.9f); /// Blue
                    break;

                case 4:
                    myColor = new Vector3(1.0f, 0.3f, 0.0f); /// Orange
                    break;

                case 5:
                    myColor = new Vector3(0.0f, 0.51f, 0.0f); /// Green
                    break;

                case 6:
                    myColor = new Vector3(1.0f, 0.7f, 0.0f); /// Yellow
                    break;

                default:
                    myColor = new Vector3(0.7f, 0.2f, 0.5f); /// Error
                    break;
            }

            return myColor;
        }

        public void RearrangePieceColors(bool swapFrontWithRightTiles = false, bool swapFrontWithTopTiles = false)
        {
            int[] theColors;
            theColors = (int[])colors.Clone();

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

            UpdateColors(theColors);
        }


    }
}
