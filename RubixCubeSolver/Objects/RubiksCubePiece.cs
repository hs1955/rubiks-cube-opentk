using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCubePiece : CompositeGameObject
    {
        readonly Vector3 blackColor = new Vector3(0.05f);
        const float tileSize = 0.9f;
        const float tileSpaceFromCentre = 0.53f;
        int[] colors;
        List<GameMaster.IGameObject> theObjects = new List<GameMaster.IGameObject>();

        public enum RubiksCubeColors
        {
            White = 1,
            Red,
            Blue,
            Orange,
            Green,
            Yellow
        }

        public RubiksCubePiece(Shader shader, int frontColor = 0, int rightColor = 0, int topColor = 0, float scale = 1.0f, Vector3? position = null, float[] angles = null, float[] invertRot = null) : base(scale, position, angles, invertRot)
        {
            List<int> theColors = new List<int>();

            if (frontColor != 0)
            {
                theObjects.Add(new Plane(shader, tileSize, new Vector3(0.0f, 0.0f, tileSpaceFromCentre), ConvertRubiksCubeEnumToColor(frontColor), new float[] { 0.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }));
                theObjects.Add(new Plane(shader, 1.0f, new Vector3(0.0f, 0.0f, 0.5f), blackColor, new float[] { 0.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }));
                
                theColors.Add(frontColor);
            }

            //*
            if (rightColor != 0)
            {
                theObjects.Add(new SidePlane(shader, tileSize, new Vector3(tileSpaceFromCentre, 0.0f, 0.0f), ConvertRubiksCubeEnumToColor(rightColor), new float[] { 0.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }, new int[] { 0, 1, 2 }));
                theObjects.Add(new SidePlane(shader, 1.0f, new Vector3(0.5f, 0.0f, 0.0f), blackColor, new float[] { 0.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }, new int[] { 0, 1, 2 }));

                theColors.Add(rightColor);
            }

            if (topColor != 0)
            {
                theObjects.Add(new TopPlane(shader, tileSize, new Vector3(0.0f, tileSpaceFromCentre, 0.0f), ConvertRubiksCubeEnumToColor(topColor), new float[] { 0.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }));
                theObjects.Add(new TopPlane(shader, 1.0f, new Vector3(0.0f, 0.5f, 0.0f), blackColor, new float[] { 0.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }));

                theColors.Add(topColor);
            }
            //*/

            /*
            if (rightColor != 0)
            {
                theObjects.Add(new Plane(shader, tileSize, new Vector3(0.51f, 0.0f, 0.0f), ConvertRubiksCubeEnumToColor(rightColor), new float[] { 90.0f, 0.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }, new int[] { 0, 1, 2 }));
                colors.Add(rightColor);
            }

            if (topColor != 0)
            {
                theObjects.Add(new Plane(shader, tileSize, new Vector3(0.0f, 0.51f, 0.0f), ConvertRubiksCubeEnumToColor(topColor), new float[] { 0.0f, 90.0f, 0.0f }, new float[] { 1.0f, 1.0f, 1.0f }, new int[] { 0, 1, 2 }));
                colors.Add(topColor);
            }
            //*/

            //theObjects.Add(new Cube(shader, color: blackColor));
            //theObjects.Add(new Cube(shader, color: blackColor));

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
                ((GameObject)theObjects[i*2]).setColor(ConvertRubiksCubeEnumToColor(colorsIn[i]));
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
    }
}
