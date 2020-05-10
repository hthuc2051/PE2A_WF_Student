
using LibGit2Sharp;
using PE2A_WF_Student.Models;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
        public Boolean StartPractical { get; set; }
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
            StartPractical = false;

        }
        private void TimeRemaining()
        {
            try
            {
                time = new System.Timers.Timer();
                time.Interval = 1000;
                time.Elapsed += OnTimeEvent;
                time.Start();
            }
            catch (Exception ex)
            {
                Util.LogException("TimeRemaining", ex.Message);
            }

        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.InvokeEx(async f =>
                {
                    lbTime.Text = practicalTimeMinute.ToString("00") + ":" + practicalTimeSecond.ToString("00");
                    if (practicalTimeMinute == 0 && practicalTimeSecond == 0)
                    {
                        practicalTimeMinute = 0;
                        practicalTimeSecond = 0;
                        time.Stop();
                        MessageBox.Show("Time is over. System will be automated submit your branch", "TIMEOVER");

                        //auto submit
                        string startupPath = Util.ExecutablePath() + @"\Submission";
                        String result = "";
                        if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA_WEB))
                        {
                            string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_JAVA_WEB + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                            FileName = projectDirectory;
                            result = await SendFileJavaWeb(FileName);
                        }
                        else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA))
                        {
                            string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_JAVA + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                            FileName = projectDirectory;
                            result = await SendFileJava(FileName);
                        }
                        else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_C_SHARP))
                        {
                            string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_C_SHARP + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                            FileName = projectDirectory;
                            result = await SendFile(FileName);
                        }
                        else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_C))
                        {
                            string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_C + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                            FileName = projectDirectory;
                            result = await SendFile(FileName);
                        }

                        ShowWaittingMessage();
                        MessageBox.Show(result);
                        //end submit
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
            catch (Exception ex)
            {
                Util.LogException("OnTimeEvent", ex.Message);
            }
        }

        private void LoadPracticalDoc(string filePath)
        {
            try
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
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing);
                    document.ActiveWindow.Selection.WholeStory();
                    document.ActiveWindow.Selection.Copy();
                    IDataObject dataObject = Clipboard.GetDataObject();
                    rtbDocument.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
                    application.Quit(ref missing, ref missing, ref missing);
                }
            }
            catch (Exception ex)
            {
                Util.LogException("LoadPracticalDoc", ex.Message);
            }
        }
        private async Task<String> SendFileJava(String fileName)
        {
            try
            {
                //pre-submit
                String saveProject = Util.PracticalSave(PracticalExamType);
                String destinationFile = fileName;
                String extractPath = Util.ExecutablePath() + @"\Submission\" + Constant.PRACTICAL_EXAM_JAVA;
                //using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                //{
                //    archive.AddAllFromDirectory(saveProject, ".", SearchOption.AllDirectories);
                //    archive.SaveTo(destinationFile, CompressionType.Deflate);
                //}
                //extract file and delete separated file
                //Util.DeleteFile(destinationFile);
                Util.UnarchiveFile(destinationFile, extractPath); //student and program file
                Util.DeleteFile(extractPath + @"\Program.java"); // not needed
                Util.DeleteFile(destinationFile);//not needed
                using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(extractPath, ".", SearchOption.AllDirectories);
                    archive.SaveTo(destinationFile, CompressionType.Deflate);
                }

                //submit xong r  a oi //alo e sua xong r a tést tiep di
                var uri = new Uri(SubmitAPIUrl);
                string fileExtension = fileName.Substring(fileName.IndexOf('.'));

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
                Util.LogException("SendFile", ex.Message);
            }

            return "Error !";
        }

        private async Task<String> SendFile(String fileName)
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
                Util.LogException("SendFile", ex.Message);
            }
            return "Error !";
        }

        private void ZipFilePath(string path, bool isWebPage)
        {
            try
            {
                using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(path, ".", SearchOption.AllDirectories);
                    if (isWebPage)
                    {
                        //archive.SaveTo(Util.DestinationOutputPath("webapp"), CompressionType.Deflate);
                    }
                    else
                    {
                        // archive.SaveTo(Util.DestinationOutputPath(StudentID), CompressionType.Deflate);
                    }

                }
            }
            catch (Exception ex)
            {
                Util.LogException("ZipFilePath", ex.Message);

            }

        }

        private async Task<String> SendFileJavaWeb(String fileName)
        {
            string startupPath = Util.ExecutablePath() + @"\Submission" + @"\" + Constant.PRACTICAL_EXAM_JAVA_WEB;
            string destinationPath = startupPath;
            string webappPath = startupPath + @"\webapp";
            string workPath = startupPath + @"\work";
            string workWebPagePath = startupPath + @"\work\web";
            string webPageZip = startupPath + StudentID + "_WEB.zip";
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
                        FileName = StudentID + "_WEB" + fileExtension
                    };
                    form.Add(content, "file");
                    form.Add(webContent, "webFile");
                    form.Add(new StringContent(StudentID), "studentCode");
                    form.Add(new StringContent(ScriptCode), "scriptCode");
                    using (var message = await client.PostAsync(uri, form))
                    {
                        var result = await message.Content.ReadAsStringAsync();
                        //time.Stop();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Util.LogException("SendFileJavaWeb", ex.Message);

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
            try
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
                                        if (MessageBox.Show(msg, "Information", MessageBoxButtons.OK) == DialogResult.OK)
                                        {
                                            Environment.Exit(Environment.ExitCode);
                                        }
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
                                        var history = new History();
                                        history.No = 0;
                                        history.Point = msg.Replace(Constant.RETURN_POINT, "").Trim();
                                        history.StudentCode = StudentID;
                                        history.PracticalName = PracticalExamType;
                                        history.PracticalDate = DateTime.Now.ToString("yyyy-MM-dd");
                                        Util.CacheHistory(history);
                                        break;
                                    }
                                    else if (msg.Contains(Constant.RETURN_EXAM_SCIPT))
                                    {
                                        if (StartPractical == false)
                                        {
                                            msg = msg.Replace(Constant.RETURN_EXAM_SCIPT, "");
                                            string time = msg;
                                            practicalTimeMinute = int.Parse(time);
                                            this.InvokeEx(f => this.lbTime.Visible = true);
                                            TimeRemaining();
                                            StartPractical = true;
                                        }
                                    }
                                    else
                                    {
                                        this.InvokeEx(func => btnSave.Enabled = true);
                                        string startupPath = Util.ExecutablePath();
                                        string projectDirectory = startupPath + @"\TemplateProject\PracticalExamDocument.docx";
                                        File.WriteAllBytes(projectDirectory, clientData);
                                        Thread.Sleep(1500);
                                        this.InvokeEx(f => LoadPracticalDoc(projectDirectory));
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
            catch (Exception ex)
            {
                Util.LogException("StartServerTCP", ex.Message);
            }

        }

        private void GetPracticalExamType(string practicalExamCode)
        {
            try
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
            catch (Exception ex)
            {
                Util.LogException("GetPracticalExamType", ex.Message);
            }

        }

        private byte[] GetAllByte(NetworkStream getStreamForFile)
        {
            try
            {
                byte[] getByte = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024 * 100];
                        count = getStreamForFile.Read(buf, 0, 1024 * 100); //read 50kb and store it to buf
                        ms.Write(buf, 0, count);
                    } while (getStreamForFile.DataAvailable);

                    getByte = ms.ToArray();

                }
                //var finalByte = new byte[getByte.Length + clientData.Length];
                //Buffer.BlockCopy(clientData, 0, finalByte, 0, clientData.Length);
                //Buffer.BlockCopy(getByte, 0, finalByte, clientData.Length, getByte.Length);
                return getByte;
            }
            catch (Exception ex)
            {
                Util.LogException("GetAllByte", ex.Message);

            }
            return null;

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
            try
            {
                Task.Run(async delegate
                {
                    await Task.Delay(10000);
                    if (isLoading)
                    {
                        this.InvokeEx(f =>
                        {
                            if (MessageBox.Show("Remind your lecturer to start server and try to Enroll again", "Information", MessageBoxButtons.OK) == DialogResult.OK)
                            {
                                Environment.Exit(Environment.ExitCode);
                            }
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Util.LogException("StudentForm_Load", ex.Message);

            }

        }

        private void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var option = MessageBox.Show("Are you sure you want to really exit ? ",
                            "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (option == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                //remove git branch
                String projectDirectory = Util.PracticalPath(PracticalExamType);
                RemoveAllBranch(projectDirectory);
                Console.WriteLine("Disposed");
                e.Cancel = false;
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string startupPath = Util.ExecutablePath();

                if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA_WEB))
                {
                    string webPageDirectory = startupPath + Constant.JAVA_WEB_PATH_GIT; // java web path
                    SaveYourWork(webPageDirectory);
                }
                else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA))
                {
                    string projectDirectory = startupPath + Constant.JAVA_PATH_GIT; //java
                    SaveYourWork(projectDirectory);
                }
                else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_C_SHARP))
                {
                    string projectDirectory = startupPath + Constant.CS_PATH_GIT; //c#
                    SaveYourWork(projectDirectory);
                }
                else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_C))
                {
                    string projectDirectory = startupPath + Constant.C_PATH_GIT; //c#
                    SaveYourWork(projectDirectory);
                }
                UpdateGridViewBranch();
            }
            catch (Exception ex)
            {
                Util.LogException("btnSave_Click", ex.Message);

            }

        }

        public void RemoveAllBranch(String repoPath)
        {
            try
            {
                using (var repo = new Repository(repoPath))
                {
                    if (repo.Branches.Count() > 0)
                    {
                        Commands.Checkout(repo, repo.Branches["master"]);
                        foreach (var item in repo.Branches)
                        {
                            if (!item.FriendlyName.Contains("master"))
                            {
                                repo.Branches.Remove(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogException("RemoveAllBranch", ex.Message);
            }

        }
        private void SaveYourWork(String workingDirectory)
        {
            try
            {
                if (!btnSubmit.Enabled)
                {
                    btnSubmit.Enabled = true;
                }
                if (this.ListBranches.Count == 0)
                {
                    var repoPath = Repository.Init(workingDirectory);
                    RemoveAllBranch(repoPath);
                }
                using (var repo = new Repository(workingDirectory))
                {
                    RepositoryStatus status = repo.RetrieveStatus();
                    if (status.IsDirty)
                    {
                        var branchName = StudentID + "-version" + this.numberOfVersion;
                        Commands.Stage(repo, "*");
                        Commit commit = repo.Commit(branchName + " updating files..", new Signature(StudentID, StudentID + "@fpt.edu.vn", DateTimeOffset.Now),
                        new Signature(StudentID, StudentID + "@fpt.edu.vn", DateTimeOffset.Now));
                        repo.Branches.Add(branchName, commit);
                        Console.WriteLine("Commited");
                        this.ListBranches.Add(new BranchModel
                        {
                            BranchName = branchName,
                            CommitTime = DateTime.Now.ToString()
                        });
                        this.numberOfVersion++;
                        string projectDirectory = Util.PracticalPath(PracticalExamType);
                        ZipYourChosenBranch(projectDirectory, branchName);
                        lbCurrentBranch.Text = branchName;
                    }
                    else
                    {
                        Console.WriteLine("Nothing changes");
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogException("SaveYourWork", ex.Message);
            }
            // Checkout branch
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
            try
            {
                dgvStudentBranch.Rows.Clear();
                foreach (var item in ListBranches)
                {
                    dgvStudentBranch.Rows.Add(new string[] { item.BranchName, item.CommitTime });
                }
            }
            catch (Exception ex)
            {
                Util.LogException("UpdateGridViewBranch", ex.Message);
            }

        }

        private void dgvStudentBranch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudentBranch.Rows.Count > 0)
            {
                if (MessageBox.Show("Do you want to choose this version?", "Checkout version", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string startupPath = Util.ExecutablePath();
                    string repoDirectory = startupPath + @"\Student\PracticalExamStudent";
                    //string projectDirectory = Directory.GetParent(startupPath).Parent.FullName + @"\Student\PracticalExamStudent\src\java\com\practicalexam"; //folder mà Student sẽ làm
                    var branchName = dgvStudentBranch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    ZipYourChosenBranch(repoDirectory, branchName);
                    lbCurrentBranch.Text = branchName;
                }
            }
        }

        private void ZipYourChosenBranch(String repoDirectory, String branchName)
        {
            try
            {
                using (var repo = new Repository(repoDirectory))
                {
                    var branch = repo.Branches[branchName];
                    if (branch != null)
                    {
                        Commands.Checkout(repo, branch);
                        Console.WriteLine("Checkout");
                    }
                }
                Thread.Sleep(1500);
                var zipPath = Util.PracticalSave(PracticalExamType); // zip all file and folder in here
                ListAllFiles(zipPath);
            }
            catch (Exception ex)
            {
                Util.LogException("ZipYourChosenBranch", ex.Message);

            }

        }

        private void ListAllFiles(String folder)
        {
            try
            {
                String practicalType = PracticalExamType;
                String destinationPath = Path.Combine(Util.ExecutablePath() + @"\Submission\" + practicalType);
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                if (File.Exists(Util.DestinationOutputPath(StudentID, practicalType)))
                {
                    File.Delete(Util.DestinationOutputPath(StudentID, practicalType));
                }
                //ZipFile.CreateFromDirectory(folder, Util.DestinationOutputPath(StudentID));
                using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(folder, ".", SearchOption.AllDirectories);
                    archive.SaveTo(Util.DestinationOutputPath(StudentID, practicalType), CompressionType.Deflate);
                }
            }
            catch (Exception ex)
            {
                Util.LogException("ListAllFiles", ex.Message);
            }
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to submit your work?", "Submission", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnSubmit.Enabled = false;
                btnSave.Enabled = false;
                dgvStudentBranch.Enabled = false;
                string startupPath = Util.ExecutablePath() + @"\Submission";
                String result = "";
                if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA_WEB))
                {
                    string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_JAVA_WEB + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                    FileName = projectDirectory;
                    result = await SendFileJavaWeb(FileName);
                }
                else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_JAVA))
                {
                    string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_JAVA + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                    FileName = projectDirectory;
                    result = await SendFileJava(FileName);
                }
                else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_C_SHARP))
                {
                    string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_C_SHARP + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                    FileName = projectDirectory;
                    result = await SendFile(FileName);
                }
                else if (PracticalExamType.Equals(Constant.PRACTICAL_EXAM_C))
                {
                    string projectDirectory = startupPath + @"\" + Constant.PRACTICAL_EXAM_C + @"\" + StudentID + ".zip"; // ...Submission/[Practical_Type]/StudentId.zip
                    FileName = projectDirectory;
                    result = await SendFile(FileName);
                }
                ShowWaittingMessage();
                MessageBox.Show(result);
            }
        }


        private void rtbDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                MessageBox.Show("Cut/Copy and Paste Options are disabled");
            }
        }
    }
}
