using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    class CompositeTest : CompositeGameObject
    {
        public CompositeTest(Shader shader, int version, float scale = 1.0f, float horizontalAngle = 0.0f, float verticalAngle = 0.0f, Vector3? position = null) : base(scale, horizontalAngle, verticalAngle, position)
        {
            switch (version)
            {
                case 1:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new Cube(shader, color: new Vector3(0.1f, 0.1f, 0.1f)),

                        new Cube(shader, scale: 1.3f, position: new Vector3(1.0f), color: new Vector3(1f, 0.1f, 0.1f))

                    });
                    break;

                case 2:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new Plane(shader, color: new Vector3(0.96f)),
                        new Plane(shader, scale: 1.3f, position: new Vector3(-1.0f, 1.0f, 1.0f), color: new Vector3(0.9f, 0.0f, 0.0f)),
                        new Plane(shader, scale: 0.8f, position: new Vector3(0.0f, 1.0f, 0.0f), color: new Vector3(0.0f, 0.0f, 0.9f)),
                        new Plane(shader, scale: 0.5f, position: new Vector3(1.0f, 1.0f, -1.0f), color: new Vector3(1.0f, 0.3f, 0.0f)),
                        new Plane(shader, scale: 1.1f, position: new Vector3(-1.0f, -1.0f, 1.0f), color: new Vector3(0.0f, 0.5f, 0.0f)),
                        new Plane(shader, scale: 0.6f, position: new Vector3(-1.0f, 0.0f, -1.0f), color: new Vector3(1.0f, 0.7f, 0.0f))

                    });
                    break;

                case 3:
                    setGameObjects(new List<GameMaster.IGameObject>
                    {

                        new Plane(shader, color: new Vector3(0.96f)),
                        new Plane(shader, scale: 1.3f, position: new Vector3(-1.0f, 1.0f, 0.0f), color: new Vector3(0.9f, 0.0f, 0.0f)),
                        new Plane(shader, scale: 0.8f, position: new Vector3(0.0f, 1.0f, 0.0f), color: new Vector3(0.0f, 0.0f, 0.9f)),
                        new Plane(shader, scale: 0.5f, position: new Vector3(1.0f, 1.0f, 0.0f), color: new Vector3(1.0f, 0.3f, 0.0f)),
                        new Plane(shader, scale: 1.1f, position: new Vector3(-1.0f, -1.0f, 0.0f), color: new Vector3(0.0f, 0.5f, 0.0f)),
                        new Plane(shader, scale: 0.6f, position: new Vector3(-1.0f, 0.0f, 0.0f), color: new Vector3(1.0f, 0.7f, 0.0f))

                    });
                    break;
            }

        }

    }
}
