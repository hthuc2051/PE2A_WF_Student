using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Student.Student
{
    public partial class PracticalExamTemplateForm : Form
    {
        public PracticalExamTemplateForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Supported file formats|*.zip;*.rar|ZIP files|*.zip|RAR files|*.rar";
                    openFileDialog.Multiselect = false;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        // Get full zip file path
                        var filePath = openFileDialog.FileName;

                        var destinationPath = Util.ExecutablePath() + @"\Student";

                        // Get zip file name without extension
                        var filename = Path.GetFileNameWithoutExtension(filePath);

                        // Get script folder
                        var scriptFolder = Path.Combine(destinationPath, filename);

                        // Compare zip file name with practical exam code from api
                        if (Directory.Exists(scriptFolder))
                        {
                            Directory.Delete(scriptFolder,true);
                        }
                        if (Directory.Exists(destinationPath))
                        {
                            Util.UnarchiveFile(filePath, destinationPath);
                            txtPath.Text = scriptFolder;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not import script file!", "Error occurred");
                Util.LogException("ImportTemplate", ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnrollForm enroll = new EnrollForm();
            enroll.Show();
            this.Visible = false;
        }
    }
}
