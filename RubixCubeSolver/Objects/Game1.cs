using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace RubixCubeSolver.Objects
{
    partial class Game : GameWindow
    {
        ///------PROPERTIES
        #region Declaration of Essential Variables and Objects

        /// Our Shader
        private string _shaderFilePath = Path.GetFullPath("OpenGL_Shaders/");

        /// Our Lamp Shader
        private Shader _lightingShader;

        /// Creating the OpenTK window class to our Game window
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

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
        Matrix4 _model;

        /// <summary>
        /// The matrix adding the camera of OpenGL
        /// </summary>
        Matrix4 _view;

        /// <summary>
        /// To transform vertex coordinates from view to clip-space.
        /// Also specifies a range of coordinates e.g. -1000 and 1000 in each dimension. 
        /// The projection matrix then transforms coordinates within this specified range to normalized device coordinates (-1.0, 1.0).
        /// All coordinates outside this range will not be mapped between -1.0 and 1.0 and therefore be clipped.
        /// </summary>
        Matrix4 _projection;

        /// <summary>
        /// This value is responsible for allowing the view to rotate vertically
        /// Vertical Rotation Number
        /// </summary>
        float vertRotNum;

        /// <summary>
        /// This value is responsible for allowing the view to rotate horizontally
        /// Horizontal Rotation Number
        /// </summary>
        float horRotNum;

        /// <summary>
        /// Holds the mouse previous state (and needs a default value initially)
        /// </summary>
        MouseState prevMouse = Mouse.GetCursorState();

        /// <summary>
        /// This variable defines differentiates between the mouse being dragged, and the mouse being first clicked:   
        /// true = first click || false = being dragged
        /// </summary>
        bool mouseNotPressedPrevFrame = true;

        float mouseCameraSensitivity = 100f;

        #endregion

        /// <summary>
        /// Runs one time, when the window first opens. Any initialization-related code should go here.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            /// Setup of shaderFilePath
            if (_shaderFilePath.Contains("bin\\Debug\\"))
            {
                _shaderFilePath = _shaderFilePath.Replace("bin\\Debug\\", "");
            }
            if (!Directory.Exists(_shaderFilePath))
            {
                throw new DirectoryNotFoundException($"Shader File Directory has not been found\nDirectory: {_shaderFilePath}");
            }

            GL.ClearColor(0.9f, 0.9f, 0.9f, 1.0f);
            //GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

            /// We enable depth testing here.
            /// If you try to draw something more complex than one plane without this, you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
            /// Obviously, we don't want this, so we enable depth testing. 
            /// We also clear the depth buffer in GL.Clear over in OnRenderFrame.
            GL.Enable(EnableCap.DepthTest);

            /// Create Lighting Shader
            // The lighting shaders uses the lighting.frag shader which is what a large part of this chapter will be about
            _lightingShader = new Shader(_shaderFilePath + "shader.vert", _shaderFilePath + "lighting.frag");
            
            /// Initialization of Some Transformation Matrices

            /// This is what the camera sees
            /// We move it backwards on the Z axis.
            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -5.0f);

            /// An orthographic projection matrix defines a cube-like frustum box that defines the clipping space where each vertex outside this box is clipped.
            /// The first two parameters specify the left and right coordinate of the frustum.
            /// The third and fourth parameter specify the bottom and top part of the frustum. 
            /// The distances between the near and far planes are the 5th and 6th parameter.
            /// This specific projection matrix transforms all coordinates between these x, y and z range values to normalized device coordinates.
            //_projection = Matrix4.CreateOrthographicOffCenter(0.0f, Width, 0.0f, Height, 0.1f, 100.0f);

            /// A perspective projection, takes the perspective effect into account, where objects that are further away appear smaller
            /// For the matrix, we use a few parameters.
            ///   Field of view. This determines how much the viewport can see at once. 45 is considered the most "realistic" setting, but most video games nowadays use 90
            ///   Aspect ratio. This should be set to Width / Height.
            ///   Near-clipping. Any vertices closer to the camera than this value will be clipped.
            ///   Far-clipping. Any vertices farther away from the camera than this value will be clipped.
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Width / (float)Height, 0.1f, 100.0f);


            Random rnd = new Random();

            AddGameObjects(new Cube(position: new Vector3(Convert.ToSingle(rnd.NextDouble()))), 2);

            AddGameObjects(new Cube(1.3f, new Vector3(1.0f), new Vector3(1.0f)));

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            /// Clears the screen, using color set in OnLoad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            /// Set the Transformation Matrix model (and other matrices)

            _model = Matrix4.Identity * Matrix4.CreateRotationY(horRotNum) * Matrix4.CreateRotationX(vertRotNum);

            DrawAllGameObjects();

            /// One area is displayed, while the other is being rendered to. Then, when you call SwapBuffers, the two are reversed. A single-buffered context could have issues such as screen tearing.
            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            /// Reset the viewport to the new width and height
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            /// Our Keyboard on this frame
            KeyboardState input = Keyboard.GetState();

            /// Our Mouse on this frame
            MouseState mouse = Mouse.GetCursorState();

            /// If the left mouse button is pressed
            if (mouse.IsButtonDown(MouseButton.Left))
            {
                /// If the mouse was not pressed the previous frame
                if (mouseNotPressedPrevFrame)
                {
                    /// Set the previous position of the mouse to where the mouse currently is, ready for the dragging
                    prevMouse = mouse;

                    /// Any subsequent frame, the mouse is being held down, and not being re-pressed.
                    mouseNotPressedPrevFrame = false;
                }

                /// scrollX is a measure of how much the mouse has moved since it's previous location, in the X direction
                float scrollX = (mouse.X - prevMouse.X) / mouseCameraSensitivity;

                /// scrollY is a measure of how much the mouse has moved since it's previous location, in the Y direction
                float scrollY = (mouse.Y - prevMouse.Y) / mouseCameraSensitivity;

                /// Vertical Rotation
                /// If the next rotation in the Y direction, will not be above 90 degrees or below -90 degrees, which is inbetween -90 and 90 degrees
                if (Math.Abs(vertRotNum + scrollY) < MathHelper.DegreesToRadians(90f))
                {
                    /// Set the current value to rotate by, to the new value (vertRotNum + scrollY)
                    vertRotNum += scrollY;
                }

                /// Horizontal Rotation
                /// Set the current value to rotate by, to the new value (horRotNum + scrollX)
                horRotNum += scrollX;

                /// Set the previous position of the mouse to where the mouse currently is, ready for the dragging
                prevMouse = mouse;

            }

            /// If the mouse is not being held down this frame
            else if (mouse.IsButtonUp(MouseButton.Left))
            {
                /// The next press of the mouse, will be the first, of the mouse presses which will cause a dragging motion
                mouseNotPressedPrevFrame = true;
            }

            #region OLD CODE (Arrow Keys Camera)
            /*
            #region Rotate Scene

            #region Vertical Rotation
            if (input.IsKeyDown(Key.Up))
            {
                if (vertRotNum - 0.01f > MathHelper.DegreesToRadians(-90f))
                {
                    vertRotNum += -0.01f;
                }
                
            }

            else if (input.IsKeyDown(Key.Down))
            {
                if (vertRotNum + 0.01f < MathHelper.DegreesToRadians(90f))
                {
                    vertRotNum += 0.01f;
                }

            }

            #endregion

            #region Horizontal Rotation
            if (input.IsKeyDown(Key.Right))
            {
                horRotNum += 0.01f;
            }

            else if (input.IsKeyDown(Key.Left))
            {
                horRotNum += -0.01f;
            }

            #endregion

            #endregion
            //*/
            #endregion

            if (input.IsKeyDown(Key.Escape))
            {
                /// Responsible for completing closing application checks and closing the application
                QuitApp();

                /// If for some reason the application hasn't exited already, then it will here.
                Exit();
            }

            /// OUTPUT DEBUG INFORMATION
            if (input.IsKeyDown(Key.Z))
            {
                for (int i = 0; i < myGameObjects.Count; i++)
                {
                    Console.WriteLine(myGameObjects[i]);
                    Console.WriteLine(myGameObjectsVAOHandles[i]);
                }
                
            }

            base.OnUpdateFrame(e);
        }

        /// <summary>
        /// After the program ends, we have to manually cleanup our buffers. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
           /// This isn't technically necessary since C# and OpenGL will clean up all resources automatically when the program closes, but it's very important to know how anyway.
            /// Binding a buffer to 0 basically sets it to null, so any calls that modify a buffer without binding one first will result in a crash. 
            /// This is easier to debug than accidentally modifying a buffer that we didn't want modified.

            /// Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            /// Delete all the resources.
            DisposeAllGameObjects();

            _lightingShader.Dispose();

            base.OnUnload(e);
        }
        
    }
}
