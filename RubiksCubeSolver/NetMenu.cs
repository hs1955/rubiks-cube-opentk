using System;
using System.Windows.Forms;
using RubiksCubeSolver.NetObjects;
using RubixCubeSolver.Objects;
using static RubiksCubeSolver.StartingForm;
using static RubiksCubeSolver.NetObjects.NetTile;
using static RubiksCubeSolver.RubiksCubeSolver;

namespace RubiksCubeSolver
{
    public partial class NetMenu : Form
    {
        public static int[] colors = null;
        public NetMenu()
        {
            InitializeComponent();
        }

        private void NetMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeProgram = true;
            Application.Exit();
        }

        RubiksCubeNet net = new RubiksCubeNet(new int[] { 70, 5 });
        private void NetMenu_Load(object sender, EventArgs e)
        {
            NetFace netFace;

            for (int i = 0; i < 6; i++)
            {
                netFace = net.myFaces[i];

                for (int j = 0; j < 9; j++)
                {
                    this.Controls.Add(netFace.myTiles[j].netTile);
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!runTimer)
            {
                return;
            }
        }

        private void btnSolveNet_Click(object sender, EventArgs e)
        {
            //*
            if (!(net.IsNetValid() == "Valid"))
            {
                return;
            }
            //*/

            colors = net.FormColorMatrix();

            Open3DRubiksCubeMenu();
            
        }

        private void btnSolveRandom_Click(object sender, EventArgs e)
        {
            RubiksCubeSolver.stepState = (int)StepState.Scramble;
            Open3DRubiksCubeMenu();
        }

        private void Open3DRubiksCubeMenu()
        {
            RubiksCubeSolver stepMenu = new RubiksCubeSolver();
            stepMenu.Show();
            Hide();

            /// This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
            using (Game window = new Game(800, 600, "Rubik's Cube Solver"))
            {
                /// Run the window
                /// The 1st argument is the rate at which the program should update per second
                /// The 2nd argument is the frames per second the program should strive to reach
                window.Run(60.0, 60.0);
            }
        }
    }
}
