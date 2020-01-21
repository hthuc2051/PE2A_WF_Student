using PE2A_WF_Student;
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
    public partial class ServerForm : Form
    {
        Socket sock;
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 5656); // open the endport with pre defined ip
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // create socket for IP
            sock.Bind(ipEnd); // hear endport ip.
            sock.Listen(100);
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
       
            //run client form
            ClientForm clientForm = new ClientForm();
            clientForm.Show();
            //wait for connecting
            Socket clientSocket = sock.Accept();
 
            // load file
            string fileName = "Cloud.txt";// "Your File Name"; 
            string filePath = @"G:\JavaWeb\";//Your File Path;
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
            byte[] fileData = File.ReadAllBytes(filePath + fileName);
            byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
            fileNameLen.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);
            //send file to client
            clientSocket.Send(clientData);
            clientForm.GetFile();
            clientSocket.Close();
         
        }
    }
}
