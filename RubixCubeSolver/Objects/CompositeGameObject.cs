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

            //StandardiseAxes();
        }

        /// <summary>
        /// This function is solely responsible for stopping a particular bug, where individual GameObjects, that are turned upsidedown, while being added, turn in the Horizontal Direction, the opposite way to the rest of the object.
        /// </summary>
        private void StandardiseAxes()
        {
            foreach (GameMaster.IGameObject aGameObject in gameObjects)
            {
                float[] anglesInfo = null;
                float[] invertRotInfo = null;
                int[] swapAnglesInfo = null;

                float[] newInvertRot = new float[3];
                int[] newSwapAngles = new int[3];

                if (aGameObject is GameObject)
                {
                    anglesInfo = ((GameObject)aGameObject).getAngles();
                    invertRotInfo = ((GameObject)aGameObject).getInvertRotation();
                    swapAnglesInfo = ((GameObject)aGameObject).getSwapAngles();
                }

                else if (aGameObject is CompositeGameObject)
                {
                    /// Do the same for it's subordinates
                    ((CompositeGameObject)aGameObject).StandardiseAxes();

                    /// Then do it for the whole object
                    anglesInfo = ((CompositeGameObject)aGameObject).getAngles();
                    invertRotInfo = ((CompositeGameObject)aGameObject).getInvertRotation();
                    swapAnglesInfo = ((CompositeGameObject)aGameObject).getSwapAngles();
                }

                newInvertRot = invertRotInfo;
                newSwapAngles = swapAnglesInfo;

                #region Checks
                /*

                switch ((anglesInfo[0] + this.getAngles()[0]) % 360)
                {
                    case 90f:
                        newSwapAngles = ReturnSwappedList(newSwapAngles, 1, 2);
                        newInvertRot = new float[] { invertRotInfo[0], invertRotInfo[1] * -1f, invertRotInfo[2] };
                        break;

                    case -90f:
                        newSwapAngles = ReturnSwappedList(newSwapAngles, 0, 2);
                        //newInvertRot = new float[] { invertRotInfo[0], invertRotInfo[1], invertRotInfo[2] * -1f };
                        break;
                }
                //*/
                
                //*
                switch ((anglesInfo[1] + this.getAngles()[1]) % 360)
                {
                    case 90f:
                        newSwapAngles = ReturnSwappedList(newSwapAngles, 0, 1);
                        //newInvertRot = new float[] { invertRotInfo[0], invertRotInfo[1], invertRotInfo[2] * -1f };
                        break;
                }

                //*/
                #endregion

                /// Throw an error, if the data has not been assigned too
                if (newInvertRot.Contains(0f) || newSwapAngles.All(number => number == 0))
                {
                    throw new Exception($"New values not properly configured.\nnewInvertRot = [ {newInvertRot[0]}, {newInvertRot[1]}, {newInvertRot[2]} ]\nnewSwapAngles = [ {newSwapAngles[0]}, {newSwapAngles[1]}, {newSwapAngles[2]} ]");
                }

                if (aGameObject is GameObject)
                {
                    ((GameObject)aGameObject).setInvertRotation((float[])newInvertRot.Clone());
                    ((GameObject)aGameObject).setSwapAngles((int[])newSwapAngles.Clone());
                }

                else if (aGameObject is CompositeGameObject)
                {
                    /// Do the same for it's subordinates
                    ((CompositeGameObject)aGameObject).StandardiseAxes();

                    ((CompositeGameObject)aGameObject).setInvertRotation((float[])newInvertRot.Clone());
                    ((CompositeGameObject)aGameObject).setSwapAngles((int[])newSwapAngles.Clone());

                }

            }
        }

        /// These Attributes Have Defaults
        Vector3 objectPos;
        float objectScale;
        float[] angles = new float[3];
        float[] invertRot = new float[] { 1f, 1f, 1f };
        int[] swapAngles = new int[] { 0, 1, 2 };

        /// Total Number of these objects
        private static int count;        

        /// The CompositeGameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public CompositeGameObject(float objectScaleIn = 1.0f, Vector3? objectPosIn = null, float[] anglesIn = null)
        {
            float[] angles = anglesIn ?? new float[] { 0.0f, 0.0f, 0.0f };
            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world

            setAngles(angles);

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
        
        int[] ReturnSwappedList(int[] list, int index1, int index2)
        {
            int[] newSwapAngles = list;

            int temp = newSwapAngles[index1];
            newSwapAngles[index1] = newSwapAngles[index2];
            newSwapAngles[index2] = temp;

            return newSwapAngles;
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

                /// Must invert the rotation here
                /*
                float[] invertRotInfo = (float[])aGameObject.getInvertRotation().Clone();

                for (int i = 0; i < invertRotInfo.Length; i++)
                {
                    invertRotInfo[i] = invertRotInfo[i]  * -1f;
                }
                //*/

                /*
                Matrix4 transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles, new float[] { -1f, -1f, -1f }, null);
                aGameObject.setPosition(objectPos + objectScale * new Vector3(transform * new Vector4(aGameObject.getPosition(), 1.0f)));
                //*/

                //*
                Matrix4 transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles, new float[] { -1f, -1f, -1f }, null);
                aGameObject.setPosition(objectPos + objectScale * new Vector3(transform * new Vector4(aGameObject.getPosition(), 1.0f)));
                //*/

                /*
                Matrix4 transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles, new float[] { -1f, -1f, -1f }, null) * Matrix4.CreateTranslation(objectPos) * Matrix4.CreateScale(objectScale);
                aGameObject.setPosition(objectPos + objectScale * new Vector3(transform * new Vector4(aGameObject.getPosition(), 1.0f)));
                //*/

                transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles, null, null);
                aGameObject.setTransform(aGameObject.getTransform() * transform);


                #region Set new Vertices

                /*
                float[] vertexInfo = aGameObject.getVertices();
                float[] newVertices = new float[vertexInfo.Length];
                for (int i = 0; i < vertexInfo.Length / 3; i++)
                {
                    /// Initialize the vertex that will be transformed (in this round)
                    Vector3 theVertex = new Vector3();
                    for (int j = 0; j < 3; j++)
                    {
                        theVertex[j] = vertexInfo[(i * 3) + j];
                    }

                    /// Transform the vertex
                    Vector4 transformedVector = transform * new Vector4(theVertex, 1.0f);

                    /// Add new vertex to the set of newVertices
                    for (int j = 0; j < 3; j++)
                    {
                        newVertices[(i * 3) + j] = transformedVector[j];
                    }

                }
                aGameObject.setVertices(newVertices);
                //*/
                #endregion

                aGameObject.setScale(aGameObject.getScale() * objectScale);
                aGameObject.setAngles(aGameObject.getAngles()[0] + angles[0], aGameObject.getAngles()[1] + angles[1], aGameObject.getAngles()[2] + angles[2]);
                //aGameObject.setInvertRotation(aGameObject.getInvertRotation() * invertRot);
            }

            return theGameObjects;
        }
    }

}
