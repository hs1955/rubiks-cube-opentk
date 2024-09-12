using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace RubixCubeSolver.Objects
{
    public static class GameMaster
    {
        public static Camera _camera;

        public static bool _firstMove = true;

        public static Vector2 _lastPos;

        public interface IGameObject { }

        /// List of GameObjects
        public static List<IGameObject> myGameObjects = new List<IGameObject>();

        public static ref List<IGameObject> getGameObjects()
        {
            return ref myGameObjects;
        }

        public static List<GameObject> gameObjectsOnlyList = new List<GameObject>();

        static List<int> VBOs = new List<int>();
        static List<int> EBOs = new List<int>();
        public static void UpdateGameObjectsOnlyList()
        {
            /// For every GameObject, if any VAOs are present, delete them
            foreach (GameObject gameObject in gameObjectsOnlyList)
            {
                int VAO = gameObject.getVAOHandle();

                /// Delete any VAOs present (if any)
                if (VAO != -1)
                {
                    /// Each GameObject has a unique VAO, so this will always be deleted whenever this function is called
                    GL.DeleteVertexArray(VAO);
                    ////GL.Finish();
                }
            }

            gameObjectsOnlyList.Clear();

            foreach (var item in myGameObjects)
            {
                if (item is GameObject)
                {
                    gameObjectsOnlyList.Add(((GameObject)item).CloneThisGameObject(false));
                }

                else if (item is CompositeGameObject)
                {
                    gameObjectsOnlyList.AddRange(((CompositeGameObject)item).ConvertToGameObjects());
                }
            }

            /// Cannot assign more VBOs/EBOs than present, and cannot assign VBOs/EBOs to objects that are not present.
            int limit = Math.Min(VBOs.Count, gameObjectsOnlyList.Count);

            /// Assign as many VBOs and EBOs (pairs) as possible to each object
            for (int i = 0; i < limit; i++)
            {
                gameObjectsOnlyList[i].setVBOHandle(VBOs[i]);
                gameObjectsOnlyList[i].setEBOHandle(EBOs[i]);
            }

            /// For every GameObject, create a VBO, EBO and VAO
            foreach (GameObject gameObject in gameObjectsOnlyList)
            {
                /// Excecuting this line, makes a VBO, EBO and new VAO for aGameObject
                gameObject.SetupObject(out int VBO, out int EBO);

                /// Add any new VBOs
                if (!VBOs.Contains(VBO))
                {
                    VBOs.Add(VBO);
                }

                /// Add any new EBOs
                if (!EBOs.Contains(EBO))
                {
                    EBOs.Add(EBO);
                }
            }
        }

        /// <summary>
        /// Adds GameObjects, so they can be drawn.
        /// NOTE: Multiple of the same GameObjects will use the SAME VBOs and EBOs. 
        /// Every object however has a unique VAO.
        /// </summary>
        /// <param name="theGameObject"></param>
        /// <param name="number"></param>
        public static void AddGameObjects(IGameObject theGameObject, int number = 1)
        {
            for (int i = 0; i < number; i++)
            {
                /// Add theGameObject to the list of GameObjects
                myGameObjects.Add(theGameObject);

            }
        }

        /// <summary>
        /// Deletes a GameObject using its VAO Handle
        /// </summary>
        /// <param name="objectVAOHandle"> The VAO Handle of the GameObject </param>
        public static void DisposeGameObjectWithVAO(int objectVAOHandle)
        {
            /// Converts the given object VAO, to the Index of the Object VAO in the myGameObjectsVAOHandles List
            int gameObjectIndex = ObtainGameObjectIndex(objectVAOHandle);

            DisposeGameObjectWithIndex(gameObjectIndex);

        }

        /// <summary>
        /// Deletes a GameObject using its Index
        /// </summary>
        /// <param name="objectIndex"> The Index of the GameObject </param>
        public static void DisposeGameObjectWithIndex(int gameObjectIndex)
        {
            IGameObject theGameObject = myGameObjects[gameObjectIndex];

            /// Checks to see if a GameObject exists at this index
            if (myGameObjects.ElementAtOrDefault(gameObjectIndex) == default)
            {
                /// If there are no Game Objects
                if (myGameObjects.Count == 0)
                {
                    throw new IndexOutOfRangeException("GameObject List is empty");
                }

                else
                {
                    throw new IndexOutOfRangeException("Index is out of range, so no GameObjects exist at this index in the GameObjects List");
                }
            }

            /// If this GameObject is a regular GameObject
            if (theGameObject is GameObject)
            {
                ((GameObject)theGameObject).DisposeThisGameObject();
            }

            /// If this GameObject is a CompositeGameObject
            else if (theGameObject is CompositeGameObject)
            {
                ((CompositeGameObject)theGameObject).DisposeThisCompositeGameObject();
            }
        }

        /// <summary>
        /// Delete all Gameobjects, except for those stated in the argument of this method
        /// </summary>
        /// <param name="omitVAOs"> These are the objects to not delete </param>
        public static void DisposeAllGameObjects(List<int> omitVAOs = null)
        {
            /// Total number of VAOs before deletion
            int VAOCountBeforeDeletion = myGameObjects.Count;

            /// If there's nothing to omit from being deleted
            if (omitVAOs == null)
            {
                /// Delete all GameObjects and information
                for (int i = 0; i < VAOCountBeforeDeletion; i++)
                {
                    /// We don't care about identifying each object, so simpily delete every object without using VAOs
                    DisposeGameObjectWithIndex(0);
                }
            }

            /// If there are some objects which cannot be deleted
            else
            {
                /// For each possible VAO
                for (int i = 1; i < VAOCountBeforeDeletion; i++)
                {
                    /// If a gameObject that exists has this VAO, and the VAO is not in the list of VAOs to not delete
                    if (myGameObjects.Exists(gameObject => (gameObject is GameObject) && ((GameObject)gameObject).getVAOHandle() == i) && !omitVAOs.Contains(i))
                    {
                        /// Delete this particular GameObject, by using it's VAO
                        DisposeGameObjectWithVAO(i);
                    }

                    /// This extremely long statement, first checks if there is a CompositeGameObject in the list.
                    /// If there is then get the GameObjects that make it up
                    /// and then check those to see if any of them have a VAO which must be omitted
                    /// 
                    /// This method is probably extremely slow, but it is also very flexible: I can interact with a list with both normal GameObjects and CompositeGameObjects, using exactly the same methods.
                    else if (myGameObjects.Exists(gameObject => (gameObject is CompositeGameObject) && ((CompositeGameObject)gameObject).ConvertToGameObjects().Exists(gameObject1 => (gameObject1.getVAOHandle() == i) && !omitVAOs.Contains(i))))
                    {
                        /// Delete this particular GameObject, by using it's VAO
                        DisposeGameObjectWithVAO(i);
                    }

                }
            }

            GameObject.DisposeAllClones();
        }

        /// <summary>
        /// Obtain the Index of a Game Object, using its VAO Handle
        /// NOTE: Using this method, with the Index of the GameObject's actual VAO Handle, may result in an IndexOutOfRange Exception, but may also result in receiveing the incorrect VAO Handle Index
        /// </summary>
        /// <param name="objectVAOHandle"> The Object's VAO (every object can be identified using it's VAO, since this is unique) </param>
        /// <returns></returns>
        public static int ObtainGameObjectIndex(int objectVAOHandle)
        {
            for (int i = 0; i < myGameObjects.Count; i++)
            {
                IGameObject gameObject = myGameObjects[i];

                if (gameObject is GameObject)
                {
                    /// This line converts requested VAO Handle of an object to the VAO Handle Index in the list
                    if (((GameObject)gameObject).getVAOHandle() == objectVAOHandle)
                    {
                        return i;
                    }
                }

                else if (gameObject is CompositeGameObject)
                {
                    int gameObjectIndex = ((CompositeGameObject)gameObject).ConvertToGameObjects().FindIndex(thisGameObject => thisGameObject.getVAOHandle() == objectVAOHandle);

                    if (gameObjectIndex != -1)
                    {
                        /// This should be the index of the CompositeGameObject. 
                        /// Select a CompositeGameObject using the VAO of any of the GameObjects the Composite holds
                        return i;
                    }
                }

            }

            return -1;
        }

        /// <summary>
        /// Allows drawing of any particular arrangment of vertices and indices stored in a VAO
        /// </summary>
        /// <param name="handleVAO"></param>
        /// <param name="objectPositionInput"> Defaults to (0, 0, 0) </param>
        /// <param name="objectScale"> Defaults to 1 </param>
        /// <param name="objectColorInput"> Defaults to (1.0, 0.3, 0.31), AKA Pink </param>
        /// <param name="lightColorInput"> Defaults to (1, 1, 1), AKA pure white light </param>

        public static void Draw(int handleVAO, uint[] indices, Shader shader, Vector3? Position = null, float Scale = 1.0f, Vector3? Color = null, Vector3? lightColor = null)
        {
            /// Sets certain values to their defaults
            Vector3 objectPosition = Position ?? new Vector3(0.0f, 0.0f, 0.0f);     /// Default: The centre of the world

            Vector3 objectColor = Color ?? new Vector3(1.0f, 0.3f, 0.31f);          /// Default: This is a pink color

            Vector3 objectlightColor = lightColor ?? new Vector3(1.0f, 1.0f, 1.0f); /// Default: This is a white color

            /// Set the Transformation Matrix model
            /// Move, Scale and Rotate the object
            /// Last transformation, is rotating the object around it's centre
            //Matrix4 model = RotateInXYZAroundPoint(new Vector3(0.0f), angles, invertRot, swapAngles);

            /// Scale and move the object into the correct space in the world
            Matrix4 model = Matrix4.CreateScale(Scale) * Matrix4.CreateTranslation(objectPosition);

            /// Load the VAO (which contains our VBO, EBO, and Shader)
            GL.BindVertexArray(handleVAO);

            /// Pass Colors to the Shader, to produce an output color
            shader.SetVector3("objectColor", objectColor);
            shader.SetVector3("lightColor", objectlightColor);

            /// Pass Transformation Matrices to the Shader
            /// IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
            /// They are then transposed properly when passed to the shader.
            /// If you pass the individual matrices to the shader and multiply there, you have to do in the order "model, view, projection".
            /// But if you do it here and then pass it to the vertex, you have to do it in order "projection, view, model".
            /// This is usually done each render iteration since transformation matrices tend to change a lot

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", _camera.GetViewMatrix());
            shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            /// Enable the shader (this shader becomes enabled globally)
            shader.Use();

            /// Draw the Triangles, using the indices
            /// Arguements:
            /// 1) The PrimitiveType. We want to use raw triangles.
            /// 2) The amount of vertices to draw. We use the length of the indices to draw everything.
            /// 3) The type of the EBO's elements. Unsigned int.
            /// 4) The offset of what we want to draw. Since we want to draw everything, we just use 0, eg. you wish to omit some of the indices at the start of the uint[], then use a value bigger than 0.
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// Draws an object, based on it's ID.
        /// </summary>
        /// <param name="objectVAOHandle"> The object ID </param>
        /// <param name="trueGameObjectIndex"> Is this the objects actual ID? Default: false 
        /// (When GameObjects are deleted, the remaining objects get their actual IDs reduced, meaining the actual ID of each GameObject changes. Set to false if you wish to draw a specific GameObject)</param>
        /// <param name="lightColor"> The color of light on the object </param>
        public static void DrawWithVAO(int objectVAOHandle, bool VAOofGameObjectsOnly, Vector3? lightColor = null)
        {
            int gameObjectIndex = -1;

            if (VAOofGameObjectsOnly)
            {
                /// Obtain the Index of the VAO Handle in the gameObjectsOnlyList
                for (int i = 0; i < gameObjectsOnlyList.Count; i++)
                {
                    /// This line converts requested VAO Handle of an object to the VAO Handle Index in the list
                    if (gameObjectsOnlyList[i].getVAOHandle() == objectVAOHandle)
                    {
                        gameObjectIndex = i;
                        break;
                    }
                }

            }

            else
            {
                /// Obtain the Index of the VAO Handle in the myGameObjectsVAOHandles List
                /// This index is equivelant to the index of the GameObject in the myGameObjects List
                gameObjectIndex = ObtainGameObjectIndex(objectVAOHandle);
            }

            DrawWithIndex(gameObjectIndex, lightColor);

        }

        /// <summary>
        /// Draws an object, based on it's Index in the GameObject List.
        /// </summary>
        /// <param name="objectIndex"> The object Index </param>
        /// <param name="lightColor"> The color of light on the object </param>
        public static void DrawWithIndex(int gameObjectIndex, Vector3? lightColor = null)
        {
            GameObject gameObject = gameObjectsOnlyList[gameObjectIndex];

            Draw(gameObject.getVAOHandle(), gameObject.getIndices(), gameObject.getShader(), gameObject.getPosition(), gameObject.getScale(), gameObject.getColor(), lightColor);

        }

        public static void DrawAllGameObjects(List<int> omitVAOs = null)
        {
            omitVAOs = omitVAOs ?? new List<int>();

            /// DRAW OBJECTS

            /// If there's nothing to omit from being drawn
            if (omitVAOs.Count == 0)
            {
                UpdateGameObjectsOnlyList();

                /// Draw all GameObjects and information
                for (int i = 0; i < gameObjectsOnlyList.Count; i++)
                {
                    /// Again, since we aren't after a specific object, it makes sense to draw using the index of the game object in the myGameObjects List
                    DrawWithIndex(i);
                }

            }

            /// There are some objects which cannot be drawn
            else
            {
                /// For each GameObject, if it exists in the omitVAOs, do not draw
                for (int i = 1; i < gameObjectsOnlyList.Count + 1; i++)
                {
                    /// If a gameobject with the specified VAO is not in the list of VAOs to not draw
                    if (!omitVAOs.Contains(i))
                    {
                        /// Draw this particular GameObject, by using it's VAO
                        DrawWithVAO(i, true);
                    }
                }
            }

            GameObject.DisposeAllClones();
        }

        public static Matrix4 RotateInXYZAroundPoint(Vector3 centreRelativeToCurrentObjectCentre, float[] anglesIn = null, float[] invertRotIn = null, int[] swapAnglesIn = null)
        {
            float[] angles = anglesIn ?? new float[] { 0.0f, 0.0f, 0.0f };
            float[] invertRot = invertRotIn ?? new float[] { 1f, 1f, 1f };
            int[] swapAngles = swapAnglesIn ?? new int[] { 0, 1, 2 };

            Matrix4 transform = Matrix4.Identity * Matrix4.CreateTranslation(centreRelativeToCurrentObjectCentre);

            float XRot = angles[swapAngles[0]] * invertRot[0];
            float YRot = angles[swapAngles[1]] * invertRot[1];
            float ZRot = angles[swapAngles[2]] * invertRot[2];

            transform *= Matrix4.CreateTranslation(centreRelativeToCurrentObjectCentre) * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-XRot)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-YRot)) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-ZRot)) * Matrix4.CreateTranslation(-centreRelativeToCurrentObjectCentre);

            return transform;
        }

        public static void QuitApp()
        {
            //Run any code which I want to, before exitting the application, most likely message box

            //OpenTK Exit Function closes windows
        }

    }
}
