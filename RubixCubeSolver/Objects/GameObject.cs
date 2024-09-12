using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace RubixCubeSolver.Objects
{
    /// <summary>
    /// This abstract class, will allow for all types of GameObjects that can be rendered, to exist in the same List. Any class that inherits from GameObject, will become one. GameObjects are any objects, with vertices and indices, and a position.
    /// Abstract classes cannot be instantiated, but can be inherited
    /// </summary>
    public class GameObject : GameMaster.IGameObject
    {
        ///
        /// Every child class will have VBOs and EBOs that should be static, but there's no set way of implementing every subclass having static members.
        /// This is the closest variation to this, without entering code into the subclass.
        ///static Dictionary<Type, int> VBOs;

        /// Necessary Parameters - They have no defaults, and a gameobject cannot be made without them
        float[] objectVertices;
        uint[] objectIndices;
        Shader shader;

        ///  Every GameObject, can be referenced by it's VAO Handle. This also stores many relavant settings.
        int VAO;

        /// Special Parameters - They don't exist until a single instance of the GameObject Type is created, then the same instance exists and is reused until none of the GameObject Type exists, after which it deletes itself
        /// Note: NOT EXIST = -1
        int VBO = -1;
        int EBO = -1;

        /// These Attributes Have Defaults
        Vector3 objectPos;
        Vector3 objectCol;
        float objectScale;
        float[] angles = new float[2];
        Matrix4 myModel = new Matrix4();

        /// Only Information for Other Functions to work properly
        /// Total Number of these objects
        private static int count;
        private string myType;
        private bool switchYForZ;

        /// The GameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public GameObject(float[] objectVerticesIn, uint[] objectIndicesIn, Shader shaderIn, float objectScaleIn = 1.0f, float horizontalAngleIn = 0.0f, float verticalAngleIn = 0.0f, Vector3? objectPosIn = null, Vector3? objectColIn = null, bool switchYForZIn = false)
        {
            objectVertices = objectVerticesIn;
            objectIndices = objectIndicesIn;

            VAO = SetupVAO(genAndGetVBOHandle(), genAndGetEBOHandle(), shaderIn);

            shader = shaderIn;

            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world
            objectCol = objectColIn ?? new Vector3(1.0f, 0.3f, 0.31f);  /// R G B  This is a pink color

            switchYForZ = switchYForZIn;

            setAngles(horizontalAngleIn, verticalAngleIn);

            myModel = Matrix4.Identity * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(verticalAngleIn)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(horizontalAngleIn));

            count++;
        }

        /// A special GameObject which holds no information, this one is required specifically for cloning of a GameObject, and thus empty GameObjects can only exist within this class.
        public GameObject() { }

        public float[] getVertices()
        {
            return objectVertices;
        }
        /// This is private, since vertices should always be set under normal circumstances
        /// All the private methods here are specifically required for cloning an object
        void setVertices(float[] value)
        {
            objectVertices = value;
        }

        public uint[] getIndices()
        {
            return objectIndices;
        }
        void setIndices(uint[] value)
        {
            objectIndices = value;
        }

        public Shader getShader()
        {
            return shader;
        }
        void setShader(Shader value)
        {
            shader = value;
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
                GL.BufferData(BufferTarget.ArrayBuffer, this.getVertices().Length * sizeof(float), this.getVertices(), BufferUsageHint.StaticDraw);

            }

            return VBO;
        }
        void setVBOHandle(int value)
        {
            VBO = value;
        }
        public void delVBOHandle()
        {
            /// Delete Buffer with handle ID
            GL.DeleteBuffer(genAndGetVBOHandle());

            /// Remove the VBO from the Dictionary entries
            //VBOs.Remove(GetType());

            /// Set the VBO value to -1 to show it doesn't exist
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
                GL.BufferData(BufferTarget.ElementArrayBuffer, this.getIndices().Length * sizeof(uint), this.getIndices(), BufferUsageHint.StaticDraw);

            }

            return EBO;
        }
        void setEBOHandle(int value)
        {
            EBO = value;
        }
        public void delEBOHandle()
        {
            /// Delete Buffer with handle ID
            GL.DeleteBuffer(genAndGetEBOHandle());

            EBO = -1;
        }

        public int getVAOHandle()
        {
            return VAO;
        }
        public void setVAOHandle(int value)
        {
            VAO = value;
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

        public string getMyType()
        {
            return myType;
        }
        public void setMyType(string value)
        {
            myType = value;
        }

        public Matrix4 getMyModel()
        {
            return myModel;
        }
        public void setMyModel(Matrix4 value)
        {
            myModel = value;
        }

        public bool getSwitchYForZ()
        {
            return switchYForZ;
        }
        public void setSwitchYForZ(bool value)
        {
            switchYForZ = value;
        }

        public void DisposeThisGameObject(bool isThisObjectPartOfComposite = false)
        {
            /// If there aren't any more objects of this type, delete the GameObject Type's buffers
            if (true) //(getCount() == 0)
            {
                /// Delete VBO Handle in the GameObject Type
                delVBOHandle();

                /// Delete EBO Handle in the GameObject Type
                delEBOHandle();
            }

            /// Each GameObject has a unique VAO, so this will always be deleted whenever this function is called
            //GL.DeleteVertexArray(myGameObjectsVAOHandles[gameObjectIndex]);
            GL.DeleteVertexArray(getVAOHandle());

            /// Decrement the count of this type of GameObjects, currently present
            setCount(getCount() - 1);

            /// If this object is part of a composite object, don't bother deleting the gameobject from the master list of gameobjects (since this won't exist in the master list).
            /// This will be handled by the CompositeGameObject's Version of DisposeThisCompositeGameObject
            if (!isThisObjectPartOfComposite)
            {
                if (!GameMaster.getGameObjects().Contains(this))
                {
                    throw new Exception($"This GameObject doesn't exist in the Master GameObjects List: {this}");
                }

                /// Delete GameObject
                GameMaster.getGameObjects().Remove(this);
            }
            
        }

        /// <summary>
        /// Allows for cloning of an object
        /// </summary>
        /// <param name="perfectClone"> If true, than a true copy of the object is created with unique VAO, (must still be added to the master list of IGameObjects to be used however). Only set to false when you want to use a modified version of this object, without affecting the original object. </param>
        /// <returns></returns>
        public GameObject CloneThisGameObject(bool perfectClone = true)
        {
            if (perfectClone)
            {
                return new GameObject(objectVertices, objectIndices, shader, objectScale, getAngles()[0], getAngles()[1], objectPos, objectCol);
            }

            /// if false, then a very special semiclone is created.
            /// This semiclone will have the same properties as the original, but has the same VAO as the current object, allowing for changes to be made to this object, which will not effect the original object.
            
            /// A very special type of GameObject which is empty. Used for placing in new information manually (like in cloning)
            GameObject clonedGameObject = new GameObject();

            /// Set the vertices, indices and shader which will be used to render this object
            clonedGameObject.setVertices(objectVertices);
            clonedGameObject.setIndices(objectIndices);
            clonedGameObject.setShader(shader);

            /// Set the Handles: VBO, EBO and VAO with the same values as the current object, since this object doesn't exist in the main list of GameObjects
            clonedGameObject.setVBOHandle(VBO);
            clonedGameObject.setEBOHandle(EBO);
            clonedGameObject.VAO = VAO;

            /// Set the position, scale and color to the same as this object
            clonedGameObject.setPosition(objectPos);
            clonedGameObject.setScale(objectScale);
            clonedGameObject.setColor(objectCol);
            clonedGameObject.setAngles(getAngles()[0], getAngles()[1]);
            clonedGameObject.setMyModel(myModel);
            clonedGameObject.setSwitchYForZ(switchYForZ);

            /// Save the type of this object in a separate variable
            /// Although this GameObject renders exactly like it's original, it cannot be casted into it's original's type, so the only way to distinguish what type it is, is by using a separate variable to store the string
            clonedGameObject.setMyType(GetType().ToString());

            //clone = true;

            return clonedGameObject;
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

            /// Also checks to make sure the shader is present
            if (objectShader == null)
            {
                throw new Exception("You must assign a shader to setup a VAO.");
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

    }

}
