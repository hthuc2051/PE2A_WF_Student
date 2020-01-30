namespace PE2A_WF_Lecturer
{
    partial class ShowResult
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
            this.btnReload = new System.Windows.Forms.Button();
            this.btnPublish = new System.Windows.Forms.Button();
            this.tbData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tbData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(605, 179);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(176, 54);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(605, 83);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(176, 54);
            this.btnPublish.TabIndex = 5;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // tbData
            // 
            this.tbData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbData.Location = new System.Drawing.Point(19, 12);
            this.tbData.Name = "tbData";
            this.tbData.RowHeadersWidth = 51;
            this.tbData.RowTemplate.Height = 24;
            this.tbData.Size = new System.Drawing.Size(559, 426);
            this.tbData.TabIndex = 4;
            // 
            // ShowResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.tbData);
            this.Name = "ShowResult";
            this.Text = "ShowResult";
            this.Load += new System.EventHandler(this.ShowResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.DataGridView tbData;
    }
}