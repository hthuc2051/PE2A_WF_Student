namespace PE2A_WF_Student
{
    partial class StudentForm
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
            this.txtFileName = new System.Windows.Forms.Label();
            this.imgSubmit = new System.Windows.Forms.PictureBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.AutoSize = true;
            this.txtFileName.Location = new System.Drawing.Point(317, 209);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(38, 13);
            this.txtFileName.TabIndex = 6;
            this.txtFileName.Text = "File.rar";
            this.txtFileName.Visible = false;

            // 
            // imgSubmit
            // 
            this.imgSubmit.BackColor = System.Drawing.Color.White;
            this.imgSubmit.Image = global::PE2A_WF_Student.Properties.Resources.file_upload_img;
            this.imgSubmit.InitialImage = null;
            this.imgSubmit.Location = new System.Drawing.Point(319, 46);
            this.imgSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.imgSubmit.Name = "imgSubmit";
            this.imgSubmit.Size = new System.Drawing.Size(144, 145);
            this.imgSubmit.TabIndex = 5;
            this.imgSubmit.TabStop = false;
            this.imgSubmit.Click += new System.EventHandler(this.imgFile_Click_1);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(319, 243);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(151, 35);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Upload";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.imgSubmit);
            this.Controls.Add(this.btnSubmit);
            this.Name = "StudentForm";
            this.Text = "StudentForm";
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtFileName;
        private System.Windows.Forms.PictureBox imgSubmit;
        private System.Windows.Forms.Button btnSubmit;
    }
}