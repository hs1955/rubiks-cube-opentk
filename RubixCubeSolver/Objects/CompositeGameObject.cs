using OpenTK;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    /// <summary>
    /// This abstract class, will allow for all types of GameObjects that can be rendered, to exist in the same List. Any class that inherits from GameObject, will become one. GameObjects are any objects, with vertices and indices, and a position.
    /// Abstract classes cannot be instantiated, but can be inherited
    /// </summary>
    public class CompositeGameObject : GameMaster.IGameObject
    {
        /// This List contains all the GameObjects that CompositeGameObjects will have
        List<GameMaster.IGameObject> gameObjects = new List<GameMaster.IGameObject>();

        public void setGameObjects(List<GameMaster.IGameObject> theGameObjects)
        {
            gameObjects.Clear();
            gameObjects.AddRange(theGameObjects);
        }

        /// These Attributes Have Defaults
        Vector3 objectPos;

        float objectScale;

        /// Total Number of these objects
        private static int count;        

        /// The GameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public CompositeGameObject(float objectScaleIn = 1.0f, Vector3? objectPosIn = null)
        {
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

        public void DisposeThisCompositeGameObject()
        {
            /// This is the number of objects to delete
            int numberOfObjectsBeforeDeletion = gameObjects.Count;
            
            /// For each object in the gameObjects, that make up this Composite Game Object
            for (int i = 0; i < numberOfObjectsBeforeDeletion; i++)
            {
                /// If it is a GameObject
                if (gameObjects[i] is GameObject)
                {
                    /// Dispose accordingly
                    ((GameObject)gameObjects[i]).DisposeThisGameObject(true);
                }

                /// If it is a CompositeGameObject
                else if (gameObjects[i] is CompositeGameObject)
                {
                    /// Get all the GameObjects that make it up
                    List<GameObject> theGameObjects = ((CompositeGameObject)gameObjects[i]).ConvertToGameObjects();
                    int numberOfTheseObjectsBeforeDeletion = theGameObjects.Count;

                    /// And for every one of these objects
                    for (int j = 0; j < numberOfTheseObjectsBeforeDeletion; j++)
                    {
                        /// Dispose of them
                        theGameObjects[i].DisposeThisGameObject(true);
                    }
                }
            }
            /// Delete GameObject
            GameMaster.getGameObjects().Remove(this);
            count--;
        }

        /// <summary>
        /// Takes a CompositeGameObject, and returns a list of GameObjects, that when rendered, produce a list of GameObjects, that when rendered, produce a result that looks identical to the CompositeGameObject.
        /// </summary>
        /// <returns> A list of GameObject, that when rendered, produce a result that looks identical to the CompositeGameObject. </returns>
        public List<GameObject> ConvertToGameObjects()
        {
            /// This list will hold all of the GameObjects that have been converted over.
            List<GameObject> theGameObjects = new List<GameObject>();

            /// For every game object in this CompositeGameObject
            for (int i = 0; i < gameObjects.Count; i++)
            {
                /// If this game object is a GameObject
                if (gameObjects[i] is GameObject)
                {
                    /// Clone this object
                    GameObject aGameObject = ((GameObject)gameObjects[i]).CloneThisGameObject(false);

                    /// Add this GameObject to the list of converted GameObjects
                    theGameObjects.Add(aGameObject);
                }

                /// If this game object is a CompositeGameObject
                else if (gameObjects[i] is CompositeGameObject)
                {
                    /// This part makes this function recursive, since the exact same process happens here, even though this Composite is embedded within another
                    theGameObjects.AddRange(((CompositeGameObject)gameObjects[i]).ConvertToGameObjects());
                }

            }

            /// For each gameObject in the list of GameObjects to convert over from a Composite, to regular GameObjects
            foreach (GameObject aGameObject in theGameObjects)
            {
                /// Set attributes, depending on the CompositeGameObejct
                aGameObject.setPosition(aGameObject.getPosition() + objectPos);
                aGameObject.setScale(aGameObject.getScale() * objectScale);

            }

            return theGameObjects;
        }
    }

}
