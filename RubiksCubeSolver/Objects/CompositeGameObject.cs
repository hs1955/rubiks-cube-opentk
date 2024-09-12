using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ref List<GameMaster.IGameObject> getGameObjects()
        {
            return ref gameObjects;
        }
        public void setGameObjects(List<GameMaster.IGameObject> theGameObjects)
        {
            gameObjects.Clear();
            gameObjects.AddRange(theGameObjects);
        }

        /// These Attributes Have Defaults
        Vector3 objectPos;
        float objectScale;
        float[] angles = new float[3];
        float[] invertRot = new float[] { 1f, 1f, 1f };
        int[] swapAngles = new int[] { 0, 1, 2 };

        bool hide = false;

        /// Total Number of these objects
        private static int count;

        /// The CompositeGameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public CompositeGameObject(float objectScaleIn = 1.0f, Vector3? objectPosIn = null, float[] anglesIn = null, bool hide = false)
        {
            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world

            angles = anglesIn ?? angles;
            setAngles(angles);

            setHide(hide);

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

        /*
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
        //*/
        public float[] getAngles()
        {
            return angles;
        }
        public void setAngles(float[] anglesIn)
        {
            if (anglesIn.Length != 3)
            {
                throw new Exception("Incorrect data given for angles");
            }

            /// No value can be above 360 degrees
            /// This however gives these angles a range between -360 and 360
            for (int i = 0; i < 3; i++)
            {
                /// Here, control which angles affect which axis - most useful for GameObjects part of CompositeGameObjects, and rotations are not according to standardised axes
                angles[i] = anglesIn[i] % 360;
            }

        }
        public void setAngles(float XAngle, float YAngle, float ZAngle)
        {
            setAngles(new float[3] { XAngle, YAngle, ZAngle });
        }

        /*
        public int getInvertRotation()
        {
            return invertRot;
        }
        public void setInvertRotation(int value)
        {
            invertRot = value;
        }
        //*/
        public float[] getInvertRotation()
        {
            return invertRot;
        }
        public void setInvertRotation(float[] invertIn)
        {
            if (invertIn.Length != 3)
            {
                throw new Exception("Incorrect data given for angles");
            }

            /// No value can be above 360 degrees
            /// This however gives these angles a range between -360 and 360
            for (int i = 0; i < 3; i++)
            {
                invertRot[i] = MathHelper.Clamp(invertIn[i], -1, 1);
            }

        }
        public void setInvertRotation(float invertX, float invertY, float invertZ)
        {
            setInvertRotation(new float[3] { invertX, invertY, invertZ });
        }

        public int[] getSwapAngles()
        {
            return swapAngles;
        }
        public void setSwapAngles(int[] swapAnglesIn)
        {
            if (swapAnglesIn.Length != 3)
            {
                throw new Exception("Incorrect data given for angles");
            }

            for (int i = 0; i < 3; i++)
            {
                swapAngles[i] = swapAnglesIn[i];
            }

        }
        public void setSwapAngles(int Xindex, int Yindex, int Zindex)
        {
            setSwapAngles(new int[3] { Xindex, Yindex, Zindex });
        }

        public bool getHide()
        {
            return hide;
        }
        public void setHide(bool value)
        {
            hide = value;
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
                    ((CompositeGameObject)gameObjects[i]).DisposeThisCompositeGameObject();
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

            /// If this.hide = true, then there is no GameObjects here that will be rendered, so just return an empty list
            if (hide)
            {
                return theGameObjects;
            }

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
                /// If aGameObject.hide = true, then do not do anything to this object, since it does not need to be rendered, and move on to the next object
                if (aGameObject.getHide())
                {
                    continue;
                }

                float[] originalVertices = aGameObject.getVertices();
                float[] newVertices = new float[originalVertices.Length];

                /// Transform each vertex, by transform
                Matrix4 transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles) * Matrix4.CreateScale(objectScale) * Matrix4.CreateTranslation(objectPos);

                /// For each Vertex
                for (int k = 0; k < newVertices.Length / 3; k++)
                {
                    /// The Vertex
                    Vector4 theVertex = new Vector4(originalVertices[k * 3], originalVertices[k * 3 + 1], originalVertices[k * 3 + 2], 1.0f);

                    /// Transform the vertex
                    Vector4 transformedVector = transform * theVertex;

                    /// Add new vertex to the set of newVertices
                    for (int j = 0; j < 3; j++)
                    {
                        newVertices[(k * 3) + j] = transformedVector[j];
                    }

                }

                aGameObject.setVertices(newVertices);

                aGameObject.setPosition(objectPos + new Vector3(transform * new Vector4(aGameObject.getPosition(), 1.0f)));

            }

            return theGameObjects;
        }
    }

}
