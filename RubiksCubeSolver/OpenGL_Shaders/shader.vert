#version 330 core
// The Vertex shader in the shader language GLSL (OpenGL Shading Language) and then compile this shader so we can use it in our application.
// This shader is responsible for getting the object in the correct place, with the correct orientation

layout (location = 0) in vec3 aPos; //aPos is short for aPosition

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // Note that shaders read martix multiplication from left to right
    gl_Position = vec4(aPos, 1.0) * model * view * projection;
}