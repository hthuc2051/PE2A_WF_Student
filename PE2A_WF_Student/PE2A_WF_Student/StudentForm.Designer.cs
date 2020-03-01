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
            this.btnSave = new System.Windows.Forms.Button();
            this.loadingBox = new System.Windows.Forms.PictureBox();
            this.imgSubmit = new System.Windows.Forms.PictureBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(496, 352);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(201, 43);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Upload";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // rtbDocument
            // 
            this.rtbDocument.Location = new System.Drawing.Point(0, 6);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(1239, 486);
            this.rtbDocument.TabIndex = 9;
            this.rtbDocument.Text = "";
            this.rtbDocument.ShortcutsEnabled = false;
            this.rtbDocument.ReadOnly = true;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(575, 7);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(77, 25);
            this.lbTime.TabIndex = 10;
            this.lbTime.Text = "lbTime";
            this.lbTime.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(1, 43);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1245, 527);
            this.tabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.loadingBox);
            this.tabPage1.Controls.Add(this.rtbDocument);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1237, 498);
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
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1237, 498);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Submit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbPoint
            // 
            this.lbPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoint.ForeColor = System.Drawing.Color.OliveDrab;
            this.lbPoint.Location = new System.Drawing.Point(104, 152);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(994, 47);
            this.lbPoint.TabIndex = 11;
            this.lbPoint.Text = "Please wait until your lecturer publish your point";
            this.lbPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPoint.Visible = false;
            // 
            // txtFileName
            // 
            this.txtFileName.AutoSize = true;
            this.txtFileName.Location = new System.Drawing.Point(492, 243);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(52, 17);
            this.txtFileName.TabIndex = 10;
            this.txtFileName.Text = "File.rar";
            this.txtFileName.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(273, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 27);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // loadingBox
            // 
            this.loadingBox.BackColor = System.Drawing.Color.White;
            this.loadingBox.Image = global::PE2A_WF_Student.Properties.Resources.loading;
            this.loadingBox.ImageLocation = "";
            this.loadingBox.Location = new System.Drawing.Point(-4, -59);
            this.loadingBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadingBox.Name = "loadingBox";
            this.loadingBox.Size = new System.Drawing.Size(1245, 563);
            this.loadingBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingBox.TabIndex = 13;
            this.loadingBox.TabStop = false;
            // 
            // imgSubmit
            // 
            this.imgSubmit.BackColor = System.Drawing.Color.White;
            this.imgSubmit.Image = global::PE2A_WF_Student.Properties.Resources.file_upload_img;
            this.imgSubmit.InitialImage = null;
            this.imgSubmit.Location = new System.Drawing.Point(494, 42);
            this.imgSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imgSubmit.Name = "imgSubmit";
            this.imgSubmit.Size = new System.Drawing.Size(192, 178);
            this.imgSubmit.TabIndex = 9;
            this.imgSubmit.TabStop = false;
            this.imgSubmit.Visible = false;
            this.imgSubmit.Click += new System.EventHandler(this.imgFile_Click_1);
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(818, 15);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(218, 27);
            this.btnChoose.TabIndex = 13;
            this.btnChoose.Text = "Choose Your Branch";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1247, 567);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lbTime);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "StudentForm";
            this.Text = "STUDENT FORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentForm_FormClosing);
            this.Load += new System.EventHandler(this.StudentForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSubmit)).EndInit();
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnChoose;
    }
}