using System;
using System.IO;
using System.Linq;
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

        public static int frameTime;
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

            /// We initialize the camera so that it is 2 units back from where the rectangle is and give it the proper aspect ratio
            _camera = new Camera(Vector3.UnitZ * 1, Width / (float)Height);

            /// Test 1
            //AddGameObjects(new Cube(lightingShader, 0.1f, new Vector3(-1000.0f), new Vector3(1.0f)));
            //AddGameObjects(new Cube(lightingShader, 0.75f, new Vector3(1.0f), new Vector3(0.5f, 0.2f, 0.7f)));

            /// Test 2
            //AddGameObjects(new RubiksCubePiece(lightingShader, 2, scale: 1f, position: new Vector3(1.2f), angles: new float[] { 0.0f, 0.0f, 0.0f }));
            //AddGameObjects(new RubiksCubePiece(lightingShader, 1, 2, 3, scale: 0.99f, position: new Vector3(-1.2f), angles: new float[] { 0.0f, 0.0f, 0.0f }));

            /// Test 3
            AddGameObjects(new RubiksCube(lightingShader, scale: 0.25f, position: new Vector3(0.0f)));
            //AddGameObjects(new RubiksCubeSlice(lightingShader, scale: 0.25f, position: new Vector3(-1.0f, 0.25f, 0.0f)));

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            /// Clears the screen, using color set in OnLoad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (!pause)
            {
                frameTime += 1;
            }

            DrawAllGameObjects(false, omitVAOs);

            /// One area is displayed, while the other is being rendered to. Then, when you call SwapBuffers, the two are reversed. A single-buffered context could have issues such as screen tearing.
            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        bool[] animationDone = new bool[] { true, true };
        System.Collections.Generic.List<int> omitVAOs = new System.Collections.Generic.List<int>();

        int sliceToAnimate = (int)RubiksCube.Slices.Left;

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

                if (input.IsKeyDown(Key.Number1) && (frameTime > _prevTime + 10))
                {
                    AddGameObjects(new Cube(lightingShader, scale: rnd.Next(5, 15) / 10f, position: new Vector3(rnd.Next(-3, 3), rnd.Next(-3, 3), rnd.Next(-3, 3)), color: new Vector3(Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble())), new float[] { Convert.ToSingle(rnd.NextDouble()) * 360f, Convert.ToSingle(rnd.NextDouble()) * 360f, Convert.ToSingle(rnd.NextDouble()) * 360f }));

                    _prevTime = frameTime;
                }
                if (input.IsKeyDown(Key.Number2) && (frameTime > _prevTime + 10))
                {
                    AddGameObjects(new Plane(lightingShader, scale: rnd.Next(5, 15) / 10f, position: new Vector3(rnd.Next(-3, 3), rnd.Next(-3, 3), rnd.Next(-3, 3)), color: new Vector3(Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble()), Convert.ToSingle(rnd.NextDouble())), new float[] { Convert.ToSingle(rnd.NextDouble()) * 360f, Convert.ToSingle(rnd.NextDouble()) * 360f, Convert.ToSingle(rnd.NextDouble()) * 360f }));

                    _prevTime = frameTime;
                }
                if (input.IsKeyDown(Key.Number0) && (frameTime > _prevTime + 10))
                {
                    DisposeAllGameObjects();

                    _prevTime = frameTime;
                }

                if (input.IsKeyDown(Key.O) && (frameTime > _prevTime + 10) || !animationDone[0])
                {
                    RubiksCube rubiksCube = (RubiksCube)myGameObjects[0];

                    animationDone[0] = rubiksCube.RotateSlice(sliceToAnimate, -1);

                    _prevTime = frameTime;
                }
                if (input.IsKeyDown(Key.P) && (frameTime > _prevTime + 10) || !animationDone[1])
                {
                    RubiksCube rubiksCube = (RubiksCube)myGameObjects[0];

                    animationDone[1] = rubiksCube.RotateSlice(sliceToAnimate, 1);

                    _prevTime = frameTime;
                }

                if (input.IsKeyDown(Key.RBracket) && (frameTime > _prevTime + 10))
                {
                    int[] theColors = new int[54];

                    for (int i = 0; i < 54; i++)
                    {
                        theColors[i] = rnd.Next(1, 7);
                    }

                    ((RubiksCube)myGameObjects[0]).UpdateColors(theColors);
                    ((RubiksCubeSlice)myGameObjects[1]).UpdateColors(theColors);

                    _prevTime = frameTime;
                }

                if (input.IsKeyDown(Key.Up))
                {
                    foreach (IGameObject theGameObject in myGameObjects)
                    {
                        if (theGameObject is GameObject)
                        {
                            GameObject gameObject = (GameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0] + 1f, theAngles[1], theAngles[2] });
                        }

                        else if (theGameObject is CompositeGameObject)
                        {
                            CompositeGameObject gameObject = (CompositeGameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0] + 1f, theAngles[1], theAngles[2] });
                        }

                    }

                }
                else if (input.IsKeyDown(Key.Down))
                {
                    foreach (IGameObject theGameObject in myGameObjects)
                    {
                        if (theGameObject is GameObject)
                        {
                            GameObject gameObject = (GameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0] - 1f, theAngles[1], theAngles[2] });
                        }

                        else if (theGameObject is CompositeGameObject)
                        {
                            CompositeGameObject gameObject = (CompositeGameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0] - 1f, theAngles[1], theAngles[2] });
                        }

                    }
                }
                if (input.IsKeyDown(Key.Right))
                {
                    foreach (IGameObject theGameObject in myGameObjects)
                    {
                        if (theGameObject is GameObject)
                        {
                            GameObject gameObject = (GameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1] - 1f, theAngles[2] });
                        }
                        
                        else if (theGameObject is CompositeGameObject)
                        {
                            CompositeGameObject gameObject = (CompositeGameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1] - 1f, theAngles[2] });
                        }

                    }

                    /*
                    foreach (CompositeGameObject theGameObject in myGameObjects)
                    {
                        float[] theAngles = theGameObject.getAngles();

                        theGameObject.setAngles(new float[] { theAngles[0], theAngles[1] - 1f, theAngles[2] });
                    }
                    //*/
                }
                else if (input.IsKeyDown(Key.Left))
                {
                    foreach (IGameObject theGameObject in myGameObjects)
                    {
                        if (theGameObject is GameObject)
                        {
                            GameObject gameObject = (GameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1] + 1f, theAngles[2] });
                        }

                        else if (theGameObject is CompositeGameObject)
                        {
                            CompositeGameObject gameObject = (CompositeGameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1] + 1f, theAngles[2] });
                        }

                    }
                }
                if (input.IsKeyDown(Key.N))
                {
                    foreach (IGameObject theGameObject in myGameObjects)
                    {
                        if (theGameObject is GameObject)
                        {
                            GameObject gameObject = (GameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1], theAngles[2] - 1f });
                        }

                        else if (theGameObject is CompositeGameObject)
                        {
                            CompositeGameObject gameObject = (CompositeGameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1], theAngles[2] - 1f });
                        }

                    }
                }
                else if (input.IsKeyDown(Key.M))
                {
                    foreach (IGameObject theGameObject in myGameObjects)
                    {
                        if (theGameObject is GameObject)
                        {
                            GameObject gameObject = (GameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1], theAngles[2] + 1f });
                        }

                        else if (theGameObject is CompositeGameObject)
                        {
                            CompositeGameObject gameObject = (CompositeGameObject)theGameObject;

                            float[] theAngles = gameObject.getAngles();

                            gameObject.setAngles(new float[] { theAngles[0], theAngles[1], theAngles[2] + 1f });
                        }

                    }
                }

                /// OUTPUT DEBUG INFORMATION
                if (input.IsKeyDown(Key.Z) && (frameTime > _prevTime + 10))
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

                    _prevTime = frameTime;
                }

                if (input.IsKeyDown(Key.Escape))
                {
                    CursorVisible = true;
                    pause = true;
                }

                if (_firstMove) /// this bool variable is initially set to true
                {
                    _lastPos = new Vector2(mouse.X, mouse.Y);
                    _firstMove = false;
                }
                else
                {
                    /// Calculate the offset of the mouse position
                    var deltaX = mouse.X - _lastPos.X;
                    var deltaY = mouse.Y - _lastPos.Y;
                    _lastPos = new Vector2(mouse.X, mouse.Y);

                    /// Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                    _camera.Yaw += deltaX * sensitivity;
                    _camera.Pitch -= deltaY * sensitivity; /// reversed since y-coordinates range from bottom to top
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
                        foreach (CompositeGameObject theGameObject in myGameObjects)
                        {
                            float scrollX = theGameObject.getAngles()[0] - deltaX * sensitivity * 1.2f;
                            float scrollY = MathHelper.Clamp(theGameObject.getAngles()[1] - deltaY * sensitivity * 1.2f, -90.0f, 90.0f);

                            float[] theAngles = theGameObject.getAngles();

                            theGameObject.setAngles(new float[] { theAngles[0] + scrollY, theAngles[1] + scrollX, theAngles[2] });
                        }
                        
                    }
                }

                else
                {
                    _firstMove = true;
                }
                
            }

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

            /// Delete Shader
            lightingShader.Dispose();

            base.OnUnload(e);
        }
        
    }
}
