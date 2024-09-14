namespace RubiksCubeSolver
{
    partial class NetMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetMenu));
            this.btnSolveNet = new System.Windows.Forms.Button();
            this.btnSolveRandom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSolveNet
            // 
            this.btnSolveNet.Location = new System.Drawing.Point(800, 336);
            this.btnSolveNet.Name = "btnSolveNet";
            this.btnSolveNet.Size = new System.Drawing.Size(154, 64);
            this.btnSolveNet.TabIndex = 0;
            this.btnSolveNet.Text = "Solve";
            this.btnSolveNet.UseVisualStyleBackColor = true;
            this.btnSolveNet.Click += new System.EventHandler(this.btnSolveNet_Click);
            // 
            // btnSolveRandom
            // 
            this.btnSolveRandom.Location = new System.Drawing.Point(800, 26);
            this.btnSolveRandom.Name = "btnSolveRandom";
            this.btnSolveRandom.Size = new System.Drawing.Size(154, 62);
            this.btnSolveRandom.TabIndex = 1;
            this.btnSolveRandom.Text = "Solve Random";
            this.btnSolveRandom.UseVisualStyleBackColor = true;
            this.btnSolveRandom.Click += new System.EventHandler(this.btnSolveRandom_Click);
            // 
            // NetMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 479);
            this.Controls.Add(this.btnSolveRandom);
            this.Controls.Add(this.btnSolveNet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NetMenu";
            this.Text = "Rubik\'s Cube Solver Net Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetMenu_FormClosing);
            this.Load += new System.EventHandler(this.NetMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSolveNet;
        private System.Windows.Forms.Button btnSolveRandom;
    }
}