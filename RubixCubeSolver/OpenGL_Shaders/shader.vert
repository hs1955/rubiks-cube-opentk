#version 330 core
//The Vertex shader in the shader language GLSL (OpenGL Shading Language) and then compile this shader so we can use it in our application.

layout (location = 0) in vec3 aPosition;

void main()
{
    gl_Position = vec4(aPosition, 1.0);
}