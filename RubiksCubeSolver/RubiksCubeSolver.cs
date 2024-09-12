using System;
using System.Windows.Forms;
using RubixCubeSolver.Objects;
using static RubiksCubeSolver.StartingForm;

namespace RubiksCubeSolver
{
    public partial class RubiksCubeSolver : Form
    {
        public RubiksCubeSolver()
        {
            InitializeComponent();
        }

        private void RubiksCubeSolver_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeProgram = true;
            Application.Exit();
        }

        public enum StepState
        {
            Free = -1,
            PrevStepFast,
            PrevStepFastPend,
            PrevStep,
            PrevStepPend,
            NextStep,
            NextStepPend,
            NextStepFast,
            NextStepFastPend,
            Scramble,
            ScramblePend,
        }

        public static int stepState = (int)StepState.Free;
        private void btnNextStep_Click(object sender, EventArgs e)
        {
            if (Game.firstTime == false && stepState == (int)StepState.Free)
            {
                stepState = (int)StepState.NextStep;
            }
        }

        private void btnNextStepFast_Click(object sender, EventArgs e)
        {
            if (Game.firstTime == false && stepState == (int)StepState.Free)
            {
                stepState = (int)StepState.NextStepFast;
            }
        }

        private void btnPrevStepFast_Click(object sender, EventArgs e)
        {
            if (Game.firstTime == false && stepState == (int)StepState.Free)
            {
                stepState = (int)StepState.PrevStepFast;
            }
        }

        private void btnPrevStep_Click(object sender, EventArgs e)
        {
            if (Game.firstTime == false && stepState == (int)StepState.Free)
            {
                stepState = (int)StepState.PrevStep;
            }
        }

        private void RubiksCubeSolver_Load_1(object sender, EventArgs e) { }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Game.firstTime == false && NetMenu.colors != null)
            {
                ((RubiksCube)GameMaster.myGameObjects[0]).UpdateColors(NetMenu.colors);
                ((RubiksCube)GameMaster.myGameObjects[0]).ResetSteps();
            }
        }
    }
}


