using PE2A_WF_Lecturer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE2A_WF_Lecturer
{
    public partial class ShowResult : Form
    {
        private List<UserResult> userResults;
        private Socket sock;
        private List<Socket> clientSocks;
        private Form preForm;
        public ShowResult()
        {
            InitializeComponent();
        }
        // thêm cái này thôi =>
        public ShowResult(List<Socket> clientSockets,  Socket serverSocket,Form prevForm)
        {
            InitializeComponent();
            this.sock = serverSocket;
            this.clientSocks = clientSockets;
            this.preForm = prevForm;
        }

        private void ShowResult_Load(object sender, EventArgs e)
        {
            Reload();
           // StartServer();
        }   
        private void Reload()
        {
            userResults = new List<UserResult>();
            String directory = AppDomain.CurrentDomain.BaseDirectory + @"Results.txt";
            Console.WriteLine(directory);
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
                    Double point = (Double.Parse(splitPassed) / Double.Parse(splitTotal)) * 100;
                    var userResult = new UserResult
                    {
                        StudentCode = studentCode,
                        Point = point
                    };
                    userResults.Add(userResult);

                }
            };
            tbData.DataSource = userResults;
        }
        //private void StartServer()
        //{
        //    Task.Run(() =>
        //    {
        //        IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 5656);
        //        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        //        sock.Bind(ipEnd);
        //        sock.Listen(100);
        //        Console.WriteLine("Server starting...");
        //        while(true)
        //        {
        //            clientSock = sock.Accept();
        //            if(clientSock != null)
        //            {
        //                Console.WriteLine("Client connecting...");

        //            }
        //        }
        //    });
        //}

        private void btnPublish_Click(object sender, EventArgs e)
        {
            //test with one ip
            //Thien must store a list Socket when student connect to server.
            foreach (var item in clientSocks)
            {               
                    string studentCode = "Student_Code : SE62882 Student_Point : 10";// "Your student code";                                                                                    //string studentPoint = "10";
                    byte[] clientStudentCodeByte = Encoding.ASCII.GetBytes(studentCode);
                    byte[] clientStudentCodeLen = BitConverter.GetBytes(clientStudentCodeByte.Length);
                    //byte[] clientStudentPointByte = Encoding.ASCII.GetBytes(studentPoint);
                    //byte[] clientStudentPointLen = BitConverter.GetBytes(clientStudentPointByte.Length);
                    byte[] clientData = new byte[4 + clientStudentCodeByte.Length];
                    clientStudentCodeLen.CopyTo(clientData, 0);
                    clientStudentCodeByte.CopyTo(clientData, 4);
                    item.Send(clientData);                
            }            
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }
    }
}
