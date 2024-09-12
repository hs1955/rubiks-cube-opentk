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
        public static List<int> myGameObjectsIDs = new List<int>();

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
                    //GL.Finish();
                }
            }

            gameObjectsOnlyList.Clear();

            /// Add all the objects in GameObject form
            foreach (var item in myGameObjects)
            {
                if (item is GameObject)
                {
                    GameObject gameObject = ((GameObject)item).CloneThisGameObject(false);
                    gameObject.setRotatedAndPositionedVertices();
                    gameObjectsOnlyList.Add(gameObject);
                }

                else if (item is CompositeGameObject)
                {
                    gameObjectsOnlyList.AddRange(((CompositeGameObject)item).ConvertToGameObjects());
                }
            }

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
                /// Excecuting this line, makes a VBO, EBO and VAO for aGameObject
                gameObject.SetupObject(out int VBO, out int EBO);

                if (!VBOs.Contains(VBO))
                {
                    VBOs.Add(VBO);
                }

                if (!EBOs.Contains(EBO))
                {
                    EBOs.Add(EBO);
                }
                
            }

        }

        public static List<int> preparedMegaObjectsVBOHandles = new List<int>();
        public static List<int> preparedMegaObjectsEBOHandles = new List<int>();
        public static List<int> preparedMegaObjectsVAOHandles = new List<int>();
        public static List<uint[]> preparedMegaObjectsIndices = new List<uint[]>();
        public static List<Vector3> preparedMegaObjectsColors = new List<Vector3>();

        /// <summary>
        /// Group the gameObjects, by color, and then create objects, by combining every object of the same color
        /// </summary>
        public static void UpdatePreparedGameObjectList()
        {
            UpdateGameObjectsOnlyList();

            preparedMegaObjectsIndices.Clear();
            preparedMegaObjectsColors.Clear();

            /// This list is responsible for controlling access to all the GameObjects.
            /// It holds all the indexes of the GameObjects to access
            /// Access to a GameObject is removed, once a GameObject has been added to the aGroupOfObjects
            List<int> AllGameObjectsIndexes = new List<int>();
            for (int i = 0; i < gameObjectsOnlyList.Count; i++)
            {
                AllGameObjectsIndexes.Add(i);
            }

            List<List<GameObject>> groupedObjects = new List<List<GameObject>>();

            //int numberOfGameObjects = copyOfAllGameObjects.Count;
            /// First, group objects in the list of gameObjects, according to their color
            //foreach (int index in AllGameObjectsIndexes)
            for (int i = 0; i < AllGameObjectsIndexes.Count; i++)
            {
                int index = AllGameObjectsIndexes[0];
                List<GameObject> aGroupOfObjects = new List<GameObject>();

                /// Add an object
                aGroupOfObjects.Add(gameObjectsOnlyList[index]);

                /// Block access to this index, as this object has been included
                AllGameObjectsIndexes.Remove(index); i--;
            
                Vector3 colorOfQuestion = gameObjectsOnlyList[index].getColor();
                preparedMegaObjectsColors.Add(colorOfQuestion);

                /// First, group objects in the list of gameObjects, according to their color
                for (int j = 0; j < AllGameObjectsIndexes.Count; j++)
                {
                    int index2 = AllGameObjectsIndexes[j];

                    if (gameObjectsOnlyList[index2].getColor() == colorOfQuestion)
                    {
                        aGroupOfObjects.Add(gameObjectsOnlyList[index2]);
                        AllGameObjectsIndexes.Remove(index2); j--;
                    }
                }

                groupedObjects.Add(aGroupOfObjects);                
            }

            /// By here, copyOfAllGameObjects should be empty, and groupedObjects will be full

            /// Time for the magic: Combine all the of the same color objects, to create a single object with all their vertices, indices, VAO Handles, and color

            /// The number of buffers must be equivalent to the Count of any of the preparedMegaObjects Lists
            int numberOfBuffers = preparedMegaObjectsVBOHandles.Count;

            for (int groupNumber = 0; groupNumber < groupedObjects.Count; groupNumber++)
            {
                var group = groupedObjects[groupNumber];

                List<float> vertices = new List<float>();
                List<uint> indices = new List<uint>();

                for (int i = 0; i < group.Count; i++)
                {
                    GameObject gameObject = group[i];
                    float[] gameObjectVertices = gameObject.getVertices();
                    uint[] gameObjectIndices = gameObject.getIndices();
                    List<float> theVertices = new List<float>();
                    Vector4 theVertex = new Vector4();

                    Matrix4 model = Matrix4.Identity;

                    /// Transform each vertex, by model
                    model *= RotateInXYZAroundPoint(new Vector3(0.0f), gameObject.getAngles(), gameObject.getInvertRotation(), gameObject.getSwapAngles());

                    model *= Matrix4.CreateScale(gameObject.getScale()) * Matrix4.CreateTranslation(gameObject.getPosition());

                    /// Repeat for every vertex available in the GameObject
                    for (int k = 0; k < gameObjectVertices.Length / 3; k++)
                    {
                        /// Setup theVertex (basically, each vertex's X, Y, Z and 1.0f (so multiplication with Matrix4 is possible. this last value will be omitted in the final vertex)
                        for (int j = 0; j < 3; j++)
                        {
                            theVertex[j] = gameObjectVertices[theVertices.Count + j];
                        }
                        theVertex[3] = 1.0f;

                        /// Transform the vertex
                        Vector4 transformedVertices = theVertex * model;

                        /// Add the vertex to the array of vertices
                        for (int j = 0; j < 3; j++)
                        {
                            theVertices.Add(transformedVertices[j]);
                        }

                        /// Last iteration 
                        if (k == (gameObjectVertices.Length / 3) - 1)
                        {
                            vertices.AddRange(theVertices);

                            uint numberOfIndices = Convert.ToUInt32(indices.Count);

                            foreach (uint index in gameObjectIndices)
                            {
                                indices.Add(numberOfIndices + index);
                            }

                        }
                    }
                    
                }

                if (vertices.Count / 3 > indices.Count)
                {
                    throw new Exception("There are unused vertices");
                }

                float[] verticesArray = new float[vertices.Count];
                vertices.CopyTo(verticesArray, 0);

                uint[] indicesArray = new uint[indices.Count];
                indices.CopyTo(indicesArray, 0);

                /// By this point, a mega object, with the correct vertex and index information should be set up
                /// Now time to create the OpenGL buffers for the object.
                int VBO;
                int EBO;
                int VAO;

                /// If there is a buffer available for reuse, then reuse the buffer
                if (groupNumber < numberOfBuffers)
                {
                    VBO = preparedMegaObjectsVBOHandles[groupNumber];

                    /// Bind Our Buffer using the Handle
                    GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

                    /// Finally, upload the vertices to the buffer.
                    GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, verticesArray.Length * sizeof(float), verticesArray);

                    /// Initialize a EBO for this object
                    EBO = preparedMegaObjectsEBOHandles[groupNumber];

                    /// Bind Our Buffer using the Handle
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

                    /// Finally, upload the indices to the buffer.
                    GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)0, indicesArray.Length * sizeof(uint), indicesArray);

                }

                /// else, create a new buffer and store its Handles for future reuse
                else
                {
                    /// Generate a VBO, with Handle
                    /// Initialize a VBO for this object
                    /// Create Buffer for Object
                    VBO = GL.GenBuffer();

                    /// Bind Our Buffer using the Handle
                    GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

                    /// Finally, upload the vertices to the buffer.
                    GL.BufferData(BufferTarget.ArrayBuffer, verticesArray.Length * sizeof(float), verticesArray, BufferUsageHint.StreamDraw);

                    preparedMegaObjectsVBOHandles.Add(VBO);


                    ///Generate an EBO, with Handle
                    /// Initialize a EBO for this object
                    EBO = GL.GenBuffer();

                    /// Bind Our Buffer using the Handle
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

                    /// Finally, upload the indices to the buffer.
                    GL.BufferData(BufferTarget.ElementArrayBuffer, indicesArray.Length * sizeof(uint), indicesArray, BufferUsageHint.StreamDraw);

                    preparedMegaObjectsEBOHandles.Add(EBO);

                    /// Setup the VAO for this megaObject
                    VAO = GameObject.SetupVAO(Game.lightingShader, VBO, EBO);

                    /// Add the megaObject to the list of VAOHandles of prepared Objects
                    preparedMegaObjectsVAOHandles.Add(VAO);

                }

                /// Add the indices to the list of indicesArrays
                preparedMegaObjectsIndices.Add(indicesArray);
                
            }

            //*/

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
                myGameObjectsIDs.Add(ReturnFreeID());

            }
        }

        /// <summary>
        /// Returns an ID which must be added to the list of IDs
        /// </summary>
        public static int ReturnFreeID()
        {
            for (int i = 0; i < myGameObjectsIDs.Count + 1; i++)
            {
                if (!myGameObjectsIDs.Contains(i))
                {
                    return i;
                }
            }

            return -1;
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
                    else if (myGameObjects.Exists(gameObject => (gameObject is CompositeGameObject) && ((CompositeGameObject)gameObject).ConvertToGameObjects().Exists(gameObject1 => (gameObject1.getVAOHandle() == i) && !omitVAOs.Contains(i)) ))
                    {
                        /// Delete this particular GameObject, by using it's VAO
                        DisposeGameObjectWithVAO(i);
                    }

                }

                /*
                /// For each GameObject
                for (int j = 0; j < VAOCountBeforeDeletion; j++)
                {
                    IGameObject gameObject = myGameObjects[j];

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
                        gameObjectIndex = ((CompositeGameObject)gameObject).ConvertToGameObjects().FindIndex(thisGameObject => thisGameObject.getVAOHandle() == objectVAOHandle);

                        if (gameObjectIndex != -1)
                        {
                            return gameObjectIndex;
                        }
                    }
                }
                //*/
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
        public static void Draw(int handleVAO, uint[] indices, Shader shader, Vector3? position = null, float scale = 1.0f, Vector3? Color = null, Vector3? lightColor = null, float[] angles = null, float[] invertRot = null, int[] swapAngles = null)
        {
            /// Sets certain values to their defaults
            Vector3 objectPosition = position ?? new Vector3(0.0f, 0.0f, 0.0f);     /// Default: The centre of the world

            Vector3 objectColor = Color ?? new Vector3(1.0f, 0.3f, 0.31f);          /// Default: This is a pink color

            Vector3 objectlightColor = lightColor ?? new Vector3(1.0f, 1.0f, 1.0f); /// Default: This is a white color

            Matrix4 model = Matrix4.Identity;

            /// Set the Transformation Matrix model

            /// Move, Scale and Rotate the object

            /// Last transformation, is rotating the object around it's centre
            model *= RotateInXYZAroundPoint(new Vector3(0.0f), angles, invertRot, swapAngles);

            /// Scale and move the object into the correct space in the world
            model *= Matrix4.CreateScale(scale) * Matrix4.CreateTranslation(objectPosition);

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

            DrawWithIndex(gameObjectIndex, false, lightColor);

            #region OLD CODE
            /*
            /// Obtain the GameObject with the requested VAO
            IGameObject theGameObject = myGameObjects[gameObjectIndex];

            if (theGameObject is GameObject)
            {
                Draw(objectVAOHandle, ((GameObject)theGameObject).getIndices(), ((GameObject)theGameObject).getShader(), ((GameObject)theGameObject).getPosition(), ((GameObject)theGameObject).getScale(), ((GameObject)theGameObject).getColor(), lightColor);
            }

            else if (theGameObject is CompositeGameObject)
            {
                foreach (var item in ((CompositeGameObject)theGameObject).ConvertToGameObjects())
                {
                    theGameObject = item;

                    Draw(objectVAOHandle, ((GameObject)theGameObject).getIndices(), ((GameObject)theGameObject).getShader(), ((GameObject)theGameObject).getPosition(), ((GameObject)theGameObject).getScale(), ((GameObject)theGameObject).getColor(), lightColor);
                }
            }
            //*/
            #endregion

        }

        /// <summary>
        /// Draws an object, based on it's Index in the GameObject List.
        /// </summary>
        /// <param name="objectIndex"> The object Index </param>
        /// <param name="lightColor"> The color of light on the object </param>
        public static void DrawWithIndex(int gameObjectIndex, bool isIndexOfPreparedList, Vector3? lightColor = null)
        {

            if (isIndexOfPreparedList)
            {
                Draw(preparedMegaObjectsVAOHandles[gameObjectIndex], preparedMegaObjectsIndices[gameObjectIndex], Game.lightingShader, null, 1.0f, preparedMegaObjectsColors[gameObjectIndex], lightColor, null, null, null);

                return;
            }

            else
            {
                GameObject gameObject = gameObjectsOnlyList[gameObjectIndex];

                //Draw(gameObject.getVAOHandle(), gameObject.getIndices(), gameObject.getShader(), gameObject.getPosition(), gameObject.getScale(), gameObject.getColor(), lightColor, gameObject.getAngles(), gameObject.getInvertRotation(), gameObject.getSwapAngles());
                Draw(gameObject.getVAOHandle(), gameObject.getIndices(), gameObject.getShader(), gameObject.getPosition(), 1.0f, gameObject.getColor(), lightColor, null, null, null);
            }

        }

        /// <summary>
        /// Draw all the GameObjects currently added in game
        /// </summary>
        /// <param name="omitVAOs"> The VAOs of objects which should not be drawn </param>
        public static void DrawAllGameObjects(bool usePreparedList, List<int> omitVAOs = null)
        {
            //UpdateGameObjectsOnlyList();

            omitVAOs = omitVAOs ?? new List<int>();

            #region RAYCASTING APPROACH
            /*
            omitVAOs.Clear();
            /// Add every object VAO to the omitVAOs
            foreach (var item in gameObjectsOnlyList)
            {
                omitVAOs.Add(item.getVAOHandle());
            }

            Vector3 startingPositionVector = new Vector3(0.0f, 0.0f, 3.0f);
            //Vector3 startingPositionVector = _camera.Position;

            foreach (GameObject gameObjectForLine in gameObjectsOnlyList)
            {
                /// The direction of the line, from the camera, to the centre of the object
                Vector3 directionVector = gameObjectForLine.getPosition() - startingPositionVector;
                /// And set to unit length
                directionVector.Normalize();

                /// Line: (x, y, z) = _camera.Position + directionVector
                
                /// Dictionary to store how close each object that intersects with the line is.
                Dictionary<int, float> VAOTOClosenessPairs = new Dictionary<int, float>();

                foreach (GameObject gameObjectForCuboid in gameObjectsOnlyList)
                {
                    gameObjectForCuboid.genAndOutLongestXYZ(out float lengthX, out float lengthY, out float lengthZ);

                    int VAOofQuestion = gameObjectForCuboid.getVAOHandle();

                    /// These are the cuboid corner most verticies positions
                    Vector3 objectCuboidVertex1 = new Vector3(-lengthX / 2 + gameObjectForCuboid.getPosition()[0], -lengthY / 2 + gameObjectForCuboid.getPosition()[1], -lengthZ / 2 + gameObjectForCuboid.getPosition()[2]) * gameObjectForCuboid.getScale();
                    Vector3 objectCuboidVertex2 = new Vector3(lengthX / 2 + gameObjectForCuboid.getPosition()[0], lengthY / 2 + gameObjectForCuboid.getPosition()[1], lengthZ / 2 + gameObjectForCuboid.getPosition()[2]) * gameObjectForCuboid.getScale();

                    /// CHECKS
                    /// The limit of the view of the camera is 100 units, We do not necessarily have to check all 100 units.
                    /// THIS VALUE OF I IS ALSO THE VALUE OF CLOSENESS!!!
                    for (float i = 0.0f; i < 3.0f; i+=0.05f)
                    {
                        /// If the line could intersect with the cuboid in the x direction
                        bool xIntersect = startingPositionVector[0] + directionVector[0] * i > objectCuboidVertex1[0] && startingPositionVector[0] + directionVector[0] * i < objectCuboidVertex2[0];

                        if (xIntersect)
                        {
                            /// If the line could intersect with the cuboid in the y direction
                            bool yIntersect = startingPositionVector[1] + directionVector[1] * i > objectCuboidVertex1[1] && startingPositionVector[1] + directionVector[1] * i < objectCuboidVertex2[1];

                            if (yIntersect)
                            {
                                /// If the line could intersect with the cuboid in the z direction
                                bool zIntersect = startingPositionVector[2] + directionVector[2] * i > objectCuboidVertex1[2] && startingPositionVector[2] + directionVector[2] * i < objectCuboidVertex2[2];

                                /// If line does intersect with cuboid, remove from the omitVAOs
                                if (zIntersect)
                                {
                                    /// Keep checking until found collision with cuboid
                                    VAOTOClosenessPairs.Add(VAOofQuestion, i);
                                    break;
                                    
                                }
                            }
                        }

                    }

                }

                if (VAOTOClosenessPairs.Values.Count != 0)
                {
                    /// Remove the closest objects, from the objects to omit from rendering / allow these objects to render
                    float[] sortedListofValues = new float[VAOTOClosenessPairs.Count];
                    VAOTOClosenessPairs.Values.CopyTo(sortedListofValues, 0);
                    Array.Sort(sortedListofValues);

                    /// Number of cuboids to penetrate. All penetrated cuboids count as being visible to the user
                    const int penetration = 3;
                    for (int i = 0; i < penetration; i++)
                    {
                        foreach (int key in VAOTOClosenessPairs.Keys)
                        {
                            if (sortedListofValues.Length > i && VAOTOClosenessPairs[key] == sortedListofValues[i])
                            {
                                omitVAOs.Remove(key);
                                break;
                            }
                        }
                    }
                    
                }

            }
            //*/
            #endregion

            /// DRAW OBJECTS

            /// If there's nothing to omit from being drawn
            if (omitVAOs.Count == 0)
            {
                if (usePreparedList)
                {
                    UpdatePreparedGameObjectList();

                    /// Draw all GameObjects and information
                    for (int i = 0; i < preparedMegaObjectsVAOHandles.Count; i++)
                    {
                        /// Again, since we aren't after a specific object, it makes sense to draw using the index of the game object in the myGameObjects List
                        DrawWithIndex(i, true);
                    }
                }

                else
                {
                    UpdateGameObjectsOnlyList();

                    /// Draw all GameObjects and information
                    for (int i = 0; i < gameObjectsOnlyList.Count; i++)
                    {
                        /// Again, since we aren't after a specific object, it makes sense to draw using the index of the game object in the myGameObjects List
                        DrawWithIndex(i, false);
                    }
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

        public static Matrix4 RotateInXYZAroundPoint(Vector3 centreRelativeToCurrentObjectCentre, float XRotIn = 0.0f, float YRotIn = 0.0f, float ZRotIn = 0.0f, float invertXRot = 1.0f, float invertYRot = 1.0f, float invertZRot = 1.0f)
        {
            Matrix4 transform = Matrix4.Identity;

            float XRot = XRotIn * invertXRot;
            float YRot = YRotIn * invertYRot;
            float ZRot = ZRotIn * invertZRot;

            transform *= Matrix4.CreateTranslation(centreRelativeToCurrentObjectCentre) * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-XRot)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-YRot)) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-ZRot)) * Matrix4.CreateTranslation(-centreRelativeToCurrentObjectCentre);

            return transform;
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

            /// This wierd construction is to help avoid the problem of gimbal lock
            for (int i = 0; i < 3; i++)
            {
                switch (swapAngles[i])
                {
                    case 0:
                        transform *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-XRot));
                        break;

                    case 1:
                        transform *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-YRot));
                        break;

                    case 2:
                        transform *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-ZRot));
                        break;
                }
            }

            //transform *= Matrix4.CreateTranslation(centreRelativeToCurrentObjectCentre) * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-XRot)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-YRot)) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-ZRot)) * Matrix4.CreateTranslation(-centreRelativeToCurrentObjectCentre);
            transform *= Matrix4.CreateTranslation(-centreRelativeToCurrentObjectCentre);

            return transform;
        }

        public static void QuitApp()
        {
            //Run any code which I want to, before exitting the application, most likely message box

            //OpenTK Exit Function closes windows
            
        }

    }
}
