using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class RubiksCubePiece : CompositeGameObject
    {
        /*
         * "White", new Vector3(1.0f, 1.0f, 1.0f);
            "Red", new Vector3(1.0f, 0.0f, 0.0f);
            "Blue",
            "Orange",
            "Green",
            "Yellow"
        //*/

        public enum RubiksCubeColors
        {
            White = 1,
            Red,
            Blue,
            Orange,
            Green,
            Yellow
        }

        /// No Tile
        public RubiksCubePiece(Shader shader, float scale = 1.0f, Vector3? position = null) : base(scale, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>
            {
                new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)),
                //new Plane(shader, position: new Vector3(0.0f, 0.0f, 0.5001f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color1))
            });

        }

        /// One Tile
        public RubiksCubePiece(Shader shader, int color1, float scale = 1.0f, Vector3? position = null) : base(scale, position) 
        {
            setGameObjects(new List<GameMaster.IGameObject> 
            { 
                new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)), 
                new Plane(shader, position: new Vector3(0.0f, 0.0f, 0.5001f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color1))
            });

        }

        /// Two Tiles
        public RubiksCubePiece(Shader shader, int color1, int color2, float scale = 1.0f, Vector3? position = null) : base(scale, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>
            {
                new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)),
                new Plane(shader, position: new Vector3(0.0f, 0.0f, 0.5f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color1)),
                new Plane(shader, position: new Vector3(0.0f, 0.5f, 0.0f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color2))
            });

        }

        /// Three Tiles
        public RubiksCubePiece(Shader shader, int color1, int color2, int color3, float scale = 1.0f, Vector3? position = null) : base(scale, position)
        {
            setGameObjects(new List<GameMaster.IGameObject>
            {
                new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)),
                new Plane(shader, position: new Vector3(0.0f, 0.0f, 0.5f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color1)),
                new Plane(shader, position: new Vector3(0.0f, 0.5f, 0.0f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color2)),
                new Plane(shader, position: new Vector3(0.5f, 0.0f, 0.0f), scale: 0.8f, color: ConvertRubiksCubeEnumToColor(color3))
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
                    myColor = new Vector3(0.0f, 0.5f, 0.0f); /// Green
                    break;

                case 6:
                    myColor = new Vector3(1.0f, 0.7f, 0.0f); /// Yellow
                    break;
            }

            return myColor;
        }
    }
}
