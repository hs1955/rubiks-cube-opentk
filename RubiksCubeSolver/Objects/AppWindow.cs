using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubixCubeSolver.Objects;

namespace RubixCubeSolver.Objects
{
    class AppWindow : GameWindow
    {
        //------PROPERTIES
        #region Declaration of Essential Variables and Objects
        //Creating the OpenTK window class to our AppWindow window
        public AppWindow(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        //Creating Paths
        static string workingDirectory = Environment.CurrentDirectory; // WORKING directory (i.e. \bin\Debug)
        static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName; // PROJECT directory (i.e. ..\bin\)

        //Our Shader
        Shader shader;
        static string shaderFilePath = projectDirectory + "\\RubixCubeSolver\\OpenGL_Shaders\\";

        //The Vertex Buffer Object
        //Holds our vertices to render;  can store a large number of vertices in the GPU's memory.
        int VertexBufferObject;

        //The Vertex Array Object
        int VertexArrayObject;

        //Element Buffer Object, is a type of buffer that lets us reuse vertices to create multiple primitives out of them. Using an EBO, you can create a rectangle using only four vertices, while still rendering only triangles
        int ElementBufferObject;

        #endregion

        //---ADDITIONAL PROPERITES (ie. the application can function without them)

        Cube myCube = new Cube();

        
        //------METHODS
        //---KEY METHODS (ie. the application cannot function without them)
        /// <summary>
        /// Runs one time, when the window first opens. Any initialization-related code should go here.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.9f, 0.9f, 0.9f, 1.0f);

            #region Initialize the VertexBufferObject. Holds our Vertices, and how to intepret the data to obtain the vertices
            //Sending data to the graphics card from the CPU is relatively slow, so wherever we can, we try to send as much data/vertices as possible at once. Introducing: VertexBufferObject

            //This is actually the ID for the VertexBufferObject. Like any in OpenGL, it requires an ID
            VertexBufferObject = GL.GenBuffer();

            //We can bind the newly created buffer to the BufferTarget.ArrayBuffer target with the GL.BindBuffer function
            //GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //From that point on any buffer calls we make (on the BufferTarget.ArrayBuffer target) will be used to configure the currently bound buffer, which is VertexBufferObject.

            /*
            //Then we can make a call to GL.BufferData function that copies the previously defined vertex data into the buffer's memory
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            //*/
            #endregion

            #region Initialize the VertexArrayObject. VertexBufferObject is required. Basically stores the state / different vertex data and attribute configurations. Modern OpenGL requires a VAO to render at all.
            //VAO has the advantage that when configuring vertex attribute pointers you only have to make those calls once and whenever we want to draw the object, we can just bind the corresponding VAO. This makes switching between different vertex data and attribute configurations as easy as binding a different VAO. All the state we just set is stored inside the VAO.
            VertexArrayObject = GL.GenVertexArray();

            //Initialization code(done once (unless your object frequently changes))
            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject); //In Initialize, but not in Render Frame?

            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

            //We specified how OpenGL should interpret the vertex data
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            //We should also enable the vertex attribute; vertex attributes are disabled by default
            GL.EnableVertexAttribArray(0);
            #endregion

            #region Initialize the ElementBufferObject. Holds our Indices, and how to interpret the data to obtain the indices. This must be done AFTER the VAO is initialized
            //Very similar process to the VertexBufferObject: Create with GL.GenBuffer(), bind with GL.BindBuffer, and then use GL.BufferData to add data to it.
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), myCube.getIndices(), BufferUsageHint.DynamicDraw);
            #endregion

            #region Create Shader object 'shader'
            shader = new Shader(shaderFilePath + "shader.vert", shaderFilePath + "shader.frag");
            #endregion

            //shader.Use();

            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //Clears the screen, using color set in OnLoad
            GL.Clear(ClearBufferMask.ColorBufferBit);

            #region The vertices and indices to render
            float[] vertices = myCube.getVertices();
            uint[] indices = myCube.getIndices();

            //float[] vertices = myCube.getVariedVertices(100);
            #endregion

            # region Bind and Copy the previously defined vertex data into the buffer's memory

            //We can make a call to GL.BufferData function that copies the previously defined vertex data into the buffer's memory ( - while creating the new storage, any pre-existing data store is deleted).
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
            #endregion

            #region Draw stuff on screen with data given
            //GL.UseProgram(shader.Handle);
            //GL.BindVertexArray(VertexArrayObject);

            //GL.BindVertexArray(VertexArrayObject);
            shader.Use();
            
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            //The PrimitiveType. We want to use raw triangles.
            //The amount of vertices to draw. We use the length of the indices to draw everything.
            //The type of the EBO's elements. Unsigned int.
            //The offset of what we want to draw. Since we want to draw everything, we just use 0.

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            //One area is displayed, while the other is being rendered to. Then, when you call SwapBuffers, the two are reversed. A single-buffered context could have issues such as screen tearing.
            #endregion

            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                QuitApp();

                //If for some reason the application hasn't exited already, then it will here.
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        /// <summary>
        /// After the program ends, we have to manually cleanup our buffers. Binding a buffer to 0 basically sets it to null, so any calls that modify a buffer without binding one first will result in a crash. This is easier to debug than accidentally modifying a buffer that we didn't want modified.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
            shader.Dispose();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            base.OnUnload(e);
        }

        //---ADDITONAL METHODS (ie. the application can function without them)
        private void QuitApp()
        {
            //Run any code which I want to, before exitting the application, most likely message box


            //OpenTK Exit Function closes windows
            Exit();
        }
        
    }
}
