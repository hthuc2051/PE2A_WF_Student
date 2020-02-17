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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lbPoint = new System.Windows.Forms.Label();
            this.loadingBox = new System.Windows.Forms.PictureBox();
            this.imgSubmit = new System.Windows.Forms.PictureBox();
            this.rtbDocument = new System.Windows.Forms.RichTextBox();
            this.lbTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.AutoSize = true;
            this.txtFileName.Location = new System.Drawing.Point(423, 258);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(52, 17);
            this.txtFileName.TabIndex = 6;
            this.txtFileName.Text = "File.rar";
            this.txtFileName.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(425, 299);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(201, 43);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Upload";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lbPoint
            // 
            this.lbPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoint.ForeColor = System.Drawing.Color.OliveDrab;
            this.lbPoint.Location = new System.Drawing.Point(35, 166);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(994, 47);
            this.lbPoint.TabIndex = 7;
            this.lbPoint.Text = "Please wait until your lecturer publish your point";
            this.lbPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPoint.Visible = false;
            // 
            // loadingBox
            // 
            this.loadingBox.Image = global::PE2A_WF_Student.Properties.Resources.loading;
            this.loadingBox.ImageLocation = "";
            this.loadingBox.Location = new System.Drawing.Point(386, 57);
            this.loadingBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadingBox.Name = "loadingBox";
            this.loadingBox.Size = new System.Drawing.Size(293, 285);
            this.loadingBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingBox.TabIndex = 8;
            this.loadingBox.TabStop = false;
            // 
            // imgSubmit
            // 
            this.imgSubmit.BackColor = System.Drawing.Color.White;
            this.imgSubmit.Image = global::PE2A_WF_Student.Properties.Resources.file_upload_img;
            this.imgSubmit.InitialImage = null;
            this.imgSubmit.Location = new System.Drawing.Point(425, 57);
            this.imgSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imgSubmit.Name = "imgSubmit";
            this.imgSubmit.Size = new System.Drawing.Size(192, 178);
            this.imgSubmit.TabIndex = 5;
            this.imgSubmit.TabStop = false;
            this.imgSubmit.Click += new System.EventHandler(this.imgFile_Click_1);
            // 
            // rtbDocument
            // 
            this.rtbDocument.Location = new System.Drawing.Point(720, 230);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(446, 309);
            this.rtbDocument.TabIndex = 9;
            this.rtbDocument.Text = "";
            this.rtbDocument.Visible = false;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(965, 82);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(77, 25);
            this.lbTime.TabIndex = 10;
            this.lbTime.Text = "lbTime";
            this.lbTime.Visible = false;
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1247, 567);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.rtbDocument);
            this.Controls.Add(this.loadingBox);
            this.Controls.Add(this.lbPoint);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.imgSubmit);
            this.Controls.Add(this.btnSubmit);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "StudentForm";
            this.Text = "STUDENT FORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentForm_FormClosing);
            this.Load += new System.EventHandler(this.StudentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtFileName;
        private System.Windows.Forms.PictureBox imgSubmit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lbPoint;
        private System.Windows.Forms.PictureBox loadingBox;
        private System.Windows.Forms.RichTextBox rtbDocument;
        private System.Windows.Forms.Label lbTime;
    }
}