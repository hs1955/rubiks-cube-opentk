using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixCubeSolver.Objects
{
    public class Shader
    {
        public int Handle;

        //There's two ints: VertexShader and FragmentShader; they are the handles to the individual shaders. They're defined in the constructor because we won't need the individual shaders after the full shader program is finished.
        public Shader(string vertexPath, string fragmentPath)
        {
            //Next, we need to load the source code from the individual shader files.
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }

            //Then, we generate our shaders, and bind the source code to the shaders.

            int VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            //Then, we compile the shaders and check for errors.

            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);

            //To debug shaders
            Console.WriteLine(GL.GetShaderInfoLog(VertexShader));
            Console.WriteLine(GL.GetShaderInfoLog(FragmentShader));

            //Linking the shaders into a program that can be run on GPU. Handle becomes a usable shader program
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            //The individual vertex and fragment shaders are useless now that they've been linked; the compiled data is copied to the shader program when you link it. You also don't need to have those individual shaders attached to the program; so detach and delete them for cleanup.

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

        }

        //We have a valid shader now, to use it: have a Use function:
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        //Lastly, we need to clean up the handle after this class dies. We can't do it in the finalizer due to the Object-Oriented Language Problem. Instead, we'll have to derive from IDisposable, and remember to call Dispose on our shader manually:

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    } //END OF SHADER CLASS

}
