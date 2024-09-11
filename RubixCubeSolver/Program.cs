using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubixCubeSolver.Objects;
using OpenTK;

//VERSION 1
namespace RubixCubeSolver
{
    class Program
    {
        static int width = 800;
        static public int getWidth()
        {
            return width;
        }

        static int height = 600;
        static public int getHeight()
        {
            return height;
        }

        static void Main(string[] args)
        {
            // This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
            using (AppWindow window = new AppWindow(width, height, "Rubik's Cube Solver"))
            { 
                //Run takes a double, which is how many frames per second it should strive to reach.
                //You can leave that out and it'll just update as fast as the hardware will allow it.
                window.Run(60.0, 60.0);
            }
        }
    }
}
