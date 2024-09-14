#version 330 core
// This shader is responsible for setting the color of each pixel on screen, once the 3D scene has been formed, and has been sent to the screen
out vec4 FragColor;
  
uniform vec3 objectColor;
uniform vec3 lightColor;

void main()
{
    FragColor = vec4(lightColor * objectColor, 0.8);
}
