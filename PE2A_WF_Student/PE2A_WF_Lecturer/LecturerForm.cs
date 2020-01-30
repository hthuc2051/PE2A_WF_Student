using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Lecturer
{
    public partial class LecturerForm : Form
    {
        private int count = 0;

        public LecturerForm()
        {
            InitializeComponent();
            lvSubmitedFiles.View = View.Details;
            lvSubmitedFiles.Columns.Add("No.");
            lvSubmitedFiles.Columns.Add("File name");
            lvSubmitedFiles.Columns[0].Width = -2;
            lvSubmitedFiles.Columns[1].Width = -2;
            lvSubmitedFiles.GridLines = true;
            //listen to Broadcast connection
            //PE2A_WF_Student.Util.ListeningToBroadcastUDPConnection(Constain.LECTURER_LISTENING_PORT);
        }

        private void btnEstimate_Click(object sender, EventArgs e)
        {
            var result = DownloadFile();
        }
        private HttpClient client = new HttpClient();

        private async Task<String> DownloadFile()
        {
            string studentCode = "";
            string startupPath = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(startupPath).Parent.FullName;
            string submitPath = projectDirectory;
            var filename = @"\JavaWebSubmit.zip";
            var uri = new Uri("http://localhost:8081/api/download");

            try
            {
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = await content.ReadAsStreamAsync();

                        // Get list of headers from content of response
                        HttpContentHeaders headers = content.Headers;

                        // Get content disposition
                        ContentDispositionHeaderValue dispositionHeaderValue = headers.ContentDisposition;

                        // Extract file name from content disposition
                        filename = dispositionHeaderValue.FileName;

                        if (filename != null)
                        {
                            using (var file = File.Create(submitPath + filename))
                            {
                                await result.CopyToAsync(file);
                                lvSubmitedFiles.Items.Add(new ListViewItem(new string[] { "" + ++count, filename }));
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            ZipFile.ExtractToDirectory(submitPath + filename, submitPath);


            if (Directory.Exists(submitPath + @"\JavaWebSubmit"))
            {
                string[] getAllFilePath = Directory.GetFiles(submitPath + @"\JavaWebSubmit");
                foreach (var item in getAllFilePath)
                {
                    try
                    {
                       
                        string zipPath = submitPath + filename;
                        string javaServerPath = projectDirectory + @"\ServerJavaWeb";
                        string extractJavaPath = projectDirectory + @"\ServerJavaWeb\src\main\java";
                        string strCmdText = "/C cd " + projectDirectory + "&cd ServerJavaWeb&mvn clean package";
                        ZipFile.ExtractToDirectory(item, extractJavaPath);
                        Thread.Sleep(5000);
                        string[] arr = item.Split(new[] { "\\" }, StringSplitOptions.None);

                        studentCode = arr[arr.Length - 1].Replace(".zip","");

                        string gitCMD = "git branch submit/" + studentCode + "&" +
                           "git add .&" +
                           "git commit -m \"" + studentCode + "\"&" +
                           "git push --set-upstream origin submit/" + studentCode;

                        File.AppendAllText(javaServerPath+ @"\mark-result.txt", studentCode + Environment.NewLine);


                        // For execute cmd
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "CMD.exe";
                        startInfo.Arguments = strCmdText;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        Process process = new Process();
                        process.StartInfo = startInfo;
                        process.Start();
                        process.WaitForExit();
                        Console.WriteLine("Check successfully: "+ studentCode);
                        Directory.Delete(extractJavaPath + @"\com", true);
                        // Process.Start("CMD.exe", "/C cd " + javaServerPath + gitCMD + "&cmd /k");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                MessageBox.Show("Successfully");
            }



            return null;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            // pass socket or list socket to constructor (later)
            ShowResult showResult = new ShowResult(); 
            showResult.Show();
            this.Dispose(true);
        }
    }
}
