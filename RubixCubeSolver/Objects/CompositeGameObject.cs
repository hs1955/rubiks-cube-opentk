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

        public void setGameObjects(List<GameMaster.IGameObject> theGameObjects)
        {
            gameObjects.Clear();
            gameObjects.AddRange(theGameObjects);

            //CheckForInverted();
        }

        /// <summary>
        /// This function is solely responsible for stopping a particular bug, where individual GameObjects, that are turned upsidedown, while being added, turn in the Horizontal Direction, the opposite way to the rest of the object.
        /// </summary>
        private void CheckForInverted()
        {
            foreach (GameMaster.IGameObject aGameObject in gameObjects)
            {
                float[] angleInfo;

                if (aGameObject is GameObject)
                {
                    //GameObject gameObject = (GameObject)aGameObject;

                    //angleInfo = gameObject.getAngles();

                }
                
                //* 
                else if (aGameObject is CompositeGameObject)
                {
                    CompositeGameObject gameObject = (CompositeGameObject)aGameObject;

                    angleInfo = gameObject.getAngles();

                    /// This step has already been covered when the Composite's gameObjects were set, and does not need to be done again
                    /// ((CompositeGameObject)aGameObject).CheckForInverted();

                    if (angleInfo[0] == 90f)
                    {

                    }

                    else if (angleInfo[1] == 90f)
                    {
                        //gameObject.setSwapAngles(0, 1, 2);

                        float[] invertInfo = gameObject.getInvertRotation();
                        //gameObject.setInvertRotation(invertInfo[0], invertInfo[1] * -1, invertInfo[2] * -1);
                    }

                }
                //*/
            }
        }

        /*
        private void CheckForInverted()
        {
            float[] invertInfo;

            foreach (GameMaster.IGameObject aGameObject in gameObjects)
            {
                if (aGameObject is GameObject)
                {
                    invertInfo = ((GameObject)aGameObject).getInvertRotation();

                    if (Math.Abs(((GameObject)aGameObject).getAngles()[1]) == 180f)
                    {
                        /// Set a value for upsidedown-ness based on how upsidedown (vertical rotation) the object is
                        //((GameObject)aGameObject).setInvertRotation(new int[] { invertInfo[0] * -1, invertInfo[1], invertInfo[2] });

                    }

                }

                else if (aGameObject is CompositeGameObject)
                {
                    invertInfo = ((CompositeGameObject)aGameObject).getInvertRotation();

                    if (Math.Abs(((CompositeGameObject)aGameObject).getAngles()[1]) == 180f)
                    {
                        /// Set a value for upsidedown-ness based on how upsidedown (vertical rotation) the object is
                        //((CompositeGameObject)aGameObject).setInvertRotation(new int[] { invertInfo[0] * -1, invertInfo[1], invertInfo[2] });
                    }

                    /// Do the same for it's subordinates
                    ((CompositeGameObject)aGameObject).CheckForInverted();

                }

            }
        }
        //*/

        /// These Attributes Have Defaults
        Vector3 objectPos;
        float objectScale;
        bool hide = false;
        float[] angles = new float[3] { 0.0f, 0.0f, 0.0f };
        float[] invertRot = new float[3] { 1.0f, 1.0f, 1.0f };
        int[] swapAngles = new int[3] { 0, 1, 2 };

        /// Total Number of these objects
        private static int count;        

        /// The CompositeGameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public CompositeGameObject(float objectScaleIn = 1.0f, Vector3? objectPosIn = null, float[] anglesIn = null, float[] invertIn = null)
        {
            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world

            angles = anglesIn ?? angles;
            invertRot = invertIn ?? invertRot;

            setAngles(new float[] { angles[0], angles[1], angles[2] });
            setInvertRotation(new float[] { invertRot[0], invertRot[1], invertRot[2] });

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
                angles[i] = anglesIn[i] % 360;
            }

        }

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

        public void SwapAngles(int index1, int index2)
        {
            int temp = swapAngles[index1];
            swapAngles[index1] = swapAngles[index2];
            swapAngles[index2] = temp;
        }

        public void ShiftAngles(int number)
        {
            int[] shiftedAngles = new int[3];

            for (int i = 0; i < shiftedAngles.Length; i++)
            {
                shiftedAngles[i] = swapAngles[(i + number) % 3];
            }
        }

        public bool getHide()
        {
            return hide;
        }
        public void setHide(bool value)
        {
            hide = value;
        }

        public void ObjectsHide(int[] indexes, bool hide)
        {
            foreach (int index in indexes)
            {
                if (gameObjects.ElementAtOrDefault(index) == default)
                {
                    throw new IndexOutOfRangeException($"There is no gameObject at this index: {index}");
                }

                GameMaster.IGameObject item = gameObjects[index];

                /// If this game object is a GameObject
                if (item is GameObject)
                {
                    ((GameObject)item).setHide(hide);
                }

                /// If this game object is a CompositeGameObject
                else if (item is CompositeGameObject)
                {
                    ((CompositeGameObject)item).setHide(hide);
                }
            }
        }

        public void ShowAllObjects()
        {
            foreach (GameMaster.IGameObject item in gameObjects)
            {
                /// If this game object is a GameObject
                if (item is GameObject)
                {
                    ((GameObject)item).setHide(false);
                }

                /// If this game object is a CompositeGameObject
                else if (item is CompositeGameObject)
                {
                    ((CompositeGameObject)item).setHide(false);
                }
            }
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
                #region OLD CODE
                /*
                /// Set attributes, depending on the CompositeGameObejct

                /// If there is no rotation, then this is definitely the position to rotate by
                /// aGameObject.setPosition(aGameObject.getPosition() + objectPos);

                Matrix3 transform = Matrix3.CreateRotationY(MathHelper.DegreesToRadians(angles[1])) * Matrix3.CreateRotationZ(MathHelper.DegreesToRadians(angles[2])) * Matrix3.CreateRotationX(MathHelper.DegreesToRadians(angles[0]));

                //aGameObject.setPosition(objectPos + transform * aGameObject.getPosition());

                aGameObject.setPosition(objectPos + objectScale * (transform * aGameObject.getPosition()));
                aGameObject.setScale(aGameObject.getScale() * objectScale);

                float[] invertInfo = aGameObject.getInvertRotation();
                float[] angleInfo = aGameObject.getAngles();
                //float[] newAngles = new float[3];

                aGameObject.setAngles(angleInfo[0] + angles[0], angleInfo[1] + angles[1], angleInfo[2] + angles[2]);

                aGameObject.ReorderAngles(swapAngles);

                aGameObject.setInvertRotation(invertInfo[0] * invertRot[0], invertInfo[1] * invertRot[1], invertInfo[2] * invertRot[2]);

                /*
                for (int i = 0; i < 3; i++)
                {
                    newAngles[swapAngles[i]] = angleInfo[i] + angles[i];
                }

                aGameObject.setAngles(newAngles);
                //*/
                #endregion

                if (aGameObject.getHide())
                {
                    continue;
                }

                /// aGameObject has 3 features. A rotation, a position and a scale.

                //*
                Matrix4 localTransform = aGameObject.setRotatedAndPositionedVertices(true);
                float[] originalVertices = aGameObject.getVertices();
                float[] newVertices = new float[originalVertices.Length];
                //*/

                /// Transform each vertex, by transform
                Matrix4 transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles) * Matrix4.CreateScale(objectScale) * Matrix4.CreateTranslation(objectPos);

                /// Repeat for every vertex available in the GameObject
                //*
                for (int k = 0; k < newVertices.Length / 3; k++)
                {
                    /// For each Vertex
                    Vector4 theVertex = new Vector4(originalVertices[k * 3], originalVertices[k * 3 + 1], originalVertices[k * 3 + 2], 1.0f);

                    /// Transform the vertex
                    Vector4 transformedVertex = transform * localTransform * theVertex;

                    /// Add the vertex to the array of vertices
                    for (int j = 0; j < 3; j++)
                    {
                        newVertices[k * 3 + j] = transformedVertex[j];
                    }
                }

                aGameObject.setVertices(newVertices);
                //*/

                aGameObject.setPosition(new Vector3(new Vector4(objectPos, 1.0f) + (transform * new Vector4(aGameObject.getPosition(), 1.0f))));

                /// Here, all the objects are set to rotate to the standardised x, y, z axis (every object in default state is always facing in one of these directions)
                /// If the object faces 90 degrees, then swap X for Z axis, and swap Z axis for minus X axis

            }

            return theGameObjects;
        }

    }

}
