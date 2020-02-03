using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PE2A_WF_Student
{
    public class Util
    {
        public static void SendBroadCast(String message, int receiverListeningPort)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Broadcast, receiverListeningPort);
            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            clientSock.SetSocketOption(SocketOptionLevel.Socket,
            SocketOptionName.Broadcast, 1);
            clientSock.SendTo(bytes, ipEnd);
            clientSock.Close();
        }
         private static Socket listeningSocket;
        public static string GetMessageFromTCPConnection(int listeningPort, int maximumRequest)
        {
            listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Byte[] buffer = new byte[1024];
            IPEndPoint senders = new IPEndPoint(IPAddress.Any, listeningPort);
            listeningSocket.Bind(senders);
            listeningSocket.Listen(maximumRequest);
            Socket conn = listeningSocket.Accept();
            int size = conn.Receive(buffer);
            ASCIIEncoding eEncpding = new ASCIIEncoding();
            string receivedMessage = eEncpding.GetString(buffer);
            listeningSocket.Close();
            return receivedMessage.Substring(0, size);
        }

       

        public static IPAddress GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName();
            string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return IPAddress.Parse(ip);
        }

    }
}
