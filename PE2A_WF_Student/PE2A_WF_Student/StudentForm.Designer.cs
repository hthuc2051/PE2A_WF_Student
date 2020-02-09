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
            this.lbPoint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.AutoSize = true;
            this.txtFileName.Location = new System.Drawing.Point(476, 322);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(57, 20);
            this.txtFileName.TabIndex = 6;
            this.txtFileName.Text = "File.rar";
            this.txtFileName.Visible = false;
            // 
            // imgSubmit
            // 
            this.imgSubmit.BackColor = System.Drawing.Color.White;
            this.imgSubmit.Image = global::PE2A_WF_Student.Properties.Resources.file_upload_img;
            this.imgSubmit.InitialImage = null;
            this.imgSubmit.Location = new System.Drawing.Point(478, 71);
            this.imgSubmit.Name = "imgSubmit";
            this.imgSubmit.Size = new System.Drawing.Size(216, 223);
            this.imgSubmit.TabIndex = 5;
            this.imgSubmit.TabStop = false;
            this.imgSubmit.Click += new System.EventHandler(this.imgFile_Click_1);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(478, 374);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(226, 54);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Upload";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lbPoint
            // 
            this.lbPoint.AutoSize = true;
            this.lbPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoint.ForeColor = System.Drawing.Color.OliveDrab;
            this.lbPoint.Location = new System.Drawing.Point(39, 208);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(1118, 59);
            this.lbPoint.TabIndex = 7;
            this.lbPoint.Text = "Please wait until your lecturer publish your point";
            this.lbPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPoint.Visible = false;
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.lbPoint);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.imgSubmit);
            this.Controls.Add(this.btnSubmit);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StudentForm";
            this.Text = "STUDENT FORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentForm_FormClosing);
            this.Load += new System.EventHandler(this.StudentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtFileName;
        private System.Windows.Forms.PictureBox imgSubmit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lbPoint;
    }
}