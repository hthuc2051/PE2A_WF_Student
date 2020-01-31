﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Lecturer
{
    public partial class LecturerForm : Form
    {
        private int count = 0;
        Image img = new Bitmap(PE2A_WF_Lecturer.Properties.Resources.done);
        DataTable dataTable = new DataTable();
        public LecturerForm()
        {
            InitializeComponent();
            //lvSubmitedFiles.View = View.Details;
            //lvSubmitedFiles.Columns.Add("No.");
            //lvSubmitedFiles.Columns.Add("Student ID");
            //lvSubmitedFiles.Columns.Add("mark");
            //lvSubmitedFiles.GridLines = true;
    
            dataTable.Columns.Add("NO.");
            dataTable.Columns.Add("Student ID");
            dataTable.Columns.Add("Mark");
            dataTable.Columns.Add("Stautus");
            dataTable.Columns.Add("Close",typeof(Image));
            dgvStudent.DataSource = dataTable;

            //listen to student
            ListeningToBroadcastUDPConnection(Constain.LECTURER_LISTENING_PORT);
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
                                //lvSubmitedFiles.Items.Add(new ListViewItem(new string[] { "" + ++count, filename }));
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
        static Socket s;
        static Byte[] buffer;
        private void ListeningToBroadcastUDPConnection(int listeningPort)
        {
            s = new Socket(AddressFamily.InterNetwork,
                          SocketType.Dgram,
                                ProtocolType.Udp);

            IPEndPoint senders = new IPEndPoint(IPAddress.Any, listeningPort);
            EndPoint tempRemoteEP = (EndPoint)senders;
            s.Bind(senders);
            buffer = new byte[1024];
            s.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref tempRemoteEP,
                                            new AsyncCallback(DoReceiveFrom), buffer);
        }

        private void DoReceiveFrom(IAsyncResult iar)
        {
            EndPoint clientEP = new IPEndPoint(IPAddress.Any, 5656);
            int size = s.EndReceiveFrom(iar, ref clientEP);
            if (size > 0)
            {
                byte[] receivedData = new Byte[size];
                receivedData = (byte[])iar.AsyncState;

                ASCIIEncoding eEncpding = new ASCIIEncoding();
                string receivedMessage = eEncpding.GetString(receivedData);
                receivedMessage = receivedMessage.Substring(0, size);
                Console.WriteLine("received message:" + receivedMessage);
                string[] data = receivedMessage.Split('-');
                Thread t = new Thread(() => ReturnWebserviceURL(data[0], int.Parse(data[1]),data[2]));
                t.Start();
              
            }
            s.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                ref clientEP, new AsyncCallback(DoReceiveFrom), buffer);
        }
        private void ReturnWebserviceURL(string ipAddess, int port, string studentID)
        {
            bool isSent = false;
            string submissionURL = "http://" + PE2A_WF_Student.Util.GetLocalIPAddress() + ":8080/api/submission";
            string[] row = new string[] { ++count + "", studentID,"",Constain.STATUSLIST[0]};
            dataTable.Rows.Add(row);
            dataTable.Rows[count - 1][4] = PE2A_WF_Lecturer.Properties.Resources.close;
            do
            {
                try
                {
                    IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ipAddess), port);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(iPEnd);
                    socket.Send(Encoding.UTF8.GetBytes("here is your submission url =" + submissionURL));
                    socket.Close();
                    isSent = true;
                    this.InvokeEx(f =>
                    dgvStudent.DataSource = dataTable);
                    this.InvokeEx(f => dgvStudent.Rows[count - 1].Cells[3].Style.ForeColor = Color.Green);
                }
                catch (Exception e)
                {

                }
            } while (!isSent);

        }
    }
}
