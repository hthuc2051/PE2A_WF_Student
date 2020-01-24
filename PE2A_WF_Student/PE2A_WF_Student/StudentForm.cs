using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Student
{
    public partial class StudentForm : Form
    {
        private string FileName = "";
        public string studentID { get; set; }
        public string submitAPIUrl { get; set; }
        public StudentForm()
        {
            InitializeComponent();
        }

        private async Task<String> sendFile(String fileName)
        {
            //var client = new WebClient();
            var uri = new Uri(submitAPIUrl);
            string fileExtension = fileName.Substring(fileName.IndexOf('.'));
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var stream = File.ReadAllBytes(fileName);
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    //form.Add(new ByteArrayContent(stream,0,stream.Length), "file");
                    //file => byte[]
                    //multipartFile => stream
                    HttpContent content = new StreamContent(new FileStream(fileName, FileMode.Open));
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = studentID + fileExtension
                    };
                    form.Add(content, "file");
                    form.Add(new StringContent(studentID), "studentCode");
                    using (var message = await client.PostAsync(uri, form))
                    {
                        var result = await message.Content.ReadAsStringAsync();
                        return "Sent !";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "Error !";

        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            String result = await sendFile(FileName);
            MessageBox.Show(result);
        }

        private void ShowSelectedFile()
        {
            btnSubmit.Visible = true;
            imgSubmit.Image = PE2A_WF_Student.Properties.Resources.files_and_folders;
            txtFileName.Visible = true;
        }



        private void StudentSubmit_Load(object sender, EventArgs e)
        {

        }

        private void txtFileName_Click(object sender, EventArgs e)
        {

        }

        private void imgFile_Click_1(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                txtFileName.Text = FileName;
                ShowSelectedFile();
            }
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
