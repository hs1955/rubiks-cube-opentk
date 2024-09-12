using System;
using System.Windows.Forms;

namespace RubiksCubeSolver
{
    public partial class StartingForm : Form
    {
        public static bool closeProgram = false;
        public StartingForm() => InitializeComponent();
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();
        private void btnStart_Click(object sender, EventArgs e)
        {
            NetMenu netMenu = new NetMenu();
            netMenu.Show();
            Hide();
        }
        private void StartingForm_Load(object sender, EventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (closeProgram) Application.Exit();
        }
        private void StartingForm_FormClosing(object sender, FormClosingEventArgs e) { }
    }
}
