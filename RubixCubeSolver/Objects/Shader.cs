using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RubixCubeSolver.Objects
{
    public class Shader
    {
        /// <summary>
        /// This is the Handle to our Shader, by which the shader can be accessed
        /// </summary>
        public int Handle;

        /// <summary>
        /// This dictionary stores the locations of our uniforms
        /// </summary>
        private readonly Dictionary<string, int> _uniformLocations;

        /// <summary>
        /// There's two ints: vertexShader and fragmentShader; they are the handles to the individual shaders. They're defined in the constructor because we won't need the individual shaders after the full shader program is finished. 
        /// </summary>
        /// <param name="vertPath"> The filepath to the Vertex Shader </param>
        /// <param name="fragPath"> The filepath to the Fragment Shader </param>
        public Shader(string vertPath, string fragPath)
        {
            #region Load vertex and fragment shaders and compile

            /// LoadSource is a simple function that just loads all text from the file whose path is given.
            var shaderSource = LoadSource(vertPath);

            /// GL.CreateShader will create an empty shader (obviously). The ShaderType enum denotes which type of shader will be created.
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);

            /// Now, bind the GLSL source code
            GL.ShaderSource(vertexShader, shaderSource);

            /// And then compile
            CompileShader(vertexShader);

            /// We do the same for the fragment shader
            shaderSource = LoadSource(fragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

            #endregion

            #region Linking the shaders into a program that can be run on GPU.
            /// Handle becomes a usable shader program
            Handle = GL.CreateProgram();

            /// Attach both shaders...
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            /// And then link them together.
            LinkProgram(Handle);

            #endregion

            #region Detach and Delete the individual shaders for cleanup.
            /// The individual vertex and fragment shaders are useless now that they've been linked; the compiled data is copied to the shader program when you link it.
            /// You also don't need to have those individual shaders attached to the program; so detach and delete them for cleanup.
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            #endregion

            /// The shader is now ready to go, but first, we're going to cache all the shader uniform locations.
            /// Querying this from the shader is very slow, so we do it once on initialization and reuse those values later.
            /// Shader Uniforms are values that can be passed into the shader files

            #region Cache all the Shader Uniform Locations

            /// First, we have to get the number of active uniforms in the shader.
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            /// Next, allocate the dictionary to hold the locations.
            _uniformLocations = new Dictionary<string, int>();

            /// Loop over all the uniforms,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                /// Get the name of this uniform,
                var key = GL.GetActiveUniform(Handle, i, out _, out _);

                /// Get the location,
                var location = GL.GetUniformLocation(Handle, key);

                /// Add it to the dictionary.
                _uniformLocations.Add(key, location);
            }

            #endregion

        }

        private static void CompileShader(int shader)
        {
            /// Try to compile the shader
            GL.CompileShader(shader);

            /// Check for compilation errors
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);

            if (code != (int)All.True)
            {
                /// We can use GL.GetShaderInfoLog(shader) to get information about the error.
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            /// We link the program
            GL.LinkProgram(program);

            /// Check for linking errors
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            
            if (code != (int)All.True)
            {
                /// We can use GL.GetProgramInfoLog(program) to get information about the error.
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        /// <summary>
        /// A wrapper function that enables the shader program. 
        /// </summary>
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        /// <summary>
        /// The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
        /// you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values. 
        /// </summary>
        /// <param name="attribName"></param>
        /// <returns></returns>
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        /// <summary>
        /// Just loads the entire file into a string. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string LoadSource(string path)
        {
            using (var sr = new StreamReader(path, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        /// Uniform setters
        /// Uniforms are variables that can be set by user code, instead of reading them from the VBO.
        /// You use VBOs for vertex-related data, and uniforms for almost everything else.

        /// Setting a uniform is almost always the exact same, so I'll explain it here once, instead of in every method:
        ///     1. Bind the program you want to set the uniform on
        ///     2. Get a handle to the location of the uniform with GL.GetUniformLocation.
        ///     3. Use the appropriate GL.Uniform* function to set the uniform.

        /// <summary>
        /// Set a uniform int on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform float on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform Matrix4 on this shader
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        /// <remarks>
        ///   <para>
        ///   The matrix is transposed before being sent to the shader.
        ///   </para>
        /// </remarks>
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// Set a uniform Vector3 on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }

        #region For cleanup of Shader Handle. Use _shaderName.Dispose() 
        /// Lastly, we need to clean up the handle after this class dies. We can't do it in the finalizer due to the Object-Oriented Language Problem. Instead, we'll have to derive from IDisposable, and remember to call Dispose on our shader manually:

        /// This variable stops the same Handle being disposed twice.
        /// Any repeated attempt to dispose won't cause anything to happen
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    } ///END OF SHADER CLASS

}
