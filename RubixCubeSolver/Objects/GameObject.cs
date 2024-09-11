using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace RubixCubeSolver.Objects
{
    /// <summary>
    /// This abstract class, will allow for all types of GameObjects that can be rendered, to exist in the same List. Any class that inherits from GameObject, will become one. GameObjects are any objects, with vertices and indices, and a position.
    /// Abstract classes cannot be instantiated, but can be inherited
    /// </summary>
    public abstract class GameObject : GameMaster.IGameObject
    {
        /// Necessary Parameters - They have no defaults, and a gameobject cannot be made without them
        static float[] objectVertices;
        static uint[] objectIndices;

        ///  Every GameObject, can be referenced by it's VAO Handle. This also stores many relavant settings.
        int VAO;

        /// Special Parameters - They don't exist until a single instance of the GameObject Type is created, then the same instance exists and is reused until none of the GameObject Type exists, after which it deletes itself
        /// Note: -1 will never be a valid value, so -1 = DONT EXIST, and trying to use it will cause an error
        static int VBO = -1;
        static int EBO = -1;

        /// These Attributes Have Defaults
        Vector3 objectPos;
        Vector3 objectCol;

        float objectScale;

        /// Only Information for Other Functions to work properly
        /// Total Number of these objects
        private static int count;
        
        /// The GameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public GameObject(float[] objectVerticesIn, uint[] objectIndicesIn, Shader shader, float objectScaleIn = 1.0f, Vector3? objectPosIn = null, Vector3? objectColIn = null)
        {
            objectVertices = objectVerticesIn;
            objectIndices = objectIndicesIn;

            VAO = SetupVAO(genAndGetVBOHandle(), genAndGetEBOHandle(), shader);

            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world
            objectCol = objectColIn ?? new Vector3(1.0f, 0.3f, 0.31f);  /// R G B  This is a pink color

            count++;
        }

        public float[] getVertices()
        {
            return objectVertices;
        }
        
        public uint[] getIndices()
        {
            return objectIndices;
        }

        /// The same type of objects, have the same type of VBOs and EBOs, so these are now static, but only exist when a single type of the GameObject exists.
        /// They are regenerated once none of the GameObjects exists, and a new one is created

        public int genAndGetVBOHandle()
        {
            /// If VBO doesn't exist
            if (VBO == -1)
            {
                /// Initialize a VBO for this object
                /// Create Buffer for Object
                VBO = GL.GenBuffer();

                /// Obtain Handle to Our Buffer
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

                /// Finally, upload the vertices to the buffer.
                /// Arguments:
                /// 1) Which buffer the data should be sent to.
                /// 2) How much data is being sent, in bytes. 
                /// You can generally set this to the length of your array, multiplied by sizeof(array type).
                /// 3) The vertices themselves.
                /// 4) How the buffer will be used, so that OpenGL can write the data to the proper memory space on the GPU.
                /// 
                /// There are three different BufferUsageHints for drawing:
                /// StaticDraw: This buffer will rarely, if ever, update after being initially uploaded.
                /// 
                /// DynamicDraw: This buffer will change frequently after being initially uploaded.
                /// 
                /// StreamDraw: This buffer will change on every frame.
                /// 
                /// Writing to the proper memory space is important! Generally, you'll only want StaticDraw,
                /// But be sure to use the right one for your use case. 
                GL.BufferData(BufferTarget.ArrayBuffer, this.getVertices().Length * sizeof(float), this.getVertices(), BufferUsageHint.DynamicDraw);

            }

            return VBO;
        }
        public void delVBOHandle()
        {
            VBO = -1;
        }

        public int genAndGetEBOHandle()
        {
            /// If EBO doesn't exist
            if (EBO == -1)
            {
                /// Initialize a EBO for this object
                EBO = GL.GenBuffer();

                /// Obtain Handle to Our Buffer
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

                /// Finally, upload the vertices to the buffer.
                GL.BufferData(BufferTarget.ElementArrayBuffer, this.getIndices().Length * sizeof(uint), this.getIndices(), BufferUsageHint.DynamicDraw);

            }

            return EBO;
        }
        public void delEBOHandle()
        {
            EBO = -1;
        }

        public int getVAOHandle()
        {
            return VAO;
        }

        public Vector3 getPosition()
        {
            return objectPos;
        }
        public void setPosition(Vector3 value)
        {
            objectPos = value;
        }

        public Vector3 getColor()
        {
            return objectCol;
        }
        public void setColor(Vector3 value)
        {
            objectCol = value;
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
            /// If there aren't any more objects of this type, delete the GameObject Type's buffers
            if (getCount() == 0)
            {
                /// Delete Buffer with handle ID
                GL.DeleteBuffer(genAndGetVBOHandle());

                /// Delete (static) VBO Handle in the GameObject Type
                delVBOHandle();

                /// Delete Buffer with handle ID
                GL.DeleteBuffer(genAndGetEBOHandle());

                /// Delete (static) EBO Handle in the GameObject Type
                delEBOHandle();
            }

            /// Each GameObject has a unique VAO, so this will always be deleted whenever this function is called
            //GL.DeleteVertexArray(myGameObjectsVAOHandles[gameObjectIndex]);
            GL.DeleteVertexArray(getVAOHandle());

            /// Decrement the count of this type of GameObjects, currently present
            setCount(getCount() - 1);

            /// Delete GameObject with gameObjectIndex
            GameMaster.getGameObjects().Remove(this);
        }

        /// ADDITIONAL METHODS (and probably only for debugging)
        /// <summary>
        /// Returns vertices, except each vertex is offset randomly each time this function is called.
        /// Useful for debugging purposes
        /// </summary>
        /// <param name="sensitivity"> The greater this value, the more offset the vertices become.
        /// The minimum value is 1 </param>
        /// <returns></returns>
        public float[] getVariedVertices(double sensitivity = 1)
        {
            /// Random Instance
            Random rnd = new Random();

            /// The list where our varied vertices will be stored
            float[] variedVertices = new float[objectVertices.Length];

            /// The sensitivity has a minimum value of 1
            if (sensitivity < 1)
                sensitivity = 1;

            /// For each vertex
            for (int i = 0; i < objectVertices.Length; i++)
            {
                /// Add a randomized version, depending on the sensitivity
                variedVertices[i] = objectVertices[i] + Convert.ToSingle((rnd.NextDouble() - 0.5f) * 2 / sensitivity);
            }

            return variedVertices;
        }

        ///<summary>
        /// This function sets up a VAO, using a given VBO, EBO and Shader.
        /// VAOs store a lot of information, which can all be received from one call of our VAO:
        /// When Binding VAOs, the relavant VBO, EBO and Shader informaation is all gathered is obtained.
        /// Shader defaults to the lighting shader
        ///</summary>
        private int SetupVAO(int VBO, int EBO, Shader objectShader)
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
    }

}
