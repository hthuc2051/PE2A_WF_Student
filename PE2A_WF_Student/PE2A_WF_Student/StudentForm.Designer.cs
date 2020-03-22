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
            this.pnAction = new System.Windows.Forms.Panel();
            this.lbTime = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnScript = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.loadingBox = new System.Windows.Forms.PictureBox();
            this.rtbDocument = new System.Windows.Forms.RichTextBox();
            this.tabPageSubmit = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbPoint = new System.Windows.Forms.Label();
            this.dgvStudentBranch = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lbCurrentBranch = new System.Windows.Forms.Label();
            this.labelBranch = new System.Windows.Forms.Label();
            this.cHeaderVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHeaderCommitTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnAction.SuspendLayout();
            this.pnScript.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).BeginInit();
            this.tabPageSubmit.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentBranch)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnAction
            // 
            this.pnAction.Controls.Add(this.lbTime);
            this.pnAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnAction.Location = new System.Drawing.Point(0, 0);
            this.pnAction.Name = "pnAction";
            this.pnAction.Size = new System.Drawing.Size(1316, 62);
            this.pnAction.TabIndex = 12;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(614, 9);
            this.lbTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(92, 33);
            this.lbTime.TabIndex = 15;
            this.lbTime.Text = "00:00";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1006, 35);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 30);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Enabled = false;
            // 
            // pnScript
            // 
            this.pnScript.Controls.Add(this.tabControl);
            this.pnScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnScript.Location = new System.Drawing.Point(0, 62);
            this.pnScript.Name = "pnScript";
            this.pnScript.Size = new System.Drawing.Size(1316, 690);
            this.pnScript.TabIndex = 13;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageScript);
            this.tabControl.Controls.Add(this.tabPageSubmit);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(150, 25);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1316, 690);
            this.tabControl.TabIndex = 12;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.loadingBox);
            this.tabPageScript.Controls.Add(this.rtbDocument);
            this.tabPageScript.Location = new System.Drawing.Point(4, 29);
            this.tabPageScript.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageScript.Size = new System.Drawing.Size(1308, 657);
            this.tabPageScript.TabIndex = 0;
            this.tabPageScript.Text = "Script            ";
            this.tabPageScript.UseVisualStyleBackColor = true;
            // 
            // loadingBox
            // 
            this.loadingBox.BackColor = System.Drawing.Color.White;
            this.loadingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingBox.Image = global::PE2A_WF_Student.Properties.Resources.loading;
            this.loadingBox.ImageLocation = "";
            this.loadingBox.Location = new System.Drawing.Point(2, 2);
            this.loadingBox.Margin = new System.Windows.Forms.Padding(2);
            this.loadingBox.Name = "loadingBox";
            this.loadingBox.Size = new System.Drawing.Size(1304, 653);
            this.loadingBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingBox.TabIndex = 13;
            this.loadingBox.TabStop = false;
            // 
            // rtbDocument
            // 
            this.rtbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDocument.Location = new System.Drawing.Point(2, 2);
            this.rtbDocument.Margin = new System.Windows.Forms.Padding(2);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.ReadOnly = true;
            this.rtbDocument.ShortcutsEnabled = false;
            this.rtbDocument.Size = new System.Drawing.Size(1304, 653);
            this.rtbDocument.TabIndex = 9;
            this.rtbDocument.Text = "";
            this.rtbDocument.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbDocument_KeyDown);
            // 
            // tabPageSubmit
            // 
            this.tabPageSubmit.Controls.Add(this.panel2);
            this.tabPageSubmit.Controls.Add(this.panel1);
            this.tabPageSubmit.Location = new System.Drawing.Point(4, 29);
            this.tabPageSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageSubmit.Name = "tabPageSubmit";
            this.tabPageSubmit.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageSubmit.Size = new System.Drawing.Size(1308, 657);
            this.tabPageSubmit.TabIndex = 1;
            this.tabPageSubmit.Text = "Submit            ";
            this.tabPageSubmit.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbPoint);
            this.panel2.Controls.Add(this.dgvStudentBranch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1304, 579);
            this.panel2.TabIndex = 18;
            // 
            // lbPoint
            // 
            this.lbPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoint.ForeColor = System.Drawing.Color.OliveDrab;
            this.lbPoint.Location = new System.Drawing.Point(246, 212);
            this.lbPoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(803, 49);
            this.lbPoint.TabIndex = 18;
            this.lbPoint.Text = "Please wait until your lecturer publish your point";
            this.lbPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPoint.Visible = false;
            // 
            // dgvStudentBranch
            // 
            this.dgvStudentBranch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudentBranch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStudentBranch.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudentBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudentBranch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cHeaderVersion,
            this.cHeaderCommitTime});
            this.dgvStudentBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudentBranch.Location = new System.Drawing.Point(0, 0);
            this.dgvStudentBranch.MultiSelect = false;
            this.dgvStudentBranch.Name = "dgvStudentBranch";
            this.dgvStudentBranch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentBranch.Size = new System.Drawing.Size(1304, 579);
            this.dgvStudentBranch.TabIndex = 17;
            this.dgvStudentBranch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentBranch_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Controls.Add(this.lbCurrentBranch);
            this.panel1.Controls.Add(this.labelBranch);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1304, 74);
            this.panel1.TabIndex = 17;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(1165, 35);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(110, 30);
            this.btnSubmit.TabIndex = 23;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lbCurrentBranch
            // 
            this.lbCurrentBranch.AutoSize = true;
            this.lbCurrentBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentBranch.Location = new System.Drawing.Point(115, 44);
            this.lbCurrentBranch.Name = "lbCurrentBranch";
            this.lbCurrentBranch.Size = new System.Drawing.Size(76, 16);
            this.lbCurrentBranch.TabIndex = 22;
            this.lbCurrentBranch.Text = "no branch";
            // 
            // labelBranch
            // 
            this.labelBranch.AutoSize = true;
            this.labelBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBranch.Location = new System.Drawing.Point(6, 44);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(103, 16);
            this.labelBranch.TabIndex = 21;
            this.labelBranch.Text = "Current version: ";
            // 
            // cHeaderVersion
            // 
            this.cHeaderVersion.HeaderText = "Version";
            this.cHeaderVersion.Name = "cHeaderVersion";
            this.cHeaderVersion.ReadOnly = true;
            this.cHeaderVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // cHeaderCommitTime
            // 
            this.cHeaderCommitTime.HeaderText = "Commit Time";
            this.cHeaderCommitTime.Name = "cHeaderCommitTime";
            this.cHeaderCommitTime.ReadOnly = true;
            this.cHeaderCommitTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1316, 752);
            this.Controls.Add(this.pnScript);
            this.Controls.Add(this.pnAction);
            this.MaximizeBox = false;
            this.Name = "StudentForm";
            this.Text = "STUDENT FORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentForm_FormClosing);
            this.Load += new System.EventHandler(this.StudentForm_Load);
            this.pnAction.ResumeLayout(false);
            this.pnAction.PerformLayout();
            this.pnScript.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingBox)).EndInit();
            this.tabPageSubmit.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentBranch)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnAction;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Panel pnScript;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageScript;
        private System.Windows.Forms.PictureBox loadingBox;
        private System.Windows.Forms.RichTextBox rtbDocument;
        private System.Windows.Forms.TabPage tabPageSubmit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbPoint;
        private System.Windows.Forms.DataGridView dgvStudentBranch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelBranch;
        private System.Windows.Forms.Label lbCurrentBranch;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHeaderVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHeaderCommitTime;
    }
}