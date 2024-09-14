using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using static RubiksCubeSolver.RubiksCubeSolver;
using static RubiksCubeSolver.StartingForm;
using static RubixCubeSolver.Objects.GameMaster;
using static RubiksCubeSolver.NetMenu;
using RubiksCubeSolver;

namespace RubixCubeSolver.Objects
{
    class Game : GameWindow
    {
        ///------PROPERTIES
        #region Declaration of Essential Variables and Objects

        public static Random rnd = new Random();

        /// Prevent the user from interfering with the Cube while its being solved
        public static bool firstTime = true;

        /// Our Lamp Shader
        public static Shader lightingShader;

        public static int frameTime;

        /// Creating the OpenTK window class to our Game window
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        private static Vector2 _lastPos = new Vector2(0.0f);


        #endregion

        public static CompositeGameObject theGameObject;

        /// <summary>
        /// Runs one time, when the window first opens. Any initialization-related code should go here.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.9f, 0.9f, 0.9f, 1.0f);

            /// We enable depth testing here.
            /// If you try to draw something more complex than one plane without this, you'll notice that polygons further in the background will occasionally be drawn over the top of the ones in the foreground.
            /// Obviously, we don't want this, so we enable depth testing. 
            /// We also clear the depth buffer in GL.Clear over in OnRenderFrame.
            GL.Enable(EnableCap.DepthTest);

            /// Create Lighting Shader
            /// The lighting shaders uses the lighting.frag shader which is what a large part of this chapter will be about
            lightingShader = new Shader(Resource1.shader_vert, Resource1.lighting_frag);

            /// We initialize the camera so that it is 3 units back from where the rectangle is and give it the proper aspect ratio
            _camera = new Camera(Vector3.UnitZ * 3, Width / (float)Height);

            AddGameObjects(new RubiksCube(lightingShader, scale: 0.5f, position: new Vector3(0.0f)));

            theGameObject = (CompositeGameObject)myGameObjects[0];

            if (colors != null)
            {
                ((RubiksCube)theGameObject).UpdateColors(colors);
            }

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            /// Clears the screen, using color set in OnLoad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            frameTime++;

            DrawAllGameObjects(omitVAOs);

            /// One area is displayed, while the other is being rendered to. Then, when you call SwapBuffers, the two are reversed. A single-buffered context could have issues such as screen tearing.
            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        System.Collections.Generic.List<int> omitVAOs = new System.Collections.Generic.List<int>();
        System.Collections.Generic.List<bool> animationDone = new System.Collections.Generic.List<bool>() { true, true };
        const float speed = 5.0f;
        const float fastSpeed = 25.0f;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            /// Our Mouse on this frame
            MouseState mouse = Mouse.GetState();

            const float sensitivity = 0.2f;

            if (closeProgram)
            {
                Exit();
            }

            RubiksCube rubiksCube = (RubiksCube)myGameObjects[0];

            switch (stepState)
            {
                case (int)StepState.PrevStepFast:
                    animationDone[1] = rubiksCube.SolveStepByStep(StepState.PrevStep, fastSpeed);
                    stepState = (int)StepState.PrevStepFastPend;
                    break;

                case (int)StepState.PrevStepFastPend:
                    if (!animationDone[1]) animationDone[1] = rubiksCube.SolveStepByStep(StepState.PrevStep, fastSpeed);
                    else stepState = (int)StepState.Free;
                    break;

                case (int)StepState.PrevStep:
                    animationDone[1] = rubiksCube.SolveStepByStep(StepState.PrevStep, speed);
                    stepState = (int)StepState.PrevStepPend;
                    break;

                case (int)StepState.PrevStepPend:
                    if (!animationDone[1]) animationDone[1] = rubiksCube.SolveStepByStep(StepState.PrevStep, speed);
                    else stepState = (int)StepState.Free;
                    break;

                case (int)StepState.NextStep:
                    animationDone[1] = rubiksCube.SolveStepByStep(StepState.NextStep, speed);
                    stepState = (int)StepState.NextStepPend;
                    break;

                case (int)StepState.NextStepPend:
                    if (!animationDone[1]) animationDone[1] = rubiksCube.SolveStepByStep(StepState.NextStep, speed);
                    else stepState = (int)StepState.Free;
                    break;

                case (int)StepState.NextStepFast:
                    animationDone[1] = rubiksCube.SolveStepByStep(StepState.NextStep, fastSpeed);
                    stepState = (int)StepState.NextStepFastPend;
                    break;

                case (int)StepState.NextStepFastPend:
                    if (!animationDone[1]) animationDone[1] = rubiksCube.SolveStepByStep(StepState.NextStep, fastSpeed);
                    else stepState = (int)StepState.Free;
                    break;

                case (int)StepState.Scramble:
                    animationDone[0] = rubiksCube.ScrambleCube(90.0f);
                    stepState = (int)StepState.ScramblePend;
                    break;

                case (int)StepState.ScramblePend:
                    if (!animationDone[0]) animationDone[0] = rubiksCube.ScrambleCube(90.0f);
                    else stepState = (int)StepState.Free;
                    break;
                    
            }

            if (!rubiksCube.isCubeSolved && firstTime && (stepState == (int)StepState.Free))
            {
                rubiksCube.SolveCube(90.0f);
            }
            else
            {
                if (firstTime && (stepState != (int)StepState.Scramble) && (stepState != (int)StepState.ScramblePend))
                {
                    rubiksCube.UpdateColors(colors);
                    firstTime = false;
                }

            }

            #region Controls
            
            if (mouse.IsButtonDown(MouseButton.Left))
            {
                if (_firstMove) /// This bool variable is initially set to true
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

                    /// Apply rotation
                    float scrollX = theGameObject.getAngles()[1] - deltaX * sensitivity * -1;
                    float scrollY = MathHelper.Clamp(theGameObject.getAngles()[0] - deltaY * sensitivity * -1, -90.0f, 90.0f);

                    theGameObject.setAngles(scrollY, scrollX, 0.0f);
                }
            }

            else
            {
                _firstMove = true;
            }

            #endregion

            base.OnUpdateFrame(e);
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

            closeProgram = true;

            base.OnUnload(e);
        }

    }
}
