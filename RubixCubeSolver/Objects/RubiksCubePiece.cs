using OpenTK;
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

        /// No Tile (Shader shader, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null, Vector3? color = null) : base(vertices, indices, shader, scale, horizontalAngle, verticalAngle, position, color)
        public RubiksCubePiece(Shader shader, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>
            {
                new Cube(shader, color: blackColor),
                new Cube(shader, color: blackColor)

            });

        }

        /// One Tile
        public RubiksCubePiece(Shader shader, int color1, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            setGameObjects(new List<GameMaster.IGameObject> 
            {
                new Plane(shader, tileSize, 0.0f, 0.0f, new Vector3(0.0f, 0.0f, 0.51f), ConvertRubiksCubeEnumToColor(color1)),
                new Cube(shader, color: blackColor),
                new Cube(shader, color: blackColor)
            });

        }

        /// Two Tiles
        public RubiksCubePiece(Shader shader, int color1, int color2, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>
            {
                new Plane(shader, tileSize, 0.0f, 0.0f, new Vector3(0.0f, 0.0f, 0.51f), ConvertRubiksCubeEnumToColor(color1)),
                new Plane(shader, tileSize, 90.0f, 0.0f, new Vector3(0.51f, 0.0f, 0.0f), ConvertRubiksCubeEnumToColor(color2)),
                new Cube(shader, color: blackColor),
                new Cube(shader, color: blackColor)
            });

        }

        /// Three Tiles
        public RubiksCubePiece(Shader shader, int color1, int color2, int color3, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>
            {
                new Plane(shader, tileSize, 0.0f, 0.0f, new Vector3(0.0f, 0.0f, 0.51f), ConvertRubiksCubeEnumToColor(color1)),
                new Plane(shader, tileSize, 90.0f, 0.0f, new Vector3(0.51f, 0.0f, 0.0f), ConvertRubiksCubeEnumToColor(color2)),
                new Plane(shader, tileSize, 0.0f, -90.0f, new Vector3(0.0f, 0.51f, 0.0f), ConvertRubiksCubeEnumToColor(color3), true),
                new Cube(shader, color: blackColor),
                new Cube(shader, color: blackColor)
            });

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
