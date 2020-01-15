namespace PE2A_WF_Lecturer
{
    partial class LecturerForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbSubmitedFiles = new System.Windows.Forms.GroupBox();
            this.lvSubmitedFiles = new System.Windows.Forms.ListView();
            this.btnEstimate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbSubmitedFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbSubmitedFiles);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5, 20, 5, 5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnEstimate);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(100, 200, 100, 300);
            this.splitContainer1.Size = new System.Drawing.Size(800, 585);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // gbSubmitedFiles
            // 
            this.gbSubmitedFiles.Controls.Add(this.lvSubmitedFiles);
            this.gbSubmitedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSubmitedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSubmitedFiles.Location = new System.Drawing.Point(5, 20);
            this.gbSubmitedFiles.Margin = new System.Windows.Forms.Padding(0);
            this.gbSubmitedFiles.Name = "gbSubmitedFiles";
            this.gbSubmitedFiles.Padding = new System.Windows.Forms.Padding(5, 20, 3, 3);
            this.gbSubmitedFiles.Size = new System.Drawing.Size(390, 560);
            this.gbSubmitedFiles.TabIndex = 0;
            this.gbSubmitedFiles.TabStop = false;
            this.gbSubmitedFiles.Text = "Submited Files";
            // 
            // lvSubmitedFiles
            // 
            this.lvSubmitedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSubmitedFiles.GridLines = true;
            this.lvSubmitedFiles.HideSelection = false;
            this.lvSubmitedFiles.Location = new System.Drawing.Point(5, 37);
            this.lvSubmitedFiles.Name = "lvSubmitedFiles";
            this.lvSubmitedFiles.Size = new System.Drawing.Size(382, 520);
            this.lvSubmitedFiles.TabIndex = 0;
            this.lvSubmitedFiles.UseCompatibleStateImageBehavior = false;
            // 
            // btnEstimate
            // 
            this.btnEstimate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEstimate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstimate.Location = new System.Drawing.Point(100, 200);
            this.btnEstimate.Name = "btnEstimate";
            this.btnEstimate.Size = new System.Drawing.Size(196, 85);
            this.btnEstimate.TabIndex = 0;
            this.btnEstimate.Text = "Estimate";
            this.btnEstimate.UseVisualStyleBackColor = true;
            this.btnEstimate.Click += new System.EventHandler(this.btnEstimate_Click);
            // 
            // LecturerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 585);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LecturerForm";
            this.Text = "Lecturer File Getter";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbSubmitedFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbSubmitedFiles;
        private System.Windows.Forms.ListView lvSubmitedFiles;
        private System.Windows.Forms.Button btnEstimate;
    }
}