﻿using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCubePiece : CompositeGameObject
    {
        readonly Vector3 blackColor = new Vector3(0.05f);
        const float tileSize = 0.9f;

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

            if (frontColor != 0)
            {
                theObjects.Add(new Plane(shader, tileSize, new float[] { 0.0f, 0.0f, 0.0f },  new Vector3(0.0f, 0.0f, 0.51f), ConvertRubiksCubeEnumToColor(frontColor)));
            }

            if (rightColor != 0)
            {
                theObjects.Add(new SidePlane(shader, tileSize, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.51f, 0.0f, 0.0f), ConvertRubiksCubeEnumToColor(rightColor)));
            }

            if (topColor != 0)
            {
                theObjects.Add(new TopPlane(shader, tileSize, new float[] { 0.0f, 0.0f, 0.0f }, new Vector3(0.0f, 0.51f, 0.0f), ConvertRubiksCubeEnumToColor(topColor)));
            }

            theObjects.Add(new Cube(shader, color: blackColor));

            setGameObjects(theObjects);

        }

        public Vector3 ConvertRubiksCubeEnumToColor(int color)
        {
            Vector3 myColor = new Vector3(1.0f, 0.3f, 0.31f); /// Default Pink Color - R G B

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
            }

            return myColor;
        }
    }
}
