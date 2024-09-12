using RubixCubeSolver.Objects;

/// VERSION 11
namespace RubixCubeSolver
{
    class Program
    {
        /// The width of the game window, in pixels
        static int width = 800;

        /// This function should be used if any other class wishes to gain access to this value
        /// (I've done this for every field, for Increased Security of Data)
        static public int getWidth()
        {
            return width;
        }

        /// The height of the game window, in pixels
        static int height = 600;
        static public int getHeight()
        {
            return height;
        }

        static void Main(string[] args)
        {
            /// This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
            using (Game window = new Game(width, height, "Rubik's Cube Solver"))
            { 
                /// Run the window
                /// The 1st argument is the rate at which the program should update per second
                /// The 2nd argument is the frames per second the program should strive to reach
                window.Run(60.0, 60.0);
            }
        }
    }
    
}
