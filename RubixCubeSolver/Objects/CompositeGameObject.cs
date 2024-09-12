using OpenTK;
using System;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    /// <summary>
    /// This class, will allow for all types of GameObjects that can be rendered, to exist in the same List. Any class that inherits from GameObject, will become one. GameObjects are any objects, with vertices and indices, and a position.
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

            CheckForInverted();
        }

        /// <summary>
        /// This function is solely responsible for stopping a particular bug, where individual GameObjects, that are turned upsidedown, while being added, turn in the Horizontal Direction, the opposite way to the rest of the object.
        /// </summary>
        private void CheckForInverted()
        {
            foreach (GameMaster.IGameObject aGameObject in gameObjects)
            {
                if (aGameObject is GameObject)
                {
                    //if (!((Math.Abs(((GameObject)aGameObject).getAngles()[1] + this.getAngles()[1]) > 90f) && (Math.Abs(((GameObject)aGameObject).getAngles()[1] + this.getAngles()[1]) < 270f)))

                    /// If the object is upsidedown, set flag for being upsidedown to positive (at 180 degrees, it does not really matter if it's set or not, but after 180 degrees, it does matter)
                    if (!(Math.Abs(((GameObject)aGameObject).getAngles()[1] + this.getAngles()[1]) >= 180f))
                    {
                        ((GameObject)aGameObject).setInvertRotation(((GameObject)aGameObject).getInvertRotation() * -1);
                    }

                }

                else if (aGameObject is CompositeGameObject)
                {
                    /// Do the same for it's subordinates
                    ((CompositeGameObject)aGameObject).CheckForInverted();

                    if (!(Math.Abs(((CompositeGameObject)aGameObject).getAngles()[1] + this.getAngles()[1]) >= 180f))
                    {
                        ((CompositeGameObject)aGameObject).setInvertRotation(((CompositeGameObject)aGameObject).getInvertRotation() * -1);
                    }
                }
                
            }
        }

        /// These Attributes Have Defaults
        Vector3 objectPos;
        float objectScale;
        float[] angles = new float[2];
        private int invertRotation = 1;

        /// Total Number of these objects
        private static int count;        

        /// The CompositeGameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public CompositeGameObject(float objectScaleIn = 1.0f, float horizontalAngleIn = 0.0f, float verticalAngleIn = 0.0f, Vector3? objectPosIn = null)
        {
            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world

            setAngles(horizontalAngleIn, verticalAngleIn);

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

        public float[] getAngles()
        {
            return angles;
        }
        public void setAngles(float horizontalAngleIn = 0.0f, float verticalAngleIn = 0.0f)
        {
            /// Neither value can be above 360 degrees
            /// This however gives these angles a range between -360 and 360
            horizontalAngleIn %= 360;
            verticalAngleIn %= 360;

            angles[0] = horizontalAngleIn;
            angles[1] = verticalAngleIn;
        }

        public int getInvertRotation()
        {
            return invertRotation;
        }
        public void setInvertRotation(int value)
        {
            invertRotation = value;
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
                        theGameObjects[j].DisposeThisGameObject(true);
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

                #region Test Stuff
                //Vector3 theObjectPos = objectPos;

                //Matrix3 moveFromobjectPosToCentre = new Matrix3() { M11 = 1, M12 = 0, M13 = theObjectPos.X, M21 = 0, M22 = 1, M23 = theObjectPos.Y, M31 = 0, M32 = 0, M33 = theObjectPos.Z };

                //Matrix3 moveToobjectPosFromCentre = new Matrix3() { M11 = 1, M12 = 0, M13 = -theObjectPos.X, M21 = 0, M22 = 1, M23 = -theObjectPos.Y, M31 = 0, M32 = 0, M33 = -theObjectPos.Z };

                //Matrix3 fullRotation = moveFromobjectPosToCentre * (Matrix3.CreateRotationY(aGameObject.getAngles()[0]) * Matrix3.CreateRotationX(aGameObject.getAngles()[1])) * moveToobjectPosFromCentre;

                //aGameObject.setPosition(aGameObject.getPosition() + fullRotation * objectPos);
                #endregion

                /// If there is no rotation, then this is definitely the position to rotate by
                //aGameObject.setPosition(aGameObject.getPosition() + objectPos);

                Matrix3 transform = Matrix3.CreateRotationX(MathHelper.DegreesToRadians(this.getAngles()[1])) * Matrix3.CreateRotationY(MathHelper.DegreesToRadians(this.getAngles()[0]));

                //aGameObject.setPosition(objectPos + transform * aGameObject.getPosition());
                aGameObject.setPosition(objectPos + objectScale * (transform * aGameObject.getPosition()));

                aGameObject.setScale(aGameObject.getScale() * objectScale);
                aGameObject.setAngles(aGameObject.getAngles()[0] + this.getAngles()[0], aGameObject.getAngles()[1] + this.getAngles()[1]);
                aGameObject.setInvertRotation(aGameObject.getInvertRotation() * invertRotation);


            }

            return theGameObjects;
        }
    }

}
