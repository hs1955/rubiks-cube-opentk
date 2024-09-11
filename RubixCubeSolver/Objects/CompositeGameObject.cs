using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    /// <summary>
    /// This abstract class, will allow for all types of GameObjects that can be rendered, to exist in the same List. Any class that inherits from GameObject, will become one. GameObjects are any objects, with vertices and indices, and a position.
    /// Abstract classes cannot be instantiated, but can be inherited
    /// </summary>
    public abstract class CompositeGameObject : GameMaster.IGameObject
    {
        /// This objects ID. Since it's made of multiple objects, which have their own VAOs, this specific object will need a manual ID
        int ID;

        /// This List contains all the GameObjects that CompositeGameObjects will have
        List<GameObject> gameObjects = new List<GameObject>();

        /// These Attributes Have Defaults
        Vector3 objectPos;

        float objectScale;

        /// Only Information for Other Functions to work properly
        /// Total Number of these objects
        private static int count;
        
        /// The GameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public CompositeGameObject(List<GameObject> gameObjectsIn, float objectScaleIn = 1.0f, Vector3? objectPosIn = null)
        {
            gameObjects.AddRange(gameObjectsIn);

            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world

            count++;
        }

        public Vector3 getPosition()
        {
            return objectPos;
        }

        public void setPosition(Vector3 value)
        {
            objectPos = value;
        }

        public float getScale()
        {
            return objectScale;
        }
        public void setScale(float value)
        {
            objectScale = value;
        }

        public int getCount()
        {
            return count;
        }
        public void setCount(int value)
        {
            count = value;
        }

        public void DisposeThisGameObject()
        {
            int numberOfObjectsBeforeDeletion = gameObjects.Count;
            
            for (int i = 0; i < numberOfObjectsBeforeDeletion; i++)
            {
                gameObjects[i].DisposeThisGameObject();
            }
        }

    }

}
