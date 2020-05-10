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

namespace PE2A_WF_Student
{
    public partial class ClientForm : Form
    {
        Socket clientSock;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            IPAddress[] ipAddress = Dns.GetHostAddresses("192.168.1.15");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            clientSock.Connect(ipEnd);
            // hear for receiving file
          
        }
        public void GetFile()
        {
            byte[] clientData = new byte[1024 * 5000];
            string receivedPath = @"G:\JavaWeb\JAV WEB";
            int receivedBytesLen = clientSock.Receive(clientData);
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
            BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + fileName, FileMode.Append));
            bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
            bWrite.Close();
            clientSock.Close();
        }
    }
}
