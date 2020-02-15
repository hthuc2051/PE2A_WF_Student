
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Student
{
    public partial class StudentForm : Form
    {
        private string FileName = "";
        public string StudentID { get; set; }
        public string SubmitAPIUrl { get; set; }
        public string ScriptCode { get; set; }
        public TcpClient client;
        Thread listeningThread;
        public StudentForm()
        {
            InitializeComponent();
        }

        private async Task<String> sendFile(String fileName)
        {
            //var client = new WebClient();
            var uri = new Uri(SubmitAPIUrl);
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
                        FileName = StudentID + fileExtension
                    };
                    form.Add(content, "file");
                    form.Add(new StringContent(StudentID), "studentCode");
                    form.Add(new StringContent(ScriptCode), "scriptCode");
                    using (var message = await client.PostAsync(uri, form))
                    {
                        var result = await message.Content.ReadAsStringAsync();
                        return "Submit Successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "Error !";

        }
        private void StartTCPClient(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                client = new System.Net.Sockets.TcpClient("127.0.0.1", 9997);
                while (true)
                {
                    WaitForServerRequest(client);
                }
            });
        }
        private void WaitForServerRequest(TcpClient client)
        {
            if (client != null)
            {
                var getMsg = client.GetStream();
                if (getMsg != null)
                {
                    byte[] data = new byte[1024 * 1024];
                    getMsg.Read(data, 0, data.Length);
                    Console.WriteLine("You got a package ..." + Util.receiveMessage(data));
                    Console.WriteLine("Client send a message to server");
                    String msg = "Hello I am Client";
                    Util.sendMessage(System.Text.Encoding.Unicode.GetBytes(msg), client);
                }
            }
        }
        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = false;
            String result = await sendFile(FileName);
            ShowWaittingMessage();
            MessageBox.Show(result);
            listeningThread = new Thread(ListenToLecturer);
            listeningThread.Start();
        }

        private void ShowWaittingMessage()
        {
            imgSubmit.Visible = false;
            txtFileName.Visible = false;
            lbPoint.Visible = true;
        }

        private void ListenToLecturer()
        {
            string returnMessage = Util.GetMessageFromTCPConnection(Constant.STUDENT_LISTENING_PORT, Constant.MAXIMUM_REQUEST);
            Console.WriteLine(returnMessage);
            this.InvokeEx(f => lbPoint.Text = "Your point: " + returnMessage);
            
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

        private void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
