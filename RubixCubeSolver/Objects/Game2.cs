using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace RubixCubeSolver.Objects
{
    partial class Game : GameWindow
    {
        /// List of GameObjects
        List<GameObject> myGameObjects = new List<GameObject>();

        /// NOTE: The Index of The VAO Handle of a GameObject, matches the Index of the according GameObject in the GameObjects List
        /// There can be many cubes in the myGameObjects List...
        /// ... but every single one will have a unique VAO (It's like an ID to the particular GameObject)

        /// List of GameObjects VAO Handles 
        /// VAOs BEGIN COUNTING FROM 1 - USE VAO HANDLE OF OBJECT TO REFERENCE A SPECIFIC OBJECT.
        List<int> myGameObjectsVAOHandles = new List<int>();

        /// <summary>
        /// Adds GameObjects, so they can be drawn.
        /// NOTE: Multiple of the same GameObjects will use the SAME VBOs and EBOs. 
        /// Every object however has a unique VAO.
        /// </summary>
        /// <param name="theGameObject"></param>
        /// <param name="number"></param>
        private void AddGameObjects(GameObject theGameObject, int number = 1)
        {
            /// Initialize a VBO for this object
            int VBO = theGameObject.genAndGetVBOHandle();

            /// Initialize a EBO for this object
            int EBO = theGameObject.genAndGetEBOHandle();

            for (int i = 0; i < number; i++)
            {
                /// Both checks below make sure an error occurs if the object VBO and EBO aren't generated/obtained properly
                /// -1 is the value the VBO and EBO Handles hold when they don't exist
                if (VBO == -1)
                {
                    throw new Exception("VBO of object not correctly generated");
                }

                if (EBO == -1)
                {
                    throw new Exception("EBO of object not correctly generated");
                }

                /// Add theGameObject to the list of GameObjects
                myGameObjects.Add(theGameObject);

                /// Initialize a VAO for this object
                int VAO = SetupVAO(VBO, EBO);

                /// Add the VAO of the Object to the List of VAOs
                myGameObjectsVAOHandles.Add(VAO);
            }
        }

        /// <summary>
        /// Deletes a GameObject using its VAO Handle
        /// </summary>
        /// <param name="objectVAOHandle"> The VAO Handle of the GameObject </param>
        private void DisposeGameObjectWithVAO(int objectVAOHandle)
        {
            /// Checks to see if the Number of VAOHandles matches the Number of Game Objects. 
            /// If not, the data has desynced, and an error will occur
            if (myGameObjectsVAOHandles.Count != myGameObjects.Count)
            {
                throw new IndexOutOfRangeException("GameObject Information List do not have the same length with VAOHandles List");
            }

            /// Converts the given object VAO, to the Index of the Object VAO in the myGameObjectsVAOHandles List
            int gameObjectIndex = ObtainGameObjectIndex(objectVAOHandle);

            DisposeGameObjectWithIndex(gameObjectIndex);

        }

        /// <summary>
        /// Deletes a GameObject using its Index
        /// </summary>
        /// <param name="objectIndex"> The Index of the GameObject </param>
        private void DisposeGameObjectWithIndex(int gameObjectIndex)
        {
            /// Checks to see if the Number of VAOHandles matches the Number of Game Objects. 
            /// If not, the data has desynced, and an error will occur
            if (myGameObjectsVAOHandles.Count != myGameObjects.Count)
            {
                throw new IndexOutOfRangeException("GameObject Information List do not have the same length with VAOHandles List");
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
            GL.DeleteVertexArray(myGameObjectsVAOHandles[gameObjectIndex]);

            /// Decrement the count of this type of GameObjects, currently present
            theGameObject.setCount(theGameObject.getCount() - 1);

            /// Delete GameObject with gameObjectIndex
            myGameObjects.RemoveAt(gameObjectIndex);

            /// Delete Gameobject VAO Handle from myGameObjectsVAOHandles
            myGameObjectsVAOHandles.RemoveAt(gameObjectIndex);

        }

        /// <summary>
        /// Delete all Gameobjects, except for those stated in the argument of this method
        /// </summary>
        /// <param name="omitVAOs"> These are the objects to not delete </param>
        private void DisposeAllGameObjects(List<int> omitVAOs = null)
        {
            /// If there's nothing to omit from being deleted
            if (omitVAOs == null)
            {
                /// Delete all GameObjects and information
                for (int i = 0; i < myGameObjectsVAOHandles.Count; i++)
                {
                    /// We don't care about identifying each object, so simpily delete every object without using VAOs
                    DisposeGameObjectWithIndex(i);
                }
            }

            /// If there are some objects which cannot be deleted
            else
            {
                /// For each GameObject
                for (int i = 1; i < myGameObjectsVAOHandles.Count + 1; i++)
                {
                    /// If it exists in the omitVAOs, do not delete
                    /// If the VAO exists in the list of VAOs, and the VAO is not in the list of VAOs to not delete
                    if (myGameObjectsVAOHandles.Contains(i) && !omitVAOs.Contains(i))
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
        private int ObtainGameObjectIndex(int objectVAOHandle)
        {
            /// When deleting GameObjects from the list, the Index of the VAO Handle in myGameObjectsVAOHandles would change
            /// myGameObjectsVAOHandles stores all the values of the objects original VAOs (in the order of creation of each GameObject), so even when some GameObjects are deleted, the same object can be referenced with its VAO Handle using this function
            /// This function stops us being forced to use the Index of the VAO Handle every time (which is good since this can change), and simpily use the actual VAO Handle of the object (which never changes for the lifespan of the Game Object)

            /// This line converts requested VAO Handle of an object to the VAO Handle Index in the list
            /// This variable will hold our index of the VAO Handle, which is the same as the index of the GameObject in the list of gameobjects
            int gameObjectIndex = myGameObjectsVAOHandles.FindIndex(thisID => thisID == objectVAOHandle);

            /// If the requested VAOHandle was not found, throw an error
            if (gameObjectIndex == -1)
            {
                throw new IndexOutOfRangeException($"GameObject ID: {objectVAOHandle} not found in myGameObjectsIDs");
            }

            return gameObjectIndex;
        }

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

        /// <summary>
        /// Allows drawing of any particular arrangment of vertices and indices stored in a VAO
        /// </summary>
        /// <param name="handleVAO"></param>
        /// <param name="objectPositionInput"> Defaults to (0, 0, 0) </param>
        /// <param name="objectScale"> Defaults to 1 </param>
        /// <param name="objectColorInput"> Defaults to (1.0, 0.3, 0.31), AKA Pink </param>
        /// <param name="lightColorInput"> Defaults to (1, 1, 1), AKA pure white light </param>
        private void Draw(int handleVAO, uint[] indices, Vector3? Position = null, float Scale = 1.0f, Vector3? Color = null, Vector3? lightColor = null)
        {
            /// Checks to see if the given VAO is valid
            if (!myGameObjectsVAOHandles.Contains(handleVAO))
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
            _lightingShader.SetVector3("objectColor", objectColor);
            _lightingShader.SetVector3("lightColor", objectlightColor);

            /// Scale the object
            _model *= Matrix4.CreateScale(Scale);

            /// Set the object position in the world
            _model *= Matrix4.CreateTranslation(objectPosition);

            /// Pass Transformation Matrices to the Shader
            /// IMPORTANT: OpenTK's matrix types are transposed from what OpenGL would expect - rows and columns are reversed.
            /// They are then transposed properly when passed to the shader.
            /// If you pass the individual matrices to the shader and multiply there, you have to do in the order "model, view, projection".
            /// But if you do it here and then pass it to the vertex, you have to do it in order "projection, view, model".
            /// This is usually done each render iteration since transformation matrices tend to change a lot

            _lightingShader.SetMatrix4("model", _model);
            _lightingShader.SetMatrix4("view", _view);
            _lightingShader.SetMatrix4("projection", _projection);

            /// Enable the shader (this shader becomes enabled globally)
            _lightingShader.Use();

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
        private void DrawWithVAO(int objectVAOHandle, Vector3? lightColor = null)
        {
            /// Obtain the Index of the VAO Handle in the myGameObjectsVAOHandles List
            /// This index is equivelant to the index of the GameObject in the myGameObjects List
            int gameObjectIndex = ObtainGameObjectIndex(objectVAOHandle);

            /// Obtain the GameObject with the requested VAO
            GameObject theGameObject = myGameObjects[gameObjectIndex];

            Draw(objectVAOHandle, theGameObject.getIndices(), theGameObject.getPosition(), theGameObject.getScale(), theGameObject.getColor(), lightColor);
        }

        /// <summary>
        /// Draws an object, based on it's Index in the GameObject List.
        /// </summary>
        /// <param name="objectIndex"> The object Index </param>
        /// <param name="lightColor"> The color of light on the object </param>
        private void DrawWithIndex(int gameObjectIndex, Vector3? lightColor = null)
        {
            /// Obtain the GameObject with the requested VAO
            GameObject theGameObject = myGameObjects[gameObjectIndex];

            Draw(myGameObjectsVAOHandles[gameObjectIndex], theGameObject.getIndices(), theGameObject.getPosition(), theGameObject.getScale(), theGameObject.getColor(), lightColor);
        }

        /// <summary>
        /// Draw all the GameObjects currently added in game
        /// </summary>
        /// <param name="omitVAOs"> The VAOs of objects which should not be drawn </param>
        private void DrawAllGameObjects(List<int> omitVAOs = null)
        {
            /// If there's nothing to omit from being drawn
            if (omitVAOs == null)
            {
                /// Draw all GameObjects and information
                for (int i = 0; i < myGameObjectsVAOHandles.Count; i++)
                {
                    /// Again, since we aren't after a specific object, it makes sense to draw using the index of the game object in the myGameObjects List
                    DrawWithIndex(i);
                }
            }

            /// There are some objects which cannot be drawn
            else
            {
                /// For each GameObject, if it exists in the omitVAOs, do not draw
                for (int i = 1; i < myGameObjectsVAOHandles.Count + 1; i++)
                {
                    /// If the VAO exists in the list of VAOs, and the VAO is not in the list of VAOs to not draw
                    if (myGameObjectsVAOHandles.Contains(i) && !omitVAOs.Contains(i))
                    {
                        /// Draw this particular GameObject, by using it's VAO
                        DrawWithVAO(i);
                    }
                }
            }
        }

        private void QuitApp()
        {
            //Run any code which I want to, before exitting the application, most likely message box

            //OpenTK Exit Function closes windows
            Exit();
        }

    }
}
