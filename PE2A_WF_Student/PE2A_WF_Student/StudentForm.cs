
using PE2A_WF_Student.Models;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
//using System.IO.Compression;
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
        public string PracticalExamType { get; set; }
        public TcpListener listener;
        TcpClient tcpClient;
        bool isLoading = true;
        System.Timers.Timer time;
        int practicalTimeMinute = 60;
        int practicalTimeSecond = 00;
        // DateTime startTime = new DateTime(2020, 02, 17, 18, 00, 00);
        public StudentForm()
        {
            InitializeComponent();
            StartServerTCP();
            //string startupPath = System.IO.Directory.GetCurrentDirectory();
            //string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\TemplateProject\Java_439576447_DE01.docx";
            //this.InvokeEx(f => loadPracticalDoc(projectDirectory));

        }
        private void TimeRemaining()
        {
            //var endTime = DateTime.Now;
            //Console.WriteLine(endTime);
            //practicalTimeMinute = 60 - (endTime.Minute - startTime.Minute);
            //practicalTimeSecond = 60 - (endTime.Second - startTime.Second);
            //var date = DateTime.Now.Minute;
            //Console.WriteLine(date);
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

        private void loadPracticalDoc(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Name.StartsWith("~$"))
            {
                rtbDocument.Visible = true;
                object readOnly = true;
                object visible = true;
                object save = false;
                object fileName = filePath;
                object newTemplate = false;
                object docType = 0;
                object missing = Type.Missing;
                Microsoft.Office.Interop.Word.Document document;
                Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application() { Visible = false };
                document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing);
                document.ActiveWindow.Selection.WholeStory();
                document.ActiveWindow.Selection.Copy();
                IDataObject dataObject = Clipboard.GetDataObject();
                rtbDocument.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
                application.Quit(ref missing, ref missing, ref missing);
            }

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

        private void zipFile(string path, bool isWebPage)
        {
            using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
            {
                archive.AddAllFromDirectory(path, ".", SearchOption.AllDirectories);
                if (isWebPage)
                {
                    archive.SaveTo(Util.DestinationOutputPath("webapp"), CompressionType.Deflate);
                }
                else
                {
                    archive.SaveTo(Util.DestinationOutputPath(StudentID), CompressionType.Deflate);
                }

            }
        }

        private async Task<String> sendFileJavaWeb(String fileName)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string destinationPath = Directory.GetParent(startupPath).Parent.FullName + @"\Submission";
            string webappPath = Directory.GetParent(startupPath).Parent.FullName + @"\Submission\webapp";
            string workPath = Directory.GetParent(startupPath).Parent.FullName + @"\Submission\work";
            string workWebPagePath = Directory.GetParent(startupPath).Parent.FullName + @"\Submission\work\web";
            string webPageZip = Directory.GetParent(startupPath).Parent.FullName + @"\Submission\"+StudentID+"_WEB.zip";
            //extract
            Util.UnarchiveFile(fileName, workPath);
            //copy

            //Now Create all of the directories
            Util.Copy(workWebPagePath, webappPath);
            // Directory.Move(destinationPath, webappPath);
            // File.Copy()
            string srcCodePath = Path.Combine(destinationPath, @"work\src\java\com\practicalexam\student");

            if (File.Exists(webPageZip))
            {
                File.Delete(webPageZip);
            }
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            //       zipFile(webPagePath, true);
            ZipFile.CreateFromDirectory(webappPath, webPageZip, CompressionLevel.NoCompression, true);
            ZipFile.CreateFromDirectory(srcCodePath, fileName, CompressionLevel.NoCompression, true);
            var uri = new Uri(SubmitAPIUrl);
            string fileExtension = fileName.Substring(fileName.IndexOf('.'));
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var stream = File.ReadAllBytes(fileName);
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    HttpContent content = new StreamContent(new FileStream(fileName, FileMode.Open));
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = StudentID + fileExtension
                    };
                    HttpContent webContent = new StreamContent(new FileStream(webPageZip, FileMode.Open));
                    webContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "webFile",
                        FileName = StudentID+ "_WEB" + fileExtension
                    };
                    form.Add(content, "file");
                    form.Add(webContent, "webFile");
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

        private void SendTimeSubmission(string studentCode)
        {
            String msg = studentCode + "=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            byte[] dataMsg = Encoding.Unicode.GetBytes(msg);
            Util.sendMessage(dataMsg, tcpClient);
        }
        private void ShowWaittingMessage()
        {
            //imgSubmit.Visible = false;
            //txtFileName.Visible = false;
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

                        if (tcpClient != null)
                        {

                            var getStream = tcpClient.GetStream();
                            Thread.Sleep(1000);
                            var getStreamForFile = getStream;
                            byte[] clientData = GetAllByte(getStreamForFile);
                            //          byte[] clientData = new byte[1024 * 5];
                            if (getStream != null)
                            {
                                //   byte[] byteToUse = Util.ConvertStreamToByte(getStreamForFile);
                                //  getStream.Read(clientData, 0, clientData.Length); // chep byte  vo clientData
                                string msg = Util.receiveMessage(clientData);
                                if (msg.Equals(Constant.EXISTED_IP_MESSAGE))
                                {
                                    MessageBox.Show(msg);
                                }
                                else if (msg.Contains(Constant.RETURN_URL_CODE))
                                {
                                    msg = msg.Replace(Constant.RETURN_URL_CODE, "");
                                    string decode = Util.Decode(msg, "SE1267");
                                    string[] msgArr = decode.Split('=');
                                    SubmitAPIUrl = msgArr[1];
                                    ScriptCode = msgArr[2];
                                    GetPracticalExamType(msgArr[3].ToUpper());
                                    isLoading = false;
                                    //this.InvokeEx(f => imgSubmit.Visible = true);
                                    this.InvokeEx(f => loadingBox.Visible = false);
                                }
                                else if (msg.Contains(Constant.RETURN_POINT))
                                {
                                    this.InvokeEx(f => lbPoint.Text = msg);
                                    break;
                                }
                                else if (msg.Contains(Constant.RETURN_EXAM_SCIPT))
                                {
                                    msg = msg.Replace(Constant.RETURN_EXAM_SCIPT, "");
                                    string time = msg;
                                    practicalTimeMinute = int.Parse(time);
                                    this.InvokeEx(f => this.lbTime.Visible = true);
                                    TimeRemaining();
                                }
                                else
                                {


                                    //using (var fs = new FileStream(@"D:\Capstone_WF\PE2A_WF_Student\PE2A_WF_Student\TemplateProject\testDoc.docx", FileMode.OpenOrCreate, FileAccess.Write))
                                    //{
                                    //    fs.Write(clientData, 0, clientData.Length);

                                    //}
                                    string startupPath = System.IO.Directory.GetCurrentDirectory();
                                    string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\TemplateProject\testDoc.docx";
                                    //if (getStream.DataAvailable)
                                    //{
                                    //byte[] getByte = null;
                                    //using (MemoryStream ms = new MemoryStream())
                                    //{
                                    //    int count = 0;
                                    //    do
                                    //    {
                                    //        byte[] buf = new byte[1024 * 5];
                                    //        count = getStreamForFile.Read(buf, 0, 1024 * 5);
                                    //        ms.Write(buf, 0, count);
                                    //    } while (count > 0 && tcpClient.Available != 0);

                                    //    getByte = ms.ToArray();

                                    //}
                                    //var finalByte = new byte[getByte.Length + clientData.Length];
                                    //Buffer.BlockCopy(clientData, 0, finalByte, 0, clientData.Length);
                                    //Buffer.BlockCopy(getByte, 0, finalByte, clientData.Length, getByte.Length);

                                    File.WriteAllBytes(projectDirectory, clientData);
                                    this.InvokeEx(f => loadPracticalDoc(projectDirectory));
                                    //  }

                                }
                                Console.WriteLine("Lecturer: " + msg);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }
            });
        }

        private void GetPracticalExamType(string practicalExamCode)
        {
            if (practicalExamCode.Contains(Constant.PRACTICAL_EXAM_JAVA_WEB))
            {
                PracticalExamType = Constant.PRACTICAL_EXAM_JAVA_WEB;
            }
            else if (practicalExamCode.Contains(Constant.PRACTICAL_EXAM_JAVA))
            {
                PracticalExamType = Constant.PRACTICAL_EXAM_JAVA;
            }
            else if (practicalExamCode.Contains(Constant.PRACTICAL_EXAM_C_SHARP))
            {
                PracticalExamType = Constant.PRACTICAL_EXAM_C_SHARP;
            }
            else
            {
                PracticalExamType = Constant.PRACTICAL_EXAM_C;
            }
        }

        private byte[] GetAllByte(NetworkStream getStreamForFile)
        {
            byte[] getByte = null;
            using (MemoryStream ms = new MemoryStream())
            {
                int count = 0;
                do
                {
                    byte[] buf = new byte[1024 * 5];
                    count = getStreamForFile.Read(buf, 0, 1024 * 5);
                    ms.Write(buf, 0, count);
                } while (getStreamForFile.DataAvailable);

                getByte = ms.ToArray();

            }
            //var finalByte = new byte[getByte.Length + clientData.Length];
            //Buffer.BlockCopy(clientData, 0, finalByte, 0, clientData.Length);
            //Buffer.BlockCopy(getByte, 0, finalByte, clientData.Length, getByte.Length);
            return getByte;
        }


        private void ShowSelectedFile()
        {
            btnSubmit.Visible = true;
            //imgSubmit.Image = PE2A_WF_Student.Properties.Resources.files_and_folders;
            //txtFileName.Visible = true;
        }


        //private void imgFile_Click_1(object sender, EventArgs e)
        //{
        //    var openFileDialog = new OpenFileDialog();
        //    var dialogResult = openFileDialog.ShowDialog();
        //    if (dialogResult == DialogResult.OK)
        //    {
        //        FileName = openFileDialog.FileName;
        //        txtFileName.Text = FileName;
        //        ShowSelectedFile();
        //    }
        //}

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
            string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Student\PracticalExamStudent\src\com\practicalexam";

            if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA_WEB))
            {
                string webPageDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Student\PracticalExamStudent";
                SaveYourWork(webPageDirectory);
            }
            else
            {
                SaveYourWork(projectDirectory);
            }
            UpdateGridViewBranch();
        }

        private void SaveYourWork(String workingDirectory)
        {
            if (!btnSubmit.Enabled)
            {
                btnSubmit.Enabled = true;
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
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

            // Checkout branch
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string projectDirectory = workingDirectory;
            ZipYourChosenBranch(projectDirectory, branchName);
            lbCurrentBranch.Text = branchName;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Equals(tabControl.TabPages[1]))
            {
                UpdateGridViewBranch();
            }
        }

        private void UpdateGridViewBranch()
        {
            dgvStudentBranch.Rows.Clear();
            foreach (var item in ListBranches)
            {
                dgvStudentBranch.Rows.Add(new string[] { item.BranchName, item.CommitTime });
            }
        }

        private void dgvStudentBranch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Do you want to choose this version?", "Checkout version", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Student\PracticalExamStudent\src\com\practicalexam"; //folder mà Student sẽ làm
                var branchName = dgvStudentBranch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                ZipYourChosenBranch(projectDirectory, branchName);
                lbCurrentBranch.Text = branchName;
            }
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
            var zipPath = workingDirectory; // zip all file and folder in here
            ListAllFiles(zipPath);
        }

        private void ListAllFiles(String folder)
        {
            try
            {
                if (File.Exists(Util.DestinationOutputPath(StudentID)))
                {
                    File.Delete(Util.DestinationOutputPath(StudentID));
                }
                //ZipFile.CreateFromDirectory(folder, Util.DestinationOutputPath(StudentID));
                using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(folder, ".", SearchOption.AllDirectories);
                    archive.SaveTo(Util.DestinationOutputPath(StudentID), CompressionType.Deflate);

                }
            }
            catch
            {

            }
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to submit your work?", "Submission", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnSubmit.Enabled = false;
                btnSave.Enabled = false;
                dgvStudentBranch.Enabled = false;
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Submission\" + StudentID + ".zip";
                FileName = projectDirectory;
                String result = "";
                if (PracticalExamType == Constant.PRACTICAL_EXAM_JAVA_WEB)
                {
                    result = await sendFileJavaWeb(FileName);
                }
                else
                {
                    result = await sendFile(FileName);
                }

                ShowWaittingMessage();
                MessageBox.Show(result);
                //if (result.Trim().Equals(Constant.SUBMMIT_SUCCESS_MESSAGE))
                //{
                //    SendTimeSubmission(StudentID);
                //}

                //listeningThread = new Thread(ListenToLecturer);
                //listeningThread.Start();
            }
        }
    }
}
