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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.rtbDocument = new System.Windows.Forms.RichTextBox();
            this.lbTime = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbPoint = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.Label();
            this.imgSubmit = new System.Windows.Forms.PictureBox();
            this.loadingBox = new System.Windows.Forms.PictureBox();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(558, 440);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(226, 54);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Upload";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // rtbDocument
            // 
            this.rtbDocument.Location = new System.Drawing.Point(0, 7);
            this.rtbDocument.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(1393, 606);
            this.rtbDocument.TabIndex = 9;
            this.rtbDocument.Text = "";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(647, 9);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(95, 29);
            this.lbTime.TabIndex = 10;
            this.lbTime.Text = "lbTime";
            this.lbTime.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(1, 54);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1401, 659);
            this.tabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.loadingBox);
            this.tabPage1.Controls.Add(this.rtbDocument);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1393, 626);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Script";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbPoint);
            this.tabPage2.Controls.Add(this.txtFileName);
            this.tabPage2.Controls.Add(this.btnSubmit);
            this.tabPage2.Controls.Add(this.imgSubmit);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1393, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Submit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbPoint
            // 
            this.lbPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoint.ForeColor = System.Drawing.Color.OliveDrab;
            this.lbPoint.Location = new System.Drawing.Point(117, 190);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(1118, 59);
            this.lbPoint.TabIndex = 11;
            this.lbPoint.Text = "Please wait until your lecturer publish your point";
            this.lbPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPoint.Visible = false;
            // 
            // txtFileName
            // 
            this.txtFileName.AutoSize = true;
            this.txtFileName.Location = new System.Drawing.Point(554, 304);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(57, 20);
            this.txtFileName.TabIndex = 10;
            this.txtFileName.Text = "File.rar";
            this.txtFileName.Visible = false;
            // 
            // imgSubmit
            // 
            this.imgSubmit.BackColor = System.Drawing.Color.White;
            this.imgSubmit.Image = global::PE2A_WF_Student.Properties.Resources.file_upload_img;
            this.imgSubmit.InitialImage = null;
            this.imgSubmit.Location = new System.Drawing.Point(556, 53);
            this.imgSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imgSubmit.Name = "imgSubmit";
            this.imgSubmit.Size = new System.Drawing.Size(216, 222);
            this.imgSubmit.TabIndex = 9;
            this.imgSubmit.TabStop = false;
            this.imgSubmit.Visible = false;
            this.imgSubmit.Click += new System.EventHandler(this.imgFile_Click_1);
            // 
            // loadingBox
            // 
            this.loadingBox.BackColor = System.Drawing.Color.White;
            this.loadingBox.Image = global::PE2A_WF_Student.Properties.Resources.loading;
            this.loadingBox.ImageLocation = "";
            this.loadingBox.Location = new System.Drawing.Point(-4, -74);
            this.loadingBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadingBox.Name = "loadingBox";
            this.loadingBox.Size = new System.Drawing.Size(1401, 704);
            this.loadingBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingBox.TabIndex = 13;
            this.loadingBox.TabStop = false;
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1403, 709);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lbTime);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "StudentForm";
            this.Text = "STUDENT FORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentForm_FormClosing);
            this.Load += new System.EventHandler(this.StudentForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.RichTextBox rtbDocument;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox loadingBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbPoint;
        private System.Windows.Forms.Label txtFileName;
        private System.Windows.Forms.PictureBox imgSubmit;
    }
}