using System;
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
        DataTable dataTable = new DataTable();
        List<StudentDTO> listStudent = new List<StudentDTO>();
        public LecturerForm()
        {
            InitializeComponent();

            dataTable.Columns.Add("NO.");
            dataTable.Columns.Add("Student ID");
            dataTable.Columns.Add("Mark");
            dataTable.Columns.Add("Stautus");
            dataTable.Columns.Add("Close", typeof(Image));
            dgvStudent.DataSource = dataTable;

            //listen to student
            ListeningToBroadcastUDPConnection(Constant.LECTURER_LISTENING_PORT);

        }


        private void btnEstimate_Click(object sender, EventArgs e)
        {
            //get point from file
            GetResultFromFile();
            //send point to client
            foreach (var item in listStudent)
            {
                SendMessage(item.IpAddress.ToString(), item.Port, item.StudentID + ":" + item.Point);
            }

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
                Thread t = new Thread(() => ReturnWebserviceURL(data[0], int.Parse(data[1]), data[2]));
                t.Start();

            }
            s.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                ref clientEP, new AsyncCallback(DoReceiveFrom), buffer);
        }
        private void ReturnWebserviceURL(string ipAddress, int port, string studentID)
        {
            string message;
            if (IsConnected(ipAddress))
            {
                message = Constant.EXISTED_IP_MESSAGE;
                SendMessage(ipAddress,port, message);
            }
            else
            {
                bool isSent = false;
                string submissionURL = "http://" + PE2A_WF_Student.Util.GetLocalIPAddress() + ":8080/api/submission";
                string[] row = new string[] { ++count + "", studentID, "", Constant.STATUSLIST[0] };
                dataTable.Rows.Add(row);
                while (!isSent) 
                {
                    try
                    {
                        message ="here is your submission url =" + submissionURL;
                        SendMessage(ipAddress, port, message);
                        StudentDTO newStudent = new StudentDTO(studentID, IPAddress.Parse(ipAddress), port);
                        listStudent.Add(newStudent);
                        isSent = true;
                        this.InvokeEx(f =>
                        dgvStudent.DataSource = dataTable);
                        this.InvokeEx(f => dgvStudent.Rows[count - 1].Cells[3].Style.ForeColor = Color.Green);
                    }
                    catch (Exception e)
                    {
                        // resent message
                    }
                }
            }

        }
        private bool IsConnected(string ipAddress)
        {
            foreach (var student in listStudent)
            {
                if (student.IpAddress.ToString().Equals(ipAddress))
                {
                    return true;
                }
            }
            return false;
        }

        private void SendMessage(string ipAddress, int port, string message)
        {
            try
            {
                IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(iPEnd);
                    socket.Send(Encoding.UTF8.GetBytes(message));
                    socket.Dispose();
                }
              
            }
            catch(Exception e)
            {
                MessageBox.Show(Constant.CLIENT_SOCKET_CLOSED_MESSAGE);
            }
        }


        private void GetResultFromFile() {
            String directory = AppDomain.CurrentDomain.BaseDirectory + @"Results.txt";
            var getFile = File.ReadAllText(directory).Replace("\n", "").Replace("\r", "");
            for (int i = 0; i < getFile.Length; i++)
            {
                int startIndex = getFile.IndexOf("//" + i);
                int endIndex = getFile.IndexOf("//end" + i) - startIndex;

                if (startIndex != -1 && endIndex != -1)
                {
                    String startString = getFile.Substring(startIndex, endIndex);
                    int startIndexCode = startString.IndexOf("SE");
                    int endIndexCode = startString.IndexOf("(StudentCode)") - startIndexCode;
                    String studentCode = startString.Substring(startIndexCode, endIndexCode);
                    int startIndexResult = startString.IndexOf("Result") + 8; // skip word result 
                    int endIndexResult = startString.IndexOf("(Point)") - startIndexResult;
                    String studentPoint = startString.Substring(startIndexResult, endIndexResult);
                    String splitPassed = studentPoint.Split('/')[0];
                    String splitTotal = studentPoint.Split('/')[1];
                    Double point = (Double.Parse(splitPassed) / Double.Parse(splitTotal)) * 10;
                    foreach(var item in listStudent)
                    {
                        if (studentCode.ToUpper().Trim().Equals(item.StudentID.ToUpper()))
                        {
                            item.Point = point;
                        }
                    }
                  
                }
            };
        }
    }
}
