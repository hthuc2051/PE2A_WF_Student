
using PE2A_WF_Student.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using System.Timers;
using System.Windows.Forms;

namespace PE2A_WF_Student
{
    // Folder TemplateProject là project mà mình dựng sẵn cho học sinh để 
    // Bấm 1 nút nó sẽ clone ra folder Student để học sinh làm
    public partial class StudentForm : Form
    {
        private int numberOfVersion = 1;
        private List<BranchModel> ListBranches = new List<BranchModel>();
        private string FileName = "";
        public string StudentID { get; set; }
        public string SubmitAPIUrl { get; set; }
        public string ScriptCode { get; set; }
        public TcpListener listener;
        TcpClient tcpClient;
        bool isLoading = true;
        System.Timers.Timer time;
        int practicalTimeMinute = 60;
        int practicalTimeSecond = 60;
        DateTime startTime = new DateTime(2020, 02, 17, 18, 00, 00);
        public StudentForm()
        {
            InitializeComponent();
            StartServerTCP();
            TimeRemaining();

        }
        private void TimeRemaining()
        {
            var endTime = DateTime.Now;
            Console.WriteLine(endTime);
            practicalTimeMinute = 60 - (endTime.Minute - startTime.Minute);
            practicalTimeSecond = 60 - (endTime.Second - startTime.Second);
            var date = DateTime.Now.Minute;
            Console.WriteLine(date);
            time = new System.Timers.Timer();
            time.Interval = 1000;
            time.Elapsed += OnTimeEvent;
            time.Start();
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            this.InvokeEx(f =>
            {
                lbTime.Text = practicalTimeMinute.ToString("00") + ":" + practicalTimeSecond.ToString("00");
                if (practicalTimeMinute == 0 && practicalTimeSecond == 0)
                {
                    practicalTimeMinute = 0;
                    practicalTimeSecond = 0;
                }
                else
                {
                    if (practicalTimeSecond == 0)
                    {
                        practicalTimeMinute -= 1;
                        practicalTimeSecond = 59;
                    }
                    else
                    {
                        practicalTimeSecond -= 1;
                    }
                }
            });         
        }

        private void loadPracticalDoc()
        {
            rtbDocument.Visible = true;
            object readOnly = true;
            object visible = true;
            object save = false;
            object fileName = @"D:testDoc.docx";
            object newTemplate = false;
            object docType = 0;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Word.Document document;
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application() { Visible = false };
            document =  application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing);
            document.ActiveWindow.Selection.WholeStory();
            document.ActiveWindow.Selection.Copy();
            IDataObject dataObject = Clipboard.GetDataObject();
            rtbDocument.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
            application.Quit(ref missing, ref missing, ref missing);
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
                        return result;
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
            btnSubmit.Visible = false;
            String result = await sendFile(FileName);
            ShowWaittingMessage();
            MessageBox.Show(result);
            if (result.Trim().Equals(Constant.SUBMMIT_SUCCESS_MESSAGE))
            {
                SendTimeSubmission(StudentID);
            }
           
            //listeningThread = new Thread(ListenToLecturer);
            //listeningThread.Start();
        }
        private void SendTimeSubmission(string studentCode)
        {
            String msg = studentCode + "-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            byte[] dataMsg = Encoding.Unicode.GetBytes(msg);
            Util.sendMessage(dataMsg, tcpClient);
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

        // TCP LISTENER
        private void StartServerTCP()
        {
            Task.Run(() =>
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, Constant.STUDENT_LISTENING_PORT);
                listener = new TcpListener(ipEnd);
                listener.Start();
                Console.WriteLine("Server starting ...");
                tcpClient = listener.AcceptTcpClient();
                while (true)
                {
                    try
                    {
                        Console.WriteLine("StartServerTCP");
                        //if (tcpClient.Connected == true)
                        //{
                        //    Console.WriteLine("Client connecting ...");
                        //    String msg = "Hello I am server";
                        //    byte[] data = System.Text.Encoding.Unicode.GetBytes(msg);
                        //    Util.sendMessage(data, tcpClient);
                        //}
                        byte[] clientData = new byte[1024];
                        var getStream = tcpClient.GetStream();
                        if (getStream != null)
                        {
                            getStream.Read(clientData, 0, clientData.Length);
                            String decode = Util.receiveMessage(clientData);
                            string msg = Util.Decode(decode, "SE1267");
                            if (msg.Equals(Constant.EXISTED_IP_MESSAGE))
                            {
                                MessageBox.Show(msg);
                            }
                            else if (msg.Contains("="))
                            {
                                string[] msgArr = msg.Split('=');
                                SubmitAPIUrl = msgArr[1];
                                ScriptCode = msgArr[2];
                                isLoading = false;
                                this.InvokeEx(f => imgSubmit.Visible = true);
                                this.InvokeEx(f =>loadPracticalDoc());
                                this.InvokeEx(f => loadingBox.Visible = false);
                                this.InvokeEx(f => this.lbTime.Visible = true);
                            }
                            else
                            {
                                this.InvokeEx(f => lbPoint.Text = msg);
                                break;
                            }
                            Console.WriteLine("Lecturer: " + msg);
                        }
                    }
                    catch (Exception e)
                    {
                       
                    }
                }
            });
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
         
            Task.Run(async delegate
            {
                await Task.Delay(10000);
                if (isLoading)
                {
                    this.InvokeEx(f => MessageBox.Show("Remind your lecturer to start server and try to Enroll again"));
                }
            });
        }

        private void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Student";
            SaveYourWork(projectDirectory);
        }
        private void SaveYourWork(String workingDirectory)
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
            var branchName = StudentID + "-version" + this.numberOfVersion;
            using (var writer = process.StandardInput)
            {
                if (this.ListBranches.Count == 0)
                {
                    writer.WriteLine("git checkout master");
                    writer.WriteLine("git branch | grep -v " + "master" + " | xargs git branch -D");

                }
                writer.WriteLine("git branch " + branchName);
                writer.WriteLine("git checkout " + branchName);
                writer.WriteLine("git add .");
                writer.WriteLine("git commit -m 'second' ");
            }
            this.ListBranches.Add(new BranchModel
            {
                BranchName = branchName,
                CommitTime = DateTime.Now.ToString()
            });
            this.numberOfVersion++;
        }

        // Mở form để chọn branch để nộp 
        private void btnChoose_Click(object sender, EventArgs e)
        {
            BranchWorking branch = new BranchWorking(this.ListBranches,this);
            branch.Show();
            this.Hide();
        }
    }
}
