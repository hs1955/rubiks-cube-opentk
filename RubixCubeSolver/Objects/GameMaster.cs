using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace RubixCubeSolver.Objects
{
    public static class GameMaster // : GameWindow
    {
        public interface IGameObject { };

        /// List of GameObjects
        static List<GameObject> myGameObjects = new List<GameObject>();

        public static List<GameObject> getGameObjects()
        {
            return myGameObjects;
        }

        /// Declaration of Matrices for Conversions between Coordinate Systems

        /* The Coordinate Systems:
         * There are a total of 5 different coordinate systems that are of importance to us:

         * Local space (or Object space):  Local coordinates are the coordinates of your object relative to its local origin; they're the coordinates your object begins in.
         * 
         * World space: These coordinates are relative to a global origin of the world, together with many other objects also placed relative to the world's origin.
         * 
         * View space (or Eye space): Transform each coordinate so they appear as seen from the camera or viewer's point of view.
         * 
         * Clip space: Clip coordinates are processed to the -1.0 and 1.0 range and determine which vertices will end up on the screen.
         * 
         * Screen space:  A process, called viewport transform, transforms the coordinates from -1.0 and 1.0 to the coordinate range defined by GL.Viewport. 
         * 
         * The resulting coordinates are then sent to the rasterizer to turn them into fragments. AKA, this step turns the non-understandable coordinates from clip space, to coordinates that computers can understand and render properly (eg. GL.Viewport origin in bottom left instead of centre of the screen, like in clip space)
        //*/

        /// <summary>
        /// This determines the position of the model.
        /// </summary>
        static Matrix4 model;

        public static Matrix4 getModel()
        {
            return model;
        }

        public static void setModel(Matrix4 value)
        {
            model = value;
        }

        /// <summary>
        /// The matrix adding the camera of OpenGL
        /// </summary>
        static Matrix4 view;

        public static Matrix4 getView()
        {
            return view;
        }

        public static void setView(Matrix4 value)
        {
            view = value;
        }

        /// <summary>
        /// To transform vertex coordinates from view to clip-space.
        /// Also specifies a range of coordinates e.g. -1000 and 1000 in each dimension. 
        /// The projection matrix then transforms coordinates within this specified range to normalized device coordinates (-1.0, 1.0).
        /// All coordinates outside this range will not be mapped between -1.0 and 1.0 and therefore be clipped.
        /// </summary>
        static Matrix4 projection;

        public static Matrix4 getProjection()
        {
            return projection;
        }

        public static void setProjection(Matrix4 value)
        {
            projection = value;
        }

        /// <summary>
        /// This value is responsible for allowing the view to rotate vertically
        /// Vertical Rotation Number
        /// </summary>
        static float vertRotNum;

        public static float getVertRotNum()
        {
            return vertRotNum;
        }

        public static void setVertRotNum(float value)
        {
            vertRotNum = value;
        }

        public static void incVertRotNum(float value)
        {
            vertRotNum += value;
        }

        /// <summary>
        /// This value is responsible for allowing the view to rotate horizontally
        /// Horizontal Rotation Number
        /// </summary>
        static float horRotNum;

        public static float getHorRotNum()
        {
            return horRotNum;
        }

        public static void setHorRotNum(float value)
        {
            horRotNum = value;
        }

        public static void incHorRotNum(float value)
        {
            horRotNum += value;
        }

        /// <summary>
        /// Adds GameObjects, so they can be drawn.
        /// NOTE: Multiple of the same GameObjects will use the SAME VBOs and EBOs. 
        /// Every object however has a unique VAO.
        /// </summary>
        /// <param name="theGameObject"></param>
        /// <param name="number"></param>
        public static void AddGameObjects(GameObject theGameObject, int number = 1)
        {
            for (int i = 0; i < number; i++)
            {
                /// Add theGameObject to the list of GameObjects
                myGameObjects.Add(theGameObject);

            }
        }

        /*
        public static void AddGameObjects(CompositeGameObject theGameObject, int number = 1)
        {
            for (int i = 0; i < number; i++)
            {
                /// Add theGameObject to the list of GameObjects
                myGameObjects.Add(theGameObject);

            }
        }
        //*/

        /// <summary>
        /// Deletes a GameObject using its VAO Handle
        /// </summary>
        /// <param name="objectVAOHandle"> The VAO Handle of the GameObject </param>
        public static void DisposeGameObjectWithVAO(int objectVAOHandle)
        {
            #region OLD CHECK (Irrelavant now)
            /*
            /// Checks to see if the Number of VAOHandles matches the Number of Game Objects. 
            /// If not, the data has desynced, and an error will occur
            if (myGameObjects.Count != myGameObjects.Count)
            {
                throw new IndexOutOfRangeException("GameObject Information List do not have the same length with VAOHandles List");
            }
            //*/
            #endregion

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
            #region OLD CHECK (Irrelavant now)
            /*
            /// Checks to see if the Number of VAOHandles matches the Number of Game Objects. 
            /// If not, the data has desynced, and an error will occur
            if (myGameObjects.Count != myGameObjects.Count)
            {
                throw new IndexOutOfRangeException("GameObject Information List do not have the same length with VAOHandles List");
            }
            //*/
            #endregion

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

            /// Get the GameObject to deleted
            GameObject theGameObject = myGameObjects[gameObjectIndex];
            /// If multiple items are added at once, they will share the same VBO and EBO Handles, 
            /// so these cannot be deleted until all the declarations of the same VBO and EBO Handles are deleted.
            /// However, if trueValue is true, then bypass all checks, since all elements will be deleted.

            /// If there aren't any more objects of this type, delete the GameObject Type's buffers
            if (theGameObject.getCount() == 0)
            {
                /// Delete Buffer with handle ID
                GL.DeleteBuffer(theGameObject.genAndGetVBOHandle());

                /// Delete (static) VBO Handle in the GameObject Type
                theGameObject.delVBOHandle();

                /// Delete Buffer with handle ID
                GL.DeleteBuffer(theGameObject.genAndGetEBOHandle());

                /// Delete (static) EBO Handle in the GameObject Type
                theGameObject.delEBOHandle();
            }

            /// Each GameObject has a unique VAO, so this will always be deleted whenever this function is called
            //GL.DeleteVertexArray(theGameObject.getVAOHandle());
            GL.DeleteVertexArray(theGameObject.getVAOHandle());

            /// Decrement the count of this type of GameObjects, currently present
            theGameObject.setCount(theGameObject.getCount() - 1);

            /// Delete GameObject with gameObjectIndex
            myGameObjects.RemoveAt(gameObjectIndex);

            /// Delete Gameobject VAO Handle from myGameObjectsVAOHandles
            //myGameObjectsVAOHandles.RemoveAt(gameObjectIndex);

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
                /// For each GameObject
                for (int i = 1; i < VAOCountBeforeDeletion; i++)
                {
                    /// If it exists in the omitVAOs, do not delete
                    /// If a gameObject that exists has this VAO, and the VAO is not in the list of VAOs to not delete
                    if (myGameObjects.Exists(gameObject => gameObject.getVAOHandle() == i) && !omitVAOs.Contains(i))
                    {
                        /// Delete this particular GameObject, by using it's VAO
                        DisposeGameObjectWithVAO(i);
                    }
                }
            }
        }

        /// <summary>
        /// Obtain the Index of a Game Object, using its VAO Handle
        /// NOTE: Using this method, with the Index of the GameObject's actual VAO Handle, may result in an IndexOutOfRange Exception, but may also result in receiveing the incorrect VAO Handle Index
        /// </summary>
        /// <param name="objectVAOHandle"> The Object's VAO (every object can be identified using it's VAO, since this is unique) </param>
        /// <returns></returns>
        public static int ObtainGameObjectIndex(int objectVAOHandle)
        {
            /// When deleting GameObjects from the list, the Index of the VAO Handle in myGameObjectsVAOHandles would change
            /// myGameObjectsVAOHandles stores all the values of the objects original VAOs (in the order of creation of each GameObject), so even when some GameObjects are deleted, the same object can be referenced with its VAO Handle using this function
            /// This function stops us being forced to use the Index of the VAO Handle every time (which is good since this can change), and simpily use the actual VAO Handle of the object (which never changes for the lifespan of the Game Object)

            /// This line converts requested VAO Handle of an object to the VAO Handle Index in the list
            /// This variable will hold our index of the VAO Handle, which is the same as the index of the GameObject in the list of gameobjects
            int gameObjectIndex = myGameObjects.FindIndex(gameObject => gameObject.getVAOHandle() == objectVAOHandle);

            /// If the requested VAOHandle was not found, throw an error
            if (gameObjectIndex == -1)
            {
                throw new IndexOutOfRangeException($"GameObject ID: {objectVAOHandle} not found in myGameObjectsIDs");
            }

            return gameObjectIndex;
        }

        public static bool CheckGameObjectExists(int objectVAOHandle)
        {
            /// When deleting GameObjects from the list, the Index of the VAO Handle in myGameObjectsVAOHandles would change
            /// myGameObjectsVAOHandles stores all the values of the objects original VAOs (in the order of creation of each GameObject), so even when some GameObjects are deleted, the same object can be referenced with its VAO Handle using this function
            /// This function stops us being forced to use the Index of the VAO Handle every time (which is good since this can change), and simpily use the actual VAO Handle of the object (which never changes for the lifespan of the Game Object)

            /// This line converts requested VAO Handle of an object to the VAO Handle Index in the list
            /// This variable will hold our index of the VAO Handle, which is the same as the index of the GameObject in the list of gameobjects
            if (myGameObjects.Exists(gameObject => gameObject.getVAOHandle() == objectVAOHandle))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        #region OLD SETUP VAO
        /*
        ///<summary>
        /// This function sets up a VAO, using a given VBO, EBO and Shader.
        /// VAOs store a lot of information, which can all be received from one call of our VAO:
        /// When Binding VAOs, the relavant VBO, EBO and Shader informaation is all gathered is obtained.
        /// Shader defaults to the lighting shader
        ///</summary>
        private int SetupVAO(int VBO, int EBO, Shader objectShaderInput = null)
        {
            /// Defaults the objectShader to our lighting shader 
            /// (It is highly unlikely that any other shader would be used)
            Shader objectShader = objectShaderInput ?? _lightingShader;

            /// The Handle to our VAO.
            int HandleVAO = GL.GenVertexArray();
            
            /// Initialization code

            /// Bind Vertex Array Object
            GL.BindVertexArray(HandleVAO);

            /// Copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            /// The location of a vertex after being transformed by the transformation matrices
            var vertexLocation = objectShader.GetAttribLocation("aPos");

            /// We specified how OpenGL should interpret the vertex data
            /// Arguments:
            /// 1) Location of the input variable in the shader. 
            /// The layout(location = 0) line in the vertex shader explicitly sets it to 0.
            /// 
            /// 2) How many elements will be sent to the variable. 
            /// In this case, 3 floats for every vertex.
            /// 
            /// 3) The data type of the elements set, in this case float.
            /// Whether or not the data should be converted to normalized device coordinates. 
            /// In this case, false, because that's already done.
            /// 
            /// 4) The stride; this is how many bytes are between the last element of one vertex and the first element of the next. 
            /// 3 * sizeof(float) in this case.
            /// 
            /// 5) The offset; this is how many bytes it should skip to find the first element of the first vertex. 0 as of right now.
            /// Stride and Offset are mostly useful when defining texture coordinates.
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            /// We should also enable the vertex attribute; vertex attributes are disabled by default
            GL.EnableVertexAttribArray(vertexLocation);

            /// Bind VBO and EBO to the VAO...
            /// ... so that when the individual VAO is loaded, the correct VBO and EBO is loaded too.
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

            return HandleVAO;
        }
        //*/
        #endregion

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
            /// Checks to see if the given VAO is valid
            if (!CheckGameObjectExists(handleVAO))
            {
                throw new Exception($"The given VAO Handle: {handleVAO} does not exist");
            }

            /// Sets certain values to their defaults
            Vector3 objectPosition = Position ?? new Vector3(0.0f, 0.0f, 0.0f);     /// The centre of the world

            Vector3 objectColor = Color ?? new Vector3(1.0f, 0.3f, 0.31f);          /// This is a pink color

            Vector3 objectlightColor = lightColor ?? new Vector3(1.0f, 1.0f, 1.0f); /// This is a white color

            /// Load the VAO (which contains our VBO, EBO, and Shader)
            GL.BindVertexArray(handleVAO);

            /// Pass Colors to the Shader, to produce an output color
            shader.SetVector3("objectColor", objectColor);
            shader.SetVector3("lightColor", objectlightColor);

            /// Set the Transformation Matrix model
            /// Rotate the objects when the mouse is dragged across the screen
            model = Matrix4.Identity * Matrix4.CreateRotationY(horRotNum) * Matrix4.CreateRotationX(vertRotNum);

            /// Scale the object
            model *= Matrix4.CreateScale(Scale);

            /// Set the object position in the world
            model *= Matrix4.CreateTranslation(objectPosition);

            /// Pass Transformation Matrices to the Shader
            /// IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
            /// They are then transposed properly when passed to the shader.
            /// If you pass the individual matrices to the shader and multiply there, you have to do in the order "model, view, projection".
            /// But if you do it here and then pass it to the vertex, you have to do it in order "projection, view, model".
            /// This is usually done each render iteration since transformation matrices tend to change a lot

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);

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
        public static void DrawWithVAO(int objectVAOHandle, Shader shader, Vector3? lightColor = null)
        {
            /// Obtain the Index of the VAO Handle in the myGameObjectsVAOHandles List
            /// This index is equivelant to the index of the GameObject in the myGameObjects List
            int gameObjectIndex = ObtainGameObjectIndex(objectVAOHandle);

            /// Obtain the GameObject with the requested VAO
            GameObject theGameObject = myGameObjects[gameObjectIndex];

            Draw(objectVAOHandle, theGameObject.getIndices(), shader, theGameObject.getPosition(), theGameObject.getScale(), theGameObject.getColor(), lightColor);
        }

        /// <summary>
        /// Draws an object, based on it's Index in the GameObject List.
        /// </summary>
        /// <param name="objectIndex"> The object Index </param>
        /// <param name="lightColor"> The color of light on the object </param>
        public static void DrawWithIndex(int gameObjectIndex, Shader shader, Vector3? lightColor = null)
        {
            /// Obtain the GameObject with the requested VAO
            GameObject theGameObject = myGameObjects[gameObjectIndex];

            Draw(theGameObject.getVAOHandle(), theGameObject.getIndices(), shader, theGameObject.getPosition(), theGameObject.getScale(), theGameObject.getColor(), lightColor);
        }

        /// <summary>
        /// Draw all the GameObjects currently added in game
        /// </summary>
        /// <param name="omitVAOs"> The VAOs of objects which should not be drawn </param>
        public static void DrawAllGameObjects(Shader shader, List<int> omitVAOs = null)
        {
            /// If there's nothing to omit from being drawn
            if (omitVAOs == null || omitVAOs.Count == 0)
            {
                /// Draw all GameObjects and information
                for (int i = 0; i < myGameObjects.Count; i++)
                {
                    /// Again, since we aren't after a specific object, it makes sense to draw using the index of the game object in the myGameObjects List
                    DrawWithIndex(i, shader);
                }
            }

            /// There are some objects which cannot be drawn
            else
            {
                /// For each GameObject, if it exists in the omitVAOs, do not draw
                for (int i = 1; i < myGameObjects.Count + 1; i++)
                {
                    /// If a gameobject with the specified VAO exists, and the VAO is not in the list of VAOs to not draw
                    if (CheckGameObjectExists(i) && !omitVAOs.Contains(i))
                    {
                        /// Draw this particular GameObject, by using it's VAO
                        DrawWithVAO(i, shader);
                    }
                }
            }
        }

        public static void QuitApp()
        {
            //Run any code which I want to, before exitting the application, most likely message box

            //OpenTK Exit Function closes windows
        }

    }
}
