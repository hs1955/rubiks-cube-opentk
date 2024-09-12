﻿using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// This list stores clones, so that they can be deleted properly
        private static List<GameObject> clones = new List<GameObject>();

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

        static Dictionary<string, int> VBOs = new Dictionary<string, int>();
        static Dictionary<string, int> EBOs = new Dictionary<string, int>();

        /// These Attributes Have Defaults
        Vector3 objectPos;
        Vector3 objectCol;
        float objectScale;
        bool hide = false;

        float[] angles = new float[3] { 0.0f, 0.0f, 0.0f };
        float[] invertRot = new float[3] { 1.0f, 1.0f, 1.0f };
        int[] swapAngles = new int[3] { 0, 1, 2 };

        /// Only Information for Other Functions to work properly

        static Dictionary<string, int> count = new Dictionary<string, int>();
        static Dictionary<string, float> longestX = new Dictionary<string, float>();
        static Dictionary<string, float> longestY = new Dictionary<string, float>();
        static Dictionary<string, float> longestZ = new Dictionary<string, float>();

        float lengthX = -1.0f;
        float lengthY = -1.0f;
        float lengthZ = -1.0f;

        private string myType;

        /// The GameObject Constructor. Here is where all the information is initialized and set (including default values), upon creation of the object
        public GameObject(float[] objectVerticesIn, uint[] objectIndicesIn, Shader shaderIn, float objectScaleIn = 1.0f, Vector3? objectPosIn = null, Vector3? objectColIn = null, float[] anglesIn = null, float[] invertIn = null, int[] swapAnglesIn = null)
        {
            objectVertices = objectVerticesIn;
            objectIndices = objectIndicesIn;

            shader = shaderIn;

            objectScale = objectScaleIn;

            objectPos = objectPosIn ?? new Vector3(0.0f);               /// (0, 0, 0) is the centre of the world
            objectCol = objectColIn ?? new Vector3(1.0f, 0.3f, 0.31f);  /// R G B  This is a pink color

            angles = anglesIn ?? angles;
            invertRot = invertIn ?? invertRot;
            swapAngles = swapAnglesIn ?? swapAngles;

            setAngles(new float[] { angles[0], angles[1], angles[2] });
            setInvertRotation(new float[] { invertRot[0], invertRot[1], invertRot[2] });

            /// If this object has not been created yet, generate VBO and EBO for GameObject, and store in dictionary, for use.
            if (!count.ContainsKey(this.GetType().ToString()))
            {
                genAndGetVBOHandle();
                genAndGetEBOHandle();
                count.Add(this.GetType().ToString(), 1);
            }

            /// Setup Object, with thier VAO
            VAO = SetupVAO(shader, genAndGetVBOHandle(), genAndGetEBOHandle());

            string key = this.GetType().ToString();

            setCount(key, getCount(key) + 1);

        }

        /// A special GameObject which holds no information, this one is required specifically for cloning of a GameObject, and thus empty GameObjects can only exist within this class.
        public GameObject() { }

        public float[] getVertices()
        {
            return objectVertices;
        }
        /// This is private, since vertices should always be set under normal circumstances
        /// All the private methods here are specifically required for cloning an object
        public void setVertices(float[] value)
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
            /// If the VBO is not -1, then return the value straight away
            /// This is highly likely to be the case for cloned objects (which always have type GameObject) 
            /// (Clones never have their original object's type)
            if (VBO != -1)
            {
                return VBO;
            }

            string key = this.GetType().ToString();

            /// If VBO doesn't exist, create one
            if (!VBOs.ContainsKey(key))
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

                GL.Finish();

                VBOs.Add(key, VBO);

                return VBO;

            }

            /// else: The object is not a clone, and a VBO does exist for it, so fetch it from the dictionary
            return VBOs[key];
            
        }

        public int genAndUpdateStreamVBO()
        {
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
                GL.BufferData(BufferTarget.ArrayBuffer, this.getVertices().Length * sizeof(float), this.getVertices(), BufferUsageHint.StreamDraw);

                GL.Finish();

            }

            else
            {
                /// Bind Our Buffer using the Handle
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

                /// Finally, upload the vertices to the buffer.
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, objectVertices.Length * sizeof(float), objectVertices);
            }

            return VBO;
        }

        /*
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
        //*/

        public void setVBOHandle(int value)
        {
            VBO = value;
        }
        public void delVBOHandle()
        {
            string key = this.GetType().ToString();

            /// Delete Buffer with handle ID
            GL.DeleteBuffer(genAndGetVBOHandle());

            GL.Finish();

            /// Remove the VBO from the Dictionary entries
            VBOs.Remove(key);

            /// Set the VBO value to -1 to show it doesn't exist
            VBO = -1;
        }

        public int genAndGetEBOHandle()
        {
            /// If the EBO is not -1, then return the value straight away
            /// This is highly likely to be the case for cloned objects, with type GameObject, and not their original object's type
            if (EBO != -1)
            {
                return EBO;
            }

            string key = this.GetType().ToString();

            /// If EBO doesn't exist, create one
            if (!EBOs.ContainsKey(key))
            {
                /// Initialize a EBO for this object
                EBO = GL.GenBuffer();

                /// Obtain Handle to Our Buffer
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

                /// Finally, upload the vertices to the buffer.
                GL.BufferData(BufferTarget.ElementArrayBuffer, this.getIndices().Length * sizeof(uint), this.getIndices(), BufferUsageHint.StaticDraw);

                GL.Finish();

                EBOs.Add(key, EBO);

                return EBO;

            }

            /// else: The object is not a clone, and a VBO does exist for it, so fetch it from the dictionary
            return EBOs[key];

        }

        public int genAndUpdateStreamEBO()
        {
            if (EBO == -1)
            {
                /// Initialize a EBO for this object
                EBO = GL.GenBuffer();

                /// Obtain Handle to Our Buffer
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

                /// Finally, upload the vertices to the buffer.
                GL.BufferData(BufferTarget.ElementArrayBuffer, this.getIndices().Length * sizeof(uint), this.getIndices(), BufferUsageHint.StreamDraw);

                GL.Finish();
            }

            else
            {
                /// Bind Our Buffer using the Handle
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

                /// Finally, upload the indices to the buffer.
                GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)0, objectIndices.Length * sizeof(uint), objectIndices);
            }

            return EBO;
        }

        /*
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
        //*/

        public void setEBOHandle(int value)
        {
            EBO = value;
        }
        public void delEBOHandle()
        {
            string key = this.GetType().ToString();

            /// Delete Buffer with handle ID
            GL.DeleteBuffer(genAndGetEBOHandle());

            GL.Finish();

            EBOs.Remove(key);

            EBO = -1;
        }

        public int getVAOHandle()
        {
            /// Bind VBO and EBO to the VAO...
            /// ... so that when the individual VAO is loaded, the correct VBO and EBO is loaded too.
            //GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);

            return VAO;
        }

        public void SetupObject(out int VBOHandle, out int EBOHandle)
        {
            VBOHandle = genAndUpdateStreamVBO();
            EBOHandle = genAndUpdateStreamEBO();

            VAO = SetupVAO(shader, VBOHandle, EBOHandle);
        }

        public void genAndOutLongestXYZ(out float XDistance, out float YDistance, out float ZDistance)
        {
            /// If the length values already exist (in this case, for a clone)
            if (lengthX != -1)
            {
                XDistance = lengthX;
                YDistance = lengthY;
                ZDistance = lengthZ;
                return;
            }

            string key = this.GetType().ToString();

            /// If LongestXYZ doesn't exist, create one. (I could have checked longestY or Z too, this would have not made a difference, as all change together)
            if (!longestX.ContainsKey(key))
            {
                float[] XCoordinates = new float[objectVertices.Length / 3];
                float[] YCoordinates = new float[objectVertices.Length / 3];
                float[] ZCoordinates = new float[objectVertices.Length / 3];

                for (int i = 0; i < objectVertices.Length; i++)
                {
                    /// Integer divide for the index we will be accessing
                    int index = i / 3;

                    float vertex = objectVertices[i];

                    switch (i % 3)
                    {
                        case 0:
                            XCoordinates[index] = vertex;
                            break;

                        case 1:
                            YCoordinates[index] = vertex;
                            break;

                        case 2:
                            ZCoordinates[index] = vertex;
                            break;
                    }
                }

                const float minLength = 0.1f;

                XDistance = XCoordinates.Max() - XCoordinates.Min();
                if (0 <= XDistance && XDistance < minLength) XDistance = minLength;
                else if (0 >= XDistance && XDistance > -minLength) XDistance = -minLength;
                longestX.Add(key, XDistance);

                YDistance = YCoordinates.Max() - YCoordinates.Min();
                if (0 < YDistance && YDistance < minLength) YDistance = minLength;
                else if (0 > YDistance && YDistance > -minLength) YDistance = -minLength;
                longestY.Add(key, YDistance);

                ZDistance = ZCoordinates.Max() - ZCoordinates.Min();
                if (0 <= ZDistance && ZDistance < minLength) ZDistance = minLength;
                else if (0 >= ZDistance && ZDistance > -minLength) ZDistance = -minLength;
                longestZ.Add(key, ZDistance);

                return;

            }

            /// else: The object has its values already generated and is not a clone, so fetch the values from the dictionary
            XDistance = longestX[key];
            YDistance = longestY[key];
            ZDistance = longestZ[key];

        }

        void setLongestXYZ(float valueX, float valueY, float valueZ)
        {
            string key = this.GetType().ToString();

            lengthX = valueX;
            lengthY = valueY;
            lengthZ = valueZ;
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

        public int getCount(string key)
        {
            return count[key];
        }
        public void setCount(string key, int value)
        {
            count.Remove(key);
            count.Add(key, value);
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
                /// Here, control which angles affect which axis - most useful for GameObjects part of CompositeGameObjects, and rotations are not according to standardised axes
                angles[i] = anglesIn[i] % 360;
            }

        }
        public void setAngles(float XAngle, float YAngle, float ZAngle)
        {
            setAngles(new float[3] { XAngle, YAngle, ZAngle});
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
        public void ReorderAngles(int[] order)
        {
            int[] reorderedAngles = new int[3];

            for (int i = 0; i < reorderedAngles.Length; i++)
            {
                reorderedAngles[i] = swapAngles[order[i]];
            }

            swapAngles = reorderedAngles;
        }

        public string getMyType()
        {
            return myType;
        }
        public void setMyType(string value)
        {
            myType = value;
        }

        public bool getHide()
        {
            return hide;
        }
        public void setHide(bool value)
        {
            hide = value;
        }

        public void DisposeThisGameObject(bool isThisObjectPartOfComposite = false)
        {
            string key = this.GetType().ToString();

            if (key.EndsWith("GameObject"))
            {
                key = myType;
            }

            /// Decrement the count of this type of GameObjects, currently present
            setCount(key, getCount(key) - 1);

            /// If there aren't any more objects of this type, delete the GameObject Type's buffers, and remove according information/handles
            if (getCount(key) == 0)
            {
                /// Delete VBO Handle in the GameObject Type
                delVBOHandle();

                /// Delete EBO Handle in the GameObject Type
                delEBOHandle();

                count.Remove(key);
            }

            /// Each GameObject has a unique VAO, so this will always be deleted whenever this function is called
            GL.DeleteVertexArray(getVAOHandle());

            GL.Finish();

            /// If this object is part of a composite object, don't bother deleting the gameobject from the master list of gameobjects (since this won't exist in the master list).
            /// This will be handled by the CompositeGameObject's version of DisposeThisCompositeGameObject
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
                return new GameObject(objectVertices, objectIndices, shader, objectScale, objectPos, objectCol, angles, invertRot, swapAngles);
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
            if (VBO != -1 && EBO != -1)
            {
                clonedGameObject.setVBOHandle(VBO);
                clonedGameObject.setEBOHandle(EBO);
            }
            if (VAO != -1)
            {
                clonedGameObject.VAO = VAO;
            }

            /// Set the position, scale and color to the same as this object
            clonedGameObject.setPosition(objectPos);
            clonedGameObject.setScale(objectScale);
            clonedGameObject.setColor(objectCol);

            /// Angle the object correctly
            clonedGameObject.setSwapAngles(swapAngles[0], swapAngles[1], swapAngles[2]);
            clonedGameObject.setAngles(angles[0], angles[1], angles[2]);
            clonedGameObject.setInvertRotation(invertRot[0], invertRot[1], invertRot[2]);

            #region RACASTING APPROACH (Detection Box)
            //genAndOutLongestXYZ(out lengthX, out lengthY, out lengthZ);
            /// This is responsible for speeding up rendering, by omitting shapes behind others
            //clonedGameObject.setLongestXYZ(lengthX, lengthY, lengthZ);
            #endregion

            /// Save the type of this object in a separate variable
            /// Although this GameObject renders exactly like it's original, it cannot be casted into it's original's type, so the only way to distinguish what type it is, is by using a separate variable to store the string
            clonedGameObject.setMyType(GetType().ToString());

            clones.Add(clonedGameObject);

            return clones[clones.Count - 1];
        }

        /// <summary>
        /// Dispose of all the cloned objects
        /// </summary>
        public static void DisposeAllClones()
        {
            clones.Clear();
        }

        ///<summary>
        /// This function sets up a VAO, using a given VBO, EBO and Shader.
        /// VAOs store a lot of information, which can all be received from one call of our VAO:
        /// When Binding VAOs, the relavant VBO, EBO and Shader informaation is all gathered is obtained.
        /// Shader defaults to the lighting shader
        ///</summary>
        public static int SetupVAO(Shader objectShader, int VBO, int EBO)
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

            //GL.Finish();

            return HandleVAO;
        }
        public Matrix4 setRotatedAndPositionedVertices(bool isPartOfComposite = false)
        {
            if (hide)
            {
                return Matrix4.Zero;
            }

            /// Transform each vertex, by transform, so the object is:
            ///     * Rotated
            ///     * Scaled
            ///     * Positioned
            Matrix4 transform = GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles) * Matrix4.CreateScale(objectScale) * Matrix4.CreateTranslation(objectPos);

            if (isPartOfComposite)
            {
                return GameMaster.RotateInXYZAroundPoint(new Vector3(0.0f), angles) * Matrix4.CreateScale(objectScale) * Matrix4.CreateTranslation(objectPos);
            }

            float[] newVertices = new float[objectVertices.Length];

            /// Repeat for every vertex available in the GameObject
            for (int k = 0; k < objectVertices.Length / 3; k++)
            {
                /// For each Vertex
                /*
                Vector4 theVertex = new Vector4();

                /// Setup theVertex (basically, each vertex's X, Y, Z and 1.0f (so multiplication with Matrix4 is possible. this last value will be omitted in the final vertex)
                
                for (int j = 0; j < 3; j++)
                {
                    theVertex[j] = objectVertices[k * 3 + j];
                }
                theVertex[3] = 1.0f;
                //*/

                Vector4 theVertex = new Vector4(objectVertices[k * 3], objectVertices[k * 3 + 1], objectVertices[k * 3 + 2], 1.0f);


                /// Transform the vertex
                Vector4 transformedVertex = transform * theVertex;

                /// Add the vertex to the array of vertices
                for (int j = 0; j < 3; j++)
                {
                    newVertices[k * 3 + j] = transformedVertex[j];
                }
            }

            setVertices(newVertices);

            return Matrix4.Identity;
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
