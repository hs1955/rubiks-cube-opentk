namespace RubiksCubeSolver
{
    partial class RubiksCubeSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNextStep = new System.Windows.Forms.Button();
            this.btnNextStepFast = new System.Windows.Forms.Button();
            this.btnPrevStep = new System.Windows.Forms.Button();
            this.btnPrevStepFast = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(183, 168);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(75, 23);
            this.btnNextStep.TabIndex = 0;
            this.btnNextStep.Text = ">";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // btnNextStepFast
            // 
            this.btnNextStepFast.Location = new System.Drawing.Point(264, 168);
            this.btnNextStepFast.Name = "btnNextStepFast";
            this.btnNextStepFast.Size = new System.Drawing.Size(75, 23);
            this.btnNextStepFast.TabIndex = 1;
            this.btnNextStepFast.Text = ">>";
            this.btnNextStepFast.UseVisualStyleBackColor = true;
            this.btnNextStepFast.Click += new System.EventHandler(this.btnNextStepFast_Click);
            // 
            // btnPrevStep
            // 
            this.btnPrevStep.Location = new System.Drawing.Point(102, 168);
            this.btnPrevStep.Name = "btnPrevStep";
            this.btnPrevStep.Size = new System.Drawing.Size(75, 23);
            this.btnPrevStep.TabIndex = 3;
            this.btnPrevStep.Text = "<";
            this.btnPrevStep.UseVisualStyleBackColor = true;
            this.btnPrevStep.Click += new System.EventHandler(this.btnPrevStep_Click);
            // 
            // btnPrevStepFast
            // 
            this.btnPrevStepFast.Location = new System.Drawing.Point(21, 168);
            this.btnPrevStepFast.Name = "btnPrevStepFast";
            this.btnPrevStepFast.Size = new System.Drawing.Size(75, 23);
            this.btnPrevStepFast.TabIndex = 2;
            this.btnPrevStepFast.Text = "<<";
            this.btnPrevStepFast.UseVisualStyleBackColor = true;
            this.btnPrevStepFast.Click += new System.EventHandler(this.btnPrevStepFast_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tpReset});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(349, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tpReset
            // 
            this.tpReset.Name = "tpReset";
            this.tpReset.Size = new System.Drawing.Size(84, 20);
            this.tpReset.Text = "Reset Colors";
            this.tpReset.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // RubiksCubeSolver
            // 
            this.ClientSize = new System.Drawing.Size(349, 199);
            this.Controls.Add(this.btnPrevStep);
            this.Controls.Add(this.btnPrevStepFast);
            this.Controls.Add(this.btnNextStepFast);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RubiksCubeSolver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RubiksCubeSolver_FormClosing);
            this.Load += new System.EventHandler(this.RubiksCubeSolver_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.Button btnNextStepFast;
        private System.Windows.Forms.Button btnPrevStep;
        private System.Windows.Forms.Button btnPrevStepFast;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tpReset;
    }
}

