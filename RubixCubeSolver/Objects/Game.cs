using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using static RubixCubeSolver.Objects.GameMaster;

namespace RubixCubeSolver.Objects
{
    class Game : GameWindow
    {
        ///------PROPERTIES
        #region Declaration of Essential Variables and Objects

        Random rnd = new Random();

        /// Our Shader
        private string _shaderFilePath = Path.GetFullPath("OpenGL_Shaders/");

        /// Our Lamp Shader
        public static Shader lightingShader;

        private bool pause = true;

        public static int _frameTime;
        private int _prevTime;

        /// Creating the OpenTK window class to our Game window
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        #region Variables required for the dragging motion
        /*
        public static float horRotNum;

        public static float vertRotNum;

        private static bool mouseNotPressedPrevFrame = true;

        private static MouseState prevMouse = Mouse.GetState();

        private static float mouseCameraSensitivity = 100f;
        //*/
        #endregion

        #endregion

        CompositeGameObject theGameObject;
        const bool debug = true;


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

            /// We enable depth testing here.
            /// If you try to draw something more complex than one plane without this, you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
            /// Obviously, we don't want this, so we enable depth testing. 
            /// We also clear the depth buffer in GL.Clear over in OnRenderFrame.
            GL.Enable(EnableCap.DepthTest);

            /// Create Lighting Shader
            /// The lighting shaders uses the lighting.frag shader which is what a large part of this chapter will be about
            lightingShader = new Shader(_shaderFilePath + "shader.vert", _shaderFilePath + "lighting.frag");

            /// We initialize the camera so that it is 3 units back from where the rectangle is and give it the proper aspect ratio
            _camera = new Camera(Vector3.UnitZ * 3, Width / (float)Height);

            //AddGameObjects(new Plane(lightingShader, 1.8f, 0f, 0f, position: new Vector3(1.0f), color: new Vector3(0.7f, 0.5f, 0.8f)));
            //AddGameObjects(new Cube(lightingShader));

            //AddGameObjects(new RubiksCubePiece(lightingShader, 2, 1.0f, 90f, 0f, position: new Vector3(0.0f)));
            //AddGameObjects(new RubiksCubePiece(lightingShader, 2, 3, 4, 1.0f, 0f, 0f, position: new Vector3(0.0f)));
            //AddGameObjects(new RubiksCubePiece(lightingShader, 2, 3, 4, 1.0f, 0f, 0f, position: new Vector3(2.0f, 0.0f, 0.0f)));

            //AddGameObjects(new RubiksCubePiece(lightingShader, 1, 5, 6, 1.0f, position: new Vector3(0.0f)));
            //AddGameObjects(new RubiksCubePiece(lightingShader, 1, 5, 6, 1.0f, position: new Vector3(2.0f, 0.0f, 0.0f)));

            //AddGameObjects(new RubiksCube(lightingShader, scale: 0.5f, position: new Vector3(0.0f)));
            AddGameObjects(new CompositeTest(lightingShader, 4, scale: 0.5f, position: new Vector3(0.0f)));
            theGameObject = (CompositeGameObject)myGameObjects[0];

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            /// Clears the screen, using color set in OnLoad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _frameTime += 1;

            UpdateGameObjectsOnlyList();
            DrawAllGameObjects(false, omitVAOs);

            /// One area is displayed, while the other is being rendered to. Then, when you call SwapBuffers, the two are reversed. A single-buffered context could have issues such as screen tearing.
            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        System.Collections.Generic.List<int> omitVAOs = new System.Collections.Generic.List<int>();
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            /// Check to see if the window is focused
            if (!Focused) 
            {
                return;
            }

            /// Our Keyboard on this frame
            KeyboardState input = Keyboard.GetState();

            /// Our Mouse on this frame
            MouseState mouse = Mouse.GetState();

            if (pause)
            {
                if (mouse.IsButtonDown(MouseButton.Left))
                {
                    if (debug)
                    {
                        CursorVisible = false;
                    }
                    pause = false;
                }

                else
                {
                    return;
                }
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            #region Controls
            if (debug)
            {
                if (input.IsKeyDown(Key.W))
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                }
                if (input.IsKeyDown(Key.S))
                {
                    _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
                }
                if (input.IsKeyDown(Key.A))
                {
                    _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
                }
                if (input.IsKeyDown(Key.D))
                {
                    _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
                }
                if (input.IsKeyDown(Key.Space))
                {
                    _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
                }
                if (input.IsKeyDown(Key.LShift))
                {
                    _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
                }

                if (input.IsKeyDown(Key.Number1) && (_frameTime > _prevTime + 10))
                {
                    AddGameObjects(new Cube(lightingShader, scale: rnd.Next(5, 15) / 10f, horizontalAngle: Convert.ToSingle(rnd.NextDouble()) * 360f, verticalAngle: Convert.ToSingle(rnd.NextDouble()) * 360f, position: new Vector3(rnd.Next(-3, 3), rnd.Next(-3, 3), rnd.Next(-3, 3)), color: new Vector3(Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble()))));

                    _prevTime = _frameTime;
                }

                if (input.IsKeyDown(Key.Number2) && (_frameTime > _prevTime + 10))
                {
                    AddGameObjects(new Plane(lightingShader, scale: rnd.Next(5, 15) / 10f, horizontalAngle: Convert.ToSingle(rnd.NextDouble()) * 360f, verticalAngle: Convert.ToSingle(rnd.NextDouble()) * 360f, position: new Vector3(rnd.Next(-3, 3), rnd.Next(-3, 3), rnd.Next(-3, 3)), color: new Vector3(Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble()))));

                    _prevTime = _frameTime;
                }

                if (input.IsKeyDown(Key.Number0) && (_frameTime > _prevTime + 10))
                {
                    DisposeAllGameObjects();

                    _prevTime = _frameTime;
                }

                if (input.IsKeyDown(Key.Up))
                {
                    theGameObject.setAngles(theGameObject.getAngles()[0], theGameObject.getAngles()[1] + 1f);
                }
                else if (input.IsKeyDown(Key.Down))
                {
                    theGameObject.setAngles(theGameObject.getAngles()[0], theGameObject.getAngles()[1] - 1f);
                }

                if (input.IsKeyDown(Key.Right))
                {
                    theGameObject.setAngles(theGameObject.getAngles()[0] - 1f, theGameObject.getAngles()[1]);
                }
                else if (input.IsKeyDown(Key.Left))
                {
                    theGameObject.setAngles(theGameObject.getAngles()[0] + 1f, theGameObject.getAngles()[1]);
                }

                /// OUTPUT DEBUG INFORMATION
                if (input.IsKeyDown(Key.Z) && (_frameTime > _prevTime + 10))
                {
                    Console.Clear();

                    for (int i = 0; i < getGameObjects().Count; i++)
                    {
                        Console.WriteLine(getGameObjects()[i]);
                    }

                    Console.WriteLine("\n");

                    for (int i = 0; i < gameObjectsOnlyList.Count; i++)
                    {
                        Console.WriteLine(gameObjectsOnlyList[i]);
                    }

                    _prevTime = _frameTime;
                }

                if (input.IsKeyDown(Key.Escape))
                {
                    CursorVisible = true;
                    pause = true;
                }

                if (_firstMove) // this bool variable is initially set to true
                {
                    _lastPos = new Vector2(mouse.X, mouse.Y);
                    _firstMove = false;
                }
                else
                {
                    // Calculate the offset of the mouse position
                    var deltaX = mouse.X - _lastPos.X;
                    var deltaY = mouse.Y - _lastPos.Y;
                    _lastPos = new Vector2(mouse.X, mouse.Y);

                    // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                    _camera.Yaw += deltaX * sensitivity;
                    _camera.Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
                }
            }

            else
            {
                if (mouse.IsButtonDown(MouseButton.Left))
                {
                    if (_firstMove) // this bool variable is initially set to true
                    {
                        _lastPos = new Vector2(mouse.X, mouse.Y);
                        _firstMove = false;
                    }
                    else
                    {
                        // Calculate the offset of the mouse position
                        var deltaX = mouse.X - _lastPos.X;
                        var deltaY = mouse.Y - _lastPos.Y;
                        _lastPos = new Vector2(mouse.X, mouse.Y);

                        /// Apply rotation
                        float scrollX = theGameObject.getAngles()[0] - deltaX * sensitivity * 1.2f;
                        float scrollY = MathHelper.Clamp(theGameObject.getAngles()[1] - deltaY * sensitivity * 1.2f, -90.0f, 90.0f);

                        theGameObject.setAngles(scrollX, scrollY);
                    }
                }

                else
                {
                    _firstMove = true;
                }
                
            }

            #endregion

            #region OLD CODE
            /*
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
                for (int i = 0; i < getGameObjects().Count; i++)
                {
                    Console.WriteLine(getGameObjects()[i]);
                    //Console.WriteLine(myGameObjectsVAOHandles[i]);
                }   
            }

            //*/
            #endregion

            base.OnUpdateFrame(e);
        }

        /// This function's main purpose is to set the mouse position back to the center of the window
        /// every time the mouse has moved. So the cursor doesn't end up at the edge of the window where it cannot move
        /// further out
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (debug)
            {
                if (Focused && !pause) // check to see if the window is focused
                {
                    Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
                }

                else
                {
                    _firstMove = true;
                }
            }

            else
            {
                return;
            }
            

            base.OnMouseMove(e);
        }

        /// In the mouse wheel function we manage all the zooming of the camera
        /// this is simply done by changing the FOV of the camera
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!Focused || pause || !debug)
            {
                return;
            }

            const float sensitivity = 5.0f;

            _camera.Fov -= e.DeltaPrecise * sensitivity;

            base.OnMouseWheel(e);
        }

        protected override void OnResize(EventArgs e)
        {
            /// Reset the viewport to the new width and height
            GL.Viewport(0, 0, Width, Height);
            /// We need to update the aspect ratio once the window has been resized
            _camera.AspectRatio = Width / (float)Height;
            base.OnResize(e);
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
            //fillerCube.DisposeThisGameObject(true);

            /// Delete Shader
            lightingShader.Dispose();

            base.OnUnload(e);
        }
        
    }
}
