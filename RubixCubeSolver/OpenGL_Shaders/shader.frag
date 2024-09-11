#version 330 core
//The fragment shader is all about calculating the color output of your pixels. To keep things simple the fragment shader will always output an orange-ish color.
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}