using PE2A_WF_Student.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Student
{
    public partial class BranchWorking : Form
    {
        private List<BranchModel> branchModels;
        private Form previousForm;
        public BranchWorking()
        {
            InitializeComponent();
        }
        public BranchWorking(List<BranchModel> branchModels,Form previousForm)
        {
            InitializeComponent();
            this.branchModels = branchModels;
            this.previousForm = previousForm;
            LoadData();
        }
        private void LoadData()
        {
            this.dgView.DataSource = this.branchModels;
        }
        // Xuất ra file Zip ngoại trừ file git
        private void ListAllFiles(String folder)
        {
            if (File.Exists(Util.DestinationOutputPath()))
            {
                File.Delete(Util.DestinationOutputPath());
            }
            ZipFile.CreateFromDirectory(folder, Util.DestinationOutputPath());
        }
        private void ZipYourChosenBranch(String workingDirectory, String branchName)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
                },
            };

            process.Start();
            using (var writer = process.StandardInput)
            {
                writer.WriteLine("git checkout " + branchName);
            }
            Thread.Sleep(1500);
            var zipPath = workingDirectory + @"\student"; // zip all file and folder in here
            ListAllFiles(zipPath);
        }

        private void dgView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Student\PracticalExamStudent\src\com\practicalexam"; //folder mà Student sẽ làm
            var branchName = dgView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            ZipYourChosenBranch(projectDirectory, branchName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.previousForm.Show();

        }
    }
}
